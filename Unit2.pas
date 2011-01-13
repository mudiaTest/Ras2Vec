unit Unit2;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, dxBar, ExtDlgs, ExtCtrls, ComCtrls, dxCntner, dxEditor, dxEdLib;

type
  TOnScroll = procedure(Sender: TObject; HorzScroll: Boolean;
  OldPos, CurrentPos: Integer) of object;

  TScrollBox = class(Forms.TScrollBox)
  private
  FOnScroll: TOnScroll;
  procedure WMHScroll(var Message: TWMHScroll); message WM_HSCROLL;
  procedure WMVScroll(var Message: TWMVScroll); message WM_VSCROLL;
  public
  property OnScroll: TOnScroll read FOnScroll write FOnScroll;
  end;

  TForm2 = class(TForm)
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
    edtZoom: TdxMaskEdit;
    procedure btnExitClick(Sender: TObject);
    procedure dlgLoadClick(Sender: TObject);
    procedure btnOpenClick(Sender: TObject);
    procedure PaintBoxMainPaint(Sender: TObject);
    procedure tbZoomChange(Sender: TObject);
  private
    { Private declarations }
    imageName: String;
    bmp, bmp2: TBitMap;
    color: Tcolor;
    procedure imageScroll(Sender: TObject; HorzScroll: Boolean; OldPos, CurrentPos: Integer);
    procedure makeZoom(abmpDst, abmpSrc: TBitmap; ax, ay, azoom: integer);
  public
    { Public declarations }
    constructor Create(AOwner: TComponent); override;
  end;

  var
    Form2: TForm2;

implementation

{$R *.dfm}

{ TScrollBox }

procedure TScrollBox.WMHScroll(var Message: TWMHScroll);
var
OldPos: Integer;
begin
OldPos := HorzScrollBar.Position;
inherited;
if HorzScrollBar.Position <> OldPos then
if Assigned(FOnScroll) then
FOnScroll(Self, True, OldPos, HorzScrollBar.Position);
end;

procedure TScrollBox.WMVScroll(var Message: TWMVScroll);
var
OldPos: Integer;
begin
OldPos := VertScrollBar.Position;
inherited;
if VertScrollBar.Position <> OldPos then
if Assigned(FOnScroll) then
FOnScroll(Self, False, OldPos, VertScrollBar.Position);
end;

//-- === === ---


procedure TForm2.btnOpenClick(Sender: TObject);
begin
  PaintBoxMain.Repaint;
end;

constructor TForm2.Create(AOwner: TComponent);
var
  x, y: integer;
begin
  inherited;
  sbMain.OnScroll := imageScroll;

  imgMain.Picture.LoadFromFile('C:\Users\mudia\Desktop\t1.bmp');
  PaintBoxMain.Width := imgMain.Width;
  PaintBoxMain.Height := imgMain.Height;

  bmp := imgMain.Picture.Bitmap;
  color := bmp.canvas.Pixels[1,1];

  bmp2:=TBitmap.create;
  bmp2.width:=200;(*Assign dimensions*)
  bmp2.height:=200;

  bmp2 := TBitmap.Create;
  bmp2.LoadFromFile('C:\Users\mudia\Desktop\t1.bmp');
  //bmp2.PixelFormat := pf32bit;

  tbZoomChange(nil);
  imgZoom.Picture.Graphic:=bmp2;(*Assign the bitmap to the image component*)
end;

procedure TForm2.makeZoom(abmpDst, abmpSrc: TBitmap; ax, ay, azoom: integer);
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

procedure TForm2.dlgLoadClick(Sender: TObject);
begin
  if dlgPicture.Execute then
  begin
    //imageName := dlgPicture.FileName;
    //Image1.Picture.LoadFromFile(imageName);
  end;
end;

procedure TForm2.imageScroll(Sender: TObject; HorzScroll: Boolean; OldPos, CurrentPos: Integer);
begin
  //PaintBoxMain.Repaint;

  {makeZoom(bmp2, bmp,
           sbMain.HorzScrollBar.Position,
           sbMain.VertScrollBar.Position,
           10);  }
end;

procedure TForm2.PaintBoxMainPaint(Sender: TObject);
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

procedure TForm2.tbZoomChange(Sender: TObject);

var
  Zoom: Integer;
begin
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

  edtZoom.Text := Zoom;
  //Label1.Caption := 'Zoom: ' +
  //    IntToStr(Round(TrackBar1.Position / FULLSCALE * 100)) + '%';
end;


procedure TForm2.btnExitClick(Sender: TObject);
begin
  Close;
end;

end.
