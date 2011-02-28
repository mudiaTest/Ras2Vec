unit Main_Form;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, dxBar, ExtDlgs, ExtCtrls, ComCtrls, dxCntner, dxEditor, dxEdLib,
  ToolWin, StdCtrls, Form_utl, Sys_utl, Main_Obj, ActnMan, ActnColorMaps,
  TeCanvas;

const
  c_mainImage = 1;
  c_zoomImage = 2;

type

  TMainForm = class(TForm)
    PaintBoxMain: TPaintBox;
    dlgPicture: TOpenPictureDialog;
    dxBarManager1: TdxBarManager;
    sbiPlik: TdxBarSubItem;
    btnOpen: TdxBarButton;
    btnExit: TdxBarButton;
    sbiImage: TdxBarSubItem;
    dlgLoad: TdxBarButton;
    sbMain: TScrollBox;
    imgMain: TImage;
    tbZoom: TTrackBar;
    sbZoom: TScrollBox;
    imgZoom: TImage;
    Panel1: TPanel;
    Panel2: TPanel;
    edtZoom: TEdit;
    ToolBar1: TToolBar;
    PaintBoxZoom: TPaintBox;
    btmR2V: TdxBarButton;
    chkGrid: TCheckBox;
    cdGrid: TColorDialog;
    btnView: TdxBarSubItem;
    btnGridColor: TdxBarButton;
    chkTestColor: TCheckBox;
    btn1: TButton;
    chkPolyRect: TCheckBox;
    procedure btnExitClick(Sender: TObject);
    procedure dlgLoadClick(Sender: TObject);
    procedure btnOpenClick(Sender: TObject);
    procedure PaintBoxMainPaint(Sender: TObject);
    procedure imgMouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure imgMouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure imgMouseMove(Sender: TObject; Shift: TShiftState; X,
      Y: Integer);
    procedure sbZoomMouseWheel(Sender: TObject; Shift: TShiftState;
      WheelDelta: Integer; MousePos: TPoint; var Handled: Boolean);
    procedure btmR2VClick(Sender: TObject);
    procedure btnGridColorClick(Sender: TObject);
    procedure btn1Click(Sender: TObject);
    procedure tbZoomKeyPress(Sender: TObject; var Key: Char);
  private
    { Private declarations }
    imageName: String;
    bmp, bmp2: TBitMap;
    color: Tcolor;

    imgStartPos: TPoint;
    imgActing: Boolean;
    imgZoomPos: TPoint;
    zoom: Integer;

    vectorList: TVectList;
    gridColor: TColor;

    procedure mainImageScroll(Sender: TObject; HorzScroll: Boolean; OldPos, CurrentPos: Integer);
    procedure zoomImageScroll(Sender: TObject; HorzScroll: Boolean; OldPos, CurrentPos: Integer);
    procedure setScrollPos(asbDest, asbSrc: TScrollBox);
    procedure saveZoomPos;
    procedure init;
    procedure DoZoom;
  public
    { Public declarations }
    constructor Create(AOwner: TComponent); override;
  end;

  var
    MainForm: TMainForm;

implementation

Uses
  Math;

{$R *.dfm}

procedure TMainForm.btnOpenClick(Sender: TObject);
begin
  PaintBoxMain.Repaint;
end;

constructor TMainForm.Create(AOwner: TComponent);
var
  x, y: integer;
  f: TCanvas;
begin
  inherited;
  vectorList := TVectList.Create;

  sbMain.OnScroll := mainImageScroll;
  sbZoom.OnScroll := zoomImageScroll;

  imgMain.Picture.LoadFromFile('C:\Users\mudia\Desktop\t2.bmp');
  PaintBoxMain.Width := imgMain.Width;
  PaintBoxMain.Height := imgMain.Height;

  bmp := imgMain.Picture.Bitmap;
  color := bmp.canvas.Pixels[1,1];

  bmp2:=TBitmap.create;
  bmp2.width:=200;(*Assign dimensions*)
  bmp2.height:=200;
  imgZoom.Picture.Graphic:=bmp2;(*Assign the bitmap to the image component*)
  zoom := 0;
end;

procedure TMainForm.dlgLoadClick(Sender: TObject);
begin
  if dlgPicture.Execute then
  begin
    imageName := dlgPicture.FileName;
    imgMain.Picture.LoadFromFile(imageName);
  end;
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

procedure TMainForm.init;
begin
  gridColor := RGB(249, 192, 192);
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

procedure TMainForm.saveZoomPos;
var
  tmpZoom: integer;
