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
using Microsoft.SqlServer.Server;

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

        private void Button_Click_AddProduct(object sender, RoutedEventArgs e)
        {
            NpgsqlCommand cmd = Connection.GetCommand("INSERT INTO  \"product\" (\"name\") VALUES ( @name)");
            cmd.Parameters.AddWithValue("@name", NpgsqlDbType.Varchar, tbProduct.Text.Trim());

            var result = cmd.ExecuteNonQuery();
            if (result == 0) 
            {
                MessageBox.Show("error");
            }
            tbProduct.Clear();
            Connection.products.Clear();
            CbBindingProduct();
        }

        private void lvProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvProduct.SelectedItem != null)
            {
                tbProduct.Text = (lvProduct.SelectedItem as ClassProduct).name.ToString();
            }
        }

        private void Button_Click_UpdateProduct(object sender, RoutedEventArgs e)
        {
            int ID = (lvProduct.SelectedItem as ClassProduct).id;
            string product =tbProduct.Text.Trim();

            NpgsqlCommand cmd = Connection.GetCommand("UPDATE \"product\" SET \"name\"= @name where id = @id");
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, ID);
            cmd.Parameters.AddWithValue("@name", NpgsqlDbType.Varchar, product);
            var result = cmd.ExecuteNonQuery();
            if (result == 0) { return; }

            tbProduct.Clear();
            Connection.products.Clear();
            CbBindingProduct();
        }

        private void Button_Click_DeleteProduct(object sender, RoutedEventArgs e)
        {
            ClassProduct classProduct = lvProduct.SelectedItem as ClassProduct;

            NpgsqlCommand cmd = Connection.GetCommand("DELETE FROM \"product\" WHERE id = @id");
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, classProduct.id);
            cmd.Parameters.AddWithValue("@name", NpgsqlDbType.Varchar, classProduct.name);
            var result = cmd.ExecuteNonQuery();
            if (result != 0)
            {
                Connection.products.Remove(lvProduct.SelectedItem as ClassProduct);
                CbBindingProduct();
            }

            Connection.products.Clear();
            CbBindingProduct();
        }
    }
}
