unit Main_Obj;

interface

uses
  Sys_utl, Windows, Graphics, ExtCtrls, Classes, Math, StdCtrls,
  Registry,
  OtlCommon,
  OtlComm,
  OtlSync,
  OtlTask,
  OtlTaskControl,
  OtlCollections,
  OtlParallel;

const
    c_fromLeft = 0;
    c_fromTop = 1;
    c_fromRight = 2;
    c_fromBottom = 3;

    c_goTop = 0;
    c_goRight = 1;
    c_goBottom = 2;
    c_goLeft = 3;

    progName = 'Ras2Vec';
type

  TVectObj = class;
  TVectRectangle = class;

  TDynamicPointArray = array of array of TVectObj;
  TDynamicEdgeArray = array of TPoint;

  TMPFile = class
  private
    reg: TRegistry;
    mpFile: TextFile;
    procedure AddMPFileHead;
    procedure AddLevels;
    procedure AddZoom;
    procedure AddPolygonFromIntList(aEdgeList: TIntList; aLevel: integer);
  public
    lineList: TStringList;
    path: String;
    name: String;
    mpFileName: String; //przechowywana w reg ¹cie¿ka i nazwa pliku MP
    procedure SavePathToReg(aPath: String);
    procedure LoadPathFromReg;
    procedure MPFileOpen;
    procedure MPFileClose;
    constructor Create; reintroduce; virtual;
    procedure MPFileSave;
    procedure ClearList;
  end;


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
    constructor Create; override;
    procedure put(x, y: Integer; o: TVectObj); reintroduce;
    function get(x, y: Integer): TVectObj; reintroduce;
    procedure ReadFromImg(aimg: TImage);
    procedure FillImgWithRect(aimg: TImage; azoom: Integer; agrid: boolean; agridColor: TColor);
    property vObj[index: integer]: TVectObj read getObjById write setObjById;
  end;

  //grupa obiektów wektorowych; zawiera:
  TVectGroup = class(TObject)
  private
    //cztery punkty geograficzne okreœlan¹ce rogi obrazka
    leftTopGeo, rightTopGeo, leftBottomGeo, rightBottomGeo: Double;
    //lista krawêdzi punktów-pixeli (kolejnych)
    fedgePxList: TIntList;
    //lista krawêdzi punktówGeograficznych (kolejnych)
    fedgeGeoList: TDoubleList;
    //lista krawêdzi punktów-pixeli (kolejnych), które zosta³y poddane uproszczaniu
    fsimpleEdgeList: TIntList;
    //lista 'kwadratów' nale¿¹cych do grupy
    frectList: TIntList;
    //kolor testowy - tym kolorem wype³niana jest grupa gdy w³¹czymy opcjê testu
    ftestColor: TColor;
    //kolor oryginalny
    fcolor: TColor;
    //lp utworzonej grupy. PóŸniej utworzona ma wy¿szy numer
    lpGrupa: integer;
    //zadajemy dwa KOLEJNE punkty poruszaj¹ce siê po liniach Hor i Ver
    //Dostajemy c_fromLeft, c_fromTop, c_fromRight, c_fromBottom
    function direction(p1, p2: TOpoint): integer;
    //tworzy krawêdŸ z 3 kolejnych punktów
    procedure makePartEdge(prvPoint, actPoint, nextPoint: TOPoint; var counter: integer; aarr: TDynamicEdgeArray; azoom: integer);
    //tworzy krawêdzie dla pojedynczego punktu
    procedure makePartEdge4OnePoint(aPoint: TOPoint;
                                    aarr: TDynamicEdgeArray;
                                    azoom: integer);
    procedure getLine(p1, p2: TOPoint; var A, C, mian: Double);
    procedure PxListToGeoList;
  published
    property edgePxList: TIntList read fedgePxList write fedgePxList;
    property edgeGeoList: TDoubleList read fedgeGeoList write fedgeGeoList;
    property simpleEdgeList: TIntList read fsimpleEdgeList write fsimpleEdgeList;
    property rectList: TIntList read frectList write frectList;
    property testColor: TColor read ftestColor write ftestColor;
    property color: TColor read fcolor write fcolor;
  public
    constructor Create; overload;
    destructor Destroy; override;
    //tworzy tablicê punktów z ponktów zawartych w edgePxList
    function makeVectorEdge(vectArr: TDynamicPointArray; azoom: integer): TDynamicEdgeArray;//(vectArr: TVarArray);
    function simplifyVectorEdge(arr: TDynamicEdgeArray): TDynamicEdgeArray;
  end;

  //lista obiektów wektorowych
  //obiekty wektorowe przechowywane s¹ w Objects
  TVectList = class(TIntList)
  private
    vectArr: TDynamicPointArray; //tablica z obektami wektorowymi
    fsrcWidth: Integer; //szerokoœæ wczytanego (ReadFromImg) obrazka
    fsrcHeight: Integer; //wysokoœæ wczytanego (ReadFromImg) obrazka
    function getObjById(index: Integer): TVectObj;
    procedure setObjById(index: Integer; avectObj: TVectObj);
    procedure InfoAkcja(aStr: String); virtual;
    procedure InfoTime(aStr: String); virtual;
  published
    property srcWidth: Integer read fsrcWidth write fsrcWidth;
    property srcHeight: Integer read fsrcHeight write fsrcHeight;
  protected
    inMod: Integer;
  public
    stMessage: String;
    stTime: String;
    //wype³nia vectArr obiektami TVectRectangle reprezentuj¹cymi poszczególne
    //pixele obrazka
    procedure ReadFromImg(aimg: TImage);
    //oddaje bitmapê - wype³nia obraz "rectanglami"
    function FillImgWithRect(aimg: TImage; azoom: Integer; atestColor: Boolean;
                     agrid: boolean; agridColor: TColor): TBitmap;
    //oddaje bitmapê - wype³nia obraz polygonami"
    function FillImgWithPolygons(aimg: TImage; azoom: Integer; atestColor,
                     agrid: boolean; agridColor: TColor): TBitmap;
    //dostêp do obiektów u¿ywaj¹c getObjById i setObjById
    property vObj[index: integer]: TVectObj read getObjById write setObjById;
    //tworzy grupy obiektów TVectRectangle czyli przysz³e polygony
    procedure groupRect;
    //dla wszystkich grup tworzone s¹ krawêdzie (mekeEdges)
    procedure makeEdgesForRect;
  end;

  TSeparateThreadVectList = class(TVectList)
  private
    fotWorker: TOmniWorker;
    procedure InfoAkcja(aStr: String); override;
    procedure InfoTime(aStr: String); override;
  published
    property otWorker: TOmniWorker read fotWorker write fotWorker;
  public
    constructor Create; override;
  end;

  TMainThreadVectList = class(TVectList)
  private
    procedure InfoAkcja(aStr: String); override;
    procedure InfoTime(aStr: String); override;
  public
    lblAkcja: TLabel;
    lblTime: TLabel;
    constructor Create; override;
  end;

  //podstawowy obekt wektorowy
  TVectObj = class
  protected
    fpoints: TIntList;
    //kolor wype³niaj¹cy obiekt
    fcolor: TColor;
    //odnoœnik do grupy, która posiada dany obiekt vektorowy
    fvectGroup: TVectGroup;
    //numer grupy (w liœcie grup), która posiada dany obiekt vektorowy
    fvectGroupId: Integer;
    //fvectArr: array of array of TVectObj;
    //przepisanie (do³¹czenie) obiektów z innej grupy do tej, która posiada obiekt self
    procedure dopiszGrupe(agroupList: TVectGroup; avectList: TVectList);
    function getP(lp: Integer): TOPoint;
    //function getVectObj(x, y: integer): TVectObj; virtual;
  published
    //kolor wype³niaj¹cy obiekt
    property color: TColor read fcolor write fcolor;
    //odnoœnik do grupy, która posiada dany obiekt vektorowy
    property vectGroup: TVectGroup read fvectGroup write fvectGroup;
    //numer grupy (w liœcie grup), która posiada dany obiekt vektorowy
    property vectGroupId: Integer read fvectGroupId write fvectGroupId;
  public
    constructor Create; reintroduce;
    //property vectArr[x, y: integer]: TVectObj read getVectObj;
    function distance(a, c: Integer; mian: Double): Double;
    function inDistance(a, c: Integer; mian, dst: Double): Boolean;
  end;

  //obiekt wektorowego Rectangle
  TVectRectangle = class (TVectObj)
  private
    function getP1: TOPoint;
    function getP2: TOPoint;
  protected
    //function getVectObj(x, y: integer): TVectObj; override;
  public
    constructor Create(acolor: TColor; p1, p2: TOPoint); overload;
    property p1: TOPoint read getP1;
    property p2: TOPoint read getP2;
    class procedure zintegruj(aobj1, aobj2: TVectRectangle; avectList: TVectList);
  end;

