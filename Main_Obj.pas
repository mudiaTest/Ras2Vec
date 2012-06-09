unit Main_Obj;

{$M+}

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
  OtlParallel,
  JclHashMaps,
  JclContainerIntf;

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
  TGeoPoint = class;
  TMapFactory = class;
  TVectRectGroup = class;
  TColorPx = class;

  TDynamicPointArray = array of array of TVectObj;
  TDynamicGeoPointArray = array of TGeoPoint;
  TDynamicPxPointArray = array of TPoint;
  TDynamicPxColorPointArray = array of array of TColorPx;

  TColorPx = class
  private
    fcolor: Integer;
    fcolorN: Integer;
    fcolorS: Integer;
    fcolorW: Integer;
    fcolorE: Integer;
    fgroup: Integer;
    fgroupN: Integer;
    fgroupS: Integer;
    fgroupW: Integer;
    fgroupE: Integer;
    fborderNS: boolean;
    fborderSN: boolean;
    fborderEW: boolean;
    fborderWE: boolean;
  published
    property color: Integer read fcolor write fcolor;
    property colorN: Integer read fcolorN write fcolorN;
    property colorS: Integer read fcolorS write fcolorS;
    property colorW: Integer read fcolorW write fcolorW;
    property colorE: Integer read fcolorE write fcolorE;
    property group: Integer read fgroup write fgroup;
    property groupN: Integer read fgroupN write fgroupN;
    property groupS: Integer read fgroupS write fgroupS;
    property groupW: Integer read fgroupW write fgroupW;
    property groupE: Integer read fgroupE write fgroupE;
    property borderNS: boolean read fborderNS write fborderNS;
    property borderSN: boolean read fborderSN write fborderSN;
    property borderEW: boolean read fborderEW write fborderEW;
    property borderWE: boolean read fborderWE write fborderWE;
  public
    constructor Create;
  end;

  TColorGroupList = class(TIntList)
  private
    fcolorPx: Integer;//color grupy
    fcolorMP: String;//string dl pliku mp: 0x....
    fcolorTyp: string;//string do pliku mp(hex)
    fidMPGroup: integer;
    procedure setColorPx(acolorPx: Integer);
  published
    property colorPx: Integer read fcolorPx write setColorPx;
    property colorMP: String read fcolorMP write fcolorMP;
    property colorTyp: String read fcolorTyp write fcolorTyp;
    property idMPGroup: Integer read fidMPGroup write fidMPGroup;
    //procedure initiateColorMP;
    constructor Create(aidMPGroup: Integer); overload;
  end;

  {TMPTyp = class
    typ: integer;
  end;

  TMPTypList = class (TIntList)

  end;}

  TMPFile = class
  private
    reg: TRegistry;
    mpFile: TextFile;
    typFile: TextFile;
    procedure AddMPFileHead;
    procedure AddLevels;
    procedure AddZoom;
    procedure AddPolygonStringsFromVectGroup(aVectRecGroup: TVectRectGroup; aLevel: integer;
                                             axGeoPX, ayGeoPX: Double;
                                             adisplaceX, adisplaceY: Double;
                                             aColorGroupList: TColorGroupList;
                                             acolorArr: TDynamicPxColorPointArray);
    procedure TypPathFromMpPath;
    procedure AddPolyColorDefs(avectRectGroupsByColor: TJclIntegerHashMap);
    procedure AddPolyDataDefs(avectRectGroupsByColor: TJclIntegerHashMap);
    procedure AddPolyDrawOrderDefs(avectRectGroupsByColor: TJclIntegerHashMap);
  public
    mpLineList: TStringList;
    stTypBody: String;
    path: String;
    name: String;
    mpFileName: String; //przechowywana w reg ¹cie¿ka i nazwa pliku MP
    typFileName: String; //plik typ ma tak¹ sam¹ œcie¿kê i nazwê, ale inne rozszerzenie
    procedure SavePathToReg(aPath: String);
    procedure LoadPathFromReg;
    procedure MPFileOpen;
    procedure MPFileClose;
    procedure TypFileOpen;
    procedure TypFileClose;
    constructor Create; reintroduce; virtual;
    procedure MPFileSave(aMapFactory: TMapFactory);
    procedure ClearFilesBody;
    procedure AddTypFileHeader(aPOICount, aLineCount, aPolyCount: integer);
    procedure typAdd(aStr: String);
    procedure TypFileSave(aMapFactory: TMapFactory);
  end;


  TOColor = class
  public
    color: TColor;
  end;

  TOPointArr = class
