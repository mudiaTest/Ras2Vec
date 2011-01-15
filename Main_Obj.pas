unit Main_Obj;

interface

uses
  Sys_utl, Windows, Graphics, ExtCtrls, Classes;

type
  vectList = class(TIntList)
  private
    flist: TIntList;
    fsrcWidth: Integer;
    fsrcHeight: Integer;
  public
    constructor Create;
    procedure ReadFromImg(aimg: TImage);
    procedure FillImg(aimg: TImage);
  published
    property vectList: TIntList read flist write flist;
    property srcWidth: Integer read fsrcWidth write fsrcWidth;
    property srcHeight: Integer read fsrcHeight write fsrcHeight;
  end;

  vectorObj = class
  private
    fpoints: TIntList;
    fcolor: TColor;
  published
    property color: TColor read fcolor write fcolor;
  end;

  rectVector = class (vectorObj)
  private
    function getP(lp: Integer): TPoint;
  public
    constructor Create(acolor: TColor; p1, p2: TPoint); reintroduce;
  property p1: TPoint read getP(0);
  end;

implementation

{ vectList }

constructor vectList.Create;
begin

end;

procedure vectList.FillImg(aimg: TImage);
var
  x, y: Integer;
  i: Integer;
  vectObj: rectVector;
begin
  aimg.Width := srcWidth;
  aimg.Height := srcHeight;
  for i:=0 to vectList.count-1 do
  begin
    vectObj := vectList.get(Integer);
    aimg.Canvas.Rectangle(vectObj.);
  end;
end;

procedure vectList.ReadFromImg(aimg: TImage);
var
  x, y: integer;
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
    for y:=0 to aimg.Height do
      for x:=0 to aimg.Width do
      begin
        flist.AddObject(ile, rectVector.Create(aimg.Canvas.Pixels[x, y], Point(x, y), Point(x, y)));
        inc(ile);
      end;
  end;
  srcWidth := aimg.Width;
  srcHeight := aimg.Height;
end;

{ rectVector }

constructor rectVector.Create(acolor: TColor; p1, p2: TPoint);
begin
  color := acolor;
end;

function rectVector.getP(lp: Integer): TPoint;
begin

end;

end.
