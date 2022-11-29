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
*******************************************************************************
*/

/**
* @file            form_ds2413_read_write.cs
* @brief           C# main code for the ds2413_read_write GUI that reads/writes
*                  the DS2413 1-Wire switch devices.
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
// using the OneWireLinkLayer.dll
using DalSemi.OneWire.Adapter;
using DalSemi.OneWire;
// using the LookAndFeel.dll -- contains GUI components that adheres to the Analog Devices "Look And Feel" standard for GUI programs.
using LookAndFeel;

namespace ds2413_read_write
{
   /// <summary>
   /// C# main code for the ds2413_read_write GUI that reads/writes to the DS2413 1-Wire switch devices.
	/// </summary>
	public class ds2413_read_write : System.Windows.Forms.Form
	{
      // DS2413 constants
      public const byte DS2413_FAMILY_CODE = 0x3A; // the family code is the byte of the serial number (ROM ID) of the device that identifies it as a DS2413
      public const byte DS2413_PIO_ACCESS_READ = 0xF5; // the 1-Wire command that starts the read process of the PIO latches and pin states from a selected device 
      public const byte DS2413_PIO_ACCESS_WRITE = 0x5A; // the 1-Wire command that starts the write process to the PIO latches and pin states of a selected device
      public const int DS2413_ROMID_SIZE = 8; // 8 bytes

      public PortAdapter adapter = null; // Declare port adapter
      //The following section intiates timer for splash screen 
      Timer timer = new Timer();
      public ADISplashScreenForm adiSplashScreen = null;

      private System.Windows.Forms.StatusBar statusBarAdapter;
      private ADIRadioButton radioButtonPIOAPinState;
      private Panel panel1;
      private ADIRadioButton radioButtonPIOBPinState;
      private GroupBox groupBox1;
      private ListView listViewDS2413Devices;
      private ADIToggleButton togglePIOALatchState;
      private ADIToggleButton togglePIOBLatchState;
      private ADIButton buttonSearch;
      private ADIButton buttonPIORead;
      private Label label1;
      private ColumnHeader columnHeader1;
      private Label labelSerNum;
      private Label labelSwitchLabel;
      private MenuStrip menuMain;
      private ToolStripMenuItem fileToolStripMenuItem;
      private ToolStripMenuItem exitToolStripMenuItem;
      private ToolStripMenuItem aboutToolStripMenuItem;
      private GroupBox groupBox2;

      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

		public ds2413_read_write()
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ds2413_read_write));
         this.statusBarAdapter = new System.Windows.Forms.StatusBar();
         this.radioButtonPIOAPinState = new LookAndFeel.ADIRadioButton();
         this.panel1 = new System.Windows.Forms.Panel();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.radioButtonPIOBPinState = new LookAndFeel.ADIRadioButton();
         this.togglePIOBLatchState = new LookAndFeel.ADIToggleButton();
         this.togglePIOALatchState = new LookAndFeel.ADIToggleButton();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.labelSerNum = new System.Windows.Forms.Label();
         this.labelSwitchLabel = new System.Windows.Forms.Label();
         this.buttonPIORead = new LookAndFeel.ADIButton();
         this.listViewDS2413Devices = new System.Windows.Forms.ListView();
         this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
         this.buttonSearch = new LookAndFeel.ADIButton();
         this.label1 = new System.Windows.Forms.Label();
         this.menuMain = new System.Windows.Forms.MenuStrip();
         this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.panel1.SuspendLayout();
         this.groupBox2.SuspendLayout();
         this.groupBox1.SuspendLayout();
         this.menuMain.SuspendLayout();
         this.SuspendLayout();
         // 
         // statusBarAdapter
         // 
         this.statusBarAdapter.Location = new System.Drawing.Point(0, 260);
         this.statusBarAdapter.Name = "statusBarAdapter";
         this.statusBarAdapter.Size = new System.Drawing.Size(584, 22);
         this.statusBarAdapter.TabIndex = 2;
         // 
         // radioButtonPIOAPinState
         // 
         this.radioButtonPIOAPinState.AutoCheck = false;
         this.radioButtonPIOAPinState.AutoSize = true;
         this.radioButtonPIOAPinState.Font = new System.Drawing.Font("Barlow", 9F);
         this.radioButtonPIOAPinState.Location = new System.Drawing.Point(6, 15);
         this.radioButtonPIOAPinState.Name = "radioButtonPIOAPinState";
         this.radioButtonPIOAPinState.Size = new System.Drawing.Size(98, 18);
         this.radioButtonPIOAPinState.TabIndex = 6;
         this.radioButtonPIOAPinState.Text = "PIOA Pin State";
         this.radioButtonPIOAPinState.UseVisualStyleBackColor = true;
         // 
         // panel1
         // 
         this.panel1.Controls.Add(this.groupBox2);
         this.panel1.Controls.Add(this.togglePIOBLatchState);
         this.panel1.Controls.Add(this.togglePIOALatchState);
         this.panel1.Location = new System.Drawing.Point(11, 21);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(247, 92);
         this.panel1.TabIndex = 7;
         // 
         // groupBox2
         // 
         this.groupBox2.Controls.Add(this.radioButtonPIOAPinState);
         this.groupBox2.Controls.Add(this.radioButtonPIOBPinState);
         this.groupBox2.Location = new System.Drawing.Point(132, 8);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(112, 80);
         this.groupBox2.TabIndex = 13;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "Read Only";
         // 
         // radioButtonPIOBPinState
         // 
         this.radioButtonPIOBPinState.AutoCheck = false;
         this.radioButtonPIOBPinState.AutoSize = true;
         this.radioButtonPIOBPinState.Font = new System.Drawing.Font("Barlow", 9F);
         this.radioButtonPIOBPinState.Location = new System.Drawing.Point(6, 50);
         this.radioButtonPIOBPinState.Name = "radioButtonPIOBPinState";
         this.radioButtonPIOBPinState.RightToLeft = System.Windows.Forms.RightToLeft.No;
         this.radioButtonPIOBPinState.Size = new System.Drawing.Size(99, 18);
         this.radioButtonPIOBPinState.TabIndex = 8;
         this.radioButtonPIOBPinState.Text = "PIOB Pin State";
         this.radioButtonPIOBPinState.UseVisualStyleBackColor = true;
         // 
         // togglePIOBLatchState
         // 
         this.togglePIOBLatchState.Font = new System.Drawing.Font("Barlow", 9F);
         this.togglePIOBLatchState.Location = new System.Drawing.Point(8, 57);
         this.togglePIOBLatchState.Name = "togglePIOBLatchState";
         this.togglePIOBLatchState.Size = new System.Drawing.Size(129, 23);
         this.togglePIOBLatchState.TabIndex = 14;
         this.togglePIOBLatchState.Text = "PIO&B Latch State";
         this.togglePIOBLatchState.UseVisualStyleBackColor = true;
         this.togglePIOBLatchState.CheckedChanged += new System.EventHandler(this.togglePIOLatchState_CheckedChanged);
         // 
         // togglePIOALatchState
         // 
         this.togglePIOALatchState.Font = new System.Drawing.Font("Barlow", 9F);
         this.togglePIOALatchState.Location = new System.Drawing.Point(8, 22);
         this.togglePIOALatchState.Name = "togglePIOALatchState";
         this.togglePIOALatchState.Size = new System.Drawing.Size(129, 23);
         this.togglePIOALatchState.TabIndex = 13;
         this.togglePIOALatchState.Text = "PIO&A Latch State";
         this.togglePIOALatchState.UseVisualStyleBackColor = true;
         this.togglePIOALatchState.CheckedChanged += new System.EventHandler(this.togglePIOLatchState_CheckedChanged);
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.labelSerNum);
         this.groupBox1.Controls.Add(this.labelSwitchLabel);
         this.groupBox1.Controls.Add(this.buttonPIORead);
         this.groupBox1.Controls.Add(this.panel1);
         this.groupBox1.Font = new System.Drawing.Font("Barlow Medium", 9F);
         this.groupBox1.Location = new System.Drawing.Point(293, 42);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(269, 205);
         this.groupBox1.TabIndex = 8;
         this.groupBox1.TabStop = false;
         this.groupBox1.UseCompatibleTextRendering = true;
         // 
         // labelSerNum
         // 
         this.labelSerNum.AutoSize = true;
         this.labelSerNum.BackColor = System.Drawing.Color.White;
         this.labelSerNum.Font = new System.Drawing.Font("Barlow", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelSerNum.Location = new System.Drawing.Point(137, 125);
         this.labelSerNum.Name = "labelSerNum";
         this.labelSerNum.Size = new System.Drawing.Size(104, 16);
         this.labelSerNum.TabIndex = 10;
         this.labelSerNum.Text = "No Device Selected";
         // 
         // labelSwitchLabel
         // 
         this.labelSwitchLabel.AutoSize = true;
         this.labelSwitchLabel.Font = new System.Drawing.Font("Barlow", 9F);
         this.labelSwitchLabel.Location = new System.Drawing.Point(16, 125);
         this.labelSwitchLabel.Name = "labelSwitchLabel";
         this.labelSwitchLabel.Size = new System.Drawing.Size(123, 16);
         this.labelSwitchLabel.TabIndex = 9;
         this.labelSwitchLabel.Text = "Switch Serial Number: ";
         // 
         // buttonPIORead
         // 
         this.buttonPIORead.Font = new System.Drawing.Font("Barlow", 9F);
         this.buttonPIORead.Location = new System.Drawing.Point(95, 156);
         this.buttonPIORead.Name = "buttonPIORead";
         this.buttonPIORead.Size = new System.Drawing.Size(80, 32);
         this.buttonPIORead.TabIndex = 8;
         this.buttonPIORead.Text = "PIO &Read";
         this.buttonPIORead.UseVisualStyleBackColor = true;
         this.buttonPIORead.Click += new System.EventHandler(this.buttonPIORead_Click);
         // 
         // listViewDS2413Devices
         // 
         this.listViewDS2413Devices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.listViewDS2413Devices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
         this.listViewDS2413Devices.HideSelection = false;
         this.listViewDS2413Devices.Location = new System.Drawing.Point(128, 48);
         this.listViewDS2413Devices.MultiSelect = false;
         this.listViewDS2413Devices.Name = "listViewDS2413Devices";
         this.listViewDS2413Devices.Size = new System.Drawing.Size(142, 198);
         this.listViewDS2413Devices.TabIndex = 9;
         this.listViewDS2413Devices.UseCompatibleStateImageBehavior = false;
         this.listViewDS2413Devices.View = System.Windows.Forms.View.List;
         this.listViewDS2413Devices.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listViewDS2413Devices_MouseClick);
         // 
         // columnHeader1
         // 
         this.columnHeader1.Text = "1-Wire Device List";
         this.columnHeader1.Width = 512;
         // 
         // buttonSearch
         // 
         this.buttonSearch.Font = new System.Drawing.Font("Barlow", 9F);
         this.buttonSearch.Location = new System.Drawing.Point(25, 113);
         this.buttonSearch.Name = "buttonSearch";
         this.buttonSearch.Size = new System.Drawing.Size(80, 23);
         this.buttonSearch.TabIndex = 10;
         this.buttonSearch.Text = "&Search";
         this.buttonSearch.UseVisualStyleBackColor = true;
         this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(128, 31);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(140, 13);
         this.label1.TabIndex = 11;
         this.label1.Text = "Device List -- Click to Select";
         // 
         // menuMain
         // 
         this.menuMain.ImageScalingSize = new System.Drawing.Size(24, 24);
         this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
         this.menuMain.Location = new System.Drawing.Point(0, 0);
         this.menuMain.Name = "menuMain";
         this.menuMain.Size = new System.Drawing.Size(584, 24);
         this.menuMain.TabIndex = 12;
         this.menuMain.Text = "menuMain";
         // 
         // fileToolStripMenuItem
         // 
         this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
         this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
         this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
         this.fileToolStripMenuItem.Text = "&File";
         // 
         // exitToolStripMenuItem
         // 
         this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
         this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
         this.exitToolStripMenuItem.Text = "E&xit";
         this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
         // 
         // aboutToolStripMenuItem
         // 
         this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
         this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
         this.aboutToolStripMenuItem.Text = "Abou&t";
         this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
         // 
         // ds2413_read_write
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(584, 282);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.buttonSearch);
         this.Controls.Add(this.listViewDS2413Devices);
         this.Controls.Add(this.groupBox1);
         this.Controls.Add(this.statusBarAdapter);
         this.Controls.Add(this.menuMain);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MainMenuStrip = this.menuMain;
         this.MaximizeBox = false;
         this.Name = "ds2413_read_write";
         this.Text = "DS2413 Read / Write";
         this.Load += new System.EventHandler(this.form_ds2413_read_write_Load);
         this.panel1.ResumeLayout(false);
         this.groupBox2.ResumeLayout(false);
         this.groupBox2.PerformLayout();
         this.groupBox1.ResumeLayout(false);
         this.groupBox1.PerformLayout();
         this.menuMain.ResumeLayout(false);
         this.menuMain.PerformLayout();
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
			Application.Run(new ds2413_read_write());
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
         System.Text.StringBuilder sb = new System.Text.StringBuilder(buff.Length * 3);
         for (int i = 7; i > -1; i--)
         {
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
      * @brief        form_ds2413_read_write_Load() event gets called when the 
      *               windows form is being loaded by the operating system. This event  
      *               is where the 1-Wire adapters are auto-detected and where the splash 
      *               screen on startup gets displayed before the main window gets drawn.
      * @param[in]    sender -- standard .NET event parameter.
      * @param[in]    e -- standard .NET event parameter.
      ****************************************************************************/
      private void form_ds2413_read_write_Load(object sender, EventArgs e)
      {
         groupBox1.Enabled = false; // disable the switch read-out section of GUI
         try
         {
            // This will find and return the first 1-Wire adapter.  First it looks 
            // on the USB port for a DS9490R. Then it looks at the serial port for 
            // any DS9481R-3C7 adapters.
            // getFirstAdapterFound() either gets the first adapter found or returns null.
            adapter = getFirstAdapterFound();

            statusBarAdapter.Text = adapter.ToString();
         }
         catch (Exception ex)
         {
            MessageBox.Show("1-Wire PC adapter not found or loaded. Close this \n" +
                            "program and run the 'Default 1-Wire Net.exe' program \n" +
                            "to find and set the adapter to use for this program. ", "1-Wire Adapter Not Found");
            statusBarAdapter.Text = "Not Loaded";
            adapter = null;
            ex.Data.Clear();
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
      * @brief        getFirstAdapterFound() performs an auto-detect sequence, returning the first 1-Wire adapter found.
      * @pre          This gets called in form_ds2413_read_write_Load() to auto-detect the adapters.
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
      * @brief        parseReadByte takes the byte read from the DS2413 and parses the bits out into 
      *               meaningful variables (booleans).  Specifically, it figures out the two switch latch 
      *               states and the two pin states.  It also updates the appropriate GUI components.
      * @return       nothing
      ****************************************************************************/
      private void parseReadByte(int readByte)
      {
         bool pioa_latch_state;
         bool pioa_pin_state;
         bool piob_latch_state;
         bool piob_pin_state;

         // parse the pio_byte
         pioa_pin_state = Convert.ToBoolean(readByte & (int)0x01);
         pioa_latch_state = Convert.ToBoolean((readByte & (int)0x02) >> 1);
         piob_pin_state = Convert.ToBoolean((readByte & (int)0x04) >> 2);
         piob_latch_state = Convert.ToBoolean((readByte & (int)0x08) >> 3);

         // put pin and latch states on the radio buttons and toggle buttons
         radioButtonPIOAPinState.Checked = pioa_pin_state;
         togglePIOALatchState.Checked = pioa_latch_state;
         radioButtonPIOBPinState.Checked = piob_pin_state;
         togglePIOBLatchState.Checked = piob_latch_state;
      }

      /**
      * @brief        DS2413PIOWrite() writes 8 status bits that encompass the PIOA and PIOB 
      *               latch states and pin states and then reads the updated status of the bits. 
      *               Bits b7 - b4 are the complement of b3 to b0 which are: PIOB Latch state, 
      *               PIOB Pin State, PIOA Latch State, and PIOA Pin State respectively. 
      * @param[in]    pa is the PortAdapter.
      * @param[in]    switch_address is the byte array of the string of the ROM number in 
      *               reverse byte order.
      * @param[in]    new_pio_byte is the byte to be written.
      * @return       the byte result of the read (after the write)
      ****************************************************************************/
      private byte DS2413PIOWrite(PortAdapter pa, byte[] switch_address, byte new_pio_byte)
      {
         int confirmation_byte = 0;
         int re_read_pio_byte = 0;

         if (pa != null)
         {
            try
            {
               // get exclusive use of resource
               pa.BeginExclusive(true);
               pa.Reset();
               if (pa.SelectDevice(switch_address, 0)) // from 1-Wire data sheets, this is the same as a "Match ROM"
               {

                  if (switch_address[0] == DS2413_FAMILY_CODE) // is this a DS2413?
                  {
                     adapter.PutByte(DS2413_PIO_ACCESS_WRITE); // send PIO Access Write command
                     adapter.PutByte(new_pio_byte); // send data byte of new PIO latch states
                     adapter.PutByte((~new_pio_byte) & 0xFF); // send complement of data byte
                     confirmation_byte = adapter.GetByte(); // read confirmation byte
                     re_read_pio_byte = adapter.GetByte(); // read pio byte again
                  }
               }
               // end exclusive use of resource
               pa.EndExclusive();
            }
            catch (Exception ex)
            {
               MessageBox.Show("Could not write to the DS2413", "Error");
               statusBarAdapter.Text = "Not Loaded";
               pa.EndExclusive();
               ex.Data.Clear();
               return (0xFF); // 0xFF would be an error as it should never be the result of a read PIO.
            }
         }
         return ((byte)re_read_pio_byte);
      }

      /**
      * @brief        DS2413PIORead() reads 8 status bits that encompass the PIOA and PIOB 
      *               latch states and pin states. Bits b7 - b4 are the complement of b3 to b0  
      *               which are: PIOB Latch state, PIOB Pin State, PIOA Latch State, and PIOA 
      *               Pin State respectively. 
      * @param[in]    pa is the PortAdapter.
      * @param[in]    switch_address is the byte array of the string of the ROM number in 
      *               reverse byte order.
      * @return       the byte result of the read
      ****************************************************************************/
      private byte DS2413PIORead(PortAdapter pa, byte[] switch_address)
      {
         byte pio_byte = 0;

         if (pa != null)
         {
            try
            {
               // get exclusive use of resource
               pa.BeginExclusive(true);
               pa.Reset();
               if (pa.SelectDevice(switch_address, 0)) // from 1-Wire data sheets, this is the same as a "Match ROM"
               {
                  if (switch_address[0] == DS2413_FAMILY_CODE) // DS2413
                  {
                     // Perform a PIO Read
                     pa.PutByte(DS2413_PIO_ACCESS_READ); // send PIO access read command
                     pio_byte = (byte)pa.GetByte(); // get PIO info
                  }
               }
               // end exclusive use of resource
               pa.EndExclusive();
            }
            catch (Exception ex)
            {
               MessageBox.Show("Could not read from the DS2413", "Error");
               statusBarAdapter.Text = "Not Loaded";
               pa.EndExclusive();
               ex.Data.Clear();
               return (0xFF); // 0xFF would be an error as it should never be the result of a read PIO.
            }
         }
         return (pio_byte);
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
      * @brief        buttonPIORead_Click() is the event that occurs when the 
      *               buttonPIORead button is clicked. It performs the following:
      *               takes the ds2413 serial number string from the label and 
      *               converts it to a byte array, then reverses the byte array,
      *               then it performs a DS2413pIORead to read the latch and pin 
      *               and pin states, and then finally parses the return byte and 
      *               updates the GUI showing the new pin status (if any).
      * @param[in]    sender -- standard .NET event parameter.
      * @param[in]    e -- standard .NET event parameter.
      ****************************************************************************/
      private void buttonPIORead_Click(object sender, EventArgs e)
      {
         byte PIOReadByte = 0;
         byte[] ds2413_address = new byte[DS2413_ROMID_SIZE]; // switch "address"

         ds2413_address = HexStringToByteArray(labelSerNum.Text);
         Array.Reverse(ds2413_address, 0, ds2413_address.Length); // The address in the serial number label 
                                                                  // is the ROMID of the device byte-reversed
         PIOReadByte = DS2413PIORead(adapter, ds2413_address);
         parseReadByte(PIOReadByte); // display PIO info
      }

      /**
      * @brief        buttonSearch_Click() is the event that occurs when the 
      *               buttonSearch button is clicked. It performs the following:
      *               searches the 1-Wire bus for all 1-Wire devices and places 
      *               the serial number of the devices on the Device List of the GUI, 
      *               which is the GUI ListView component.
      * @param[in]    sender -- standard .NET event parameter.
      * @param[in]    e -- standard .NET event parameter.
      ****************************************************************************/
      private void buttonSearch_Click(object sender, EventArgs e)
      {
         // first, clear the DS2413 device list
         listViewDS2413Devices.Clear();

         if (adapter != null)
         {
            try
            {
               // get exclusive use of resource
               adapter.BeginExclusive(true);
               // clear any previous search restrictions
               adapter.SetSearchAllDevices();
               adapter.TargetAllFamilies();
               adapter.Speed = OWSpeed.SPEED_REGULAR;
               // get 1-Wire Addresses
               byte[] address = new byte[DS2413_ROMID_SIZE];
               // get the first 1-Wire device's address
               // keep in mind the first device is not necessarily the first 
               // device physically located on the network.
               if (adapter.GetFirstDevice(address, 0))
               {
                  do  // get subsequent 1-Wire device addresses
                  {
                     if (address[0] == DS2413_FAMILY_CODE) // DS2413?
                     {
                        listViewDS2413Devices.Items.Add(Print1WireHexAddress(address));
                     }
                  }
                  while (adapter.GetNextDevice(address, 0));
               }
               // end exclusive use of resource
               adapter.EndExclusive();
            }
            catch (Exception ex)
            {
               //textBoxResults.AppendText("Error: " + ex.Message + "\n" + ex.StackTrace + "\n" + ((ex.InnerException != null) ? ex.InnerException.Message : ""));
               statusBarAdapter.Text = "Not Loaded";
               adapter.EndExclusive();
               ex.Data.Clear();
            }
         }
      }

      /**
      * @brief        listViewDS2413Devices_MouseClick() is the event that occurs when the 
      *               ListView GUI component (a.k.a. "Device List") is clicked. The ListView is  
      *               not multi-select, so it will only allow the user to select (by clicking) 
      *               only a single entry, which, in this case is a DS2413 device. It performs 
      *               the following 3 things: enables the the group box of PIO toggle buttons 
      *               and radio buttons, copies the selected serial number into the labelSerNum 
      *               text field, and then reads the state of the DS2413 switches and updates 
      *               the GUI.
      * @param[in]    sender -- standard .NET event parameter.
      * @param[in]    e -- standard .NET event parameter.
      ****************************************************************************/
      private void listViewDS2413Devices_MouseClick(object sender, MouseEventArgs e)
      {
         groupBox1.Enabled = true; // enable the PIO buttons and radio check boxes.
         labelSerNum.Text = listViewDS2413Devices.SelectedItems[0].Text; // copy serial number from device list to the label.
         buttonPIORead_Click(sender, e); // read the PIO and update the GUI.
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
      * @brief        togglePIOLatchState_CheckedChanged() is the event that occurs after one 
      *               of the PIOLatchState toggle buttons is clicked (either PIOA or PIOB). It performs 
      *               the following 3 things: 1) copies the serial number of the currently-selected 
      *               device from labelSerNum, converts it into a byte array, and reverses the byte
      *               array, 2) converts the GUI swtich data into a byte and writes this to the switch,
      *               and 3) reads the resulting switch states and updates the GUI with what has changed.
      * @param[in]    sender -- standard .NET event parameter.
      * @param[in]    e -- standard .NET event parameter.
      ****************************************************************************/
      private void togglePIOLatchState_CheckedChanged(object sender, EventArgs e)
      {
         byte PIOResultByte = 0;
         byte PIO_Write_Byte = 0;
         byte[] ds2413_address = new byte[DS2413_ROMID_SIZE]; // switch "address"

         ds2413_address = HexStringToByteArray(labelSerNum.Text);
         Array.Reverse(ds2413_address, 0, ds2413_address.Length); // The address in the serial number label 
                                                                  // is the ROMID of the device byte-reversed
         PIO_Write_Byte = (byte)(0xFC + ((Convert.ToInt32(togglePIOBLatchState.Checked)) << 1) + Convert.ToInt32(togglePIOALatchState.Checked));

         PIOResultByte = DS2413PIOWrite(adapter, ds2413_address, PIO_Write_Byte);
         parseReadByte(PIOResultByte); // display PIO info
      }
   }
}

