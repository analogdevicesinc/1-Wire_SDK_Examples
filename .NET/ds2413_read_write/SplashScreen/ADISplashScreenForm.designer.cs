/*******************************************************************************
* Copyright (C) 2022 Maxim Integrated Products, Inc., All rights Reserved.
* 
* This software is protected by copyright laws of the United States and
* of foreign countries. This material may also be protected by patent laws
* and technology transfer regulations of the United States and of foreign
* countries. This software is furnished under a license agreement and/or a
* nondisclosure agreement and may only be used or reproduced in accordance
* with the terms of those agreements. Dissemination of this information to
* any party or parties not specified in the license agreement and/or
* nondisclosure agreement is expressly prohibited.
*
* The above copyright notice and this permission notice shall be included
* in all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
* OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
* MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
* IN NO EVENT SHALL MAXIM INTEGRATED BE LIABLE FOR ANY CLAIM, DAMAGES
* OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
* ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
* OTHER DEALINGS IN THE SOFTWARE.
*
* Except as contained in this notice, the name of Maxim Integrated
* Products, Inc. shall not be used except as stated in the Maxim Integrated
* Products, Inc. Branding Policy.
*
* The mere transfer of this software does not imply any licenses
* of trade secrets, proprietary technology, copyrights, patents,
* trademarks, maskwork rights, or any other form of intellectual
* property whatsoever. Maxim Integrated Products, Inc. retains all
* ownership rights.
*******************************************************************************
*/

namespace ds2413_read_write
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
         this.adiSplashScreen.ApplicationName = "DS2413 Read/Write";
         this.adiSplashScreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
         this.adiSplashScreen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("adiSplashScreen.BackgroundImage")));
         this.adiSplashScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
         this.adiSplashScreen.CheckBoxVisible = true;
         this.adiSplashScreen.Checked = false;
         this.adiSplashScreen.CopyrightString = "Copyright (C) 2022 Analog Devices, Inc.";
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