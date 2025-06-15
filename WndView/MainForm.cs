using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using static WndView.Enums;
using static WndView.Functions;
using static WndView.INIFunction;
using static WndView.Methods;
using static WndView.Structs;




namespace WndView
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            //string cultureCode = "en";
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureCode);
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCode);

            InitializeComponent();

            string settingsDir = Path.GetDirectoryName(ProfilePath) ?? "";
            string defaultIniPath = Path.Combine(Application.StartupPath, "settings_default.ini");
            Directory.CreateDirectory(settingsDir);
            if (!File.Exists(ProfilePath) && File.Exists(defaultIniPath))
            {
                File.Copy(defaultIniPath, ProfilePath, true);
            }

            this.BackColor = Color.DimGray;
            Reset();
            LoadIniAndSetHotkey();

        }

        IntPtr Thumb;
        bool ResizeForm = false;
        int FormMargins = 1;
        int initialDwmHeight, initialDwmWidth;
        int currentDwmHeight, currentDwmWidth;

        #region 全域快捷鍵
        private HotkeyManager hotkey = null!;
        private void LoadIniAndSetHotkey()
        {
            hotkey = new HotkeyManager();
            RegHotkey("SetKey_ClickThrough", "ClickThrough");
            RegHotkey("SetKey_Show", "Show");
            RegHotkey("SetKey_Hide", "Hide");
            RegHotkey("SetKey_Exit", "Exit");
        }

        private void RegHotkey(string RegHotkeyName, string settingName)
        {
            string iniValue1 = GPPS("HotKeysEnable", settingName);
            string iniValue = GPPS("HotKeys", settingName);
            if (bool.TryParse(iniValue1, out bool result))
            {
                if (result)
                {
                    if (uint.TryParse(iniValue, out uint hotkeyValue))
                    {
                        uint modifiers = hotkeyValue >> 16;
                        Keys keyCode = (Keys)(hotkeyValue & 0xFFFF);

                        switch (RegHotkeyName)
                        {
                            case "SetKey_ClickThrough":
                                hotkey.Register(keyCode, modifiers, () =>
                                {
                                    ClickThrough();
                                });
                                break;

                            case "SetKey_Show":
                                hotkey.Register(keyCode, modifiers, () =>
                                {
                                    FormShow();
                                });
                                break;

                            case "SetKey_Hide":
                                hotkey.Register(keyCode, modifiers, () =>
                                {
                                    this.Hide();
                                });
                                break;

                            case "SetKey_Exit":
                                hotkey.Register(keyCode, modifiers, () =>
                                {
                                    Close();
                                });
                                break;
                        }
                    }
                }
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            hotkey?.Dispose();
            base.OnFormClosed(e);
        }
        #endregion

        #region 取得程式清單
        private void GetWindows()
        {
            //保留項目的Name
            var keepNames = new HashSet<string> { 
                "FormMenu_None",
                "FormMenu_Exit",
                "toolStripSeparator5"
            };
            //逐步刪除非保留的項目
            for (int i = FormMenu.Items.Count - 1; i >= 0; i--)
            {
                if (!keepNames.Contains(FormMenu.Items[i].Name ?? ""))
                {
                    FormMenu.Items.RemoveAt(i);
                }
            }
            EnumWindows(EnumWindowCallback, 0);
        }

        //加入程式項目
        private bool EnumWindowCallback(IntPtr hWnd, int lParam)
        {
            //項目篩選
            if (hWnd == this.Handle) return true;
            if (!IsWindowValidForCapture(hWnd)) return true;

            //取得名稱
            StringBuilder sb = new StringBuilder(256);
            GetWindowText(hWnd, sb, sb.Capacity);

            //取得ICON
            Icon gIcon = GetAppIcon(hWnd);

            //項目處理及添加
            ToolStripMenuItem nItem = new ToolStripMenuItem();
            int maxitemlength = 20; //最大顯示字數
            if (sb.Length > maxitemlength) 
            {
                nItem.Text = sb.ToString(0, maxitemlength) + " ...";
            }
            else
            {
                nItem.Text = sb.ToString();
            }
            if (gIcon != null) 
            { 
                nItem.Image = gIcon.ToBitmap();
                gIcon.Dispose();
            }
            nItem.Tag = hWnd;
            int insertIndex = Math.Max(FormMenu.Items.Count - 2, 0); //加入位置
            FormMenu.Items.Insert(insertIndex, nItem);

            return true;
        }
        #endregion

        #region 視窗控制
        const int _ = 5; //邊距
        Rectangle RectangleCentre { get { return new Rectangle(_, _, this.ClientSize.Width - _ - _, this.ClientSize.Height - _ - _); } }
        Rectangle RectangleTop { get { return new Rectangle(0, 0, this.ClientSize.Width, _); } }
        Rectangle RectangleLeft { get { return new Rectangle(0, 0, _, this.ClientSize.Height); } }
        Rectangle RectangleBottom { get { return new Rectangle(0, this.ClientSize.Height - _, this.ClientSize.Width, _); } }
        Rectangle RectangleRight { get { return new Rectangle(this.ClientSize.Width - _, 0, _, this.ClientSize.Height); } }

        //事件_滑鼠按下
        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var cursor = this.PointToClient(Cursor.Position);
                if (RectangleCentre.Contains(cursor)) { ReleaseCapture(); SendMessage(Handle, (uint)0xA1, (int)HT.HT_CAPTION, 0); }
                else if (RectangleTop.Contains(cursor)) { ReleaseCapture(); SendMessage(Handle, (uint)0xA1, (int)HT.HT_TOP, 0); }
                else if (RectangleLeft.Contains(cursor)) { ReleaseCapture(); SendMessage(Handle, (uint)0xA1, (int)HT.HT_LEFT, 0); }
                else if (RectangleRight.Contains(cursor)) { ReleaseCapture(); SendMessage(Handle, (uint)0xA1, (int)HT.HT_RIGHT, 0); }
                else if (RectangleBottom.Contains(cursor)) { ReleaseCapture(); SendMessage(Handle, (uint)0xA1, (int)HT.HT_BOTTOM, 0); }
            }
        }

        //事件_滑鼠移動
        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X <= _ - 1 || e.X + _ >= this.Width)
            {
                this.Cursor = Cursors.SizeWE;
            }
            else if (e.Y <= _ - 1 || e.Y + _ >= this.Height)
            {
                this.Cursor = Cursors.SizeNS;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        //改變視窗大小
        protected override void WndProc(ref Message m)
        {
            if (ResizeForm)
            {
                if (m.Msg == (int)WM.WM_SIZING)
                {
                    RECT rc = Marshal.PtrToStructure<RECT>(m.LParam);
                    float tempWidth, tempHeight;
                    switch (m.WParam.ToInt32()) // Resize handle
                    {
                        case (int)WMSZ.WMSZ_LEFT:
                        case (int)WMSZ.WMSZ_RIGHT:
                            tempHeight = initialDwmHeight * ((float)(this.Width - FormMargins) / initialDwmWidth);
                            tempWidth = this.Width - FormMargins;
                            currentDwmHeight = (int)tempHeight;
                            currentDwmWidth = (int)tempWidth;
                            rc.Bottom = rc.Top + currentDwmHeight + FormMargins;
                            UpdateThumb(Thumb, FormMargins, currentDwmWidth, currentDwmHeight);
                            break;

                        case (int)WMSZ.WMSZ_TOP:
                        case (int)WMSZ.WMSZ_BOTTOM:
                            tempWidth = initialDwmWidth * ((float)(this.Height - FormMargins) / initialDwmHeight);
                            tempHeight = this.Height - FormMargins;
                            currentDwmHeight = (int)tempHeight;
                            currentDwmWidth = (int)tempWidth;
                            rc.Right = rc.Left + currentDwmWidth + FormMargins;
                            UpdateThumb(Thumb, FormMargins, currentDwmWidth, currentDwmHeight);
                            break;
                    }
                    Marshal.StructureToPtr(rc, m.LParam, true);
                }
            }
            base.WndProc(ref m);
        }

        #endregion

        #region 功能_選單
        //點擊穿透
        bool Click_Through_Temp = false;
        private void ClickThrough()
        {
            if (Click_Through_Temp)
            {
                SetWindowLong(this.Handle, (int)(-20), GetWindowLong(this.Handle, (int)(-20)) & ~(long)(0x00080000) & ~(long)(0x00000020));
                NotifyIconMenu_ClickThrough.Checked = false;
                Click_Through_Temp = false;
                NotifyIcon.Icon = Properties.Resources.Icon_Off;
            }
            else
            {
                SetWindowLong(this.Handle, (int)(-20), GetWindowLong(this.Handle, (int)(-20)) | (long)(0x00080000) | (long)(0x00000020));
                NotifyIconMenu_ClickThrough.Checked = true;
                Click_Through_Temp = true;
                NotifyIcon.Icon = Properties.Resources.Icon_On;
            }
        }

        //重設
        private void Reset()
        {
            if (Thumb != IntPtr.Zero)
            {
                UnregisterThumbnail(Thumb);
            }
            ResizeForm = false;
            int ScreenWidth = GetScreenSize(this).Width;
            int ScreenHeight = GetScreenSize(this).Height;
            this.MinimumSize = new Size((int)(ScreenWidth * 0.1), (int)(ScreenHeight * 0.1));
            this.MaximumSize = new Size(ScreenWidth, ScreenHeight);
            this.Size = new Size((int)(ScreenWidth * 0.3), (int)(ScreenHeight * 0.3));
        }

        //顯示
        private void FormShow()
        {
            this.Show();
            if (this.TopMost == false)
            {
                this.TopMost = true;
                this.TopMost = false;
            }
            else
            {
                this.TopMost = false;
                this.TopMost = true;
            }
        }

        //置頂
        private void FormTopMost()
        {
            if (this.TopMost == true)
            {
                this.TopMost = false;
                NotifyIconMenu_OnTop.Checked = false;
            }
            else
            {
                this.TopMost = true;
                NotifyIconMenu_OnTop.Checked = true;
            }
        }
        #endregion

        #region 事件_工具列圖示點擊
        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ClickThrough();
            }
        }
        #endregion

        #region 事件_工作列選單點選
        private static SettingsForm? settingsForm;

        private void NotifyIconMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem?.Name)
            {
                case "NotifyIconMenu_Exit": //離開
                    Close();
                    break;

                case "NotifyIconMenu_Setting": //設定
                    this.Visible = false;
                    if (settingsForm == null || settingsForm.IsDisposed)
                    {
                        settingsForm = new SettingsForm(); //建立實例
                        hotkey?.Dispose(); //釋放hotkey

                        settingsForm.SettingsSaved += () =>
                        {
                            this.Visible = true; //重新顯示
                            LoadIniAndSetHotkey(); //重新註冊hotkey
                        };
                    }
                    settingsForm.Show();
                    settingsForm.BringToFront();
                    break;

                case "NotifyIconMenu_Hide": //隱藏
                    this.Hide();
                    break;

                case "NotifyIconMenu_Show": //顯示
                    FormShow();
                    break;

                case "NotifyIconMenu_Reset": //重設
                    Reset();
                    break;

                case "NotifyIconMenu_ClickThrough": //點擊穿透
                    ClickThrough();
                    break;

                case "NotifyIconMenu_OnTop": //置頂
                    FormTopMost();
                    break;

            }
        }
        #endregion

        #region 事件_開啟Form選單
        private void FormMenu_Opening(object sender, CancelEventArgs e)
        {
            GetWindows();
        }
        #endregion

        #region 事件_Form選單點選
        private void FormMenu_ItemClick(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem?.Name)
            {
                case "FormMenu_Exit":
                    {
                        Close();
                    }
                    break;

                case "FormMenu_None":
                    {
                        Reset();
                    }
                    break;

                default:
                    {
                        if (e.ClickedItem?.Tag != null)
                        {
                            Reset();
                            IntPtr ItemhWnd = (IntPtr)e.ClickedItem.Tag;
                            int i = RegisterThumbnail(this.Handle, ItemhWnd, out Thumb);
                            if (i == 0)
                            {
                                QueryThumbnailSize(Thumb, out initialDwmWidth, out initialDwmHeight);
                                this.MinimumSize = new Size((int)(initialDwmWidth * 0.1) + FormMargins * 2, (int)(initialDwmHeight * 0.1) + FormMargins * 2);
                                this.MaximumSize = new Size(initialDwmWidth + FormMargins * 2, initialDwmHeight + FormMargins * 2);
                                this.Size = new Size((int)(initialDwmWidth * 0.4) + FormMargins * 2, (int)(initialDwmHeight * 0.4) + FormMargins * 2);
                                currentDwmWidth = this.Width - FormMargins;
                                currentDwmHeight = this.Height - FormMargins;
                                UpdateThumb(Thumb, FormMargins, currentDwmWidth, currentDwmHeight);
                                ResizeForm = true;
                            }
                        }
                    }
                    break;
            }
            
        }

        #endregion




    }
}