implementation

uses
  SysUtils, Main_Thread, Sys_Const;

{ TVectList }

function TVectList.FillImgWithRect(aimg: TImage; azoom: Integer; atestColor: Boolean;
                           agrid: boolean; agridColor: TColor): TBitmap;
var
  x, y: Integer;
  i: Integer;
  vectObj: TVectRectangle;
  p: Pointer;
  bmp: TBitmap;
begin
  //ustalamy wielkoœæ obrazu do wype³nienia bior¹c pod uwagê, ¿e jest zoom
  //obrazek bêdzie sk³ada³ siê z grup pixeli u³o¿onych w kwadraty udaj¹cych
  //"du¿e" pixele
  aimg.Width := srcWidth * azoom;
  aimg.Height := srcHeight * azoom;
  bmp := TBitmap.Create;
  bmp.Width := srcWidth * azoom;
  bmp.Height := srcHeight * azoom;
  //zamykamy p³ótno
  aimg.Canvas.Lock;

  //jeœli ma byæ malowana siatka/grid
  if agrid then
    bmp.Canvas.Pen.Color := agridColor;
  bmp.Canvas.Pen.Style := psSolid;
  {jeœli rectangle ma mieæ np. 10x10 to œrodkowe osiem bêdzie wype³nione z bmp.Canvas.Brush.Color
   a ramka bêdzie bmp.Canvas.Pen.Style i bmp.Canvas.Pen.Color}
   {rectangle robi ramkê - musi byæ conajmniej 2x2 - przy zoomie ponizej x4 nic nie widac
    fillrect wype³nia kolorem - mo¿e byæ 1x1
    Bounds}
  //wype³niamy bitmapê
  for y:=0 to srcHeight-1 do
    for x:=0 to srcWidth-1 do
    begin
      vectObj := vectArr[x, y] as TVectRectangle;
      with bmp.Canvas do
      begin
        if not atestColor then
          Brush.Color := vectObj.color
        else
          Brush.Color := vectObj.vectGroup.testColor;
        //jeœli ma byæ grid
        if agrid then
          Rectangle(Rect((vectObj.p1.X)*azoom,   (vectObj.p1.Y)*azoom,
                         (vectObj.p2.X+1)*azoom, (vectObj.p2.Y+1)*azoom))
        //jeœli rysujemy bez grida
        else
          FillRect(Rect((vectObj.p1.X)*azoom,   (vectObj.p1.Y)*azoom,
                        (vectObj.p2.X+1)*azoom, (vectObj.p2.Y+1)*azoom));
      end;
    end;
  Result := bmp;
  //otwieramy p³ótno
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
  pointArr: TDynamicEdgeArray; //lista punktów do przekazania, aby stworzyæ polygon
  tmpPoint: TPoint;
  doit: boolean;
