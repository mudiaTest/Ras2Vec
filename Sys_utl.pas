unit Sys_utl;

interface

uses
  Classes, SysUtils;

type
  TIntList = class (TStringList)
    function AddObject(key: integer; obj: TObject): Integer; reintroduce;

  end;


implementation

{ TIntList }

function TIntList.AddObject(key: integer; obj: TObject): Integer;
begin
  Result := inherited AddObject(IntToStr(key), obj);
end;

end.
