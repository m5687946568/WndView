using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WndView
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

        }

        private void SettingsForm_FormCloseed(object sender, FormClosedEventArgs e)
        {
            if (Owner != null) Owner.Visible = true;
        }

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
                    if (lastClickedLabel != null && labelState.ContainsKey(lastClickedLabel) && lbl != lastClickedLabel)
                    {
                        (lastClickedLabel.Text, lastClickedLabel.Tag) = labelState[lastClickedLabel];
                    }
                }
                //儲存新 Label 的原始狀態
                if (lbl != lastClickedLabel) labelState[lbl] = (lbl.Text, lbl.Tag);

                lastClickedLabel = lbl;

                switch (lbl.Name)
                {
                    case "SetKey_ClickThrough":
                        SetKey_ClickThrough.Text = Properties.Resources.SetKeyLabelText;
                        break;

                    case "SetKey_Show":
                        SetKey_Show.Text = "請按下按鍵...";
                        break;

                    case "SetKey_Hide":
                        SetKey_Hide.Text = "請按下按鍵...";
                        break;

                    case "SetKey_Exit":
                        SetKey_Exit.Text = "請按下按鍵...";
                        break;
                }
            }
            setLabelFinish = false;
            this.KeyDown += SettingsForm_KeyDown;
            this.KeyUp += SettingsForm_KeyUp;
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

            if (activeModifiers.Count != 0)
            {
                activeKeys.Add(e.KeyCode);
                if (lastClickedLabel != null) UpdateLabel(lastClickedLabel);
            }
        }

        private void SettingsForm_KeyUp(object? sender, KeyEventArgs e)
        {
            activeKeys.Remove(e.KeyCode);

            if (activeKeys.Count == 0 && activeModifiers.Count != 0 && activeKeyCode != Keys.None)
            {
                this.KeyPreview = false;
                Task.Delay(100).ContinueWith(_ =>
                {
                    this.KeyDown -= SettingsForm_KeyDown;
                    this.KeyUp -= SettingsForm_KeyUp;
                    setLabelFinish = true;
                });
            }
        }

        private void UpdateLabel(Label lbl)
        {
            List<string> keysList = new();
            List<Keys> keyValues = new();

            foreach (Keys key in activeModifiers)
            {
                keysList.Add(FormatKeyDisplay(key));
                keyValues.Add(key);
            }

            keysList.Add(FormatKeyDisplay(activeKeyCode));
            keyValues.Add(activeKeyCode);

            lbl.Text = string.Join(" + ", keysList);
            lbl.Tag = keyValues;
        }

        #endregion

        private void InitializeComponent()
        {

        }
    }
}