public
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

  //grupa rectangli wektorowych tworz¹cych jeden¹ grupê koloru;
  TVectRectGroup = class(TObject)
  private
    //cztery punkty geograficzne okreœlan¹ce rogi obrazka
    //leftTopGeo, rightTopGeo, leftBottomGeo, rightBottomGeo: Double;
    //lista krawêdzi punktów Integer (pixeli) (kolejnych)
    fedgePxList: TIntList;
    //lista krawêdzi punktów Double
    //fedgeGeoList: TIntList;
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

    fMapFactory: TMapFactory; //rodzic
    //zadajemy dwa KOLEJNE punkty poruszaj¹ce siê po liniach Hor i Ver
    //Dostajemy c_fromLeft, c_fromTop, c_fromRight, c_fromBottom
    function direction(p1, p2: TOpoint): integer;
    //tworzy krawêdŸ z 3 kolejnych punktów
    //  multi = mno¿nik: dla grafiki bedzie to zoom, dla geo bêdzie to szerokoœc
    //          geograficzna jednego px
    procedure makePartEdge(prvPoint, actPoint, nextPoint: TOPoint;
                           var counter: integer; aGeoArr: TDynamicGeoPointArray;
                           amultiX, aMultiY: Double; displaceX, displaceY: Double;
                           acolorArr: TDynamicPxColorPointArray);
    //tworzy krawêdzie dla pojedynczego punktu
    procedure makePartEdge4OnePoint(aPoint: TOPoint; aGeoArr: TDynamicGeoPointArray;
                                    amultiX, amultiY: Double; displaceX, displaceY: Double;
                                    acolorArr: TDynamicPxColorPointArray);
    procedure getLine(p1, p2: TOPoint; var A, C, mian: Double);
    //procedure PxListToGeoList;
    function SrcHeight: Integer;
    function SrcWidth: Integer;
    function vectObjArr: TDynamicPointArray;
    function PxPointToGeoPoint(aPxPoint: TVectRectangle): TGeoPoint;
  published
    property edgePxList: TIntList read fedgePxList write fedgePxList;
    //property edgeGeoList: TIntList read fedgeGeoList write fedgeGeoList;
    property simpleEdgeList: TIntList read fsimpleEdgeList write fsimpleEdgeList;
    property rectList: TIntList read frectList write frectList;
    property testColor: TColor read ftestColor write ftestColor;
    property color: TColor read fcolor write fcolor;
    property mapFactory: TMapFactory read fMapFactory write fMapFactory;
  public
    constructor Create; overload;
    destructor Destroy; override;
    //tworzy tablicê punktów z ponktów zawartych w edgePxList
    function makeVectorEdge({vectArr: TDynamicPointArray; }aMultiX, aMultiY: Double;
                            adisplaceX, adisplaceY: Double;
                            acolorArr: TDynamicPxColorPointArray): TDynamicGeoPointArray;//(vectArr: TVarArray);
    function simplifyVectorEdge(arr: TDynamicGeoPointArray): TDynamicGeoPointArray;
    function GeoArray2PxArray(aGeoArr: TDynamicGeoPointArray): TDynamicPxPointArray;
    //buduje krawêdŸ
    procedure makeEdges;
  end;

  //lista grup rectangli wektorowych - TVectRectGroup
  //obiekty wektorowe przechowywane s¹ w Objects
  TMapFactory = class(TIntList)
  private
    fvectArr: TDynamicPointArray; //tablica z obektami wektorowymi
    fsrcWidth: Integer; //szerokoœæ wczytanego (ReadFromImg) obrazka
    fsrcHeight: Integer; //wysokoœæ wczytanego (ReadFromImg) obrazka
    //wsp. geo. lewego górnego rogu
    fgeoLeftUpX: Double;
    fgeoLeftUpY: Double;
    //wsp. geo. prawego dolnego rogu
    fgeoRightDownX: Double;
    fgeoRightDownY: Double;
    fXGeoPX: Double;
    fYGeoPX: Double;
    fvectRectGroupsByColor: TJclIntegerHashMap; //key - kolor; obj - lista grup w tym kolorze
    fcolorArr: TDynamicPxColorPointArray;
    function getObjById(index: Integer): TVectObj;
    procedure setObjById(index: Integer; avectObj: TVectObj);
    procedure InfoAkcja(aStr: String); virtual;
    procedure InfoTime(aStr: String); virtual;
  published
    property srcWidth: Integer read fsrcWidth write fsrcWidth;
    property srcHeight: Integer read fsrcHeight write fsrcHeight;
    property vectArr: TDynamicPointArray read fvectArr write fvectArr;
    property vectRectGroupsByColor: TJclIntegerHashMap read fvectRectGroupsByColor write fvectRectGroupsByColor;
    property geoLeftUpX: Double read fgeoLeftUpX write fgeoLeftUpX;
    property geoLeftUpY: Double read fgeoLeftUpY write fgeoLeftUpY;
    property geoRightDownX: Double read fgeoRightDownX write fgeoRightDownX;
    property geoRightDownY: Double read fgeoRightDownY write fgeoRightDownY;
    property xGeoPX: Double read fXGeoPX write fXGeoPX;
    property yGeoPX: Double read fYGeoPX write fYGeoPX;
    property colorArr: TDynamicPxColorPointArray read fcolorArr write fcolorArr;
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
    //wype³ania arrColor, czyli tablicê z informacj¹ o kolorach piceli i ich s¹siadów
    procedure fillColorArr;
    //dla wszystkich grup tworzone s¹ krawêdzie (mekeEdges)
    procedure makeEdgesForGroups;
    constructor Create; override;
    //Oblicze ile stopni zawiera jeden PX
    procedure CalculateGeoPx;
  end;

  TSeparateThreadVectList = class(TMapFactory)
  private
    fotWorker: TOmniWorker;
    procedure InfoAkcja(aStr: String); override;
    procedure InfoTime(aStr: String); override;
  published
    property otWorker: TOmniWorker read fotWorker write fotWorker;
  public
    constructor Create; override;
  end;

  TMainThreadVectList = class(TMapFactory)
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
    fvectGroup: TVectRectGroup;
    //numer grupy (w liœcie grup), która posiada dany obiekt vektorowy
    fvectGroupId: Integer;
    //fvectArr: array of array of TVectObj;
    //przepisanie (do³¹czenie) obiektów z innej grupy do tej, która posiada obiekt self
    procedure dopiszGrupe(agroupList: TVectRectGroup; avectList: TMapFactory);
    function getP(lp: Integer): TOPoint;
    //function getVectObj(x, y: integer): TVectObj; virtual;
  published
    //kolor wype³niaj¹cy obiekt
    property color: TColor read fcolor write fcolor;
    //odnoœnik do grupy, która posiada dany obiekt vektorowy
    property vectGroup: TVectRectGroup read fvectGroup write fvectGroup;
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
    class procedure zintegruj(aobj1, aobj2: TVectRectangle;
                              amapFactory: TMapFactory);
  end;

  TGeoPoint = class
  public
    x: Double;
    y: Double;
    constructor Create(ax, ay: Double); overload;
  end;

implementation

uses
  SysUtils, Main_Thread, Sys_Const;

{ TMapFactory }

function TMapFactory.FillImgWithRect(aimg: TImage; azoom: Integer; atestColor: Boolean;
                           agrid: boolean; agridColor: TColor): TBitmap;
var
  x, y: Integer;
  vectObj: TVectRectangle;
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

procedure TMapFactory.CalculateGeoPx;
begin
  xGeoPX := (geoRightDownX - geoLeftUpX) / (srcWidth + 1);
  yGeoPX := (geoLeftUpY - geoRightDownY) / (srcHeight + 1);
end;

constructor TMapFactory.Create;
begin
  inherited;
  vectRectGroupsByColor := TJclIntegerHashMap.Create(256, true);
end;

function TMapFactory.FillImgWithPolygons(aimg: TImage; azoom: Integer; atestColor: Boolean;
                           agrid: boolean; agridColor: TColor): TBitmap;
var
  i: Integer;
  //vectObj: TVectRectangle;
  bmp: TBitmap;
  vectGroup: TVectRectGroup;
  geoPointArr: TDynamicGeoPointArray; //lista punktów do przekazania, aby stworzyæ polygon
  pxPointArray: TDynamicPxPointArray;
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
      vectGroup := Objects[i] as TVectRectGroup;

      if not atestColor then
        Brush.Color := vectGroup.color
      else
        Brush.Color := vectGroup.testColor;
      brush.Style := bsSolid;

      geoPointArr := vectGroup.makeVectorEdge({self.vectArr,} azoom, azoom, 0 ,0, colorArr);
      pxPointArray := vectGroup.GeoArray2PxArray(geoPointArr);

      {for j:=0 to vectGroup.edgePxList.Count-1 do
      begin
        vectObj := vectGroup.edgePxList.objects[j] as TVectRectangle;
        tmpPoint := TGeoPoint.Create((vectObj.p2.X)*azoom, (vectObj.p2.Y+1)*azoom);
        pointArr[j] := tmpPoint;
      end;}
      doit := true;
      if doit then
      Polygon(pxPointArray);
    end;

  end;

  Result := bmp;
  aimg.Canvas.Unlock;
