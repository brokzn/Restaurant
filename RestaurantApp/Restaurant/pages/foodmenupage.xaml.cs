using Restaurant.Model;
using Restaurant.Wforms.MenuForms;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Xml.Linq;
using System.Data;
using System.Windows.Input;


namespace Restaurant.pages
{
    /// <summary>
    /// Логика взаимодействия для foodmenupage.xaml
    /// </summary>
    public partial class foodmenupage : Page
    {
        public foodmenupage()
        {
            InitializeComponent();
        }

        private void RotateMainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new menupage());
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)

        {

            DataGridMenu.ItemsSource = AppData.db.Menu.ToList();
            RowCount.Content = DataGridMenu.Items.Count.ToString();
        }
        private void LoadDataGridMenu()
        {
            DataGridMenu.ItemsSource = AppData.db.Menu.ToList();

            RowCount.Content = DataGridMenu.Items.Count.ToString();
        }
        private void DeleteRowDataGridStorage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы действительно хотите удалить выбранные блюда?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var selectedItems = DataGridMenu.SelectedItems;
                    if (selectedItems != null && selectedItems.Count > 0)
                    {
                        foreach (var item in selectedItems)
                        {
                            var currentUser = item as Restaurant.Model.Menu;
                            AppData.db.Menu.Remove(currentUser);
                        }
                        AppData.db.SaveChanges();

                        DataGridMenu.ItemsSource = AppData.db.Menu.ToList();
                        MessageBox.Show("Успешно", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        RowCount.Content = DataGridMenu.Items.Count.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Выбранные блюда используются в таблице Заказы, см. раздел |Заказы| главного меню", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void AddRowDataGridStorage_Click(object sender, RoutedEventArgs e)
        {

            Window windowToOpen = new AddMenu();
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
            DataGridMenu.ItemsSource = AppData.db.Menu.ToList();
            RowCount.Content = DataGridMenu.Items.Count.ToString();

        }
        private void DeleteRowToolButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы действительно хотите удалить это блюдо?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var selectedItems = DataGridMenu.SelectedItems;
                    if (selectedItems != null && selectedItems.Count > 0)
                    {
                        var currentUser = selectedItems[0] as Restaurant.Model.Menu;
                        AppData.db.Menu.Remove(currentUser);
                        AppData.db.SaveChanges();
                        DataGridMenu.ItemsSource = AppData.db.Menu.ToList();
                        MessageBox.Show("Успешно", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        RowCount.Content = DataGridMenu.Items.Count.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Выбранное блюдо используется в таблице Заказы, см. раздел |Заказы| главного меню", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void NewWindow_Closed(object sender, EventArgs e)
        {
            LoadDataGridMenu();
        }
        private void SearchDataGrid_Click(object sender, RoutedEventArgs e)
        {
            var searchText = DataFilter.Text.ToLower();


            ICollectionView view = CollectionViewSource.GetDefaultView(DataGridMenu.ItemsSource);
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
                        var product = (Restaurant.Model.Menu)item;

                        return product.Food_Name.ToLower().Contains(searchText);
                  //          || product.Cost.ToLower().Contains(searchText);



                    }
                };
            }
        }

        private async void EditRowToolButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = DataGridMenu.SelectedItem as Restaurant.Model.Menu;

            if (selectedRow != null)
            {
                var newWindow = new EditMenu(selectedRow.Dish_code);
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



        private void DataGridMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var dataGrid = sender as DataGrid;

            if (dataGrid != null)
            {
                var menu = (Model.Menu)dataGrid.SelectedItem;

                PreviewMenu(menu);

            }
            else
            {
                RowCount.Content = DataGridMenu.Items.Count.ToString();
            }


        }

        private void PreviewMenu(Model.Menu menu)
        {
            try
            {

                if (menu != null)
                {
                    FoodDescription.Visibility = Visibility.Hidden;
                    MenuNameLabel.Content = menu.Food_Name;
                    MenuCostLabel.Content = "Цена: " + menu.Cost + "р.";
                    MenuCTLabel.Content = "Время приготовления: " + menu.Cooking_time;
                    if (menu.Restaurant_Storage != null)
                    {
                        try
                        {
                        Ing1Label.Content = menu.Restaurant_Storage.Ingredient_Name;
                        Ing2Label.Content = menu.Restaurant_Storage1.Ingredient_Name;
                        Ing3Label.Content = menu.Restaurant_Storage2.Ingredient_Name;
                        }
                        catch
                        {
                            MessageBox.Show("Произошла ошибка обновления информации, нажмите ОК что бы продолжить", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка обновления информации о блюде, пожалуйста перезагрузите приложение", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        private void СloseDescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            FoodDescription.Visibility = Visibility.Hidden;
            var column = DescHide.ColumnDefinitions[2];
            column.Width = new GridLength(0);
        }

        private void InfoRowToolButton_Click(object sender, RoutedEventArgs e)
        {
            FoodDescription.Visibility = Visibility.Visible;
            var column = DescHide.ColumnDefinitions[2];
            column.Width = new GridLength(400);
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

                var filteredMenuList = AppData.db.Menu.ToList().Where(menu => menu.Cost >= costFrom && menu.Cost <= costTo).ToList();

                DataGridMenu.ItemsSource = filteredMenuList;
                var Newcount = DataGridMenu.Items.Count.ToString();
                RowCount.Content = "фильтр - цена: " + Newcount;
            }
            else
            {
                DataGridMenu.ItemsSource = AppData.db.Menu.ToList();
                RowCount.Content = DataGridMenu.Items.Count.ToString();
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
