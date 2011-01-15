unit Main_Form;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, dxBar, ExtDlgs, ExtCtrls, ComCtrls, dxCntner, dxEditor, dxEdLib,
  ToolWin, StdCtrls, Form_utl, Sys_utl, Obj;

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
    procedure btnExitClick(Sender: TObject);
    procedure dlgLoadClick(Sender: TObject);
    procedure btnOpenClick(Sender: TObject);
    procedure PaintBoxMainPaint(Sender: TObject);
    procedure tbZoomChange(Sender: TObject);
    procedure imgMouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure imgMouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure imgMouseMove(Sender: TObject; Shift: TShiftState; X,
      Y: Integer);
    procedure sbZoomMouseWheel(Sender: TObject; Shift: TShiftState;
      WheelDelta: Integer; MousePos: TPoint; var Handled: Boolean);
    procedure btmR2VClick(Sender: TObject);
  private
    { Private declarations }
    imageName: String;
    bmp, bmp2: TBitMap;
    color: Tcolor;

    imgStartPos: TPoint;
    imgActing: Boolean;
    imgZoomPos: TPoint;
    zoom: Integer;

    vectorList: vectList;

    procedure mainImageScroll(Sender: TObject; HorzScroll: Boolean; OldPos, CurrentPos: Integer);
    procedure makeZoom(abmpDst, abmpSrc: TBitmap; ax, ay, azoom: integer);
    procedure zoomImageScroll(Sender: TObject; HorzScroll: Boolean; OldPos, CurrentPos: Integer);
    procedure setScrollPos(asbDest, asbSrc: TScrollBox);
    procedure saveZoomPos;
  public
    { Public declarations }
    constructor Create(AOwner: TComponent); override;
  end;

  var
    MainForm: TMainForm;

implementation

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
  vectorList := vectList.Create;

  sbMain.OnScroll := mainImageScroll;
  sbZoom.OnScroll := zoomImageScroll;

  imgMain.Picture.LoadFromFile('C:\Users\mudia\Desktop\t1.bmp');
  PaintBoxMain.Width := imgMain.Width;
  PaintBoxMain.Height := imgMain.Height;

  bmp := imgMain.Picture.Bitmap;
  color := bmp.canvas.Pixels[1,1];

  bmp2:=TBitmap.create;
  bmp2.width:=200;(*Assign dimensions*)
  bmp2.height:=200;

  bmp2 := TBitmap.Create;
  //bmp2.LoadFromFile('C:\Users\mudia\Desktop\t1.bmp');
  //bmp2.PixelFormat := pf32bit;
  imgZoom.Picture.Graphic:=bmp2;(*Assign the bitmap to the image component*)
  zoom := 1;
  tbZoomChange(nil);
end;

procedure TMainForm.makeZoom(abmpDst, abmpSrc: TBitmap; ax, ay, azoom: integer);
var
  x, y: integer;
  x1, y1: integer;
begin
  for y := ax to abmpSrc.Width-1 do
    for x := ay to abmpSrc.Height-1 do
    begin
      for y1 := y*azoom to (y+1)*azoom-1 do
        for x1 := x*azoom to (x+1)*azoom-1 do
          abmpDst.canvas.Pixels[x1, y1] := abmpSrc.canvas.Pixels[x, y];
    end;
end;

procedure TMainForm.dlgLoadClick(Sender: TObject);
begin
  if dlgPicture.Execute then
  begin
    //imageName := dlgPicture.FileName;
    //Image1.Picture.LoadFromFile(imageName);
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
    //saveZoomPos;
  end;
end;

procedure TMainForm.saveZoomPos;
begin
  imgZoomPos := Point(Round((sbZoom.HorzScrollBar.Position - (zoom-1)*Round(sbZoom.Width/2 ))/zoom),
                      Round((sbZoom.VertScrollBar.Position - (zoom-1)*Round(sbZoom.Height/2))/zoom));
end;


procedure TMainForm.sbZoomMouseWheel(Sender: TObject; Shift: TShiftState;
  WheelDelta: Integer; MousePos: TPoint; var Handled: Boolean);
begin
  tbZoomChange(nil);
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
  //ustaw mainImage wg zoomImage
  //setScrollPos(sbZoom, sbMain);
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

procedure TMainForm.tbZoomChange(Sender: TObject);
begin
  if zoom <> tbZoom.Position then
    saveZoomPos;

  Zoom := tbZoom.Position;
  if not (Visible or (Zoom = 100)) or (Zoom = 0) then
    Exit;

  SetMapMode(imgZoom.Canvas.Handle, MM_ISOTROPIC);
  SetWindowExtEx(imgZoom.Canvas.Handle, 1, 1, nil);
  SetViewportExtEx(imgZoom.Canvas.Handle, Zoom, Zoom, nil);

  imgZoom.Width := Round(bmp2.Width * Zoom);
  imgZoom.Height := Round(bmp2.Height * Zoom);
  if Assigned(imgZoom.Picture.Graphic) then
  begin
    imgZoom.Picture.Graphic.Width := imgZoom.Width;
    imgZoom.Picture.Graphic.Height := imgZoom.Height;
  end;
  imgZoom.Canvas.Draw(0, 0, bmp2);

  edtZoom.Text := intToStr(Zoom);
  //Label1.Caption := 'Zoom: ' +
  //    IntToStr(Round(TrackBar1.Position / FULLSCALE * 100)) + '%';

  sbZoom.HorzScrollBar.Position := imgZoomPos.X*zoom + (zoom-1)*Round(sbZoom.Width/2);
  sbZoom.VertScrollBar.Position := imgZoomPos.Y*zoom + (zoom-1)*Round(sbZoom.Height/2);
end;


procedure TMainForm.btmR2VClick(Sender: TObject);
begin
  vectorList.ReadFromImg(imgMain);
  imgZoom.Width := imgMain.Width;
  imgZoom.Height := imgMain.Height;
  vectorList.FillImage(imgZoom);
  Beep;
end;

procedure TMainForm.btnExitClick(Sender: TObject);
begin
  Close;
end;

end.
