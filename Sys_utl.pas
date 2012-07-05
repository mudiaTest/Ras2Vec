unit Sys_utl;

{$M+}

interface

uses
  Classes, SysUtils, Windows, Math;

type
  TTimeInterval = class (TObject)
  public
    dtStart, dtStop: TDateTime;
    interval: Double;
    procedure Start(blReset: boolean = true);
    procedure Stop;
    procedure Reset;
    procedure ZeroInterval;
    function InterSt: String;
    function InterDp: Double;
  end;

  TAdvList = class (TStringList)
  protected
    fnextKey: Integer;
  public
    function nextKey: Integer;
    constructor Create; overload; virtual;
  end;

  TIntList = class (TAdvList)
    function AddObject(val: integer; obj: TObject): Integer; reintroduce;
    function GetInt(Index: Integer): integer;
    procedure PutInt(Index: Integer; const S: integer);
    function GetObjByVal(val: Integer): TObject;
    function IndexOf(const val: integer): Integer; reintroduce; overload;
  public
    property Value[Index: Integer]: Integer read GetInt write PutInt; default; //reintroduce; default;
    property ObjByVal[val: integer]: TObject read GetObjByVal;
  end;

  TDoubleList = class (TAdvList)
    function AddObject(val: double; obj: TObject): Integer; reintroduce;
    function GetDouble(Index: integer): double;
    procedure PutDouble(Index: integer; const S: double);
    function GetObjByVal(val: Double): TObject;
    function IndexOf(const val: double): Integer; reintroduce; overload;
  public
    property Value[Index: Integer]: Double read GetDouble write PutDouble; default; //reintroduce; default;
    property ObjByVal[val: Double]: TObject read GetObjByVal;
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

  function TrimSlash(aStr: String): String;
  function JoinPaths(aPath1, aPath2: String): string;

  function StrConcat(separator, left, right: String): String;

  //zmiania int na dowolnej d�ugo�ci hexa.
  //Ka�dy bajt to 2 znaki w stringu
  //bajty mniej znacz�ce s� pierwsze (od lewej strony)
  //aBytesLen okre�la ilobajtowy ma to by� ci�g
  procedure AddInt2StrHex(aInt: integer; aBytesLen: integer; var aStr: String);

  //zmienia string Hex na liczb� integer
  function Hex2Int(astHex: String): integer;

implementation

  function StrConcat(separator, left, right: String): String;
  begin
    if left = '' then
      result := right
    else if right = '' then
      result := left
    else
      result := left + separator + right;
  end;

  function TrimSlash(aStr: String): String;
  begin
    if Copy(aStr, Length(aStr)-1, Length(aStr)) = '/' then
      result :=  Copy(aStr, 1, Length(aStr));
  end;

  function JoinPaths(aPath1, aPath2: String): string;
  begin
    aPath1 := TrimSlash(aPath1);
    Result := aPath1 + aPath2;
  end;

  procedure addInt2StrHex(aInt: integer; aBytesLen: integer; var aStr: String);
  var
    i: integer;
    stMain: String;
    stPart: String;
  begin
    stMain := intToHex(aInt, aBytesLen*2);
    //w stMain bajty mniej znacz�ce s� po prawej stronie, a my potrzebujemy ich po lewej
    //wi�c zamieniamy kolejno�ci�
    for i := 0 to Ceil(Length(stMain)/2)-1 do
    begin
      stPart := Copy(stMain, Length(stMain)-1, 2);
      stMain := Copy(stMain, 0, Length(stMain)-2);
      aStr := aStr + AnsiChar(strToInt('$' + stPart));
    end;
  end;

  function Hex2Int(astHex: String): integer;
  var
    i: integer;
    stPart: String;
  begin
    result := 0;
    for i := 0 to Ceil(Length(astHex)/2)-1 do
    begin
      stPart := '$' + Copy(astHex, i*2, 2);
      result := result + StrToInt(stPart);
    end;

  end;

{ TIntList }

function TIntList.AddObject(val: integer; obj: TObject): Integer;
begin
Result := inherited AddObject(IntToStr(val), obj);
  fnextKey := Math.max(fnextKey, val+1);
end;

function TIntList.GetInt(Index: Integer): Integer;
begin
  result := strToInt(Get(Index));
end;

function TIntList.GetObjByVal(val: Integer): TObject;
begin
  result := Objects[IndexOf(val)];
end;

function TIntList.IndexOf(const val: integer): Integer;
begin
  Result := IndexOf(IntToStr(val));
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

{ TAdvList }

constructor TAdvList.Create;
begin
  inherited;
  fnextKey := 0;
end;

function TAdvList.nextKey: Integer;
begin
  Result := fnextKey;
end;

{ TDoubleList }

function TDoubleList.AddObject(val: double; obj: TObject): Integer;
begin
  Result := inherited AddObject(FloatToStr(val), obj);
  fnextKey := Math.max(fnextKey, Math.Ceil(val+1));
end;

function TDoubleList.GetDouble(Index: integer): double;
begin
  result := strToFloat(Get(Index));
end;

function TDoubleList.GetObjByVal(val: Double): TObject;
begin
  Result := Objects[IndexOf(val)];
end;

function TDoubleList.IndexOf(const val: double): Integer;
begin
  Result := IndexOf(FloatToStr(val));
end;

procedure TDoubleList.PutDouble(Index: integer; const S: double);
begin
  Put(Index, FloatToStr(s));
  fnextKey := Math.max(fnextKey, Math.Ceil(s+1));
end;

end.
