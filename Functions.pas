unit Functions;

interface

uses
  Sys_utl, Graphics;

type
  vectList = class(TIntList)
  private
    vlist: TIntList;
  public
    constructor Create;
    procedure FillFromBmp(abmp: TBitMap);
    
  end;

implementation

{ vectList }

constructor vectList.Create;
begin
  vlist := TIntList.Create;
end;

procedure vectList.FillFromBmp(abmp: TBitMap);
var
  i, j: integer;
begin
  if abmp.Height * abmp.Width > 0 then
  begin
    vlist := nil;
    Exit;
  end else
  begin
    vlist := TIntList.Create;
    for i:=0 to abmp.Height-1 do
      for j:=0 to abmp.Width-1 do 
      begin
        vlist.AddObject()
      end;
  end;    
end;

end.
