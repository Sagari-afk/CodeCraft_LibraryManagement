using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using LibraryManagement.Models;

using System.IO;
using System.Text.Json;
using Microsoft.Win32;



namespace LibraryManagementWPF
{
    public partial class MainWindow : Window
    {
        static List<Book> books = new List<Book>();
        static List<Reader> readers = new List<Reader>();
        static List<Loan> loans = new List<Loan>();
        static int nextBookId = 1;
        static int nextLoanId = 1;

        public MainWindow()
        {
            InitializeComponent();
            LoadBooks();
            LoadUsers();
            LoadLoanReaders();
            LoadLoanBooks();
            LoadReturnLoans();
            LoadActiveLoans();
        }

        #region Books Management
        private void LoadBooks()
        {
            BooksDataGrid.ItemsSource = null;
            BooksDataGrid.ItemsSource = books;
        }


        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            AddBookWindow addBookWindow = new AddBookWindow();
            if (addBookWindow.ShowDialog() == true)
            {
                string title = addBookWindow.BookTitle;
                string author = addBookWindow.BookAuthor;
                string isbn = addBookWindow.BookISBN;
                int year = addBookWindow.BookYear;
                int copies = addBookWindow.AvailableCopies;
                BookCategory category = addBookWindow.SelectedCategory;

                Book newBook = new Book(nextBookId++, title, author, isbn, year, copies, category);
                books.Add(newBook);
                LoadBooks();
                LoadLoanBooks();
            }
        }

