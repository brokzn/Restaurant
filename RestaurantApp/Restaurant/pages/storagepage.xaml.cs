using Restaurant.Model;
using Restaurant.Wforms.StorageForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
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
using System.Xml.Linq;

namespace Restaurant.pages
{
    /// <summary>
    /// Логика взаимодействия для storagepage.xaml
    /// </summary>
    public partial class storagepage : Page
    {

        
        public storagepage()
        {
            InitializeComponent();
        }

        
        private void RotateMainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new menupage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGridProduct();

            
        }

        private void LoadDataGridProduct()
        {
            DataGridProduct.ItemsSource = AppData.db.Restaurant_Storage.ToList();

            RowCount.Content = DataGridProduct.Items.Count.ToString();
        }


      
        private void DeleteRowDataGridStorage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы действительно хотите удалить выбранные продукты?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var selectedItems = DataGridProduct.SelectedItems;
                    if (selectedItems != null && selectedItems.Count > 0)
                    {
                        foreach (var item in selectedItems)
                        {
                            var currentUser = item as Restaurant_Storage;
                            AppData.db.Restaurant_Storage.Remove(currentUser);
                        }
                        AppData.db.SaveChanges();

                        DataGridProduct.ItemsSource = AppData.db.Restaurant_Storage.ToList();
                        MessageBox.Show("Успешно", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        RowCount.Content = DataGridProduct.Items.Count.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Выбранные продукты используются в таблице Меню, см. раздел |Меню ресторана| главного меню" , "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



       
        private void AddRowDataGridStorage_Click(object sender, RoutedEventArgs e)
        {
            Window windowToOpen = new AddStorage();
            windowToOpen.Owner = Application.Current.MainWindow;
            double centerX = Application.Current.MainWindow.Left + Application.Current.MainWindow.Width / 2;
            double centerY = Application.Current.MainWindow.Top + Application.Current.MainWindow.Height / 2;
            double left = centerX - windowToOpen.Width / 2;
            double top = centerY - windowToOpen.Height / 2;
            windowToOpen.Left = left;
            windowToOpen.Top = top;
            windowToOpen.ShowDialog();
        }

        private void UpdateDataGrid_Click(object sender, RoutedEventArgs e)
        {
            DataGridProduct.ItemsSource = AppData.db.Restaurant_Storage.ToList();
            RowCount.Content = DataGridProduct.Items.Count.ToString();
        }

        private void DeleteRowToolButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы действительно хотите удалить выбранный продукт?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var selectedItems = DataGridProduct.SelectedItems;
                    if (selectedItems != null && selectedItems.Count > 0)
                    {
                        var currentUser = selectedItems[0] as Restaurant_Storage;
                        AppData.db.Restaurant_Storage.Remove(currentUser);
                        AppData.db.SaveChanges();
                        DataGridProduct.ItemsSource = AppData.db.Restaurant_Storage.ToList();
                        MessageBox.Show("Успешно", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        RowCount.Content = DataGridProduct.Items.Count.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Выбранный продукт используется в таблице Меню, см. раздел |Меню ресторана| главного меню", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void EditRowToolButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = DataGridProduct.SelectedItem as Restaurant_Storage;

            if (selectedRow != null)
            {
                var newWindow = new EditStorage(selectedRow.Ingredient_Code);
                newWindow.Closed += NewWindow_Closed;
                newWindow.Owner = Application.Current.MainWindow;
                double centerX = Application.Current.MainWindow.Left + Application.Current.MainWindow.Width / 2;
                double centerY = Application.Current.MainWindow.Top + Application.Current.MainWindow.Height / 2;
                double left = centerX - newWindow.Width / 2;
                double top = centerY - newWindow.Height / 2;
                newWindow.Left = left;
                newWindow.Top = top;
                newWindow.ShowDialog();

            }
        }

        private void NewWindow_Closed(object sender, EventArgs e)
        {
            LoadDataGridProduct();
        }

        private void SearchDataGrid_Click(object sender, RoutedEventArgs e)
        {
            var searchText = DataFilter.Text.ToLower();


            ICollectionView view = CollectionViewSource.GetDefaultView(DataGridProduct.ItemsSource);
            if (view != null)
            {
                view.Filter = item =>
                {
                    if (string.IsNullOrEmpty(DataFilter.Text))
                    {

                        return true;
                    }
                    else
                    {
                        var product = (Restaurant_Storage)item;

                        return product.Ingredient_Name.ToLower().Contains(searchText)
                            || product.Supplier.ToLower().Contains(searchText) 
                            || product.Amount.ToLower().Contains(searchText) 
                            || product.Shelf_Life.ToLower().Contains(searchText) 
               //             || product.Cost.ToLower().Contains(searchText) 
                            || product.Date_of_Issue.ToString().ToLower().Contains(searchText);


                    }
                };
            }
        }


        bool isIconRotated = false;

        private void CostFilterDataGrid_Click(object sender, RoutedEventArgs e)
        {

            if (isIconRotated)
            {
                CostPopUpIcon.RenderTransform = new RotateTransform(0);
                FilterSettings.Visibility = Visibility.Hidden;
            }
            else
            {
                FilterSettings.Visibility = Visibility.Visible;
                CostPopUpIcon.RenderTransformOrigin = new Point(0.5, 0.5);
                CostPopUpIcon.RenderTransform = new RotateTransform(180);
            }

            isIconRotated = !isIconRotated;
        }
        private void ApplyFilter()
        {

            if (!string.IsNullOrEmpty(CostFromTB.Text) && !string.IsNullOrEmpty(CostToTB.Text))
            {
                int costFrom = Convert.ToInt32(CostFromTB.Text);
                int costTo = Convert.ToInt32(CostToTB.Text);

                var filteredMenuList = AppData.db.Restaurant_Storage.ToList().Where(storage => storage.Cost >= costFrom && storage.Cost <= costTo).ToList();

                DataGridProduct.ItemsSource = filteredMenuList;
                var Newcount = DataGridProduct.Items.Count.ToString();
                RowCount.Content = "фильтр - цена: " + Newcount;
            }
            else
            {
                DataGridProduct.ItemsSource = AppData.db.Menu.ToList();
                RowCount.Content = DataGridProduct.Items.Count.ToString();
            }

        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }
        private void FirstCostRadiusCB_Checked(object sender, RoutedEventArgs e)
        {
            CostFromTB.Text = "0";
            CostToTB.Text = "500";
        }

        private void SecondCostRadiusCB_Checked(object sender, RoutedEventArgs e)
        {
            CostFromTB.Text = "500";
            CostToTB.Text = "1000";
        }

        private void ThirdCostRadiusCB_Checked(object sender, RoutedEventArgs e)
        {
            CostFromTB.Text = "1000";
            CostToTB.Text = "1500";
        }

        private void FourCostRadiusCB_Checked(object sender, RoutedEventArgs e)
        {
            CostFromTB.Text = "1500";
            CostToTB.Text = "2000";
        }

        private void CostFromTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilter();
            FirstCostRadiusCB.IsChecked = false;
            SecondCostRadiusCB.IsChecked = false;
            ThirdCostRadiusCB.IsChecked = false;
            FourCostRadiusCB.IsChecked = false;
        }

        private void CostToTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilter();
            FirstCostRadiusCB.IsChecked = false;
            SecondCostRadiusCB.IsChecked = false;
            ThirdCostRadiusCB.IsChecked = false;
            FourCostRadiusCB.IsChecked = false;
        }
    }
}
