   M S B A M L   `   `   `  ����L  HNguyenThanhlocWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:WPFApp.ViewModelWPFApp.ViewModel  5      #    WPFApp.Page.AvailableRoomPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      -vmclr-namespace:WPFApp.ViewModel     5      Aav2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        N� Title$  Available Room��5	       N� 
Background �#F9FAFB � 	Resources 5      2   ,WPFApp.ViewModel.DateOnlyToDateTimeConverter   DateOnlyConverter0   *WPFApp.ViewModel.BoolToVisibilityConverter  BoolToVisibilityConverter��%:   &	        &	        5   
   6)     5   
   6'   5      .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��🛏 FUMiniHotel��6!   $��28(�5      $
��Bold"�6"   ���#1F2937$��0 0 0 20q�5   )   �� 5   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $��0 0 0 12q�6;   .+�5       �� $��📅 Start:��6   $��Center=�6)   $	��0 5q�6D   (  "System.Windows.Controls.DatePicker  5!      $	��140��5"      $��5q�6%      SelectedDate 5!      �� *FilterStartDate+ �� 	Converter# [   �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5"   %   �� 5$      $��	📆 End:��6   $��Center=�6'   $	��0 5q�6B     5%      $	��140��5&      $��5q�6%    5%      �� *FilterEndDate+# [  $ TwoWay=�$ PropertyChanged=�5&   %   �� 5(      $��🔍 Filter��6   $
��10 0q�5)      $
��10 4q�6#   ���#3B82F6
�������$��0q�5*   =   $��SemiBold"�5+      	 � Cursor$
	 Hand��6+   ��5(   )   �� *FilterCommand+5+   +   �� 5-      $��🛏 Book Selected Rooms��6   $
��10 0q�5.      $
��10 4q�6#   ���#10B981
�������$��0q�5/   =   $��SemiBold"�50      $
	 Hand��6+   ��5-   6   �� *BookCommand+50   +   51      &   System.Windows.Controls.DataGrid  54   
   -   5        
  Name$
 RoomDataGrid��55      $��2��54         AutoGenerateColumns$ False��58         SelectionMode$ Single=�59         CanUserAddRows$ False��5:         
IsReadOnly$ False��5;         GridLinesVisibility$ 
Horizontal=�5<         AlternatingRowBackground �#F3F4F6���#D1D5DB$��1q�5?         	RowHeight$ 34Q�5@      $��14(�5A      ��57      �� *Rooms+   Columns 5C      4  .System.Windows.Controls.DataGridCheckBoxColumn  5D      ,  &System.Windows.Controls.DataGridColumn   Header$	 ✔��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ 40 5E   z   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   �� *
IsSelected+$ TwoWay=�$ PropertyChanged=�6z   0	  *System.Windows.Controls.DataGridTextColumn	  5F      $ Room #��6%      
IsReadOnly$
 True��5G   I    6%   �� *Room.RoomNumber+6I   	  5H      $
 Type��6%   $
 True��5I   T    6%   �� *Room.RoomType.RoomTypeName+6T   	  5J      $ 	Price/Day��6%   $
 True��5K   _    6%   �� *Room.RoomPricePerDay+ �� StringFormat$ N0��6_   4
  .System.Windows.Controls.DataGridTemplateColumn
  5N      $ 
Start Date��6)   $	 140 6=    
  CellTemplate 5O      �� 5P      .R�5Q         6)   �� *	StartDate+# [  $ TwoWay=�$ PropertyChanged=�y�5R   )   �� *
IsSelected+# [ 5S      5U      
  5X      $ End Date��6)   $	 140 6;    5Y      �� 5Z      .R�5[         6)   �� *	EndDate+# [  $ TwoWay=�$ PropertyChanged=�y�5\   )   �� *
IsSelected+# [ 5]      5_      5a      �� 5d   
   $��3��6   $��5 10 0 0q�6!   $��Italic$�5e      
��� ���$n��h👉 Select your rooms, enter check-in/check-out dates, and then click 'Book Selected Rooms' to reserve.��5g      5h      5i      