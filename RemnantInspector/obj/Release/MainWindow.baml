   M S B A M L   `   `   `  ���� K  GRemnantInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:RemnantInspectorRemnantInspector  G(clr-namespace:RemnantInspector.ConverterRemnantInspector.Converter  !    RemnantInspector.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:RemnantInspector     <convert(clr-namespace:RemnantInspector.Converter       � WindowStartupLocation$  Manual=� � Title$ RemnantInspector��$
��Auto��$	��350�� � WindowStyle$
 None=� � ShowInTaskbar .  � AllowsTransparency . 
������  � SizeToContent$ Height=� � Topmost .  � 
ResizeMode$ NoResize=� � Icon$3 -Assets/iconfinder_handyArtboard_4_3044135.png��$
��8000��.��� 	 � RowDefinitions	 �� $
��Auto ��� $��* �.+��� -   
 ��Name$
 lstData��$��1��
������  �� SelectionMode$ Single=�$��0q�   OverlayItemListStyle#��[  $	��350��$
��Auto��$��Disabled=�$��Disabled=� J� IsHitTestVisible .  ���� *RemnantDataObjects+