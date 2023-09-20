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
* @file            form_ds28EC20_read_write.cs
* @brief           C# main code for the ds28EC20_read_write GUI that reads/writes
*                  DS28EC20 devices.
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


namespace ds28ec20_read_write
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class form_ds28ec20_read_write : System.Windows.Forms.Form
	{
      // DS28EC20 constants
      public const byte DS28EC20_WRITE_SCRATCHPAD = 0x0F;
      public const byte DS28EC20_READ_SCRATCHPAD = 0xAA;
      public const byte DS28EC20_COPY_SCRATCHPAD = 0x55;
      public const byte DS28EC20_TPROG = 10; // program time for copy scratchpad in milliseconds
      public const int DS28EC20_MEMORY_SIZE = 2560; // number of bytes of user memmory in the DS28EC20
      public const int DS28EC20_SCRATCHPAD_SIZE = 32; // 32 bytes
      public const int DS28EC20_ROMID_SIZE = 8; // 8 bytes


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
      private ADIGroupBox groupBoxSpeed;
      private ADIRadioButton radioButtonStandard;
      private ADIRadioButton radioButtonOverdrive;

      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

		public form_ds28ec20_read_write()
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form_ds28ec20_read_write));
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
         this.groupBoxSpeed = new LookAndFeel.ADIGroupBox();
         this.radioButtonStandard = new LookAndFeel.ADIRadioButton();
         this.radioButtonOverdrive = new LookAndFeel.ADIRadioButton();
         this.adiButton1 = new LookAndFeel.ADIButton();
         this.menuStrip1.SuspendLayout();
         this.groupBoxSpeed.SuspendLayout();
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
         this.adiButtonRead.Location = new System.Drawing.Point(488, 188);
         this.adiButtonRead.Name = "adiButtonRead";
         this.adiButtonRead.Size = new System.Drawing.Size(127, 47);
         this.adiButtonRead.TabIndex = 1;
         this.adiButtonRead.Text = "&Read";
         this.adiButtonRead.UseVisualStyleBackColor = true;
         this.adiButtonRead.Click += new System.EventHandler(this.buttonRead_Click);
         // 
         // statusBarAdapter
         // 
         this.statusBarAdapter.Location = new System.Drawing.Point(0, 783);
         this.statusBarAdapter.Name = "statusBarAdapter";
         this.statusBarAdapter.Size = new System.Drawing.Size(636, 32);
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
         this.menuStrip1.Size = new System.Drawing.Size(636, 33);
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
         this.exitToolStripMenuItem.Size = new System.Drawing.Size(252, 30);
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
         // groupBoxSpeed
         // 
         this.groupBoxSpeed.BackgroundColor = System.Drawing.SystemColors.Control;
         this.groupBoxSpeed.Controls.Add(this.radioButtonStandard);
         this.groupBoxSpeed.Controls.Add(this.radioButtonOverdrive);
         this.groupBoxSpeed.FlatStyle = System.Windows.Forms.FlatStyle.System;
         this.groupBoxSpeed.Font = new System.Drawing.Font("Barlow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
         this.groupBoxSpeed.Location = new System.Drawing.Point(488, 57);
         this.groupBoxSpeed.Name = "groupBoxSpeed";
         this.groupBoxSpeed.Size = new System.Drawing.Size(127, 103);
         this.groupBoxSpeed.TabIndex = 10;
         this.groupBoxSpeed.TabStop = false;
         this.groupBoxSpeed.Text = "Speed";
         this.groupBoxSpeed.TitleEnable = false;
         // 
         // radioButtonStandard
         // 
         this.radioButtonStandard.AutoSize = true;
         this.radioButtonStandard.Checked = true;
         this.radioButtonStandard.Font = new System.Drawing.Font("Barlow", 9F);
         this.radioButtonStandard.Location = new System.Drawing.Point(6, 61);
         this.radioButtonStandard.Name = "radioButtonStandard";
         this.radioButtonStandard.Size = new System.Drawing.Size(102, 26);
         this.radioButtonStandard.TabIndex = 5;
         this.radioButtonStandard.TabStop = true;
         this.radioButtonStandard.Text = "Standard";
         this.radioButtonStandard.UseVisualStyleBackColor = true;
         // 
         // radioButtonOverdrive
         // 
         this.radioButtonOverdrive.AutoSize = true;
         this.radioButtonOverdrive.Font = new System.Drawing.Font("Barlow", 9F);
         this.radioButtonOverdrive.Location = new System.Drawing.Point(6, 29);
         this.radioButtonOverdrive.Name = "radioButtonOverdrive";
         this.radioButtonOverdrive.Size = new System.Drawing.Size(107, 26);
         this.radioButtonOverdrive.TabIndex = 4;
         this.radioButtonOverdrive.Text = "&Overdrive";
         this.radioButtonOverdrive.UseVisualStyleBackColor = true;
         // 
         // adiButton1
         // 
         this.adiButton1.Font = new System.Drawing.Font("Barlow", 9F);
         this.adiButton1.Location = new System.Drawing.Point(488, 428);
         this.adiButton1.Name = "adiButton1";
         this.adiButton1.Size = new System.Drawing.Size(127, 47);
         this.adiButton1.TabIndex = 3;
         this.adiButton1.Text = "&Write";
         this.adiButton1.Click += new System.EventHandler(this.buttonWrite_Click);
         // 
         // form_ds28ec20_read_write
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
         this.ClientSize = new System.Drawing.Size(636, 815);
         this.Controls.Add(this.groupBoxSpeed);
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
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MainMenuStrip = this.menuStrip1;
         this.MaximizeBox = false;
         this.Name = "form_ds28ec20_read_write";
         this.Text = "DS28EC20 Read/Write";
         this.Load += new System.EventHandler(this.form_ds28ec20_read_write_Load);
         this.menuStrip1.ResumeLayout(false);
         this.menuStrip1.PerformLayout();
         this.groupBoxSpeed.ResumeLayout(false);
         this.groupBoxSpeed.PerformLayout();
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
			Application.Run(new form_ds28ec20_read_write());
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
               if (i % ValuesPerLine == 0) sb.Append(Environment.NewLine);
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
      * @brief        form_ds28EC20_read_write_Load() event gets called when the 
      *               windows form is being loaded by the operating system. This event  
      *               is where the 1-Wire adapters are auto-detected and where the splash 
      *               screen on startup gets displayed before the main window gets drawn.
      * @param[in]    sender -- standard .NET event parameter.
      * @param[in]    e -- standard .NET event parameter.
      * @return       the same string with no spaces, line endings, or line separators.
      ****************************************************************************/
      private void form_ds28ec20_read_write_Load(object sender, EventArgs e)
      {
         try
         {
            // getFirstAdapterFound() either gets the first adapter found or returns null.
            adapter = getFirstAdapterFound();

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
      * @pre          This gets called in form_ds28ec20_read_write_Load() to auto-detect the adapters.
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
      *               to the DS28EC20-type device.
      * @param[in]    sender -- standard .NET event parameter.
      * @param[in]    e -- standard .NET event parameter.
      ****************************************************************************/
      private void buttonWrite_Click(object sender, EventArgs e)
      {
         // create a buffer to write
         byte[] writebuffer = new byte[DS28EC20_MEMORY_SIZE];

         byte[] inputbuffer;
         int numofrows = 0;
         int rowremainder = 0;

         // first, get the hexadecimal string to write to part from the text box.
         string hexString = textBoxWriteResults.Text;
         hexString = hexString.Trim();
         hexString = RemoveLineEndings(hexString);
         if (hexString.Length > (DS28EC20_MEMORY_SIZE * 2)) // Make sure string is no greater than the maximum size of the DS28EC20's memory (128 bytes = 256 characters)
         {
            hexString = hexString.Remove(DS28EC20_MEMORY_SIZE * 2);
         }
         try
         { 
            inputbuffer = HexStringToByteArray(hexString);
            numofrows = inputbuffer.Length / DS28EC20_SCRATCHPAD_SIZE;
            rowremainder = (inputbuffer.Length % DS28EC20_SCRATCHPAD_SIZE);
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
               adapter.Speed = OWSpeed.SPEED_REGULAR;
               // print header to text box
               textBoxActivityLog.AppendText(Environment.NewLine + "1-Wire List:" + Environment.NewLine);
               textBoxActivityLog.AppendText("=================" + Environment.NewLine);
               // get 1-Wire Addresses
               byte[] address = new byte[DS28EC20_ROMID_SIZE];
               // get the first 1-Wire device's address
               // keep in mind the first device is not necessarily the first 
               // device physically located on the network.
               if (adapter.GetFirstDevice(address, 0))
               {
                  do  // get subsequent 1-Wire device addresses
                  {

                     textBoxActivityLog.AppendText(Print1WireHexAddress(address) + Environment.NewLine);
                     if (address[0] == 0x43) // is this a DS28EC20
                     {
                        byte TA1 = 0; // T7:T0  LSB
                        byte TA2 = 0; // T15:T8 MSB
                        byte[] rowdata = new byte[DS28EC20_SCRATCHPAD_SIZE];
                        int numofpages = DS28EC20_MEMORY_SIZE / DS28EC20_SCRATCHPAD_SIZE;
                        int sourceOffset = 0; 

                        writebuffer = readDS28EC20(adapter, address); // first read the device and then copy text box contents over it

                        Buffer.BlockCopy(inputbuffer, 0, writebuffer, 0, inputbuffer.Length);
                        adapter.Reset();
                        adapter.SelectDevice(address, 0);

                        // write to each row 8 bytes at a time.
                        for (int i = 0; i < numofpages; i++)
                        {
                           sourceOffset = ((int)TA2 << 8) + (int)TA1;
                           //Buffer.BlockCopy(writebuffer, TA1, rowdata, 0, DS28EC20_SCRATCHPAD_SIZE);
                           Buffer.BlockCopy(writebuffer, sourceOffset, rowdata, 0, DS28EC20_SCRATCHPAD_SIZE);
                           write32Bytes(address, rowdata, TA1, TA2);
                           textBoxActivityLog.AppendText("Write 32 bytes at address: 0x" + TA2.ToString("X2") + TA1.ToString("X2")); 
                           textBoxActivityLog.AppendText(Environment.NewLine);
                           if (TA1 == 0xE0)
                           {
                              TA2 = (byte) (TA2 + 1);
                           }
                           TA1 = (byte) (TA1 + DS28EC20_SCRATCHPAD_SIZE);
                        }
                        textBoxActivityLog.AppendText("Done writing to device");
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
      * @brief        write32Bytes() This is the method that actually writes to the DS28EC20-type device. The part 
      *               can only be written to 32 bytes at a time due to the size of the scratchpad. It gets called 
      *               by the buttonWrite_Click() event. It writes 32 bytes to the scratchpad, then performs a read 
      *               scratchpad just long enough to get the E/S byte, and then copies the scratchpad to memory 
      *               with strong pullup supplied for 10ms.
      * @param[in]    address.  The 8-byte serial number.
      * @param[in]    writebuffer.  The 32 bytes of data to write to the part.
      * @param[in]    TA1.  The byte representing the most significant byte of the memory address to write.
      * @param[in]    TA2.  The byte representing the least significant byte of the memory address to write.
      * @return       The PortAdapter found or null.
      ****************************************************************************/
      private bool write32Bytes(byte[] address, byte[] writebuffer, byte TA1, byte TA2)
      {
         byte[] readbuffer = new byte[DS28EC20_SCRATCHPAD_SIZE];
         byte CRC1 = 0;
         byte CRC2 = 0;
         byte ES = 0;
         int copyresult = 0;
         int flag = 0;
         bool returnValue = false;
         int memoryAddress = 0;

         adapter.SelectDevice(address, 0); // reset + match rom
         // Perform a write scratchpad (assumes part was already discovered and selected previously)
         adapter.PutByte(DS28EC20_WRITE_SCRATCHPAD); // send write scratchpad memory command
         adapter.PutByte(TA1); // first memory address byte T7:0
         adapter.PutByte(TA2); // second memory byte T15:8 of the address to which to write
         adapter.DataBlock(writebuffer, 0, DS28EC20_SCRATCHPAD_SIZE); // same as "PutByte" calls -- size of scratchpad
         CRC1 = (byte)adapter.GetByte();
         CRC2 = (byte)adapter.GetByte();

         // Perform read scratchpad to validate the data

         //adapter.SelectDevice(address, 0); // reset + match rom
         adapter.Reset();
         adapter.PutByte(0xA5); // resume
         adapter.PutByte(DS28EC20_READ_SCRATCHPAD); // send read scratchpad memory command
         TA1 = (byte)adapter.GetByte(); // first memory address byte T7:0
         TA2 = (byte)adapter.GetByte(); // second memory byte T15:8 of the address to which to write
         ES = (byte)adapter.GetByte();
         adapter.GetBlock(readbuffer, 0, DS28EC20_SCRATCHPAD_SIZE); // gets 8 bytes of readbuffer (same as 8 "GetByte" calls)
         CRC1 = (byte)adapter.GetByte();
         CRC2 = (byte)adapter.GetByte();
         // todo:  validate CRC data.  Not done here, but is recommended (especially in noisy environments)

         // Perform copy scratchpad to copy to memory
         adapter.SelectDevice(address, 0); // reset + match rom
         adapter.PutByte(DS28EC20_COPY_SCRATCHPAD); // send copy scratchpad command
         adapter.PutByte(TA1);
         adapter.PutByte(TA2);
         adapter.SetPowerDuration(OWPowerTime.DELIVERY_INFINITE);
         adapter.StartPowerDelivery(OWPowerStart.CONDITION_AFTER_BYTE); // turn power delivery on after sending next byte;
         adapter.PutByte(ES); // send E/S byte
         System.Threading.Thread.Sleep(DS28EC20_TPROG); // sleep however long it takes with power delivery on 
                           // to copy 32 bytes from scratchpad to memory (10 milliseconds).  
                           // See note in DS28EC20's datasheet.
         adapter.SetPowerNormal();

         for (int i = 0; i < 10; i++)
         {
            flag = adapter.GetByte();
            copyresult = flag & 0x03; // mask out last 2 bits looking for 01 or 10
            if ((copyresult > 0) && (copyresult < 3)) break; // alternating zero and one pattern detected
         }

         if ((copyresult > 0) && (copyresult < 3))
         {
            // success!
            memoryAddress = (int)(TA2 << 8) + (int)TA1;
            returnValue = true;
         }
         else
         {
            // error: copy did not occur
            textBoxActivityLog.AppendText("Error: Scratchpad copy might not have occurred ");
            textBoxActivityLog.AppendText(Environment.NewLine);
            returnValue = false;
         }
         return returnValue;
      }

      /**
      * @brief        readDS28EC20() This reads the entire user memory contents of a DS28EC20 (128 bytes). 
      * @param[in]    adapter.  This is the PortAdapter (1-Wire adapter) object.
      * @param[in]    address.  This is the 8-byte address of the DS28EC20 to be read.
      * @pre          This requires that a 1-Wire reset and a match rom (select) has been successfully sent. 
      * @return       byte array.  128 bytes of DS28EC20 user memory.
      ****************************************************************************/
      private byte[] readDS28EC20(PortAdapter adapter, byte[] address)
      {
         byte[] readbuffer = new byte[DS28EC20_MEMORY_SIZE];

         if (adapter != null)
         {
            try
            {
               // Perform a read and then write data
               adapter.PutByte(0xF0); // send read memory command
               adapter.PutByte(0x00); // first memory address byte 
               adapter.PutByte(0x00); // second memory byte of the address from which to read
               for (int i = 0; i < DS28EC20_MEMORY_SIZE; i++)
               {
                  readbuffer[i] = (byte)adapter.GetByte();
               }

               //adapter.GetBlock(readbuffer, DS28EC20_MEMORY_SIZE); // reads 8 bytes from part (4 pages)
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
      *               if it is a DS28EC20-equivalent, then it will read out the entire user 
      *               memory contents and display the contents on the Read All Devices text box.
      * @param[in]    sender -- standard .NET event parameter.
      * @param[in]    e -- standard .NET event parameter.    
      ****************************************************************************/
      private void buttonRead_Click(object sender, EventArgs e)
      {
         byte[] readbuffer = new byte[DS28EC20_MEMORY_SIZE];

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
               // print header to text box
               textBoxActivityLog.AppendText(Environment.NewLine + "1-Wire List:" + Environment.NewLine);
               textBoxActivityLog.AppendText("=================" + Environment.NewLine);
               // get 1-Wire Addresses
               byte[] address = new byte[DS28EC20_ROMID_SIZE];
               // get the first 1-Wire device's address
               // keep in mind the first device is not necessarily the first 
               // device physically located on the network.
               if (adapter.GetFirstDevice(address, 0))
               {
                  do  // get subsequent 1-Wire device addresses
                  {
                     textBoxActivityLog.AppendText(Print1WireHexAddress(address) + Environment.NewLine);
                     if (address[0] == 0x43)
                     {                       
                        textBoxActivityLog.AppendText("Family Code 43 device detected.  Reading..." + Environment.NewLine);
                        // Perform a read and then write data
                        adapter.PutByte(0xF0); // send read memory command
                        adapter.PutByte(0x00); // first memory address byte 
                        adapter.PutByte(0x00); // second memory byte of the address from which to read
                        for (int i = 0; i<DS28EC20_MEMORY_SIZE; i++)
                        {
                           readbuffer[i] = (byte) adapter.GetByte();
                        }
                        
                        textBoxReadResults.AppendText(Print1WireHexBytes(readbuffer, 16));
                        textBoxReadResults.AppendText(Environment.NewLine);
                        textBoxActivityLog.AppendText("Reading complete." + Environment.NewLine);
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
   }
}
