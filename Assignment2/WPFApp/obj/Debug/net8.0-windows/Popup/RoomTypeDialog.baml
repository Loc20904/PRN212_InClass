   M S B A M L   `   `   `  ����L  HNguyenThanhlocWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:WPFApp.PopupWPFApp.Popup  5   	   !    WPFApp.Popup.RoomTypeDialog   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:WPFApp.Popup     5   	     � Title$  Room Type Details��5   	   $	��380��5	   	   $	��400��6   ���#F9FAFB � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�5   	   .��5      � $��
30,30,30,7q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��🏷 Room Type Name��6!   $
��Bold"�6:   $��0,0,0,5q�6L   �� 5   
   -   5        $��1��5      $��0,0,0,12q�5      $��14(�5      $��6q�6    $��30��6,   ��5      �� *RoomType.RoomTypeName+5   ,   �� 5"   
   $��2��6   $��📝 Description��6!   $
��Bold"�67   $��0,0,0,5q�6I   �� 5#   
   $��3��6   $��60��5%      $��0,0,0,12q�6   $��14(�5&      $��6q�6     �� AcceptsReturn$
 True��6,   ��5$      �� *RoomType.TypeDescription+5&   ,   �� 5)   
   $��4��6   $��	🗒 Note��6!   $
��Bold"�60   $��0,0,0,5q�6B   �� 5*   
   $��5��6   $��60��5,      $��0,0,0,12q�6   $��14(�5-      $��6q�6    $
 True��6,   ��5+      �� *RoomType.TypeNote+5-   ,   �� 50   
   $��6��6    �� Orientation$ 
Horizontal=�51      $��Center=�52      $��0,12,0,0q�53      .+�54      �� -   5        $��	💾 Save��54       �� 	IsDefault$
 True��55      $��90��56      $��35��6    ���#4CAF50
�������$
��Bold"�58   =   $��0,0,10,0q�59       � Cursor$
 Hand��6'   �� 5:      $��
❌ Cancel��6    �� IsCancel$
 True��5;      $��90��5<      $��35��6    ���#E53935
�������$
��Bold"�5=   =   $
 Hand��5>      5?      5@      5A      