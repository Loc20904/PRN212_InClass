   M S B A M L   `   `   `  ����L  HNguyenThanhlocWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WPFApp.PageWPFApp.Page  5      3clr-namespace:WPFApp.ViewModelWPFApp.ViewModel  5      %    WPFApp.Page.RoomTypeManagerPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:WPFApp.Page     5      -vmclr-namespace:WPFApp.ViewModel     5        N� 
Background  �#F9FAFB.��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   $��0 0 0 12q�6;   .+�5      �� $	��200��6   $��14(�6"   $��0 0 10 0q�5      $��32��5      $��6q�6"   $��Center=�5      ��5      �� *	Keyword+ �� UpdateSourceTrigger$ PropertyChanged=�5      �� 5      $��
➕ Create��6   $��90��5      $��32��6    $��0,0,6,0q�6,   ���#4CAF50
�������$��SemiBold"�5   =   ��5   (   �� *CreateCommand+5   =   �� 5      $��✏ Edit��6   $��90��5      $��32��6    $��0,0,6,0q�6,   ���#2196F3
�������$��SemiBold"�5   =   ��5   &   �� *EditCommand+5   =   �� 5!      $��🗑 Delete��6   $��90��5"      $��32��6    ���#F44336
�������$��SemiBold"�5#   =   ��5!   )   �� *DeleteCommand+5#   =   5$      &   System.Windows.Controls.DataGrid  5'   
   $��1��6      AutoGenerateColumns$ False��5*         CanUserAddRows$ False��5+         
IsReadOnly$
 True��5,         	RowHeight$ 36Q�5-         ColumnHeaderHeight$ 36Q�5.      $��14(�5/      	   GridLinesVisibility$
	 None=�50      
   HeadersVisibility$
 Column=�51      $��0q�52         AlternatingRowBackground �#F0F0F0���#D1D5DB$��1q�54   )   ��5(      �� *FilteredRoomTypes+ �� SelectedItem 5)      �� *SelectedRoomType+ �� Mode$ TwoWay=�   Columns 55      0  *System.Windows.Controls.DataGridTextColumn  56      ,  &System.Windows.Controls.DataGridColumn   Header$ 🆔 ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 6S   1  +System.Windows.Controls.DataGridBoundColumn   Binding 64   �� *
RoomTypeId+6S     57      $ 	🏷 Name��6%   $	 200 6W    66   �� *RoomTypeName+6W     58      $ 📝 Description��6%   $ * 6a    6=   �� *TypeDescription+6a     59      $ 	🗒 Note��6%   $ * 6S    66   �� *
TypeNote+6S   5;      5<      5=      