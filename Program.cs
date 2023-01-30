public class Program
{
    // FILE_ORGANIZATION is an array of strings that matches up with the headers in the Tickets.csv file
    static readonly string[] FILE_ORGANIZATION = { "Ticket ID", "Summary", "Status", "Priority", "Submitter", "Assigned", "Watching" };
    const string FILE = "Tickets.csv";
    // Tickets.csv will contain a list of comma separated values in the following format:
    // TicketID,Summary,Status,Priority,Submitter,Assigned,Watching
    // Additional people listed under watching will be separated with a | and split later on

    // Main method
    static void Main(string[] args)
    {
        // On program initialization, check if the Tickets.csv file exists.
        // If the file does not exist, create the file with the name Tickets.csv, and continue.
        if (!File.Exists(FILE))
        {
            StreamWriter sw = new StreamWriter(FILE);
            sw.Close();
        }
        Menu();
    }
    // Displays the main menu and accepts an input
    static void Menu()
    {
        // Creates a string to hold the user's input value from the main menu
        string menuInput = "";
        do
        {

            // Main Menu options output to the console.
            Console.WriteLine("Ethan Fridley's Fantastical Ticketing System\n");
            Console.WriteLine("1) Read Tickets");
            Console.WriteLine("2) Add Tickets");
            Console.WriteLine("X) Exit");
            Console.Write("\nPlease select an option from above: ");

            // Stores the user's input value in the menuInput string
            // Automatically converted to uppercase to make the switch statement easier
            menuInput = Console.ReadLine().ToUpper();

            // Menu option logic structure based on the value of menuInput
            switch (menuInput)
            {
                // Read Tickets menu option
                case "1":
                    ReadTickets();
                    break;
                // Add Tickets menu option
                case "2":
                    AddTickets();
                    break;
                // Exit menu option
                case "X":
                case "EXIT":
                    break;
                // Bad input value. Default case
                default:
                    Console.WriteLine($"{menuInput} is not a valid option.");
                    break;
            }

        } while (menuInput != "X" && menuInput != "EXIT");

    }

    // Reads the file and displays all information to the console
    static void ReadTickets()
    {
        // Create a new StreamReader reading the Tickets.csv file
        StreamReader sr = new StreamReader(FILE);

        // Loop through this block until the end of the Tickets.csv file
        while (!sr.EndOfStream)
        {

            // Stores the full line of the csv iteration in the variable line
            string line = sr.ReadLine();

            // Splits the full line using the comma as a delimiter
            string[] arr = line.Split(',');

            // Outputs each of the values in the line, now stored as an array, to the console with its corresponding label
            Console.WriteLine($"Ticket ID: {arr[0]}");
            Console.WriteLine($"Summary: {arr[1]}");
            Console.WriteLine($"Status: {arr[2]}");
            Console.WriteLine($"Priority: {arr[3]}");
            Console.WriteLine($"Submitter: {arr[4]}");
            Console.WriteLine($"Assigned: {arr[5]}");
            // TODO: When moving to Ticket objects, change this to another split with the '|' as the delimiter
            // TODO: Each person under watching should be added to a watching List in the Ticket object
            Console.WriteLine($"Watching: {arr[6].Replace("|", ", ")}");
            Console.WriteLine("");

        }
        // Close the StreamReader
        sr.Close();

        // Causes the program to wait for user input before continuing back to the main menu
        Console.WriteLine("\nPress any key to continue");
        Console.ReadKey();
    }

    // Loops through and prompts the user for input to add to CSV file
    static void AddTickets()
    {
        // This string will be appended to and contain the full string of new comma separated values
        string csvString = "";
        // Loops through each of the headers in FILE_ORGANIZATION except for the last one
        // "Watching" is a special case and is handled later
        for (int i = 0; i < FILE_ORGANIZATION.Length - 1; i++)
        {
            // Prompts the user for input and appends that to previously created csvString with a comma following
            Console.WriteLine($"Please enter the {FILE_ORGANIZATION[i]}: ");
            csvString += Console.ReadLine() + ",";
        }

        // Calls the AddWatching method to loop through and add as many people watching the ticket as the user would like
        csvString += AddWatching();

        // Creates a new instance of the streamwriter set to append to the end of the current Tickets.csv file
        // Appends the full csvString to the of Tickets.csv and closes the streamwriter after
        StreamWriter sw = new StreamWriter(FILE, append: true);
        sw.WriteLine(csvString);
        sw.Close();
    }
    
    // AddWatching prompts the user to add as many people "Watching" a ticket as they would like
    // Returns a string of names separated by a "|"
    static string AddWatching() {
        // Will contain the full list of people added as "Watching" the ticket separated by a "|"
        string csvString = "";
        // Temporary variable to contain user input. Will be overwritten every loop
        // Default value of "Y" to allow the user to enter at least one person as watching the ticket 
        string input = "Y";
        while (input != "N")
        {
            // If the user would like to continue adding people watching, or if it's the first iteration through the loop,
            // the csvString is appended with another name from the user's input
            if (input == "Y")
            {
                Console.WriteLine($"Please enter one person watching: ");
                csvString += Console.ReadLine();
            }

            // Prompts the user if they would like to continue and receives their input
            Console.WriteLine("Would you like to add another person watching? (Y/N)");
            input = Console.ReadLine().ToUpper();

            // If the user would like to continue, a "|" delimiter is appended to csvString before continuing with the loop to add another person
            if (input == "Y")
            {
                csvString += "|";
            }
            // Checks if the input is not the other possible option "N"
            // Notifies the user of the issue and continues with the loop to ask if they would like to add another person
            else if (input != "N")
            {
                Console.WriteLine("Not a valid selection.");
            }
        }
        // Returns the full csv string delimited by "|"
        return csvString;
    }
}