begin
  if (zoom >0) and (zoom<11) then
    tmpZoom := zoom
  else
    tmpZoom := 1;
  imgZoomPos := Point(Round((sbZoom.HorzScrollBar.Position - (tmpZoom-1)*Round(sbZoom.Width/2 ))/tmpZoom),
                      Round((sbZoom.VertScrollBar.Position - (tmpZoom-1)*Round(sbZoom.Height/2))/tmpZoom));
end;


procedure TMainForm.sbZoomMouseWheel(Sender: TObject; Shift: TShiftState;
  WheelDelta: Integer; MousePos: TPoint; var Handled: Boolean);
begin
  DoZoom;
  imgZoom.Refresh;
end;

procedure TMainForm.setScrollPos(asbDest, asbSrc: TScrollBox);
var
  src: Integer;
begin
  if asbDest = sbZoom then
    src := c_zoomImage
  else
    src := c_mainImage;
end;

procedure TMainForm.mainImageScroll(Sender: TObject; HorzScroll: Boolean; OldPos, CurrentPos: Integer);
begin
  //ustaw zoomImage wg main image
  setScrollPos(sbZoom, sbMain);
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

procedure TMainForm.tbZoomKeyPress(Sender: TObject; var Key: Char);
begin
  DoZoom;
end;

procedure TMainForm.DoZoom;
var
  tmpBmp: TBitmap;
begin
  Screen.Cursor := crHourGlass;
  saveZoomPos;
  Zoom := round(Math.Power(2.0, tbZoom.Position-1));
  SetMapMode(imgZoom.Canvas.Handle, MM_ISOTROPIC);
  SetWindowExtEx(imgZoom.Canvas.Handle, 1, 1, nil);
  SetViewportExtEx(imgZoom.Canvas.Handle, Zoom, Zoom, nil);
  if not chkPolyRect.checked then
    tmpBmp := vectorList.FillImg(imgZoom, zoom, chkTestColor.Checked, chkGrid.Checked, gridColor)
  else
    tmpBmp := vectorList.FillImgWithPolygons(imgZoom, zoom, chkTestColor.Checked, chkGrid.Checked, gridColor);
  //nie rozumiem po co muszê ustawiæ dolowoln¹ wartoœæ sta³¹, ale inaczej obrazy mog¹ siê rozjechaæ
  imgZoom.Width := 0;
  imgZoom.Height := 0;
  imgZoom.Width := Round(imgMain.Width * Zoom);
  imgZoom.Height := Round(imgMain.Height * Zoom);
  if Assigned(imgZoom.Picture.Graphic) then
  begin
    imgZoom.Picture.Graphic.Width := imgZoom.Width;
    imgZoom.Picture.Graphic.Height := imgZoom.Height;
  end;
  imgZoom.Canvas.Draw(0, 0, tmpBmp);
  edtZoom.Text := intToStr(Zoom);
  sbZoom.HorzScrollBar.Position := imgZoomPos.X*zoom + (zoom-1)*Round(sbZoom.Width/2);
  sbZoom.VertScrollBar.Position := imgZoomPos.Y*zoom + (zoom-1)*Round(sbZoom.Height/2);
  Screen.Cursor := crDefault;
end;

procedure TMainForm.btmR2VClick(Sender: TObject);
begin
  Screen.Cursor := crHourGlass;
  vectorList.ReadFromImg(imgMain);
  imgZoom.Width := imgMain.Width;
  imgZoom.Height := imgMain.Height;
  //vectorList2.FillImg(imgZoom, zoom, chkGrid.Checked, gridColor);
  vectorList.groupRect;
  vectorList.joinRect;
  Screen.Cursor := crDefault;
end;

procedure TMainForm.btn1Click(Sender: TObject);
begin
imgZoom.Width := 100;
imgZoom.Height := 100;
  with imgZoom.Canvas do
  begin
    Pen.Style := psSolid;
    Pen.Color := clblack;

    Brush.Color := clRed;
    polygon( [
      point(1,1),
      Point(1,2),
      point(2,2),
      Point(2,1)
      ]
    );

    polygon( [
      point(2,1),
      Point(2,2),
      point(3,2),
      Point(3,1)
      ]
    );
  end;
end;

procedure TMainForm.btnExitClick(Sender: TObject);
begin
  Close;
end;

procedure TMainForm.btnGridColorClick(Sender: TObject);
begin
  if cdGrid.execute then
    gridColor := cdGrid.Color;
end;

end.
