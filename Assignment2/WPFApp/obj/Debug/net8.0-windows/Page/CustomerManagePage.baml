   M S B A M L   `   `   `  ����L  HNguyenThanhlocWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WPFApp.PageWPFApp.Page  5      3clr-namespace:WPFApp.ViewModelWPFApp.ViewModel  5      $    WPFApp.Page.CustomerManagePage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:WPFApp.Page     5      -vmclr-namespace:WPFApp.ViewModel     5      .��5
      � $��20q�6     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   $��0 0 0 12q�6;   .+�5      �� $	��200��6   $��30��5      $��14(�5      $��6q�5      ���#F3F4F6���#D1D5DB$��0 0 10 0q�5       � ToolTip$, &🔍 Search by name, email or phone...��5      ��5      �� *	Keyword+ �� UpdateSourceTrigger$ PropertyChanged=�5      �� 5      $��
➕ Create��6   $��90��5      $��30��6    $��0 0 6 0q�5      ���#4CAF50
�������$��0q�5    =    � Cursor$
 Hand��6Q   ��5      �� *CreateCommand+5    Q   �� 5"      $��✏️ Edit��6   $��90��5$      $��30��6    $��0 0 6 0q�5%      ���#2196F3
�������$��0q�5&   =   $
 Hand��6Q   ��5#      �� *EditCommand+5&   Q   �� 5(      $��🗑 Delete��6   $��90��5*      $��30��6    ���#E53935
�������$��0q�5+   =   $
 Hand��6Q   ��5)      �� *DeleteCommand+5+   Q   5,      &   System.Windows.Controls.DataGrid  5/   
   $��1��6      AutoGenerateColumns$ False��52         CanUserAddRows$ False��53         
IsReadOnly$
 True��54         	RowHeight$ 32Q�55      	   HeadersVisibility$	 Column=�56      $��13(�57      
   GridLinesVisibility$
 
Horizontal=�58         AlternatingRowBackground �#F9FAFB���#E0E0E0$��1q�5;      ��50      �� *FilteredCustomers+ �� SelectedItem 51      �� *SelectedCustomer+ �� Mode$ TwoWay=�   Columns 5=      0  *System.Windows.Controls.DataGridTextColumn  5>      ,  &System.Windows.Controls.DataGridColumn   Header$ 🆔 ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 40 6S   1  +System.Windows.Controls.DataGridBoundColumn   Binding 64   �� *
CustomerId+6S     5?      $ 👤 Full Name��6%   $ * 6`    6;   �� *CustomerFullName+6`     5@      $ 📞 Telephone��6%   $	 120 6Y    6;   �� *	Telephone+6Y     5A      $ 
📧 Email��6%   $ * 6X    67   �� *EmailAddress+6X     5B      $ 🎂 Birthday��6%   $	 110 6x    6:   �� *CustomerBirthday+ �� StringFormat$ 
dd/MM/yyyy��6x     5C      $ 
✅ Status��6%   $ 80 6Z    67   �� *CustomerStatus+6Z   5E      5F      5G      