using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;

namespace Diablo3_AutoAttack
{
    unsafe class KeyMouseEvents
    {
        #region "局部模擬"
        [DllImport("user32", EntryPoint = "PostMessageA")]
        public static extern bool PostMessage(IntPtr hwnd, uint wMsg, UIntPtr wParam, IntPtr lParam);
        [DllImport("user32", EntryPoint = "MapVirtualKeyA")]
        public static extern uint MapVirtualKey(uint wCode, uint wMapType);
        #endregion

        #region "定義常數"
        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;
        private const int VK_C = 0x43;
        private const int WM_CLOSE = 0x10;
        private const int WM_LBUTTONDBLCLK = 0x203;
        private const int WM_LBUTTONDOWN = 0x201;
        private const int WM_LBUTTONUP = 0x202;
        private const int WM_MBUTTONDBLCLK = 0x209;
        private const int WM_MBUTTONDOWN = 0x207;
        private const int WM_MBUTTONUP = 0x208;
        private const int WM_RBUTTONDBLCLK = 0x206;
        private const int WM_RBUTTONDOWN = 0x204;
        private const int WM_RBUTTONUP = 0x205;
        private const int WM_MOUSEACTIVATE = 0x21;
        private const int WM_MOUSEFIRST = 0x200;
        private const int WM_MOUSELAST = 0x209;
        private const int WM_MOUSEMOVE = 0x200;
        private const int WM_SETCURSOR = 0x20;
        private const int KBC_KEY_CMD = 0x64; //鍵盤命令埠
        private const int KBC_KEY_DATA = 0x60; //鍵盤資料埠
        private const int KEYEVENTF_KEYUP = 0x2;
        #endregion
        #region "全域模擬"
        [DllImport("user32")]
        public static extern int SetCursorPos(int X, int Y);
        [DllImport("user32")]
        public static extern int GetCursorPos(ref PointAPI lpPoint);
        //滑鼠座標資料結構
        public struct PointAPI
        {
            public int CurX,CurY;
            public PointAPI(int x,int y)
            {
                CurX = x;
                CurY = y;
            }
        }
        [DllImport("user32")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        /* dwFlags說明
            * 1 移動事件
            * 2 左鍵按下
            * 4 左鍵放開
            * 8 右鍵按下
            * 16 右鍵放開
            * 32 中鍵按下
            * 64 中鍵放開
            * 2048 滾輪
            */
        [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, IntPtr dwExtraInfo);
        #endregion

        #region "驅動層級模擬"
        private static IntPtr hMod; //接入點
        [DllImport("kernel32")]
        private static extern IntPtr LoadLibrary(String DllName);
        [DllImport("kernel32")]
        private static extern IntPtr GetProcAddress(IntPtr hModule, String ProcName);
        [DllImport("kernel32")]
        private static extern bool FreeLibrary(IntPtr hModule);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate bool InitializeWinIoType();
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate bool ShutdownWinIoType();
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate bool GetPortValType(UInt16 PortAddr, UInt32* pPortVal, UInt16 Size);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate bool SetPortValType(UInt16 PortAddr, UInt32 PortVal, UInt16 Size);

        /// <summary>
        /// 初始化WinIO
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool InitializeDriver()
        {
            // Check if this is a 32 bit or 64 bit system
            if (IntPtr.Size == 4)
            {
                hMod = LoadLibrary("WinIo32.dll");
            }
            else if (IntPtr.Size == 8)
            {
                hMod = LoadLibrary("WinIo64.dll");
            }

            if (hMod == IntPtr.Zero)
            {
                MessageBox.Show("Can't find WinIo dll (WinIo32.dll or WinIo64.dll).\nMake sure the WinIo library files are located in the same directory as your executable file.");
                return false;
            }

            IntPtr pFunc = GetProcAddress(hMod, "InitializeWinIo");

            if (pFunc != IntPtr.Zero)
            {
                InitializeWinIoType InitializeWinIo = (InitializeWinIoType)Marshal.GetDelegateForFunctionPointer(pFunc, typeof(InitializeWinIoType));
                bool Result = InitializeWinIo();

                if (!Result)
                {
                    MessageBox.Show("Error returned from InitializeWinIo.\nMake sure you are running with administrative privileges and that the WinIo library files are located in the same directory as your executable file.");
                    FreeLibrary(hMod);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 卸載WinIO
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool ShutdownDriver()
        {
            IntPtr pFunc = GetProcAddress(hMod, "ShutdownWinIo");

            if (pFunc != IntPtr.Zero)
            {
                ShutdownWinIoType ShutdownWinIo = (ShutdownWinIoType)Marshal.GetDelegateForFunctionPointer(pFunc, typeof(ShutdownWinIoType));
                ShutdownWinIo();
                FreeLibrary(hMod);
            }
            return true;
        }

        //等待鍵盤緩衝區為空
        private static void KBCWait4IBE()
        {
            IntPtr pFunc = GetProcAddress(hMod, "GetPortVal");
            if (pFunc != IntPtr.Zero)
            {
                GetPortValType GetPortVal = (GetPortValType)Marshal.GetDelegateForFunctionPointer(pFunc, typeof(GetPortValType));
                UInt32 PortVal;
                do
                {
                    bool flag = GetPortVal(0x64, &PortVal, 1);
                }
                while ((PortVal & 0x2) > 0);
            }
        }
        #endregion

        //產生掃瞄碼
        private static IntPtr MakeKeyLparam(uint VirtualKey, int flag)
        {
            string Firstbyte = "";
            //lparam参数的24-31位            
            if (flag == WM_KEYDOWN)
            {
                Firstbyte = "00";
            }
            else
            {
                int First = 12 * 16;
                Firstbyte = First.ToString();
            }
            uint Scancode = 0;
            Scancode = MapVirtualKey(VirtualKey, 0); //獲得鍵的掃描碼 
            string Secondbyte = "";
            string Temp = "00" + Scancode.ToString(); //lparam參數的16-23位元，即虛擬鍵掃描碼     
            Secondbyte = Temp.Substring(Temp.Length - 2, 2);
            string s = Firstbyte + Secondbyte + "0001";   //0001為lparam參數的0-15位，即發送次數和其他擴展資訊         
            uint Back = 0;
            uint.TryParse(s, out Back);
            return (IntPtr) Back; 
        }

        #region ! Win32 API |
            [DllImport("user32.dll")]
            public static extern IntPtr GetForegroundWindow();
        #endregion

        #region "鍵盤事件"
        /// <summary>
        /// 按住指定按鍵
        /// </summary>
        /// <param name="keyCode">指定按鍵</param>
        /// <param name="mode">局域模擬0，全局模擬1，驅動層級模擬2</param>
        /// <param name="hwnd">目標視窗hwnd</param>
        /// <remarks></remarks>
        public static void keyDown(System.Windows.Forms.Keys key, int mode, IntPtr hwnd)
        {
            UIntPtr keyCode = (UIntPtr)key;
            switch (mode)
            {
                case 0:
                    PostMessage(hwnd, WM_KEYDOWN, keyCode, MakeKeyLparam((uint)keyCode, WM_KEYDOWN));
                    break;
                case 1: 
                    keybd_event((byte)keyCode, 0, 0, IntPtr.Zero);
                    break;
                case 2:
                    IntPtr pFunc = GetProcAddress(hMod, "SetPortVal");
                    if (pFunc != IntPtr.Zero)
                    {
                        SetPortValType SetPortVal = (SetPortValType)Marshal.GetDelegateForFunctionPointer(pFunc, typeof(SetPortValType));
                        uint btScancode = 0;
                        btScancode = MapVirtualKey( (uint)keyCode, 0);
                        KBCWait4IBE(); // 等待鍵盤緩衝區為空
                        SetPortVal(KBC_KEY_CMD, 0xD2, 1);// ''發送鍵盤寫入命令
                        KBCWait4IBE();
                        SetPortVal(KBC_KEY_DATA, (uint)btScancode, 1);// ''寫入按下鍵
                    }
                    break;
            }
        }

        /// <summary>
        /// 指定按鍵彈起
        /// </summary>
        /// <param name="keyCode">指定按鍵</param>
        /// <param name="mode">局域模擬0，全局模擬1，驅動層級模擬2</param>
        /// <param name="hwnd">目標視窗hwnd</param>
        /// <remarks></remarks>
        public static void keyUp(System.Windows.Forms.Keys key, int mode, IntPtr hwnd)
        {
            UIntPtr keyCode = (UIntPtr)key;
            switch (mode)
            {
                case 0:
                    PostMessage(hwnd, WM_KEYUP, keyCode, MakeKeyLparam((uint)keyCode, WM_KEYUP));
                    break;
                case 1:
                    keybd_event((byte)keyCode, 0, KEYEVENTF_KEYUP, IntPtr.Zero);
                    break;
                case 2:
                    IntPtr pFunc = GetProcAddress(hMod, "SetPortVal");
                    if (pFunc != IntPtr.Zero)
                    {
                        SetPortValType SetPortVal = (SetPortValType)Marshal.GetDelegateForFunctionPointer(pFunc, typeof(SetPortValType));
                        uint btScancode = 0;
                        btScancode = MapVirtualKey((uint)keyCode, 0);
                        KBCWait4IBE(); // ''等待鍵盤緩衝區為空
                        SetPortVal(KBC_KEY_CMD, 0xD2, 1); //''發送寫入命令
                        KBCWait4IBE();
                        SetPortVal(KBC_KEY_DATA, (uint)btScancode | 0x80, 1);// ''寫入釋放鍵
                    }
                    break;
            }
        }

        /// <summary>
        /// 按下指定按鍵
        /// </summary>
        /// <param name="keyCode">指定按鍵</param>
        /// <param name="mode">局域模擬0，全局模擬1，驅動層級模擬2</param>
        /// <param name="hwnd">目標視窗hwnd</param>
        public static void keyPress(System.Windows.Forms.Keys key, int mode, IntPtr hwnd)
        {
            keyDown(key, mode, hwnd);
            Thread.Sleep(20);
            keyUp(key, mode, hwnd);
        }
        #endregion

        #region "滑鼠事件"
        public static void mouseLClick()
        {
            mouse_event(2, 0, 0, 0, 0);
            Thread.Sleep(70);
            mouse_event(4, 0, 0, 0, 0);
        }

        public static void mouseRClick()
        {
            mouse_event(8, 0, 0, 0, 0);
            Thread.Sleep(70);
            mouse_event(16, 0, 0, 0, 0);
        }

        public static void BgdmouseLClick(IntPtr hwnd, IntPtr lparam)
        {
            PostMessage(hwnd, WM_LBUTTONDOWN, (UIntPtr)1, lparam);
            PostMessage(hwnd, WM_LBUTTONUP, (UIntPtr)0, lparam);
        }

        public static void BgdmouseLDown(IntPtr hwnd, IntPtr lparam)
        {
            PostMessage(hwnd, WM_LBUTTONDOWN, (UIntPtr)1, lparam);
        }

        public static void BgdmouseLUp(IntPtr hwnd, IntPtr lparam)
        {
            PostMessage(hwnd, WM_LBUTTONUP, (UIntPtr)0, lparam);
        }

        public static void BgdmouseRClick(IntPtr hwnd, IntPtr lparam)
        {
            PostMessage(hwnd, WM_RBUTTONDOWN, (UIntPtr)2, lparam);
            PostMessage(hwnd, WM_RBUTTONUP, (UIntPtr)0, lparam);
        }

        public static void BgdmouseRDown(IntPtr hwnd, IntPtr lparam)
        {
            PostMessage(hwnd, WM_RBUTTONDOWN, (UIntPtr)2, lparam);
        }

        public static void BgdmouseRUp(IntPtr hwnd, IntPtr lparam)
        {
            PostMessage(hwnd, WM_RBUTTONUP, (UIntPtr)0, lparam);
        }

        public static void BgdmouseMove(IntPtr hwnd, IntPtr lparam)
        {
            PostMessage(hwnd, WM_MOUSEMOVE, (UIntPtr)0, lparam);
        }
        #endregion
    }
}
