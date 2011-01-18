unit Sys_utl;

interface

uses
  Classes, SysUtils, Windows;

type
  TIntList = class (TStringList)
    function AddObject(key: integer; obj: TObject): Integer; reintroduce;
    function GetInt(Index: Integer): integer;
    procedure PutInt(Index: Integer; const S: integer);
    function GetObjByVal(val: Integer): TObject;
    function IndexOf(const val: integer): Integer; overload;

  public
    property Integers[Index: Integer]: Integer read GetInt write PutInt; default; //reintroduce; default;
    property ObjByVal[val: integer]: TObject read GetObjByVal;
  end;

  TOPoint = class (TObject)
  private
    fpoint: TPoint;
  published
    property x: integer read fpoint.x write fpoint.x;
    property y: integer read fpoint.y write fpoint.y;
  public
    class function getPoint(point: TPoint): TOPoint;
  end;


implementation

{ TIntList }

function TIntList.AddObject(key: integer; obj: TObject): Integer;
begin
  Result := inherited AddObject(IntToStr(key), obj);
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

procedure TIntList.PutInt(Index: Integer; const S: Integer);
begin
   Put(Index, IntToStr(s));
end;

{ TOPoint }

class function TOPoint.getPoint(point: TPoint): TOPoint;
begin
  Result := TOPoint.Create;
  Result.x := point.X;
  Result.y := point.y;
end;

end.