begin
  //ustalamy wielkoœæ obrazu do wype³nienia bior¹c pod uwagê, ¿e jest zoom
  //obrazek bêdzie sk³ada³ siê z grup pixeli u³o¿onych w kwadraty udaj¹cych
  //"du¿e" pixele
  aimg.Width := srcWidth * azoom;
  aimg.Height := srcHeight * azoom;
  bmp := TBitmap.Create;
  bmp.Width := srcWidth * azoom;
  bmp.Height := srcHeight * azoom;
  //zamykamy p³ótno
  aimg.Canvas.Lock;

 if agrid then
 begin
    bmp.Canvas.Pen.Color := agridColor;
    bmp.Canvas.Pen.Style := psSolid;
 end
 else
   bmp.Canvas.Pen.Style := psClear;


  {//solid - jednolity kolor
  bmp.Canvas.Pen.Style := psSolid;
  //blead - brak koloru
  bmp.Canvas.Pen.Style := psSolid;}

  if Count > 0 then
//  vectobj.vectGroupId

  with bmp.Canvas do
  begin
    for i:=0 to Count-1 do
    begin
      vectGroup := Objects[i] as TVectGroup;
      SetLength(pointArr, vectGroup.edgePxList.Count*3);

      if not atestColor then
        Brush.Color := vectGroup.color
      else
        Brush.Color := vectGroup.testColor;
      brush.Style := bsSolid;

      pointArr := vectGroup.makeVectorEdge(self.vectArr, azoom);

      {for j:=0 to vectGroup.edgePxList.Count-1 do
      begin
        vectObj := vectGroup.edgePxList.objects[j] as TVectRectangle;
        tmpPoint := Point((vectObj.p2.X)*azoom, (vectObj.p2.Y+1)*azoom);
        pointArr[j] := tmpPoint;
      end;}
      doit := true;
      if doit then
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
  lpGrupa: integer;
  perf: TTimeInterval;
  perf2, perf3: TTimeInterval;
begin
  perf := TTimeInterval.Create;
  perf2 := TTimeInterval.Create;
  perf3 := TTimeInterval.Create;
  Clear;
  lpGrupa := 0;
  for y:=0 to srcHeight-1 do
  begin
    perf.Start;
    for x:=0 to srcWidth-1 do
    begin

      vectObj := vectArr[x, y] as TVectRectangle;
      perf2.Start(false);
      if vectObj.vectGroup = nil then
      begin
        vectObj.vectGroup := TVectGroup.Create;
        vectObj.vectGroup.lpGrupa := lpGrupa;
        inc(lpGrupa);
        //vectObj.vectGroup.testColor := TColo;
        vectObj.vectGroup.testColor := Math.RandomRange(0, 99999);
        vectObj.vectGroup.color := vectObj.color;
        vectObj.vectGroup.rectList.AddObject(0, vectObj);
        vectObj.vectGroup.edgePxList.AddObject(0, vectObj);
        key := nextKey;
        addObject(key, vectObj.vectGroup);
        vectObj.vectGroupId := key;
      end;
      perf2.Stop;
      perf3.Start(false);
      if x < srcWidth-1 then
        TVectRectangle.zintegruj(vectObj, vectArr[x+1, y] as TVectRectangle, self);
      if y < srcHeight-1 then
        TVectRectangle.zintegruj(vectObj, vectArr[x, y+1] as TVectRectangle, self);
      perf3.Stop;
    end;
    perf.Stop;
    InfoAkcja('Grupowanie pixeli - linia:' + IntToStr(y) + '/' + IntToStr(srcHeight-1));
    InfoTime('Time: ' + perf.InterSt + ' / ' + perf2.InterSt + ' / ' + perf3.InterSt);
    perf2.Reset;
    perf3.Reset;
  end;
