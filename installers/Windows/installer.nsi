; http://nsis.sourceforge.net/Uninstall_only_installed_files

!include "UnInstallLog.nsh"
!include "LogicLib.nsh"

; The name of the installer
Name "Lost Soul Squasher"

; The file to write
OutFile "lostsoulsquasher-ver-arch-setup.exe"

; The default installation directory
InstallDir "$PROGRAMFILES\LostSoulSquasher"

;ComponentText "Please choose the options you would like to install"

; The text to prompt the user to enter a directory
DirText "Choose install directory"

RequestExecutionLevel admin

;--------------------------------

;--------------------------------
; Configure UnInstall log to only remove what is installed
;--------------------------------
  ;Set the name of the uninstall log
    !define UninstLog "lostsoulcrusher_uninstall.log"
    Var UninstLog
  ;The root registry to write to
    !define REG_ROOT "HKLM"
  ;The registry path to write to
    !define REG_APP_PATH "SOFTWARE\appname"

  ;Uninstall log file missing.
    LangString UninstLogMissing ${LANG_ENGLISH} "${UninstLog} not found!$\r$\nUninstallation cannot proceed!"

  ;AddItem macro
    !define AddItem "!insertmacro AddItem"

  ;BackupFile macro
    !define BackupFile "!insertmacro BackupFile"

  ;BackupFiles macro
    !define BackupFiles "!insertmacro BackupFiles"

  ;Copy files macro
    !define CopyFiles "!insertmacro CopyFiles"

  ;CreateDirectory macro
    !define CreateDirectory "!insertmacro CreateDirectory"

  ;CreateShortcut macro
    !define CreateShortcut "!insertmacro CreateShortcut"

  ;CreateShortcut2 macro
    !define CreateShortcutTwo "!insertmacro CreateShortcut2"

  ;File macro
    !define File "!insertmacro File"

  ;Rename macro
    !define Rename "!insertmacro Rename"

  ;RestoreFile macro
    !define RestoreFile "!insertmacro RestoreFile"

  ;RestoreFiles macro
    !define RestoreFiles "!insertmacro RestoreFiles"

  ;SetOutPath macro
    !define SetOutPath "!insertmacro SetOutPath"

  ;WriteRegDWORD macro
    !define WriteRegDWORD "!insertmacro WriteRegDWORD"

  ;WriteRegStr macro
    !define WriteRegStr "!insertmacro WriteRegStr"

  ;WriteUninstaller macro
    !define WriteUninstaller "!insertmacro WriteUninstaller"


!define NETVersion "4.0.30319"
!define NETInstaller "dotNetFx40_Full_x86_x64.exe"
Section "MS .NET Framework v${NETVersion}" SecDotNetFramework
  IfFileExists "$WINDIR\Microsoft.NET\Framework\v${NETVersion}" NETFrameworkInstalled 0
  File /oname=$TEMP\${NETInstaller} ${NETInstaller}

  DetailPrint "Starting Microsoft .NET Framework v${NETVersion} Setup..."
  ExecWait "$TEMP\${NETInstaller}"
  Return

  NETFrameworkInstalled:
  DetailPrint "Microsoft .NET Framework is already installed!"
SectionEnd

!define XNAInstaller "xnafx40_redist.msi"
Section "Microsoft XNA Redist" SecXnaRedist
  File /oname=$TEMP\${XNAInstaller} ${XNAInstaller}
  #check 32bit
  ReadRegDWORD $0 HKLM "Software\Microsoft\XNA\Framework\v4.0" "Installed"
  #check 64bit
  ReadRegDWORD $1 HKLM "Software\Wow6432Node\Microsoft\XNA\Framework\v4.0" "Installed"
  DetailPrint "32 bit XNA: $0"
  DetailPrint "64 bit XNA: $1"

  ${If} $0 != 1
  ${AndIf} $1 != 1
    DetailPrint "Starting Microsoft XNA Redist Setup..."
    ExecWait '"msiexec" /i "$TEMP\${XNAInstaller}" /passive' $2
    DetailPrint "Microsoft XNA Redist setup result $0"
    ${If} $2 != 0
      MessageBox MB_OK|MB_ICONEXCLAMATION "XNA 4.0 redistributable failed to install. You might have problems running the game. Please consider installing it manually if game doesn't work."
    ${EndIf}
  ${Else}
    DetailPrint "Microsoft XNA Redist is already installed!"
  ${EndIf}
SectionEnd

Section -openlogfile
  CreateDirectory "$INSTDIR"
  IfFileExists "$INSTDIR\${UninstLog}" +3
    FileOpen $UninstLog "$INSTDIR\${UninstLog}" w
  Goto +4
    SetFileAttributes "$INSTDIR\${UninstLog}" NORMAL
    FileOpen $UninstLog "$INSTDIR\${UninstLog}" a
    FileSeek $UninstLog 0 END
SectionEnd

;--------------------------------

; The stuff to install
Section "" ;No components page, name is not important

; Set output path to the installation directory.
${SetOutPath} $INSTDIR
${WriteUninstaller} "Uninstall.exe"

