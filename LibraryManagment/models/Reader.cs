namespace LibraryManagement.Models
{
    public class Reader
    {
        private static int _nextId = 1;     // priklad pre automaticke generovane ID
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public Reader(string firstName, string lastName, string email, string phone)
        {
            Id = _nextId++;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
        }

        public override string ToString()
        {
            return $"ID: {Id} | {FirstName} {LastName}, Email: {Email}, Phone: {Phone}";
        }
    }
}
