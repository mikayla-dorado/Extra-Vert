// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;

List<Plant> plants = new List<Plant>()
{
    new Plant()
    {
        Species = "Lily",
        LightNeeds = 3,
        AskingPrice = 12.00M,
        City = "Pittsfield",
        ZIP = 62363,
        Sold = true,
        AvailableUntil = new DateTime(2023, 12, 2)
    },
    new Plant()
    {
        Species = "Daisy",
        LightNeeds = 4,
        AskingPrice = 13.00M,
        City = "Nashville",
        ZIP = 37013,
        Sold = false,
        AvailableUntil = new DateTime(2023, 11, 2)
    },
    new Plant()
    {
        Species = "Pothos",
        LightNeeds = 1,
        AskingPrice = 14.00M,
        City = "Nashville",
        ZIP = 37015,
        Sold = true,
        AvailableUntil = new DateTime(2023, 12, 29)
    },
    new Plant()
    {
        Species = "Snake Plant",
        LightNeeds = 2,
        AskingPrice = 9.00M,
        City = "Nashville",
        ZIP = 37086,
        Sold = false,
        AvailableUntil = new DateTime(2023, 10, 2)
    },
    new Plant()
    {
        Species = "Cactus",
        LightNeeds = 5,
        AskingPrice = 16.00M,
        City = "Nashville",
        ZIP = 37072,
        Sold = true,
        AvailableUntil = new DateTime(2023, 11, 24)
    },
    new Plant()
    {
        Species = "Rose",
        LightNeeds = 3,
        AskingPrice = 16.00M,
        City = "Springfield",
        ZIP = 37072,
        Sold = false,
        AvailableUntil = new DateTime(2023, 11, 26)
    }
};

string greeting = "Welcome to ExtraVert!";

Console.WriteLine(greeting);


string choice = null;
while (choice != "0")
{
    Console.WriteLine(@"Choose an option:
    0. Exit
    1. Plant List
    2. Post a Plant for Adoption
    3. Adopt a Plant
    4. Delist a Plant");
    choice = Console.ReadLine();
    switch (choice)
    {
        case "0":
            Console.WriteLine("Adios!");
            break;
        case "1":
            ListPlants();
            break;
        case "2":
            PostPLantForAdoption();
            break;
        case "3":
            AdoptAPlant();
            break;
        default:
            Console.WriteLine("Invalid input. Please choose a valid option.");
            break;
    }
}

void ListPlants()
{
    Console.WriteLine("Plants:");
    //this is what makes the list of plants
    for (int i = 0; i < plants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {plants[i].Species} in {plants[i].City} {(plants[i].Sold ? "was sold" : $"is available for {plants[i].AskingPrice} dollars.")}");
    }
}



void PostPLantForAdoption()
{
    //     Console.WriteLine("Please enter the details for the plant you are posting:");
    //     Console.WriteLine("What species is the plant?");
    //     string plantPostedSpecies = Console.ReadLine();
    //     Console.WriteLine("How much light does this plant need on a scale of 1-5?");
    //     int plantPostedLightNeeds = int.Parse(Console.ReadLine());
    //     Console.WriteLine("How much does this plant cost?");
    //     decimal plantPostedAskingPrice = decimal.Parse(Console.ReadLine());
    //     Console.WriteLine("Where is this plant located?");
    //     string plantPostedCity = Console.ReadLine();
    //     Console.WriteLine("What zip code is the plant in?");
    //     int plantPostedZIP = int.Parse(Console.ReadLine());
    //     Console.WriteLine("When do you want your post to expire? Use MM/DD/YYYY format");
    //     DateTime plantPostedAvailableUntil = DateTime.Now;
    //     while (plantPostedAvailableUntil < DateTime.Now)
    // {
    //     try
    //     {
    //         plantPostedAvailableUntil = DateTime.Parse(Console.ReadLine());
    //     }
    //     catch (FormatException)
    //     {
    //         Console.WriteLine("Date format is invalid.");
    //         return;
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine(ex);
    //         Console.WriteLine("Date Entered Error");
    //     }
    // }

    Console.WriteLine("Please enter the details for the plant you are posting:");
    Console.WriteLine("What species is the plant?");
    string plantPostedSpecies = Console.ReadLine();

    Console.WriteLine("How much light does this plant need on a scale of 1-5?");
    int plantPostedLightNeeds;
    while (!int.TryParse(Console.ReadLine(), out plantPostedLightNeeds) || plantPostedLightNeeds < 1 || plantPostedLightNeeds > 5)
    {
        Console.WriteLine("Invalid input. Please enter a number between 1 and 5 for light needs.");
    }

    Console.WriteLine("How much does this plant cost?");
    decimal plantPostedAskingPrice;
    while (!decimal.TryParse(Console.ReadLine(), out plantPostedAskingPrice) || plantPostedAskingPrice < 0)
    {
        Console.WriteLine("Invalid input. Please enter a non-negative number for asking price.");
    }

    Console.WriteLine("Where is this plant located?");
    string plantPostedCity = Console.ReadLine();

    Console.WriteLine("What zip code is the plant in?");
    int plantPostedZIP;
    while (!int.TryParse(Console.ReadLine(), out plantPostedZIP) || plantPostedZIP < 0)
    {
        Console.WriteLine("Invalid input. Please enter a non-negative number for ZIP code.");
    }

    Plant plantPosted = new Plant();
    plantPosted.Species = plantPostedSpecies;
    plantPosted.LightNeeds = plantPostedLightNeeds;
    plantPosted.AskingPrice = plantPostedAskingPrice;
    plantPosted.City = plantPostedCity;
    plantPosted.ZIP = plantPostedZIP;
    plantPosted.Sold = false;
    //plantPosted.AvailableUntil = plantPostedAvailableUntil;
    plants.Add(plantPosted);

    Console.WriteLine($"Your {plantPostedSpecies} has been posted for adoption!");

}


void UnsoldPlantsList()
{
    for (int i = 0; i < plants.Count; i++)
    {
        if (!plants[i].Sold && plants[i].AvailableUntil > DateTime.Now)
        {
            Console.WriteLine($"{i + 1}. {plants[i].Species}");
        }
    }
}


void AdoptAPlant()
{
    do
    {
        Console.WriteLine("Which plant do you want to adopt? Enter a number:");

        UnsoldPlantsList();

        Console.WriteLine("Enter 0 to return to main menu");

        try
        {

            int response = int.Parse(Console.ReadLine());
            if (response == 0)
            {
                Console.WriteLine("Returning to main menu");
                return;
            }
            Plant chosenPlant = plants[response - 1];

            if (chosenPlant.Sold || chosenPlant.AvailableUntil <= DateTime.Now)
            {
                Console.WriteLine("This plant is not available. Please choose another one.");
            }
            else
            {
                chosenPlant.Sold = true;
                Console.WriteLine($"Congratulations! You have adopted a {chosenPlant.Species}.");
                return;
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Please write only integers!");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please choose an item on the list only!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine("Uh-oh! You made an error");
        }
    
} while (true);
}
