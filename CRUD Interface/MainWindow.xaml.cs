using Npgsql;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using NpgsqlTypes;
using System.Data.Common;

namespace CRUD_Interface
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();

            Connection.Connect("localhost", "5432", "postgres", "1234", "CRUD");

            CbBindingProduct();
        }
        public void CbBindingProduct()
        {
            Binding binding = new Binding
            { Source = Connection.products };
            lvProduct.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            Connection.SelectTableProduct();
        }

    }
}
