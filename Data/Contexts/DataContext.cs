using Data.Entites;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<ProjectEntity> Projects { get; set; }

    public DbSet<TimeEntity> TimePeriods { get; set; }

    public DbSet<ManagerEntity> ProjectManagers { get; set; }

    public DbSet<ClientEntity> Clients { get; set; }

}
