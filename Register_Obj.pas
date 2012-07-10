unit Register_Obj;

interface

uses
  Registry, Windows, Main_CV;

type
  TMainReg = class
  private
    reg: TRegistry;
  public
    constructor Create;
    destructor Destroy;
    function GetMain(aKey: String): String;
    procedure SetMain(aKey, aVal: String);
    function GetVal(aKey: String): String; virtual; abstract;
    procedure SetVal(aKey, aVal: String); virtual; abstract;
  end;

  TSrcReg = class(TMainReg)
  public
    function GetVal(aKey: String): String; override;
    procedure SetVal(aKey, aVal: String); override;
    function GetFilePathName: String;
    function GetGeo1X: String;
    function GetGeo1Y: String;
    function GetGeo2X: String;
    function GetGeo2Y: String;
    function GetLastSLPath: String;
    procedure SetFilePathName(aVal: String);
    procedure SetGeo1X(aVal: String);
    procedure SetGeo1Y(aVal: String);
    procedure SetGeo2X(aVal: String);
    procedure SetGeo2Y(aVal: String);
    procedure SetLastSLPath(aVal: String);
  end;

implementation

{ TSrcReg }

constructor TMainReg.Create;
begin
  reg := TRegistry.Create;
  reg.rootKey := HKEY_CURRENT_USER;
end;

destructor TMainReg.Destroy;
begin
  reg.Free;
end;

function TMainReg.GetMain(aKey: String): String;
begin
  if reg.OpenKey(PROGRAM_REG_PATH, true) then
  begin
    try
      result := GetVal(aKey);
    finally
      reg.CloseKey;
    end;
  end else
  begin
    //if not reg.OpenKey(PROGRAM_REG_PATH, true) then
      Assert(false, 'Nie mo¿na utworzyæ wpisu w rejestrze dla "' + PROGRAM_REG_PATH + '"');
    //reg.CloseKey;
  end;
end;

procedure TMainReg.SetMain(aKey, aVal: String);
begin
  if reg.OpenKey(PROGRAM_REG_PATH, true) then
  begin
    try
      SetVal(aKey, aVal);
    finally
      reg.CloseKey;
    end;
  end else
  begin
    //if not reg.OpenKey(PROGRAM_REG_PATH, true) then
      Assert(false, 'Nie mo¿na utworzyæ wpisu w rejestrze dla "' + PROGRAM_REG_PATH + '"');
    //reg.CloseKey;
  end;
end;

procedure TSrcReg.SetFilePathName(aVal: String);
begin
  SetMain(FILE_PATH_NAME, aVal);
end;

procedure TSrcReg.SetGeo1X(aVal: String);
begin
  SetMain(GEO_1_X, aVal);
end;

procedure TSrcReg.SetGeo1Y(aVal: String);
begin
  SetMain(GEO_1_Y, aVal);
end;

procedure TSrcReg.SetGeo2X(aVal: String);
begin
  SetMain(GEO_2_X, aVal);
end;

procedure TSrcReg.SetGeo2Y(aVal: String);
begin
  SetMain(GEO_2_Y, aVal);
end;

procedure TSrcReg.SetLastSLPath(aVal: String);
begin
  SetMain(LAST_SL_PATH, aVal);
end;

procedure TSrcReg.SetVal(aKey, aVal: String);
begin
  if not reg.KeyExists(aKey) then
  try
    reg.WriteString(aKey, aVal);
  except
    Assert(false, 'Nie mo¿na utworzyæ wartoœci w rejestrze dla "' + aKey + '"');
  end;
end;

{ TSrcReg }

function TSrcReg.GetFilePathName: String;
begin
  result := GetMain(FILE_PATH_NAME);
end;

function TSrcReg.GetGeo1X: String;
begin
  result := GetMain(GEO_1_X);
end;

function TSrcReg.GetGeo1Y: String;
begin
  result := GetMain(GEO_1_Y);
end;

function TSrcReg.GetGeo2X: String;
begin
  result := GetMain(GEO_2_X);
end;

function TSrcReg.GetGeo2Y: String;
begin
  result := GetMain(GEO_2_Y);
end;

function TSrcReg.GetLastSLPath: String;
begin
  result := GetMain(LAST_SL_PATH);
end;

function TSrcReg.GetVal(aKey: String): String;
begin
  if not reg.KeyExists(aKey) then
  try
    reg.ReadString(aKey);
  except
    Assert(false, 'Nie mo¿na utworzyæ wartoœci w rejestrze dla "' + aKey + '"');
  end;
  result := reg.ReadString(aKey);
end;

end.
