unit Main_Thread;

interface

uses
  Main_Obj
  {$IFNDEF VER185}
  ,OtlCommon, OtlComm, OtlSync, OtlTask, OtlTaskControl
  ,OtlCollections, OtlParallel
  {$ENDIF};

const
  OW_DO_R2V = 1;
  MSG_DO_SEND_MESSAGE = 2;
  {$IFNDEF VER185}
type
  TR2VOmniWorker = class (TOmniWorker)
  public
    //zawiera
    mapFactory: TSeparateThreadVectList;
    procedure OMSendMessage(msg: String); {message MSG_DO_SEND_MESSAGE;}
    procedure OMDoWork(var msg: TOmniMessage); message OW_DO_R2V;
    function Initialize: boolean; override;
  end;
  {$ENDIF}
implementation

{$IFNDEF VER185}
{ TRas2Vec }

function TR2VOmniWorker.Initialize: boolean;
begin
  mapFactory.otWorker := Self;
  result := true;
end;

procedure TR2VOmniWorker.OMDoWork(var msg: TOmniMessage);
begin
  Task.ClearTimer(1);
  mapFactory.GroupRect;
  mapFactory.MakeEdgesForGroups;
end;

procedure TR2VOmniWorker.OMSendMessage(msg: String);
begin
  Task.Comm.Send(0, msg);
end;
{$ENDIF}

end.
