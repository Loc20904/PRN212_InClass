   M S B A M L   `   `   `  ����L  HNguyenThanhlocWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:WPFApp.PopupWPFApp.Popup  5   	   3clr-namespace:WPFApp.ViewModelWPFApp.ViewModel  5   	       WPFApp.Popup.RoomDialog   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:WPFApp.Popup     5   	   1localsclr-namespace:WPFApp.ViewModel     5   	     � Title$  Room Details��5	   	   $	��470��5
   	   $	��420��6    � WindowStartupLocation$ CenterScreen=�5   	   ���#F9FAFB � 	Resources 5      1   +WPFApp.ViewModel.IntToNullableByteConverter   IntToNullableByteConverter��%   &	          5   
   6,   5      .��5      � $��30,30,30,11q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5    
   �� $��0��6   $��🆔 Room Number��6!   $��SemiBold"�67   �� 5!   
   -   5        $��1��5!      $��30��5#      $��0,4,0,8q�6   $��6q�6/   ��5!      �� *Room.RoomNumber+5#   /   �� 5&   
   $��2��6   $��📝 Description��6!   $��SemiBold"�67   �� 5'   
   $��3��6   $��60��5(       �� 	MaxLength$	 200��6    �� TextWrapping$
 Wrap=�6.   $��0,4,0,8q�5)      $��6q�6#   ��5'      �� *Room.RoomDetailDescription+5)   #   �� 5,   
   $��4��6    �� Orientation$ 
Horizontal=�6"   $��0,6,0,8q�6;   .+�5-      �� $	��150��6   .+�5.      �� $��👥 Capacity��6   $��SemiBold"�6/   �� 5/      -   5        $��30��51      $��6q�6&   $��0,4,0,0q�62   ��5/      �� *Room.RoomMaxCapacity+51   2   52      �� 53      $	��180��6   $��10,0,0,0q�6%   .+�54      �� $��💰 Price / Day��6   $��SemiBold"�62   �� 55      -   5        $��30��57      $��6q�6&   $��0,4,0,0q�62   ��55      �� *Room.RoomPricePerDay+57   2   58      59      �� 5<   
   $��5��6   $��🏷 Room Type��6!   $��SemiBold"�65   �� 5=   
   $��6��6    �� SelectedValuePath$ 
RoomTypeId��5?       �� DisplayMemberPath$ RoomTypeName��5@      $��30��5A      $��0,4,0,8q�6   ��5=       �� *	RoomTypes+	 �� SelectedValue	 5>      �� *Room.RoomTypeId+5A      �� 5D   
   $��7��6   $��🔘 Status��6!   $��SemiBold"�62   �� 5E   
   $��8��6   $��30��5G      $��0,4,0,8q�6   ��5E       �� *StatusOptions+	 5F      �� *Room.RoomStatus+
 �� 	Converter#
 [  5G      �� 5J   
   $��9��6   $ 
Horizontal=�6"   $��Center=�5K      $��0,16,0,0q�62   .+�5L      �� -   5        $��	💾 Save��5L       �� 	IsDefault$
 True��6'   $	��111��68   ���#4CAF50
�������$��0q�5N   =   $��6q�6Q   $��42��6\   �� 5O      $��
❌ Cancel��6    �� IsCancel$
 True��6(   $��90��68   ���#F44336
�������$��0q�5P   =   $��6q�6Q   5Q      5R      5S      