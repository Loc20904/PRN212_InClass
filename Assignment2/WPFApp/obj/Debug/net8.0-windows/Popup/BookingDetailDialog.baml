   M S B A M L   `   `   `  ����L  HNguyenThanhlocWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:WPFApp.ViewModelWPFApp.ViewModel  5   	   &     WPFApp.Popup.BookingDetailDialog   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -vmclr-namespace:WPFApp.ViewModel     5   	     � Title$  Booking Detail��5   	   $	��500��5   	   $	��600��6    � WindowStartupLocation$ CenterOwner=�5   	   ���#F9FAFB � 	Resources 5	      1   +WPFApp.ViewModel.BookingStatusTextConverter   StatusConverter2   ,WPFApp.ViewModel.DateOnlyToDateTimeConverter  DateOnlyConverter��%:   &	        &	        5
   
   6(     5   
   6)   5      .��5      �� $
��Auto=�6   .��5   
   �� $��20q�6   
�������.+�5      �� $��📋 Booking Information��6   $
��Bold"�67   $��0 0 0 20q�6I   .��5      �� $��10q�6   .+�5      ��  �� Orientation$ 
Horizontal=�6!   $	��0 5q�6:   .+�5      �� $��📅 Booking Date:��6$   $	��140��6<   $��SemiBold"�6H   �� 5      ��6$   �� *Booking.BookingDate+5      �� 5      $ 
Horizontal=�6!   $	��0 5q�6:   .+�5      �� $��💰 Total Price:��6$   $	��140��6;   $��SemiBold"�6G   �� 5      ��6$   �� *Booking.TotalPrice+ �� StringFormat$ N0��5      �� 5       $ 
Horizontal=�6!   $	��0 5q�6:   .+�5!      �� $��📦 Status:��6$   $	��140��66   $��SemiBold"�6B   �� 5"      ��6$   �� *Booking.BookingStatus+ �� 	Converter# [  5#      �� 5%      $ 
Horizontal=�6!   $	��0 5q�6:   .+�5&      �� $��👤 Customer Name:��6$   $	��140��6=   $��SemiBold"�6I   �� 5'      ��6$   �� *#!Booking.Customer.CustomerFullName+5(      5)      5*      �� 5-      $��🛏 Room Details��6   $
��Bold"�60   .��5.      &   System.Windows.Controls.DataGrid     AutoGenerateColumns$ False��5/         
IsReadOnly$
 True��50         AlternatingRowBackground �#F3F4F6���#D1D5DB$��1q�53      	   	RowHeight$	 34Q�54      $��14(�55      $
��0 10q�56      ��5.      �� *Booking.BookingDetails+
   Columns
 57      0  *System.Windows.Controls.DataGridTextColumn  58      ,  &System.Windows.Controls.DataGridColumn   Header$ 	🏨 Room��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6b   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6>   �� *Room.RoomNumber+6b     59      $ 
📅 Start��6-   $	 120 5;       5:      �� *	StartDate+# [ 5;        5<      $ 📆 End��6-   $	 120 5>       5=      �� *	EndDate+# [ 5>        5@      $ 💵 Price (VND)��6-   $	 130 6v    6E   �� *ActualPrice+$ N0��6v   5B      5C      �� 5F      $ 
Horizontal=�6   $��Right=�62   $��0 20 0 0q�6N   .+�5G      �� -   5        $��✅ Confirm��5G      $	��100��5I      ���#3B82F6
�������$��SemiBold"�5L       � Cursor$
 Hand��5M      5N      5O      5P      5Q      