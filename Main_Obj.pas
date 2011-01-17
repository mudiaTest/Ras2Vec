unit Main_Obj;

interface

uses
  Sys_utl, Windows, Graphics, ExtCtrls, Classes;

type
  TVectList = class(TIntList)
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

  TVectorObj = class
  protected
    fpoints: TIntList;
    fcolor: TColor;
  published
    property color: TColor read fcolor write fcolor;
  end;

  TRectVector = class (TVectorObj)
  private
    function getP(val: Integer): TPoint;
    function getP1: TPoint;
    function getP2: TPoint;
  public
    constructor Create(acolor: TColor; p1, p2: TPoint); reintroduce;
    property p1: TPoint read getP1;
    property p2: TPoint read getP2;
  end;

implementation

{ vectList }

constructor TVectList.Create;
begin

end;

procedure TVectList.FillImg(aimg: TImage);
var
  x, y: Integer;
  i: Integer;
  vectObj: TRectVector;
begin
  aimg.Width := srcWidth;
  aimg.Height := srcHeight;
  for i:=0 to vectList.count-1 do
  begin
    vectObj := vectList.Objects[i] as TRectVector;
    aimg.Canvas.Rectangle(vectObj.p1.X, vectObj.p1.Y, vectObj.p2.X, vectObj.p2.Y);
  end;
end;

procedure TVectList.ReadFromImg(aimg: TImage);
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
        flist.AddObject(ile, TRectVector.Create(aimg.Canvas.Pixels[x, y], Point(x, y), Point(x, y)));
        inc(ile);
      end;
  end;
  srcWidth := aimg.Width;
  srcHeight := aimg.Height;
end;

{ rectVector }

constructor TRectVector.Create(acolor: TColor; p1, p2: TPoint);
begin
  color := acolor;
  fpoints.AddObject(0, p1);
  fpoints.AddObject(1, p2);
end;

function TRectVector.getP(val: Integer): TPoint;
var
  r: TPoint
begin
  Result := Point((fpoints.ObjByVal[val]) as tPo, 1);
end;

function TRectVector.getP1: TPoint;
begin
  Result := getP(0);
end;

function TRectVector.getP2: TPoint;
begin
  Result := getP(1);
end;

end.
