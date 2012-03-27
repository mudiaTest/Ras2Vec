unit Main_Thread;

interface

uses
  Main_Obj,
  OtlCommon, OtlComm, OtlSync, OtlTask, OtlTaskControl,
  OtlCollections, OtlParallel;

const
  OW_DO_R2V = 1;
  MSG_DO_SEND_MESSAGE = 2;

type

  TR2VOmniWorker = class (TOmniWorker)
  public
    //zawiera
    vectorGroupList: TVectList;
    procedure OMSendMessage(msg: String); {message MSG_DO_SEND_MESSAGE;}
    procedure OMDoWork(var msg: TOmniMessage); message OW_DO_R2V;
    function Initialize: boolean; override;
  end;

implementation

{ TRas2Vec }

function TR2VOmniWorker.Initialize: boolean;
begin
  vectorGroupList.otWorker := Self;
  result := true;
end;

procedure TR2VOmniWorker.OMDoWork(var msg: TOmniMessage);
begin
  Task.ClearTimer(1);
  vectorGroupList.groupRect;
  vectorGroupList.makeEdgesForRect;
end;

procedure TR2VOmniWorker.OMSendMessage(msg: String);
begin
  Task.Comm.Send(0, msg);
end;

end.