; Put file there
${File} "." "LostSoul.exe"
${File} "." "CREDITS.txt"
${File} "." "README.md"
${File} "." "LICENSE.freedoom.txt"
${File} "." "LICENSE.lostsoul.txt"

${SetOutPath} "$INSTDIR\Content"
${File} "Content" "1up.xnb"
${File} "Content" "atomboom.xnb"
${File} "Content" "baby.xnb"
${File} "Content" "bonus_1up.xnb"
${File} "Content" "bonus_5up.xnb"
${File} "Content" "bonus_atom.xnb"
${File} "Content" "bonus_baby.xnb"
${File} "Content" "bonus_turtle.xnb"
${File} "Content" "Carefree.wma"
${File} "Content" "Carefree.xnb"
${File} "Content" "crosshair.xnb"
${File} "Content" "DSBAREXP.xnb"
${File} "Content" "DSDMACT.xnb"
${File} "Content" "DSDMPAIN.xnb"
${File} "Content" "DSSKLATK.xnb"
${File} "Content" "ecfike__the-end-of-the-world.wma"
${File} "Content" "ecfike__the-end-of-the-world.xnb"
${File} "Content" "Miramonte.xnb"
${File} "Content" "MISLB0.xnb"
${File} "Content" "MISLC0.xnb"
${File} "Content" "MISLD0.xnb"
${File} "Content" "SKUL.xnb"
${File} "Content" "SKUL_left.xnb"
${File} "Content" "turtle.xnb"

${SetOutPath} "$INSTDIR\Content\backgrounds"
${File} "Content/backgrounds" "bg1.xnb"
${File} "Content/backgrounds" "bg2.xnb"
${File} "Content/backgrounds" "bg3.xnb"
${File} "Content/backgrounds" "bg4.xnb"
${File} "Content/backgrounds" "bg5.xnb"
${File} "Content/backgrounds" "bg6.xnb"
${File} "Content/backgrounds" "bg7.xnb"
${File} "Content/backgrounds" "gameover1.xnb"


${CreateDirectory} "$SMPROGRAMS\LostSoulSquasher"
${CreateShortcutTwo} "$SMPROGRAMS\LostSoulSquasher\LostSoulSquasher.lnk" "$INSTDIR\LostSoul.exe"
${CreateShortcutTwo} "$SMPROGRAMS\LostSoulSquasher\Uninstall.lnk" "$INSTDIR\Uninstall.exe"

; //////// CREATE REGISTRY KEYS FOR ADD/REMOVE PROGRAMS IN CONTROL PANEL /////////

${WriteRegStr} HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\LostSoulSquasher" \
	"DisplayName" \
	"LostSoulSquasher (remove only)"

${WriteRegStr} HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\LostSoulSquasher"  \
	"UninstallString" \
	"$INSTDIR\Uninstall.exe"

MessageBox MB_YESNO|MB_ICONQUESTION "Installation was successful. Start the program now?" \
	IDNO end_install

SetAutoClose true
Exec "$INSTDIR\LostSoul.exe"

	end_install:

SectionEnd ; end the section

; The uninstall section
;--------------------------------
; Uninstaller
;--------------------------------
Section Uninstall
  ;Can't uninstall if uninstall log is missing!
  IfFileExists "$INSTDIR\${UninstLog}" +3
    MessageBox MB_OK|MB_ICONSTOP "$(UninstLogMissing)"
      Abort

  Push $R0
  Push $R1
  Push $R2
  SetFileAttributes "$INSTDIR\${UninstLog}" NORMAL
  FileOpen $UninstLog "$INSTDIR\${UninstLog}" r
  StrCpy $R1 -1

  GetLineCount:
    ClearErrors
    FileRead $UninstLog $R0
    IntOp $R1 $R1 + 1
    StrCpy $R0 $R0 -2
    Push $R0
    IfErrors 0 GetLineCount

  Pop $R0

  LoopRead:
    StrCmp $R1 0 LoopDone
    Pop $R0

    IfFileExists "$R0\*.*" 0 +3
      RMDir $R0  #is dir
    Goto +9
    IfFileExists $R0 0 +3
      Delete $R0 #is file
    Goto +6
    StrCmp $R0 "${REG_ROOT} ${REG_APP_PATH}" 0 +3
      DeleteRegKey ${REG_ROOT} "${REG_APP_PATH}" #is Reg Element
    Goto +3
    StrCmp $R0 "${REG_ROOT} ${UNINSTALL_PATH}" 0 +2
      DeleteRegKey ${REG_ROOT} "${UNINSTALL_PATH}" #is Reg Element

    IntOp $R1 $R1 - 1
    Goto LoopRead
  LoopDone:
  FileClose $UninstLog
  Delete "$INSTDIR\${UninstLog}"
  RMDir "$INSTDIR"
  Pop $R2
  Pop $R1
  Pop $R0

  ;Remove registry keys
    ;DeleteRegKey ${REG_ROOT} "${REG_APP_PATH}"
    ;DeleteRegKey ${REG_ROOT} "${UNINSTALL_PATH}"
SectionEnd
