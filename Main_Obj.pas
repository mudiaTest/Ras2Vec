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
    procedure ReadFromImg(aimg: TImage);
    procedure FillImg(aimg: TImage; azoom: Integer; agrid: boolean; agridColor: TColor);
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
  public
    constructor Create; reintroduce;
  end;

  TRectVector = class (TVectorObj)
  private
    function getP(lp: Integer): TOPoint;
    function getP1: TOPoint;
    function getP2: TOPoint;
  public
    constructor Create(acolor: TColor; p1, p2: TOPoint); overload;
    property p1: TOPoint read getP1;
    property p2: TOPoint read getP2;
  end;

implementation

{ vectList }

procedure TVectList.FillImg(aimg: TImage; azoom: Integer; agrid: boolean; agridColor: TColor);
var
  x, y: Integer;
  i: Integer;
  vectObj: TRectVector;
begin
  aimg.Width := srcWidth;
  aimg.Height := srcHeight;
  for i:=0 to vectList.count-1 do
  begin

    with aimg.Canvas do
    begin
      vectObj := vectList.Objects[i] as TRectVector;
      Pen.Style := psClear;
      if agrid then
      begin
        Pen.Style := psSolid;
        Pen.Color := agridColor;
      end;

      Brush.Color := vectObj.color;
      Rectangle(vectObj.p1.X*azoom, vectObj.p1.Y*azoom,
                (vectObj.p2.X+2)*azoom, (vectObj.p2.Y+2)*azoom);

      //Rectangle(0+i, 0+i, 2+i, 2+i);
    end;
  end;

  with aimg.Canvas do
  begin
   // Pen.Style := psClear;
    //Pen.Color := clGreen;
    //Brush.Style := bsclear;
   // Brush.Color := clRed;
    //Polyline([Point(40, 40), Point(20, 60), Point(70, 30),
    //  Point(90, 30)]);
   // Rectangle(0, 0, 2, 2);
    //Rectangle(2, 1, 4, 3);
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
        flist.AddObject(ile, TRectVector.Create(aimg.Canvas.Pixels[x, y],
                        TOPoint.getPoint(Point(x, y)), TOpoint.getPoint(Point(x, y))));
        inc(ile);
      end;
  end;
  srcWidth := aimg.Width;
  srcHeight := aimg.Height;
end;

{ rectVector }

constructor TRectVector.Create(acolor: TColor; p1, p2: TOPoint);
begin
  inherited Create;
  color := acolor;
  fpoints.AddObject(0, p1);
  fpoints.AddObject(1, p2);
end;

function TRectVector.getP(lp: Integer): TOPoint;
begin
  Result := fpoints.Objects[lp] as TOpoint;
end;

function TRectVector.getP1: TOPoint;
begin
  Result := getP(0);
end;

function TRectVector.getP2: TOPoint;
begin
  Result := getP(1);
end;

{ TVectorObj }

constructor TVectorObj.Create;
begin
  fpoints := TIntList.Create;
end;

end.
