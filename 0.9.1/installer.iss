; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

[Setup]
AppName=NProf
AppVerName=NProf 0.9.1
AppPublisher=NProf Community
AppPublisherURL=http://nprof.sourceforge.net
AppSupportURL=http://nprof.sourceforge.net
AppUpdatesURL=http://nprof.sourceforge.net
DefaultDirName={pf}/NProf0.9.1
DefaultGroupName=NProf
DisableProgramGroupPage=yes
OutputDir=C:\nprof\Releases
OutputBaseFilename=nprof-0.9.1-setup
SetupIconFile=C:\nprof\branches\0_9_1\nprof\NProf.Application\App.ico
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
Source: "C:\nprof\branches\0_9_1\nprof\NProf.Application\bin\Release\NProf.Application.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\nprof\branches\0_9_1\nprof\NProf.Application\bin\Release\NProf.Utilities.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\nprof\branches\0_9_1\nprof\NProf.Application\bin\Release\CommandBar.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\nprof\branches\0_9_1\nprof\NProf.Application\bin\Release\DotNetLib.Windows.Forms.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\nprof\branches\0_9_1\nprof\NProf.Application\bin\Release\DotNetLib.Windows.Forms.Themes.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\nprof\branches\0_9_1\nprof\NProf.Application\bin\Release\genghis.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\nprof\branches\0_9_1\nprof\NProf.Application\bin\Release\ICSharpCode.SharpZipLib.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\nprof\branches\0_9_1\nprof\NProf.Application\bin\Release\MagicLibrary.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\nprof\branches\0_9_1\nprof\NProf.Application\bin\Release\NProf.Glue.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\nprof\branches\0_9_1\nprof\NProf.Application\bin\Release\NProf.GUI.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\nprof\branches\0_9_1\nprof\Build\SupportFiles\RegisterProfilerHook.bat"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\nprof\branches\0_9_1\nprof\Build\SupportFiles\nprof.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\nprof\branches\0_9_1\nprof\Libraries\DotNetLib\msvcr70.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\nprof\branches\0_9_1\nprof\NProf.Hook\Release\NProf.Hook.dll"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\NProf"; Filename: "{app}\NProf.Application.exe"

[Run]
Filename: "{app}\RegisterProfilerHook.bat"; Description: "View the README file"
Filename: "{app}\NProf.Application.exe"; Description: "{cm:LaunchProgram,NProf}"; Flags: nowait postinstall skipifsilent

