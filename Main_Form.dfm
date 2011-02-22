object MainForm: TMainForm
  Left = 0
  Top = 0
  Caption = 'Ras2Vec'
  ClientHeight = 635
  ClientWidth = 877
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  KeyPreview = True
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
    Left = 284
    Top = 168
    Width = 1
    Height = 313
    Caption = 'Panel1'
    TabOrder = 4
  end
  object Panel2: TPanel
    Left = 120
    Top = 320
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
  object ToolBar1: TToolBar
    Left = 0
    Top = 23
    Width = 877
    Height = 29
    Caption = 'ToolBar1'
    TabOrder = 6
  end
  object chkGrid: TCheckBox
    Left = 16
    Top = 72
    Width = 97
    Height = 17
    Caption = 'Grid'
    Color = clGrayText
    ParentColor = False
    TabOrder = 7
  end
  object chkTestColor: TCheckBox
    Left = 16
    Top = 104
    Width = 97
    Height = 17
    Caption = 'kolor testowy'
    TabOrder = 8
  end
  object btn1: TButton
    Left = 472
    Top = 104
    Width = 75
    Height = 25
    Caption = 'btn1'
    TabOrder = 12
    OnClick = btn1Click
  end
  object chkPolyRect: TCheckBox
    Left = 16
    Top = 136
    Width = 177
    Height = 17
    Caption = 'polygons (yes) / rectangles (no)'
    TabOrder = 14
  end
  object dlgPicture: TOpenPictureDialog
    Left = 804
    Top = 44
  end
  object dxBarManager1: TdxBarManager
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = []
    Bars = <
      item
        Caption = 'main'
        DockedDockingStyle = dsTop
        DockedLeft = 0
        DockedTop = 0
        DockingStyle = dsTop
        FloatLeft = 347
        FloatTop = 173
        FloatClientWidth = 23
        FloatClientHeight = 22
        ItemLinks = <
          item
            Item = sbiPlik
            Visible = True
          end
          item
            BeginGroup = True
            Item = sbiImage
            Visible = True
          end
          item
            BeginGroup = True
            Item = btnView
            Visible = True
          end>
        Name = 'mainTB'
        OneOnRow = True
        Row = 0
        UseOwnFont = False
        Visible = True
        WholeRow = False
      end>
    Categories.Strings = (
      'Default'
      'File'
      'Image'
      'View')
    Categories.ItemsVisibles = (
      2
      2
      2
      2)
    Categories.Visibles = (
      True
      True
      True
      True)
    PopupMenuLinks = <>
    UseSystemFont = True
    Left = 760
    Top = 40
    DockControlHeights = (
      0
      0
      23
      0)
    object sbiPlik: TdxBarSubItem
      Caption = 'File'
      Category = 1
      Visible = ivAlways
      ItemLinks = <
        item
          Item = btnOpen
          Visible = True
        end
        item
          BeginGroup = True
          Item = btnExit
          Visible = True
        end>
    end
    object btnOpen: TdxBarButton
      Caption = 'Open'
      Category = 1
      Hint = 'Open'
      Visible = ivAlways
      OnClick = btnOpenClick
    end
    object btnExit: TdxBarButton
      Caption = 'Exit'
      Category = 1
      Hint = 'Exit'
      Visible = ivAlways
      OnClick = btnExitClick
    end
    object sbiImage: TdxBarSubItem
      Caption = 'Image'
      Category = 2
      Visible = ivAlways
      ItemLinks = <
        item
          Item = dlgLoad
          Visible = True
        end
        item
          Item = btmR2V
          Visible = True
        end>
    end
    object dlgLoad: TdxBarButton
      Caption = 'Load'
      Category = 2
      Hint = 'Load'
      Visible = ivAlways
      OnClick = dlgLoadClick
    end
    object btmR2V: TdxBarButton
      Caption = 'R2V'
      Category = 2
      Hint = 'R2V'
      Visible = ivAlways
      OnClick = btmR2VClick
    end
    object btnView: TdxBarSubItem
      Caption = 'View'
      Category = 3
      Visible = ivAlways
      ItemLinks = <
        item
          Item = btnGridColor
          Visible = True
        end>
    end
    object btnGridColor: TdxBarButton
      Caption = 'Grid color'
      Category = 3
      Hint = 'Grid color'
      Visible = ivAlways
      OnClick = btnGridColorClick
    end
  end
  object cdGrid: TColorDialog
    Left = 800
    Top = 88
  end
end
