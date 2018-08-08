namespace VAuthDemo
{
    partial class Main
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
            this.recordButton = new System.Windows.Forms.Button();
            this.comboWaveInDevice = new System.Windows.Forms.ComboBox();
            this.deviceTimer = new System.Windows.Forms.Timer(this.components);
            this.labelDevice = new System.Windows.Forms.Label();
            this.playButton = new System.Windows.Forms.Button();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.authButton = new System.Windows.Forms.Button();
            this.testVoxCeleb = new System.Windows.Forms.Button();
            this.voxPathTextBox = new System.Windows.Forms.TextBox();
            this.voxLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // recordButton
            // 
            this.recordButton.Location = new System.Drawing.Point(18, 41);
            this.recordButton.Name = "recordButton";
            this.recordButton.Size = new System.Drawing.Size(288, 72);
            this.recordButton.TabIndex = 0;
            this.recordButton.Text = "Hold To Record";
            this.recordButton.UseVisualStyleBackColor = true;
            this.recordButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.recordButton_MouseDown);
            this.recordButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.recordButton_MouseUp);
            // 
            // comboWaveInDevice
            // 
            this.comboWaveInDevice.FormattingEnabled = true;
            this.comboWaveInDevice.Location = new System.Drawing.Point(65, 14);
            this.comboWaveInDevice.Name = "comboWaveInDevice";
            this.comboWaveInDevice.Size = new System.Drawing.Size(241, 21);
            this.comboWaveInDevice.TabIndex = 1;
            // 
            // deviceTimer
            // 
            this.deviceTimer.Enabled = true;
            this.deviceTimer.Tick += new System.EventHandler(this.deviceTimer_Tick);
            // 
            // labelDevice
            // 
            this.labelDevice.AutoSize = true;
            this.labelDevice.Location = new System.Drawing.Point(15, 17);
            this.labelDevice.Name = "labelDevice";
            this.labelDevice.Size = new System.Drawing.Size(44, 13);
            this.labelDevice.TabIndex = 2;
            this.labelDevice.Text = "Device:";
            // 
            // playButton
            // 
            this.playButton.Location = new System.Drawing.Point(18, 119);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(75, 23);
            this.playButton.TabIndex = 3;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(189, 122);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(117, 20);
            this.passwordTextBox.TabIndex = 4;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(117, 125);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(56, 13);
            this.passwordLabel.TabIndex = 5;
            this.passwordLabel.Text = "Password:";
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(117, 153);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(58, 13);
            this.usernameLabel.TabIndex = 6;
            this.usernameLabel.Text = "Username:";
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(189, 150);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(117, 20);
            this.usernameTextBox.TabIndex = 7;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(18, 148);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 8;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // authButton
            // 
            this.authButton.Location = new System.Drawing.Point(18, 177);
            this.authButton.Name = "authButton";
            this.authButton.Size = new System.Drawing.Size(75, 23);
            this.authButton.TabIndex = 9;
            this.authButton.Text = "Authenticate";
            this.authButton.UseVisualStyleBackColor = true;
            this.authButton.Click += new System.EventHandler(this.authButton_Click);
            // 
            // testVoxCeleb
            // 
            this.testVoxCeleb.Location = new System.Drawing.Point(18, 206);
            this.testVoxCeleb.Name = "testVoxCeleb";
            this.testVoxCeleb.Size = new System.Drawing.Size(75, 23);
            this.testVoxCeleb.TabIndex = 12;
            this.testVoxCeleb.Text = "VoxCeleb";
            this.testVoxCeleb.UseVisualStyleBackColor = true;
            this.testVoxCeleb.Click += new System.EventHandler(this.testVoxCeleb_Click);
            // 
            // voxPathTextBox
            // 
            this.voxPathTextBox.Location = new System.Drawing.Point(189, 208);
            this.voxPathTextBox.Name = "voxPathTextBox";
            this.voxPathTextBox.Size = new System.Drawing.Size(117, 20);
            this.voxPathTextBox.TabIndex = 11;
            this.voxPathTextBox.Text = "E:\\Desktop\\Jobs\\Voice Authentication\\Research\\vox1_test_wav\\wav";
            // 
            // voxLabel
            // 
            this.voxLabel.AutoSize = true;
            this.voxLabel.Location = new System.Drawing.Point(117, 211);
            this.voxLabel.Name = "voxLabel";
            this.voxLabel.Size = new System.Drawing.Size(28, 13);
            this.voxLabel.TabIndex = 10;
            this.voxLabel.Text = "Vox:";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 256);
            this.Controls.Add(this.testVoxCeleb);
            this.Controls.Add(this.voxPathTextBox);
            this.Controls.Add(this.voxLabel);
            this.Controls.Add(this.authButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.usernameTextBox);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.labelDevice);
            this.Controls.Add(this.comboWaveInDevice);
            this.Controls.Add(this.recordButton);
            this.Name = "Main";
            this.Text = "VAuth Demo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button recordButton;
        private System.Windows.Forms.ComboBox comboWaveInDevice;
        private System.Windows.Forms.Timer deviceTimer;
        private System.Windows.Forms.Label labelDevice;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button authButton;
        private System.Windows.Forms.Button testVoxCeleb;
        private System.Windows.Forms.TextBox voxPathTextBox;
        private System.Windows.Forms.Label voxLabel;
    }
}

