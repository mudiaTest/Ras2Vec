unit Main_Obj;

interface

uses
  Sys_utl, Windows, Graphics, ExtCtrls, Classes, Math;

const
    c_fromLeft = 0;
    c_fromTop = 1;
    c_fromRight = 2;
    c_fromBottom = 3;

    c_goLeft = 0;
    c_goTop = 1;
    c_goRight = 2;
    c_goBottom = 3;
type

  TVectObj = class;
  TVectRectangle = class;

  TDynamicPointArray = array of array of TVectObj;
  TDynamicEdgeArray = array of TPoint;

  TOColor = class
    color: TColor;
  end;

  TOPointArr = class
    pointArr: array of TOPoint;
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
    procedure put(x, y: Integer; o: TVectObj); reintroduce;
    function get(x, y: Integer): TVectObj; reintroduce;
    procedure ReadFromImg(aimg: TImage);
    procedure FillImg(aimg: TImage; azoom: Integer; agrid: boolean; agridColor: TColor);
    property vObj[index: integer]: TVectObj read getObjById write setObjById;
  end;

  TVectGroup = class(TObject)
  private
    fedgeList: TIntList;
    frectList: TIntList;
    ftestColor: TColor;
    fedgePolyRawArr: array of TPoint;
  published
    property edgeList: TIntList read fedgeList write fedgeList;
    property rectList: TIntList read frectList write frectList;
    property testColor: TColor read ftestColor write ftestColor;
    //property edgePolyRawArr: TVarArray read fedgePolyRawArr write fedgePolyRawArr;
  public
    constructor Create; overload;
    function makeVectorEdge(vectArr: TDynamicPointArray): TDynamicPointArray;//(vectArr: TVarArray);
  end;

  //lista obiektów wektorowych
  TVectList = class(TIntList)
  private
    vectArr: TDynamicPointArray;
    fsrcWidth: Integer;
    fsrcHeight: Integer;
    function getObjById(index: Integer): TVectObj;
    procedure setObjById(index: Integer; avectObj: TVectObj);
    procedure mekeEdges(avectGroup: TVectGroup);
    function getNextEdge(aprevEdge: TVectRectangle; var arrDir: Integer): TVectRectangle;
    function checkRight(aprevEdge: TVectRectangle): TVectRectangle;
    function checkTop(aprevEdge: TVectRectangle): TVectRectangle;
    function checkBottom(aprevEdge: TVectRectangle): TVectRectangle;
    function checkLeft(aprevEdge: TVectRectangle): TVectRectangle;
    function checkNextEdge(aprevEdge: TVectRectangle;
      arrDir: integer): TVectRectangle;
  published
    property srcWidth: Integer read fsrcWidth write fsrcWidth;
    property srcHeight: Integer read fsrcHeight write fsrcHeight;
  public
    procedure ReadFromImg(aimg: TImage);
    function FillImg(aimg: TImage; azoom: Integer; atestColor: Boolean;
                     agrid: boolean; agridColor: TColor): TBitmap;
    function FillImgWithPolygons(aimg: TImage; azoom: Integer; atestColor,
                     agrid: boolean; agridColor: TColor): TBitmap;
    property vObj[index: integer]: TVectObj read getObjById write setObjById;
    procedure groupRect;
    procedure joinRect;
    constructor Create;
  end;

  //podstawowy obekt wektorowy
  TVectObj = class
  protected
    fpoints: TIntList;
    fcolor: TColor;
    fvectGroup: TVectGroup;
    fvectGroupId: Integer;
    fvectArr: array of array of TVectObj;
    procedure dopiszGrupe(agroupList: TVectGroup; avectList: TVectList);
    function getVectObj(x, y: integer): TVectObj; virtual;
  published
    property color: TColor read fcolor write fcolor;
    property vectGroup: TVectGroup read fvectGroup write fvectGroup;
    property vectGroupId: Integer read fvectGroupId write fvectGroupId;
  public
    constructor Create; reintroduce;
    property vectArr[x, y: integer]: TVectObj read getVectObj;
  end;

  //obiekt wektorowego Rectangle
  TVectRectangle = class (TVectObj)
  private
    function getP(lp: Integer): TOPoint;
    function getP1: TOPoint;
    function getP2: TOPoint;
  protected
    function getVectObj(x, y: integer): TVectObj; override;
  public
    constructor Create(acolor: TColor; p1, p2: TOPoint); overload;
    property p1: TOPoint read getP1;
    property p2: TOPoint read getP2;
    procedure zintegruj(avectObj: TVectRectangle; avectList: TVectList);
  end;

implementation

uses
  SysUtils;

{ TVectList }

constructor TVectList.Create;
begin
  inherited;
