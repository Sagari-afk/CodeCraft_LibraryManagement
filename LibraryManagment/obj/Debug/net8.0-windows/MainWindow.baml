   M S B A M L   `   `   `  ����  LibraryManagement^Aclr-namespace:LibraryManagement.Models;assembly=LibraryManagementLibraryManagement.Models  5   	    LibraryManagment%   LibraryManagementWPF.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   TmodelsAclr-namespace:LibraryManagement.Models;assembly=LibraryManagement     5   	     � Title$  Library Management��5   	   $	��700��6$   $	��900��61   .��5      � .+�5   
   �� .@�5      �� $��Books��6   .��5	      �  � RowDefinitions 5
      �� 5      $
��Auto �6(   �� 5      $��* �6(   .+�5      ��  �� Orientation$ 
Horizontal=�6!   $��10q�6:   .+�5      �� -   5        $��AddBookButton��5   !   $��Add Book��66   $��5q�6I   6T   �� 5      -   5        $��RemoveBookButton��5   !   $��Remove Book��69   $��5q�6O   6Z   �� 5      -   5        $��SearchBooksButton��5   !   $��Search Books��6:   $��5q�6Q   6\   �� 5      -   5        $��RefreshBooksButton��5   !   $��Refresh��6;   $��5q�6M   6X   �� 5      -   5        $��SaveBooksToFileButton��5   !   $
��Save��6>   $��5q�6M   6X   �� 5      -   5        $��LoadBooksFromFileButton��5   !   $
��Load��6@   $��5q�6O   6Z   5      &   System.Windows.Controls.DataGrid  5      -   5        $��BooksDataGrid��5      $��1��64   $��10q�6A      AutoGenerateColumns$ False��6M      
IsReadOnly$
 True��6i      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��61      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6T   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� *Id+6T     5      $ Title��61   $ * 6Z    6@   �� *Title+6Z     5      $ Author��61   $ * 6\    6A   �� *Author+6\     5      $
 ISBN��61   $
 Auto 6X    6?   �� *ISBN+6X     5      $
 Year��61   $
 Auto 6X    6?   �� *Year+6X     5      $ Copies��61   $
 Auto 6f    6B   �� *AvailableCopies+6f     5      $ Category��61   $
 Auto 6`    6C   �� *
Category+6`   5       5!      5"      �� 5$      $��Users��6   .��5%      �  5&      �� 5'      $
��Auto �6(   �� 5(      $��* �6(   .+�5*      �� $ 
Horizontal=�6!   $��10q�6:   .+�5+      �� -   5        $��AddUserButton��5+   !   $��Add User��66   $��5q�6I   6T   �� 5,      -	   5        $��RemoveUserButton��5,   !   $��Remove User��69   $��5q�6O   6Z   �� 5-      -
   5        $��SearchUserButton��5-   !   $��Search User��69   $��5q�6O   6Z   �� 5.      -   5        $��RefreshUsersButton��5.   !   $��Refresh��6;   $��5q�6M   6X   5/        50      -   5        $��UsersDataGrid��50      $��1��64   $��10q�6A   $ False��6M   $
 True��6i    51        52      $ ID��61   $
 Auto 6T    6=   �� *Id+6T     53      $ 
First Name��61   $ * 6c    6E   �� *	FirstName+6c     54      $ 	Last Name��61   $ * 6a    6D   �� *
LastName+6a     55      $ Email��61   $ * 6Z    6@   �� *Email+6Z     56      $ Phone��61   $ * 6Z    6@   �� *Phone+6Z   58      59      5:      �� 5<      $��Loans��6   .��5=      � $��10q�6    5>      �� 5?      $
��Auto �6(   �� 5@      $
��Auto �6(   �� 5A      $
��Auto �6(   �� 5B      $��* �6(   .+�5E      �� $ 
Horizontal=�6!   $��5q�6:   $��Center=�6E   .+�5F      �� -   5        $��SaveLoadsToFileButton��5F   !   $
��Save��6>   $��5q�6M   6X   �� 5G      -   5        $��LoadLoadsFromFileButton��5G   !   $
��Load��6@   $��5q�6O   6Z   5H      �� 5J      $��Return a Book��6   $��5q�66   $��2��6A   .��5K      �� $ 
Horizontal=�6%   $��5q�6>   $��Center=�6I   .+�5L      �� $��Select Loan:��6$   $��Center=�6;   �� 5M      -   5        $��ReturnLoanComboBox��5M   '   $	��300��6A   $��5q�6M   	 �� DisplayMemberPath$	 Id��6X   �� 5N      -   5        $��Return Book��5N   %   $��5q�6;   6F   5O      5P      �� 5R      $��Rent a Book��6   $��5q�64   $��1��6@   .��5S      �� $ 
Horizontal=�6%   $��3q�6>   $��Center=�6I   .+�5T      �� $��Reader:��6$   $��Center=�66   �� 5U      -   5        $��LoanReaderComboBox��5U   '   $	��150��6A   $��5q�6M   $	 	FirstName��6X   �� 5V      $��Book:��6$   $��Center=�64   �� 5W      -   5        $��LoanBookComboBox��5W   '   $	��150��6?   $��5q�6K   $	 Title��6V   �� 5X      $��Days:��6$   $��Center=�64   �� 5Y      -   5        $��LoanDaysTextBox��5Y   &   $��50��6=   $��5q�6H   $��14��6S   �� 5Z      -   5        $��	Rent Book��5Z   %   $��5q�69   6D   5[      5\      �� 5^      $��Active Loans��6   $��5q�65   $��3��6@   .��5_        -   5        $��ActiveLoansDataGrid��5_   #   $ False��6>   $
 True��6Z   $��5q�6l    5`        5a   "   $ Loan ID��65   $
 Auto 6]    6F   �� *Id+6]     5b   "   $ 
Book Title��65   $ * 6h    6I   �� *
Book.Title+6h     5c   "   $ Reader��65   $
 Auto 6j    6E   �� *Reader.FirstName+6j     5d   "   $ 	Loan Date��65   $
 Auto 6u    6H   �� *
LoanDate+
 �� StringFormat$
 d��6u     5e   "   $ Expected Return��65   $
 Auto 6�    6N   �� *ExpectedReturnDate+$
 d��6�   5g      5h      5i      5j      5k      5l      5m      