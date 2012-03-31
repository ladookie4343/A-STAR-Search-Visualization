namespace Project1Main
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.panel = new System.Windows.Forms.Panel();
            this.contextMenu_EditCity = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeCityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeStartCityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeEndCityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_GO = new System.Windows.Forms.Button();
            this.button_Step = new System.Windows.Forms.Button();
            this.groupBox_Heuristic = new System.Windows.Forms.GroupBox();
            this.radioButton_OneHop = new System.Windows.Forms.RadioButton();
            this.radioButton_StraightLine = new System.Windows.Forms.RadioButton();
            this.button_LocationFile = new System.Windows.Forms.Button();
            this.button_ConnectionsFile = new System.Windows.Forms.Button();
            this.textBox_LocationFile = new System.Windows.Forms.TextBox();
            this.textBox_ConnectionsFile = new System.Windows.Forms.TextBox();
            this.groupBox_Tools = new System.Windows.Forms.GroupBox();
            this.checkBox_2WayPath = new System.Windows.Forms.CheckBox();
            this.checkBox_1WayPath = new System.Windows.Forms.CheckBox();
            this.checkBox_NewCity = new System.Windows.Forms.CheckBox();
            this.button_Reset = new System.Windows.Forms.Button();
            this.button_Slower = new System.Windows.Forms.Button();
            this.button_Faster = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip_Help = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_FinalPath = new System.Windows.Forms.Button();
            this.textBox_TextRepresentation = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenu_EditCity.SuspendLayout();
            this.groupBox_Heuristic.SuspendLayout();
            this.groupBox_Tools.SuspendLayout();
            this.menuStrip_Help.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel.Location = new System.Drawing.Point(225, 93);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(858, 616);
            this.panel.TabIndex = 0;
            this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            this.panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseMove);
            this.panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_MouseUp);
            // 
            // contextMenu_EditCity
            // 
            this.contextMenu_EditCity.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeCityToolStripMenuItem,
            this.makeStartCityToolStripMenuItem,
            this.makeEndCityToolStripMenuItem});
            this.contextMenu_EditCity.Name = "contextMenu_RemoveCity";
            this.contextMenu_EditCity.Size = new System.Drawing.Size(155, 70);
            // 
            // removeCityToolStripMenuItem
            // 
            this.removeCityToolStripMenuItem.Name = "removeCityToolStripMenuItem";
            this.removeCityToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.removeCityToolStripMenuItem.Text = "Remove City";
            this.removeCityToolStripMenuItem.Click += new System.EventHandler(this.removeCityToolStripMenuItem_Click);
            // 
            // makeStartCityToolStripMenuItem
            // 
            this.makeStartCityToolStripMenuItem.Name = "makeStartCityToolStripMenuItem";
            this.makeStartCityToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.makeStartCityToolStripMenuItem.Text = "Make Start City";
            this.makeStartCityToolStripMenuItem.Click += new System.EventHandler(this.makeStartCityToolStripMenuItem_Click);
            // 
            // makeEndCityToolStripMenuItem
            // 
            this.makeEndCityToolStripMenuItem.Name = "makeEndCityToolStripMenuItem";
            this.makeEndCityToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.makeEndCityToolStripMenuItem.Text = "Make End City";
            this.makeEndCityToolStripMenuItem.Click += new System.EventHandler(this.makeEndCityToolStripMenuItem_Click);
            // 
            // button_GO
            // 
            this.button_GO.Enabled = false;
            this.button_GO.Location = new System.Drawing.Point(12, 38);
            this.button_GO.Name = "button_GO";
            this.button_GO.Size = new System.Drawing.Size(95, 55);
            this.button_GO.TabIndex = 11;
            this.button_GO.Text = "GO";
            this.button_GO.UseVisualStyleBackColor = true;
            this.button_GO.Click += new System.EventHandler(this.button_GO_Click);
            // 
            // button_Step
            // 
            this.button_Step.Enabled = false;
            this.button_Step.Location = new System.Drawing.Point(11, 103);
            this.button_Step.Name = "button_Step";
            this.button_Step.Size = new System.Drawing.Size(95, 23);
            this.button_Step.TabIndex = 12;
            this.button_Step.Text = "Step";
            this.button_Step.UseVisualStyleBackColor = true;
            this.button_Step.Click += new System.EventHandler(this.button_Step_Click);
            // 
            // groupBox_Heuristic
            // 
            this.groupBox_Heuristic.Controls.Add(this.radioButton_OneHop);
            this.groupBox_Heuristic.Controls.Add(this.radioButton_StraightLine);
            this.groupBox_Heuristic.Enabled = false;
            this.groupBox_Heuristic.Location = new System.Drawing.Point(12, 238);
            this.groupBox_Heuristic.Name = "groupBox_Heuristic";
            this.groupBox_Heuristic.Size = new System.Drawing.Size(145, 72);
            this.groupBox_Heuristic.TabIndex = 15;
            this.groupBox_Heuristic.TabStop = false;
            this.groupBox_Heuristic.Text = "Heuristic";
            // 
            // radioButton_OneHop
            // 
            this.radioButton_OneHop.AutoSize = true;
            this.radioButton_OneHop.Location = new System.Drawing.Point(10, 44);
            this.radioButton_OneHop.Name = "radioButton_OneHop";
            this.radioButton_OneHop.Size = new System.Drawing.Size(87, 17);
            this.radioButton_OneHop.TabIndex = 1;
            this.radioButton_OneHop.Text = "Fewest Links";
            this.radioButton_OneHop.UseVisualStyleBackColor = true;
            this.radioButton_OneHop.CheckedChanged += new System.EventHandler(this.radioButton_OneHop_CheckedChanged);
            // 
            // radioButton_StraightLine
            // 
            this.radioButton_StraightLine.AutoSize = true;
            this.radioButton_StraightLine.Checked = true;
            this.radioButton_StraightLine.Location = new System.Drawing.Point(10, 20);
            this.radioButton_StraightLine.Name = "radioButton_StraightLine";
            this.radioButton_StraightLine.Size = new System.Drawing.Size(129, 17);
            this.radioButton_StraightLine.TabIndex = 0;
            this.radioButton_StraightLine.TabStop = true;
            this.radioButton_StraightLine.Text = "Straight Line Distance";
            this.radioButton_StraightLine.UseVisualStyleBackColor = true;
            this.radioButton_StraightLine.CheckedChanged += new System.EventHandler(this.radioButton_StraightLine_CheckedChanged);
            // 
            // button_LocationFile
            // 
            this.button_LocationFile.Location = new System.Drawing.Point(232, 28);
            this.button_LocationFile.Name = "button_LocationFile";
            this.button_LocationFile.Size = new System.Drawing.Size(143, 23);
            this.button_LocationFile.TabIndex = 16;
            this.button_LocationFile.Text = "Choose Locations File";
            this.button_LocationFile.UseVisualStyleBackColor = true;
            this.button_LocationFile.Click += new System.EventHandler(this.button_LocationFile_Click);
            // 
            // button_ConnectionsFile
            // 
            this.button_ConnectionsFile.Location = new System.Drawing.Point(233, 58);
            this.button_ConnectionsFile.Name = "button_ConnectionsFile";
            this.button_ConnectionsFile.Size = new System.Drawing.Size(143, 23);
            this.button_ConnectionsFile.TabIndex = 17;
            this.button_ConnectionsFile.Text = "Choose Connections File";
            this.button_ConnectionsFile.UseVisualStyleBackColor = true;
            this.button_ConnectionsFile.Click += new System.EventHandler(this.button_ConnectionsFile_Click);
            // 
            // textBox_LocationFile
            // 
            this.textBox_LocationFile.Enabled = false;
            this.textBox_LocationFile.Location = new System.Drawing.Point(382, 30);
            this.textBox_LocationFile.Name = "textBox_LocationFile";
            this.textBox_LocationFile.Size = new System.Drawing.Size(538, 20);
            this.textBox_LocationFile.TabIndex = 18;
            // 
            // textBox_ConnectionsFile
            // 
            this.textBox_ConnectionsFile.Enabled = false;
            this.textBox_ConnectionsFile.Location = new System.Drawing.Point(382, 60);
            this.textBox_ConnectionsFile.Name = "textBox_ConnectionsFile";
            this.textBox_ConnectionsFile.Size = new System.Drawing.Size(538, 20);
            this.textBox_ConnectionsFile.TabIndex = 19;
            // 
            // groupBox_Tools
            // 
            this.groupBox_Tools.Controls.Add(this.checkBox_2WayPath);
            this.groupBox_Tools.Controls.Add(this.checkBox_1WayPath);
            this.groupBox_Tools.Controls.Add(this.checkBox_NewCity);
            this.groupBox_Tools.Enabled = false;
            this.groupBox_Tools.Location = new System.Drawing.Point(12, 334);
            this.groupBox_Tools.Name = "groupBox_Tools";
            this.groupBox_Tools.Size = new System.Drawing.Size(163, 100);
            this.groupBox_Tools.TabIndex = 20;
            this.groupBox_Tools.TabStop = false;
            this.groupBox_Tools.Text = "Tools";
            // 
            // checkBox_2WayPath
            // 
            this.checkBox_2WayPath.AutoSize = true;
            this.checkBox_2WayPath.Location = new System.Drawing.Point(10, 68);
            this.checkBox_2WayPath.Name = "checkBox_2WayPath";
            this.checkBox_2WayPath.Size = new System.Drawing.Size(122, 17);
            this.checkBox_2WayPath.TabIndex = 2;
            this.checkBox_2WayPath.Text = "Create 2 - Way Path";
            this.checkBox_2WayPath.UseVisualStyleBackColor = true;
            this.checkBox_2WayPath.CheckedChanged += new System.EventHandler(this.checkBox_2WayPath_CheckedChanged);
            // 
            // checkBox_1WayPath
            // 
            this.checkBox_1WayPath.AutoSize = true;
            this.checkBox_1WayPath.Location = new System.Drawing.Point(10, 44);
            this.checkBox_1WayPath.Name = "checkBox_1WayPath";
            this.checkBox_1WayPath.Size = new System.Drawing.Size(122, 17);
            this.checkBox_1WayPath.TabIndex = 1;
            this.checkBox_1WayPath.Text = "Create 1 - Way Path";
            this.checkBox_1WayPath.UseVisualStyleBackColor = true;
            this.checkBox_1WayPath.CheckedChanged += new System.EventHandler(this.checkBox_1WayPath_CheckedChanged);
            // 
            // checkBox_NewCity
            // 
            this.checkBox_NewCity.AutoSize = true;
            this.checkBox_NewCity.Location = new System.Drawing.Point(10, 20);
            this.checkBox_NewCity.Name = "checkBox_NewCity";
            this.checkBox_NewCity.Size = new System.Drawing.Size(90, 17);
            this.checkBox_NewCity.TabIndex = 0;
            this.checkBox_NewCity.Text = "Add New City";
            this.checkBox_NewCity.UseVisualStyleBackColor = true;
            this.checkBox_NewCity.CheckedChanged += new System.EventHandler(this.checkBox_NewCity_CheckedChanged);
            // 
            // button_Reset
            // 
            this.button_Reset.Enabled = false;
            this.button_Reset.Location = new System.Drawing.Point(11, 168);
            this.button_Reset.Name = "button_Reset";
            this.button_Reset.Size = new System.Drawing.Size(94, 23);
            this.button_Reset.TabIndex = 21;
            this.button_Reset.Text = "Reset";
            this.button_Reset.UseVisualStyleBackColor = true;
            this.button_Reset.Click += new System.EventHandler(this.button_Reset_Click);
            // 
            // button_Slower
            // 
            this.button_Slower.Location = new System.Drawing.Point(121, 38);
            this.button_Slower.Name = "button_Slower";
            this.button_Slower.Size = new System.Drawing.Size(75, 23);
            this.button_Slower.TabIndex = 22;
            this.button_Slower.Text = "Slower";
            this.button_Slower.UseVisualStyleBackColor = true;
            this.button_Slower.Click += new System.EventHandler(this.button_Slower_Click);
            // 
            // button_Faster
            // 
            this.button_Faster.Location = new System.Drawing.Point(121, 70);
            this.button_Faster.Name = "button_Faster";
            this.button_Faster.Size = new System.Drawing.Size(75, 23);
            this.button_Faster.TabIndex = 23;
            this.button_Faster.Text = "Faster";
            this.button_Faster.UseVisualStyleBackColor = true;
            this.button_Faster.Click += new System.EventHandler(this.button_Faster_Click);
            // 
            // timer
            // 
            this.timer.Interval = 200;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // menuStrip_Help
            // 
            this.menuStrip_Help.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.menuStrip_Help.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip_Help.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip_Help.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_Help.Name = "menuStrip_Help";
            this.menuStrip_Help.Size = new System.Drawing.Size(1095, 24);
            this.menuStrip_Help.TabIndex = 24;
            this.menuStrip_Help.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewHelpToolStripMenuItem});
            this.helpToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // viewHelpToolStripMenuItem
            // 
            this.viewHelpToolStripMenuItem.Name = "viewHelpToolStripMenuItem";
            this.viewHelpToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.viewHelpToolStripMenuItem.Text = "View Help";
            this.viewHelpToolStripMenuItem.Click += new System.EventHandler(this.viewHelpToolStripMenuItem_Click);
            // 
            // button_FinalPath
            // 
            this.button_FinalPath.Enabled = false;
            this.button_FinalPath.Location = new System.Drawing.Point(11, 132);
            this.button_FinalPath.Name = "button_FinalPath";
            this.button_FinalPath.Size = new System.Drawing.Size(94, 23);
            this.button_FinalPath.TabIndex = 25;
            this.button_FinalPath.Text = "Show Final Path";
            this.button_FinalPath.UseVisualStyleBackColor = true;
            this.button_FinalPath.Click += new System.EventHandler(this.button_FinalPath_Click);
            // 
            // textBox_TextRepresentation
            // 
            this.textBox_TextRepresentation.Location = new System.Drawing.Point(14, 470);
            this.textBox_TextRepresentation.Multiline = true;
            this.textBox_TextRepresentation.Name = "textBox_TextRepresentation";
            this.textBox_TextRepresentation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_TextRepresentation.Size = new System.Drawing.Size(182, 239);
            this.textBox_TextRepresentation.TabIndex = 26;
            this.textBox_TextRepresentation.TextChanged += new System.EventHandler(this.textBox_TextRepresentation_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 451);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Textual Representation";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 721);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_TextRepresentation);
            this.Controls.Add(this.button_FinalPath);
            this.Controls.Add(this.menuStrip_Help);
            this.Controls.Add(this.groupBox_Tools);
            this.Controls.Add(this.button_Faster);
            this.Controls.Add(this.button_Slower);
            this.Controls.Add(this.textBox_ConnectionsFile);
            this.Controls.Add(this.textBox_LocationFile);
            this.Controls.Add(this.button_Reset);
            this.Controls.Add(this.button_ConnectionsFile);
            this.Controls.Add(this.button_LocationFile);
            this.Controls.Add(this.groupBox_Heuristic);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.button_GO);
            this.Controls.Add(this.button_Step);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip_Help;
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "A* Search Algorithm";
            this.contextMenu_EditCity.ResumeLayout(false);
            this.groupBox_Heuristic.ResumeLayout(false);
            this.groupBox_Heuristic.PerformLayout();
            this.groupBox_Tools.ResumeLayout(false);
            this.groupBox_Tools.PerformLayout();
            this.menuStrip_Help.ResumeLayout(false);
            this.menuStrip_Help.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.ContextMenuStrip contextMenu_EditCity;
        private System.Windows.Forms.ToolStripMenuItem removeCityToolStripMenuItem;
        private System.Windows.Forms.Button button_GO;
        private System.Windows.Forms.Button button_Step;
        private System.Windows.Forms.GroupBox groupBox_Heuristic;
        private System.Windows.Forms.RadioButton radioButton_OneHop;
        private System.Windows.Forms.RadioButton radioButton_StraightLine;
        private System.Windows.Forms.Button button_LocationFile;
        private System.Windows.Forms.Button button_ConnectionsFile;
        private System.Windows.Forms.TextBox textBox_LocationFile;
        private System.Windows.Forms.TextBox textBox_ConnectionsFile;
        private System.Windows.Forms.GroupBox groupBox_Tools;
        private System.Windows.Forms.CheckBox checkBox_2WayPath;
        private System.Windows.Forms.CheckBox checkBox_1WayPath;
        private System.Windows.Forms.CheckBox checkBox_NewCity;
        private System.Windows.Forms.Button button_Reset;
        private System.Windows.Forms.ToolStripMenuItem makeStartCityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem makeEndCityToolStripMenuItem;
        private System.Windows.Forms.Button button_Slower;
        private System.Windows.Forms.Button button_Faster;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.MenuStrip menuStrip_Help;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewHelpToolStripMenuItem;
        private System.Windows.Forms.Button button_FinalPath;
        private System.Windows.Forms.TextBox textBox_TextRepresentation;
        private System.Windows.Forms.Label label1;
    }
}