end;

function TMapFactory.getObjById(index: Integer): TVectObj;
begin
  result := Objects[index] as TVectObj;
end;

procedure TMapFactory.groupRect;
var
  x, y: Integer;
  vectObj: TVectRectangle;
  key: integer;
  lpGrupa: integer;
  perf: TTimeInterval;
  perf2, perf3: TTimeInterval;
  colorGroupList: TColorGroupList;
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
        vectObj.vectGroup := TVectRectGroup.Create;
        vectObj.vectGroup.mapFactory := Self;
        vectObj.vectGroup.lpGrupa := lpGrupa;
        inc(lpGrupa);
        vectObj.vectGroup.testColor := Math.RandomRange(0, 99999);
        vectObj.vectGroup.color := vectObj.color;
        vectObj.vectGroup.rectList.AddObject(0, vectObj);
        vectObj.vectGroup.edgePxList.AddObject(0, vectObj);
        key := nextKey;
        addObject(key, vectObj.vectGroup);
        vectObj.vectGroupId := key;

        //dodanie grupy do listy kolorów
        colorGroupList := vectRectGroupsByColor.GetValue(vectObj.vectGroup.color) as TColorGroupList;
        if colorGroupList = nil then
        begin
          colorGroupList := TColorGroupList.Create(vectRectGroupsByColor.Size);
          vectRectGroupsByColor.PutValue(vectObj.vectGroup.color, colorGroupList);
        end;
        colorGroupList.setColorPx(vectObj.vectGroup.color);
        colorGroupList.AddObject(vectObj.vectGroupId, vectObj.vectGroup);
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

procedure TMapFactory.fillColorArr;
  function dajColorPx(x, y: Integer): TColorPx;
  begin
    result := TColorPx.Create;
    result.color := (vectArr[x, y] as TVectRectangle).color;
    result.group := (vectArr[x, y] as TVectRectangle).vectGroupId;
    if y>0 then
    begin
      result.colorN := (vectArr[x, y-1] as TVectRectangle).color;
      result.groupN := (vectArr[x, y-1] as TVectRectangle).vectGroupId;
    end else
    begin
      result.colorN := kdPinecha;
      result.groupN := kdPinecha;
    end;
    if y<srcHeight-1 then
    begin
      result.colorS := (vectArr[x, y+1] as TVectRectangle).color;
      result.groupS := (vectArr[x, y+1] as TVectRectangle).vectGroupId;
    end else
    begin
      result.colorS := kdPinecha;
      result.groupS := kdPinecha;
    end;
    if x<srcWidth-1 then
    begin
      result.colorE := (vectArr[x+1, y] as TVectRectangle).color;
      result.groupE := (vectArr[x+1, y] as TVectRectangle).vectGroupId;
    end else
    begin
      result.colorE := kdPinecha;
      result.groupE := kdPinecha;
    end;
    if x>0 then
    begin
      result.colorW := (vectArr[x-1, y] as TVectRectangle).color;
      result.groupW := (vectArr[x-1, y] as TVectRectangle).vectGroupId;
    end else
    begin
      result.colorW := kdPinecha;
      result.groupW := kdPinecha;
    end;
  end;
var
  x, y: Integer;
  vectObj: TVectRectangle;
  key: integer;
  //lpGrupa: integer;
  //perf: TTimeInterval;
 // perf2, perf3: TTimeInterval;
  //colorGroupList: TColorGroupList;
  colorPx: TColorPx;
begin
  //perf := TTimeInterval.Create;
  //perf2 := TTimeInterval.Create;
  //perf3 := TTimeInterval.Create;
  //Clear;
  //lpGrupa := 0;
  //srodek
  for y:=0 to srcHeight-1 do
    //perf.Start;
    for x:=0 to srcWidth-1 do
      colorArr[x,y] := dajColorPx(x, y);
end;


procedure TMapFactory.InfoAkcja(aStr: String);
begin
  stMessage := aStr;
end;

procedure TMapFactory.InfoTime(aStr: String);
begin
  stTime := aStr;
end;

procedure TMapFactory.makeEdgesForGroups;
var
  i: Integer;
  vectGroup: TVectRectGroup;
  wrRes, wrDiv: Word;
begin
  for i:=0 to Count-1 do
  begin
    vectGroup := Objects[i] as TVectRectGroup;
    DivMod(i, inMod, wrRes, wrDiv);
    if wrDiv = 0 then
      InfoAkcja('Tworzenie granicy dla grupy ' + IntToStr(i) + '/' + IntToStr(Count-1) );
    vectGroup.makeEdges;
  end;
end;

procedure TMapFactory.setObjById(index: Integer; avectObj: TVectObj);
begin
  Objects[index] := avectObj;
end;

procedure TMapFactory.ReadFromImg(aimg: TImage);
var
  x, y: integer;
  rec: TVectRectangle;
begin
  srcWidth := aimg.Width;
  srcHeight := aimg.Height;
  SetLength(fvectArr, srcWidth, srcHeight);
  SetLength(fcolorArr, srcWidth, srcHeight);
  for y:=0 to aimg.Height-1 do
    for x:=0 to aimg.Width-1 do
    begin
      rec := TVectRectangle.Create(
                  aimg.Canvas.Pixels[x, y],
                  TOPoint.getPoint(Point(x, y)),
                  TOpoint.getPoint(Point(x, y))
               );
      vectArr[x,y] := rec;
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

class procedure TVectRectangle.zintegruj(aobj1, aobj2: TVectRectangle;
                                         amapFactory: TMapFactory);
var
  obj1, obj2: TVectRectangle;
  delIdx: Integer;
  colorGruopList: TIntList;
  colorGruopListIdx: Integer;
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
      obj1.dopiszGrupe(obj2.vectGroup, amapFactory);
      colorGruopListIdx := obj2.vectGroup.color;
    end
    else
    begin
      delIdx := obj1.vectGroupId;
      obj2.dopiszGrupe(obj1.vectGroup, amapFactory);
      colorGruopListIdx := obj1.vectGroup.color;
    end;
    //usuniêcie przepisanej grupy z listy grup
    assert(amapFactory.indexOf(delIdx) >= 0, 'Brak grupy do usuniêcia: ' + intToStr(delIdx) + '.');
    amapFactory.delete(amapFactory.indexOf(delIdx));

    colorGruopList := amapFactory.vectRectGroupsByColor.GetValue(colorGruopListIdx) as TIntList;
    colorGruopList.Delete(colorGruopList.indexOf(delIdx));
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
  vectObj: TVectRectangle;
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
          vectObj := get(x, y) as TVectRectangle;
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
begin
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

