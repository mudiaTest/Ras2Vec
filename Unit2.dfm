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
  object Image1: TImage
    Left = 460
    Top = 168
    Width = 349
    Height = 273
  end
  object PaintBoxMain: TPaintBox
    Left = 36
    Top = 168
    Width = 345
    Height = 273
  end
  object ScrollBox1: TScrollBox
    Left = 36
    Top = 484
    Width = 185
    Height = 41
    TabOrder = 0
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
