namespace Business.Models;

public class Client
{

    public Client() { }
    public Client(int id, string companyName)
    {
        Id = id;
        CompanyName = companyName;
    }

    public int Id { get; set; }

    public string CompanyName { get; set; } = null!;
}
