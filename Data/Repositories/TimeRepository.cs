using Data.Contexts;
using Data.Entites;
using Data.Interfaces;

namespace Data.Repositories;

public class TimeRepository(DataContext context) : BaseRepository<TimeEntity>(context), ITimeRepository
{
    private readonly DataContext _context = context;
}