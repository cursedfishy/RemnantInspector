   M S B A M L   `   `   `  ���� K  GRemnantInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:RemnantInspectorRemnantInspector  G(clr-namespace:RemnantInspector.ConverterRemnantInspector.Converter      RemnantInspector.App   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     0localclr-namespace:RemnantInspector     <convert(clr-namespace:RemnantInspector.Converter       �� 	Resources  B   <RemnantInspector.Converter.IsMissingToForegroundColorConvert   IsMissingToForegroundColor>   8RemnantInspector.Converter.IsMissingItemToOpacityConvert  IsMissingItemToOpacityG   ARemnantInspector.Converter.IsAllItemsCompletedToVisibilityConvert # IsAllItemsCompletedToVisibilityN   HRemnantInspector.Converter.IsBossDescriptionAvailableToVisibilityConvert * &IsBossDescriptionAvailableToVisibilityA   ;RemnantInspector.Converter.EventLocationToVisibilityConvert " EventLocationToVisibilityGroup  EventItemListStyle �� 
TargetType �� Property �� Value �� 	Converter �� 
TargetType � ColumnDefinitions � RowDefinitions J� Opacity	 �� StringFormat  OverlayItemListStyle
 �� CornerRadius J� SnapsToDevicePixels J� IsHitTestVisible 
� GradientOrigin 
� RadiusX � Offset � Color �� Orientation �� DisplayMemberPath��%�  &	        &	      &	 
     &	      &	      &	      7   7  7  &	     7  7  7  7            �� " ��.���  � ��
 ���� ��  � �� �� *IsMissingItem+8   ��  � ��$ SemiBold"���  � ��$ 10(���  �@J�Opacity �� *IsMissingItem+8  ��  �@J�IsHitTestVisible .  ��  � �� �� " ��.R��  �� $
��Auto ��� $��* ��� $
��Auto � �� $��* ��� $
��Auto �.+��� $��70��$��0,0,8,0q�$��0��$��0��$
��Left=�$��Center=����� *
ItemType+�� $��1��$��0��$
��Left=�$��Center=����� *ItemDisplayName+�� $��2��$��0��$
��Left=�$��Center=����� *IsMissingItemDisplay+�� $��0��$��3��$��1��$
��Left=�$��Center=�$	 .60Q�$��10,0,0,0q�$
��Wrap=����#ffffff���� *ItemDescription+$	 ↳ {0}��y��� *DisplayItemInfo+8  �� " ��.���  � ��$ 13(���  � ��
 ���� ��  � ��
 �������  � ��$ SemiBold"���  � ��$	 0,2q���  �@J�Opacity$ 1Q���  �@J�IsHitTestVisible .  ��  � y� �� *EventMainZone+8   ��  � �� �� " ��.R��� $
 3�� .  .  ��
� $ 0.0,0.0&�$ 2Q�.��� $	 0.0Q�$ 	#48131013��� $	 0.5Q�$ 	#48131013��� $	 1.0Q�$ 	#480d0a0d��._��� $ Vertical=�.+��� $ Vertical=�$
��10,5q�.+�� $��5q� �� $
��Auto ��� $��* �.+��� $ Vertical=�$��0��.+��� $
��Left=�$��Center=�#��z��$��Light"����#ffffff . $	 0.7Q�$��0,0,8,0q�$��75������ *	EventType+�� $
��Left=�$��Center=�$��11(�$��Light"����#ffffff . $	 0.7Q����� *EventLocation+�� $��Right=�$��Center=�#��z��#��z�����#ffffff . $	 1.0Q�$��1������ *
EventTitle+�� $ Vertical=�y��� *IsAllItemsCompleted+8  .+��� 8�� ���#ffffff$ ItemDisplayName��$��0q�
������ $��Disabled=�$��Disabled=����� *
EventItems+�� $ Vertical=�y��� *IsBossDescriptionAvailable+8  .+��� $��Stretch=�$��Center=�$��5q�$��1�����#ffffff$
 0.25Q��� $��5q�$	 0.7Q�$
��Left=�$��Center=�$��10(�$
��Wrap=�$��Regular"� . ���� *EventBossDescription+