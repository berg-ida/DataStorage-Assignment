namespace Business.Models;

public class Manager
{
    public Manager() { }
    public Manager(int id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }

    public int Id { get; set; }
    
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;
}
