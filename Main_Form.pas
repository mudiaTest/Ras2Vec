unit Main_Form;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ExtDlgs, ExtCtrls, ComCtrls, ToolWin, StdCtrls, Form_utl,
  Main_Obj, ActnMan, ActnColorMaps, TeCanvas, Menus, Sys_utl, ActnList, Mask,
  Register_Obj, Main_CV, Ini_Obj
  {$IFNDEF VER185}
  ,OtlEventMonitor, OtlTaskControl, OtlComm, OtlThreadPool
  ,Main_Thread
  {,Vcl.ActnList, Vcl.Mask}
  {$ENDIF}
  ;

const
  c_mainImage = 1;
  c_zoomImage = 2;
  c_EmptyGeoText = '  ,  ,  ,  ';

type

  TMainForm = class(TForm)
    PaintBoxMain: TPaintBox;
    dlgPicture: TOpenPictureDialog;
    sbMain: TScrollBox;
    imgMain: TImage;
    tbZoom: TTrackBar;
    sbZoom: TScrollBox;
    imgZoom: TImage;
    Panel1: TPanel;
    Panel2: TPanel;
    edtZoom: TEdit;
    PaintBoxZoom: TPaintBox;
    chkGrid: TCheckBox;
    cdGrid: TColorDialog;
    chkTestColor: TCheckBox;
    btn1: TButton;
    chkPolyRect: TCheckBox;
    btnZoomIn: TButton;
    btnZoomOut: TButton;
    Button1: TButton;
    Button2: TButton;
    mmToolBar1: TMainMenu;
    N1: TMenuItem;
    MainMG: TMenuItem;
    OtherMG: TMenuItem;
    Open1: TMenuItem;
    Load1: TMenuItem;
    Tylkoread1: TMenuItem;
    R2V1: TMenuItem;
    Exit1: TMenuItem;
    GridColor1: TMenuItem;
    lblAkcja: TLabel;
    lblTime: TLabel;
    btnStopR2V: TButton;
    MainActionList: TActionList;
    actR2VBtnStop: TAction;
    actR2VMenu: TAction;
    Button3: TButton;
    btnSave: TButton;
    SaveDialog: TSaveDialog;
    edtLeftUpX: TMaskEdit;
    edtLeftUpY: TMaskEdit;
    edtRightDownY: TMaskEdit;
    lbl1: TLabel;
    lbl2: TLabel;
    edtRightDownX: TMaskEdit;
    Button4: TButton;
    TEST: TMenuItem;
    test1: TMenuItem;
    Save1: TMenuItem;
    SaveAs1: TMenuItem;
    Load2: TMenuItem;
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    procedure PaintBoxMainPaint(Sender: TObject);
    procedure imgMouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure imgMouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure imgMouseMove(Sender: TObject; Shift: TShiftState; X,
      Y: Integer);
    procedure sbZoomMouseWheel(Sender: TObject; Shift: TShiftState;
      WheelDelta: Integer; MousePos: TPoint; var Handled: Boolean);
    procedure tbZoomKeyPress(Sender: TObject; var Key: Char);
    procedure tbZoomChange(Sender: TObject);
    procedure btnZoomInClick(Sender: TObject);
    procedure btnZoomOutClick(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Open1Click(Sender: TObject);
    procedure Load1Click(Sender: TObject);
    procedure Tylkoread1Click(Sender: TObject);
    procedure R2V1Click(Sender: TObject);
    procedure Exit1Click(Sender: TObject);
    procedure GridColor1Click(Sender: TObject);
    {$IFNDEF VER185}
    procedure oemR3VTaskTerminated(const task: IOmniTaskControl);
    procedure oemR3VTaskMessage(const task: IOmniTaskControl;
      const msg: TOmniMessage);
    {$ENDIF}
    procedure btnStopR2VClick(Sender: TObject);
    procedure actR2VBtnStopExecute(Sender: TObject);
    procedure actR2VMenuExecute(Sender: TObject);
    procedure btn1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure btnSaveClick(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure edtLeftUpXExit(Sender: TObject);
    procedure edtLeftUpYExit(Sender: TObject);
    procedure edtRightDownXExit(Sender: TObject);
    procedure edtRightDownYExit(Sender: TObject);
    procedure Load2Click(Sender: TObject);
    procedure Save1Click(Sender: TObject);
    procedure SaveAs1Click(Sender: TObject);
  private
    { Private declarations }
    stGraphFileNamePath: String;
    bmp, bmp2: TBitMap;
    color: Tcolor;

    imgStartPos: TPoint;
    imgActing: Boolean;
    imgZoomPos: TPoint;
    lpZoom: Integer; //poziom zoomu z sówaka - zapamiêtuje poziom powiêkszenia obrazka
    lpActImgZoom: Integer; //poziom zoomu obrazka = mo¿e byæ inny ni¿ sówaka

    mapFactory: TMapFactory; //lista grup obiektów wektorowych - ka¿da grupa
                                //bedzie potem polygonem
    MPFile: TMPFile; //obiekt do kompleksowej obs³ugi plików MP

    gridColor: TColor; //kolor siatki/otoczki polygonów
    {$IFNDEF VER185}
    taskR2V: IOmniTaskControl; //omni task grupowania pixeli i wyznaczania granic dla rectangli
    oemR3V: TOmniEventMonitor;
    {$ENDIF}
    perf: TTimeInterval; //perf dla ca³ego R2V

    srcReg: TSrcReg; //obiekt czytania i pisania do rejestru - ostatni plik graficzny i koordynaty z kontrolek
    iniSL: TIniSL; //obiekt zapamiêtywania istawieñ w plikach ini
    procedure mainImageScroll(Sender: TObject; HorzScroll: Boolean; OldPos, CurrentPos: Integer);
    procedure zoomImageScroll(Sender: TObject; HorzScroll: Boolean; OldPos, CurrentPos: Integer);
    //procedure setScrollPos(asbDest, asbSrc: TScrollBox);
    procedure saveZoomPos;
    procedure DoZoom;
    procedure SetControls(atask: integer);
    {$IFNDEF VER185}
    procedure DoOnR2VTerminate;
    {$ENDIF}
    procedure CreateMainThreadVectorGroupList;
    procedure CreateSeperateThreadVectorGroupList;
    function  DecodeGeoStr(aGeoPointStr: String): Double;
    procedure FillFromIni(ainiSL: TIniSL);
    procedure AktReg;
  public
    { Public declarations }
    constructor Create(AOwner: TComponent); override;
    destructor Destroy; override;
    procedure InfoAkcja(aStr: String);
  end;

  var
    MainForm: TMainForm;

implementation

Uses
  Math;

{$R *.dfm}

procedure TMainForm.btn1Click(Sender: TObject);
begin
  inherited;
  CreateMainThreadVectorGroupList;
end;

procedure TMainForm.btnSaveClick(Sender: TObject);
begin
  inherited;
  SaveDialog.FileName := MPFile.stmpFileName;
  SaveDialog.Filter := 'MP files (*.mp)|*.MP|Any file (*.*)|*.*';
  SaveDialog.Execute;
  MPFile.SavePathToReg(SaveDialog.FileName);
  if SaveDialog.FileName <> '' then
  begin
    Assert(edtLeftUpX.text<>c_EmptyGeoText, 'UpX nie jest poprawnie wype³niony.');
    Assert(edtLeftUpY.text<>c_EmptyGeoText, 'UpY nie jest poprawnie wype³niony.');
    Assert(edtRightDownX.text<>c_EmptyGeoText, 'DownX nie jest poprawnie wype³niony.');
    Assert(edtRightDownY.text<>c_EmptyGeoText, 'DownY nie jest poprawnie wype³niony.');
    mapFactory.geoLeftUpX :=  DecodeGeoStr(edtLeftUpX.text);
    mapFactory.geoLeftUpY := DecodeGeoStr(edtLeftUpY.text);
    mapFactory.geoRightDownX := DecodeGeoStr(edtRightDownX.text);
    mapFactory.geoRightDownY := DecodeGeoStr(edtRightDownY.text);
    MPFile.MPFileSave(mapFactory);
  end;
end;

procedure TMainForm.btnStopR2VClick(Sender: TObject);
begin
  inherited;
  {$IFNDEF VER185}
  try
    if assigned(taskR2V) then
    begin
      taskR2V.Terminate(10);
      taskR2V := nil;
    end else
      Assert(false, 'taskR2V = nil');
  finally
    actR2VBtnStopExecute(nil);
    DoOnR2VTerminate;
  end;
  {$ENDIF}
end;

procedure TMainForm.btnZoomInClick(Sender: TObject);
begin
  inherited;
  if tbZoom.Position < 9 then
  begin
    tbZoom.Position := tbZoom.Position + 1;
    tbZoomChange(nil);
    DoZoom;
  end;
end;

procedure TMainForm.btnZoomOutClick(Sender: TObject);
begin
  inherited;
  if tbZoom.Position > 0 then
  begin
    tbZoom.Position := tbZoom.Position - 1;
    tbZoomChange(nil);
    DoZoom;
  end;
end;

procedure TMainForm.Button1Click(Sender: TObject);
begin
  inherited;
  DoZoom;
end;

procedure TMainForm.Button2Click(Sender: TObject);
begin
  inherited;
  CreateSeperateThreadVectorGroupList;
end;

procedure TMainForm.Button3Click(Sender: TObject);
begin
  inherited;
  R2V1Click(nil);
end;

procedure TMainForm.Button4Click(Sender: TObject);
begin
  inherited;
  MPFile.TypFileSave(nil);
end;

procedure TMainForm.CreateMainThreadVectorGroupList;
begin
  if mapFactory <> nil then
    mapFactory.Free;
  mapFactory := TMainThreadVectList.Create; //zwealniane w TMainForm.Destroy
  {$IFNDEF VER185}
  mapFactory.OwnsObjects := true;
  {$ENDIF}
  (mapFactory as TMainThreadVectList).lblAkcja := lblAkcja;
  (mapFactory as TMainThreadVectList).lblTime := lblTime;
end;

procedure TMainForm.CreateSeperateThreadVectorGroupList;
begin
  {$IFNDEF VER185}
  if mapFactory <> nil then
    mapFactory.Free;
  mapFactory := TSeparateThreadVectList.Create; //zwalniane w TMainForm.Destroy
  mapFactory.OwnsObjects := true;
  {$ENDIF}
end;

constructor TMainForm.Create(AOwner: TComponent);
begin
  inherited;
  oemR3V := TOmniEventMonitor.Create(nil); //zwalniane w TMainForm.Destroy
  mapFactory := nil;
  srcReg := TSrcReg.Create; //zwalniane w TMainForm.Destroy
  iniSL := TIniSL.Create; //zwalniane w TMainForm.Destroy
  //CreateSeperateThreadVectorGroupList;
  CreateMainThreadVectorGroupList;
  MPFile := TMPFile.Create;
  MPFile.LoadPathFromReg;

  sbMain.OnScroll := mainImageScroll;
  sbZoom.OnScroll := zoomImageScroll;

  stGraphFileNamePath := srcReg.GetFilePathName;
  if stGraphFileNamePath <> '' then
    imgMain.Picture.LoadFromFile(stGraphFileNamePath);
  edtLeftUpX.Text := srcReg.GetGeo1X;
  edtLeftUpY.Text := srcReg.GetGeo1Y;
  edtRightDownX.Text := srcReg.GetGeo2X;
  edtRightDownY.Text := srcReg.GetGeo2Y;
  lastSLPath := srcReg.GetLastSLPath;

  PaintBoxMain.Width := imgMain.Width;
  PaintBoxMain.Height := imgMain.Height;

  bmp := imgMain.Picture.Bitmap;
  color := bmp.canvas.Pixels[1,1];

  bmp2:=TBitmap.create;
  bmp2.width:=200;(*Assign dimensions*)
  bmp2.height:=200;
  imgZoom.Picture.Graphic:=bmp2;(*Assign the bitmap to the image component*)
  lpZoom := 1;
  lpActImgZoom := 1;
end;

procedure TMainForm.imgMouseDown(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
  imgActing := true;
  imgStartPos := Point(x, y);
end;

procedure TMainForm.imgMouseUp(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
  imgActing := false;
  imgStartPos := Point(x, y);
end;

procedure TMainForm.InfoAkcja(aStr: String);
begin
  lblAkcja.Caption := aStr;
end;

procedure TMainForm.Load1Click(Sender: TObject);
begin
  inherited;
  if dlgPicture.Execute then
  begin
    stGraphFileNamePath := dlgPicture.FileName;
    imgMain.Picture.LoadFromFile(stGraphFileNamePath);
    srcReg.SetFilePathName(stGraphFileNamePath);
  end;
end;

procedure TMainForm.Load2Click(Sender: TObject);
begin
  iniSL.Load(lastSLPath);
  FillFromIni(iniSL);
  srcReg.SetLastSLPath(lastSLPath);
end;

procedure TMainForm.imgMouseMove(Sender: TObject; Shift: TShiftState; X,
  Y: Integer);
var
  gc: TGraphicControl;
  sb: TScrollBox;
begin
  if imgActing then
  begin
    gc := sender as TGraphicControl;
    sb := gc.Parent as TScrollBox;
    sb.HorzScrollBar.Position := sb.HorzScrollBar.Position + imgStartPos.x - x;
    sb.VertScrollBar.Position := sb.VertScrollBar.Position + imgStartPos.y - y;
  end;
end;

//zapamiêtuje po³o¿enie obrazka tak, aby powiêszanie nie przesuwa³o go
procedure TMainForm.saveZoomPos;
var
  tmpZoom: integer;
begin
  //if (lpActImgZoom >0) and (lpActImgZoom<11) then
    tmpZoom := lpActImgZoom;
  //else
  //  tmpZoom := 1;
  {imgZoomPos := Point(Round((sbZoom.HorzScrollBar.Position - (tmpZoom-1)*Round(sbZoom.Width/2 ))/tmpZoom),
                      Round((sbZoom.VertScrollBar.Position - (tmpZoom-1)*Round(sbZoom.Height/2))/tmpZoom)); }
  {imgZoomPos := Point(Round((sbZoom.HorzScrollBar.Position / tmpZoom),
                      Round((sbZoom.VertScrollBar.Position / tmpZoom));   }
  imgZoomPos := Point(Round((sbZoom.HorzScrollBar.Position + (sbZoom.Width-21)/2 )/tmpZoom),
                      Round((sbZoom.VertScrollBar.Position + (sbZoom.Height-21)/2)/tmpZoom));
end;


procedure TMainForm.sbZoomMouseWheel(Sender: TObject; Shift: TShiftState;
  WheelDelta: Integer; MousePos: TPoint; var Handled: Boolean);
begin
  DoZoom;
  imgZoom.Refresh;
end;

{procedure TMainForm.setScrollPos(asbDest, asbSrc: TScrollBox);
var
  src: Integer;
begin
  if asbDest = sbZoom then
    src := c_zoomImage
  else
    src := c_mainImage;
end;}

procedure TMainForm.mainImageScroll(Sender: TObject; HorzScroll: Boolean; OldPos, CurrentPos: Integer);
begin
  //ustaw zoomImage wg main image
  //setScrollPos(sbZoom, sbMain);
end;

{$IFNDEF VER185}
procedure TMainForm.oemR3VTaskMessage(const task: IOmniTaskControl;
  const msg: TOmniMessage);
begin
  lblAkcja.Caption := msg.MsgData;
  //lblAkcja.Repaint;
end;

procedure TMainForm.DoOnR2VTerminate;
begin
  //w³¹czyæ przyciski
  taskR2V := nil;//prawdopodobnie na samoczynna zakoñczenie pracy trzeba to wynilowaæ
  SetControls(OW_DO_R2V);
  Screen.Cursor := crDefault;
  perf.Stop;
  lblAkcja.Caption := perf.InterSt;
  perf.Free;
end;

procedure TMainForm.oemR3VTaskTerminated(const task: IOmniTaskControl);
begin
  DoOnR2VTerminate;
end;
{$ENDIF}

procedure TMainForm.Open1Click(Sender: TObject);
begin
  inherited;
  PaintBoxMain.Repaint;
end;

procedure TMainForm.zoomImageScroll(Sender: TObject; HorzScroll: Boolean; OldPos, CurrentPos: Integer);
begin
end;

procedure TMainForm.PaintBoxMainPaint(Sender: TObject);
begin
  with PaintBoxMain.Canvas do begin
    Lock;
    Brush.Style := bsSolid;
    Brush.Color := clRed;
    PaintBoxMain.Canvas.Polyline([Point(40, 10), Point(20, 60), Point(70, 30),
    Point(10, 30), Point(60, 60), Point(40, 10)]);
    UnLock;
  end;
end;

procedure TMainForm.SetControls(atask: integer);
begin
{$IFNDEF VER185}
  if atask = OW_DO_R2V then
  begin
    //OtherMG.Enabled := not assigned(taskR2V);
    //MainMG.Enabled := not assigned(taskR2V);
    //btnStopR2V.Enabled := assigned(taskR2V);
  end;
{$ENDIF}
end;

{$IFNDEF VER185}
function TMainForm.DecodeGeoStr(aGeoPointStr: String): Double;
var
  list: TStringList;
  wideChars : PWideChar;
begin
  list := TStringList.Create;
  try
    New(wideChars);
    StringToWideChar(aGeoPointStr, wideChars, Length(aGeoPointStr) + 1);
    ExtractStrings([','], [], wideChars, list);
    Result := StrToFloat(list[0]) + StrToFloat(list[1])/60+StrToFloat(list[2])/3600+StrToFloat(list[3])/360000;
  finally
    list.free;
  end;
end;
{$ELSE}
function TMainForm.DecodeGeoStr(aGeoPointStr: String): Double;
var
  list: TStringList;
  chars : PAnsiChar;
begin
  list := TStringList.Create;
  try
    New(chars);
    chars := PChar(aGeoPointStr);
    //StringToWideChar(aGeoPointStr, chars, Length(aGeoPointStr) + 1);
    ExtractStrings([','], [], chars, list);
    Result := StrToFloat(list[0]) + StrToFloat(list[1])/60+StrToFloat(list[2])/3600+StrToFloat(list[3])/360000;
  finally
    list.free;
  end;
end;
{$ENDIF}

destructor TMainForm.Destroy;
begin
  mapFactory.free;
  MPFile.free;
{$IFNDEF VER185}
  oemR3V.free;
{$ENDIF}
  perf.free;
  srcReg.free;
  iniSL.free;
  inherited;
end;

procedure TMainForm.R2V1Click(Sender: TObject);
{$IFNDEF VER185}
var
  workerR2V: TR2VOmniWorker;//omni worker grupowania pixeli i wyznaczania granic dla rectangli
{$ENDIF}
begin
  inherited;
  try
    InfoAkcja('Wczytywanie obrazka.');
    Screen.Cursor := crHourGlass;
    mapFactory.Clear;
    mapFactory.ReadFromImg(imgMain);
    mapFactory.geoLeftUpX := DecodeGeoStr(edtLeftUpX.text);
    mapFactory.geoLeftUpY := DecodeGeoStr(edtLeftUpY.text);
    mapFactory.geoRightDownX := DecodeGeoStr(edtRightDownX.text);
    mapFactory.geoRightDownY := DecodeGeoStr(edtRightDownY.text);
    imgZoom.Width := imgMain.Width;
    imgZoom.Height := imgMain.Height;
    mapFactory.CalculateGeoPx;

    //vectorList2.FillImgWithRect(imgZoom, lpZoom, chkGrid.Checked, gridColor);
    perf := TTimeInterval.Create; //zwalniane w TMainForm.Destroy
    perf.Start;
    {$IFNDEF VER185}
    if mapFactory is TSeparateThreadVectList then
    begin
      workerR2V := TR2VOmniWorker.Create;
      try
        workerR2V.mapFactory := mapFactory as TSeparateThreadVectList;
        taskR2V := oemR3V.Monitor(CreateTask(workerR2V, 'R2V'))
          .SetTimer(1, 1, OW_DO_R2V)
          .Run;
      finally
        workerR2V.free;
      end;
    end
    else
    {$ENDIF}
    if mapFactory is TMainThreadVectList then
    begin
      mapFactory.groupRect;
      mapFactory.FillColorArr;
      mapFactory.makeEdgesForGroups;
      mapFactory.UpdateColorArr;
      mapFactory.makeInnerEdgesForGroups;
    end else
      Assert(False, 'Klasa mapFactory rózna od TSeparateThreadVectList i TMainThreadVectList');

  finally
    Screen.Cursor := crDefault;
  end;
end;

procedure TMainForm.Save1Click(Sender: TObject);
begin
  Assert(lastSLPath <> '', 'lastSLPath jest puste');
  Assert(FileExists(lastSLPath), 'lastSLPath ="'+lastSLPath+'" nie jest plikiem');
  iniSL.Init(edtLeftUpX.Text,
             edtLeftUpY.Text,
             edtRightDownX.Text,
             edtRightDownY.Text,
             stGraphFileNamePath,
             MPFile.stmpFileName);
  iniSL.SaveAs(lastSLPath);
  srcReg.SetLastSLPath(lastSLPath);
end;

procedure TMainForm.SaveAs1Click(Sender: TObject);
begin
  iniSL.SaveAs;
  srcReg.SetLastSLPath(lastSLPath);
end;

procedure TMainForm.tbZoomChange(Sender: TObject);
begin
  lpZoom := max(1, round(Math.Power(2.0, tbZoom.Position-1)));
  edtZoom.Text := intToStr(lpZoom);
end;

procedure TMainForm.tbZoomKeyPress(Sender: TObject; var Key: Char);
begin
  DoZoom;
end;

procedure TMainForm.Tylkoread1Click(Sender: TObject);
begin
  inherited;
  Screen.Cursor := crHourGlass;
  mapFactory.ReadFromImg(imgMain);
  DoZoom;
  Screen.Cursor := crDefault;
end;

procedure TMainForm.DoZoom;
var
  tmpBmp: TBitmap;
  //scrollHorPos, scrollVerPos: double;
begin

  Screen.Cursor := crHourGlass;
  try
    saveZoomPos;
    //scrollHorPos := sbZoom.HorzScrollBar.Position;
    //scrollVerPos := sbZoom.VertScrollBar.Position;
    //zapisyje do zmiennej glob. nowy poziom zoomu 2^x
    lpZoom := round(Math.Power(2.0, tbZoom.Position-1));
    lpActImgZoom := lpZoom;

    //tego nie rozumiem, ale jest potrzebane do wyœwietlania
    SetMapMode(imgZoom.Canvas.Handle, MM_ISOTROPIC);
    SetWindowExtEx(imgZoom.Canvas.Handle, 1, 1, nil);
    SetViewportExtEx(imgZoom.Canvas.Handle, lpZoom, lpZoom, nil);

    if not chkPolyRect.checked then
      //wype³nia bitmapê grafik¹
      tmpBmp := mapFactory.FillImgWithRect(imgZoom, lpZoom, chkTestColor.Checked, chkGrid.Checked, gridColor)
    else
      //wype³nia bitmapê grafikê
      tmpBmp := mapFactory.FillImgWithPolygons(imgZoom, lpZoom, chkTestColor.Checked, chkGrid.Checked, gridColor);

    //ustawia wielkoœæ wszystkich warstw obrazka
    imgZoom.Width := Round(imgMain.Width * lpZoom);
    imgZoom.Height := Round(imgMain.Height * lpZoom);
    if Assigned(imgZoom.Picture.Graphic) then
    begin
      imgZoom.Picture.Graphic.Width := imgZoom.Width;
      imgZoom.Picture.Graphic.Height := imgZoom.Height;
    end;

    //tego nie rozumiem, ale jest potrzebane do wyœwietlania
    //SetMapMode(imgZoom.Canvas.Handle, MM_ISOTROPIC);
    //SetWindowExtEx(imgZoom.Canvas.Handle, 1, 1, nil);
   // SetViewportExtEx(imgZoom.Canvas.Handle, lpZoom, lpZoom, nil);

    SetViewportExtEx(imgZoom.Canvas.Handle, 1, 1, nil);

    //wyœwiatla obrazek na canwas
    imgZoom.Canvas.Draw(0, 0, tmpBmp);

    //wyœwiela w kontrolce poziom zoomu
    edtZoom.Text := intToStr(lpZoom);

    //przesówa powiêkszony obrazek
    //sbZoom.HorzScrollBar.Position := imgZoomPos.X*lpZoom + (lpZoom-1)*Round(sbZoom.Width/2);
    //sbZoom.VertScrollBar.Position := imgZoomPos.Y*lpZoom + (lpZoom-1)*Round(sbZoom.Height/2);
    //sbZoom.HorzScrollBar.Position := Round(scrollHorPos);
    //sbZoom.VertScrollBar.Position := Round(scrollVerPos);
    sbZoom.HorzScrollBar.Position := Round(imgZoomPos.X*lpZoom - (sbZoom.Width-21)/2 );
    sbZoom.VertScrollBar.Position := Round(imgZoomPos.Y*lpZoom - (sbZoom.Height-21)/2);
    {SetWindowOrgEx (imgZoom.Canvas.Handle,
    10,
    100, nil);}

  finally
    Screen.Cursor := crDefault;
  end;
end;

procedure TMainForm.edtLeftUpXExit(Sender: TObject);
begin
  srcReg.SetGeo1X(edtLeftUpX.Text);
end;

procedure TMainForm.edtLeftUpYExit(Sender: TObject);
begin
  srcReg.SetGeo1Y(edtLeftUpY.Text);
end;

procedure TMainForm.edtRightDownXExit(Sender: TObject);
begin
  inherited;
  srcReg.SetGeo2X(edtRightDownX.Text);
end;

procedure TMainForm.edtRightDownYExit(Sender: TObject);
begin
  srcReg.SetGeo2Y(edtRightDownY.Text);
end;

procedure TMainForm.Exit1Click(Sender: TObject);
begin
  inherited;
  Close;
end;

procedure TMainForm.FillFromIni(ainiSL: TIniSL);
begin
  edtLeftUpX.Text := ainiSL.stGeo1X;
  edtLeftUpY.Text := ainiSL.stGeo1Y;
  edtRightDownX.Text := ainiSL.stGeo2X;
  edtRightDownY.Text := ainiSL.stGeo2Y;
  stGraphFileNamePath := ainiSL.stGraphFileNamePath;
  MPFile.stmpFileName := ainiSL.stGraphFileNamePath;


  imgMain.Picture.LoadFromFile(stGraphFileNamePath);
  AktReg;
end;

procedure TMainForm.GridColor1Click(Sender: TObject);
begin
  inherited;
  if cdGrid.execute then
    gridColor := cdGrid.Color;
end;

procedure TMainForm.actR2VMenuExecute(Sender: TObject);
begin
//  OtherMG.Enabled := not assigned(taskR2V);
//  MainMG.Enabled := not assigned(taskR2V);
end;

procedure TMainForm.AktReg;
begin
  srcReg.SetFilePathName(stGraphFileNamePath);
  srcReg.SetGeo1X(edtLeftUpX.Text);
  srcReg.SetGeo1Y(edtLeftUpY.Text);
  srcReg.SetGeo2X(edtRightDownX.Text);
  srcReg.SetGeo2Y(edtRightDownY.Text);
end;

procedure TMainForm.actR2VBtnStopExecute(Sender: TObject);
begin
//  btnStopR2V.Enabled := assigned(taskR2V);
end;

end.
