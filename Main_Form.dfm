object MainForm: TMainForm
  Left = 0
  Top = 0
  Caption = 'Ras2Vec'
  ClientHeight = 695
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
  object sbMain: TScrollBox
    Left = 520
    Top = 184
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
    Top = 504
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
    Top = 184
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
    Top = 164
    Width = 1
    Height = 313
    Caption = 'Panel1'
    TabOrder = 4
  end
  object Panel2: TPanel
    Left = 119
    Top = 284
    Width = 337
    Height = 1
    Caption = 'Panel2'
    TabOrder = 5
  end
  object edtZoom: TEdit
    Left = 431
    Top = 504
    Width = 25
    Height = 21
    MaxLength = 1
    TabOrder = 3
    Text = ' '
  end
  object chkGrid: TCheckBox
    Left = 16
    Top = 72
    Width = 97
    Height = 17
    Caption = 'Grid / Edge'
    Color = clGrayText
    ParentColor = False
    TabOrder = 6
  end
  object chkTestColor: TCheckBox
    Left = 16
    Top = 104
    Width = 97
    Height = 17
    Caption = 'kolor testowy'
    TabOrder = 7
  end
  object btn1: TButton
    Left = 472
    Top = 104
    Width = 75
    Height = 25
    Caption = 'btn1'
    TabOrder = 8
    OnClick = btn1Click
  end
  object chkPolyRect: TCheckBox
    Left = 16
    Top = 136
    Width = 177
    Height = 17
    Caption = 'polygons (yes) / rectangles (no)'
    TabOrder = 9
  end
  object btnZoomIn: TButton
    Left = 104
    Top = 489
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
    Top = 520
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
    Left = 208
    Top = 68
    Width = 75
    Height = 25
    Caption = 'DoZoom'
    TabOrder = 12
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 584
    Top = 80
    Width = 75
    Height = 25
    Caption = 'btn1'
    TabOrder = 13
    OnClick = btn1Click
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
    Left = 120
    Top = 32
    object N1: TMenuItem
      Caption = '-'
    end
    object Main1: TMenuItem
      Caption = 'Main'
    end
    object Other1: TMenuItem
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
end
