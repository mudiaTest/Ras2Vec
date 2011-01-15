unit Obj;

interface

uses
  Sys_utl, Windows, Graphics, ExtCtrls, Classes;

type
  vectList = class(TIntList)
  private
    flist: TIntList;
  public
    constructor Create;
    procedure FillFromBmp(aimg: TImage);
  published
    property vectList: TIntList read flist write flist;
  end;

  vectorObj = class
  private
    fpoints: TIntList;
    fcolor: TColor;
  published
    property color: TColor read fcolor write fcolor;
  end;

  rectVector = class (vectorObj)
  public
    constructor Create(acolor: TColor; p1, p2: TPoint); reintroduce;
  end;

implementation

{ vectList }

constructor vectList.Create;
begin

end;

procedure vectList.FillFromBmp(aimg: TImage);
var
  i, j: integer;
  ile: Integer;
begin
  ile := 0;
  if (aimg.Width * aimg.Height = 0) then
  begin
    flist := nil;
    Exit;
  end else
  begin
    flist := TIntList.Create;
    for j:=0 to aimg.Height do
      for i:=0 to aimg.Width do
      begin
        flist.AddObject(ile, rectVector.Create(aimg.Canvas.Pixels[i, j], Point(i,j), Point(i,j)));
        inc(ile);
      end;
  end;
end;

{ rectVector }

constructor rectVector.Create(acolor: TColor; p1, p2: TPoint);
begin
  color := acolor;
end;

end.
