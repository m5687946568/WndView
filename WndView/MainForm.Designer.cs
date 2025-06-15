namespace WndView
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            toolStripSeparator5 = new ToolStripSeparator();
            NotifyIcon = new NotifyIcon(components);
            NotifyIconMenu = new ContextMenuStrip(components);
            NotifyIconMenu_OnTop = new ToolStripMenuItem();
            NotifyIconMenu_ClickThrough = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            NotifyIconMenu_Show = new ToolStripMenuItem();
            NotifyIconMenu_Hide = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            NotifyIconMenu_Reset = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            NotifyIconMenu_Setting = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            NotifyIconMenu_Exit = new ToolStripMenuItem();
            FormMenu = new ContextMenuStrip(components);
            FormMenu_None = new ToolStripMenuItem();
            FormMenu_Exit = new ToolStripMenuItem();
            label1 = new Label();
            NotifyIconMenu.SuspendLayout();
            FormMenu.SuspendLayout();
            SuspendLayout();
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(toolStripSeparator5, "toolStripSeparator5");
            // 
            // NotifyIcon
            // 
            NotifyIcon.ContextMenuStrip = NotifyIconMenu;
            resources.ApplyResources(NotifyIcon, "NotifyIcon");
            NotifyIcon.MouseClick += NotifyIcon_MouseClick;
            // 
            // NotifyIconMenu
            // 
            NotifyIconMenu.Items.AddRange(new ToolStripItem[] { NotifyIconMenu_OnTop, NotifyIconMenu_ClickThrough, toolStripSeparator1, NotifyIconMenu_Show, NotifyIconMenu_Hide, toolStripSeparator2, NotifyIconMenu_Reset, toolStripSeparator3, NotifyIconMenu_Setting, toolStripSeparator4, NotifyIconMenu_Exit });
            NotifyIconMenu.Name = "contextMenuStrip1";
            resources.ApplyResources(NotifyIconMenu, "NotifyIconMenu");
            NotifyIconMenu.ItemClicked += NotifyIconMenu_ItemClicked;
            // 
            // NotifyIconMenu_OnTop
            // 
            NotifyIconMenu_OnTop.Checked = true;
            NotifyIconMenu_OnTop.CheckState = CheckState.Checked;
            NotifyIconMenu_OnTop.Name = "NotifyIconMenu_OnTop";
            resources.ApplyResources(NotifyIconMenu_OnTop, "NotifyIconMenu_OnTop");
            // 
            // NotifyIconMenu_ClickThrough
            // 
            NotifyIconMenu_ClickThrough.Name = "NotifyIconMenu_ClickThrough";
            resources.ApplyResources(NotifyIconMenu_ClickThrough, "NotifyIconMenu_ClickThrough");
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            // 
            // NotifyIconMenu_Show
            // 
            NotifyIconMenu_Show.Name = "NotifyIconMenu_Show";
            resources.ApplyResources(NotifyIconMenu_Show, "NotifyIconMenu_Show");
            // 
            // NotifyIconMenu_Hide
            // 
            NotifyIconMenu_Hide.Name = "NotifyIconMenu_Hide";
            resources.ApplyResources(NotifyIconMenu_Hide, "NotifyIconMenu_Hide");
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
            // 
            // NotifyIconMenu_Reset
            // 
            NotifyIconMenu_Reset.Name = "NotifyIconMenu_Reset";
            resources.ApplyResources(NotifyIconMenu_Reset, "NotifyIconMenu_Reset");
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(toolStripSeparator3, "toolStripSeparator3");
            // 
            // NotifyIconMenu_Setting
            // 
            NotifyIconMenu_Setting.Name = "NotifyIconMenu_Setting";
            resources.ApplyResources(NotifyIconMenu_Setting, "NotifyIconMenu_Setting");
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(toolStripSeparator4, "toolStripSeparator4");
            // 
            // NotifyIconMenu_Exit
            // 
            NotifyIconMenu_Exit.Name = "NotifyIconMenu_Exit";
            resources.ApplyResources(NotifyIconMenu_Exit, "NotifyIconMenu_Exit");
            // 
            // FormMenu
            // 
            FormMenu.Items.AddRange(new ToolStripItem[] { FormMenu_None, toolStripSeparator5, FormMenu_Exit });
            FormMenu.Name = "contextMenuStrip2";
            resources.ApplyResources(FormMenu, "FormMenu");
            FormMenu.Opening += FormMenu_Opening;
            FormMenu.ItemClicked += FormMenu_ItemClick;
            // 
            // FormMenu_None
            // 
            FormMenu_None.Name = "FormMenu_None";
            resources.ApplyResources(FormMenu_None, "FormMenu_None");
            // 
            // FormMenu_Exit
            // 
            FormMenu_Exit.Name = "FormMenu_Exit";
            resources.ApplyResources(FormMenu_Exit, "FormMenu_Exit");
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.ForeColor = SystemColors.Menu;
            label1.Name = "label1";
            label1.UseMnemonic = false;
            label1.MouseDown += Form_MouseDown;
            label1.MouseMove += Form_MouseMove;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            CausesValidation = false;
            ContextMenuStrip = FormMenu;
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MainForm";
            TopMost = true;
            NotifyIconMenu.ResumeLayout(false);
            FormMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private NotifyIcon NotifyIcon;
        private ContextMenuStrip NotifyIconMenu;
        private ContextMenuStrip FormMenu;
        private Label label1;
        private ToolStripMenuItem NotifyIconMenu_OnTop;
        private ToolStripMenuItem NotifyIconMenu_ClickThrough;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem NotifyIconMenu_Show;
        private ToolStripMenuItem NotifyIconMenu_Hide;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem NotifyIconMenu_Reset;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem NotifyIconMenu_Exit;
        private ToolStripMenuItem FormMenu_Exit;
        private ToolStripMenuItem FormMenu_None;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem NotifyIconMenu_Setting;
    }
}