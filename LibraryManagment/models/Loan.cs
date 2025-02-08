using System;

namespace LibraryManagement.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public Reader Reader { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public Loan(int id, Book book, Reader reader, DateTime loanDate, DateTime expectedReturnDate)
        {
            Id = id;
            Book = book;
            Reader = reader;
            LoanDate = loanDate;
            ExpectedReturnDate = expectedReturnDate;
            ReturnDate = null;
        }

        public override string ToString()
        {
            return $"Loan ID: {Id} | Book: {Book.Title} (ID: {Book.Id}) | Reader: {Reader.FirstName} {Reader.LastName} (ID: {Reader.Id}) | Loan Date: {LoanDate.ToShortDateString()} | Expected Return: {ExpectedReturnDate.ToShortDateString()} | Returned: {(ReturnDate.HasValue ? ReturnDate.Value.ToShortDateString() : "Not Returned")}";
        }
    }
}
