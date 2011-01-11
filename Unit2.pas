unit Unit2;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, dxBar, ExtDlgs, ExtCtrls;

type
  TForm2 = class(TForm)
    Image1: TImage;
    ScrollBox1: TScrollBox;
    PaintBoxMain: TPaintBox;
    dlgPicture: TOpenPictureDialog;
    dxBarManager1: TdxBarManager;
    sbiPlik: TdxBarSubItem;
    btnOpen: TdxBarButton;
    btnExit: TdxBarButton;
    sbiImage: TdxBarSubItem;
    dlgLoad: TdxBarButton;
    procedure btnExitClick(Sender: TObject);
    procedure dlgLoadClick(Sender: TObject);
  private
    { Private declarations }
    imageName: String;
  public
    { Public declarations }
  end;

  var
    Form2: TForm2;

implementation

{$R *.dfm}

procedure TForm2.dlgLoadClick(Sender: TObject);
begin
  if dlgPicture.Execute then
  begin
    imageName := dlgPicture.FileName;
    paintBoxMain.Canvas.
    paintBoxMain.Repaint;
  end;
end;

procedure TForm2.btnExitClick(Sender: TObject);
begin
  Close;
end;

end.
