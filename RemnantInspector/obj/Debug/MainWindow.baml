   M S B A M L   `   `   `  ����K  GRemnantInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:RemnantInspectorRemnantInspector  5   	   G(clr-namespace:RemnantInspector.ConverterRemnantInspector.Converter  5   	   !    RemnantInspector.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:RemnantInspector     5   	   <convert(clr-namespace:RemnantInspector.Converter     5   	     � WindowStartupLocation$  Manual=�5       � Title$ RemnantInspector��5	   	   $
��Auto��6"   $	��350��60    � WindowStyle$
 None=�6<    � ShowInTaskbar .  � AllowsTransparency . 
������  � SizeToContent$ Height=�5   	    � Topmost .  � 
ResizeMode$ NoResize=�6/    � Icon$3 -Assets/iconfinder_handyArtboard_4_3044135.png��6E   $
��8000��5   	   .��5      � 	 � RowDefinitions	 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� -   5        
 ��Name$
 lstData��5      $��1��6#   
������  �� SelectionMode$ Single=�6I   $��0q�6`      OverlayItemListStyle#��[  5      $	��350��6M   $
��Auto��6Y   $��Disabled=�5      $��Disabled=�6H    J� IsHitTestVisible .  ��5   +   �� *RemnantDataObjects+5      5      