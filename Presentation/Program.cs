using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Dialogs;
using Presentation.Interfaces;

var services = new ServiceCollection()
    .AddDbContext<DataContext>(x => x.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\idabe\\source\\repos\\DataStorage_Assignment\\Data\\Databases\\local_database.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True"))
    .AddScoped<IProjectRepository, ProjectRepository>()
    .AddScoped<IProjectService, ProjectService>()
    .AddScoped<IProjectDialogs, ProjectDialogs>()
    .BuildServiceProvider();

var projectDialogs = services.GetRequiredService<IProjectDialogs>();
await projectDialogs.MenuOptions();