/*******************************************************************************
* Copyright (C) 2022 Maxim Integrated Products, Inc., All Rights Reserved.
*
* Permission is hereby granted, free of charge, to any person obtaining a
* copy of this software and associated documentation files (the "Software"),
* to deal in the Software without restriction, including without limitation
* the rights to use, copy, modify, merge, publish, distribute, sublicense,
* and/or sell copies of the Software, and to permit persons to whom the
* Software is furnished to do so, subject to the following conditions:
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
*******************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ds2502_read_write
{
    public partial class ADISplashScreenForm : Form
    {
        #region Public Fields
        public bool FormActive = false;

        #endregion

        #region Private Fields
        private Timer timer = new Timer();
        bool timerEnabled = false;
        #endregion

        #region Other Fields

        #endregion

        #region Public Properties

        #endregion

        #region Private Properties

        #endregion

        #region Other Properties

        #endregion

        #region Public Methods
        public ADISplashScreenForm(string version, bool _timerEnabled = true, int timeout = 3)
        {
            InitializeComponent();
            this.adiSplashScreen.VersionString = "Version " + version;
            adiSplashScreen.Checked = Properties.Settings.Default.disableSplashScreen;
            timerEnabled = _timerEnabled;
            adiSplashScreen.DismissTime = timeout;

            if (!timerEnabled)
                SetToHelpWindow();

        }

        void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            if (timerEnabled == false)
                return;
            FormActive = false;

            this.Close();
        }
        void LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = (LinkLabel)sender;
            System.Diagnostics.Process.Start("http://" + linkLabel.Text);
        }

        void DisableSplashScreenClicked(object sender, EventArgs e)
        {
            if ((sender as CheckBox) != null)
                Properties.Settings.Default.disableSplashScreen = (sender as CheckBox).Checked;
            Properties.Settings.Default.Save();
            
        }

        public void SetToHelpWindow()
        {
            // change behavior of window to be a help window
            timer.Enabled = false;
            adiButtonOK.Visible = true;
        }
        #endregion

  

        private void adiButtonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ADISplashScreenForm_Load(object sender, EventArgs e)
        {
            FormActive = true;
            adiSplashScreen.ConfigureTimer(timer, new EventHandler(timer_Tick));
            adiSplashScreen.LinkClicked += new LinkLabelLinkClickedEventHandler(LinkClicked);
            adiSplashScreen.DisableSplashScreenClicked += new EventHandler(DisableSplashScreenClicked);
        }
    }


    #region Other Methods

    #endregion
}
