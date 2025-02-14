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
    //.AddScoped<ITimeRepository, TimeRepository>()
    //.AddScoped<ITimeService, TimeService>()
    //.AddScoped<ITimeDialogs, TimeDialogs>()
    //.AddScoped<IManagerRepository, ManagerRepository>()
    //.AddScoped<IManagerService, ManagerService>()
    //.AddScoped<IManagerDialogs, ManagerDialogs>()
    //.AddScoped<IClientRepository, ClientRepository>()
    //.AddScoped<IClientService, ClientService>()
    //.AddScoped<IClientDialogs, ClientDialogs>()
    .BuildServiceProvider();

//var clientDialogs = services.GetRequiredService<IClientDialogs>();
//await clientDialogs.MenuOptions();

//var managerDialogs = services.GetRequiredService<IManagerDialogs>();
//await managerDialogs.MenuOptions();

//var timeDialogs = services.GetRequiredService<ITimeDialogs>();
//await timeDialogs.MenuOptions();

var projectDialogs = services.GetRequiredService<IProjectDialogs>();
await projectDialogs.MenuOptions();

