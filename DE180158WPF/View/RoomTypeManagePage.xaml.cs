﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DE180158WPF.ViewModel;

namespace DE180158WPF.View
{
    /// <summary>
    /// Interaction logic for RoomTypeManagePage.xaml
    /// </summary>
    public partial class RoomTypeManagerPage : Page
    {
        public RoomTypeManagerPage()
        {
            InitializeComponent();
            DataContext = new RoomTypeManagerViewModel();
        }
    }
}
