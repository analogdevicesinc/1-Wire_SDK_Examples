Attribute VB_Name = "TMEXLIB"
'/*******************************************************************************
'* Copyright (C) 2022 Maxim Integrated Products, Inc., All Rights Reserved.
'*
'* Permission is hereby granted, free of charge, to any person obtaining a
'* copy of this software and associated documentation files (the "Software"),
'* to deal in the Software without restriction, including without limitation
'* the rights to use, copy, modify, merge, publish, distribute, sublicense,
'* and/or sell copies of the Software, and to permit persons to whom the
'* Software is furnished to do so, subject to the following conditions:
'*
'* The above copyright notice and this permission notice shall be included
'* in all copies or substantial portions of the Software.
'*
'* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
'* OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
'* MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
'* IN NO EVENT SHALL MAXIM INTEGRATED BE LIABLE FOR ANY CLAIM, DAMAGES
'* OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
'* ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
'* OTHER DEALINGS IN THE SOFTWARE.
'*
'* Except as contained in this notice, the name of Maxim Integrated
'* Products, Inc. shall not be used except as stated in the Maxim Integrated
'* Products, Inc. Branding Policy.
'*
'* The mere transfer of this software does not imply any licenses
'* of trade secrets, proprietary technology, copyrights, patents,
'* trademarks, maskwork rights, or any other form of intellectual
'* property whatsoever. Maxim Integrated Products, Inc. retains all
'* ownership rights.
'*******************************************************************************
'*/

' External DLL function declarations

'// Session Layer functions
Declare PtrSafe Function TMExtendedStartSession Lib "IBFS64.DLL" (ByVal PortNum As Integer, ByVal PortType As Integer, ByVal Reserved As Any) As Long
Declare PtrSafe Function TMValidSession Lib "IBFS64.DLL" (ByVal session_handle As Long) As Integer
Declare PtrSafe Function TMEndSession Lib "IBFS64.DLL" (ByVal session_handle As Long) As Integer

'// File Operations Layer functions
Declare PtrSafe Function TMFirstFile Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte, fentry As Byte) As Integer
Declare PtrSafe Function TMNextFile Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte, fentry As Byte) As Integer
Declare PtrSafe Function TMOpenFile Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte, fentry As Byte) As Integer
Declare PtrSafe Function TMCreateFile Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte, maxwrite As Integer, fentry As Byte) As Integer
Declare PtrSafe Function TMCloseFile Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte, ByVal file_handle As Integer) As Integer
Declare PtrSafe Function TMReadFile Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte, ByVal file_handle As Integer, read_buffer As Byte, ByVal max_read As Integer) As Integer
Declare PtrSafe Function TMWriteFile Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte, ByVal file_handle As Integer, write_buffer As Byte, ByVal num_write As Integer) As Integer
Declare PtrSafe Function TMDeleteFile Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte, fentry As Byte) As Integer
Declare PtrSafe Function TMFormat Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte) As Integer
Declare PtrSafe Function TMAttribute Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte, ByVal attrib As Integer, fentry As Byte) As Integer
Declare PtrSafe Function TMReNameFile Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte, ByVal file_handle As Integer, fentry As Byte) As Integer
Declare PtrSafe Function TMChangeDirectory Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte, ByVal operation As Integer, cd_buf As Byte) As Integer
Declare PtrSafe Function TMDirectoryMR Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte, ByVal operation As Integer, fentry As Byte) As Integer
Declare PtrSafe Function TMCreateProgramJob Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte) As Integer
Declare PtrSafe Function TMDoProgramJob Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte) As Integer
Declare PtrSafe Function TMWriteAddFile Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte, ByVal operation As Integer, ByVal offset As Integer, write_buffer As Byte, ByVal num_write As Integer) As Integer
Declare PtrSafe Function TMTerminateAddFile Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte, fentry As Byte) As Integer
Declare PtrSafe Function TMGetFamilySpec Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte, FamSpec As Byte) As Integer
Declare PtrSafe Function Get_Version Lib "IBFS64.DLL" (ByVal ID_buf$) As Integer

