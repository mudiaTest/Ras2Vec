unit Main_Obj;

interface

uses
  Sys_utl, Windows, Graphics, ExtCtrls, Classes;

type

  TVectObj = class;

  TOColor = class
    color: TColor;
  end;

  TVectByCoordList = class(TIntList)
  private
    fsrcWidth: Integer;
    fsrcHeight: Integer;
    function getObjById(index: Integer): TVectObj;
    procedure setObjById(index: Integer; avectObj: TVectObj);
  published
    property srcWidth: Integer read fsrcWidth write fsrcWidth;
    property srcHeight: Integer read fsrcHeight write fsrcHeight;
  public
    constructor Create;
    procedure put(x, y: Integer; o: TVectObj);
    function get(x, y: Integer): TVectObj;
    procedure ReadFromImg(aimg: TImage);
    procedure FillImg(aimg: TImage; azoom: Integer; agrid: boolean; agridColor: TColor);
    property vObj[index: integer]: TVectObj read getObjById write setObjById;
  end;

  //lista obiektów wektorowych
  TVectList = class(TIntList)
  private
    vectArr: array of array of TVectObj;
    fsrcWidth: Integer;
    fsrcHeight: Integer;
    function getObjById(index: Integer): TVectObj;
    procedure setObjById(index: Integer; avectObj: TVectObj);
  published
    property srcWidth: Integer read fsrcWidth write fsrcWidth;
    property srcHeight: Integer read fsrcHeight write fsrcHeight;
  public
    procedure ReadFromImg(aimg: TImage);
    function FillImg(aimg: TImage; azoom: Integer; agrid: boolean; agridColor: TColor): TBitmap;
    property vObj[index: integer]: TVectObj read getObjById write setObjById;
    constructor Create;
  end;

  //podstawowy obekt wektorowy
  TVectObj = class
  protected
    fpoints: TIntList;
    fcolor: TColor;
  published
    property color: TColor read fcolor write fcolor;
  public
    constructor Create; reintroduce;
  end;

  //obiekt wektorowego Rectangle
  TVectRectangle = class (TVectObj)
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

{ TVectList }

constructor TVectList.Create;
begin
  inherited;
end;

function TVectList.FillImg(aimg: TImage; azoom: Integer; agrid: boolean; agridColor: TColor): TBitmap;
var
  x, y: Integer;
  i: Integer;
  vectObj: TVectRectangle;
  p: Pointer;
  bmp: TBitmap;
begin
  aimg.Width := srcWidth * azoom;
  aimg.Height := srcHeight * azoom;
  bmp := TBitmap.Create;
  bmp.Width := srcWidth * azoom;
  bmp.Height := srcHeight * azoom;
  aimg.Canvas.Lock;

  if agrid then
  begin
    bmp.Canvas.Pen.Style := psSolid;
    bmp.Canvas.Pen.Color := agridColor;
  end
  else
    bmp.Canvas.Pen.Style := psClear;
  for y:=0 to srcHeight-1 do
    for x:=0 to srcWidth-1 do
    begin
      with bmp.Canvas do
      begin
        vectObj := vectArr[x, y] as TVectRectangle;
        Brush.Color := vectObj.color;
        Rectangle(vectObj.p1.X*azoom, vectObj.p1.Y*azoom,
                  (vectObj.p2.X+2)*azoom, (vectObj.p2.Y+2)*azoom);
      end;
    end;
  {
  if agrid then
  begin
    aimg.Canvas.Pen.Style := psSolid;
    aimg.Canvas.Pen.Color := agridColor;
  end;
  for y:=0 to srcHeight-1 do
    for x:=0 to srcWidth-1 do
    begin
      with aimg.Canvas do
      begin
        vectObj := vectArr[x, y] as TVectRectangle;
        Brush.Color := vectObj.color;
        Rectangle(vectObj.p1.X*azoom, vectObj.p1.Y*azoom,
                  (vectObj.p2.X+2)*azoom, (vectObj.p2.Y+2)*azoom);
      end;
    end;
  }
  Result := bmp;
  aimg.Canvas.Unlock;
end;

function TVectList.getObjById(index: Integer): TVectObj;
begin
  result := Objects[index] as TVectObj;
