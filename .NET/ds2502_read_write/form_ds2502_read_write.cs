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
* @file            form_ds2502_read_write.cs
* @brief           C# main code for the ds2502__read_write GUI that reads/writes
*                  DS2502/DS1982 devices.
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
using DalSemi.Utils;
// using LookAndFeel.dll
using LookAndFeel;


namespace ds2502_read_write
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class form_ds2502_read_write : System.Windows.Forms.Form
	{
      // DS2502 constants
      public const byte DS2502_WRITE_MEMORY = 0x0F;
      public const byte DS2502_READ_MEMORY = 0xF0;
      public const byte DS2502_FAMILY_CODE = 0x09;
      public const int DS2502_MEMORY_SIZE = 128; // number of bytes of user memmory in the DS2502
      public const int DS2502_ROMID_SIZE = 8; // 8 bytes

      public PortAdapter adapter = null;  // Declare port adapter
      public ADISplashScreenForm adiSplashScreen = null; 

      private System.Windows.Forms.TextBox textBoxReadResults;
      private LookAndFeel.ADIButton buttonRead;
		private System.Windows.Forms.StatusBar statusBarAdapter;
      private ADIButton buttonWrite;
      private Label labelRead;
      private TextBox textBoxWriteResults;
      private Label labelWrite;
      private TextBox textBoxLogResults;
      private Label labelLog;
      private MenuStrip menuStrip1;
      private ToolStripMenuItem fileToolStripMenuItem;
      private ToolStripMenuItem exitToolStripMenuItem;
      private ToolStripMenuItem aboutToolStripMenuItem;

      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

		public form_ds2502_read_write()
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
      /// <param name="disposing">destructor of this form</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing)
         {
            if (components != null)
            {
               components.Dispose();
            }
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
         var resources = new System.ComponentModel.ComponentResourceManager(typeof(form_ds2502_read_write));
         this.textBoxReadResults = new System.Windows.Forms.TextBox();
         this.buttonRead = new LookAndFeel.ADIButton();
         this.statusBarAdapter = new System.Windows.Forms.StatusBar();
         this.buttonWrite = new LookAndFeel.ADIButton();
         this.labelRead = new System.Windows.Forms.Label();
         this.textBoxWriteResults = new System.Windows.Forms.TextBox();
         this.labelWrite = new System.Windows.Forms.Label();
         this.textBoxLogResults = new System.Windows.Forms.TextBox();
         this.labelLog = new System.Windows.Forms.Label();
         this.menuStrip1 = new System.Windows.Forms.MenuStrip();
         this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.menuStrip1.SuspendLayout();
         this.SuspendLayout();
         // 
         // textBoxReadResults
         // 
         this.textBoxReadResults.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.textBoxReadResults.Location = new System.Drawing.Point(26, 62);
         this.textBoxReadResults.Multiline = true;
         this.textBoxReadResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.textBoxReadResults.Size = new System.Drawing.Size(346, 153);
         this.textBoxReadResults.TabIndex = 0;
         // 
         // buttonRead
         // 
         this.buttonRead.Font = new System.Drawing.Font("Barlow Medium", 9F);
         this.buttonRead.Location = new System.Drawing.Point(400, 108);
         this.buttonRead.Size = new System.Drawing.Size(64, 32);
         this.buttonRead.TabIndex = 1;
         this.buttonRead.Text = "&Read";
         this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
         // 
         // statusBarAdapter
         // 
         this.statusBarAdapter.Location = new System.Drawing.Point(0, 540);
         this.statusBarAdapter.Size = new System.Drawing.Size(486, 22);
         this.statusBarAdapter.TabIndex = 2;
         // 
         // buttonWrite
         // 
         this.buttonWrite.Font = new System.Drawing.Font("Barlow Medium", 9F);
         this.buttonWrite.Location = new System.Drawing.Point(400, 294);
         this.buttonWrite.Size = new System.Drawing.Size(64, 32);
         this.buttonWrite.TabIndex = 3;
         this.buttonWrite.Text = "&Write";
         this.buttonWrite.Click += new System.EventHandler(this.buttonWrite_Click);
         // 
         // labelRead
         // 
         this.labelRead.AutoSize = true;
         this.labelRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelRead.Location = new System.Drawing.Point(23, 46);
         this.labelRead.Size = new System.Drawing.Size(175, 13);
         this.labelRead.TabIndex = 4;
         this.labelRead.Text = "Read All DS2502/DS1982 Devices";
         // 
         // textBoxWriteResults
         // 
         this.textBoxWriteResults.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.textBoxWriteResults.Location = new System.Drawing.Point(26, 245);
         this.textBoxWriteResults.Multiline = true;
         this.textBoxWriteResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.textBoxWriteResults.Size = new System.Drawing.Size(346, 151);
         this.textBoxWriteResults.TabIndex = 5;
         // 
         // labelWrite
         // 
         this.labelWrite.AutoSize = true;
         this.labelWrite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelWrite.Location = new System.Drawing.Point(23, 229);
         this.labelWrite.Size = new System.Drawing.Size(161, 13);
         this.labelWrite.TabIndex = 6;
         this.labelWrite.Text = "Input Hex to Write to All Devices";
         // 
         // textBoxLogResults
         // 
         this.textBoxLogResults.Location = new System.Drawing.Point(26, 428);
         this.textBoxLogResults.Multiline = true;
         this.textBoxLogResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.textBoxLogResults.Size = new System.Drawing.Size(346, 106);
         this.textBoxLogResults.TabIndex = 7;
         // 
         // labelLog
         // 
         this.labelLog.AutoSize = true;
         this.labelLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.labelLog.Location = new System.Drawing.Point(23, 412);
         this.labelLog.Size = new System.Drawing.Size(96, 13);
         this.labelLog.TabIndex = 8;
         this.labelLog.Text = "1-Wire Activity Log";
         // 
         // menuStrip1
         // 
         this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
         this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
         this.menuStrip1.Location = new System.Drawing.Point(0, 0);
         this.menuStrip1.Size = new System.Drawing.Size(486, 24);
         this.menuStrip1.TabIndex = 9;
         // 
         // fileToolStripMenuItem
         // 
         this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
         this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
         this.fileToolStripMenuItem.Text = "&File";
         // 
         // exitToolStripMenuItem
         // 
         this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
         this.exitToolStripMenuItem.Text = "E&xit";
         this.exitToolStripMenuItem.Click += new System.EventHandler(exitToolStripMenuItem_Click);
         // 
         // aboutToolStripMenuItem
         // 
         this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
         this.aboutToolStripMenuItem.Text = "A&bout";
         this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
         // 
         // form_ds2502_read_write
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(486, 562);
         this.Controls.Add(this.labelLog);
         this.Controls.Add(this.textBoxLogResults);
         this.Controls.Add(this.labelWrite);
         this.Controls.Add(this.textBoxWriteResults);
         this.Controls.Add(this.labelRead);
         this.Controls.Add(this.buttonWrite);
         this.Controls.Add(this.statusBarAdapter);
         this.Controls.Add(this.buttonRead);
         this.Controls.Add(this.textBoxReadResults);
         this.Controls.Add(this.menuStrip1);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MainMenuStrip = this.menuStrip1;
         this.MaximizeBox = false;
         this.Text = "DS2502/DS1982 Read/Write";
         this.Load += new System.EventHandler(this.form_ds2502_read_write_Load);
         this.menuStrip1.ResumeLayout(false);
         this.menuStrip1.PerformLayout();
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
			Application.Run(new form_ds2502_read_write());
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
			var sb = new System.Text.StringBuilder(buff.Length*3);
         for (int i=7; i>-1; i--)
			{
				sb.Append(buff[i].ToString("X2"));
			}
			return sb.ToString();
		}

      /**
      * @brief        Print1WireHexBytes() takes a byte buffer representing data bytes 
      *               and converts it to a hexadecimal string and returns it.
      * @param[in]    Array of bytes representing memory data bytes.
      * @param[in]    Integer representing how many bytes per line before a new line is started. 
      * @return       A hexadecimal string representing the data.
      ****************************************************************************/
      private static string Print1WireHexBytes(byte[] buff, int ValuesPerLine)
      {
         var sb = new System.Text.StringBuilder(buff.Length * 2);
         for (int i = 0; i < buff.Length; i++)
         {
               if (i % ValuesPerLine == 0) sb.Append(Environment.NewLine);
               sb.Append(buff[i].ToString("X2"));
         }
         return sb.ToString();
      }

      /**
      * @brief        form_ds2502_read_write_Load() event gets called when the 
      *               windows form is being loaded by the operating system. This event  
      *               is where the 1-Wire adapters are auto-detected and where the splash 
      *               screen on startup gets displayed before the main window gets drawn.
      * @param[in]    sender -- standard .NET event parameter.
      * @param[in]    e -- standard .NET event parameter.
      * @return       the same string with no spaces, line endings, or line separators.
      ****************************************************************************/
      private void form_ds2502_read_write_Load(object sender, EventArgs e)
      {
         try
         {
            // getFirstAdapterFound() either gets the first adapter found or returns null.
            adapter = getFirstAdapterFound();

            statusBarAdapter.Text = adapter.ToString();
         }
         catch (Exception ex)
         {
            var exceptionString = ex.Message;
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
      * @pre          This gets called in form_ds2432_ds1972_read_write_Load() to auto-detect the adapters.
      * @return       The PortAdapter found or null.
      ****************************************************************************/
      private static PortAdapter getFirstAdapterFound()
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

         var arr = new byte[hex.Length >> 1];

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
         var val = (int)hex;
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
         var lineSeparator = ((char)0x2028).ToString();
         var paragraphSeparator = ((char)0x2029).ToString();

         return value.Replace(" ", string.Empty).Replace("\r\n", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty).Replace(lineSeparator, string.Empty).Replace(paragraphSeparator, string.Empty);
      }

      /**
      * @brief        WriteBufferToDS2502() This function performs the steps to write to a part. It first performs 
      *               a "Match ROM" followed by sending the write command and 2 address bytes and finally the data 
      *               byte to write. The last steps are to read the CRC8 of the sequence, pulse the 1-Wire line 
      *               with 12V, and then read back the data byte from the part. Upon success, the function returns 
      *               true or false otherwise.
      * @param[in]    writeadapter.  The PortAdapter object representing the 1-Wire adapter. Should not be null.
      * @param[in]    bytebuffer. The bytes to write to the device.
      * @param[in]    length. An integer giving the length of the bytebuffer array (how many bytes).
      * @param[in]    romid (a.k.a. "1-Wire address"). The 8-byte serial number of the device.
      * @param[in]    integer. Memory address to write. 
      * @return       boolean. True if successful. False if unsuccessful.
      ****************************************************************************/
      public static bool WriteBufferToDS2502(PortAdapter writeadapter, byte[] bytebuffer, int length, byte[] romid, int address)
      {
         byte TA1 = 0;
         byte TA2 = 0;
         var databyte = 0x00;
         var databytecheck = 0x00;
         uint crc = 0x00;
         uint crc_calc = 0x00;
         var index = 0;

         if (length == 0) return false;

         //set pulse duration
         writeadapter.SetProgramPulseDuration(OWPowerTime.DELIVERY_EPROM);

         TA2 = (byte)(address >> 8);
         TA1 = (byte)(address & 0xFF);

         // Perform a write first pass 
         writeadapter.SelectDevice(romid, 0); // performs a match rom followed by reset
         writeadapter.PutByte(DS2502_WRITE_MEMORY); // send write memory command
         writeadapter.PutByte(TA1); // first memory address byte T7:0
         writeadapter.PutByte(TA2); // second memory byte T15:8 of the address to which to write
         databyte = bytebuffer[index];
         index++;

         writeadapter.PutByte(databyte); // send databyte to EPROM

         crc = (uint) writeadapter.GetByte(); // get crc from EPROM

         // calculate crc
         crc_calc = CRC8.Compute((uint)DS2502_WRITE_MEMORY, 0x00); // seed here is initial seed of 0x00
         crc_calc = CRC8.Compute((uint)TA1, crc_calc); // seed here is previous crc_calc
         crc_calc = CRC8.Compute((uint)TA2, crc_calc); // seed here is previous crc_calc
         crc_calc = CRC8.Compute((uint)databyte, crc_calc); // seed here is previous crc_calc
         if (crc != crc_calc)
         {
            return false;
         }

         // start 12V pulse
         writeadapter.SetProgramPulseDuration(OWPowerTime.DELIVERY_EPROM);
         writeadapter.StartProgramPulse(OWPowerStart.CONDITION_NOW);

         // get databyte back from part
         databytecheck = writeadapter.GetByte();
         // compare databyte sent with databyte received and fail the subroutine if they do not match
         if (databyte != databytecheck)
         {
            return false; // fail
         }

         // increment address
         address = address + 1;

         if (length>1)
         {
            // do repeated program bytes
            while (address < DS2502_MEMORY_SIZE && index < length)
            {
               // Perform a write subsequent byte 
               databyte = bytebuffer[index]; // byte to send
               index++;

               // calculate crc to compare with later
               // Here, CRC8 is of the databyte to write seeded with the new address
               crc_calc = CRC8.Compute((uint)databyte, (uint)address); // CRC8 of databyte seeded to address

               // send byte to write followed by reading the crc from the DS2502.
               writeadapter.PutByte(databyte); // send byte to write on 1-Wire line
               crc = (uint)writeadapter.GetByte(); // crc to receive back from DS2502

               // compare CRCs and fail the subroutine if they do not match
               if (crc != crc_calc)
               {
                  return false; // fail
               }
                  
               // Since CRCs match, start 12V pulse
               writeadapter.SetProgramPulseDuration(OWPowerTime.DELIVERY_EPROM);
               writeadapter.StartProgramPulse(OWPowerStart.CONDITION_NOW);

               // get databyte back from part
               databytecheck = writeadapter.GetByte();
               // compare databyte sent with databyte received and fail the subroutine if they do not match
               if (databyte != databytecheck)
               {
                  return false; // fail
               }

               // increment address
               address = address + 1;
            }
         }         
         return true;
      }

      /**
      * @brief        buttonWrite_Click() event gets called when the "write" button 
      *               gets clicked. This event is where the string data found in the  
      *               hex input box gets converted to a byte array and then written 
      *               to the DS2502/DS1982 device.
      * @param[in]    sender -- standard .NET event parameter.
      * @param[in]    e -- standard .NET event parameter.
      ****************************************************************************/
      private void buttonWrite_Click(object sender, EventArgs e)
      {
         // create a buffer to write
         var readbuffer = new byte[32];
         byte[] writebuffer;
         var devicesFound = false;

         // first, get the hexadecimal string to write to part from the text box.
         var hexString = textBoxWriteResults.Text;
         hexString = hexString.Trim();
         hexString = RemoveLineEndings(hexString);
         writebuffer = HexStringToByteArray(hexString);
         var numofpages = writebuffer.Length / 32;

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
               textBoxLogResults.AppendText(Environment.NewLine + "1-Wire List:" + Environment.NewLine);
               textBoxLogResults.AppendText("=================" + Environment.NewLine);
               // get 1-Wire Addresses
               var address = new byte[DS2502_ROMID_SIZE];
               // get the first 1-Wire device's address
               // keep in mind the first device is not necessarily the first 
               // device physically located on the network.
               if (adapter.GetFirstDevice(address, 0))
               {
                  do  // get subsequent 1-Wire device addresses
                  {
                     textBoxLogResults.AppendText(Print1WireHexAddress(address) + Environment.NewLine);
                     if (address[0] == DS2502_FAMILY_CODE) // is this a DS2502 or DS1982?
                     {
                        devicesFound = true;
                        if (WriteBufferToDS2502(adapter, writebuffer, writebuffer.Length, address, 0)) // start at address 0
                        {
                           // success!
                           textBoxLogResults.AppendText("Write bytes to memory complete ");
                           textBoxLogResults.AppendText(Environment.NewLine);
                        }
                        else
                        {
                           // error!
                           textBoxLogResults.AppendText("Error: write did not complete ");
                           textBoxLogResults.AppendText(Environment.NewLine);
                        }                           
                     }
                  }
                  while (adapter.GetNextDevice(address, 0));
               }
               if (!devicesFound)
               {
                  MessageBox.Show("No DS2502/DS1982 devices found! \n" + "Connect some devices and try again.\n", "No Device Found");
               }
               // end exclusive use of resource
               adapter.EndExclusive();
            }
            catch (Exception ex)
            {
               textBoxLogResults.AppendText("Error: " + ex.Message + "\n" + ex.StackTrace + "\n" + ((ex.InnerException != null) ? ex.InnerException.Message : ""));
               statusBarAdapter.Text = "Not Loaded";
               adapter.EndExclusive();
            }
         }
      }

      /**
      * @brief        buttonRead_Click() event gets called when the user clicks on the  
      *               button labeled "read".  This will discover all 1-Wire devices and 
      *               if it is a DS2502/DS1982, then it will read out the entire user 
      *               memory contents and display the contents on the Read All DS2502/DS1982 
      *               Devices text box.
      * @param[in]    sender -- standard .NET event parameter.
      * @param[in]    e -- standard .NET event parameter.    
      ****************************************************************************/
      private void buttonRead_Click(object sender, EventArgs e)
      {
         var readbuffer = new byte[DS2502_MEMORY_SIZE];
         var crc = 0x00;
         uint crc_calc = 0x00;
         var devicesFound = false;

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
               textBoxLogResults.AppendText(Environment.NewLine + "1-Wire List:" + Environment.NewLine);
               textBoxLogResults.AppendText("=================" + Environment.NewLine);

               // get 1-Wire Addresses
               var address = new byte[DS2502_ROMID_SIZE];
               // get the first 1-Wire device's address
               // keep in mind the first device is not necessarily the first 
               // device physically located on the network.
               if (adapter.GetFirstDevice(address, 0))
               {
                  do  // get subsequent 1-Wire device addresses
                  {
                     textBoxLogResults.AppendText(Print1WireHexAddress(address) + Environment.NewLine);
                     if (address[0] == DS2502_FAMILY_CODE)
                     {
                        devicesFound = true;
                        // Perform a read and then write data
                        adapter.PutByte(DS2502_READ_MEMORY); // send read memory command
                        adapter.PutByte(0x00); // first memory address byte 
                        adapter.PutByte(0x00); // second memory byte of the address from which to read
                        crc_calc = CRC8.Compute((uint)DS2502_READ_MEMORY, (uint)0x00); // calculating CRC8 from command and address bytes
                        crc_calc = CRC8.Compute((uint)0x00, (uint)crc_calc);
                        crc_calc = CRC8.Compute((uint)0x00, (uint)crc_calc);
                        crc = adapter.GetByte(); // get 8-bit CRC of command and address from device
                        adapter.GetBlock(readbuffer, DS2502_MEMORY_SIZE); // reads entire user memory of 128 bytes from part (4 pages)
                                                                          //for (int i = 0; i < 128; i++) readbuffer[i] = (byte)adapter.GetByte();
                        textBoxLogResults.AppendText("   Calculated CRC8:  " + crc_calc.ToString("X2"));
                        textBoxLogResults.AppendText(Environment.NewLine);
                        textBoxLogResults.AppendText("   Part's CRC8: " + crc.ToString("X2"));
                        textBoxLogResults.AppendText(Environment.NewLine);
                        textBoxReadResults.AppendText(Print1WireHexBytes(readbuffer, 16));
                        textBoxReadResults.AppendText(Environment.NewLine);
                     }                     
                  }
                  while (adapter.GetNextDevice(address, 0));
               }
               if (!devicesFound)
               {
                  MessageBox.Show("No DS2502/DS1982 devices found! \n" + "Connect some devices and try again.\n", "No Device Found");
               }
               // end exclusive use of resource
               adapter.EndExclusive();
            }
            catch (Exception ex)
            {
               textBoxLogResults.AppendText("Error: " + ex.Message + "\n" + ex.StackTrace + "\n" + ((ex.InnerException != null) ? ex.InnerException.Message : ""));
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
      private static void exitToolStripMenuItem_Click(object sender, EventArgs e)
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
