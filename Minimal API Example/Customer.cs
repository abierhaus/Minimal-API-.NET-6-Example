public class Customer
{
    public Customer(int id, string firstname, string lastname)
    {
        Id = id;
        FirstName = firstname;
        LastName = lastname;
    }


    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
}