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

/**
* @file            form_ds28e05_read_write.cs
* @brief           C# main code for the ds28e05_read_write GUI that reads/writes
*                  ds28e05 devices.
* @version         1.0.0.0.  Version is in AssemblyInfo.cs code module
* @notes           Compiled with Visual Studio 2017
*                  Requires: .NET Framework 4.8 developer pack installed separately
*                  https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48
*                  Requires: 1-Wire Drivers 4 or higher installed separately.
*                  https://www.maximintegrated.com/en/products/ibutton-one-wire/one-wire/software-tools/drivers.html
*                                   
*****************************************************************************/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
// using the OneWireLinkLayer
using DalSemi.OneWire.Adapter;
using DalSemi.OneWire;
using LookAndFeel;


namespace ds28e05_read_write
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class form_ds28e05_read_write : System.Windows.Forms.Form
	{
      // DS28E05 constants
      public const byte DS28E05_READ_MEMORY = 0xF0;
      public const byte DS28E05_WRITE_MEMORY = 0x55;
      public const byte DS28E05_TPROG = 16; // program time for writing two bytes in milliseconds
      public const int DS28E05_MEMORY_SIZE = 112; // number of bytes of user memory in the DS28E05
      public const int DS28E05_ROMID_SIZE = 8; // 8 bytes
      public const int DS28E05_FAMILY_CODE = 0x0D; // first byte of the serial number
      public const int DS28E05_MEMORY_PAGE_SIZE = 16; // 16 bytes per page
      public const int DS28E05_PAGE_PROTECTION_SIZE = 4; // 4 bytes
      public const int DS28E05_PAGE_PROTECTION_ADDRESS = 0x70; // 0x70 is the starting address of the page protection bytes -- used to set page properties for page 0-7 and Copy Lock memory areas.
      public const int DS28E05_COPY_LOCK_OPEN = 0x00; // Setting for upper nybble of address 0x73 "Copy Lock".  It is protection for the page protection bytes PPA,PPB,PPC, and PPD. 0x00 is open (not protected)
      public const int DS28E05_COPY_LOCK_WRITE_PROTECT = 0x05; // Setting for upper nybble of address 0x73 "Copy Lock".  It is protection for the page protection bytes PPA,PPB,PPC, and PPD. Anything other than 0x00 is write protected
      public const int DS28E05_PAGE_SETTINGS_OPEN = 0x00; // Page 0-7 write protection.  Open means "not protected"
      public const int DS28E05_PAGE_SETTINGS_WRITE_PROTECT = 0x05; // Page 0-7 write protection. Anything other than 0x00 or 0x0A is write protected.
      public const int DS28E05_PAGE_SETTINGS_EPROM_EMULATION = 0x0A; // Page 0-7 write protection. 0x0A means eprom-emulation. Only bits that are 1 can be changed (written to 0). 

      public PortAdapter adapter = null;  // Declare port adapter
      //The following section intiates timer for splash screen 
      Timer timer = new Timer();
      public ADISplashScreenForm adiSplashScreen = null;
      
      private System.Windows.Forms.TextBox textBoxReadResults;
      private LookAndFeel.ADIButton adiButtonRead;
		private System.Windows.Forms.StatusBar statusBarAdapter;
      private TextBox textBoxWriteResults;
      private TextBox textBoxActivityLog;
      private Label labelReadAllDevices;
      private Label labelInputHex;
      private Label label1WireActivity;
      private MenuStrip menuStrip1;
      private ToolStripMenuItem fileToolStripMenuItem;
      private ToolStripMenuItem exitToolStripMenuItem;
      private ToolStripMenuItem aboutToolStripMenuItem;
      //private GroupBox groupBoxSpeed;
      //private RadioButton radioButtonStandard;
      //private RadioButton radioButtonOverdrive;
      private ADIButton adiButton1;
      private ADIGroupBox adiGroupBoxPageSettings;
      private ADIComboBox adiComboBox0;
      private Label label0;
      private ADIButton adiButtonSettings;
      private ADIComboBox adiComboBox6;
      private ADIComboBox adiComboBox5;
      private ADIComboBox adiComboBox4;
      private ADIComboBox adiComboBox3;
      private ADIComboBox adiComboBox2;
      private ADIComboBox adiComboBox1;
      private Label label6;
      private Label label5;
      private Label label4;
      private Label label3;
      private Label label2;
      private Label label1;
      private Label labelSettings;
      private ADIComboBox adiComboBox7;
      private Label label7;

      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

		public form_ds28e05_read_write()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
	            
		}
	

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form_ds28e05_read_write));
         this.textBoxReadResults = new System.Windows.Forms.TextBox();
         this.adiButtonRead = new LookAndFeel.ADIButton();
         this.statusBarAdapter = new System.Windows.Forms.StatusBar();
         this.textBoxWriteResults = new System.Windows.Forms.TextBox();
         this.textBoxActivityLog = new System.Windows.Forms.TextBox();
         this.labelReadAllDevices = new System.Windows.Forms.Label();
         this.labelInputHex = new System.Windows.Forms.Label();
         this.label1WireActivity = new System.Windows.Forms.Label();
         this.menuStrip1 = new System.Windows.Forms.MenuStrip();
         this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.adiButton1 = new LookAndFeel.ADIButton();
         this.adiGroupBoxPageSettings = new LookAndFeel.ADIGroupBox();
         this.adiComboBox7 = new LookAndFeel.ADIComboBox();
         this.label7 = new System.Windows.Forms.Label();
         this.labelSettings = new System.Windows.Forms.Label();
         this.adiButtonSettings = new LookAndFeel.ADIButton();
         this.adiComboBox6 = new LookAndFeel.ADIComboBox();
         this.adiComboBox5 = new LookAndFeel.ADIComboBox();
         this.adiComboBox4 = new LookAndFeel.ADIComboBox();
         this.adiComboBox3 = new LookAndFeel.ADIComboBox();
         this.adiComboBox2 = new LookAndFeel.ADIComboBox();
         this.adiComboBox1 = new LookAndFeel.ADIComboBox();
         this.label6 = new System.Windows.Forms.Label();
         this.label5 = new System.Windows.Forms.Label();
         this.label4 = new System.Windows.Forms.Label();
         this.label3 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.label1 = new System.Windows.Forms.Label();
         this.adiComboBox0 = new LookAndFeel.ADIComboBox();
         this.label0 = new System.Windows.Forms.Label();
         this.menuStrip1.SuspendLayout();
         this.adiGroupBoxPageSettings.SuspendLayout();
         this.SuspendLayout();
         // 
         // textBoxReadResults
         // 
         this.textBoxReadResults.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.textBoxReadResults.Location = new System.Drawing.Point(18, 86);
         this.textBoxReadResults.Multiline = true;
         this.textBoxReadResults.Name = "textBoxReadResults";
         this.textBoxReadResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.textBoxReadResults.Size = new System.Drawing.Size(445, 217);
         this.textBoxReadResults.TabIndex = 0;
         // 
         // adiButtonRead
         // 
         this.adiButtonRead.Font = new System.Drawing.Font("Barlow", 9F);
         this.adiButtonRead.Location = new System.Drawing.Point(572, 86);
         this.adiButtonRead.Name = "adiButtonRead";
         this.adiButtonRead.Size = new System.Drawing.Size(138, 47);
         this.adiButtonRead.TabIndex = 1;
         this.adiButtonRead.Text = "&Read";
         this.adiButtonRead.UseVisualStyleBackColor = true;
         this.adiButtonRead.Click += new System.EventHandler(this.buttonRead_Click);
         // 
         // statusBarAdapter
         // 
         this.statusBarAdapter.Location = new System.Drawing.Point(0, 783);
         this.statusBarAdapter.Name = "statusBarAdapter";
         this.statusBarAdapter.Size = new System.Drawing.Size(800, 32);
         this.statusBarAdapter.TabIndex = 2;
         // 
         // textBoxWriteResults
         // 
         this.textBoxWriteResults.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.textBoxWriteResults.Location = new System.Drawing.Point(19, 342);
         this.textBoxWriteResults.Multiline = true;
         this.textBoxWriteResults.Name = "textBoxWriteResults";
         this.textBoxWriteResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.textBoxWriteResults.Size = new System.Drawing.Size(445, 216);
         this.textBoxWriteResults.TabIndex = 4;
         // 
         // textBoxActivityLog
         // 
         this.textBoxActivityLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.textBoxActivityLog.Location = new System.Drawing.Point(18, 602);
         this.textBoxActivityLog.Multiline = true;
         this.textBoxActivityLog.Name = "textBoxActivityLog";
         this.textBoxActivityLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.textBoxActivityLog.Size = new System.Drawing.Size(445, 165);
         this.textBoxActivityLog.TabIndex = 5;
         // 
         // labelReadAllDevices
         // 
         this.labelReadAllDevices.AutoSize = true;
         this.labelReadAllDevices.Font = new System.Drawing.Font("Barlow", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelReadAllDevices.Location = new System.Drawing.Point(16, 57);
         this.labelReadAllDevices.Name = "labelReadAllDevices";
         this.labelReadAllDevices.Size = new System.Drawing.Size(140, 24);
         this.labelReadAllDevices.TabIndex = 6;
         this.labelReadAllDevices.Text = "Read All Devices";
         // 
         // labelInputHex
         // 
         this.labelInputHex.AutoSize = true;
         this.labelInputHex.Font = new System.Drawing.Font("Barlow", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelInputHex.Location = new System.Drawing.Point(16, 314);
         this.labelInputHex.Name = "labelInputHex";
         this.labelInputHex.Size = new System.Drawing.Size(268, 24);
         this.labelInputHex.TabIndex = 7;
         this.labelInputHex.Text = "Input Hex to Write to All Devices";
         // 
         // label1WireActivity
         // 
         this.label1WireActivity.AutoSize = true;
         this.label1WireActivity.Font = new System.Drawing.Font("Barlow", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.label1WireActivity.Location = new System.Drawing.Point(16, 574);
         this.label1WireActivity.Name = "label1WireActivity";
         this.label1WireActivity.Size = new System.Drawing.Size(160, 24);
         this.label1WireActivity.TabIndex = 8;
         this.label1WireActivity.Text = "1-Wire Activity Log";
         // 
         // menuStrip1
         // 
         this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
         this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
         this.menuStrip1.Location = new System.Drawing.Point(0, 0);
         this.menuStrip1.Name = "menuStrip1";
         this.menuStrip1.Size = new System.Drawing.Size(800, 33);
         this.menuStrip1.TabIndex = 9;
         this.menuStrip1.Text = "menuStrip1";
         // 
         // fileToolStripMenuItem
         // 
         this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
         this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
         this.fileToolStripMenuItem.Size = new System.Drawing.Size(50, 29);
         this.fileToolStripMenuItem.Text = "&File";
         // 
         // exitToolStripMenuItem
         // 
         this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
         this.exitToolStripMenuItem.Size = new System.Drawing.Size(123, 30);
         this.exitToolStripMenuItem.Text = "E&xit";
         this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
         // 
         // aboutToolStripMenuItem
         // 
         this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
         this.aboutToolStripMenuItem.Size = new System.Drawing.Size(74, 29);
         this.aboutToolStripMenuItem.Text = "A&bout";
         this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
         // 
         // adiButton1
         // 
         this.adiButton1.Font = new System.Drawing.Font("Barlow", 9F);
         this.adiButton1.Location = new System.Drawing.Point(572, 195);
         this.adiButton1.Name = "adiButton1";
         this.adiButton1.Size = new System.Drawing.Size(138, 47);
         this.adiButton1.TabIndex = 3;
         this.adiButton1.Text = "&Write";
         this.adiButton1.Click += new System.EventHandler(this.buttonWrite_Click);
         // 
         // adiGroupBoxPageSettings
         // 
         this.adiGroupBoxPageSettings.BackColor = System.Drawing.SystemColors.Control;
         this.adiGroupBoxPageSettings.BackgroundColor = System.Drawing.SystemColors.Control;
         this.adiGroupBoxPageSettings.Controls.Add(this.adiComboBox7);
         this.adiGroupBoxPageSettings.Controls.Add(this.label7);
         this.adiGroupBoxPageSettings.Controls.Add(this.labelSettings);
         this.adiGroupBoxPageSettings.Controls.Add(this.adiButtonSettings);
         this.adiGroupBoxPageSettings.Controls.Add(this.adiComboBox6);
         this.adiGroupBoxPageSettings.Controls.Add(this.adiComboBox5);
         this.adiGroupBoxPageSettings.Controls.Add(this.adiComboBox4);
         this.adiGroupBoxPageSettings.Controls.Add(this.adiComboBox3);
         this.adiGroupBoxPageSettings.Controls.Add(this.adiComboBox2);
         this.adiGroupBoxPageSettings.Controls.Add(this.adiComboBox1);
         this.adiGroupBoxPageSettings.Controls.Add(this.label6);
         this.adiGroupBoxPageSettings.Controls.Add(this.label5);
         this.adiGroupBoxPageSettings.Controls.Add(this.label4);
         this.adiGroupBoxPageSettings.Controls.Add(this.label3);
         this.adiGroupBoxPageSettings.Controls.Add(this.label2);
         this.adiGroupBoxPageSettings.Controls.Add(this.label1);
         this.adiGroupBoxPageSettings.Controls.Add(this.adiComboBox0);
         this.adiGroupBoxPageSettings.Controls.Add(this.label0);
         this.adiGroupBoxPageSettings.Font = new System.Drawing.Font("Barlow Medium", 9F);
         this.adiGroupBoxPageSettings.Location = new System.Drawing.Point(488, 303);
         this.adiGroupBoxPageSettings.Name = "adiGroupBoxPageSettings";
         this.adiGroupBoxPageSettings.Size = new System.Drawing.Size(291, 464);
         this.adiGroupBoxPageSettings.TabIndex = 12;
         this.adiGroupBoxPageSettings.TabStop = false;
         this.adiGroupBoxPageSettings.Text = "Page Settings";
         this.adiGroupBoxPageSettings.TitleEnable = false;
         // 
         // adiComboBox7
         // 
         this.adiComboBox7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
         this.adiComboBox7.DropDownHeight = 200;
         this.adiComboBox7.DropDownWidth = 0;
         this.adiComboBox7.Font = new System.Drawing.Font("Barlow Medium", 9F);
         this.adiComboBox7.Items.AddRange(new object[] {
            "Open",
            "Locked"});
         this.adiComboBox7.Location = new System.Drawing.Point(84, 307);
         this.adiComboBox7.Name = "adiComboBox7";
         this.adiComboBox7.Size = new System.Drawing.Size(187, 31);
         this.adiComboBox7.TabIndex = 18;
         this.adiComboBox7.Text = "Open";
         this.adiComboBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
         this.adiComboBox7.TextAlignDropDownList = System.Windows.Forms.HorizontalAlignment.Left;
         // 
         // label7
         // 
         this.label7.AutoSize = true;
         this.label7.Location = new System.Drawing.Point(6, 307);
         this.label7.Name = "label7";
         this.label7.Size = new System.Drawing.Size(71, 24);
         this.label7.TabIndex = 17;
         this.label7.Text = "PP A-D:";
         // 
         // labelSettings
         // 
         this.labelSettings.AutoSize = true;
         this.labelSettings.Font = new System.Drawing.Font("Barlow", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelSettings.Location = new System.Drawing.Point(6, 8);
         this.labelSettings.Name = "labelSettings";
         this.labelSettings.Size = new System.Drawing.Size(141, 27);
         this.labelSettings.TabIndex = 13;
         this.labelSettings.Text = "Page Settings:";
         // 
         // adiButtonSettings
         // 
         this.adiButtonSettings.Font = new System.Drawing.Font("Barlow", 9F);
         this.adiButtonSettings.Location = new System.Drawing.Point(84, 392);
         this.adiButtonSettings.Name = "adiButtonSettings";
         this.adiButtonSettings.Size = new System.Drawing.Size(138, 47);
         this.adiButtonSettings.TabIndex = 16;
         this.adiButtonSettings.Text = "Write &Settings";
         this.adiButtonSettings.Click += new System.EventHandler(this.adiButtonSettings_Click);
         // 
         // adiComboBox6
         // 
         this.adiComboBox6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
         this.adiComboBox6.DropDownHeight = 200;
         this.adiComboBox6.DropDownWidth = 0;
         this.adiComboBox6.Font = new System.Drawing.Font("Barlow Medium", 9F);
         this.adiComboBox6.Items.AddRange(new object[] {
            "Open",
            "Write Protect",
            "Eprom Emulation"});
         this.adiComboBox6.Location = new System.Drawing.Point(84, 270);
         this.adiComboBox6.Name = "adiComboBox6";
         this.adiComboBox6.Size = new System.Drawing.Size(187, 31);
         this.adiComboBox6.TabIndex = 14;
         this.adiComboBox6.Text = "Open";
         this.adiComboBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
         this.adiComboBox6.TextAlignDropDownList = System.Windows.Forms.HorizontalAlignment.Left;
         // 
         // adiComboBox5
         // 
         this.adiComboBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
         this.adiComboBox5.DropDownHeight = 200;
         this.adiComboBox5.DropDownWidth = 0;
         this.adiComboBox5.Font = new System.Drawing.Font("Barlow Medium", 9F);
         this.adiComboBox5.Items.AddRange(new object[] {
            "Open",
            "Write Protect",
            "Eprom Emulation"});
         this.adiComboBox5.Location = new System.Drawing.Point(84, 233);
         this.adiComboBox5.Name = "adiComboBox5";
         this.adiComboBox5.Size = new System.Drawing.Size(187, 31);
         this.adiComboBox5.TabIndex = 13;
         this.adiComboBox5.Text = "Open";
         this.adiComboBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
         this.adiComboBox5.TextAlignDropDownList = System.Windows.Forms.HorizontalAlignment.Left;
         // 
         // adiComboBox4
         // 
         this.adiComboBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
         this.adiComboBox4.DropDownHeight = 200;
         this.adiComboBox4.DropDownWidth = 0;
         this.adiComboBox4.Font = new System.Drawing.Font("Barlow Medium", 9F);
         this.adiComboBox4.Items.AddRange(new object[] {
            "Open",
            "Write Protect",
            "Eprom Emulation"});
         this.adiComboBox4.Location = new System.Drawing.Point(84, 196);
         this.adiComboBox4.Name = "adiComboBox4";
         this.adiComboBox4.Size = new System.Drawing.Size(187, 31);
         this.adiComboBox4.TabIndex = 12;
         this.adiComboBox4.Text = "Open";
         this.adiComboBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
         this.adiComboBox4.TextAlignDropDownList = System.Windows.Forms.HorizontalAlignment.Left;
         // 
         // adiComboBox3
         // 
         this.adiComboBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
         this.adiComboBox3.DropDownHeight = 200;
         this.adiComboBox3.DropDownWidth = 0;
         this.adiComboBox3.Font = new System.Drawing.Font("Barlow Medium", 9F);
         this.adiComboBox3.Items.AddRange(new object[] {
            "Open",
            "Write Protect",
            "Eprom Emulation"});
         this.adiComboBox3.Location = new System.Drawing.Point(84, 159);
         this.adiComboBox3.Name = "adiComboBox3";
         this.adiComboBox3.Size = new System.Drawing.Size(187, 31);
         this.adiComboBox3.TabIndex = 11;
         this.adiComboBox3.Text = "Open";
         this.adiComboBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
         this.adiComboBox3.TextAlignDropDownList = System.Windows.Forms.HorizontalAlignment.Left;
         // 
         // adiComboBox2
         // 
         this.adiComboBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
         this.adiComboBox2.DropDownHeight = 200;
         this.adiComboBox2.DropDownWidth = 0;
         this.adiComboBox2.Font = new System.Drawing.Font("Barlow Medium", 9F);
         this.adiComboBox2.Items.AddRange(new object[] {
            "Open",
            "Write Protect",
            "Eprom Emulation"});
         this.adiComboBox2.Location = new System.Drawing.Point(84, 122);
         this.adiComboBox2.Name = "adiComboBox2";
         this.adiComboBox2.Size = new System.Drawing.Size(187, 31);
         this.adiComboBox2.TabIndex = 10;
         this.adiComboBox2.Text = "Open";
         this.adiComboBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
         this.adiComboBox2.TextAlignDropDownList = System.Windows.Forms.HorizontalAlignment.Left;
         // 
         // adiComboBox1
         // 
         this.adiComboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
         this.adiComboBox1.DropDownHeight = 200;
         this.adiComboBox1.DropDownWidth = 0;
         this.adiComboBox1.Font = new System.Drawing.Font("Barlow Medium", 9F);
         this.adiComboBox1.Items.AddRange(new object[] {
            "Open",
            "Write Protect",
            "Eprom Emulation"});
         this.adiComboBox1.Location = new System.Drawing.Point(84, 85);
         this.adiComboBox1.Name = "adiComboBox1";
         this.adiComboBox1.Size = new System.Drawing.Size(187, 31);
         this.adiComboBox1.TabIndex = 9;
         this.adiComboBox1.Text = "Open";
         this.adiComboBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
         this.adiComboBox1.TextAlignDropDownList = System.Windows.Forms.HorizontalAlignment.Left;
         // 
         // label6
         // 
         this.label6.AutoSize = true;
         this.label6.Location = new System.Drawing.Point(6, 270);
         this.label6.Name = "label6";
         this.label6.Size = new System.Drawing.Size(69, 24);
         this.label6.TabIndex = 7;
         this.label6.Text = "Page 6:";
         // 
         // label5
         // 
         this.label5.AutoSize = true;
         this.label5.Location = new System.Drawing.Point(6, 233);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(69, 24);
         this.label5.TabIndex = 6;
         this.label5.Text = "Page 5:";
         // 
         // label4
         // 
         this.label4.AutoSize = true;
         this.label4.Location = new System.Drawing.Point(6, 196);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(70, 24);
         this.label4.TabIndex = 5;
         this.label4.Text = "Page 4:";
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(6, 159);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(69, 24);
         this.label3.TabIndex = 4;
         this.label3.Text = "Page 3:";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(6, 122);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(70, 24);
         this.label2.TabIndex = 3;
         this.label2.Text = "Page 2:";
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(6, 85);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(66, 24);
         this.label1.TabIndex = 2;
         this.label1.Text = "Page 1:";
         // 
         // adiComboBox0
         // 
         this.adiComboBox0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
         this.adiComboBox0.DropDownHeight = 200;
         this.adiComboBox0.DropDownWidth = 0;
         this.adiComboBox0.Font = new System.Drawing.Font("Barlow Medium", 9F);
         this.adiComboBox0.Items.AddRange(new object[] {
            "Open",
            "Write Protect",
            "Eprom Emulation"});
         this.adiComboBox0.Location = new System.Drawing.Point(84, 48);
         this.adiComboBox0.Name = "adiComboBox0";
         this.adiComboBox0.Size = new System.Drawing.Size(187, 31);
         this.adiComboBox0.TabIndex = 0;
         this.adiComboBox0.Text = "Open";
         this.adiComboBox0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
         this.adiComboBox0.TextAlignDropDownList = System.Windows.Forms.HorizontalAlignment.Left;
         // 
         // label0
         // 
         this.label0.AutoSize = true;
         this.label0.Location = new System.Drawing.Point(6, 48);
         this.label0.Name = "label0";
         this.label0.Size = new System.Drawing.Size(70, 24);
         this.label0.TabIndex = 1;
         this.label0.Text = "Page 0:";
         // 
         // form_ds28e05_read_write
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
         this.ClientSize = new System.Drawing.Size(800, 815);
         this.Controls.Add(this.adiGroupBoxPageSettings);
         this.Controls.Add(this.adiButtonRead);
         this.Controls.Add(this.adiButton1);
         this.Controls.Add(this.label1WireActivity);
         this.Controls.Add(this.labelInputHex);
         this.Controls.Add(this.labelReadAllDevices);
         this.Controls.Add(this.textBoxActivityLog);
         this.Controls.Add(this.textBoxWriteResults);
         this.Controls.Add(this.statusBarAdapter);
         this.Controls.Add(this.textBoxReadResults);
         this.Controls.Add(this.menuStrip1);
         this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MainMenuStrip = this.menuStrip1;
         this.MaximizeBox = false;
         this.Name = "form_ds28e05_read_write";
         this.Text = "DS28E05 Read/Write";
         this.Load += new System.EventHandler(this.form_ds28e05_read_write_Load);
         this.menuStrip1.ResumeLayout(false);
         this.menuStrip1.PerformLayout();
         this.adiGroupBoxPageSettings.ResumeLayout(false);
         this.adiGroupBoxPageSettings.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new form_ds28e05_read_write());
		}

      /**
      * @brief        getProgramVersion() retrieves the program version from the AssemblyInfo.cs
      *               where Visual Studio likes to keep it.
      * @param[in]    None.
      * @return       A string that representing the version number of the program and assembly file.
      ****************************************************************************/
      private static string getProgramVersion()
      {
         return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
      }

      /**
      * @brief        Print1WireHexAddress() takes a byte buffer representing a 1-Wire serial number
      *               and converts it to a hexadecimal string and returns it.
      * @param[in]    Array of bytes representing a 1-Wire serial number.
      * @return       A hexadecimal string representing the 1-Wire serial number.
      ****************************************************************************/
      private static string Print1WireHexAddress(byte[] buff)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder(buff.Length*3);
			for(int i=7; i>-1; i--)
			{
				sb.Append(buff[i].ToString("X2"));
			}
			return sb.ToString();
		}

      /**
      * @brief        Print1WireHexAddress() takes a byte buffer representing a 1-Wire serial number
      *               and converts it to a hexadecimal string and returns it.
      * @param[in]    Array of bytes representing a 1-Wire serial number.
      * @return       A hexadecimal string representing the 1-Wire serial number.
      ****************************************************************************/
      private static string Print1WireHexBytes(byte[] buff, int ValuesPerLine)
      {
         System.Text.StringBuilder sb = new System.Text.StringBuilder(buff.Length * 2);
         for (int i = 0; i < buff.Length; i++)
         {
               if ((i % ValuesPerLine == 0) && (i>0)) sb.Append(Environment.NewLine);
               sb.Append(buff[i].ToString("X2"));
         }
         return sb.ToString();
      }

      /**
      * @brief        HexStringToByteArray() takes a hexadecimal string as input and 
      *               converts it to an array of bytes. Use it in a try/catch to catch the 
      *               case where the string has an odd number of digits.
      * @param[in]    Hexadecimal string.
      * @return       Array of bytes.
      ****************************************************************************/
      public static byte[] HexStringToByteArray(string hex)
      {
         if (hex.Length % 2 == 1)
            throw new Exception("The binary key cannot have an odd number of digits");

         byte[] arr = new byte[hex.Length >> 1];

         for (int i = 0; i < hex.Length >> 1; ++i)
         {
            arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
         }

         return arr;
      }

      /**
      * @brief        GetHexVal() takes a hexadecimal character and returns an integer.
      * @param[in]    Hexadecimal character (0-f or 0-F).
      * @return       integer of the converted char.
      ****************************************************************************/
      public static int GetHexVal(char hex)
      {
         int val = (int)hex;
         //For uppercase A-F letters:
         //return val - (val < 58 ? 48 : 55);
         //For lowercase a-f letters:
         //return val - (val < 58 ? 48 : 87);
         //Or the two combined, but a bit slower:
         return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
      }

      /**
      * @brief        RemoveLineEndings() takes string and removes all spaces, line endings, and line separators.
      * @param[in]    string.
      * @return       the same string with no spaces, line endings, or line separators.
      ****************************************************************************/
      public static string RemoveLineEndings(string value)
      {
         if (String.IsNullOrEmpty(value))
         {
            return value;
         }
         string lineSeparator = ((char)0x2028).ToString();
         string paragraphSeparator = ((char)0x2029).ToString();

         return value.Replace(" ", string.Empty).Replace("\r\n", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty).Replace(lineSeparator, string.Empty).Replace(paragraphSeparator, string.Empty);
      }

      /**
      * @brief        form_ds28e05_read_write_Load() event gets called when the 
      *               windows form is being loaded by the operating system. This event  
      *               is where the 1-Wire adapters are auto-detected and where the splash 
      *               screen on startup gets displayed before the main window gets drawn.
      * @param[in]    sender -- standard .NET event parameter.
      * @param[in]    e -- standard .NET event parameter.
      * @return       the same string with no spaces, line endings, or line separators.
      ****************************************************************************/
      private void form_ds28e05_read_write_Load(object sender, EventArgs e)
      {
         try
         {
            // getFirstAdapterFound() either gets the first adapter found or returns null.
            adapter = getFirstAdapterFound();
            adapter.Reset();
            adapter.PutByte(0x3C);
            adapter.Speed = OWSpeed.SPEED_OVERDRIVE;
            statusBarAdapter.Text = adapter.ToString();
         }
         catch (Exception ex)
         {
            string exceptionString = ex.Message;
            MessageBox.Show("1-Wire PC adapter not found or loaded. Close this \n" +
                            "program and check if a 1-Wire adapter is plugged in \n");
            statusBarAdapter.Text = "Not Loaded";
            adapter = null;
            exceptionString = "";
         }
         // take care of splash screen
         this.Hide();
         if (Properties.Settings.Default.disableSplashScreen != true)
         {
            adiSplashScreen = new ADISplashScreenForm(getProgramVersion(), true, 3); // create splashscreen with timer enabled for 3 seconds
            ShowSplashScreen(); // show splashscreen
         }
         this.Show();
      }

      /**
      * @brief        getFirstAdapterFound() takes string and removes all spaces, line endings, and line separators.
      * @pre          This gets called in form_ds28e05_read_write_Load() to auto-detect the adapters.
      * @return       The PortAdapter found or null.
      ****************************************************************************/
      private PortAdapter getFirstAdapterFound()
      {
         PortAdapter adapter = null;
         string exmessage = null;
         try
         {
            adapter = AccessProvider.GetAdapter("{DS9490}", "USB1");
            if (adapter != null) return adapter;
         }
         catch (Exception ex)
         {
            exmessage = ex.Message;        
         }
         for (int i = 0; i < 64; i++) // checking for an adapter on COM1 to COM64
         {
            try
            {
               adapter = null;
               adapter = AccessProvider.GetAdapter("DS9097U", ("COM" + (i + 1).ToString()));
               if (adapter != null) return adapter;
            }
            catch (Exception ex)
            {
               exmessage = ex.Message;
            }
         }
         adapter = null;
         return adapter;
      }

      /**
      * @brief        buttonWrite_Click() event gets called when the "write" button 
      *               gets clicked. This event is where the string data found in the  
      *               hex input box gets converted to a byte array and then written 
      *               to the ds28e05-type device.
      * @param[in]    sender -- standard .NET event parameter.
      * @param[in]    e -- standard .NET event parameter.
      ****************************************************************************/
      private void buttonWrite_Click(object sender, EventArgs e)
      {
         // create a buffer to write
         byte[] writebuffer = new byte[DS28E05_MEMORY_SIZE]; // This will hold the memory contents to eventually write to the device in byte form.

         byte[] inputbuffer; // byte array to hold the memory contents in byte form coming from the text box.
         int numofrows = 0;
         int rowremainder = 0;

         // first, get the hexadecimal string to write to device from the text box, remove data greater than size of memory * 2, and place in inputbuffer
         string hexString = textBoxWriteResults.Text;
         hexString = hexString.Trim();
         hexString = RemoveLineEndings(hexString);
         if (hexString.Length > (DS28E05_MEMORY_SIZE * 2)) // Make sure string is no greater than the maximum size of the DS28E05's memory (112 bytes = 224 characters)
         {
            hexString = hexString.Remove(DS28E05_MEMORY_SIZE * 2);
         }
         try
         { 
            inputbuffer = HexStringToByteArray(hexString); // Transform hexadecimal string to inputbuffer variable 
            numofrows = inputbuffer.Length / DS28E05_MEMORY_PAGE_SIZE;
            rowremainder = (inputbuffer.Length % DS28E05_MEMORY_PAGE_SIZE);
         }
         catch (Exception ex)
         {
            MessageBox.Show("Could not convert text box numbers into hexadecimal digits. \n" + 
                            "Make sure the numbers are from 0 to F and that there are an \n" +
                            "even number of digits. Also, no spaces are allowed.","Hexadecimal Conversion Error");
            ex.Data.Clear();
            return;
         }

         if ((numofrows == 0) && (rowremainder == 0))
         {
            MessageBox.Show("No data to write. \n", "No Data Error");
            return;
         }

         if (adapter != null)
         {
            try
            {
               // get exclusive use of resource
               adapter.BeginExclusive(true);
               // clear any previous search restrictions
               adapter.SetSearchAllDevices();
               adapter.TargetAllFamilies();
               // print header to text box
               textBoxActivityLog.AppendText(Environment.NewLine + "1-Wire List:" + Environment.NewLine);
               textBoxActivityLog.AppendText("=================" + Environment.NewLine);
               // get 1-Wire Addresses
               byte[] address = new byte[DS28E05_ROMID_SIZE];
               // get the first 1-Wire device's address
               // keep in mind the first device is not necessarily the first 
               // device physically located on the network.
               if (adapter.GetFirstDevice(address, 0))
               {
                  do  // get subsequent 1-Wire device addresses
                  {

                     textBoxActivityLog.AppendText(Print1WireHexAddress(address) + Environment.NewLine);
                     if (address[0] == DS28E05_FAMILY_CODE) // is this a DS28E05?
                     {
                        byte TA1 = 0; // T6:T0  LSB
                        byte[] rowdata = new byte[DS28E05_MEMORY_PAGE_SIZE];
                        int numofpages = DS28E05_MEMORY_SIZE / DS28E05_MEMORY_PAGE_SIZE;
                        int sourceOffset = 0; 

                        writebuffer = readDS28E05(adapter, address); // first read the device memory and then copy text box contents over it
                        Buffer.BlockCopy(inputbuffer, 0, writebuffer, 0, inputbuffer.Length);
                        adapter.Reset();
                        
                        adapter.SelectDevice(address, 0);

                        // write to each row 8 bytes at a time.
                        for (int i = 0; i < numofpages; i++)
                        {
                           sourceOffset = (int)TA1;
                           Buffer.BlockCopy(writebuffer, sourceOffset, rowdata, 0, DS28E05_MEMORY_PAGE_SIZE);

                           write16Bytes(address, rowdata, TA1);

                           textBoxActivityLog.AppendText(" Write 16 bytes at address: 0x" + TA1.ToString("X2")); 
                           textBoxActivityLog.AppendText(Environment.NewLine);

                           TA1 = (byte) (TA1 + DS28E05_MEMORY_PAGE_SIZE);
                        }
                        textBoxActivityLog.AppendText(" Done writing to device");
                        textBoxActivityLog.AppendText(Environment.NewLine);
                     }
                  }
                  while (adapter.GetNextDevice(address, 0));
               }
               // end exclusive use of resource
               adapter.EndExclusive();
            }
            catch (Exception ex)
            {
               textBoxActivityLog.AppendText("Error: " + ex.Message + "\n" + ex.StackTrace + "\n" + ((ex.InnerException != null) ? ex.InnerException.Message : ""));
               statusBarAdapter.Text = "Not Loaded";
               adapter.EndExclusive();
            }
         }
      }



      /**
      * @brief        write16Bytes() This is the method that actually writes to the ds28e05-type device. The part 
      *               can only be written to 16 bytes at a time (the length of a page) by the buttonWrite_Click() event. 
      * @param[in]    address.  The 8-byte serial number.
      * @param[in]    writebuffer.  The 16 bytes of data to write to the part.
      * @param[in]    TA1.  The byte representing the least significant byte of the memory address to write.
      * @return       The PortAdapter found or null.
      ****************************************************************************/
      private bool write16Bytes(byte[] address, byte[] writebuffer, byte TA1)
      {
         byte[] readbuffer = new byte[DS28E05_MEMORY_PAGE_SIZE];

         bool returnValue = false;
         int returnbyte = 0;
         byte CS = 0;
         byte Page_Number = 0;
         byte Parameter_Byte = 0; // |0|P|P|P|S|S|S|0| where P is page # and S is segment #
         byte FF_byte = 0xFF;

         // Check TA1 to see if it is a multiple of 16
         if ((TA1 % 16) > 0) return returnValue; // check for page boundary. If not on page boundary, return fail.

         // derive the page number from memory address TA1
         Page_Number = (byte)(TA1 / DS28E05_MEMORY_PAGE_SIZE);
         //textBoxActivityLog.AppendText(" TA1 = " + TA1.ToString()+ " Page_Number = " + Page_Number.ToString() + Environment.NewLine);

         adapter.SelectDevice(address, 0); // reset + match rom
         // Perform a write  (assumes part was already discovered and selected previously)

         adapter.PutByte(DS28E05_WRITE_MEMORY); // send write memory command
         Parameter_Byte = (byte)((int)Page_Number << 4); // make parameter byte expecting the segment bits to be zero.
         adapter.PutByte(Parameter_Byte);
         adapter.PutByte(FF_byte);
         for (int i = 0; i < DS28E05_MEMORY_PAGE_SIZE; i = i + 2)
         {
            adapter.PutByte(writebuffer[i]); // first data byte
            adapter.PutByte(writebuffer[i + 1]); // second data byte
            returnbyte = adapter.GetByte(); // read back the first data byte from what was written (verifying is ignored for now)
            returnbyte = adapter.GetByte(); // read back the second data byte from what was written (verifying is ignored for now)
            adapter.PutByte(FF_byte); // send release byte (0xFF)
            System.Threading.Thread.Sleep(DS28E05_TPROG); // sleep however long it takes
            CS = (byte)adapter.GetByte(); // CS byte  (ignore for now)       
         }

         returnValue = true;
         return returnValue;
      }

      /**
      * @brief        writePPBytes() This is the method that writes the page protection settings to the ds28e05 device. These are 
      *               four consecutive bytes starting at address 0x70.
      * @param[in]    address.  The 8-byte serial number.
      * @param[in]    writebuffer.  The 16 bytes of data to write to the part.
      * @return       The PortAdapter found or null.
      ****************************************************************************/
      private bool writePPBytes(byte[] address, byte[] writebuffer)
      {
         byte[] readbuffer = new byte[DS28E05_PAGE_PROTECTION_SIZE];

         byte TA1 = (byte)DS28E05_PAGE_PROTECTION_ADDRESS;
         bool returnValue = false;
         int returnbyte = 0;
         byte CS = 0;
         byte Page_Number = 0;
         byte Parameter_Byte = 0; // |0|P|P|P|S|S|S|0| where P is page # and S is segment #
         byte FF_byte = 0xFF;

         // derive the page number from memory address TA1
         Page_Number = (byte)(TA1 / DS28E05_MEMORY_PAGE_SIZE);

         adapter.SelectDevice(address, 0); // reset + match rom
         // Perform a write  (assumes part was already discovered and selected previously)

         adapter.PutByte(DS28E05_WRITE_MEMORY); // send write memory command
         Parameter_Byte = (byte)((int)Page_Number << 4); // make parameter byte expecting the segment bits to be zero.
         adapter.PutByte(Parameter_Byte);
         adapter.PutByte(FF_byte);
         for (int i = 0; i < DS28E05_PAGE_PROTECTION_SIZE; i = i + 2)
         {
            adapter.PutByte(writebuffer[i]); // first data byte
            adapter.PutByte(writebuffer[i + 1]); // second data byte
            returnbyte = adapter.GetByte(); // read back the first data byte from what was written (verifying is ignored for now)
            returnbyte = adapter.GetByte(); // read back the second data byte from what was written (verifying is ignored for now)
            adapter.PutByte(FF_byte); // send release byte (0xFF)
            System.Threading.Thread.Sleep(DS28E05_TPROG); // sleep however long it takes
            CS = (byte)adapter.GetByte(); // CS byte  (ignore for now)       
         }

         returnValue = true;
         return returnValue;
      }


      /**
      * @brief        readds28e05() This reads the entire user memory contents of a ds28e05 (128 bytes). 
      * @param[in]    adapter.  This is the PortAdapter (1-Wire adapter) object.
      * @param[in]    address.  This is the 8-byte address of the ds28e05 to be read.
      * @pre          This requires that a 1-Wire reset and a match rom (select) has been successfully sent. 
      * @return       byte array.  128 bytes of ds28e05 user memory.
      ****************************************************************************/
      private byte[] readDS28E05(PortAdapter adapter, byte[] address)
      {
         byte[] readbuffer = new byte[DS28E05_MEMORY_SIZE];

         if (adapter != null)
         {
            try
            {
               // Perform a read and then write data
               adapter.PutByte(0xF0); // send read memory command
               adapter.PutByte(0x00); // first memory address byte 
               adapter.PutByte(0x00); // second memory byte of the address from which to read
               for (int i = 0; i < DS28E05_MEMORY_SIZE; i++)
               {
                  readbuffer[i] = (byte)adapter.GetByte();
               }

               //adapter.GetBlock(readbuffer, DS28E05_MEMORY_SIZE); // reads 8 bytes from part (4 pages)
            }
            catch (Exception ex)
            {
               textBoxReadResults.AppendText("Error: " + ex.Message + "\n" + ex.StackTrace + "\n" + ((ex.InnerException != null) ? ex.InnerException.Message : ""));
               statusBarAdapter.Text = "Not Loaded";
            }
         }
         return readbuffer;
      }

      /**
      * @brief        buttonRead_Click() event gets called when the user clicks on the  
      *               button labeled "read".  This will discover all 1-Wire devices and 
      *               if it is a ds28e05-equivalent, then it will read out the entire user 
      *               memory contents and display the contents on the Read All Devices text box.
      * @param[in]    sender -- standard .NET event parameter.
      * @param[in]    e -- standard .NET event parameter.    
      ****************************************************************************/
      private void buttonRead_Click(object sender, EventArgs e)
      {
         byte[] readbuffer = new byte[DS28E05_MEMORY_SIZE];
         byte[] pagebuffer = new byte[DS28E05_MEMORY_PAGE_SIZE];
         byte memory_address_TA1 = 0x00; // the lower address byte (TA1, T[6:0])
         byte memory_address_TA2 = 0x00; // the upper address byte (TA2, T[15:8]) which must be 00 to be valid.
         string tempstr = "";

         if (adapter != null)
         {
            try
            {
               // get exclusive use of resource
               adapter.BeginExclusive(true);
               // clear any previous search restrictions
               adapter.SetSearchAllDevices();
               adapter.TargetAllFamilies();
               // print header to text box
               textBoxActivityLog.AppendText(Environment.NewLine + "1-Wire List:" + Environment.NewLine);
               textBoxActivityLog.AppendText("=================" + Environment.NewLine);
               // get 1-Wire Addresses
               byte[] address = new byte[DS28E05_ROMID_SIZE];
               // get the first 1-Wire device's address
               // keep in mind the first device is not necessarily the first 
               // device physically located on the network.
               if (adapter.GetFirstDevice(address, 0))
               {
                  do  // get subsequent 1-Wire device addresses
                  {
                     textBoxActivityLog.AppendText(Print1WireHexAddress(address) + Environment.NewLine);
                     if (address[0] == DS28E05_FAMILY_CODE)
                     {                       
                        textBoxActivityLog.AppendText(" Family Code 0D device detected." + Environment.NewLine +  " Reading..." + Environment.NewLine);
                        // Perform a read and then write data
                        adapter.PutByte(DS28E05_READ_MEMORY); // send read memory command
                        adapter.PutByte(memory_address_TA1); // LSB memory address byte 
                        adapter.PutByte(memory_address_TA2); // MSB of the address from which to read (should always be 0x00)
                        for (int i = 0; i < DS28E05_MEMORY_SIZE; i++) // read all the user memory
                        {
                           readbuffer[i] = (byte) adapter.GetByte();
                        }

                        for (int j = 0; j < DS28E05_MEMORY_PAGE_SIZE; j++)
                        {
                           pagebuffer[j] = (byte)adapter.GetByte(); // read all the Page Protection Bytes on the last 16 bytes of the device.
                        }
                        textBoxReadResults.AppendText(Print1WireHexBytes(readbuffer, 16) + Environment.NewLine);
                        textBoxReadResults.AppendText(Environment.NewLine);
                        textBoxActivityLog.AppendText(" Protection Properties Page:" + Environment.NewLine);
                        //tempstr = Print1WireHexBytes(pagebuffer, 16);
                        textBoxActivityLog.AppendText(" " + Print1WireHexBytes(pagebuffer, 16) + Environment.NewLine);
                        textBoxActivityLog.AppendText(" Reading complete." + Environment.NewLine);
                     }                     
                  }
                  while (adapter.GetNextDevice(address, 0));
 
               }
                  // end exclusive use of resource
                  adapter.EndExclusive();
            }
            catch (Exception ex)
            {
               textBoxActivityLog.AppendText(" Error: " + ex.Message + "\n" + ex.StackTrace + "\n" + ((ex.InnerException != null) ? ex.InnerException.Message : ""));
               statusBarAdapter.Text = "Not Loaded";
               adapter.EndExclusive();
            }
         }
      }

      /**
      * @brief        ShowSplashScreen() makes the splash screen visible.
      ****************************************************************************/
      private void ShowSplashScreen()
      {
         adiSplashScreen.Hide();
         adiSplashScreen.ShowDialog();
         adiSplashScreen.WindowState = FormWindowState.Normal;
      }

      /**
      * @brief        exitToolStripMenuItem_Click() Upon clicking File->Exit from the main menu, will close the application cleanly.
      * @param[in]    sender -- standard .NET event parameter.
      * @param[in]    e -- standard .NET event parameter.    
      ****************************************************************************/
      private void exitToolStripMenuItem_Click(object sender, EventArgs e)
      {
         Application.Exit();
      }

      /**
      * @brief        aboutToolStripMenuItem_Click() Upon clicking "About" from the 
      *               main menu, will display the splash screen with an OK button.
      * @param[in]    sender -- standard .NET event parameter.
      * @param[in]    e -- standard .NET event parameter.    
      ****************************************************************************/
      private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
      {
         adiSplashScreen = new ADISplashScreenForm(getProgramVersion(), false); // create splashscreen with no timer
         ShowSplashScreen();
      }

      /**
      * @brief        adiButtonSettings_Click event gets called when the user clicks on the  
      *               button labeled "Write Settings".  This will discover all 1-Wire devices and 
      *               if it is a ds28e05, then it will read the page protection settings selected by 
      *               the user, and write these values to the page protection bytes: PPA, PPB, PPC, and PPD.
      *               Note: Once the page protection bytes have been written to a value other than 1.
      * @param[in]    sender -- standard .NET event parameter.
      * @param[in]    e -- standard .NET event parameter.    
      ****************************************************************************/
      private void adiButtonSettings_Click(object sender, EventArgs e)
      {
         // settings for writing page properties (like write protection and eprom emulation).
         // create a buffer to write
         byte[] writebuffer = new byte[DS28E05_PAGE_PROTECTION_SIZE]; // This will hold the memory contents to eventually write to the device in byte form.

         // byte 0
         if (adiComboBox0.Text == "Open") writebuffer[0] = DS28E05_PAGE_SETTINGS_OPEN;
         if (adiComboBox0.Text == "Write Protect") writebuffer[0] = DS28E05_PAGE_SETTINGS_WRITE_PROTECT;
         if (adiComboBox0.Text == "Eprom Emulation") writebuffer[0] = DS28E05_PAGE_SETTINGS_EPROM_EMULATION;
         
         if (adiComboBox1.Text == "Open") writebuffer[0] = (byte)((DS28E05_PAGE_SETTINGS_OPEN << 4) + writebuffer[0]);
         if (adiComboBox1.Text == "Write Protect") writebuffer[0] = (byte)((DS28E05_PAGE_SETTINGS_WRITE_PROTECT << 4) + writebuffer[0]);
         if (adiComboBox1.Text == "Eprom Emulation") writebuffer[0] = (byte)((DS28E05_PAGE_SETTINGS_EPROM_EMULATION << 4) + writebuffer[0]);

         // byte 1
         if (adiComboBox2.Text == "Open") writebuffer[1] = DS28E05_PAGE_SETTINGS_OPEN;
         if (adiComboBox2.Text == "Write Protect") writebuffer[1] = DS28E05_PAGE_SETTINGS_WRITE_PROTECT;
         if (adiComboBox2.Text == "Eprom Emulation") writebuffer[1] = DS28E05_PAGE_SETTINGS_EPROM_EMULATION;

         if (adiComboBox3.Text == "Open") writebuffer[1] = (byte)((DS28E05_PAGE_SETTINGS_OPEN << 4) + writebuffer[1]);
         if (adiComboBox3.Text == "Write Protect") writebuffer[1] = (byte)((DS28E05_PAGE_SETTINGS_WRITE_PROTECT << 4) + writebuffer[1]);
         if (adiComboBox3.Text == "Eprom Emulation") writebuffer[1] = (byte)((DS28E05_PAGE_SETTINGS_EPROM_EMULATION << 4) + writebuffer[1]);

         // byte 2
         if (adiComboBox4.Text == "Open") writebuffer[2] = DS28E05_PAGE_SETTINGS_OPEN;
         if (adiComboBox4.Text == "Write Protect") writebuffer[2] = DS28E05_PAGE_SETTINGS_WRITE_PROTECT;
         if (adiComboBox4.Text == "Eprom Emulation") writebuffer[2] = DS28E05_PAGE_SETTINGS_EPROM_EMULATION;

         if (adiComboBox5.Text == "Open") writebuffer[2] = (byte)((DS28E05_PAGE_SETTINGS_OPEN << 4) + writebuffer[2]);
         if (adiComboBox5.Text == "Write Protect") writebuffer[2] = (byte)((DS28E05_PAGE_SETTINGS_WRITE_PROTECT << 4) + writebuffer[2]);
         if (adiComboBox5.Text == "Eprom Emulation") writebuffer[2] = (byte)((DS28E05_PAGE_SETTINGS_EPROM_EMULATION << 4) + writebuffer[2]);

         // byte 3
         if (adiComboBox6.Text == "Open") writebuffer[3] = DS28E05_PAGE_SETTINGS_OPEN;
         if (adiComboBox6.Text == "Write Protect") writebuffer[3] = DS28E05_PAGE_SETTINGS_WRITE_PROTECT;
         if (adiComboBox6.Text == "Eprom Emulation") writebuffer[3] = DS28E05_PAGE_SETTINGS_EPROM_EMULATION;

         if (adiComboBox7.Text == "Open") writebuffer[3] = (byte)((DS28E05_PAGE_SETTINGS_OPEN << 4) + writebuffer[3]);
         if (adiComboBox7.Text == "Locked") writebuffer[3] = (byte)((DS28E05_PAGE_SETTINGS_WRITE_PROTECT << 4) + writebuffer[3]);

         if (adapter != null)
         {
            try
            {
               // get exclusive use of resource
               adapter.BeginExclusive(true);
               // clear any previous search restrictions
               adapter.SetSearchAllDevices();
               adapter.TargetAllFamilies();
               // print header to text box
               textBoxActivityLog.AppendText(Environment.NewLine + "1-Wire List:" + Environment.NewLine);
               textBoxActivityLog.AppendText("=================" + Environment.NewLine);
               // get 1-Wire Addresses
               byte[] address = new byte[DS28E05_ROMID_SIZE];
               // get the first 1-Wire device's address
               // keep in mind the first device is not necessarily the first 
               // device physically located on the network.
               if (adapter.GetFirstDevice(address, 0))
               {
                  do  // get subsequent 1-Wire device addresses
                  {

                     textBoxActivityLog.AppendText(Print1WireHexAddress(address) + Environment.NewLine);
                     if (address[0] == DS28E05_FAMILY_CODE) // is this a DS28E05?
                     {
                        writePPBytes(address, writebuffer);
                        textBoxActivityLog.AppendText(" Write 4 bytes at address 0x" + DS28E05_PAGE_PROTECTION_ADDRESS.ToString("X2") + ": " +
                                                       writebuffer[0].ToString("X2") + writebuffer[1].ToString("X2") + writebuffer[2].ToString("X2") +
                                                       writebuffer[3].ToString("X2"));
                        textBoxActivityLog.AppendText(Environment.NewLine);
                        textBoxActivityLog.AppendText(" Done writing to device");
                        textBoxActivityLog.AppendText(Environment.NewLine);
                     }
                  }
                  while (adapter.GetNextDevice(address, 0));
               }
               // end exclusive use of resource
               adapter.EndExclusive();
            }
            catch (Exception ex)
            {
               textBoxActivityLog.AppendText("Error: " + ex.Message + "\n" + ex.StackTrace + "\n" + ((ex.InnerException != null) ? ex.InnerException.Message : ""));
               statusBarAdapter.Text = "Not Loaded";
               adapter.EndExclusive();
            }
         }
      }
   }
}
