using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diablo3_AutoAttack
{
    
    public partial class frmD3_AutoAttacker : Form
    {
        #region "Field"
        private IntPtr hwnd;
        private Thread Work;
        private Thread Detector;
        private Random rndGenerator = new Random(Guid.NewGuid().GetHashCode()); //以Guid作為種子產生亂數
        private int delay;
        private const int WM_HOTKEY = 0x312;
        private bool isRunning = false;
        private delegate void InvokeDelegate();
        #endregion

        #region "WindowsAPI"
        [DllImport("user32")]
        public static extern int RegisterHotKey(IntPtr hwnd, int id, uint fsModifiers, uint vk);
        [DllImport("user32")]
        public static extern int UnregisterHotKey(IntPtr hwnd, int id);
        #endregion

        public frmD3_AutoAttacker()
        {
            InitializeComponent();
        }

        private void frmD3_AutoAttacker_Load(object sender, EventArgs e) 
        {
            //initiate
            delay = 100;
            txtDelay.Text = delay.ToString();
            RegisterHotKey(this.Handle, 1, 0, (uint)System.Windows.Forms.Keys.F1);
            RegisterHotKey(this.Handle, 2, 0, (uint)System.Windows.Forms.Keys.F2);
            txtHotkeyStart.Text = "F1";
            txtHotkeyAbort.Text = "F2";
        }

        private void frmD3_AutoAttacker_Closing(object sender, FormClosingEventArgs e)
        {
            UnregisterHotKey(this.Handle, 1);
            UnregisterHotKey(this.Handle, 2);
            KeyMouseEvents.ShutdownDriver();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            delay += 10;
            txtDelay.Text = delay.ToString();
        }

        private void btnMiuns_Click(object sender, EventArgs e)
        {
            if (delay > 10)
            {
                delay -= 10;
                txtDelay.Text = delay.ToString();
            }
        }

        private void frmD3_AutoAttacker_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 107)
            {
                delay += 10;
                txtDelay.Text = delay.ToString();
            }
            else if (e.KeyValue == 109)
            {
                if (delay > 10)
                {
                    delay -= 10;
                    txtDelay.Text = delay.ToString();
                }
            }
        }

        private void txtHotkeyAbort_KeyDown(object sender, KeyEventArgs e)
        {
            UnregisterHotKey(this.Handle, 2);
            RegisterHotKey(this.Handle, 2, 0, (uint)e.KeyCode);
            txtHotkeyAbort.Text = e.KeyCode.ToString();
        }

        private void txtHotkeyStart_KeyDown(object sender, KeyEventArgs e)
        {
            UnregisterHotKey(this.Handle, 1);
            RegisterHotKey(this.Handle, 1, 0, (uint)e.KeyCode);
            txtHotkeyStart.Text = e.KeyCode.ToString();
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            try
            {
                if (m.Msg == WM_HOTKEY)
                {
                    switch (m.WParam.ToInt32())
                    {
                        case 1:
                            if (chkHotkeyFun.Checked)
                            {
                                hwnd = KeyMouseEvents.GetForegroundWindow();
                                KeyMouseEvents.keyPress(System.Windows.Forms.Keys.F1, 0, hwnd);
                            }
                            if (isRunning)
                            {
                                return;
                            }
                            hwnd = WindowsInfo.FindWindow("D3 Main Window Class",null);
                            if (hwnd == IntPtr.Zero)
                            {
                                MessageBox.Show("找不到Diablo 3視窗");
                                return;
                            }
                            if (hwnd != WindowsInfo.GetForegroundWindow())
                            {
                                MessageBox.Show("Diablo 3並不是前景視窗");
                                return;
                            }
                            if (!chk1.Checked && !chk2.Checked && !chk3.Checked && !chk4.Checked && !chkLeft.Checked && !chkRight.Checked)
                            {
                                MessageBox.Show("請選擇按鍵");
                                return;
                            }
                            isRunning = true;
                            Work = new System.Threading.Thread(this.doWork);
                            Detector = new System.Threading.Thread(this.detection);
                            Work.Start();
                            Detector.Start();
                            break;
                        case 2:
                            if (chkHotkeyFun.Checked)
                            {
                                hwnd = KeyMouseEvents.GetForegroundWindow();
                                KeyMouseEvents.keyPress(System.Windows.Forms.Keys.F2, 0, hwnd);
                            }
                            if (isRunning)
                            {
                                Work.Abort();
                                Detector.Abort();
                                //release keyboard and mouse
                                if (chk1.Checked)
                                    KeyMouseEvents.keyPress(System.Windows.Forms.Keys.D1, 0, hwnd);
                                if (chk2.Checked)
                                    KeyMouseEvents.keyPress(System.Windows.Forms.Keys.D2, 0, hwnd);
                                if (chk3.Checked)
                                    KeyMouseEvents.keyPress(System.Windows.Forms.Keys.D3, 0, hwnd);
                                if (chk4.Checked)
                                    KeyMouseEvents.keyPress(System.Windows.Forms.Keys.D4, 0, hwnd);
                                if (chkLeft.Checked)
                                    KeyMouseEvents.mouseLClick();
                                if (chkRight.Checked)
                                    KeyMouseEvents.mouseRClick();
                                isRunning = false;
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + ": " + ex.Message);
            }
            finally
            {
                base.WndProc(ref m);
            }
        }

        private void detection()
        {
            while (true)
            {
                if (hwnd != WindowsInfo.GetForegroundWindow())
                {
                    Work.Abort();
                    isRunning = false;
                    break;
                }
            }
        }

        private void doWork()
        {
            //Create hotkey sequence (action means a delegate without parameter and return void, each action pointer to a method)
            List<Action> hotkeys = new List<Action>();
            if (chk1.Checked)
                hotkeys.Add(() => KeyMouseEvents.keyPress(System.Windows.Forms.Keys.D1, 0, hwnd));
            if (chk2.Checked)
                hotkeys.Add(() => KeyMouseEvents.keyPress(System.Windows.Forms.Keys.D2, 0, hwnd));
            if (chk3.Checked)
                hotkeys.Add(() => KeyMouseEvents.keyPress(System.Windows.Forms.Keys.D3, 0, hwnd));
            if (chk4.Checked)
                hotkeys.Add(() => KeyMouseEvents.keyPress(System.Windows.Forms.Keys.D4, 0, hwnd));
            if (chkLeft.Checked)
                hotkeys.Add(() => KeyMouseEvents.mouseLClick());
            if (chkRight.Checked)
                hotkeys.Add(() => KeyMouseEvents.mouseLClick());
            while (true)
            {
                hotkeys[rndGenerator.Next(hotkeys.Count)].Invoke();
                //Generate a delay factor between 0.5-1.0
                Thread.Sleep((int)(rndGenerator.Next(5,11) /10 * delay));
            }
        }
    }
}