end;

function TVectList.FillImg(aimg: TImage; azoom: Integer; atestColor: Boolean;
                           agrid: boolean; agridColor: TColor): TBitmap;
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
        if not atestColor then
          Brush.Color := vectObj.color
        else
          Brush.Color := vectObj.vectGroup.testColor;
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

function TVectList.FillImgWithPolygons(aimg: TImage; azoom: Integer; atestColor: Boolean;
                           agrid: boolean; agridColor: TColor): TBitmap;
var
  x, y: Integer;
  i, j: Integer;
  vectObj: TVectRectangle;
  p: Pointer;
  bmp: TBitmap;
  vectGroup: TVectGroup;
  pointArr: array of TPoint;
  tmpPoint: TPoint;
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

  if Count > 0 then
//  vectobj.vectGroupId

  with bmp.Canvas do
  begin
    for i:=0 to Count-1 do
    begin
      vectGroup := Objects[i] as TVectGroup;
      SetLength(pointArr, vectGroup.edgeList.Count);

      if not atestColor then
        Brush.Color := vectGroup.testColor
      else
        Brush.Color := vectGroup.testColor;
      brush.Style := bsSolid;

      pointArr := vectGroup.makeVectorEdge(self.vectArr);

      {for j:=0 to vectGroup.edgeList.Count-1 do
      begin
        vectObj := vectGroup.edgeList.objects[j] as TVectRectangle;
        tmpPoint := Point((vectObj.p2.X)*azoom, (vectObj.p2.Y+1)*azoom);
        pointArr[j] := tmpPoint;
      end; }

      Polygon(pointArr);
    end;

  end;

  Result := bmp;
  aimg.Canvas.Unlock;
end;

function TVectList.getObjById(index: Integer): TVectObj;
begin
  result := Objects[index] as TVectObj;
end;

procedure TVectList.groupRect;
var
  x, y: Integer;
  vectObj: TVectRectangle;
  key: integer;
begin
for y:=0 to srcHeight-1 do
  for x:=0 to srcWidth-1 do
  begin
    vectObj := vectArr[x, y] as TVectRectangle;
    if vectObj.vectGroup = nil then
    begin
      vectObj.vectGroup := TVectGroup.Create;
      //vectObj.vectGroup.testColor := TColo;
      vectObj.vectGroup.testColor := Math.RandomRange(0, 99999);
      vectObj.vectGroup.rectList.AddObject(0, vectObj);
      vectObj.vectGroup.edgeList.AddObject(0, vectObj);
      key := nextKey;
      addObject(key, vectObj.vectGroup);
      vectObj.vectGroupId := key;
    end;
    if x < srcWidth-1 then
      vectObj.zintegruj(vectArr[x+1, y] as TVectRectangle, self);
    if y < srcHeight-1 then
      vectObj.zintegruj(vectArr[x, y+1] as TVectRectangle, self);
  end;
end;

procedure TVectList.joinRect;
var
  i: Integer;
  vectGroup: TVectGroup;
begin
  for i:=0 to Count-1 do
  begin
    vectGroup := Objects[i] as TVectGroup;
    mekeEdges(vectGroup);
  end;
end;

procedure TVectList.mekeEdges(avectGroup: TVectGroup);
var
  edgeStart, nextEdge, prevEdge: TVectRectangle;
  arrivDir: integer;
begin
  edgeStart := avectGroup.rectList.Objects[0] as TVectRectangle;
  prevEdge := edgeStart;
  arrivDir := c_fromLeft;
  while nextEdge <> edgeStart do
  begin
    nextEdge := getNextEdge(prevEdge, arrivDir);
    if nextEdge = nil then
    begin
      Assert(avectGroup.edgeList.Count = 1, 'Oddany edge jest nil (' + IntToStr(prevEdge.P1.x) +
                            ',' + IntToStr(prevEdge.p1.y) + '), liczba znalezionych kreawêdzi:' +
                            IntToStr(avectGroup.edgeList.Count));
      Break;
    end;
    avectGroup.edgeList.AddObject(avectGroup.edgeList.nextKey, nextEdge);
    prevEdge := nextEdge;
  end;

end;

function TVectList.getNextEdge(aprevEdge: TVectRectangle; var arrDir: Integer): TVectRectangle;
var
  i, j: Integer;
begin
  i := 0;
  j := 0;
  for i := 0 to 3 do
  begin
    if arrDir + j = 4 then
      j := -arrDir;
    Result := checkNextEdge(aprevEdge, arrDir+j);
    if Result <> nil then
    begin
      if arrDir + j = c_fromLeft then
        arrDir := c_fromBottom
      else if arrDir + j = c_fromBottom then
        arrDir := c_fromRight
      else if arrDir + j = c_fromTop then
        arrDir := c_fromLeft
      else if arrDir + j = c_fromRight then
        arrDir:= c_fromTop;
      Break;
    end;
    Inc(j);
  end;
