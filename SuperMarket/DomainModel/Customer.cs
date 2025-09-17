namespace SuperMarket.DomainModel
{
    public class Customer : Entity
    {
        public string FirstName { get;}
        public string LastName { get; }
        public string Email { get;  }

        public Customer(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