'// Transport Layer functions
Declare PtrSafe Function TMReadPacket Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte, ByVal StartPg As Integer, ReadBuf As Byte, ByVal MaxRead As Integer) As Integer
Declare PtrSafe Function TMWritePacket Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte, ByVal StartPg As Integer, WriteBuf As Byte, ByVal Writelen As Integer) As Integer
Declare PtrSafe Function TMBlockIO Lib "IBFS64.DLL" (ByVal session_handle As Long, tran_buffer As Byte, ByVal num_tran As Integer) As Integer
Declare PtrSafe Function TMExtendedReadPage Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte, ByVal StartPg As Integer, ReadBuf As Byte, ByVal MSpace As Integer) As Integer
Declare PtrSafe Function TMProgramByte Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte, ByVal WRByte As Integer, ByVal Addr As Integer, ByVal MSpace As Integer, Bits As Integer, ByVal Zeros As Integer) As Integer
Declare PtrSafe Function TMProgramBlock Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte, WriteBuf As Byte, ByVal Length As Integer, ByVal Address As Integer, Bits As Integer) As Integer
Declare PtrSafe Function TMCRC Lib "IBFS64.DLL" (ByVal Length As Integer, Buf As Byte, ByVal Seed As Integer, ByVal CRCType As Integer) As Integer

'// Network Layer functions
Declare PtrSafe Function TMFirst Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte) As Integer
Declare PtrSafe Function TMNext Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte) As Integer
Declare PtrSafe Function TMAccess Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte) As Integer
Declare PtrSafe Function TMStrongAccess Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte) As Integer
Declare PtrSafe Function TMStrongAlarmAccess Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte) As Integer
Declare PtrSafe Function TMOverAccess Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte) As Integer
Declare PtrSafe Function TMRom Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte, ROM As Integer) As Integer
Declare PtrSafe Function TMFirstAlarm Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte) As Integer
Declare PtrSafe Function TMNextAlarm Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte) As Integer
Declare PtrSafe Function TMFamilySearchSetup Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte, ByVal family_type As Integer) As Integer
Declare PtrSafe Function TMSkipFamily Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte) As Integer
Declare PtrSafe Function TMAutoOverDrive Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte, Mode As Integer) As Integer
Declare PtrSafe Function TMSearch Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte, ResetSearch As Integer, ByVal PerformReset As Integer, ByVal SrchCmd As Integer) As Integer

'// Hardware Specific Layer functions
Declare PtrSafe Function TMSetup Lib "IBFS64.DLL" (ByVal session_handle As Long) As Integer
Declare PtrSafe Function TMTouchByte Lib "IBFS64.DLL" (ByVal session_handle As Long, ByVal outbyte As Integer) As Integer
Declare PtrSafe Function TMTouchReset Lib "IBFS64.DLL" (ByVal session_handle As Long) As Integer
Declare PtrSafe Function TMTouchBit Lib "IBFS64.DLL" (ByVal session_handle As Long, ByVal outbit As Integer) As Integer
Declare PtrSafe Function TMProgramPulse Lib "IBFS64.DLL" (ByVal session_handle As Long) As Integer
Declare PtrSafe Function TMOneWireLevel Lib "IBFS64.DLL" (ByVal session_handle As Long, ByVal operation As Integer, ByVal LevelMode As Integer, ByVal primed As Integer) As Integer
Declare PtrSafe Function TMOneWireCom Lib "IBFS64.DLL" (ByVal session_handle As Long, ByVal operation As Integer, ByVal TimeMode As Integer) As Integer
Declare PtrSafe Function TMClose Lib "IBFS64.DLL" (ByVal session_handle As Long) As Integer
Declare PtrSafe Function TMGetTypeVersion Lib "IBFS64.DLL" (ByVal HSType As Integer, ByVal ID_buf$) As Integer
Declare PtrSafe Function TMBlockStream Lib "IBFS64.DLL" (ByVal session_handle As Long, tran_buffer As Byte, ByVal num_tran As Integer) As Integer
Declare PtrSafe Function TMReadDefaultPort Lib "IBFS64.DLL" (PortNum As Integer, PortType As Integer) As Integer
Declare PtrSafe Function TMGetAdapterSpec Lib "IBFS64.DLL" (ByVal session_handle As Long, state_buffer As Byte, AdapterSpec As Byte) As Integer


