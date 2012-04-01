unit Sys_utl;

interface

uses
  Classes, SysUtils, Windows, Math;

type
  TTimeInterval = class (TObject)
    dtStart, dtStop: TDateTime;
    interval: Double;
    procedure Start(blReset: boolean = true);
    procedure Stop;
    procedure Reset;
    procedure ZeroInterval;
    function InterSt: String;
    function InterDp: Double;
  end;

  TIntList = class (TStringList)
    function AddObject(val: integer; obj: TObject): Integer; reintroduce;
    function GetInt(Index: Integer): integer;
    procedure PutInt(Index: Integer; const S: integer);
    function GetObjByVal(val: Integer): TObject;
    function IndexOf(const val: integer): Integer; reintroduce; overload;
  private
    fnextKey: Integer;
  public
    function nextKey: Integer;
    property Integers[Index: Integer]: Integer read GetInt write PutInt; default; //reintroduce; default;
    property ObjByVal[val: integer]: TObject read GetObjByVal;
    constructor Create; overload; virtual;
  end;

  TOPoint = class
  private
    fpoint: TPoint;
  published
    property x: integer read fpoint.x write fpoint.x;
    property y: integer read fpoint.y write fpoint.y;
  public
    class function getPoint(point: TPoint): TOPoint;
  end;

  function JoinPaths(aPath1, aPath2: String): string;
implementation

  function JoinPaths(aPath1, aPath2: String): string;
  begin
    if Copy(aPath1, Length(aPath1)-1, Length(aPath1)) = '/' then
      aPath1 := Copy(aPath1, 1, Length(aPath1)-1);
    Result := aPath1 + aPath2;
  end;
{ TIntList }

function TIntList.AddObject(val: integer; obj: TObject): Integer;
begin
  Result := inherited AddObject(IntToStr(val), obj);
  fnextKey := Math.max(fnextKey, val+1);
end;

constructor TIntList.Create;
begin
  inherited;
  fnextKey := 0;
end;

function TIntList.GetInt(Index: Integer): Integer;
begin
  result := strToInt(Get(Index));
end;

function TIntList.GetObjByVal(val: Integer): TObject;
begin
  Objects[IndexOf(val)];
end;

function TIntList.IndexOf(const val: integer): Integer;
begin
  Result := IndexOf(IntToStr(val));
end;

function TIntList.nextKey: Integer;
begin
  Result := fnextKey;
end;

procedure TIntList.PutInt(Index: Integer; const S: Integer);
begin
   Put(Index, IntToStr(s));
   fnextKey := Math.max(fnextKey, s+1);
end;

{ TOPoint }

class function TOPoint.getPoint(point: TPoint): TOPoint;
begin
  Result := TOPoint.Create;
  Result.x := point.X;
  Result.y := point.y;
end;

{ TTimeInterval }

function TTimeInterval.InterDp: Double;
begin
  result := interval;
end;

function TTimeInterval.InterSt: String;
begin
  result := FormatFloat('0.0000000000', interval);

end;

procedure TTimeInterval.Reset;
begin
  dtStart := 0;
  dtStop := 0;
  interval := 0;
end;

procedure TTimeInterval.Start(blReset: boolean);
begin
  if blReset then
    Reset;
  dtStart := getTime;
end;

procedure TTimeInterval.Stop;
begin
  dtStop := getTime;
  interval := interval + (dtStop - dtStart);
end;

procedure TTimeInterval.ZeroInterval;
begin
  interval := 0;
end;

end.
