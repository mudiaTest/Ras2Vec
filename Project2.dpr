program Project2;

uses
  Forms,
  Main_Form in 'Main_Form.pas' {MainForm},
  Form_utl in 'Form_utl.pas',
  Sys_utl in 'Sys_utl.pas',
  Main_Obj in 'Main_Obj.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.MainFormOnTaskbar := True;
  Application.CreateForm(TMainForm, MainForm);
  Application.Run;
end.