end;

procedure TVectList.InfoAkcja(aStr: String);
begin
  stMessage := aStr;
end;

procedure TVectList.InfoTime(aStr: String);
begin
  stTime := aStr;
end;

procedure TVectList.makeEdgesForRect;
  //buduje (wype³nia odpowiednie listy) krawêdz dla podanej grupy
  procedure mekeEdges(avectGroup: TVectGroup);

    function nextDirection(aDirection: Integer): Integer;
    var
      res, reminder: Word;
    begin
      inc(aDirection);
      DivMod(aDirection, 4, res, reminder);
      result := reminder;
    end;

    function CheckBottomPX(astartEdgePoint: TVectRectangle): boolean;
    var
      bottomVectorRectangle: TVectObj;
    begin
      result := false;
      if astartEdgePoint.p1.y < srcHeight-1 then
      try
        bottomVectorRectangle := vectArr[astartEdgePoint.p1.x, astartEdgePoint.p1.y+1];
        result := (bottomVectorRectangle <> nil) and (bottomVectorRectangle.vectGroupId = astartEdgePoint.vectGroupId);
      except
        raise;
      end;
    end;

    function getNextEdge(aprevEdge: TVectRectangle; var arrDir: Integer): TVectRectangle;
      function checkNextEdge(aprevEdge: TVectRectangle; arrDir: integer): TVectRectangle;
        function checkTop(aprevEdge: TVectRectangle): TVectRectangle;
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
        function checkRight(aprevEdge: TVectRectangle): TVectRectangle;
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
        function checkBottom(aprevEdge: TVectRectangle): TVectRectangle;
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
        function checkLeft(aprevEdge: TVectRectangle): TVectRectangle;
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
      begin
        if arrDir = c_goTop then
          Result := checkTop(aprevEdge)
        else if arrDir = c_goRight then
          Result := checkRight(aprevEdge)
        else if arrDir = c_goBottom then
          Result := checkBottom(aprevEdge)
        else if arrDir = c_goLeft then
          Result := checkLeft(aprevEdge);
      end;
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
  var
    startEdgePoint, nextEdgePoint, prevEdgePoint: TVectRectangle;
    arrivDir: integer;
  begin
    avectGroup.edgePxList.Clear;
    //startEdgePoint to pierwszy ponkt na liœcie, bo idziemy ol lewej strony
    //w najwy¿szym wierszu
    startEdgePoint := avectGroup.rectList.Objects[0] as TVectRectangle;
    prevEdgePoint := startEdgePoint;
    arrivDir := c_goRight; //zaczynamy od max lewego ponktu na górnej linji
                           //Ka¿emy zacz¹æ szukanie od prawej
    nextEdgePoint := nil;
    //1-pixelowwy obiekt traktujemy inaczej
    if avectGroup.rectList.count <> 1 then
      //koñczymy jeœli trafiamy na pocz¹tek, lub na 1-pixelowy obiekt
      //while (nextEdgePoint <> startEdgePoint) and (prevEdgePoint <> nil) do
      while true do
      begin
        if nextEdgePoint = startEdgePoint then
        begin
          if CheckBottomPX(startEdgePoint) and (arrivDir = c_fromRight) then
          begin
            arrivDir := c_goBottom
          end
          else
          begin
            //arrivDir := -1;
            Exit;
          end;

        end;
        //try
        nextEdgePoint := getNextEdge(prevEdgePoint, arrivDir);
        //powstanie gdy nie mo¿emy oddaæ nastêpnej krawêdzi, ale wyj¹tkikem jest gdy jest to pojedynczy pixel
        if (nextEdgePoint = nil) and (avectGroup.edgePxList.Count <> 0) then
          Assert(false, 'Oddany edge jest nil (' + IntToStr(prevEdgePoint.P1.x) +
                                  ',' + IntToStr(prevEdgePoint.p1.y) + '), liczba znalezionych kreawêdzi:' +
                                  IntToStr(avectGroup.edgePxList.Count));

        avectGroup.edgePxList.AddObject(avectGroup.edgePxList.nextKey, nextEdgePoint);
        prevEdgePoint := nextEdgePoint;

        //except
        //  on E: Exception do
        //    raise;
        //end;


      end
    //dla obiektu 1-pixelowego
    else
      avectGroup.edgePxList.AddObject(avectGroup.edgePxList.nextKey, startEdgePoint);
  end;
var
  i: Integer;
  vectGroup: TVectGroup;
  wrRes, wrDiv: Word;
begin
  for i:=0 to Count-1 do
  begin
    vectGroup := Objects[i] as TVectGroup;
    DivMod(i, inMod, wrRes, wrDiv);
    if wrDiv = 0 then
      InfoAkcja('Tworzenie granicy dla grupy ' + IntToStr(i) + '/' + IntToStr(Count-1) );
    mekeEdges(vectGroup);
  end;
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

function TVectRectangle.getP1: TOPoint;
begin
  Result := getP(0);
end;

