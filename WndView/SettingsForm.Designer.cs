namespace WndView
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            btn_OK = new Button();
            btn_Cancel = new Button();
            groupBox1 = new GroupBox();
            ExitEnable = new CheckBox();
            HideEnable = new CheckBox();
            ShowEnable = new CheckBox();
            ClickThroughEnable = new CheckBox();
            SetKey_Exit = new Label();
            SetKeyName_Exit = new Label();
            SetKeyName_Hide = new Label();
            SetKeyName_Show = new Label();
            SetKey_Hide = new Label();
            SetKeyName_ClickThrough = new Label();
            SetKey_ClickThrough = new Label();
            SetKey_Show = new Label();
            btn_Browse = new Button();
            label1 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // btn_OK
            // 
            resources.ApplyResources(btn_OK, "btn_OK");
            btn_OK.Name = "btn_OK";
            btn_OK.UseVisualStyleBackColor = true;
            btn_OK.Click += btn_OK_Click;
            // 
            // btn_Cancel
            // 
            resources.ApplyResources(btn_Cancel, "btn_Cancel");
            btn_Cancel.Name = "btn_Cancel";
            btn_Cancel.UseVisualStyleBackColor = true;
            btn_Cancel.Click += btn_Cancel_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ExitEnable);
            groupBox1.Controls.Add(HideEnable);
            groupBox1.Controls.Add(ShowEnable);
            groupBox1.Controls.Add(ClickThroughEnable);
            groupBox1.Controls.Add(SetKey_Exit);
            groupBox1.Controls.Add(SetKeyName_Exit);
            groupBox1.Controls.Add(SetKeyName_Hide);
            groupBox1.Controls.Add(SetKeyName_Show);
            groupBox1.Controls.Add(SetKey_Hide);
            groupBox1.Controls.Add(SetKeyName_ClickThrough);
            groupBox1.Controls.Add(SetKey_ClickThrough);
            groupBox1.Controls.Add(SetKey_Show);
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // ExitEnable
            // 
            resources.ApplyResources(ExitEnable, "ExitEnable");
            ExitEnable.Name = "ExitEnable";
            ExitEnable.TabStop = false;
            ExitEnable.UseVisualStyleBackColor = true;
            ExitEnable.CheckedChanged += ExitEnable_CheckedChanged;
            // 
            // HideEnable
            // 
            resources.ApplyResources(HideEnable, "HideEnable");
            HideEnable.Name = "HideEnable";
            HideEnable.TabStop = false;
            HideEnable.UseVisualStyleBackColor = true;
            HideEnable.CheckedChanged += HideEnable_CheckedChanged;
            // 
            // ShowEnable
            // 
            resources.ApplyResources(ShowEnable, "ShowEnable");
            ShowEnable.Name = "ShowEnable";
            ShowEnable.TabStop = false;
            ShowEnable.UseVisualStyleBackColor = true;
            ShowEnable.CheckedChanged += ShowEnable_CheckedChanged;
            // 
            // ClickThroughEnable
            // 
            resources.ApplyResources(ClickThroughEnable, "ClickThroughEnable");
            ClickThroughEnable.Name = "ClickThroughEnable";
            ClickThroughEnable.TabStop = false;
            ClickThroughEnable.UseVisualStyleBackColor = true;
            ClickThroughEnable.CheckedChanged += ClickThroughEnable_CheckedChanged;
            // 
            // SetKey_Exit
            // 
            SetKey_Exit.BorderStyle = BorderStyle.FixedSingle;
            resources.ApplyResources(SetKey_Exit, "SetKey_Exit");
            SetKey_Exit.Name = "SetKey_Exit";
            SetKey_Exit.Tag = "0";
            SetKey_Exit.Click += SetKeyLabel_Click;
            // 
            // SetKeyName_Exit
            // 
            resources.ApplyResources(SetKeyName_Exit, "SetKeyName_Exit");
            SetKeyName_Exit.Name = "SetKeyName_Exit";
            // 
            // SetKeyName_Hide
            // 
            resources.ApplyResources(SetKeyName_Hide, "SetKeyName_Hide");
            SetKeyName_Hide.Name = "SetKeyName_Hide";
            // 
            // SetKeyName_Show
            // 
            resources.ApplyResources(SetKeyName_Show, "SetKeyName_Show");
            SetKeyName_Show.Name = "SetKeyName_Show";
            // 
            // SetKey_Hide
            // 
            SetKey_Hide.BorderStyle = BorderStyle.FixedSingle;
            resources.ApplyResources(SetKey_Hide, "SetKey_Hide");
            SetKey_Hide.Name = "SetKey_Hide";
            SetKey_Hide.Tag = "0";
            SetKey_Hide.Click += SetKeyLabel_Click;
            // 
            // SetKeyName_ClickThrough
            // 
            resources.ApplyResources(SetKeyName_ClickThrough, "SetKeyName_ClickThrough");
            SetKeyName_ClickThrough.Name = "SetKeyName_ClickThrough";
            // 
            // SetKey_ClickThrough
            // 
            SetKey_ClickThrough.BorderStyle = BorderStyle.FixedSingle;
            resources.ApplyResources(SetKey_ClickThrough, "SetKey_ClickThrough");
            SetKey_ClickThrough.Name = "SetKey_ClickThrough";
            SetKey_ClickThrough.Tag = "0";
            SetKey_ClickThrough.Click += SetKeyLabel_Click;
            // 
            // SetKey_Show
            // 
            SetKey_Show.BorderStyle = BorderStyle.FixedSingle;
            resources.ApplyResources(SetKey_Show, "SetKey_Show");
            SetKey_Show.Name = "SetKey_Show";
            SetKey_Show.Tag = "0";
            SetKey_Show.Click += SetKeyLabel_Click;
            // 
            // btn_Browse
            // 
            resources.ApplyResources(btn_Browse, "btn_Browse");
            btn_Browse.Name = "btn_Browse";
            btn_Browse.UseVisualStyleBackColor = true;
            btn_Browse.Click += btn_Browse_Click;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // SettingsForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label1);
            Controls.Add(btn_Browse);
            Controls.Add(btn_Cancel);
            Controls.Add(btn_OK);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsForm";
            FormClosed += SettingsForm_FormCloseed;
            Load += SettingsForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btn_OK;
        private Button btn_Cancel;
        private GroupBox groupBox1;
        private Label SetKey_Exit;
        private Label SetKeyName_Exit;
        private Label SetKey_Hide;
        private Label SetKeyName_Hide;
        private Label SetKey_Show;
        private Label SetKeyName_Show;
        private Label SetKey_ClickThrough;
        private Label SetKeyName_ClickThrough;
        private CheckBox ExitEnable;
        private CheckBox HideEnable;
        private CheckBox ShowEnable;
        private CheckBox ClickThroughEnable;
        private Button btn_Browse;
        private Label label1;
    }
}