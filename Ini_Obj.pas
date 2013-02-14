unit Ini_Obj;

interface

type
  TIniSL = class
  public
    stGraphFileNamePath: String;
    stGeo1X, stGeo1Y, stGeo2X, stGeo2Y: String;
    stMPFilePathName: String;
    constructor Create;
    procedure SaveAs(aFileNamePath: String); overload;
    procedure SaveAs; overload;
    procedure Load(var alastSLPath: String);
    procedure Init(astGeo1X,
                   astGeo1Y,
                   astGeo2X,
                   astGeo2Y,
                   astGraphFileNamePath,
                   astMPFilePathName: String);
  end;


implementation

uses
  Dialogs, Main_CV, SysUtils, IniFiles;

{ TIniSL }

procedure TIniSL.SaveAs(aFileNamePath: String);
var
  ini: TIniFile;
begin
  lastSLPath := aFileNamePath;
  ini := TIniFile.Create(aFileNamePath);
  try
    ini.WriteString('Main', 'Title', 'R2VIni');
    ini.WriteString('Main', 'GraphFile', stGraphFileNamePath);
    ini.WriteString('Main', 'Geo1X', stGeo1X);
    ini.WriteString('Main', 'Geo1Y', stGeo1Y);
    ini.WriteString('Main', 'Geo2X', stGeo2X);
    ini.WriteString('Main', 'Geo2Y', stGeo2Y);
    ini.WriteString('Main', 'MPFilePathName', stMPFilePathName);
  finally
    ini.Free;
  end;
end;

constructor TIniSL.Create;
begin
  stGraphFileNamePath := '';
  stGeo1X := '';
  stGeo1Y := '';
  stGeo2X := '';
  stGeo2Y := '';
  stMPFilePathName := '';
end;

procedure TIniSL.Init(astGeo1X,
                      astGeo1Y,
                      astGeo2X,
                      astGeo2Y,
                      astGraphFileNamePath,
                      astMPFilePathName: String);

begin
  stGeo1X := astGeo1X;
  stGeo1Y := astGeo1Y;
  stGeo2X := astGeo2X;
  stGeo2Y := astGeo2Y;
  stGraphFileNamePath := astGraphFileNamePath;
  stMPFilePathName := astGraphFileNamePath;
end;

procedure TIniSL.Load(var alastSLPath: String);
var
  od: TOpenDialog;
  ini: TIniFile;
  stTitle: String;
begin
  od := TOpenDialog.Create(nil);
  try
    od.InitialDir := ExtractFileDir(lastSLPath);
    od.DefaultExt := 'ini';
    od.Filter := 'Ini files (*.ini)|*.INI;All files (*.*)|*.*';
    if not od.Execute then
      Exit;
    if not FileExists(od.FileName) then
      Assert(false, 'Nie odnaleziono pliku konfiguracyjnego: ' + od.FileName);
    ini := TIniFile.Create(od.FileName);
    alastSLPath := od.FileName;
    try
      stTitle := ini.ReadString('Main', 'Title', '');
      Assert(stTitle <> '', 'Btak Tytu³u - niepoprawny plik konfiguracyjny.');
      stGraphFileNamePath := ini.ReadString('Main', 'GraphFile', '');
      stGeo1X := ini.ReadString('Main', 'Geo1X', '');
      stGeo1Y := ini.ReadString('Main', 'Geo1Y', '');
      stGeo2X := ini.ReadString('Main', 'Geo2X', '');
      stGeo2Y := ini.ReadString('Main', 'Geo2Y', '');
      stMPFilePathName := ini.ReadString('Main', 'MPFilePathName', '');
    finally
      ini.free;
    end;
  finally
    od.Free;
  end;
end;

procedure TIniSL.SaveAs();
var
  sd: TSaveDialog;
begin
  sd := TSaveDialog.Create(nil);
  try
    sd.InitialDir := ExtractFileDir(lastSLPath);
    sd.DefaultExt := 'ini';
    sd.Filter := 'Ini files (*.ini)|*.INI;All files (*.*)|*.*';
    if not sd.Execute then
      Exit;
    SaveAs(sd.FileName);
  finally
    sd.Free;
  end;
end;

end.
