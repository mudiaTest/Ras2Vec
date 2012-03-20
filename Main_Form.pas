unit Main_Form;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ExtDlgs, ExtCtrls, ComCtrls, ToolWin, StdCtrls, Form_utl, Sys_utl,
  Main_Obj, ActnMan, ActnColorMaps, TeCanvas, Menus;

const
  c_mainImage = 1;
  c_zoomImage = 2;

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
    Main1: TMenuItem;
    Other1: TMenuItem;
    Open1: TMenuItem;
    Load1: TMenuItem;
    Tylkoread1: TMenuItem;
    R2V1: TMenuItem;
    Exit1: TMenuItem;
    GridColor1: TMenuItem;
    lblAkcja: TLabel;
    procedure PaintBoxMainPaint(Sender: TObject);
    procedure imgMouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure imgMouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure imgMouseMove(Sender: TObject; Shift: TShiftState; X,
      Y: Integer);
    procedure sbZoomMouseWheel(Sender: TObject; Shift: TShiftState;
      WheelDelta: Integer; MousePos: TPoint; var Handled: Boolean);
    procedure btn1Click(Sender: TObject);
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
  private
    { Private declarations }
    imageName: String;
    bmp, bmp2: TBitMap;
    color: Tcolor;

    imgStartPos: TPoint;
    imgActing: Boolean;
    imgZoomPos: TPoint;
    lpZoom: Integer; //poziom zoomu z sówaka - zapamiêtuje poziom powiêkszenia obrazka
    lpActImgZoom: Integer; //poziom zoomu obrazka = mo¿e byæ inny ni¿ sówaka

    vectorGroupList: TVectList; //lista grup obiektów wektorowych - ka¿da grupa
                                //bedzie potem polygonem
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
    procedure InfoAkcja(aStr: String);
  end;

  var
    MainForm: TMainForm;

implementation

Uses
  Math;

{$R *.dfm}

procedure TMainForm.btnZoomInClick(Sender: TObject);
begin
  if tbZoom.Position < 9 then
  begin
    tbZoom.Position := tbZoom.Position + 1;
    tbZoomChange(nil);
    DoZoom;
  end;
end;

procedure TMainForm.btnZoomOutClick(Sender: TObject);
begin
  if tbZoom.Position > 0 then
  begin
    tbZoom.Position := tbZoom.Position - 1;
    tbZoomChange(nil);
    DoZoom;
  end;
end;

procedure TMainForm.Button1Click(Sender: TObject);
begin
  DoZoom;
end;

constructor TMainForm.Create(AOwner: TComponent);
var
  x, y: integer;
  f: TCanvas;
begin
  inherited;
  vectorGroupList := TVectList.Create;
  vectorGroupList.lblAkcja := lblAkcja;

  sbMain.OnScroll := mainImageScroll;
  sbZoom.OnScroll := zoomImageScroll;

  imgMain.Picture.LoadFromFile('C:\Users\mudia\Desktop\t3.bmp');
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

procedure TMainForm.init;
begin
  gridColor := RGB(249, 192, 192);
end;

procedure TMainForm.Load1Click(Sender: TObject);
begin
  if dlgPicture.Execute then
  begin
    imageName := dlgPicture.FileName;
    imgMain.Picture.LoadFromFile(imageName);
  end;
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

procedure TMainForm.Open1Click(Sender: TObject);
begin
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

procedure TMainForm.R2V1Click(Sender: TObject);
begin
  InfoAkcja('Wczytywanie obrazka.');
  Screen.Cursor := crHourGlass;
  vectorGroupList.Clear;
  vectorGroupList.ReadFromImg(imgMain);
  imgZoom.Width := imgMain.Width;
  imgZoom.Height := imgMain.Height;
  //vectorList2.FillImgWithRect(imgZoom, lpZoom, chkGrid.Checked, gridColor);

  vectorGroupList.groupRect;
  vectorGroupList.makeEdgesForRect;
  Screen.Cursor := crDefault;
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
  Screen.Cursor := crHourGlass;
  vectorGroupList.ReadFromImg(imgMain);
  DoZoom;
  Screen.Cursor := crDefault;
end;

procedure TMainForm.DoZoom;
var
  tmpBmp: TBitmap;
  scrollHorPos, scrollVerPos: double;
  prvLpZoom: integer;
begin

  Screen.Cursor := crHourGlass;
  try
    saveZoomPos;
    prvLpZoom := lpZoom;
    scrollHorPos := sbZoom.HorzScrollBar.Position;
    scrollVerPos := sbZoom.VertScrollBar.Position;
    //zapisyje do zmiennej glob. nowy poziom zoomu 2^x
    lpZoom := round(Math.Power(2.0, tbZoom.Position-1));
    lpActImgZoom := lpZoom;

    //tego nie rozumiem, ale jest potrzebane do wyœwietlania
    SetMapMode(imgZoom.Canvas.Handle, MM_ISOTROPIC);
    SetWindowExtEx(imgZoom.Canvas.Handle, 1, 1, nil);
    SetViewportExtEx(imgZoom.Canvas.Handle, lpZoom, lpZoom, nil);

    if not chkPolyRect.checked then
      //wype³nia bitmapê grafik¹
      tmpBmp := vectorGroupList.FillImgWithRect(imgZoom, lpZoom, chkTestColor.Checked, chkGrid.Checked, gridColor)
    else
      //wype³nia bitmapê grafikê
      tmpBmp := vectorGroupList.FillImgWithPolygons(imgZoom, lpZoom, chkTestColor.Checked, chkGrid.Checked, gridColor);

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

procedure TMainForm.Exit1Click(Sender: TObject);
begin
  Close;
end;

procedure TMainForm.GridColor1Click(Sender: TObject);
begin
  if cdGrid.execute then
    gridColor := cdGrid.Color;
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

end.