procedure TVectObj.dopiszGrupe(agroupList: TVectRectGroup; avectList: TMapFactory);
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

{ TVectRectGroup }

constructor TVectRectGroup.Create;
begin
  inherited;
  fedgePXList := TIntList.Create;
  frectList := TIntList.Create;
  MapFactory := nil;
end;

destructor TVectRectGroup.Destroy;
begin
  beep;
  inherited;
end;

function TVectRectGroup.direction(p1, p2: TOpoint): integer;
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

function TVectRectGroup.GeoArray2PxArray(
  aGeoArr: TDynamicGeoPointArray): TDynamicPxPointArray;
var
  i: integer;
begin
  SetLength(result, Length(aGeoArr));
  for i := 0 to Length(aGeoArr)-1 do
    result[i] := Point(Round(aGeoArr[i].x), Round(aGeoArr[i].y));
end;

procedure TVectRectGroup.getLine(p1, p2: TOPoint; var A, C, mian: Double);
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

function TVectRectGroup.makeVectorEdge({vectArr: TDynamicPointArray; }aMultiX, aMultiY: Double;
                                   adisplaceX, adisplaceY: Double;
                                   acolorArr: TDynamicPxColorPointArray): TDynamicGeoPointArray;
var
  i: integer;
  o1, o2, o3: TOPoint;
  counter: integer;
begin
  counter := 0;
  SetLength(result, edgePxList.Count*3);
  if edgePxList.Count > 1 then
  begin
    //SetLength(result, self.rectList.Count+30);
    o1 := (edgePxList.Objects[edgePxList.Count-1] as TVectObj).getP(0);
    o2 := (edgePxList.Objects[0] as TVectObj).getP(0);
    o3 := (edgePxList.Objects[1] as TVectObj).getP(0);
    makePartEdge(o1, o2, o3, counter, result, aMultiX, aMultiY, adisplaceX, adisplaceY, acolorArr);

    for i:=1 to edgePxList.Count-2 do
    begin
      o1 := (edgePxList.Objects[i-1] as TVectObj).getP(0);
      o2 := (edgePxList.Objects[i] as TVectObj).getP(0);
      o3 := (edgePxList.Objects[i+1] as TVectObj).getP(0);
      makePartEdge(o1, o2, o3, counter, result, amultiX, aMultiY, adisplaceX, adisplaceY, acolorArr);
    end;

    o1 := (edgePxList.Objects[edgePxList.Count-2] as TVectObj).getP(0);
    o2 := (edgePxList.Objects[edgePxList.Count-1] as TVectObj).getP(0);
    o3 := (edgePxList.Objects[0] as TVectObj).getP(0);
    makePartEdge(o1, o2, o3, counter, result, amultiX, aMultiY, adisplaceX, adisplaceY, acolorArr);
    //SetLength(result, Min(counter,10));
    SetLength(result, counter);
  end else
  begin
    SetLength(result, 4);
    makePartEdge4OnePoint((edgePxList.Objects[0] as TVectObj).getP(0),
                          result, amultiX, aMultiY, adisplaceX, adisplaceY,
                          acolorArr);
    counter := 4;
  end;
end;

{procedure TVectRectGroup.PxListToGeoList;
var
  i: Integer;
  edgePoint: TVectRectangle;
  geoPoint: TGeoPoint;
begin
  for i:=0 to edgePxList.Count-1 do
  begin
    edgePoint := edgePxList.Objects[i] as TVectRectangle;
    geoPoint := PxPointToGeoPoint(edgePoint);
    //edgeGeoList.AddObject(edgePxList[i], geoPoint);//przepisujemy klucz z edgePxList
  end;
end;  }

function TVectRectGroup.PxPointToGeoPoint(aPxPoint: TVectRectangle): TGeoPoint;
begin
  Result := TGeoPoint.Create;
  //P1 i P2 bêd¹ takie same, bo aPxPoint reprezentuje pojedynczy pixel, wiêc wartoœæi x i y mo¿emy wzi¹æ z p1
  Result.x := aPxPoint.p1.x;
  Result.y := aPxPoint.p1.y;
end;

function TVectRectGroup.SrcHeight: Integer;
begin
  Result := mapFactory.srcHeight;
end;

function TVectRectGroup.SrcWidth: Integer;
begin
  Result := mapFactory.srcWidth;
end;

function TVectRectGroup.vectObjArr: TDynamicPointArray;
begin
  Result := mapFactory.vectArr;
end;

function TVectRectGroup.simplifyVectorEdge(
  arr: TDynamicGeoPointArray): TDynamicGeoPointArray;
begin
  //to do
end;

procedure TVectRectGroup.makeEdges;
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
      bottomVectorRectangle := vectObjArr[astartEdgePoint.p1.x, astartEdgePoint.p1.y+1];
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
          Result := vectObjArr[aprevEdge.getP1.x, aprevEdge.getP1.y-1] as TVectRectangle;
          if aprevEdge.vectGroupId = Result.vectGroupId then
            Exit;
        end;
        Result := nil;
      end;
      function checkRight(aprevEdge: TVectRectangle): TVectRectangle;
      begin
        if aprevEdge.getP1.x < srcWidth-1 then
        begin
          Result := vectObjArr[aprevEdge.getP1.x+1, aprevEdge.getP1.y] as TVectRectangle;
          if aprevEdge.vectGroupId = Result.vectGroupId then
            Exit;
        end;
        Result := nil;
      end;
      function checkBottom(aprevEdge: TVectRectangle): TVectRectangle;
      begin
        if aprevEdge.getP1.y < srcHeight-1 then
        begin
          Result := vectObjArr[aprevEdge.getP1.x, aprevEdge.getP1.y+1] as TVectRectangle;
          if aprevEdge.vectGroupId = Result.vectGroupId then
            Exit;
        end;
        Result := nil;
      end;
      function checkLeft(aprevEdge: TVectRectangle): TVectRectangle;
      begin
        if aprevEdge.getP1.x > 0 then
        begin
          Result := vectObjArr[aprevEdge.getP1.x-1, aprevEdge.getP1.y] as TVectRectangle;
          if aprevEdge.vectGroupId = Result.vectGroupId then
            Exit;
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
        Result := checkLeft(aprevEdge)
      else
      begin
        Result := nil;
        Assert(False, 'checkNextEdge');
      end;

    end;
  var
    i, j: Integer;
  begin
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
  edgePxList.Clear;
  //startEdgePoint to pierwszy punkt na liœcie, bo idziemy ol lewej strony
  //w najwy¿szym wierszu
  startEdgePoint := rectList.Objects[0] as TVectRectangle;
  prevEdgePoint := startEdgePoint;
  arrivDir := c_goRight; //zaczynamy od max lewego ponktu na górnej linji
                         //Ka¿emy zacz¹æ szukanie od prawej
  nextEdgePoint := nil;
  //1-pixelowy obiekt traktujemy inaczej
  if rectList.count <> 1 then
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
      if (nextEdgePoint = nil) and (edgePxList.Count <> 0) then
        Assert(false, 'Oddany edge jest nil (' + IntToStr(prevEdgePoint.P1.x) +
                                ',' + IntToStr(prevEdgePoint.p1.y) + '), liczba znalezionych kreawêdzi:' +
                                IntToStr(edgePxList.Count));

      edgePxList.AddObject(edgePxList.nextKey, nextEdgePoint);
      prevEdgePoint := nextEdgePoint;

      //except
      //  on E: Exception do
      //    raise;
      //end;

    end
  //dla obiektu 1-pixelowego
  else
  begin
    //dodajemy punkty graniczne do listy
    edgePxList.AddObject(edgePxList.nextKey, startEdgePoint);
  end;
  //PxListToGeoList;
