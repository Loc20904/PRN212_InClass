   M S B A M L   `   `   `  ����L  HNguyenThanhlocWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WPFApp.PageWPFApp.Page  5      !    WPFApp.Page.RoomManagerPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:WPFApp.Page     5      .��5	      � $��20q�6   ���#F8F9FA  � RowDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      .+�5   
   �� ���#FFFFFF �� Padding$ 10q�6&    �� CornerRadius$ 6��63   $��0 0 0 10q�6D   ���#DDD$��1q�6i   ._�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $	��200��6   $��35��5      $��14(�5      $��8q�6(   $��0 0 10 0q�5      ��5      �� *	Keyword+ �� UpdateSourceTrigger$ PropertyChanged=�5      �� 5      $��
➕ Create��6   $��90��5      $��35��6$   $��0 0 6 0q�60   ���#4CAF50
�������$��0q�5   A    � Cursor$
 Hand��5      ��5      �� *CreateCommand+5      �� 5      $��✏ Edit��6   $��90��5      $��35��6$   $��0 0 6 0q�60   ���#FF9800
�������$��0q�5    A   $
 Hand��5!      ��5      �� *EditCommand+5!      �� 5"      $��🗑 Delete��6   $��90��5$      $��35��6$   ���#F44336
�������$��0q�5%   A   $
 Hand��5&      ��5#      �� *DeleteCommand+5&      5'      5(      &   System.Windows.Controls.DataGrid  5+   
   $��1��6      AutoGenerateColumns$ False��5.         CanUserAddRows$ False��5/         
IsReadOnly$
 True��50      $��14(�51      	   	RowHeight$	 32Q�52      
   HeadersVisibility$
 Column=�53      ���#DDD   GridLinesVisibility$ Vertical=�55      
�������$��1q�57      $��0q�58      ��5,      �� *FilteredRooms+ �� SelectedItem 5-      �� *SelectedRoom+ �� Mode$ TwoWay=�   Columns 59      0  *System.Windows.Controls.DataGridTextColumn  5:      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 40 6R   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *RoomId+6R     5;      $ Number��6%   $	 100 6V    67   �� *
RoomNumber+6V     5<      $ Description��6%   $ 2* 6d    6:   �� *RoomDetailDescription+6d     5=      $
 Type��6%   $ * 6a    67   �� *RoomType.RoomTypeName+6a     5>      $ Capacity��6%   $ 80 6[    67   �� *RoomMaxCapacity+6[     5?      $ Price (VND)��6%   $	 100 6o    6:   �� *RoomPricePerDay+ �� StringFormat$ N0��6o     5@      $ Status��6%   $ 80 6V    67   �� *
RoomStatus+6V   5C      5D      5E      