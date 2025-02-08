using System;
using System.Windows;
using LibraryManagement.Models;

namespace LibraryManagementWPF
{
    public partial class AddBookWindow : Window
    {
        public string BookTitle { get; private set; }
        public string BookAuthor { get; private set; }
        public string BookISBN { get; private set; }
        public int BookYear { get; private set; }
        public int AvailableCopies { get; private set; }
        public BookCategory SelectedCategory { get; private set; }

        public AddBookWindow()
        {
            InitializeComponent();
            CategoryComboBox.ItemsSource = Enum.GetValues(typeof(BookCategory));
            CategoryComboBox.SelectedIndex = 0;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            BookTitle = TitleTextBox.Text;
            BookAuthor = AuthorTextBox.Text;
            BookISBN = ISBNTextBox.Text;


            if (!int.TryParse(YearTextBox.Text, out int year))
            {
                MessageBox.Show("Invalid year.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            BookYear = year;

            if (!int.TryParse(CopiesTextBox.Text, out int copies))
            {
                MessageBox.Show("Invalid copies number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            AvailableCopies = copies;

            if (CategoryComboBox.SelectedItem is BookCategory category)
            {
                SelectedCategory = category;
            }
            else
            {
                MessageBox.Show("Select a valid category.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
