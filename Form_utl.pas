unit Form_utl;

interface

uses
  Forms, Messages;

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

implementation

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

end.
