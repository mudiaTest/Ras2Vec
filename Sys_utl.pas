unit Sys_utl;

interface

uses
  Classes, SysUtils;

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

end.
