   M S B A M L   `   `   `  ����L  HNguyenThanhlocWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:WPFApp.PopupWPFApp.Popup  5   	   !    WPFApp.Popup.CustomerDialog   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:WPFApp.Popup     5   	     � Title$  Customer Details��5   	   $	��567��5	   	   $	��420��6    � WindowStartupLocation$ CenterScreen=�5
   	    � 
ResizeMode$ NoResize=�5   	   ���#F9FAFB � 	Resources 5      .   (WPFApp.Popup.DateOnlyToDateTimeConverter   DateOnlyConverter��%   &	          5   
   6,   5      .��5      � $��30q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5       $
��Auto �6   �� 5!      $
��Auto �6   �� 5#      $
��Auto �6   �� 5$      $
��Auto �6   .+�5)   
   �� $��0��6   $��👤 Full Name��6!   $
��Bold"�65   $��0,0,0,4q�6G   �� 5*   
   -   5        $��1��5*      $��14(�5-      $��6q�6    $��30��6,   ���#FFFFFF���#D1D5DB��5+      �� *Customer.CustomerFullName+5.   '   �� 51   
   $��2��6   $��📞 Telephone��6!   $
��Bold"�65   $��0,10,0,4q�6G   �� 52   
   -   5        $��3��52      $��14(�55      $��6q�6    $��30��6,   ���#FFFFFF���#D1D5DB��53      �� *Customer.Telephone+56   '   �� 59   
   $��4��6   $��
📧 Email��6!   $
��Bold"�61   $��0,10,0,4q�6C   �� 5:   
   $��5��6   $��14(�5<      $��6q�6    $��30��6,   ���#FFFFFF���#D1D5DB��5;      �� *Customer.EmailAddress+5=   '   �� 5@   
   $��6��6   $��🎂 Birthday��6!   $
��Bold"�64   $��0,10,0,4q�6F   (  "System.Windows.Controls.DatePicker  5A   
   $��7��6   $��30��5C      $��14(�6   ���#FFFFFF���#D1D5DB   SelectedDate 5B      �� *Customer.CustomerBirthday+ �� 	Converter# [   �� Mode$ TwoWay=�5D   "   �� 5I   
   $��10��6   $��0,10,0,0q�6#   .+�5J      �� $��🔒 Password��6   $
��Bold"�6+   $��0,0,0,4q�6=   � 5K      .+�5M      G� -   5         G�Name$ PasswordBoxHidden��5M      $��30��5N      $��0,0,0,30q�5O      $��14(�5P      $��6q�6,   ���#FFFFFF���#D1D5DB5Q   3   �� 5S      -   5        	 ��Name$	 PasswordBoxVisible��5S      $y�	Collapsed=�5T      $��30��5U      $��0,0,0,30q�6&   $��14(�5V      $��6q�6(   ���#FFFFFF���#D1D5DB5W   /   �� 5Y      -   5        $��👁 Hiện mật khẩu��5Y      $��Bottom=�5\      $��0,33,0,0q�66   5]      5^      �� 5a   
   $��11��6   
 �� Orientation$
 
Horizontal=�5b      $��Center=�5c      $��0,20,0,0q�5d      .+�5e      �� -   5        $��	💾 Save��5e      $��90��5f      $��35��6    ���#4CAF50
�������$
��Bold"�5i      $��0,0,10,0q�6'   �� 5j      $��
❌ Cancel��6   $��90��5k      $��35��6     �� IsCancel$
 True��5l      ���#E53935
�������$
��Bold"�5n      5o      5p      5q      