// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;
﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.IO.Compression;
using System.Security.Authentication;
using System.Xml.Serialization;

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
        AvailableUntil = new DateTime(2023, 12, 2),
        PlantType = "flower"
    },
    new Plant()
    {
        Species = "Daisy",
        LightNeeds = 4,
        AskingPrice = 13.00M,
        City = "Nashville",
        ZIP = 37013,
        Sold = false,
        AvailableUntil = new DateTime(2023, 11, 2),
        PlantType = "flower"
    },
    new Plant()
    {
        Species = "Pothos",
        LightNeeds = 1,
        AskingPrice = 14.00M,
        City = "Nashville",
        ZIP = 37015,
        Sold = true,
        AvailableUntil = new DateTime(2023, 12, 29),
        PlantType = "flower"
    },
    new Plant()
    {
        Species = "Snake Plant",
        LightNeeds = 2,
        AskingPrice = 9.00M,
        City = "Nashville",
        ZIP = 37086,
        Sold = false,
        AvailableUntil = new DateTime(2023, 10, 2),
        PlantType = "bush"
    },
    new Plant()
    {
        Species = "Cactus",
        LightNeeds = 5,
        AskingPrice = 16.00M,
        City = "Nashville",
        ZIP = 37072,
        Sold = true,
        AvailableUntil = new DateTime(2023, 11, 24),
        PlantType = "bush"
    },
    new Plant()
    {
        Species = "Rose",
        LightNeeds = 3,
        AskingPrice = 16.00M,
        City = "Springfield",
        ZIP = 37072,
        Sold = false,
        AvailableUntil = new DateTime(2023, 11, 26),
        PlantType = "flower"
    },
    new Plant()
    {
        Species = "Tulip",
        LightNeeds = 4,
        AskingPrice = 7.00M,
        City = "Pearl",
        ZIP = 37072,
        Sold = false,
        AvailableUntil = new DateTime(2023, 11, 15),
        PlantType = "flower"
    }
};


Random random = new Random();
Plant randomPlant = null;

while (randomPlant == null)
{
    //this generates a random number between 0 and the number of elements in the 'plants' collection
    // using the 'Next" method of the 'Random' object. the result is stored in 'randomPlantIndex'
    int randomPlantIndex = random.Next(plants.Count);
    //Retrieves a Plant object from the plants collection using the randomly generated index 
    //and assigns it to the variable possibleRandomPlant.
    Plant possibleRandomPlant = plants[randomPlantIndex];
    if (!possibleRandomPlant.Sold)
    {
        randomPlant = possibleRandomPlant;
    }
}

string greeting = "Welcome to ExtraVert!";

Console.WriteLine(greeting);


//this code makes the main menu
string choice = null;
while (choice != "0")
{
    Console.WriteLine(@"Choose an option:
    0. Exit
    1. Plant List
    2. Post a Plant for Adoption
    3. Adopt a Plant
    4. Delist a Plant
    5. Display a Random Plant
    6. Search for a Plant by Light Needs
    7. View Plant Statistics
    8. Inventory by Species");
    choice = Console.ReadLine();
    Console.Clear();
    //this is the functionallity for the main menu
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
        case "4":
            DelistPlants();
            break;
        case "5":
            Console.WriteLine(@$"Here is your random plant: {PlantDetails(randomPlant)}");
            break;
        case "6":
            SearchPlants();
            break;
        case "7":
            ViewStatistics();
            break;
        case "8":
            InventoryBySpecies();
            break;
        default:
            Console.WriteLine("Invalid input. Please choose a valid option.");
            break;
    }
}

//this is what makes the list of plants
void ListPlants()
{
    Console.WriteLine("Plants:");
    for (int i = 0; i < plants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {plants[i].Species} in {plants[i].City} {(plants[i].Sold ? "was sold" : $"is available for {plants[i].AskingPrice} dollars.")}");
    }
}