end;

procedure TVectList.setObjById(index: Integer; avectObj: TVectObj);
begin
  Objects[index] := avectObj;
end;

procedure TVectList.ReadFromImg(aimg: TImage);
var
  x, y: integer;
  ile: Integer;
  p: Pointer;
  rec: TVectRectangle;
  rec2: TVectRectangle;
begin
  srcWidth := aimg.Width;
  srcHeight := aimg.Height;
  SetLength(vectArr, srcWidth, srcHeight);
  for y:=0 to aimg.Height-1 do
    for x:=0 to aimg.Width-1 do
    begin
      rec := TVectRectangle.Create(
                  aimg.Canvas.Pixels[x, y],
                  TOPoint.getPoint(Point(x, y)),
                  TOpoint.getPoint(Point(x, y))
               );
      vectArr[x,y] := rec;
      //rec2 := @rec;
      inc(ile);
    end;
end;

{ TVectRectangle }

constructor TVectRectangle.Create(acolor: TColor; p1, p2: TOPoint);
begin
  inherited Create;
  color := acolor;
  fpoints.AddObject(0, p1);
  fpoints.AddObject(1, p2);
end;

function TVectRectangle.getP(lp: Integer): TOPoint;
begin
  Result := fpoints.Objects[lp] as TOpoint;
end;

function TVectRectangle.getP1: TOPoint;
begin
  Result := getP(0);
end;

function TVectRectangle.getP2: TOPoint;
begin
  Result := getP(1);
end;

{ TVectorObj }

constructor TVectObj.Create;
begin
  fpoints := TIntList.Create;
end;

{ TVectByCoordList }

constructor TVectByCoordList.Create;
begin
  
end;

procedure TVectByCoordList.FillImg(aimg: TImage; azoom: Integer; agrid: boolean;
  agridColor: TColor);
var
  x, y: Integer;
  i: Integer;
  vectObj: TVectRectangle;
  h: integer;
begin
  aimg.Width := srcWidth;
  aimg.Height := srcHeight;
  //for i:=0 to count-1 do
  begin

    with aimg.Canvas do
    begin
      if agrid then
      begin
        Pen.Style := psSolid;
        Pen.Color := agridColor;
      end else
        Pen.Style := psClear;

      for x:=0 to srcWidth-1 do
      begin
        for y:=0 to srcHeight-1 do
        begin
          try
            vectObj := get(x, y) as TVectRectangle;
          except
            inc(h);
          end;

          Brush.Color := vectObj.color;
          Rectangle(vectObj.p1.X*azoom, vectObj.p1.Y*azoom,
                    (vectObj.p2.X+2)*azoom, (vectObj.p2.Y+2)*azoom);
        end;
      end;
    end;
  end;
end;

function TVectByCoordList.get(x, y: Integer): TVectObj;
begin
  result := (Objects[x] as TIntlist).Objects[y] as TVectObj;
end;

function TVectByCoordList.getObjById(index: Integer): TVectObj;
begin
   result := Objects[index] as TVectObj;
end;

procedure TVectByCoordList.put(x, y: Integer; o: TVectObj);
var
  listaY: TIntList;
begin
  if count > x then
    listaY := Objects[x] as TIntList
  else
    listaY := TIntList.Create;
  listaY.addObject(y, o);
  addObject(x, listaY);
end;

procedure TVectByCoordList.ReadFromImg(aimg: TImage);
var
  x, y: integer;
  ile: Integer;
begin
  ile := 0;
  if (aimg.Width * aimg.Height = 0) then
  begin
    Clear;
    Exit;
  end else
  begin
    for y:=0 to aimg.Height do
      for x:=0 to aimg.Width do
      begin
        put(x, y, TVectRectangle.Create(aimg.Canvas.Pixels[x, y],
                  TOPoint.getPoint(Point(x, y)), TOpoint.getPoint(Point(x, y))));
        inc(ile);
      end;
  end;
  srcWidth := aimg.Width;
  srcHeight := aimg.Height;
end;

procedure TVectByCoordList.setObjById(index: Integer; avectObj: TVectObj);
begin
  Objects[index] := avectObj;
end;

end.
