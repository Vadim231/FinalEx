using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace FinalExam
{
    public partial class MainWindow : Window
    {
        ProductContext context = new ProductContext();
        bool isInsertMode = false;
        bool isBeingEdited = false;

        public MainWindow()
        {
            InitializeComponent();
            var pr = context.Products.ToList();
            dataGrid1.ItemsSource = pr;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var pr = context.Products.ToList();
            dataGrid1.ItemsSource = pr;
        }

        


        private void dataGrid1_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            Product product = new Product();
            Product emp = e.Row.DataContext as Product;
            if (isInsertMode)
            {
                var InsertRecord = MessageBox.Show("Do you want to add " + emp.Name + " as a new emploee?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (InsertRecord == MessageBoxResult.Yes)
                {
                    product.Id = emp.Id;
                    product.Name = emp.Name;
                    product.Count = emp.Count;
                    product.Price = emp.Price;
                    context.Products.Add(product);
                    context.SaveChanges();
                }
                var pr = context.Products.ToList();
                dataGrid1.ItemsSource = pr;
            }
            context.SaveChanges();
        }
        
        

        private void DataGrid1_AddingNewItem_1(object sender, AddingNewItemEventArgs e)
        {
            isInsertMode = true;
        }

        private void DataGrid1_BeginningEdit_1(object sender, DataGridBeginningEditEventArgs e)
        {
            isBeingEdited = true;
        }
        private void DataGrid1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && !isBeingEdited)
            {
                MessageBox.Show("123");
                var grid = (DataGrid)sender;
                if (grid.SelectedItems.Count > 0)
                {
                    var Res = MessageBox.Show("Are you sure you want to delete " + grid.SelectedItems.Count + " Employees?", "Deleting Records", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if (Res == MessageBoxResult.Yes)
                    {
                        foreach (var row in grid.SelectedItems)
                        {
                            Product product = row as Product;
                            context.Products.Remove(product);
                        }
                        context.SaveChanges();
                        MessageBox.Show(grid.SelectedItems.Count + " Employees have being deleted!");
                    }
                    else
                    {
                        var pr = context.Products.ToList();
                        dataGrid1.ItemsSource = pr;
                    }
                }
            }
        }
    }
}
