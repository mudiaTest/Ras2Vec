object MainForm: TMainForm
  Left = 0
  Top = 0
  Caption = 'Ras2Vec'
  ClientHeight = 558
  ClientWidth = 877
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  KeyPreview = True
  Menu = mmToolBar1
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object lblAkcja: TLabel
    Left = 40
    Top = 504
    Width = 36
    Height = 13
    Caption = 'lblAkcja'
  end
  object lblTime: TLabel
    Left = 40
    Top = 523
    Width = 32
    Height = 13
    Caption = 'lblTime'
  end
  object lbl1: TLabel
    Left = 368
    Top = 112
    Width = 8
    Height = 13
    Caption = '>'
  end
  object lbl2: TLabel
    Left = 368
    Top = 83
    Width = 8
    Height = 13
    Caption = '>'
  end
  object sbMain: TScrollBox
    Left = 520
    Top = 144
    Width = 289
    Height = 273
    TabOrder = 0
    object imgMain: TImage
      Left = 0
      Top = 0
      Width = 105
      Height = 89
      AutoSize = True
      OnMouseDown = imgMouseDown
      OnMouseMove = imgMouseMove
      OnMouseUp = imgMouseUp
    end
    object PaintBoxMain: TPaintBox
      Left = 0
      Top = 0
      Width = 49
      Height = 49
      OnMouseDown = imgMouseDown
      OnMouseMove = imgMouseMove
      OnMouseUp = imgMouseUp
      OnPaint = PaintBoxMainPaint
    end
  end
  object tbZoom: TTrackBar
    Left = 144
    Top = 464
    Width = 281
    Height = 29
    Max = 8
    Min = 1
    Position = 1
    TabOrder = 1
    OnChange = tbZoomChange
    OnKeyPress = tbZoomKeyPress
  end
  object sbZoom: TScrollBox
    Left = 144
    Top = 144
    Width = 281
    Height = 273
    TabOrder = 2
    OnMouseWheel = sbZoomMouseWheel
    object imgZoom: TImage
      Left = 0
      Top = 0
      Width = 105
      Height = 89
      OnMouseDown = imgMouseDown
      OnMouseMove = imgMouseMove
      OnMouseUp = imgMouseUp
    end
    object PaintBoxZoom: TPaintBox
      Left = 0
      Top = 0
      Width = 49
      Height = 49
      OnMouseDown = imgMouseDown
      OnMouseMove = imgMouseMove
      OnMouseUp = imgMouseUp
      OnPaint = PaintBoxMainPaint
    end
  end
  object Panel1: TPanel
    Left = 243
    Top = 124
    Width = 1
    Height = 313
    Caption = 'Panel1'
    TabOrder = 4
  end
  object Panel2: TPanel
    Left = 119
    Top = 244
    Width = 337
    Height = 1
    Caption = 'Panel2'
    TabOrder = 5
  end
  object edtZoom: TEdit
    Left = 431
    Top = 464
    Width = 25
    Height = 21
    MaxLength = 1
    TabOrder = 3
    Text = ' '
  end
  object chkGrid: TCheckBox
    Left = 16
    Top = 32
    Width = 97
    Height = 17
    Caption = 'Grid / Edge'
    Color = clGrayText
    ParentColor = False
    TabOrder = 6
  end
  object chkTestColor: TCheckBox
    Left = 16
    Top = 64
    Width = 97
    Height = 17
    Caption = 'kolor testowy'
    TabOrder = 7
  end
  object btn1: TButton
    Left = 224
    Top = 8
    Width = 102
    Height = 25
    Caption = 'MainThread'
    TabOrder = 8
    OnClick = btn1Click
  end
  object chkPolyRect: TCheckBox
    Left = 16
    Top = 96
    Width = 177
    Height = 17
    Caption = 'polygons (yes) / rectangles (no)'
    TabOrder = 9
  end
  object btnZoomIn: TButton
    Left = 104
    Top = 449
    Width = 25
    Height = 25
    Caption = '+'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -19
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 11
    OnClick = btnZoomInClick
  end
  object btnZoomOut: TButton
    Left = 104
    Top = 480
    Width = 25
    Height = 25
    Caption = '-'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -19
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentFont = False
    TabOrder = 10
    OnClick = btnZoomOutClick
  end
  object Button1: TButton
    Left = 431
    Top = 8
    Width = 75
    Height = 25
    Caption = 'DoZoom'
    TabOrder = 12
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 224
    Top = 39
    Width = 102
    Height = 25
    Caption = 'SeparateThread'
    TabOrder = 13
    OnClick = Button2Click
  end
  object btnStopR2V: TButton
    Left = 343
    Top = 39
    Width = 75
    Height = 25
    Action = actR2VBtnStop
    TabOrder = 14
  end
  object Button3: TButton
    Left = 343
    Top = 8
    Width = 75
    Height = 25
    Action = actR2VBtnStop
    Caption = 'R2V'
    TabOrder = 15
    OnClick = Button3Click
  end
  object btnSave: TButton
    Left = 590
    Top = 8
    Width = 75
    Height = 25
    Caption = 'Save'
    TabOrder = 16
    OnClick = btnSaveClick
  end
  object edtLeftUpX: TEdit
    Left = 224
    Top = 80
    Width = 121
    Height = 21
    TabOrder = 17
    Text = '18,489034'
  end
  object edtLeftUpY: TEdit
    Left = 224
    Top = 107
    Width = 121
    Height = 21
    TabOrder = 18
    Text = '54,492328'
  end
  object edtRightDownX: TEdit
    Left = 400
    Top = 80
    Width = 121
    Height = 21
    TabOrder = 19
    Text = '18,500565'
  end
  object edtRightDownY: TEdit
    Left = 400
    Top = 107
    Width = 121
    Height = 21
    TabOrder = 20
    Text = '54,489520'
  end
  object dlgPicture: TOpenPictureDialog
    Left = 804
    Top = 44
  end
  object cdGrid: TColorDialog
    Left = 800
    Top = 88
  end
  object mmToolBar1: TMainMenu
    Left = 128
    Top = 24
    object N1: TMenuItem
      Caption = '-'
    end
    object MainMG: TMenuItem
      Action = actR2VMenu
      Caption = 'Main'
    end
    object OtherMG: TMenuItem
      Action = actR2VMenu
      Caption = 'Other'
      object Open1: TMenuItem
        Caption = 'Open'
        OnClick = Open1Click
      end
      object Load1: TMenuItem
        Caption = 'Load'
        OnClick = Load1Click
      end
      object Tylkoread1: TMenuItem
        Caption = 'Tylko read'
        OnClick = Tylkoread1Click
      end
      object R2V1: TMenuItem
        Caption = 'R2V'
        OnClick = R2V1Click
      end
      object GridColor1: TMenuItem
        Caption = 'Grid Color'
        OnClick = GridColor1Click
      end
    end
    object Exit1: TMenuItem
      Caption = 'Exit'
      OnClick = Exit1Click
    end
  end
  object oemR3V: TOmniEventMonitor
    OnTaskMessage = oemR3VTaskMessage
    OnTaskTerminated = oemR3VTaskTerminated
    Left = 680
    Top = 464
  end
  object MainActionList: TActionList
    Left = 8
    Top = 208
    object actR2VBtnStop: TAction
      Caption = 'R2V Stop'
      OnUpdate = actR2VBtnStopExecute
    end
    object actR2VMenu: TAction
      Caption = 'actR2VMenu'
      OnUpdate = actR2VMenuExecute
    end
  end
  object SaveDialog: TSaveDialog
    Left = 608
    Top = 48
  end
end
