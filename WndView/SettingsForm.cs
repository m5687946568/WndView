using System.Diagnostics;
using static WndView.INIFunction;

namespace WndView
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

        }

        public event Action SettingsSaved = null!;

        #region 全域快捷鍵按鍵擷取
        bool setLabelFinish = true;
        Label? lastClickedLabel = new();
        HashSet<Keys> activeKeys = new();
        Keys activeKeyCode = Keys.None;
        HashSet<Keys> activeModifiers = new();
        Dictionary<Label, (string, object?)> labelState = new();
        string FormatKeyDisplay(Keys key)
        {
            return key switch
            {
                Keys.ControlKey => "Ctrl",
                Keys.ShiftKey => "Shift",
                Keys.Menu => "Alt",
                Keys.NumPad0 => "Num0",
                Keys.NumPad1 => "Num1",
                Keys.NumPad2 => "Num2",
                Keys.NumPad3 => "Num3",
                Keys.NumPad4 => "Num4",
                Keys.NumPad5 => "Num5",
                Keys.NumPad6 => "Num6",
                Keys.NumPad7 => "Num7",
                Keys.NumPad8 => "Num8",
                Keys.NumPad9 => "Num9",
                Keys.Decimal => "\".\"",
                Keys.Add => "\"+\"",
                Keys.Subtract => "\"-\"",
                Keys.Multiply => "\"*\"",
                Keys.Divide => "\"/\"",
                Keys.D0 => "0",
                Keys.D1 => "1",
                Keys.D2 => "2",
                Keys.D3 => "3",
                Keys.D4 => "4",
                Keys.D5 => "5",
                Keys.D6 => "6",
                Keys.D7 => "7",
                Keys.D8 => "8",
                Keys.D9 => "9",
                Keys.Oemcomma => "\",\"",
                Keys.OemPeriod => "\".\"",
                Keys.OemQuestion => "\"/\"",
                Keys.OemSemicolon => "\";\"",
                Keys.OemQuotes => "\"'\"",
                Keys.OemOpenBrackets => "\"[\"",
                Keys.OemCloseBrackets => "\"]\"",
                Keys.OemPipe => "\"\\\"",
                Keys.OemMinus => "\"-\"",
                Keys.Oemplus => "\"=\"",
                Keys.Oemtilde => "\"`\"",
                Keys.Escape => "Esc",
                _ => key.ToString()
            };

        }

        private void SetKeyLabel_Click(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.Focus();

            activeKeys = new HashSet<Keys>();
            activeModifiers = new HashSet<Keys>();
            activeKeyCode = Keys.None;
            Label? lbl = sender as Label;

            if (lbl != null)
            {
                //還原前一個 Label 狀態
                if (!setLabelFinish)
                {
                    if (lastClickedLabel != null && labelState.ContainsKey(lastClickedLabel))
                    {
                        (lastClickedLabel.Text, lastClickedLabel.Tag) = labelState[lastClickedLabel];
                    }
                }

                //儲存 Label 的原始狀態
                labelState[lbl] = (lbl.Text, lbl.Tag);

                lastClickedLabel = lbl;
                lbl.Text = Properties.Resources.SettingsForm_SetKeyLabelText;

                setLabelFinish = false;
                btn_OK.Enabled = false;
                this.KeyDown += SettingsForm_KeyDown;
                this.KeyUp += SettingsForm_KeyUp;
            }
        }

        private void SettingsForm_KeyDown(object? sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;

            if (!(
                (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) ||  //數字鍵盤 0~9
                (e.KeyCode == Keys.Decimal) || //數字鍵盤 `.`
                (e.KeyCode == Keys.Add) || //數字鍵盤 `+`
                (e.KeyCode == Keys.Subtract) || //數字鍵盤 `-`
                (e.KeyCode == Keys.Multiply) || //數字鍵盤 `*`
                (e.KeyCode == Keys.Divide) || //數字鍵盤 `/`
                (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z) || //A~Z
                (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || //1~9
                (e.KeyCode == Keys.Oemcomma) ||  // `,`
                (e.KeyCode == Keys.OemPeriod) || // `.`
                (e.KeyCode == Keys.OemQuestion) || // `/`
                (e.KeyCode == Keys.OemSemicolon) || // `;`
                (e.KeyCode == Keys.OemQuotes) || // `'`
                (e.KeyCode == Keys.OemOpenBrackets) || // `[`
                (e.KeyCode == Keys.OemCloseBrackets) || // `]`
                (e.KeyCode == Keys.OemPipe) || // `\`
                (e.KeyCode == Keys.OemMinus) || // `-`
                (e.KeyCode == Keys.Oemplus) || // `=`
                (e.KeyCode == Keys.Oemtilde) || // `~`
                (e.KeyCode >= Keys.F1 && e.KeyCode <= Keys.F12) || //F1~F12
                (e.KeyCode == Keys.Escape) ||
                (e.KeyCode == Keys.ControlKey) ||
                (e.KeyCode == Keys.ShiftKey) ||
                (e.KeyCode == Keys.Menu)
                )) { return; } //忽略所有非指定的按鍵

            if (e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.Menu)
            {
                if (activeModifiers.Count() == 0)
                {
                    activeModifiers.Add(e.KeyCode);
                }
                else
                {
                    HashSet<Keys> activeModifiersTemp = new(activeModifiers);
                    activeModifiers.Clear();
                    if (activeModifiersTemp.Contains(Keys.ControlKey)) activeModifiers.Add(Keys.ControlKey);
                    if (activeModifiersTemp.Contains(Keys.ShiftKey)) activeModifiers.Add(Keys.ShiftKey);
                    if (activeModifiersTemp.Contains(Keys.Menu)) activeModifiers.Add(Keys.Menu);
                    activeModifiers.Add(e.KeyCode);
                }
            }
            else
            {
                activeKeyCode = e.KeyCode;
            }

            activeKeys.Add(e.KeyCode);
            if (lastClickedLabel != null) UpdateLabel(lastClickedLabel);

        }

        private void SettingsForm_KeyUp(object? sender, KeyEventArgs e)
        {
            activeKeys.Remove(e.KeyCode);

            if (activeKeys.Count == 0 && activeModifiers.Count != 0 && activeKeyCode != Keys.None)
            {
                this.KeyPreview = false;
                this.KeyDown -= SettingsForm_KeyDown;
                this.KeyUp -= SettingsForm_KeyUp;
                setLabelFinish = true;
                btn_OK.Enabled = true;
            }
        }

        private void UpdateLabel(Label lbl)
        {
            List<string> keysList = new();
            List<Keys> keyValues = new();

            if (activeModifiers.Count == 0)
            {
                keysList.Add("None");
            }
            else
            {
                foreach (Keys key in activeModifiers)
                {
                    keysList.Add(FormatKeyDisplay(key));
                    keyValues.Add(key);
                }
            }

            if (activeKeyCode == Keys.None)
            {
                keysList.Add("None");
            }
            else
            {
                keysList.Add(FormatKeyDisplay(activeKeyCode));
                keyValues.Add(activeKeyCode);
            }

            lbl.Text = string.Join(" + ", keysList);
            lbl.Tag = ConvertKeysToUint(keyValues);


            //uint combined = (uint)lbl.Tag;

            //uint modifiers = combined >> 16;  // 取出高 16 位
            //Keys keyCode = (Keys)(combined & 0xFFFF); // 取出低 16 位

            //Debug.WriteLine($"t1: {modifiers}");
            //Debug.WriteLine($"t2: {keyCode}");
            //hotkey.Register(keyCode, modifiers, () =>
            //{
            //    MessageBox.Show("你按了");
            //});


        }

        private uint ConvertKeysToUint(List<Keys> keyValues)
        {
            if (keyValues is null || keyValues.Count == 0)
                return 0; // 如果 `keyValues` 為空，存入 `0`

            uint modFlags = 0;
            Keys keyCode = Keys.None;

            foreach (Keys key in keyValues)
            {
                if (key == Keys.ControlKey) modFlags |= 0x0002;
                else if (key == Keys.ShiftKey) modFlags |= 0x0004;
                else if (key == Keys.Menu) modFlags |= 0x0001;
                else if (key == Keys.LWin || key == Keys.RWin) modFlags |= 0x0008;
                else keyCode = key; // ✅ 設定主要按鍵
            }

            return modFlags << 16 | (uint)keyCode; // ✅ 組合 `Modifiers` 和 `KeyCode`
        }

        #endregion

        #region 事件_點擊OK按鈕
        private void btn_OK_Click(object sender, EventArgs e)
        {
            WPPS("HotKeysEnable", "ClickThrough", ClickThroughEnable.Checked.ToString());
            WPPS("HotKeys", "ClickThrough", SetKey_ClickThrough.Tag?.ToString() ?? "0");

            WPPS("HotKeysEnable", "Show", ShowEnable.Checked.ToString());
            WPPS("HotKeys", "Show", SetKey_Show.Tag?.ToString() ?? "0");

            WPPS("HotKeysEnable", "Hide", HideEnable.Checked.ToString());
            WPPS("HotKeys", "Hide", SetKey_Hide.Tag?.ToString() ?? "0");

            WPPS("HotKeysEnable", "Exit", ExitEnable.Checked.ToString());
            WPPS("HotKeys", "Exit", SetKey_Exit.Tag?.ToString() ?? "0");

            this.Close();
        }
        #endregion

        #region 事件_點擊Cancel按鈕
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 事件_點擊Browse按鈕
        private void btn_Browse_Click(object sender, EventArgs e)
        {
            string folderPath = Path.GetDirectoryName(ProfilePath) ?? "";
            if (Directory.Exists(folderPath))
            {
                Process.Start(new ProcessStartInfo()
                {
                    FileName = folderPath,
                    UseShellExecute = true //確保使用 Windows Explorer 開啟目錄
                });
            }
        }
        #endregion

        #region 事件_SettingsForm載入
        private void SettingsForm_Load(object sender, EventArgs e)
        {
            SetLabelHotKeyValue(ClickThroughEnable, SetKey_ClickThrough, "ClickThrough");
            SetLabelHotKeyValue(ShowEnable, SetKey_Show, "Show");
            SetLabelHotKeyValue(HideEnable, SetKey_Hide, "Hide");
            SetLabelHotKeyValue(ExitEnable, SetKey_Exit, "Exit");
        }

        private void SetLabelHotKeyValue(CheckBox cb, Label lbl, string settingName)
        {
            string iniValue1 = GPPS("HotKeysEnable", settingName);
            string iniValue2 = GPPS("HotKeys", settingName);

            if (bool.TryParse(iniValue1, out bool result))
            {
                if (result)
                {
                    cb.Checked = true;
                    lbl.Enabled = true;
                }
                else
                {
                    cb.Checked = false;
                    lbl.Enabled = false;
                }
            }
            else
            {
                cb.Checked = false;
                lbl.Enabled = false;
            }

            if (uint.TryParse(iniValue2, out uint hotkeyValue))
            {
                uint modifiers = hotkeyValue >> 16;
                Keys keyCode = (Keys)(hotkeyValue & 0xFFFF);
                lbl.Text = $"{FormatModifiers(modifiers)} + {keyCode}";
                lbl.Tag = hotkeyValue;
            }
            else
            {
                lbl.Text = "";
                lbl.Tag = 0;
            }
        }

        private string FormatModifiers(uint modifiers)
        {
            var modList = new List<string>();

            if ((modifiers & 0x0002) != 0) modList.Add("Ctrl");
            if ((modifiers & 0x0004) != 0) modList.Add("Shift");
            if ((modifiers & 0x0001) != 0) modList.Add("Alt");

            return modList.Count > 0 ? string.Join(" + ", modList) : "";
        }

        #endregion

        #region 事件_SettingsForm關閉
        private void SettingsForm_FormCloseed(object sender, FormClosedEventArgs e)
        {
            SettingsSaved?.Invoke();
        }
        #endregion

        #region 事件_CheckedChanged
        private void ClickThroughEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (ClickThroughEnable.Checked)
            {
                SetKey_ClickThrough.Enabled = true;
            }
            else
            {
                SetKey_ClickThrough.Enabled = false;
            }
        }

        private void ShowEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowEnable.Checked)
            {
                SetKey_Show.Enabled = true;
            }
            else
            {
                SetKey_Show.Enabled = false;
            }
        }

        private void HideEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (HideEnable.Checked)
            {
                SetKey_Hide.Enabled = true;
            }
            else
            {
                SetKey_Hide.Enabled = false;
            }
        }

        private void ExitEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (ExitEnable.Checked)
            {
                SetKey_Exit.Enabled = true;
            }
            else
            {
                SetKey_Exit.Enabled = false;
            }
        }
        #endregion


    }
}
