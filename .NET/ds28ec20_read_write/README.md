## Description

This is a C# graphical user interface (GUI) for Windows 10 x64, specifically to read and write the [DS28EC20](https://www.maximintegrated.com/en/products/ibutton-one-wire/memory-products/DS28EC20.html) eeprom memory devices. 

## Hardware Requirements
-	A DS28EC20 chip (TO92 package is recommended). This software can read or write multiple devices on the same 1-Wire line.
-   An appropriate 1-Wire socket board and cable to hold the DS28EC20 package and connect to the PC 1-Wire adapter. For 1-Wire socket boards, see the [DS9120 family of boards](https://www.maximintegrated.com/en/products/interface/controllers-expanders/DS9120.html). They come with an appropriate interconnect cable (RJ12 male-to-male). The DS9120P+ can handle both a TO92 package and a TSOC package.
-	A 1-Wire PC adapter.  User either the [DS9490R# USB-to-1-Wire PC adapter](https://www.maximintegrated.com/en/products/ibutton-one-wire/ibutton/DS9490R.html) or the [DS9481R-3C7 USB-to-Serial 1-Wire adapter](https://www.maximintegrated.com/en/products/ibutton-one-wire/ibutton/DS9481R-3C7.html).
- Connect 1-Wire adapter of choice to the PC on a spare USB port. Connect one end of the cable to the adapter and the other end should be connected to the DS28EC20 device through a socket board. 
 
## Software Development Tools and Dependencies
-	Visual Studio 2017 capable of writing C# programs.
-	[.NET version 4.8 runtime](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48) pre-installed. If compiling, please install the developer pack.
-	It assumes that the [1-Wire Drivers](https://www.maximintegrated.com/en/products/ibutton-one-wire/one-wire/software-tools/drivers/download-1-wire-ibutton-drivers-for-windows.html) have been installed for USB DS9490R adapter support but should work stand-alone without 1-Wire Drivers for the DS9481R-3C7 PC adapter. 
-  Before installing the 1-Wire Drivers, make sure the adapter is unplugged.  For extra 1-Wire Drivers help see this [troubleshooting guide](https://maximsupport.microsoftcrmportals.com/en-us/knowledgebase/article/KA-16429).

## Downloading and Running
- From this GitHub page, click the [1-Wire_SDK_Examples](https://github.com/MaximIntegratedTechSupport/1-Wire_SDK_Examples) link and then click the Code button followed by the "Download Zip" option.  Unzip the downloaded file and find the executable and double-click.
  
## Operation or Software Flow
When run, the GUI software will do the following before the main Window appears:
1.	Automatically discover the first 1-Wire adapter it can.  This is done by first looking to see if a DS9490R 1-Wire USB adapter exists on “USB1”.  USB1 is mapped as a copy of the first Windows “handle” to the hardware device that was most recently plugged in. If it doesn’t find one there, it will attempt to find a DS9481R-3C7 serial port 1-Wire adapter on COM1 through COM64. If no adapters are found, the GUI will generate a “MessageBox” error message before the main Window appears and before the Splash Screen is visible (see step 2). Figure 1 shows the error message.

    ![Figure 1.  No Adapter Found Error.](./images/Adapter_Not_Found.png) 
 
    *Figure 1.  No Adapter Found Error.*

2.	The GUI will display a splash screen before the main window appears (using the LookAndFeel.dll).  It will appear for 3 seconds and give the name of the software, along with copyright information, version information, and how to contact Analog Devices. It also has a checkbox that a user can check to disable the splash screen as desired. Figure 2 shows the splash screen. To re-enable the splash screen, the user can click the “About” menu item from the main menu of the main GUI window as shown in Figure 3 below. This is the same splash screen but dismisses with a click of the “OK” button.

    ![Figure 2.  Splash Screen on Startup.](./images/SplashScreen.png)  	 
 
    Figure 2.  Splash Screen on Startup. 		

    ![Figure 3. About Screen with OK Button](./images/SplashScreenOK.png)  	 
 
    Figure 3. About Screen with OK Button


When the main Window appears, it will contain 3 text edit boxes vertically aligned, along with 2 buttons to go with the read and write events.  See Figure 4 below.

   ![Figure 4.  GUI Main Window](./images/GUI_Screenshot.png)   

   Figure 4.  GUI Main Window

### Reading
The top button is the “read” button.  When clicked, the program discovers all 1-Wire devices connected on the 1-Wire bus, reads the entire user memory contents of the DS28EC20 devices, and displays the entire memory contents in hexadecimal form (no spaces) in the topmost text box labeled “Read All Devices”.  See Figure 4 for the Main Window, the button labeled “Read” and the “Read All Devices” text box.  
### Writing
The “Write” button, when clicked, discovers all DS28EC20-equivalent 1-Wire devices and attempts to take the hexadecimal contents that the user places in the middle text box labeled “Input Hex to Write All Devices”, converts the text into an array of bytes and writes this array to all DS28EC20-equivalent devices found, one at a time. See Figure 4 for the Main Window, the button labeled “Write” and the “Input Hex to Write to All Devices" text box.

### Other GUI Functions 
1. The Speed group box.  This has two radio buttons that allow the user to choose which 1-Wire speed to choose:  Standard or Overdrive. Standard allows for much longer lines but "Overdrive" can speed up 1-Wire reads and writes.
2. 1-Wire Activity text box.  The text box labeled “1-Wire Activity Log” is the bottom text box.  This displays the 1-Wire activity during the read and write events.  Specifically, it shows the serial numbers of all 1-Wire devices found.  If they are DS28EC20-equivalent, then the text box displays a message that reading is occurring or that writing is occurring.
3.	The Status Bar. This is at the very bottom of the main Window and either displays “No Adapter Found” or the adapter and port, such as “{DS9490} USB1” connected to the PC as shown in Figure 4.
4.	Main Menu.  See Figure 4. This is located at the top of the main window and consists of “File” and “About” menu items.  When clicking "File", it will present a sub menu item called “Exit”.  Clicking this causes the program to exit cleanly.  Clicking About will cause the “About" screen to appear as shown in Figure 3.


## Limitations
-	Testing was done only on Windows 10 x64.
-	Only DS28EC20 devices were tested.
-	LONG TIME Reads and Writes.  Be patient, the DS28EC20 is large memory and at standard 1-Wire speed, it can take over 40 seconds to read a device's full memory. For a write, the program first reads the entire memory and then performs the write, so it can take well over a minute to complete a write.
