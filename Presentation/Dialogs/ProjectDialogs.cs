using Data.Interfaces;
using Presentation.Interfaces;
using Business.Models;
using Data.Factories;


namespace Presentation.Dialogs;

public class ProjectDialogs(IProjectService projectService) : IProjectDialogs
{
    private readonly IProjectService _projectService = projectService;

    public async Task MenuOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("-----------PROJECT SYSTEM-----------");
            Console.WriteLine("");
            Console.WriteLine($"{"[1]",-10} Create a project.");
            Console.WriteLine($"{"[2]",-10} View all projects.");
            Console.WriteLine($"{"[3]",-10} Update a project.");
            Console.WriteLine($"{"[4]",-10} Delete a project.");
            Console.WriteLine($"{"[5]",-10} Quit application.");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateProjectOption();
                    break;

                case "2":
                    await ViewAllProjectsOption();
                    break;

                case "3":
                    await UpdateProjectOption();
                    break;

                case "4":
                    await DeleteProjectOption();
                    break;

                case "5":
                    ExitApplicationOption();
                    break;
            }
        }
    }

    public async Task CreateProjectOption()
    {
        var project = ProjectFactory.CreateRegistrationForm();
        project.TimePeriod = new Time();
        project.ProjectManager = new Manager();
        project.Client = new Client();

        Console.Clear();
        Console.WriteLine("-----------CREATE PROJECT-----------");
        Console.WriteLine("");

        Console.WriteLine("Please give the project a... ");
        Console.Write("Name: ");
        project.Name = Console.ReadLine()!;
        Console.Write("Service: ");
        project.ServiceName = Console.ReadLine()!;
        Console.Write("Price: ");
        project.Price = Console.ReadLine()!;

        Console.Clear();
        Console.WriteLine("-----------CREATE PROJECT-----------");
        Console.WriteLine("");
        Console.WriteLine("Please give the project a timeperiod... (dd/mm/yyyy)");
        Console.Write("Start day: ");
        project.TimePeriod.StartDay = Console.ReadLine()!;
        Console.Write("Start month: ");
        project.TimePeriod.StartMonth = Console.ReadLine()!;
        Console.Write("Start year: ");
        project.TimePeriod.StartYear = Console.ReadLine()!;
        Console.Write("End day: ");
        project.TimePeriod.EndDay = Console.ReadLine()!;
        Console.Write("End month: ");
        project.TimePeriod.EndMonth = Console.ReadLine()!;
        Console.Write("End year: ");
        project.TimePeriod.EndYear = Console.ReadLine()!;

        Console.Clear();
        Console.WriteLine("-----------CREATE PROJECT-----------");
        Console.WriteLine("");
        Console.WriteLine("Who is the manager of the project?");
        Console.Write("First name: ");
        project.ProjectManager.FirstName = Console.ReadLine()!;
        Console.Write("Last name: ");
        project.ProjectManager.LastName = Console.ReadLine()!;

        Console.Clear();
        Console.WriteLine("-----------CREATE PROJECT-----------");
        Console.WriteLine("");
        Console.WriteLine("What is the name of the client company the project is for?");
        Console.Write("Company name: ");
        project.Client.CompanyName = Console.ReadLine()!;

        Console.Clear();
        Console.WriteLine("-----------CREATE PROJECT-----------");
        Console.WriteLine("");
        Console.WriteLine("Lastly, what is the status of the project?");
        Console.WriteLine("[1] - Not yet started.");
        Console.WriteLine("[2] - Ongoing.");
        Console.WriteLine("[3] - Finished.");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                {
                    project.Status = "Not yet started";

                    var result = await _projectService.CreateProjectAsync(project);
                    if (result)
                    {
                        Console.WriteLine("Project was created sucessfully.");

                    }
                    else
                    {
                        Console.WriteLine("Project was not created.");
                    }
                    Console.ReadLine();
                    break;
                }
            case "2":
                {
                    project.Status = "Ongoing";

                    var result = await _projectService.CreateProjectAsync(project);
                    if (result)
                    {
                        Console.WriteLine("Project was created sucessfully.");
                    }
                    else
                    {
                        Console.WriteLine("Project was not created.");
                    }
                    Console.ReadLine();
                    break;
                }
            case "3":
                {
                    project.Status = "Finished";

                    var result = await _projectService.CreateProjectAsync(project);
                    if (result)
                    {
                        Console.WriteLine("Project was created sucessfully.");
                    }
                    else
                    {
                        Console.WriteLine("Project was not created.");
                    }
                    Console.ReadLine();
                    break;
                }
        }
    }

    public async Task ViewAllProjectsOption()
    {
        Console.Clear();
        Console.WriteLine("---------VIEW ALL PROJECTS----------");
        Console.WriteLine("");

        var projects = await _projectService.GetProjectsAsync();

        if (projects.Any())
        {
            foreach (var project in projects)
            {
                Console.WriteLine($"{"Id: ",-10} {project.Id}");
                Console.WriteLine($"{"Name: ",-10} {project.Name}");
                Console.WriteLine($"{"Service: ",-10} {project.ServiceName}");
                Console.WriteLine($"{"Price: ",-10} {project.Price}");
                Console.WriteLine($"{"Time: ",-10} " +
                    $"{project.TimePeriod.StartDay}/" +
                    $"{project.TimePeriod.StartMonth}/" +
                    $"{project.TimePeriod.StartYear} - " +
                    $"{project.TimePeriod.EndDay}/" +
                    $"{project.TimePeriod.EndMonth}/" +
                    $"{project.TimePeriod.EndYear}");
                Console.WriteLine($"{"Manager: ",-10} " +
                    $"{project.ProjectManager.FirstName} " +
                    $"{project.ProjectManager.LastName}");
                Console.WriteLine($"{"Client: ",-10} {project.Client.CompanyName}");
                Console.WriteLine($"{"Status: ",-10} {project.Status}");
                Console.WriteLine("");
            }
        }
        else
        {
            Console.WriteLine("No projects found.");
        }
        Console.ReadLine();
    }

    public async Task UpdateProjectOption()
    {
        Console.Clear();
        Console.WriteLine("----------UPDATE PROJECT------------");
        Console.WriteLine("");

        Console.WriteLine("What is the id of the project you want to update?");
        var stringId = Console.ReadLine()!;

        if (int.TryParse(stringId, out int intId))
        {
            var project = await _projectService.GetProjectByIdAsync(intId);
            if (project == null)
            {
                Console.WriteLine("No project found with this id.");
            }
            else
            {
                Console.WriteLine($"{"Id: ",-10} {project.Id}");
                Console.WriteLine($"{"Name: ",-10} {project.Name}");
                Console.WriteLine($"{"Service: ",-10} {project.ServiceName}");
                Console.WriteLine($"{"Price: ",-10} {project.Price}");
                Console.WriteLine($"{"Time: ",-10} " +
                    $"{project.TimePeriod.StartDay}/" +
                    $"{project.TimePeriod.StartMonth}/" +
                    $"{project.TimePeriod.StartYear} - " +
                    $"{project.TimePeriod.EndDay}/" +
                    $"{project.TimePeriod.EndMonth}/" +
                    $"{project.TimePeriod.EndYear}");
                Console.WriteLine($"{"Manager: ",-10}" +
                    $"{project.ProjectManager.FirstName} " +
                    $"{project.ProjectManager.LastName}");
                Console.WriteLine($"{"Client: ",-10} {project.Client.CompanyName}");
                Console.WriteLine($"{"Status: ",-10} {project.Status}");
                Console.WriteLine("");

                var projectUpdateForm = ProjectFactory.CreateUpdateForm();
                projectUpdateForm.Id = project.Id;
                projectUpdateForm.Name = project.Name;
                projectUpdateForm.ServiceName = project.ServiceName;
                projectUpdateForm.Price = project.Price;
                projectUpdateForm.Status = project.Status;
                projectUpdateForm.TimePeriod = new Time
                {
                    StartDay = project.TimePeriod.StartDay,
                    StartMonth = project.TimePeriod.StartMonth,
                    StartYear = project.TimePeriod.StartYear,
                    EndDay = project.TimePeriod.EndDay,
                    EndMonth = project.TimePeriod.EndMonth,
                    EndYear = project.TimePeriod.EndYear
                };
                projectUpdateForm.ProjectManager = new Manager
                {
                    FirstName = project.ProjectManager.FirstName,
                    LastName = project.ProjectManager.LastName
                };
                projectUpdateForm.Client = new Client
                {
                    CompanyName = project.Client.CompanyName
                };
                projectUpdateForm.TimePeriodId = project.TimePeriodId;
                projectUpdateForm.ProjectManagerId = project.ProjectManagerId;
                projectUpdateForm.ClientId = project.ClientId;

                Console.Clear();
                Console.WriteLine("----------UPDATE PROJECT------------");
                Console.WriteLine("");
                Console.WriteLine("Please give the project a... ");
                Console.Write("Name: ");
                var name = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(name) && name != project.Name)
                    projectUpdateForm.Name = name;
                Console.Write("Service: ");
                var serviceName = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(serviceName) && serviceName != project.ServiceName)
                    projectUpdateForm.ServiceName = serviceName;
                Console.Write("Price: ");
                var price = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(price) && price != project.Price)
                    projectUpdateForm.Price = price;

                Console.Clear();
                Console.WriteLine("----------UPDATE PROJECT------------");
                Console.WriteLine("");
                Console.WriteLine("Please give the project a timeperiod... (dd/mm/yyyy)");
                Console.Write("Start day: ");
                var startDay = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(startDay) && startDay != project.TimePeriod.StartDay)
                    projectUpdateForm.TimePeriod.StartDay = startDay;
                Console.Write("Start month: ");
                var startMonth = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(startMonth) && startMonth != project.TimePeriod.StartMonth)
                    projectUpdateForm.TimePeriod.StartMonth = startMonth;
                Console.Write("Start year: ");
                var startYear = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(startYear) && startYear != project.TimePeriod.StartYear)
                    projectUpdateForm.TimePeriod.StartYear = startYear;
                Console.Write("End day: ");
                var endDay = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(endDay) && endDay != project.TimePeriod.EndDay)
                    projectUpdateForm.TimePeriod.EndDay = endDay;
                Console.Write("End month: ");
                var endMonth = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(endMonth) && endMonth != project.TimePeriod.EndMonth)
                    projectUpdateForm.TimePeriod.EndMonth = endMonth;
                Console.Write("End year: ");
                var endYear = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(endYear) && endYear != project.TimePeriod.EndYear)
                    projectUpdateForm.TimePeriod.EndYear = endYear;

                Console.Clear();
                Console.WriteLine("----------UPDATE PROJECT------------");
                Console.WriteLine("");
                Console.WriteLine("Who is the manager of the project?");
                Console.Write("First name: ");
                var firstName = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(firstName) && firstName != project.ProjectManager.FirstName)
                    projectUpdateForm.ProjectManager.FirstName = firstName;
                Console.Write("Last name: ");
                var lastName = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(lastName) && lastName != project.ProjectManager.LastName)
                    projectUpdateForm.ProjectManager.LastName = lastName;

                Console.Clear();
                Console.WriteLine("----------UPDATE PROJECT------------");
                Console.WriteLine("");
                Console.WriteLine("What is the name of the client company the project is for?");
                Console.Write("Company name: ");
                var companyName = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(companyName) && companyName != project.Client.CompanyName)
                    projectUpdateForm.Client.CompanyName = companyName;

                Console.Clear();
                Console.WriteLine("----------UPDATE PROJECT------------");
                Console.WriteLine("");
                Console.WriteLine("Lastly, what is the status of the project?");
                Console.WriteLine("[1] - Not yet started.");
                Console.WriteLine("[2] - Ongoing.");
                Console.WriteLine("[3] - Finished.");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        {
                            projectUpdateForm.Status = "Not yet started";

                            project = await _projectService.UpdateProjectAsync(projectUpdateForm);
                            if (project != null)
                            {
                                Console.WriteLine("Project was updated sucessfully.");

                            }
                            else
                            {
                                Console.WriteLine("Project was not updated.");
                            }
                            Console.ReadLine();
                            break;
                        }
                    case "2":
                        {
                            projectUpdateForm.Status = "Ongoing";

                            project = await _projectService.UpdateProjectAsync(projectUpdateForm);
                            if (project != null)
                            {
                                Console.WriteLine("Project was updated sucessfully.");
                            }
                            else
                            {
                                Console.WriteLine("Project was not updated.");
                            }
                            Console.ReadLine();
                            break;
                        }
                    case "3":
                        {
                            projectUpdateForm.Status = "Finished";

                            project = await _projectService.UpdateProjectAsync(projectUpdateForm);
                            if (project != null)
                            {
                                Console.WriteLine("Project was updated sucessfully.");
                            }
                            else
                            {
                                Console.WriteLine("Project was not updated.");
                            }
                            Console.ReadLine();
                            break;
                        }
                }
            }
        }
        else
        {
            Console.WriteLine("No project found with this id.");
        }
    }

    public async Task DeleteProjectOption()
    {
        Console.Clear();
        Console.WriteLine("-----------DELETE PROJECT-----------");
        Console.WriteLine("");

        Console.WriteLine("What is the id of the project you want to delete?");
        var stringId = Console.ReadLine()!;

        if (int.TryParse(stringId, out int intId))
        {
            var project = await _projectService.GetProjectByIdAsync(intId);
            if (project == null)
            {
                Console.WriteLine("No project found with this id.");
            }
            else
            {
                var result = await _projectService.DeleteProjectAsync(project.Id);
                if (result)
                {
                    Console.WriteLine("Project was deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Project was not deleted.");
                }
            }
        }
        else
        {
            Console.WriteLine("No project found with this id.");
        }
        Console.ReadLine();
    }

    public void ExitApplicationOption()
    {
        Console.Clear();
        Console.WriteLine("----------EXIT APPLICATION----------");
        Console.WriteLine("");
        Console.WriteLine("Do you want to exit the application? (y/n)");
        var exitOption = Console.ReadLine()!;

        if (exitOption.ToLower() == "y")
        {
            Environment.Exit(0);
        }
        else
        {
            Console.WriteLine("Going back to the menu.");
            Console.ReadLine();
        }
    }
}

