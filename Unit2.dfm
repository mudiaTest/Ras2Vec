object Form2: TForm2
  Left = 0
  Top = 0
  Caption = 'Form2'
  ClientHeight = 635
  ClientWidth = 877
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object sbMain: TScrollBox
    Left = 520
    Top = 184
    Width = 289
    Height = 273
    TabOrder = 1
    object imgMain: TImage
      Left = 0
      Top = 0
      Width = 41
      Height = 41
      AutoSize = True
    end
    object PaintBoxMain: TPaintBox
      Left = 0
      Top = 0
      Width = 284
      Height = 268
      OnPaint = PaintBoxMainPaint
    end
  end
  object tbZoom: TTrackBar
    Left = 232
    Top = 512
    Width = 150
    Height = 45
    Min = 1
    Position = 1
    TabOrder = 3
    OnChange = tbZoomChange
  end
  object sbZoom: TScrollBox
    Left = 144
    Top = 184
    Width = 281
    Height = 273
    TabOrder = 5
    object imgZoom: TImage
      Left = 0
      Top = 0
      Width = 200
      Height = 200
    end
  end
  object edtZoom: TdxMaskEdit
    Left = 388
    Top = 512
    Width = 25
    TabOrder = 7
    EditMask = '0;1;_'
    IgnoreMaskBlank = False
    Text = ' '
    StoredValues = 4
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
          end>
        Name = 'mainTB'
        OneOnRow = True
        Row = 0
        UseOwnFont = False
        Visible = True
        WholeRow = False
      end>
    Categories.Strings = (
      'Default')
    Categories.ItemsVisibles = (
      2)
    Categories.Visibles = (
      True)
    PopupMenuLinks = <>
    UseSystemFont = True
    Left = 304
    Top = 88
    DockControlHeights = (
      0
      0
      23
      0)
    object sbiPlik: TdxBarSubItem
      Caption = 'File'
      Category = 0
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
      Category = 0
      Hint = 'Open'
      Visible = ivAlways
      OnClick = btnOpenClick
    end
    object btnExit: TdxBarButton
      Caption = 'Exit'
      Category = 0
      Hint = 'Exit'
      Visible = ivAlways
      OnClick = btnExitClick
    end
    object sbiImage: TdxBarSubItem
      Caption = 'Image'
      Category = 0
      Visible = ivAlways
      ItemLinks = <
        item
          Item = dlgLoad
          Visible = True
        end>
    end
    object dlgLoad: TdxBarButton
      Caption = 'Load'
      Category = 0
      Hint = 'Load'
      Visible = ivAlways
      OnClick = dlgLoadClick
    end
  end
end
