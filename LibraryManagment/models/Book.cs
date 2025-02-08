using System;

namespace LibraryManagement.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int Year { get; set; }
        public int AvailableCopies { get; set; }
        public BookCategory Category { get; set; }

        public Book() { }

        public Book(int id, string title, string author, string isbn, int year, int availableCopies, BookCategory category) // Pozor aby boli rovnake nazvy ako hore
        {
            Id = id;
            Title = title;
            Author = author;
            ISBN = isbn;
            Year = year;
            AvailableCopies = availableCopies;
            Category = category;
        }

        public override string ToString()
        {
            return $"ID: {Id} | {Title} by {Author}, ISBN: {ISBN}, Year: {Year}, Copies: {AvailableCopies}, Category: {Category}";
        }
    }
}
