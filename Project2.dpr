program Project2;

uses
  Forms,
  Main_Form in 'Main_Form.pas' {MainForm},
  Form_utl in 'Form_utl.pas',
  Sys_utl in 'Sys_utl.pas',
  Main_Obj in 'Main_Obj.pas',
  Main_Thread in 'Main_Thread.pas',
  Sys_Const in 'Sys_Const.pas',
  Register_Obj in 'Register_Obj.pas',
  Main_CV in 'Main_CV.pas',
  Ini_Obj in 'Ini_Obj.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.MainFormOnTaskbar := True;
  Application.CreateForm(TMainForm, MainForm);
  Application.Run;
end.
