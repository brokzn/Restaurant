using Restaurant.Model;

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
    /// Логика взаимодействия для ticketpage.xaml
    /// </summary>
    public partial class ticketpage : Page
    {
        public ticketpage()
        {
            InitializeComponent();
        }

        private void InfoRowToolButton_Click(object sender, RoutedEventArgs e)
        {
            TicketDescription.Visibility = Visibility.Visible;
            var column = DescHide.ColumnDefinitions[2];
            column.Width = new GridLength(400);
        }

        private void EditRowToolButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteRowToolButton_Click(object sender, RoutedEventArgs e)
        {
                if (MessageBox.Show("Вы действительно хотите завершить этот заказ?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var selectedItems = DataGridTicket.SelectedItems;
                    if (selectedItems != null && selectedItems.Count > 0)
                    {
                        foreach (var item in selectedItems)
                        {
                            var currentUser = item as Restaurant.Model.Ticket;
                            AppData.db.Ticket.Remove(currentUser);
                        }
                        AppData.db.SaveChanges();

                        DataGridTicket.ItemsSource = AppData.db.Ticket.ToList();
                        MessageBox.Show("Успешно", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        RowCount.Content = DataGridTicket.Items.Count.ToString();
                    }
                }
        }

        private void СloseDescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            TicketDescription.Visibility = Visibility.Hidden;
            var column = DescHide.ColumnDefinitions[2];
            column.Width = new GridLength(0);
        }

        private void AddRowDataGridStorage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteRowDataGridStorage_Click(object sender, RoutedEventArgs e)
        {
                if (MessageBox.Show("Вы действительно хотите завершить выбранные заказы?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var selectedItems = DataGridTicket.SelectedItems;
                    if (selectedItems != null && selectedItems.Count > 0)
                    {
                        foreach (var item in selectedItems)
                        {
                            var currentUser = item as Restaurant.Model.Ticket;
                            AppData.db.Ticket.Remove(currentUser);
                        }
                        AppData.db.SaveChanges();

                        DataGridTicket.ItemsSource = AppData.db.Ticket.ToList();
                        MessageBox.Show("Успешно", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        RowCount.Content = DataGridTicket.Items.Count.ToString();
                    }
                }

        }

        private void UpdateDataGrid_Click(object sender, RoutedEventArgs e)
        {
            DataGridTicket.ItemsSource = AppData.db.Ticket.ToList();
            RowCount.Content = DataGridTicket.Items.Count.ToString();
        }

        private void SearchDataGrid_Click(object sender, RoutedEventArgs e)
        {
            var searchText = DataFilter.Text.ToLower();


            ICollectionView view = CollectionViewSource.GetDefaultView(DataGridTicket.ItemsSource);
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
                        var ticket = (Restaurant.Model.Ticket)item;

                        return ticket.Ticket_Code.ToString().Contains(searchText)
                            || ticket.Ticket_Date.ToString().ToLower().Contains(searchText)
                            || ticket.Ticket_Time.ToString().ToLower().Contains(searchText);



                    }
                };
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataGridTicket.ItemsSource = AppData.db.Ticket.ToList();
            RowCount.Content = DataGridTicket.Items.Count.ToString();
        }
       
        private void DataGridMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dataGrid = sender as DataGrid;

            if (dataGrid != null)
            {
                var menu = (Model.Ticket)dataGrid.SelectedItem;

                PreviewMenu(menu);

            }
            else
            {
                RowCount.Content = DataGridTicket.Items.Count.ToString();
            }
        }
        private void PreviewMenu(Model.Ticket menu)
        {
            try
            {
                if (menu != null)
            {
                    try
                    {
                        TicketDescription.Visibility = Visibility.Hidden;
                        CustomerNameLabel.Content = menu.Customers_Name;
                        CustomerPhoneLabel.Content = menu.Phone_Number;
                        CustomerDish1Label.Content = menu.Menu.Food_Name;
                        CustomerDish2Label.Content = menu.Menu1.Food_Name;
                        CustomerDish3Label.Content = menu.Menu2.Food_Name;
                        TicketCostLabel.Content = "Цена: " + menu.Cost + "р.";
                        DeliveryNoteLabel.Content = menu.Delivery_note;
                        CompleteDateTimeLabel.Content = menu.Completion_Time.ToString() + " " + menu.Completion_Date.ToString("dd.MM.yyyy");

                        DeliverNameLabel.Content = menu.Restaurant_Employees.Firstname + " " + menu.Restaurant_Employees.Middlename + " " + menu.Restaurant_Employees.Lastname;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Произошла ошибка обновления информации о заказе, нажмите ОК что бы продолжить " + ex, "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
            }
        }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка обновления информации о заказе, пожалуйста перезагрузите приложение" + ex, "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
}
        private void RotateMainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new menupage());
        }
    }
}
