/*******************************************************************************
*   Copyright (c) 2024 Analog Devices, Inc. All Rights Reserved.
*   This software is proprietary to Analog Devices, Inc. and its licensors.
*
*   Licensed under the Apache License, Version 2.0 (the "License");
*   you may not use this file except in compliance with the License.
*
*   Unless required by applicable law or agreed to in writing, software
*   distributed under the License is distributed on an "AS IS" BASIS,
*   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
*   See the License for the specific language governing permissions and
*   limitations under the License. 
*/

namespace ds28e05_read_write
{
    partial class ADISplashScreenForm
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ADISplashScreenForm));
         this.adiButtonOK = new LookAndFeel.ADIButton();
         this.adiSplashScreen = new LookAndFeel.ADISplashScreen();
         this.SuspendLayout();
         // 
         // adiButtonOK
         // 
         this.adiButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.adiButtonOK.Font = new System.Drawing.Font("Barlow", 9F);
         this.adiButtonOK.Location = new System.Drawing.Point(387, 355);
         this.adiButtonOK.Name = "adiButtonOK";
         this.adiButtonOK.Size = new System.Drawing.Size(68, 23);
         this.adiButtonOK.TabIndex = 1;
         this.adiButtonOK.Text = "OK";
         this.adiButtonOK.UseVisualStyleBackColor = true;
         this.adiButtonOK.Visible = false;
         this.adiButtonOK.Click += new System.EventHandler(this.adiButtonOK_Click);
         // 
         // adiSplashScreen
         // 
         this.adiSplashScreen.AccessibleName = "adiSplashScreen";
         this.adiSplashScreen.ApplicationName = "DS28E05 Read/Write";
         this.adiSplashScreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
         this.adiSplashScreen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("adiSplashScreen.BackgroundImage")));
         this.adiSplashScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
         this.adiSplashScreen.CheckBoxVisible = true;
         this.adiSplashScreen.Checked = false;
         this.adiSplashScreen.CopyrightString = "Copyright (C) 2024 Analog Devices, Inc.";
         this.adiSplashScreen.Dock = System.Windows.Forms.DockStyle.Fill;
         this.adiSplashScreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
         this.adiSplashScreen.Location = new System.Drawing.Point(0, 0);
         this.adiSplashScreen.Margin = new System.Windows.Forms.Padding(12, 14, 12, 14);
         this.adiSplashScreen.Name = "adiSplashScreen";
         this.adiSplashScreen.NonADICopyrightHeight = -1;
         this.adiSplashScreen.NonADICopyrightString = "";
         this.adiSplashScreen.Size = new System.Drawing.Size(500, 400);
         this.adiSplashScreen.TabIndex = 0;
         this.adiSplashScreen.VersionString = "Version 0.0.1";
         // 
         // ADISplashScreenForm
         // 
         this.AccessibleName = "ADISplashScreenForm";
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
         this.ClientSize = new System.Drawing.Size(500, 400);
         this.Controls.Add(this.adiButtonOK);
         this.Controls.Add(this.adiSplashScreen);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "ADISplashScreenForm";
         this.ShowIcon = false;
         this.ShowInTaskbar = false;
         this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "ADISplashScreenForm";
         this.Load += new System.EventHandler(this.ADISplashScreenForm_Load);
         this.ResumeLayout(false);

        }

        #endregion

        private LookAndFeel.ADISplashScreen adiSplashScreen;
        private LookAndFeel.ADIButton adiButtonOK;
    }
}