end;

procedure TVectRectGroup.makePartEdge(prvPoint, actPoint, nextPoint: TOPoint;
                                  var counter: integer; aGeoArr: TDynamicGeoPointArray;
                                  amultiX, aMultiY: double; displaceX, displaceY: Double;
                                  acolorArr: TDynamicPxColorPointArray);
var
  colorPx: TColorPx;
begin
  colorPx := acolorArr[actPoint.x, actPoint.y] as TColorPx;
  if direction(prvPoint, actPoint) = c_fromLeft then
  begin
    if direction(actPoint, nextPoint) = c_fromBottom then
    begin
      //nic
    end
    else if direction(actPoint, nextPoint) = c_fromTop then
    begin
      aGeoArr[counter] := TGeoPoint.Create(actPoint.x*amultiX + displaceX, actPoint.y*amultiY + displaceY);
      aGeoArr[counter+1] := TGeoPoint.Create((actPoint.x+1)*amultiX + displaceX, actPoint.y*amultiY + displaceY);
      counter := counter + 2;
      colorPx.borderWE := True;
      colorPx.borderNS := True;
    end
    else if direction(actPoint, nextPoint) = c_fromLeft then
    begin
      aGeoArr[counter] := TGeoPoint.Create((actPoint.x)*amultiX + displaceX, (actPoint.y)*amultiY + displaceY);
      counter := counter + 1;
      colorPx.borderWE := True;
    end
    else
    begin
      aGeoArr[counter] := TGeoPoint.Create((actPoint.x)*amultiX + displaceX, (actPoint.y)*amultiY + displaceY);
      aGeoArr[counter+1] := TGeoPoint.Create((actPoint.x+1)*amultiX + displaceX, actPoint.y*amultiY + displaceY);
      aGeoArr[counter+2] := TGeoPoint.Create((actPoint.x+1)*amultiX + displaceX, (actPoint.y+1)*amultiY + displaceY);
      counter := counter + 3;
      colorPx.borderWE := True;
      colorPx.borderNS := True;
      colorPx.borderEW := True;
    end;
  end

  else if direction(prvPoint, actPoint) = c_fromRight then
  begin
    if direction(actPoint, nextPoint) = c_fromBottom then
    begin
      aGeoArr[counter] := TGeoPoint.Create((actPoint.x+1)*amultiX + displaceX, (actPoint.y+1)*amultiY + displaceY);
      aGeoArr[counter+1] := TGeoPoint.Create(actPoint.x*amultiX + displaceX, (actPoint.y+1)*amultiY + displaceY);
      counter := counter + 2;
      colorPx.borderEW := True;
      colorPx.borderSN := True;
    end
    else if direction(actPoint, nextPoint) = c_fromTop then
    begin
      //nic
    end
    else if direction(actPoint, nextPoint) = c_fromRight then
    begin
      aGeoArr[counter] := TGeoPoint.Create((actPoint.x + 1)*amultiX + displaceX, (actPoint.y + 1)*amultiY + displaceY);
      counter := counter + 1;
      colorPx.borderEW := True;
    end
    else
    begin
      aGeoArr[counter] := TGeoPoint.Create((actPoint.x+1)*amultiX + displaceX, (actPoint.y+1)*amultiY + displaceY);
      aGeoArr[counter+1] := TGeoPoint.Create(actPoint.x*amultiX + displaceX, (actPoint.y+1)*amultiY + displaceY);
      aGeoArr[counter+2] := TGeoPoint.Create(actPoint.x*amultiX + displaceX, (actPoint.y)*amultiY + displaceY);
      counter := counter + 3;
      colorPx.borderEW := True;
      colorPx.borderNS := True;
      colorPx.borderWE := True;
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
      aGeoArr[counter] := TGeoPoint.Create((actPoint.x+1)*amultiX + displaceX, actPoint.y*amultiY + displaceY);
      aGeoArr[counter+1] := TGeoPoint.Create((actPoint.x+1)*amultiX + displaceX, (actPoint.y+1)*amultiY + displaceY);
      counter := counter + 2;
      colorPx.borderNS := True;
      colorPx.borderEW := True;
    end
    else if direction(actPoint, nextPoint) = c_fromTop then
    begin
      aGeoArr[counter] := TGeoPoint.Create((actPoint.x+1)*amultiX + displaceX, actPoint.y*amultiY + displaceY);
      counter := counter + 1;
      colorPx.borderNS := True;
    end
    else
    begin
      aGeoArr[counter] := TGeoPoint.Create((actPoint.x+1)*amultiX + displaceX, actPoint.y*amultiY + displaceY);
      aGeoArr[counter+1] := TGeoPoint.Create((actPoint.x+1)*amultiX + displaceX, (actPoint.y+1)*amultiY + displaceY);
      aGeoArr[counter+2] := TGeoPoint.Create(actPoint.x*amultiX + displaceX, (actPoint.y+1)*amultiY + displaceY);
      counter := counter + 3;
      colorPx.borderNS := True;
      colorPx.borderWE := True;
      colorPx.borderSN := True;
    end;
  end

  else //from bottom
  begin
    if direction(actPoint, nextPoint) = c_fromLeft then
    begin
      aGeoArr[counter] := TGeoPoint.Create(actPoint.x*amultiX + displaceX, (actPoint.y+1)*amultiY + displaceY);
      aGeoArr[counter+1] := TGeoPoint.Create(actPoint.x*amultiX + displaceX, actPoint.y*amultiY + displaceY);
      counter := counter + 2;
      colorPx.borderSN := True;
      colorPx.borderWE := True;
    end
    else if direction(actPoint, nextPoint) = c_fromRight then
    begin
      //nic
    end
    else if direction(actPoint, nextPoint) = c_fromBottom then
    begin
      aGeoArr[counter] := TGeoPoint.Create(actPoint.x*amultiX + displaceX, (actPoint.y+1)*amultiY + displaceY);
      counter := counter + 1;
      colorPx.borderSN := True;
    end
    else
    begin
      aGeoArr[counter] := TGeoPoint.Create(actPoint.x*amultiX + displaceX, (actPoint.y+1)*amultiY + displaceY);
      aGeoArr[counter+1] := TGeoPoint.Create(actPoint.x*amultiX + displaceX, actPoint.y*amultiY + displaceY);
      aGeoArr[counter+2] := TGeoPoint.Create((actPoint.x+1)*amultiX + displaceX, actPoint.y*amultiY + displaceY);
      counter := counter + 3;
      colorPx.borderSN := True;
      colorPx.borderWE := True;
      colorPx.borderNS := True;
    end;
  end