function TVectRectangle.getP2: TOPoint;
begin
  Result := getP(1);
end;

{function TVectRectangle.getVectObj(x, y: integer): TVectObj;
begin
  Result := inherited getVectObj(x, y) as TVectRectangle
end;}

class procedure TVectRectangle.zintegruj(aobj1, aobj2: TVectRectangle; avectList: TVectList);
var
  obj1, obj2: TVectRectangle;
  delIdx: Integer;
begin
  if (aobj2.vectGroup = nil) or (aobj1.vectGroup.lpGrupa < aobj2.vectGroup.lpGrupa) then
  begin
    obj1 := aobj1;
    obj2 := aobj2;
  end
  else
  begin
    obj1 := aobj2;
    obj2 := aobj1;
  end;

  //jeœli s¹siad jest niezintegrowany i ma taki sam kolor
  if (obj2.vectGroup = nil) and (obj2.color = obj1.color) then
  //dodajemy do grupy obj na którym jesteœmy
  begin
    obj2.vectGroup := obj1.vectGroup;
    obj2.vectGroupId := obj1.vectGroupId;
    obj2.vectGroup.lpGrupa := obj1.vectGroup.lpGrupa;
    obj1.vectGroup.rectList.AddObject(obj1.vectGroup.rectList.Count, obj2);
  //jeœli s¹siad jest ma grupê, ale ta grupa ma takisam kolor, to
  end else
  if (obj2.vectGroup <> obj1.vectGroup) and (obj2.color = obj1.color) {and (obj2.vectGroup.rectList.Count < 1000)} then
  begin
    if obj1.vectGroup.rectList.Count > obj2.vectGroup.rectList.Count then
    begin
      delIdx := obj2.vectGroupId;
      obj1.dopiszGrupe(obj2.vectGroup, avectList);
    end
    else
    begin
      delIdx := obj1.vectGroupId;
      obj2.dopiszGrupe(obj1.vectGroup, avectList);
    end;
    //usuniêcie przepisanej grupy z listy grup
    assert(avectList.indexOf(delIdx) >= 0, 'Brak grupy do usuniêcia: ' + intToStr(delIdx) + '.');
    avectList.delete(avectList.indexOf(delIdx));
  end;
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
  inherited;
end;

