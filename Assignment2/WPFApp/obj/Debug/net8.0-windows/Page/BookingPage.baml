   M S B A M L   `   `   `  ����L  HNguyenThanhlocWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WPFApp.PageWPFApp.Page  5      3clr-namespace:WPFApp.ViewModelWPFApp.ViewModel  5          WPFApp.Page.BookingPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:WPFApp.Page     5      -vmclr-namespace:WPFApp.ViewModel     5        � 	Resources  5
      1   +WPFApp.ViewModel.BookingStatusTextConverter   BookingStatusTextConverter��%   &	          5   
   6(   5      .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   $��0 0 0 12q�6;   .+�5      �� $	��200��6   $��30��5      $��14(�5      $��6q�5      ���#F3F4F6���#D1D5DB$��0 0 10 0q�5       � ToolTip$( "🔍 Search by customer or date...��5      ��5      �� *	Keyword+ �� UpdateSourceTrigger$ PropertyChanged=�5      �� 5       $��
➕ Create��6   $��90��5"      $��30��6    $��0 0 6 0q�5#      ���#4CAF50
�������$��0q�5$   =    � Cursor$
 Hand��6Q   ��5!      �� *CreateCommand+5$   Q   �� 5&      $��✏️ Edit��6   $��90��5(      $��30��6    $��0 0 6 0q�5)      ���#2196F3
�������$��0q�5*   =   $
 Hand��6Q   ��5'      �� *EditCommand+5*   Q   �� 5,      $��🗑 Delete��6   $��90��5.      $��30��6    ���#E53935
�������$��0q�5/   =   $
 Hand��6Q   ��5-      �� *DeleteCommand+5/   Q   50      &   System.Windows.Controls.DataGrid  53   
   $��1��6      AutoGenerateColumns$ False��56         CanUserAddRows$ False��57         
IsReadOnly$
 True��58      	   	RowHeight$	 32Q�59      
   HeadersVisibility$
 Column=�5:      $��13(�5;         GridLinesVisibility$ 
Horizontal=�5<         AlternatingRowBackground �#F9FAFB���#E0E0E0$��1q�5?      ��54      �� *FilteredBookings+ �� SelectedItem 55      �� *SelectedBooking+ �� Mode$ TwoWay=�   Columns 5A      0  *System.Windows.Controls.DataGridTextColumn  5B      ,  &System.Windows.Controls.DataGridColumn   Header$ 🆔 ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 6]   1  +System.Windows.Controls.DataGridBoundColumn   Binding 64   �� *BookingReservationId+6]     5C      $ 📅 Booking Date��6%   $	 110 6^    6>   �� *BookingDate+6^     5D      $ 💵 Total Price��6%   $	 120 6m    6=   �� *
TotalPrice+ �� StringFormat$ N0��6m     5E      $ 👤 Customer��6%   $ * 6h    6:   �� *Customer.CustomerFullName+6h     5F      $ 📦 Status��6%   $	 100 6�    68   �� *BookingStatus+ �� 	Converter# [  6�   4  .System.Windows.Controls.DataGridTemplateColumn  5G      $ 📄 Detail��6)   $ 80 6<      CellTemplate 5H      �� 5I      .R�5J      �� $��Detail��6%   $��4q�5M      $��12(�6!   ���#E3F2FD���#BBDEFB$
 Hand��5N   @   ��5K      �� *DataContext.DetailCommand+ �� RelativeSource ��  �� AncestorType Page��5L      �� 5N   @   5O      5Q      5S      5T      5U      