        private void RemoveBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (BooksDataGrid.SelectedItem is Book selectedBook)
            {
                bool isLoaned = loans.Any(l => l.Book.Id == selectedBook.Id && l.ReturnDate == null);
                if (isLoaned)
                {
                    MessageBox.Show("Book is currently loaned out and cannot be removed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                books.Remove(selectedBook);
                LoadBooks();
                LoadLoanBooks();
            }
            else
            {
                MessageBox.Show("Select a book to remove.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SearchBooksButton_Click(object sender, RoutedEventArgs e)
        {
            string term = Microsoft.VisualBasic.Interaction.InputBox("Enter title to search:", "Search Books", "");
            if (!string.IsNullOrWhiteSpace(term))
            {
                var results = books.Where(b => b.Title.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                BooksDataGrid.ItemsSource = results;
            }
        }

        private void RefreshBooksButton_Click(object sender, RoutedEventArgs e)
        {
            LoadBooks();
        }


        private void SaveBooksToFileButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Save Books File",
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                FileName = "books.json"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string BooksFilePath = saveFileDialog.FileName;

                try
                {
                    var options = new JsonSerializerOptions { WriteIndented = true };
                    string json = JsonSerializer.Serialize(books, options);
                    File.WriteAllText(BooksFilePath, json);
                    MessageBox.Show("Saved! ", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving books: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
            }
        }

        private void LoadBooksFromFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Open Books File",
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    if (File.Exists(filePath))
                    {
                        string json = File.ReadAllText(filePath);
                        List<Book> loadedBooks = JsonSerializer.Deserialize<List<Book>>(json);
                        books = loadedBooks;
                        LoadBooks();
                        LoadLoanBooks();

                        MessageBox.Show("Books loaded successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading books: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        #endregion

        #region Users Management
        private void LoadUsers()
        {
            UsersDataGrid.ItemsSource = null;
            UsersDataGrid.ItemsSource = readers;
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            AddUserWindow addUserWindow = new AddUserWindow();
            if (addUserWindow.ShowDialog() == true)
            {
                Reader newReader = new Reader(addUserWindow.FirstName, addUserWindow.LastName, addUserWindow.Email, addUserWindow.Phone);
                readers.Add(newReader);
                LoadUsers();
                LoadLoanReaders();
            }
        }

        private void RemoveUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem is Reader selectedReader)
            {
                bool hasActiveLoans = loans.Any(l => l.Reader.Id == selectedReader.Id && l.ReturnDate == null);
                if (hasActiveLoans)
                {
                    MessageBox.Show("Reader has active loans and cannot be removed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                readers.Remove(selectedReader);
                LoadUsers();
                LoadLoanReaders();
            }
            else
            {
                MessageBox.Show("Select a reader to remove.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SearchUserButton_Click(object sender, RoutedEventArgs e)
        {
            string term = Microsoft.VisualBasic.Interaction.InputBox("Enter search term for reader (Name, ID, or Email):", "Search Reader", "");
            if (!string.IsNullOrWhiteSpace(term))
            {
                var results = readers.Where(r =>
                    ($"{r.FirstName} {r.LastName}").IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    r.Id.ToString() == term ||
                    r.Email.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0
                ).ToList();
                UsersDataGrid.ItemsSource = results;
            }
        }

        private void RefreshUsersButton_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers();
        }

        private void SaveUsersToFileButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Save Books File",
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                FileName = "reader.json"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string ReaderFilePath = saveFileDialog.FileName;

                try
                {
                    var options = new JsonSerializerOptions { WriteIndented = true };
                    string json = JsonSerializer.Serialize(readers, options);
                    File.WriteAllText(ReaderFilePath, json);
                    MessageBox.Show("Saved! ", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving books: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
            }
        }

        private void LoadUsersFromFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Open Books File",
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    if (File.Exists(filePath))
                    {
                        string json = File.ReadAllText(filePath);
                        List<Reader> loadedReader = JsonSerializer.Deserialize<List<Reader>>(json);
                        readers = loadedReader;
                        LoadUsers();
                        LoadLoanReaders();

                        MessageBox.Show("Books loaded successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading books: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        #endregion

        #region Loans Management

        private void LoadLoanReaders()
        {
            LoanReaderComboBox.ItemsSource = readers;
            LoanReaderComboBox.DisplayMemberPath = "FirstName";
            if (readers.Any())
                LoanReaderComboBox.SelectedIndex = 0;
        }

        private void LoadLoanBooks()
        {
            var availableBooks = books.Where(b => b.AvailableCopies > 0).ToList();

            LoanBookComboBox.ItemsSource = availableBooks;
            LoanBookComboBox.DisplayMemberPath = "Title";
            if (availableBooks.Any())
                LoanBookComboBox.SelectedIndex = 0;
        }

        private void LoadReturnLoans()
        {
            var activeLoans = loans.Where(l => l.ReturnDate == null).ToList();
            ReturnLoanComboBox.ItemsSource = activeLoans;
            if (activeLoans.Any())
                ReturnLoanComboBox.SelectedIndex = 0;
        }

        private void LoadActiveLoans()
        {
            ActiveLoansDataGrid.ItemsSource = null;
            ActiveLoansDataGrid.ItemsSource = loans.Where(l => l.ReturnDate == null).ToList();
        }

        private void RentBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoanReaderComboBox.SelectedItem is not Reader selectedReader)
            {
                MessageBox.Show("Please select a reader.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (LoanBookComboBox.SelectedItem is not Book selectedBook)
            {
                MessageBox.Show("Please select a book.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(LoanDaysTextBox.Text, out int days) || days <= 0)
            {
                MessageBox.Show("Please enter a valid number of days.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (selectedBook.AvailableCopies < 1)
            {
                MessageBox.Show("Selected book is not available.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DateTime loanDate = DateTime.Now;
            DateTime expectedReturnDate = loanDate.AddDays(days);

            Loan newLoan = new Loan(nextLoanId++, selectedBook, selectedReader, loanDate, expectedReturnDate);
            loans.Add(newLoan);
            selectedBook.AvailableCopies--;

            MessageBox.Show("Book rented successfully!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

            LoadLoanBooks();
            LoadReturnLoans();
            LoadActiveLoans();
        }

        private void ReturnBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (ReturnLoanComboBox.SelectedItem is not Loan selectedLoan)
            {
                MessageBox.Show("Please select a loan to return.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            selectedLoan.ReturnDate = DateTime.Now;
            selectedLoan.Book.AvailableCopies++;

            MessageBox.Show("Book returned successfully!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

            LoadLoanBooks();
            LoadReturnLoans();
            LoadActiveLoans();
        }

        private void SaveLoadsToFileButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Save Books File",
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                FileName = "loans.json"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string LoansFilePath = saveFileDialog.FileName;

                try
                {
                    var options = new JsonSerializerOptions { WriteIndented = true };
                    string json = JsonSerializer.Serialize(loans, options);
                    File.WriteAllText(LoansFilePath, json);
                    MessageBox.Show("Saved! ", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving books: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
            }
        }

        private void LoadLoadsFromFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Open Books File",
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    if (File.Exists(filePath))
                    {
                        string json = File.ReadAllText(filePath);
                        List<Loan> loadedLoans = JsonSerializer.Deserialize<List<Loan>>(json);
                        loans = loadedLoans;
                        LoadActiveLoans();

                        MessageBox.Show("Books loaded successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading books: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        #endregion
    }
}