procedure TVectByCoordList.FillImgWithRect(aimg: TImage; azoom: Integer; agrid: boolean;
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

function TVectObj.distance(a, c: Integer; mian: Double): Double;
var
  p: TOPoint;
begin
  p := getP(0);
  Result := Abs(a*p.x - p.y + c) / mian;
end;

procedure TVectObj.dopiszGrupe(agroupList: TVectGroup; avectList: TVectList);
var
  i: Integer;
  vectObj: TVectObj;
begin
  for i:=0 to agroupList.rectList.Count-1 do
  begin
    vectObj := agroupList.rectList.Objects[i] as TVectObj;
    vectObj.vectGroup := vectGroup;
    vectObj.vectGroupId := vectGroupId;
    vectGroup.rectList.AddObject(vectGroup.rectList.nextKey, vectObj);
  end;
end;

function TVectObj.getP(lp: Integer): TOPoint;
begin
  Result := fpoints.Objects[lp] as TOpoint;
end;

function TVectObj.inDistance(a, c: Integer; mian, dst: Double): Boolean;
begin
  Result := True;
  if distance(a, c, mian) > dst then
    Result := False;
end;

{function TVectObj.getVectObj(x, y: integer): TVectObj;
begin
  Result := fvectArr[x, y] as TVectObj;
end;}

{ TVectGroup }

constructor TVectGroup.Create;
begin
  inherited;
  fedgePXList := TIntList.Create;
  frectList := TIntList.Create;
end;

destructor TVectGroup.Destroy;
begin
  beep;
  inherited;
end;

function TVectGroup.direction(p1, p2: TOpoint): integer;
begin
  if p1.X > p2.X then
    result := c_fromRight
  else if p1.X < p2.X then
    result := c_fromLeft
  else if p1.Y > p2.Y then
    result := c_fromBottom
  else
    result := c_fromTop;
end;

procedure TVectGroup.getLine(p1, p2: TOPoint; var A, C, mian: Double);
  function getLineA(p1, p2: TOpoint): Double;
  begin
    Result := (p2.y - p1.y) / (p2.x - p1.x);
  end;
  function getLineC(p1: TOpoint; A: Double): Double;
  begin
    Result := p1.y - A*p1.x;
  end;
  function getMianownik(A: Double): Double;
  begin
    Result := Sqrt(Sqr(A) + 1);
  end;
begin
  A := getLineA(p1, p2);
  C := getLineC(p1, A);
  mian := getMianownik(A);
end;

function TVectGroup.makeVectorEdge(vectArr: TDynamicPointArray; azoom: integer): TDynamicEdgeArray;
var
  i: integer;
  o1, o2, o3: TOPoint;
  counter: integer;
begin
  counter := 0;

  if edgePxList.Count > 1 then
  begin
    //SetLength(result, self.rectList.Count+30);
    o1 := (edgePxList.Objects[edgePxList.Count-1] as TVectObj).getP(0);
    o2 := (edgePxList.Objects[0] as TVectObj).getP(0);
    o3 := (edgePxList.Objects[1] as TVectObj).getP(0);
    makePartEdge(o1, o2, o3, counter, result, azoom);

    for i:=1 to edgePxList.Count-2 do
    begin
      o1 := (edgePxList.Objects[i-1] as TVectObj).getP(0);
      o2 := (edgePxList.Objects[i] as TVectObj).getP(0);
      o3 := (edgePxList.Objects[i+1] as TVectObj).getP(0);
      makePartEdge(o1, o2, o3, counter, result, azoom);
    end;

    o1 := (edgePxList.Objects[edgePxList.Count-2] as TVectObj).getP(0);
    o2 := (edgePxList.Objects[edgePxList.Count-1] as TVectObj).getP(0);
    o3 := (edgePxList.Objects[0] as TVectObj).getP(0);
    makePartEdge(o1, o2, o3, counter, result, azoom);
    //SetLength(result, Min(counter,10));
    SetLength(result, counter);
  end else
  begin
    SetLength(result, 4);
    makePartEdge4OnePoint((edgePxList.Objects[0] as TVectObj).getP(0),
                          result, azoom);
    counter := 4;
  end;
end;

procedure TVectGroup.PxListToGeoList;
begin
  {!!}
end;

function TVectGroup.simplifyVectorEdge(
  arr: TDynamicEdgeArray): TDynamicEdgeArray;
begin
  //to do
end;

procedure TVectGroup.makePartEdge(prvPoint, actPoint, nextPoint: TOPoint; var counter: integer; aarr: TDynamicEdgeArray; azoom: integer);
begin
  if direction(prvPoint, actPoint) = c_fromLeft then
  begin
    if direction(actPoint, nextPoint) = c_fromBottom then
    begin
      //nic
    end
    else if direction(actPoint, nextPoint) = c_fromTop then
    begin
      aarr[counter] := Point(actPoint.x*azoom, actPoint.y*azoom);
      aarr[counter+1] := Point((actPoint.x+1)*azoom, actPoint.y*azoom);
      counter := counter + 2;
    end
    else if direction(actPoint, nextPoint) = c_fromLeft then
    begin
      aarr[counter] := Point((actPoint.x)*azoom, (actPoint.y)*azoom);
      counter := counter + 1;
    end
    else
    begin
      aarr[counter] := Point((actPoint.x)*azoom, (actPoint.y)*azoom);
      aarr[counter+1] := Point((actPoint.x+1)*azoom, actPoint.y*azoom);
      aarr[counter+2] := Point((actPoint.x+1)*azoom, (actPoint.y+1)*azoom);
      counter := counter + 3;
    end;
  end

  else if direction(prvPoint, actPoint) = c_fromRight then
  begin
    if direction(actPoint, nextPoint) = c_fromBottom then
    begin
      aarr[counter] := Point((actPoint.x+1)*azoom, (actPoint.y+1)*azoom);
      aarr[counter+1] := Point(actPoint.x*azoom, (actPoint.y+1)*azoom);
      counter := counter + 2;
    end
    else if direction(actPoint, nextPoint) = c_fromTop then
    begin
      //nic
    end
    else if direction(actPoint, nextPoint) = c_fromRight then
    begin
      aarr[counter] := Point((actPoint.x + 1)*azoom, (actPoint.y + 1)*azoom);
      counter := counter + 1;
    end
    else
    begin
      aarr[counter] := Point((actPoint.x+1)*azoom, (actPoint.y+1)*azoom);
      aarr[counter+1] := Point(actPoint.x*azoom, (actPoint.y+1)*azoom);
      aarr[counter+2] := Point(actPoint.x*azoom, (actPoint.y)*azoom);
      counter := counter + 3;
    end;
  end

  else if direction(prvPoint, actPoint) = c_fromTop then
  begin
    if direction(actPoint, nextPoint) = c_fromLeft then
    begin
      //nic
    end
    else if direction(actPoint, nextPoint) = c_fromRight then
    begin
      aarr[counter] := Point((actPoint.x+1)*azoom, actPoint.y*azoom);
      aarr[counter+1] := Point((actPoint.x+1)*azoom, (actPoint.y+1)*azoom);
      counter := counter + 2;
    end
    else if direction(actPoint, nextPoint) = c_fromTop then
    begin
      aarr[counter] := Point((actPoint.x+1)*azoom, actPoint.y*azoom);
      counter := counter + 1;
    end
    else
    begin
      aarr[counter] := Point((actPoint.x+1)*azoom, actPoint.y*azoom);
      aarr[counter+1] := Point((actPoint.x+1)*azoom, (actPoint.y+1)*azoom);
      aarr[counter+2] := Point(actPoint.x*azoom, (actPoint.y+1)*azoom);
      counter := counter + 3;
    end;
  end

  else //from bottom
  begin
    if direction(actPoint, nextPoint) = c_fromLeft then
    begin
      aarr[counter] := Point(actPoint.x*azoom, (actPoint.y+1)*azoom);
      aarr[counter+1] := Point(actPoint.x*azoom, actPoint.y*azoom);
      counter := counter + 2;
    end
    else if direction(actPoint, nextPoint) = c_fromRight then
    begin
      //nic
    end
    else if direction(actPoint, nextPoint) = c_fromBottom then
    begin
      aarr[counter] := Point(actPoint.x*azoom, (actPoint.y+1)*azoom);
      counter := counter + 1;
    end
    else
    begin
      aarr[counter] := Point(actPoint.x*azoom, (actPoint.y+1)*azoom);
      aarr[counter+1] := Point(actPoint.x*azoom, actPoint.y*azoom);
      aarr[counter+2] := Point((actPoint.x+1)*azoom, actPoint.y*azoom);
      counter := counter + 3;
    end;
  end
end;

{procedure TVectGroup.makePartEdge(prvPoint, actPoint, nextPoint: TOPoint; var counter: integer; aarr: TDynamicEdgeArray; azoom: integer);
begin
  if direction(prvPoint, actPoint) = c_fromLeft then
  begin
    if direction(actPoint, nextPoint) = c_fromBottom then
    begin
      //nic
    end
    else if direction(actPoint, nextPoint) = c_fromTop then
    begin
      aarr[counter] := Point(actPoint.x*azoom, actPoint.y*azoom);
      aarr[counter+1] := Point((actPoint.x+1)*azoom, actPoint.y*azoom);
      counter := counter + 2;
    end
    else if direction(actPoint, nextPoint) = c_fromLeft then
    begin
      aarr[counter] := Point((actPoint.x)*azoom, (actPoint.y)*azoom);
      counter := counter + 1;
    end
    else
    begin
      aarr[counter] := Point((actPoint.x)*azoom, (actPoint.y)*azoom);
      aarr[counter+1] := Point((actPoint.x+1)*azoom, actPoint.y*azoom);
      aarr[counter+2] := Point((actPoint.x+1)*azoom, (actPoint.y+1)*azoom);
      counter := counter + 3;
    end;
  end

  else if direction(prvPoint, actPoint) = c_fromRight then
  begin
    if direction(actPoint, nextPoint) = c_fromBottom then
    begin
      aarr[counter] := Point((actPoint.x+1)*azoom, (actPoint.y+1)*azoom);
      aarr[counter+1] := Point(actPoint.x*azoom, (actPoint.y+1)*azoom);
      counter := counter + 2;
    end
    else if direction(actPoint, nextPoint) = c_fromTop then
    begin
      //nic
    end
    else if direction(actPoint, nextPoint) = c_fromRight then
    begin
      aarr[counter] := Point((actPoint.x + 1)*azoom, (actPoint.y + 1)*azoom);
      counter := counter + 1;
    end
    else
    begin
      aarr[counter] := Point((actPoint.x+1)*azoom, (actPoint.y+1)*azoom);
      aarr[counter+1] := Point(actPoint.x*azoom, (actPoint.y+1)*azoom);
      aarr[counter+2] := Point(actPoint.x*azoom, (actPoint.y)*azoom);
      counter := counter + 3;
    end;
  end

  else if direction(prvPoint, actPoint) = c_fromTop then
  begin
    if direction(actPoint, nextPoint) = c_fromLeft then
    begin
      //nic
    end
    else if direction(actPoint, nextPoint) = c_fromRight then
    begin
      aarr[counter] := Point((actPoint.x+1)*azoom, actPoint.y*azoom);
      aarr[counter+1] := Point((actPoint.x+1)*azoom, (actPoint.y+1)*azoom);
      counter := counter + 2;
    end
    else if direction(actPoint, nextPoint) = c_fromTop then
    begin
      aarr[counter] := Point((actPoint.x+1)*azoom, actPoint.y*azoom);
      counter := counter + 1;
    end
    else
    begin
      aarr[counter] := Point((actPoint.x+1)*azoom, actPoint.y*azoom);
      aarr[counter+1] := Point((actPoint.x+1)*azoom, (actPoint.y+1)*azoom);
      aarr[counter+2] := Point(actPoint.x*azoom, (actPoint.y+1)*azoom);
      counter := counter + 3;
    end;
  end

  else //from bottom
  begin
    if direction(actPoint, nextPoint) = c_fromLeft then
    begin
      aarr[counter] := Point(actPoint.x*azoom, (actPoint.y+1)*azoom);
      aarr[counter+1] := Point(actPoint.x*azoom, actPoint.y*azoom);
      counter := counter + 2;
    end
    else if direction(actPoint, nextPoint) = c_fromRight then
    begin
      //nic
    end
    else if direction(actPoint, nextPoint) = c_fromBottom then
    begin
      aarr[counter] := Point(actPoint.x*azoom, (actPoint.y+1)*azoom);
      counter := counter + 1;
    end
    else
    begin
      aarr[counter] := Point(actPoint.x*azoom, (actPoint.y+1)*azoom);
      aarr[counter+1] := Point(actPoint.x*azoom, actPoint.y*azoom);
      aarr[counter+2] := Point((actPoint.x+1)*azoom, actPoint.y*azoom);
      counter := counter + 3;
    end;
  end
end;}

procedure TVectGroup.makePartEdge4OnePoint(aPoint: TOPoint;
                                           aarr: TDynamicEdgeArray;
                                           azoom: integer);
begin
  aarr[0] := Point((aPoint.x)*azoom, (aPoint.y)*azoom);
  aarr[1] := Point((aPoint.x+1)*azoom, aPoint.y*azoom);
  aarr[2] := Point((aPoint.x+1)*azoom, (aPoint.y+1)*azoom);
  aarr[3] := Point((aPoint.x)*azoom, (aPoint.y+1)*azoom);
end;

{ TSeparateThreadVectList }

constructor TSeparateThreadVectList.Create;
begin
  otWorker := nil;
  inMod := 10;
end;

procedure TSeparateThreadVectList.InfoAkcja(aStr: String);
begin
  inherited;
  (otWorker as TR2VOmniWorker).OMSendMessage(aStr);
end;

procedure TSeparateThreadVectList.InfoTime(aStr: String);
begin
  //separate tread
  //(otWorker as TR2VOmniWorker).OMSendMessage(aStr);
end;

{ TMainThreadVectList }

constructor TMainThreadVectList.Create;
begin
  inherited;
  inMod := 1;
end;

procedure TMainThreadVectList.InfoAkcja(aStr: String);
begin
  lblAkcja.Caption := aStr;
  lblAkcja.Repaint;
  stMessage := aStr;
end;

procedure TMainThreadVectList.InfoTime(aStr: String);
begin
  lblTime.Caption := aStr;
  lblTime.Repaint;
  stTime := aStr;
end;

{ TMPFile }

procedure TMPFile.ClearList;
begin
  lineList.Clear;
end;

constructor TMPFile.Create;
begin
  reg := TRegistry.Create;
  mpFileName := '';
  lineList := TStringList.Create;
end;

procedure TMPFile.MPFileClose;
begin
  CloseFile(mpFile);
end;

procedure TMPFile.AddLevels;
begin
  with LineList do
  begin
    Add('Levels=8');
    Add('Level0=24');
    Add('Level1=23');
    Add('Level2=22');
    Add('Level3=21');
    Add('Level4=19');
    Add('Level5=15');
    Add('Level6=14');
    Add('Level7=13');
  end;
end;

procedure TMPFile.AddZoom;
begin
  with LineList do
  begin
    Add('Zoom0=0');
    Add('Zoom1=1');
    Add('Zoom2=2');
    Add('Zoom3=3');
    Add('Zoom4=4');
    Add('Zoom5=5');
    Add('Zoom6=6');
    Add('Zoom7=7');
  end;
end;

procedure TMPFile.AddMPFileHead;
begin
  with LineList do
  begin
    Add('[IMG ID]');
    Add('CodePage=1252');
    Add('LblCoding=9');
    Add('ID=70040001');
    Add('Name=Test');
    Add('Elevation=M');
    Add('Preprocess=F');
    Add('TreSize=511');
    Add('TreMargin=0.00000');
    Add('RgnLimit=127');
    Add('POIIndex=Y');
    Add('Copyright=Annonymus');
    AddLevels;
    AddZoom;
    Add('[END-IMG ID]');
    Add('[Countries]');
    Add('Country1=POLSKA~[0x1d]PL');
    Add('[END-Countries]');

    Add('[POLYLINE]');
    Add('Type=0x15');
    Add('Label=P czarny');
    Add('Data0=(54.59917,18.29001),(54.59999,18.29112),(54.60059,18.29421)');
    Add('[END]');
  end;
end;

procedure TMPFile.AddPolygonFromIntList(aEdgeList: TIntList; aLevel: integer);

  function getGdgeStr(aEdgeList: TIntList): String;
  var
    i: integer;

  begin
    result := '';
    {!!}
   // for i := 0 to aEdgeList.Count-1 do
   //   result := StrConcat(',', result, '('++','++')');
  end;

begin
  with lineList do
  begin
    Add('[POLYGON]');
    Add('Type=0x9');
    Add('Label=eee');
    Add('Data' + intToStr(aLevel) + getGdgeStr(aEdgeList));//'=(54.60100,18.29108),(54.60102,18.29284),(54.60042,18.29267),(54.60038,18.29103)
    Add('[END]');
  end;
end;

procedure TMPFile.MPFileOpen;
begin
  AssignFile(mpFile, mpFileName);
  ReWrite(mpFile);
end;

procedure TMPFile.MPFileSave;
begin
  //MPFileOpen;
  ClearList;
  AddMPFileHead;
  lineList.SaveToFile(mpFileName);
 // MPFileClose;
end;

procedure TMPFile.SavePathToReg(aPath: String);
begin
  mpFileName := aPath;
  reg.OpenKey(REGSOFT + progName, true);
  reg.WriteString('MPFilePathName', mpFileName);
  reg.CloseKey;
end;

procedure TMPFile.LoadPathFromReg;
begin
  reg.OpenKey(REGSOFT + progName, true);
  mpFileName := reg.ReadString('MPFilePathName');
  reg.CloseKey;
end;

end.
