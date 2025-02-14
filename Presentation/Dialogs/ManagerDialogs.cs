using Data.Factories;
using Data.Services;
using Presentation.Interfaces;

namespace Presentation.Dialogs;

public class ManagerDialogs(IManagerService managerService) : IManagerDialogs
{
    private readonly IManagerService _managerService = managerService;

    public async Task MenuOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("-----------MANAGER SYSTEM-----------");
            Console.WriteLine("");
            Console.WriteLine($"{"[1]",-10} Create a manager.");
            Console.WriteLine($"{"[2]",-10} View all managers.");
            Console.WriteLine($"{"[3]",-10} Update a manager.");
            Console.WriteLine($"{"[4]",-10} Delete a manager.");
            Console.WriteLine($"{"[5]",-10} Quit application.");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateManagerOption();
                    break;

                case "2":
                    await ViewAllManagersOption();
                    break;

                case "3":
                    await UpdateManagerOption();
                    break;

                case "4":
                    await DeleteManagerOption();
                    break;

                case "5":
                    ExitApplicationOption();
                    break;
            }
        }  
    }

    public async Task CreateManagerOption()
    {
        var manager = ManagerFactory.CreateRegistrationForm();

        Console.Clear();
        Console.WriteLine("-----------CREATE MANAGER-----------");
        Console.WriteLine("");
        Console.WriteLine("Who is the manager of the project?");
        Console.Write("First name: ");
        manager.FirstName = Console.ReadLine()!;
        Console.Write("Last name: ");
        manager.LastName = Console.ReadLine()!;

        var result = await _managerService.CreateManagerAsync(manager);
        if (result)
        {
            Console.WriteLine("Manager was created sucessfully.");
        }
        else
        {
            Console.WriteLine("Manager was not created.");
        }
        Console.ReadLine();
    }

    public async Task ViewAllManagersOption()
    {
        Console.Clear();
        Console.WriteLine("---------VIEW ALL MANAGERS----------");
        Console.WriteLine("");

        var managers = await _managerService.GetManagersAsync();

        if (managers.Any())
        {
            foreach (var manager in managers)
            {
                Console.WriteLine($"{"Id: ",-10} {manager.Id}");
                Console.WriteLine($"{"Manager: ",-10}" +
                    $"{manager.FirstName} " +
                    $"{manager.LastName}");
                Console.WriteLine("");
            }
        }
        else
        {
            Console.WriteLine("No managers found.");
        }
        Console.ReadLine();
    }

    public async Task UpdateManagerOption()
    {
        Console.Clear();
        Console.WriteLine("----------UPDATE MANAGER------------");
        Console.WriteLine("");

        Console.WriteLine("What is the id of the manager you want to update?");
        var stringId = Console.ReadLine()!;

        if (int.TryParse(stringId, out int intId))
        {
            var manager = await _managerService.GetManagerByIdAsync(intId);
            if (manager == null)
            {
                Console.WriteLine("No manager found with this id.");
            }
            else
            {
                Console.WriteLine($"{"Id: ",-10} {manager.Id}");
                Console.WriteLine($"{"Manager: ",-10}" +
                    $"{manager.FirstName} " +
                    $"{manager.LastName}");
                Console.WriteLine("");

                var managerUpdateForm = ManagerFactory.CreateUpdateForm();
                managerUpdateForm.Id = manager.Id;
                managerUpdateForm.FirstName = manager.FirstName;
                managerUpdateForm.LastName = manager.LastName;

                Console.Clear();
                Console.WriteLine("----------UPDATE MANAGER------------");
                Console.WriteLine("");
                Console.WriteLine("Who is the manager?");
                Console.Write("First name: ");
                var firstName = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(firstName) && firstName != manager.FirstName)
                    managerUpdateForm.FirstName = firstName;
                Console.Write("Last name: ");
                var lastName = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(lastName) && lastName != manager.LastName)
                    managerUpdateForm.LastName = lastName;

                manager = await _managerService.UpdateManagerAsync(managerUpdateForm);
                if (manager != null)
                {
                    Console.WriteLine("Manager was updated sucessfully.");

                }
                else
                {
                    Console.WriteLine("Manager was not updated.");
                }
                Console.ReadLine();
            }
        }
        else
        {
            Console.WriteLine("No manager found with this id.");
        }
    }

    public async Task DeleteManagerOption()
    {
        Console.Clear();
        Console.WriteLine("-----------DELETE MANAGER-----------");
        Console.WriteLine("");

        Console.WriteLine("What is the id of the manager you want to delete?");
        var stringId = Console.ReadLine()!;

        if (int.TryParse(stringId, out int intId))
        {
            var manager = await _managerService.GetManagerByIdAsync(intId);
            if (manager == null)
            {
                Console.WriteLine("No manager found with this id.");
            }
            else
            {
                var result = await _managerService.DeleteManagerAsync(manager.Id);
                if (result)
                {
                    Console.WriteLine("Manager was deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Manager was not deleted.");
                }
            }
        }
        else
        {
            Console.WriteLine("No manager found with this id.");
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

