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
using System.Windows.Shapes;

namespace WpfApp2.Entidades
{
    /// <summary>
    /// Lógica interna para AddTentativa.xaml
    /// </summary>
    public partial class AddTentativa : Window
    {
        public AddTentativa()
        {
            InitializeComponent();
            InitializeComboBox();
        }
        public void InitializeComboBox()
        {

            string[] enumElements = Enum.GetNames(typeof(Trick));

            foreach (var item in enumElements)
            {
                ComboBox1.Items.Add(item);
            }
        }
        public void bnt_salvar(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
