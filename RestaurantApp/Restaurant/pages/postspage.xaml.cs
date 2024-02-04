using Restaurant.Model;
using Restaurant.Wforms.PostForms;
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
    /// Логика взаимодействия для postspage.xaml
    /// </summary>
    public partial class postspage : Page
    {
        public postspage()
        {
            InitializeComponent();
        }
        private void LoadDataGridPosts()
        {
            DataGridPosts.ItemsSource = AppData.db.Restaurant_Posts.ToList();

            RowCount.Content = DataGridPosts.Items.Count.ToString();
        }
        private void SearchDataGrid_Click(object sender, RoutedEventArgs e)
        {
            var searchText = DataFilter.Text.ToLower();


            ICollectionView view = CollectionViewSource.GetDefaultView(DataGridPosts.ItemsSource);
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
                        var posts = (Restaurant_Posts)item;

                        return posts.Post_Name.ToLower().Contains(searchText)
                            || posts.Salary.ToLower().Contains(searchText)
                            || posts.Responsibilities.ToLower().Contains(searchText)
                            || posts.Requirements.ToLower().Contains(searchText);
                            
                            


                    }
                };
            }
        }

        private void AddRowDataGridStorage_Click(object sender, RoutedEventArgs e)
        {
            Window windowToOpen = new AddPost();
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
                if (MessageBox.Show("Вы действительно хотите удалить выбранные должности?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var selectedItems = DataGridPosts.SelectedItems;
                    if (selectedItems != null && selectedItems.Count > 0)
                    {
                        foreach (var item in selectedItems)
                        {
                            var currentUser = item as Restaurant_Posts;
                            AppData.db.Restaurant_Posts.Remove(currentUser);
                        }
                        AppData.db.SaveChanges();

                        DataGridPosts.ItemsSource = AppData.db.Restaurant_Posts.ToList();
                        MessageBox.Show("Успешно", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        RowCount.Content = DataGridPosts.Items.Count.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Выбранные должности используются в таблице Сотрудники, см. раздел |Сотрудники| главного меню", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteRowToolButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы действительно хотите удалить эту должность?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var selectedItems = DataGridPosts.SelectedItems;
                    if (selectedItems != null && selectedItems.Count > 0)
                    {
                        var currentUser = selectedItems[0] as Restaurant_Posts;
                        AppData.db.Restaurant_Posts.Remove(currentUser);
                        AppData.db.SaveChanges();
                        DataGridPosts.ItemsSource = AppData.db.Restaurant_Posts.ToList();
                        MessageBox.Show("Успешно", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        RowCount.Content = DataGridPosts.Items.Count.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Выбранная должность используется в таблице Сотрудники, см. раздел |Сотрудники| главного меню", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditRowToolButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = DataGridPosts.SelectedItem as Restaurant_Posts;

            if (selectedRow != null)
            {
                var newWindow = new EditPost(selectedRow.Post_Code);
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
            LoadDataGridPosts();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGridPosts();
        }

        private void UpdateDataGrid_Click(object sender, RoutedEventArgs e)
        {
            DataGridPosts.ItemsSource = AppData.db.Restaurant_Posts.ToList();
            RowCount.Content = DataGridPosts.Items.Count.ToString();
        }

        private void RotateMainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new menupage());
        }
    }
}