end;

procedure TVectRectGroup.makePartEdge4OnePoint(aPoint: TOPoint; aGeoArr: TDynamicGeoPointArray;
                                           amultiX, amultiY: Double; displaceX, displaceY: Double;
                                           acolorArr: TDynamicPxColorPointArray);
var
  colorPx: TColorPx;
begin
  aGeoArr[0] := TGeoPoint.Create((aPoint.x)*amultiX + displaceX, (aPoint.y)*amultiY + displaceY);
  aGeoArr[1] := TGeoPoint.Create((aPoint.x+1)*amultiX + displaceX, aPoint.y*amultiY + displaceY);
  aGeoArr[2] := TGeoPoint.Create((aPoint.x+1)*amultiX + displaceX, (aPoint.y+1)*amultiY + displaceY);
  aGeoArr[3] := TGeoPoint.Create((aPoint.x)*amultiX + displaceX, (aPoint.y+1)*amultiY + displaceY);
  colorPx := acolorArr[aPoint.x, aPoint.y] as TColorPx;
  colorPx.borderNS := true;
  colorPX.borderSN := true;
  colorPx.borderEW := true;
  colorPx.borderWE := true;
end;

{ TSeparateThreadVectList }

constructor TSeparateThreadVectList.Create;
begin
  inherited;
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

procedure TMPFile.ClearFilesBody;
begin
  mpLineList.Clear;
  stTypBody := '';
end;

constructor TMPFile.Create;
begin
  reg := TRegistry.Create;
  mpFileName := '';
  typFileName := '';
  mpLineList := TStringList.Create;
  stTypBody := '';
end;

procedure TMPFile.MPFileClose;
begin
  CloseFile(mpFile);
end;

procedure TMPFile.AddLevels;
begin
  with mpLineList do
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
  with mpLineList do
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
  with mpLineList do
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
  end;
end;

procedure TMPFile.AddPolygonStringsFromVectGroup(aVectRecGroup: TVectRectGroup; aLevel: integer;
                                                 axGeoPX, ayGeoPX: Double;
                                                 adisplaceX, adisplaceY: Double;
                                                 aColorGroupList: TColorGroupList;
                                                 acolorArr: TDynamicPxColorPointArray);

  function getEdgeStr: String;
    function GeoPiontToStr(aVal: Double): String;
    begin
      result := StringReplace(Format('%.5f', [aVal]),',','.',[rfReplaceAll]);
      //FormatFloat('0.00000', aVal);
    end;
  var
    i: integer;
    geoPoint: TGeoPoint;
    geoEdgeArr: TDynamicGeoPointArray;
  begin
    //najpiewrw" mapa ma "wysokoœæ" a potem "szerokoœæ"
    //"wysokoœæ" mapy dla polski zwiêksza siê z po³udnia na pó³noc, podczas gdy
    //punkt 0 grafiki to lewy górny róg, wiêc dlatego w wysokoœci jest minus
    //"szerokoœæ" mapy zwiêksza siê z zachodu na wschód, wiêc tak jak na
    //grafice, dlatego jest plus
    geoEdgeArr := aVectRecGroup.makeVectorEdge(axGeoPX, -ayGeoPX, adisplaceX, adisplaceY, acolorArr);
    result := '';
    for i := 0 to Length(GeoEdgeArr)-1 do
    begin
      geoPoint := GeoEdgeArr[i] as TGeoPoint;
      //result := StrConcat(',', result, '('+Format('%.5g', [geoPoint.x])+','+Format('%.5g', [geoPoint.y])+')');

      result := StrConcat(',', result, '('+GeoPiontToStr(geoPoint.y)+','+GeoPiontToStr(geoPoint.x)+')');
    end;
  end;

  {Format(%2.8d, geoPoint.x)}

begin
  with mpLineList do
  begin
    Add('[POLYGON]');
    Add('Type=' + aColorGroupList.colorMP);
    //Add(' * Typ=' + aColorGroupList.colorTyp);
    Add('Label=');
    Add('EndLevel=4');
    Add('Data' + intToStr(aLevel) + '=' + getEdgeStr);//'=(54.60100,18.29108),(54.60102,18.29284),(54.60042,18.29267),(54.60038,18.29103)
    Add('[END]');
  end;
end;

procedure TMPFile.AddTypFileHeader(aPOICount, aLineCount, aPolyCount: integer);
var
  y,m,d: Word;
  hr,mi,se,ms: Word;
  lpStartBlock, lDummypStartBlock: Integer;
  //offset dla  bloku POI, Line, Poly
  lpPOIOff, lpLineOff, lpPolyOff: Integer;
  //d³ugoœæ bloku dla POI, Linji i Poly
  lpPOILength, lpLineLength, lpPolyLength: Integer;
  //offset, d³upoœæ danych w bajtach i d³ugoœæ ca³ego bloko Data dla POI;
  lpPOIDataOff, lpPOIDataLength, lpPOIDataBlockLength: Integer;
  //offset, d³upoœæ danych w bajtach i d³ugoœæ ca³ego bloko Data dla POI;
  lpLineDataOff, lpLineDataLength, lpLineDataBlockLength: Integer;
  //offset, d³upoœæ danych w bajtach i d³ugoœæ ca³ego bloko Data dla POI;
  lpPolyDataOff, lpPolyDataLength, lpPolyDataBlockLength: Integer;
  //offset, d³ugoœæ danych, d³ugoœæ bloku dla drawOrder
  lpPolyDrawOff, lpPolyDrawLength, lpPolyDrawBlockLength: Integer;