void PostPLantForAdoption()
{
    Console.WriteLine("Please enter the details for the plant you are posting:");
    Console.WriteLine("What species is the plant?");
    string plantPostedSpecies = Console.ReadLine();


    //uses a while loop to repeatedly prompt the user to input a valid integer from 1-5
    Console.WriteLine("How much light does this plant need on a scale of 1-5?");
    int plantPostedLightNeeds;
    while (!int.TryParse(Console.ReadLine(), out plantPostedLightNeeds) || plantPostedLightNeeds < 1 || plantPostedLightNeeds > 5)
    {
        Console.WriteLine("Invalid input. Please enter a number between 1 and 5 for light needs.");
    }

    //uses a while loop to repeatedly prompt the user to input a valid non-negative decimal value
    Console.WriteLine("How much does this plant cost?");
    decimal plantPostedAskingPrice;
    while (!decimal.TryParse(Console.ReadLine(), out plantPostedAskingPrice) || plantPostedAskingPrice < 0)
    {
        Console.WriteLine("Invalid input. Please enter a non-negative number for asking price.");
    }

    Console.WriteLine("Where is this plant located?");
    string plantPostedCity = Console.ReadLine();

    //uses a while loop to repeatedly prompt the user to input a valid non-negative integer value
    Console.WriteLine("What zip code is the plant in?");
    int plantPostedZIP;
    while (!int.TryParse(Console.ReadLine(), out plantPostedZIP) || plantPostedZIP < 0)
    {
        Console.WriteLine("Invalid input. Please enter a non-negative number for ZIP code.");
    }
    //initializes a 'DateTime' variable with the current date and time
    Console.WriteLine("Choose when you would like your post to expire, following the MM/DD/YYY format:");
    DateTime plantPostedAvailableUntil = DateTime.Now;

    //uses a while loop to repeatedly prompt the user for the expiration date until a valid date
    //in the future is entered
    while (plantPostedAvailableUntil < DateTime.Now)
    {
        try
        {
            plantPostedAvailableUntil = DateTime.Parse(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Date format was invalid. Returning to main menu.");
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine("Date Entry Error");
        }

        string[] plantTypes =
{
    "tree",
    "bush",
    "flower",
    "herb"
};
        Console.WriteLine("What type of plant is it? Enter a number:");
        for (int i = 0; i < plantTypes.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {plantTypes[i]}");
        }
        int plantPostedPlantTypeUserInput = -1;
        string plantPostedPlantType = "";
        while (plantPostedPlantTypeUserInput < 1 || plantPostedPlantTypeUserInput > plantTypes.Length)
        {
            try
            {
                plantPostedPlantTypeUserInput = int.Parse(Console.ReadLine());
                switch (plantPostedPlantTypeUserInput)
                {
                    case 1:
                        plantPostedPlantType = plantTypes[0];
                        break;
                    case 2:
                        plantPostedPlantType = plantTypes[1];
                        break;
                    case 3:
                        plantPostedPlantType = plantTypes[2];
                        break;
                    case 4:
                        plantPostedPlantType = plantTypes[3];
                        break;
                    default:
                        Console.WriteLine("Invalid input.");
                        break;
                }
                if (plantPostedPlantTypeUserInput < 1 || plantPostedPlantTypeUserInput > plantTypes.Length)
                {
                    throw new ValidationException($"Please enter a number between 1-{plantTypes.Length}.");
                }
            }
            catch (ValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }
        //created a new Plant object and set its properties using the collected info
        Plant plantPosted = new Plant();
        plantPosted.Species = plantPostedSpecies;
        plantPosted.LightNeeds = plantPostedLightNeeds;
        plantPosted.AskingPrice = plantPostedAskingPrice;
        plantPosted.City = plantPostedCity;
        plantPosted.ZIP = plantPostedZIP;
        plantPosted.Sold = false;
        plantPosted.AvailableUntil = plantPostedAvailableUntil;
        plantPosted.PlantType = plantPostedPlantType;
        plants.Add(plantPosted);

        Console.WriteLine($"Your {plantPostedSpecies} has been posted for adoption!");

    }
}

//this is checking for plants that are unsold and have an available date for the future
void UnsoldPlantsList()
{
    //this iterates over each element in the 'plants' collection
    for (int i = 0; i < plants.Count; i++)
    {
        //this checks if the plant at index 'i' in the 'plants' collection is not sold
        //and its availability expiration date is in the future
        if (!plants[i].Sold && plants[i].AvailableUntil > DateTime.Now)
        {
            //this displays the species of the plant at index 'i'
            //the index is incremented by 1 so that the count starts at 1 instead of 0
            Console.WriteLine($"{i + 1}. {plants[i].Species}");
        }
    }
}


void AdoptAPlant()
{
    do
    {
        Console.WriteLine("Which plant do you want to adopt? Enter a number:");

        //calls the list of unsold plants we made
        UnsoldPlantsList();

        //lets us return to the main menu again
        Console.WriteLine("Enter 0 to return to main menu");

        try
        {
            //this reads the users input as a string, parses it into an integer
            int response = int.Parse(Console.ReadLine());
            if (response == 0)
            {
                Console.WriteLine("Returning to main menu");
                return;
            }
            //retrieves the plant chosen by user from 'plants' collection 
            //using the index intered by the user (adjusted by subtracting 1)
            Plant chosenPlant = plants[response - 1];
            //checks if the chosen plant is sold, or if the expiration date is in the past
            if (chosenPlant.Sold || chosenPlant.AvailableUntil <= DateTime.Now)
            {
                Console.WriteLine("This plant is not available. Please choose another one.");
            }
            else
            {
                //marks the chosen plant as sold
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

void DelistPlants()
{
    //declares a variable named chosenPlant of type 'Plant' and initializes it to 'null'
    Plant chosenPlant = null;
    Console.WriteLine("Which plant do you want to remove?");
    //brings in our list of plants created earlier
    ListPlants();

    while (chosenPlant == null)
    {
        try
        {
            //reads users input as a string, parses it to an integer, an stores in variable 'response'
            int response = int.Parse((Console.ReadLine()));
            chosenPlant = plants[response - 1];
            //removes the plant from the 'plants' collection using the same index the user 
            //entered by the user (adjusted by subtracting 1)
            plants.RemoveAt(response - 1);
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
    }
    Console.WriteLine($"You removed the {chosenPlant.Species}.");
}


//this allows the user to choose their plant light needs, and shows the available plant type
//for the number they entered
void SearchPlants()
{
    Console.WriteLine("Please enter the light needs you would like, between 1-5:");

    int userAnswer = int.Parse(Console.ReadLine());

    //this declares a new list of 'Plant' object named 'lightPlants' 
    //this list will store plants that match the user's specified light needs
    List<Plant> lightPlants = new List<Plant>();

    for (int i = 0; i < plants.Count; i++)
    {
        //checks if the light needs of the plant at index 'i' in the plants collection
        //are less that or equal to the users specified light needs
        if (plants[i].LightNeeds <= userAnswer)
        {
            //adds the plantat index 'i' from the 'plants' collection to the 'lightPlants'
            //list because it meets the users specified light needs
            lightPlants.Add(plants[i]);
        }
    }
    Console.WriteLine("Plants within your light needs range:");

    //iterates over each element in the 'lightPlants' list
    foreach (Plant plant in lightPlants)
    {
        //outputs the index(plus 1 for a more user-friendly display)
        //and the species of the plant in the 'lightPlants' list
        Console.WriteLine($"{lightPlants.IndexOf(plant) + 1}. {plant.Species}");
    }
}

void ViewStatistics()
{
    //initializing several variables to store the statistics info
    string lowestPricedPlant = "";
    decimal lowestPrice = decimal.MaxValue;
    int plantsAvailable = 0;
    string highestLightNeedsPlant = "";
    int highestLightNeeds = 0;
    decimal totalLightNeeds = 0;
    int plantsAdopted = 0;

    foreach (Plant plant in plants)
    {
        //checks if asking price of current plant is lower than current lowest price
        if (plant.AskingPrice < lowestPrice)
        {
            //updates these with the info from the current plant
            lowestPrice = plant.AskingPrice;
            lowestPricedPlant = plant.Species;
        }
        //checks if current plant is not sold and has not expired
        if (!plant.Sold && plant.AvailableUntil > DateTime.Now)
        {
            plantsAvailable++;
        }
        //checks if light needs of current plant are higher than current highest light needs
        if (plant.LightNeeds > highestLightNeeds)
        {
            //updates these with info from current plant
            highestLightNeeds = plant.LightNeeds;
            highestLightNeedsPlant = plant.Species;
        }
        if (plant.Sold)
        {
            plantsAdopted++;
        }
        //adds light needs of current plant to 'totalLightNeeds' variable
        totalLightNeeds += plant.LightNeeds;
    }

    Console.WriteLine(@$"Current Statistics:
                    1. Lowest price plant: {lowestPricedPlant} (${lowestPrice})
                    2. Number of plants available: {plantsAvailable}
                    3. Plant with highest light needs: {highestLightNeedsPlant}
                    4. Average light needs: {(double)totalLightNeeds / plants.Count}
                    5. Percentage of plants adopted: {(double)plantsAdopted / plants.Count * 100}%");
}


//the capital Plant is the parameter type, the lower case plant is the parameter
string PlantDetails(Plant plant)
{
    string plantString = $"{plant.Species} in {plant.City} for {plant.AskingPrice}. This plant needs a light of {plant.LightNeeds} from a scale of 1-5.";
    return plantString;
}

void InventoryBySpecies()
{
    Dictionary<string, int> plantInventory = new Dictionary<string, int>();
    foreach (Plant plant in plants)
    {
        int plantNumber;
        bool plantNumberSuccess = plantInventory.TryGetValue(plant.Species, out plantNumber);

        if (plantNumberSuccess)
        {
            plantInventory[plant.Species]++;
        }
        else
        {
            plantInventory.Add(plant.Species, 1);
        }
    }
    foreach (KeyValuePair<string, int> kv in plantInventory)
    {
        Console.WriteLine($"Species: {kv.Key}, Amount: {kv.Value}");
    }
}