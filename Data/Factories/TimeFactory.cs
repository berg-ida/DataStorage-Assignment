using Business.Models;
using Data.Dtos;
using Data.Entites;

namespace Data.Factories;

public class TimeFactory
{
    public static TimeRegistrationForm CreateRegistrationForm() => new();
    public static TimeUpdateForm CreateUpdateForm() => new();

    public static TimeEntity Create(TimeRegistrationForm form) => new()
    {
        StartDay = form.StartDay,
        StartMonth = form.StartMonth,
        StartYear = form.StartYear,
        EndDay = form.EndDay,
        EndMonth = form.EndMonth,
        EndYear = form.EndYear,
    };

    public static TimeEntity Create(int id, TimeUpdateForm form) => new()
    {
        Id = id,
        StartDay = form.StartDay,
        StartMonth = form.StartMonth,
        StartYear = form.StartYear,
        EndDay = form.EndDay,
        EndMonth = form.EndMonth,
        EndYear = form.EndYear,
    };

    public static Time Create(TimeEntity entity) =>
        new(entity.Id, entity.StartDay, entity.StartMonth, entity.StartYear, entity.EndDay, entity.EndMonth, entity.EndYear);
}
