namespace Local_documentation
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            newButton = new Button();
            saveButton = new Button();
            pageList = new ComboBox();
            pageContents = new Panel();
            label1 = new Label();
            SettingsButton = new Button();
            removebutton = new Button();
            webnamelabel = new Label();
            SuspendLayout();
            // 
            // newButton
            // 
            newButton.BackColor = Color.White;
            newButton.Location = new Point(591, 76);
            newButton.Margin = new Padding(0);
            newButton.Name = "newButton";
            newButton.Size = new Size(87, 45);
            newButton.TabIndex = 0;
            newButton.Text = "NEW";
            newButton.UseVisualStyleBackColor = false;
            newButton.Click += New_Button_Click;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(729, 76);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(87, 45);
            saveButton.TabIndex = 1;
            saveButton.Text = "SAVE";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += Save_Button_Click;
            // 
            // pageList
            // 
            pageList.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            pageList.FormattingEnabled = true;
            pageList.ItemHeight = 37;
            pageList.Location = new Point(43, 76);
            pageList.Name = "pageList";
            pageList.Size = new Size(501, 45);
            pageList.TabIndex = 2;
            pageList.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // pageContents
            // 
            pageContents.AutoScroll = true;
            pageContents.BackColor = Color.White;
            pageContents.BorderStyle = BorderStyle.FixedSingle;
            pageContents.Location = new Point(43, 137);
            pageContents.Name = "pageContents";
            pageContents.Size = new Size(1044, 651);
            pageContents.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(43, 18);
            label1.Name = "label1";
            label1.Size = new Size(255, 45);
            label1.TabIndex = 4;
            label1.Text = "Website Name :";
            // 
            // SettingsButton
            // 
            SettingsButton.Location = new Point(1000, 76);
            SettingsButton.Name = "SettingsButton";
            SettingsButton.Size = new Size(87, 45);
            SettingsButton.TabIndex = 5;
            SettingsButton.Text = "SETTINGS";
            SettingsButton.UseVisualStyleBackColor = true;
            SettingsButton.Click += SettingsButton_Click;
            // 
            // removebutton
            // 
            removebutton.Location = new Point(871, 76);
            removebutton.Name = "removebutton";
            removebutton.Size = new Size(87, 45);
            removebutton.TabIndex = 6;
            removebutton.Text = "REMOVE";
            removebutton.UseVisualStyleBackColor = true;
            removebutton.Click += removebutton_Click;
            // 
            // webnamelabel
            // 
            webnamelabel.AutoSize = true;
            webnamelabel.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            webnamelabel.ForeColor = Color.White;
            webnamelabel.Location = new Point(293, 18);
            webnamelabel.Name = "webnamelabel";
            webnamelabel.Size = new Size(165, 45);
            webnamelabel.TabIndex = 7;
            webnamelabel.Text = "webname";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(1130, 817);
            Controls.Add(webnamelabel);
            Controls.Add(removebutton);
            Controls.Add(SettingsButton);
            Controls.Add(newButton);
            Controls.Add(label1);
            Controls.Add(pageContents);
            Controls.Add(pageList);
            Controls.Add(saveButton);
            Name = "Form1";
            Text = "Local Website";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button newButton;
        private Button saveButton;
        private ComboBox pageList;
        private Panel pageContents;
        private Label label1;
        private Button SettingsButton;
        private Button removebutton;
        private Label webnamelabel;
    }
}
