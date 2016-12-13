using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Diablo3_AutoAttack
{
    class WindowsInfo
    {
        //視窗四角座標
        private int x0;
        private int y0;
        private int x1;
        private int y1;
        //視窗左上座標(不含標題邊框)
        private int Client_x0;
        private int Client_y0;
        //視窗長寬(不含標題邊框)
        private int ClientX;
        private int ClientY;
        //視窗邊框寬度
        private int Border;
        //視窗標題高度
        private int Title;
        public struct RECT
        {
            public int x1,y1,x2,y2;
        }
        
        //設定視窗用API
        [DllImport("user32")] 
        public static extern int SetWindowPos(IntPtr hwnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);
        /*
         * hWnd: HWND            窗口句柄
         * hWndInsertAfter: HWND 窗口的 Z 順序
         * X, Y: Integer         位置
         * cx, cy: Integer       大小
         * uFlags: UINT          選項
         */

        //使視窗獲得焦點
        [DllImport("user32")]
        public static extern int SetForegroundWindow(IntPtr hwnd);
        //取得視窗資訊用API
        [DllImport("user32", EntryPoint = "GetWindowLongA")]
        public static extern int GetWindowLong(IntPtr hwnd, int nIndex);
        //設定視窗用API
        [DllImport("user32", EntryPoint = "SetWindowLongA")]
        public static extern int SetWindowLong(IntPtr hwnd, int nIndex, int dwNewLong);
        //找出視窗hwnd用API
        [DllImport("user32", EntryPoint = "FindWindowA")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        //hWnd1若有帶值，表是從某個Window內找裡面的Window，若帶0，則找所有的Window；hWnd2：表示從那一個Window內的Window開始找起，若帶0，則由第一個開始找；lpsz1為Class名稱；lpsz2為WindowText（或是Form的Title）
        [DllImport("user32", EntryPoint = "FindWindowExA")]
        public static extern IntPtr FindWindowEx(IntPtr hWnd1, IntPtr hWnd2, string lpsz1, string lpsz2);
        //取得視窗大小(含邊框，功能表)
        [DllImport("user32")]
        public static extern int GetWindowRect(IntPtr hWnd, ref RECT rectangle);
        //取得視窗大小(不含邊框，功能表，左上角為0,0)
        [DllImport("user32")]
        public static extern int GetClientRect(IntPtr hwnd, ref RECT lpRect);
        //取得目前前景視窗hwnd
        [DllImport("user32")]
        public static extern IntPtr GetForegroundWindow();
        //取得目前視窗某點顏色相關API
        [DllImport("user32")]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("gdi32")]
        public static extern int GetPixel(IntPtr hdc, int x, int y);
        [DllImport("user32")]
        public static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

        #region "常數定義"
        #region "WindowPos常數"
        //hWndInsertAfter 參數可選值:
        private const int HWND_TOP = 0;//在前面
        private const int HWND_BOTTOM = 1;//在後面
        private const int HWND_TOPMOST = -1;//在前面, 位於任何頂部窗口的前面
        private const int HWND_NOTOPMOST = -2;//在前面, 位於其他頂部窗口的後面
        ////uFlags 參數可選值:
        private const uint SWP_NOSIZE = 1;//忽略 cx、cy, 保持大小
        private const uint SWP_NOMOVE = 2;//忽略 X、Y, 不改變位置
        private const uint SWP_NOZORDER = 4;//忽略 hWndInsertAfter, 保持 Z 順序
        private const uint SWP_NOREDRAW = 8; //不重繪
        private const uint SWP_NOACTIVATE = 0x10;//不激活
        private const uint SWP_FRAMECHANGED = 0x20;//強制發送 WM_NCCALCSIZE 消息, 一般只是在改變大小時才發送此消息
        private const uint SWP_SHOWWINDOW = 0x40;//顯示窗口
        private const uint SWP_HIDEWINDOW = 0x80;//隱藏窗口
        private const uint SWP_NOCOPYBITS = 0x100;//丟棄客戶區
        private const uint SWP_NOOWNERZORDER = 0x200;//忽略 hWndInsertAfter, 不改變 Z 序列的所有者
        private const uint SWP_NOSENDCHANGING = 0x400;//不發出 WM_WINDOWPOSCHANGING 消息
        private const uint SWP_DRAWFRAME = SWP_FRAMECHANGED;//畫邊框
        private const uint SWP_NOREPOSITION = SWP_NOOWNERZORDER;
        private const uint SWP_DEFERERASE = 0x2000;//防止產生 WM_SYNCPAINT 消息
        private const uint SWP_ASYNCWINDOWPOS = 0x4000;//若調用進程不擁有窗口, 系統會向擁有窗口的線程發出需求
        #endregion
        #region"WindowLong常數"
        //nIndex = -20 (GWL_EXSTYLE)：設定視窗額外的樣式
        private const int WS_EX_ACCEPTFILES = 0x00000010;//設定能接收檔案總管拖曳過來檔案的樣式
        private const int WS_EX_TOPMOST = 0x00000008;//設定最上層顯示(Top Most)
        private const int WS_EX_TRANSPARENT = 0x00000020;//設定視窗不會自動重繪，直到被視窗覆蓋下的視窗本身做重繪時才會跟著重繪
        private const int WS_EX_NOACTIVATE = 0x8000000;//設定縮小後，不會收到Taskbar裡
        private const int WS_EX_APPWINDOW = 0x40000;//設定縮小後，會收到Taskbar裡 
        //nIndex = -16 (GWL_STYLE)：設定視窗的樣式
        private const int WS_BORDER = 0x800000;//擁有邊框(Thin Border)
        private const int WS_CAPTION = 0xc00000;//擁有標題列(Title)，可讓視窗移動
        private const int WS_CHILDWINDOW = 0x40000000;//設定為子視窗(ChildWindow)
        private const int WS_DISABLED = 0x8000000;//設定為Disabled
        private const int WS_THICKFRAME = 0x40000;//設定為具有改變視窗大小的邊框
        private const int WS_SYSMENU = 0x80000;//設定有系統功能表，要與WS_CAPTION一起使用
        private const int WS_MINIMIZEBOX = 0x20000;//設定有最小化按鈕，要與WS_SYSMENU一起使用
        private const int WS_MAXIMIZEBOX = 0x10000;//設定有最大化按鈕，要與WS_SYSMENU一起使用
        private const int WS_DLGFRAME = 0x400000;//設定有邊框(Border)，但不可以搭配WS_CAPTION一起使用
        private const int WS_HSCROLL = 0x100000;//設定有水平捲軸
        private const int WS_VSCROLL = 0x200000;//設定有垂直捲軸 
        #endregion
        #endregion

        //取得視窗資訊
        public void GetWindowInfo()
        {
            RECT R = new RECT();
            int RetVal = 0;
            RetVal = GetWindowRect(GetForegroundWindow(), ref R);
            x0 = R.x1;
            x1 = R.x2;
            y0 = R.y1;
            y1 = R.y2;
            RetVal = GetClientRect(GetForegroundWindow(), ref R);
            ClientX = R.x2;
            ClientY = R.y2;
            Border = ((x1 - x0) - ClientX) / 2;
            Title = (y1 - y0) - ClientY - Border;
            Client_x0 = x0 + Border;
            Client_y0 = y0 + Title;
        }

        public void GetWindowInfo(IntPtr hwnd)
        {
            RECT R = new RECT();
            int RetVal = 0;
            RetVal = GetWindowRect(hwnd, ref R);
            x0 = R.x1;
            x1 = R.x2;
            y0 = R.y1;
            y1 = R.y2;
            RetVal = GetClientRect(hwnd, ref R);
            ClientX = R.x2;
            ClientY = R.y2;
            Border = ((x1 - x0) - ClientX) / 2;
            Title = (y1 - y0) - ClientY - Border;
            Client_x0 = x0 + Border;
            Client_y0 = y0 + Title;
        }
    }
}
