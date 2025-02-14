using Business.Models;
using Data.Dtos;
using Data.Factories;
using Data.Interfaces;
using Data.Services;
using Presentation.Interfaces;

namespace Presentation.Dialogs;

public class ClientDialogs(IClientService clientService) : IClientDialogs
{
    private readonly IClientService _clientService = clientService;

    public async Task MenuOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("-----------CLIENT SYSTEM-----------");
            Console.WriteLine("");
            Console.WriteLine($"{"[1]",-10} Create a client.");
            Console.WriteLine($"{"[2]",-10} View all clients.");
            Console.WriteLine($"{"[3]",-10} Update a client.");
            Console.WriteLine($"{"[4]",-10} Delete a client.");
            Console.WriteLine($"{"[5]",-10} Quit application.");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateClientOption();
                    break;

                case "2":
                    await ViewAllClientsOption();
                    break;

                case "3":
                    await UpdateClientOption();
                    break;

                case "4":
                    await DeleteClientOption();
                    break;

                case "5":
                    ExitApplicationOption();
                    break;
            }
        }
    }

    public async Task CreateClientOption()
    {
        var client = ClientFactory.CreateRegistrationForm();

        Console.Clear();
        Console.WriteLine("-----------CREATE CLIENT-----------");
        Console.WriteLine("");
        Console.WriteLine("What is the name of the client company the project is for?");
        Console.Write("Company name: ");
        client.CompanyName = Console.ReadLine()!;

        var result = await _clientService.CreateClientAsync(client);
        if (result)
        {
            Console.WriteLine("Client was created sucessfully.");
        }
        else
        {
            Console.WriteLine("Client was not created.");
        }
        Console.ReadLine();
    }

    public async Task ViewAllClientsOption()
    {
        Console.Clear();
        Console.WriteLine("---------VIEW ALL CLIENTS----------");
        Console.WriteLine("");

        var clients = await _clientService.GetClientsAsync();

        if (clients.Any())
        {
            foreach (var client in clients)
            {
                Console.WriteLine($"{"Id: ",-10} {client.Id}");
                Console.WriteLine($"{"Client: ",-10} {client.CompanyName}");
                Console.WriteLine("");
            }
        }
        else
        {
            Console.WriteLine("No clients found.");
        }
        Console.ReadLine();
    }

    public async Task UpdateClientOption()
    {
        Console.Clear();
        Console.WriteLine("----------UPDATE CLIENT------------");
        Console.WriteLine("");

        Console.WriteLine("What is the id of the client you want to update?");
        var stringId = Console.ReadLine()!;

        if (int.TryParse(stringId, out int intId))
        {
            var client = await _clientService.GetClientByIdAsync(intId);
            if (client == null)
            {
                Console.WriteLine("No client found with this id.");
            }
            else
            {
                Console.WriteLine($"{"Id: ",-10} {client.Id}");
                Console.WriteLine($"{"Client: ",-10} {client.CompanyName}");
                Console.WriteLine("");

                var clientUpdateForm = ClientFactory.CreateUpdateForm();
                clientUpdateForm.Id = client.Id;
                clientUpdateForm.CompanyName = client.CompanyName;

                Console.Clear();
                Console.WriteLine("----------UPDATE CLIENT------------");
                Console.WriteLine("");
                Console.WriteLine("Who is the client?");
                Console.Write("Company name: ");
                var companyName = Console.ReadLine()!;
                if (!string.IsNullOrEmpty(companyName) && companyName != client.CompanyName)
                    clientUpdateForm.CompanyName = companyName;

                client = await _clientService.UpdateClientAsync(clientUpdateForm);
                if (client != null)
                {
                    Console.WriteLine("Client was updated sucessfully.");

                }
                else
                {
                    Console.WriteLine("Client was not updated.");
                }
                Console.ReadLine();
            }
        }
        else
        {
            Console.WriteLine("No client found with this id.");
        }
    }

    public async Task DeleteClientOption()
    {
        Console.Clear();
        Console.WriteLine("-----------DELETE CLIENT-----------");
        Console.WriteLine("");

        Console.WriteLine("What is the id of the client you want to delete?");
        var stringId = Console.ReadLine()!;

        if (int.TryParse(stringId, out int intId))
        {
            var client = await _clientService.GetClientByIdAsync(intId);
            if (client == null)
            {
                Console.WriteLine("No client found with this id.");
            }
            else
            {
                var result = await _clientService.DeleteClientAsync(client.Id);
                if (result)
                {
                    Console.WriteLine("Client was deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Client was not deleted.");
                }
            }
        }
        else
        {
            Console.WriteLine("No client found with this id.");
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