begin
  lpStartBlock := 91;
  lDummypStartBlock := 0;
  DecodeDate(now,y,m,d);
  DecodeTime(now,hr,mi,se,ms);

  lpPOIOff := lDummypStartBlock;
  lpPOILength := aPOICount * 666;

  lpLineOff := lDummypStartBlock + lpPOILength;
  lpLineLength := aLineCount * 666;

  lpPolyOff := lpStartBlock + lpPOILength + lpLineLength;
  lpPolyLength := aPolyCount * 4; //4 bajty na reprezentacjê polygonu

  if aPOICount = 0 then
  begin
    lpPOIDataOff := 0;
    lpPOIDataLength := 0;
    lpPOIDataBlockLength := 0;
  end
  else
  begin
    lpPOIDataOff := lDummypStartBlock + lpPOILength + lpLineLength + lpPolyLength;
    lpPOIDataLength := 3;// przyk³adowa iloœæ bajów
    lpPOIDataBlockLength := aPOICount * lpPOIDataLength;
  end;

  if aLineCount = 0 then
  begin
    lpLineDataOff := 0;
    lpLineDataLength := 0;
    lpLineDataBlockLength := 0;
  end
  else
  begin
    lpLineDataOff := lDummypStartBlock + lpPOILength + lpLineLength + lpPolyLength +
                     lpPOIDataBlockLength;
    lpLineDataLength := 3;// przyk³adowa iloœæ bajów
    lpLineDataBlockLength := aLineCount * lpLineDataLength;
  end;

  if aPolyCount = 0 then
  begin
    lpPolyDataOff := 0;
    lpPolyDataLength := 0;
    lpPolyDataBlockLength := 0;
    lpPolyDrawOff := 0;
    lpPolyDrawLength := 0;
    lpPolyDrawBlockLength := 0;
  end
  else
  begin
    lpPolyDataOff := lpStartBlock + lpPOILength + lpLineLength + lpPolyLength +
                     lpPOIDataBlockLength + lpLineDataBlockLength;
    lpPolyDataLength := 4; //bajt pocz¹tkowy (?) + 3 bajty koloru dla polygona prostego
    lpPolyDataBlockLength := aPolyCount * lpPolyDataLength;
    lpPolyDrawOff := lpStartBlock + lpPOILength + lpLineLength + lpPolyLength +
                     lpPOIDataBlockLength + lpLineDataBlockLength + lpPolyDataBlockLength;
    lpPolyDrawLength := 5; //d³ugoœæ bloku dla poly Draw
    lpPolyDrawBlockLength := aPolyCount {+ iloœæ zmian leveli - same 0} * lpPolyDrawLength;
  end;

  //pocz¹tek bloku z danymi: 5B = 91
  addIntToStrHex(91, 1, stTypBody);
  //zero
  addIntToStrHex(0, 1, stTypBody);
  //nazwa - 10 bajtów/znaków
  typAdd('GARMIN TYP');
  //0/1: zero to ignorowanie pliku typ
  addIntToStrHex(1, 1, stTypBody);
  //zero: w inftukcji pisze FF or 00
  addIntToStrHex(0, 1, stTypBody);
  //7 bajtów daty i czasu, 2 na rok i po jednym na resztê
  addIntToStrHex(y-1900, 2, stTypBody);
  addIntToStrHex(m, 1, stTypBody);
  addIntToStrHex(d, 1, stTypBody);
  addIntToStrHex(hr, 1, stTypBody);
  addIntToStrHex(mi, 1, stTypBody);
  addIntToStrHex(se, 1, stTypBody);
  //zapis strony kodowej - 2 bajty
  addIntToStrHex(1252, 2, stTypBody);

  //offset bloku poi - 4bajty
  addIntToStrHex(lpPOIOff, 4, stTypBody);
  //d³ugoœæ bloku poi: lpPOI*d³ugoœæPOI w bajtach - 4 bajty
  addIntToStrHex(lpPOILength, 4, stTypBody);

  //offset lini - bajty
  addIntToStrHex(lpLineOff, 4, stTypBody);
  //d³ugoœæ bloku linji: lpLine*d³ugoœæLine w bajtach - 4 bajty
  addIntToStrHex(lpLineLength, 4, stTypBody);

  //offset polygogów
  addIntToStrHex(lpPolyOff, 4, stTypBody);
  //offset polygonów - bajty
  //dla prostych polygonów u¿ywamy 4 bajty na 1 definicjê
  addIntToStrHex(lpPolyLength , 4, stTypBody);

  //Family ID:
  addIntToStrHex(666, 2, stTypBody);
  //Produvt ID:
  addIntToStrHex(666, 2, stTypBody);

  //offste POI data
  addIntToStrHex(lpPOIDataOff , 4, stTypBody);
  //d³ugoœæ pojedynczego data dla POI
  addIntToStrHex(lpPOIDataLength , 2, stTypBody);
  //d³ugoœæ ca³ego bloku data dla POI
  addIntToStrHex(lpPOIDataBlockLength , 4, stTypBody);

  //offste Line data
  addIntToStrHex(lpLineDataOff , 4, stTypBody);
  //d³ugoœæ pojedynczego data dla Line
  addIntToStrHex(lpLineDataLength , 2, stTypBody);
  //d³ugoœæ ca³ego bloku data dla Line
  addIntToStrHex(lpLineDataBlockLength , 4, stTypBody);

  //offste Line data
  addIntToStrHex(lpPolyDataOff , 4, stTypBody);
  //d³ugoœæ pojedynczego data dla Line
  addIntToStrHex(lpPolyDataLength , 1, stTypBody);
  //linked to polygons
  addIntToStrHex(0 , 1, stTypBody);
  //d³ugoœæ ca³ego bloku data dla Line
  addIntToStrHex(lpPolyDataBlockLength , 4, stTypBody);

  //offste Line data
  addIntToStrHex(lpPolyDrawOff , 4, stTypBody);
  //d³ugoœæ pojedynczego data dla Line
  addIntToStrHex(lpPolyDrawLength , 2, stTypBody);
  //d³ugoœæ ca³ego bloku data dla Line
  addIntToStrHex(lpPolyDrawBlockLength , 4, stTypBody);
end;

procedure TMPFile.AddPolyColorDefs(avectRectGroupsByColor: TJclIntegerHashMap);
var
  i: Integer;
  colorGroupList :TColorGroupList;
  It: IJclIterator;
