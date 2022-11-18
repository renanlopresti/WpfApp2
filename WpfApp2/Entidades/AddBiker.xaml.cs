using System;
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
    /// Lógica interna para AddBiker.xaml
    /// </summary>
    public partial class AddBiker : Window
    {
        public AddBiker()
        {
            InitializeComponent();
        }
        public void bnt_salvar(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
