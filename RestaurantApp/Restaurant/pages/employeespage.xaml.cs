using Restaurant.Model;
using Restaurant.Wforms.EmployeesForms;
using Restaurant.Wforms.MenuForms;
using Restaurant.Wforms.StorageForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Restaurant.pages
{
    /// <summary>
    /// Логика взаимодействия для employeespage.xaml
    /// </summary>
    public partial class employeespage : Page
    {
       
        public employeespage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataGridEpm.ItemsSource = AppData.db.Restaurant_Employees.ToList();
            RowCount.Content = DataGridEpm.Items.Count.ToString();
           
        }
        private void LoadDataGridEpm()
        {
            DataGridEpm.ItemsSource = AppData.db.Restaurant_Employees.ToList();

            RowCount.Content = DataGridEpm.Items.Count.ToString();
        }
        private void DataGridEpm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dataGrid = sender as DataGrid;

            if (dataGrid != null)
            {
                var menu = (Model.Restaurant_Employees)dataGrid.SelectedItem;

                PreviewMenu(menu);

            }
            else
            {
                RowCount.Content = DataGridEpm.Items.Count.ToString();
            }
        }
        private void PreviewMenu(Model.Restaurant_Employees menu)
        {
            try
            {
                if (menu != null)
                {
                    EmpDescription.Visibility = Visibility.Hidden;
                    LastnameLabel.Content = "Фамилия: " + menu.Lastname;
                    FirstnameLabel.Content = "Имя: " + menu.Firstname;
                    MiddlenameLabel.Content = "Отчество: " + menu.Middlename;
                    AgeLabel.Content = "Возраст: " + menu.Age;
                    GenderLabel.Content = "Пол: " + menu.Gender;
                    PostCodeLabel.Content = "Должность: " + menu.Restaurant_Posts.Post_Name;
                    AdressLabel.Content = "Адрес: " + menu.Adress;
                    PhoneLabel.Content = "Телефон: " + menu.Phone;
                    PassportLabel.Content = "Номер пасспорта: " + menu.Passport;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка обновления информации о сотруднике, пожалуйста перезагрузите приложение", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void SearchDataGrid_Click(object sender, RoutedEventArgs e)
        {
            var searchText = DataFilter.Text.ToLower();


            ICollectionView view = CollectionViewSource.GetDefaultView(DataGridEpm.ItemsSource);
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
                        var emp = (Restaurant.Model.Restaurant_Employees)item;

                        return emp.Firstname.ToLower().Contains(searchText)
                            || emp.Middlename.ToLower().Contains(searchText)
                            || emp.Lastname.ToLower().Contains(searchText);



                    }
                };
            }
        }

        private void AddRowDataGridStorage_Click(object sender, RoutedEventArgs e)
        {
            
            Window windowToOpen = new AddEmployees();
            windowToOpen.Owner = Application.Current.MainWindow;
            double centerX = Application.Current.MainWindow.Left + Application.Current.MainWindow.Width / 2;
            double centerY = Application.Current.MainWindow.Top + Application.Current.MainWindow.Height / 2;
            double left = centerX - windowToOpen.Width / 2;
            double top = centerY - windowToOpen.Height / 2;
            windowToOpen.Left = left;
            windowToOpen.Top = top;
            windowToOpen.ShowDialog();
        }

        private void DeleteRowDataGridStorage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы действительно хотите удалить выбранных сотрудников?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var selectedItems = DataGridEpm.SelectedItems;
                    if (selectedItems != null && selectedItems.Count > 0)
                    {
                        foreach (var item in selectedItems)
                        {
                            var currentUser = item as Restaurant.Model.Restaurant_Employees;
                            AppData.db.Restaurant_Employees.Remove(currentUser);
                        }
                        AppData.db.SaveChanges();

                        DataGridEpm.ItemsSource = AppData.db.Restaurant_Employees.ToList();
                        MessageBox.Show("Успешно", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        RowCount.Content = DataGridEpm.Items.Count.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Выбранные сотрудники используются в таблице Заказы, см. раздел |Заказы| главного меню", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateDataGrid_Click(object sender, RoutedEventArgs e)
        {
            DataGridEpm.ItemsSource = AppData.db.Restaurant_Employees.ToList();
            RowCount.Content = DataGridEpm.Items.Count.ToString();
        }

        

        private void СloseDescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            EmpDescription.Visibility = Visibility.Hidden;
            var column = DescHide.ColumnDefinitions[2];
            column.Width = new GridLength(0);
        }

        private void DeleteRowToolButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы действительно хотите удалить этого сотрудника?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var selectedItems = DataGridEpm.SelectedItems;
                    if (selectedItems != null && selectedItems.Count > 0)
                    {
                        var currentUser = selectedItems[0] as Restaurant.Model.Restaurant_Employees;
                        AppData.db.Restaurant_Employees.Remove(currentUser);
                        AppData.db.SaveChanges();
                        DataGridEpm.ItemsSource = AppData.db.Restaurant_Employees.ToList();
                        MessageBox.Show("Успешно", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        RowCount.Content = DataGridEpm.Items.Count.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Выбранный сотрудник используется в таблице Заказы, см. раздел |Заказы| главного меню", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditRowToolButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = DataGridEpm.SelectedItem as Restaurant.Model.Restaurant_Employees;

            if (selectedRow != null)
            {
                var newWindow = new EditEmployees(selectedRow.Employee_code);
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
            LoadDataGridEpm();
        }
        private void InfoRowToolButton_Click(object sender, RoutedEventArgs e)
        {
            EmpDescription.Visibility = Visibility.Visible;
            var column = DescHide.ColumnDefinitions[2]; 
            column.Width = new GridLength(400);
        }

        private void RotateMainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new menupage());
        }
    }
}