begin
  It := avectRectGroupsByColor.Values.First;
  while It.HasNext do
  begin
    addIntToStrHex(6 , 1, stTypBody);
    colorGroupList := TColorGroupList(It.Next);
    stTypBody := stTypBody + AnsiChar(strToInt('$' + colorGroupList.colorTyp[1] + colorGroupList.colorTyp[2]));
    stTypBody := stTypBody + AnsiChar(strToInt('$' + colorGroupList.colorTyp[3] + colorGroupList.colorTyp[4]));
    stTypBody := stTypBody +  AnsiChar(strToInt('$' + colorGroupList.colorTyp[5] + colorGroupList.colorTyp[6]));
  end;
end;

procedure TMPFile.AddPolyDataDefs(avectRectGroupsByColor: TJclIntegerHashMap);
var
  i: Integer;
  colorGroupList :TColorGroupList;
  It: IJclIterator;
begin
  It := avectRectGroupsByColor.Values.First;
  while It.HasNext do
  begin
    addIntToStrHex(6 , 1, stTypBody);
    colorGroupList := TColorGroupList(It.Next);
    addIntToStrHex(colorGroupList.idMPGroup*32, 2, stTypBody);
    //offset: 4 bajty na ka¿dy polygon
    addIntToStrHex(i*4 , 2, stTypBody);
  end;
end;

procedure TMPFile.AddPolyDrawOrderDefs(avectRectGroupsByColor: TJclIntegerHashMap);
var
  i: Integer;
  colorGroupList :TColorGroupList;
  It: IJclIterator;
begin
  It := avectRectGroupsByColor.Values.First;
  while It.HasNext do
  begin
    addIntToStrHex(6 , 1, stTypBody);
    colorGroupList := TColorGroupList(It.Next);
    addIntToStrHex(colorGroupList.idMPGroup , 1, stTypBody);
    addIntToStrHex(0 , 4, stTypBody);
  end;
end;

procedure TMPFile.MPFileOpen;
begin
  AssignFile(mpFile, mpFileName);
  ReWrite(mpFile);
end;

procedure TMPFile.MPFileSave(aMapFactory: TMapFactory);
var
  i: Integer;
begin
  //MPFileOpen;
  ClearFilesBody;
  AddMPFileHead;

  with mpLineList do
  begin
    Add('[POLYGON]');
    Add('Type=0x4b');
    Add('Backgruond=Y');
    Add('Data0=(55.00000,17.00000),(53.00000,17.10000),(53.00000,19.00000),(55.00000,19.00000)');//'=(54.60100,18.29108),(54.60102,18.29284),(54.60042,18.29267),(54.60038,18.29103)
    Add('[END]');
  end;

  for i:=0 to aMapFactory.Count-1 do
    AddPolygonStringsFromVectGroup(TVectRectGroup(aMapFactory.Objects[i]), 0,
                                   aMapFactory.xGeoPX, aMapFactory.yGeoPX,
                                   aMapFactory.geoLeftUpX, aMapFactory.geoLeftUpY,
                                   aMapFactory.vectRectGroupsByColor.GetValue(TVectRectGroup(aMapFactory.Objects[i]).color) as TColorGroupList,
                                   aMapFactory.colorArr);
  mpLineList.SaveToFile(mpFileName);
  TypFileSave(aMapFactory);
 // MPFileClose;
end;

procedure TMPFile.TypFileClose;
begin
  CloseFile(typFile);
end;

procedure TMPFile.TypFileOpen;
begin
  AssignFile(typFile, typFileName);
  ReWrite(typFile);
end;

procedure TMPFile.TypFileSave(aMapFactory: TMapFactory);
var
  typFile : TextFile;
begin
  //Assert(aMapFactory <> nil);
  if aMapFactory <> nil then
  begin
    AddTypFileHeader(0,0, aMapFactory.vectRectGroupsByColor.Size);
    //toDo - jeœli w przysz³oœci bêdzie
    //AddPOIDefs
    //AddLineDefs
    AddPolyColorDefs(aMapFactory.vectRectGroupsByColor);
    AddPolyDataDefs(aMapFactory.vectRectGroupsByColor);
    AddPolyDrawOrderDefs(aMapFactory.vectRectGroupsByColor);
  end
  else
    AddTypFileHeader(0, 0, 0);
  AssignFile(typFile, typFileName);
  ReWrite(typFile);

  Write(typFile, stTypBody);
  CloseFile(typFile);
  //typLineList.SaveToFile(typFileName);
end;

procedure TMPFile.TypPathFromMpPath;
begin
  typFileName := Copy(mpFileName, 1, Length(mpFileName)-2) + 'typ';
end;

procedure TMPFile.SavePathToReg(aPath: String);
begin
  mpFileName := aPath;
  TypPathFromMpPath;
  reg.OpenKey(REGSOFT + progName, true);
  reg.WriteString('MPFilePathName', mpFileName);
  reg.CloseKey;
end;

procedure TMPFile.typAdd(aStr: String);
begin
  stTypBody := stTypBody + aStr;
end;

procedure TMPFile.LoadPathFromReg;
begin
  reg.OpenKey(REGSOFT + progName, true);
  mpFileName := reg.ReadString('MPFilePathName');
  TypPathFromMpPath;
  reg.CloseKey;
end;

{ TGeoPoint }

constructor TGeoPoint.Create(ax, ay: Double);
begin
  Create;
  x := ax;
  y := ay;
end;

{ TColorGroupList }

constructor TColorGroupList.Create(aidMPGroup: Integer);
begin
  Create;
  idMPGroup := aidMPGroup+1;
end;

procedure TColorGroupList.setColorPx(acolorPx: Integer);
var
  stRed, stGreen, stBlue, stColor: String;
begin
  if fcolorMP = '' then
  begin
    fcolorPx := acolorPx;
    fcolorMP := '0x' + IntToHex(idMPGroup,4);
    stColor := IntToHex(acolorPx, 6);
    {stBlue := copy(stColor, 1, 2);
    stGreen := copy(stColor, 3, 2);
    stRed := copy(stColor, 5, 2);
    fcolorTyp := stRed + stGreen + stBlue; }
    fcolorTyp := stColor;
  end else
    Assert(fcolorPx = acolorPx, 'Przypisujemy inny kolor. By³o: ' + intToStr(fcolorPx) + ' a ma byæ:' + IntToStr(acolorPx)) ;
end;

{ TColorPx }

constructor TColorPx.Create;
begin
  inherited;
  borderNS := False;
  borderSN := False;
  borderEW := False;
  borderWE := False;
end;

end.
