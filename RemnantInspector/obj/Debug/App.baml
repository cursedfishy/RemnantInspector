   M S B A M L   `   `   `  ����K  GRemnantInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:RemnantInspectorRemnantInspector  5      G(clr-namespace:RemnantInspector.ConverterRemnantInspector.Converter  5          RemnantInspector.App   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      0localclr-namespace:RemnantInspector     5      <convert(clr-namespace:RemnantInspector.Converter     5        �� 	Resources  5      B   <RemnantInspector.Converter.IsMissingToForegroundColorConvert   IsMissingToForegroundColor>   8RemnantInspector.Converter.IsMissingItemToOpacityConvert  IsMissingItemToOpacityG   ARemnantInspector.Converter.IsAllItemsCompletedToVisibilityConvert # IsAllItemsCompletedToVisibilityN   HRemnantInspector.Converter.IsBossDescriptionAvailableToVisibilityConvert * &IsBossDescriptionAvailableToVisibilityA   ;RemnantInspector.Converter.EventLocationToVisibilityConvert " EventLocationToVisibilityGroup  EventItemListStyle �� 
TargetType �� Property �� Value �� 	Converter �� 
TargetType � ColumnDefinitions � RowDefinitions J� Opacity	 �� StringFormat  OverlayItemListStyle
 �� CornerRadius J� SnapsToDevicePixels J� IsHitTestVisible 
� GradientOrigin 
� RadiusX � Offset � Color �� Orientation �� DisplayMemberPath��%  &	        &	      &	 &     &	 9     &	 L     &	 _     7   7  7  &	 /    7  7  7  7    5   
   64     5	   
   60     5
   
   69     5   
   6@     5   
   63   �� 5   
   " ��6+   .�5      ��  � ��
 ���� 6+   �� 5       � �� 6+   �� *IsMissingItem+8   �� 5       � ��$ SemiBold"�6+   �� 5       � ��$ 10(�6)   �� 5       �@J�Opacity 6(   �� *IsMissingItem+8  �� 5       �@J�IsHitTestVisible .  61   �� 5       � �� 5      �� 5      " ��6&   .R�5      �  5      �� 5   "   $
��Auto �63   �� 5   "   $��* �63   �� 5   "   $
��Auto �63    5      �� 5   "   $��* �60   �� 5    "   $
��Auto �60   .+�5"      �� $��70��6B   $��0,0,8,0q�6P   $��0��6a   $��0��6q   $
��Left=�6~   $��Center=�6�   ��6(   �� *
ItemType+6�   �� 5#      $��1��6I   $��0��6Y   $
��Left=�6g   $��Center=�6�   ��6(   �� *ItemDisplayName+6�   �� 5$      $��2��6N   $��0��6^   $
��Left=�6l   $��Center=�6�   ��6(   �� *IsMissingItemDisplay+6�   �� 5%      $��0��6_   $��3��6o   $��1��6�   $
��Left=�6�   $��Center=�6�   $	 .60Q�6�   $��10,0,0,0q�5&   (   $
��Wrap=�6:   ���#ffffff��5%   (   �� *ItemDescription+$	 ↳ {0}��y�5'   (   �� *DisplayItemInfo+8  6�   5(      5)      5+      5,      �� 5-   
   " ��6-   .�5.      ��  � ��$ 13(�6)   �� 5/       � ��
 ���� 6+   �� 50       � ��
 �����6+   �� 51       � ��$ SemiBold"�6+   �� 52       � ��$	 0,2q�6'   �� 53       �@J�Opacity$ 1Q�6(   �� 54       �@J�IsHitTestVisible .  61   �� 55       � y� 6+   �� *EventMainZone+8   �� 56       � �� 57      �� 58      " ��6&   .R�59      �� $
 3��6!    .  .  ��5:      
� 5;   "   $ 0.0,0.0&�66   $ 2Q�6O   .��5<   &   � $	 0.0Q�63   $ 	#48131013��6@   � 5=   &   $	 0.5Q�63   $ 	#48131013��6@   � 5>   &   $	 1.0Q�63   $ 	#480d0a0d��6@   5?   #   ._�5A      �� $ Vertical=�6)   .+�5D   "   �� $ Vertical=�6-   $
��10,5q�6D   .+�5E   &   � $��5q�6+    5F   *   �� 5G   .   $
��Auto �6?   �� 5H   .   $��* �6?   .+�5J   *   �� $ Vertical=�65   $��0��6L   .+�5K   .   �� $
��Left=�6S   $��Center=�6n   #��z��5L   0   $��Light"�6V   ���#ffffff . $	 0.7Q�5N   0   $��0,0,8,0q�6>   $��75��6O   ��5K   8   �� *	EventType+5N   O   �� 5O   .   $
��Left=�6W   $��Center=�6r   $��11(�5P   0   $��Light"�6>   ���#ffffff . $	 0.7Q�5R   0   ��5O   8   �� *EventLocation+5R   0   5S   +   �� 5U   *   $��Right=�6P   $��Center=�6l   #��z��5V   0   #��z��6V   ���#ffffff . $	 1.0Q�5Y   0   $��1��6>   ��5U   4   �� *
EventTitle+5Y   >   5Z   '   �� 5[   &   $ Vertical=�61   y�6H   �� *IsAllItemsCompleted+8  .+�5\   *   �� 8�� ���#ffffff$ ItemDisplayName��6�   $��0q�5]   .   
������ $��Disabled=�6[   $��Disabled=�6�   ��5\   2   �� *
EventItems+5]   �   �� 5^   *   $ Vertical=�65   y�6L   �� *IsBossDescriptionAvailable+8  .+�5_   .   �� $��Stretch=�68   $��Center=�6V   $��5q�6q   $��1��6|   ���#ffffff$
 0.25Q�6�   �� 5`   .   $��5q�6^   $	 0.7Q�6i   $
��Left=�6w   $��Center=�6�   $��10(�6�   $
��Wrap=�6�   $��Regular"�6�    . ��68   �� *EventBossDescription+6�   5a   +   5b   '   5c   #   5f      5g      5h      5j      5k      5n      5o      