end;

function TVectList.checkNextEdge(aprevEdge: TVectRectangle; arrDir: integer): TVectRectangle;
begin
  if arrDir = c_fromLeft then
    Result := checkTop(aprevEdge)
  else if arrDir = c_fromTop then
    Result := checkRight(aprevEdge)
  else if arrDir = c_fromRight then
    Result := checkBottom(aprevEdge)
  else if arrDir = c_fromBottom then
    Result := checkLeft(aprevEdge);
end;

function TVectList.checkTop(aprevEdge: TVectRectangle): TVectRectangle;
begin
  if aprevEdge.getP1.y > 0 then
  begin
    Result := vectArr[aprevEdge.getP1.x, aprevEdge.getP1.y-1] as TVectRectangle;
    if aprevEdge.vectGroupId = Result.vectGroupId then
      Exit;
    Result := nil;
  end;
  Result := nil;
end;

function TVectList.checkRight(aprevEdge: TVectRectangle): TVectRectangle;
begin
  if aprevEdge.getP1.x < srcWidth-1 then
  begin
    Result := vectArr[aprevEdge.getP1.x+1, aprevEdge.getP1.y] as TVectRectangle;
    if aprevEdge.vectGroupId = Result.vectGroupId then
      Exit;
    Result := nil;
  end;
  Result := nil;
end;

function TVectList.checkBottom(aprevEdge: TVectRectangle): TVectRectangle;
begin
  if aprevEdge.getP1.y < srcHeight-1 then
  begin
    Result := vectArr[aprevEdge.getP1.x, aprevEdge.getP1.y+1] as TVectRectangle;
    if aprevEdge.vectGroupId = Result.vectGroupId then
      Exit;
    Result := nil;
  end;
  Result := nil;
end;

function TVectList.checkLeft(aprevEdge: TVectRectangle): TVectRectangle;
begin
  if aprevEdge.getP1.x > 0 then
  begin
    Result := vectArr[aprevEdge.getP1.x-1, aprevEdge.getP1.y] as TVectRectangle;
    if aprevEdge.vectGroupId = Result.vectGroupId then
      Exit;
    Result := nil;
  end;
  Result := nil;
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

function TVectRectangle.getVectObj(x, y: integer): TVectObj;
begin
  Result := inherited getVectObj(x, y) as TVectRectangle
end;

procedure TVectRectangle.zintegruj(avectObj: TVectRectangle; avectList: TVectList);
begin
  //jeœli s¹siad jest niezintegrowany i ma taki sam kolor
  if (avectObj.vectGroup = nil) and (avectObj.color = color) then
  //dodajemy do grupy obj na którym jesteœmy
  begin
    avectObj.vectGroup := vectGroup;
    avectObj.vectGroupId := vectGroupId;
    vectGroup.rectList.AddObject(vectGroup.rectList.Count, avectObj);
  end else
  if (avectObj.vectGroup <> vectGroup) and (avectObj.color = color) then
    dopiszGrupe(avectObj.vectGroup, avectList);
end;

{ TVectorObj }

constructor TVectObj.Create;
begin
  fpoints := TIntList.Create;
  vectGroup := nil;
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

procedure TVectObj.dopiszGrupe(agroupList: TVectGroup; avectList: TVectList);
var
  I: Integer;
  delIdx: Integer;
  vectObj: TVectObj;
begin
  delIdx := (agroupList.rectList.Objects[0] as TVectObj).vectGroupId;
  for i:=0 to agroupList.rectList.Count-1 do
  begin
    vectObj := agroupList.rectList.Objects[i] as TVectObj;
    vectObj.vectGroup := vectGroup;
    vectObj.vectGroupId := vectGroupId;
    vectGroup.rectList.AddObject(vectGroup.rectList.nextKey, vectObj);
  end;
  agroupList.Free;
  assert(avectList.indexOf(delIdx) >= 0, 'Brak grupy do usuniêcia.');
  avectList.delete(avectList.indexOf(delIdx));
end;

function TVectObj.getVectObj(x, y: integer): TVectObj;
begin
  Result := fvectArr[x, y] as TVectObj;
end;

{ TVectGroup }

constructor TVectGroup.Create;
begin
  inherited;
  fedgeList := TIntList.Create;
  frectList := TIntList.Create;
end;

function TVectGroup.makeVectorEdge(vectArr: TDynamicPointArray): TDynamicPointArray;
begin

end;

end.
