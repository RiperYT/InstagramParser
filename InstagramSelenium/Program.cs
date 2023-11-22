using InstagramAutomatization.Data;
using InstagramAutomatization.Services;
using InstagramAutomatization.Services.Abstractions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text;

Console.ForegroundColor = ConsoleColor.Green;
var period = 2000;

while (true)
{
    Console.Write("PLease enter number of loading page period in milliseconds(recommended 2000): ");
    var periodStr = Console.ReadLine();
    if (int.TryParse(periodStr, out int t) && t >= 0)
    {
        period = t;
        break;
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Incorrect number");
        Console.ForegroundColor = ConsoleColor.Green;
    }
}

IInstagram instagram = new InstagramSelenium("C:\\Users\\Andrij\\Downloads\\chromedriver.exe", period);
IInstagramUser? user = null;

Console.WriteLine();

while (true)
{
    try
    {
        if (instagram.GetStatusLogin())
        {
            Console.WriteLine("Please choose what you want to do:");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   1. Get info about user by username\n   2. Get all followers usernames to console\n   3. Get and save all followers info");
            Console.ForegroundColor = ConsoleColor.Green;

            Console.Write("PLease enter number of variant: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            var variant = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Green;

            if (int.TryParse(variant, out int t) && t > 0 && t < 4)
            {
                switch (t)
                {
                    case 1:
                        userInfoConsole();
                        break;
                    case 2:
                        followersUsernamesConsole();
                        break;
                    case 3:
                        followersInfoGetAndSaveConsole();
                        break;
                }
            }
            else
            {
                throw new Exception("Incorrect number");
            }
        }
        else
        {
            Console.WriteLine("Please choose what you want to do:");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   1. Login\n   2. Get info about user by username");
            Console.ForegroundColor = ConsoleColor.Green;

            Console.Write("PLease enter number of variant: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            var variant = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Green;

            if (int.TryParse(variant, out int t) && t > 0 && t < 3)
            {
                switch (t)
                {
                    case 1:
                        loginConsole();
                        break;
                    case 2:
                        userInfoConsole();
                        break;
                }
            }
            else
            {
                throw new Exception("Incorrect number");
            }
        }
        Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(ex.Message + "\n");
        Console.ForegroundColor = ConsoleColor.Green;
    }
}

void loginConsole()
{
    Console.Write("   Enter username: ");
    Console.ForegroundColor = ConsoleColor.Yellow;
    var username = Console.ReadLine();
    Console.ForegroundColor = ConsoleColor.Green;
    if (string.IsNullOrEmpty(username))
        throw new Exception("Username cannot be empty");

    Console.Write("   Enter password: ");
    Console.ForegroundColor = ConsoleColor.Yellow;
    var password = GetConsolePassword();
    Console.ForegroundColor = ConsoleColor.Green;
    if (string.IsNullOrEmpty(password))
        throw new Exception("Password cannot be empty");

    user = instagram.Login(username, password);
}

void userInfoConsole()
{
    Console.Write("   Enter username of user: ");
    Console.ForegroundColor = ConsoleColor.Yellow;
    var username = Console.ReadLine();
    Console.ForegroundColor = ConsoleColor.Green;

    if (!string.IsNullOrEmpty(username))
    {
        var userLocal = instagram.GetUserInfo(username);

        Console.Write($"      Username: ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(userLocal.UserName);
        Console.ForegroundColor = ConsoleColor.Green;

        if (!string.IsNullOrEmpty(userLocal.Name))
        {
            Console.Write($"      Name: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(userLocal.Name);
            Console.ForegroundColor = ConsoleColor.Green;
        }

        if (!string.IsNullOrEmpty(userLocal.Name))
        {
            Console.Write($"      Description: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(userLocal.Description);
            Console.ForegroundColor = ConsoleColor.Green;
        }
    }
    else
    {
        throw new Exception("It cannot be empty");
    }
}

void followersUsernamesConsole()
{
    if (user != null)
    {
        var names = user.GetFollowersUsernames();

        Console.WriteLine("   Usernames of followers:");
        Console.ForegroundColor = ConsoleColor.Yellow;
        foreach (var name in names)
        {
            Console.WriteLine("     " + name);
        }
        Console.ForegroundColor = ConsoleColor.Green;
    }
    else
    {
        throw new Exception("User is not defined");
    }
}

void followersInfoGetAndSaveConsole(){
    if (user != null)
    {
        Console.WriteLine("   Please choose method to save user infos:");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("      1. SCV\n      2. JSON");
        Console.ForegroundColor = ConsoleColor.Green;

        Console.Write("   PLease enter number of method: ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        var variant = Console.ReadLine();
        Console.ForegroundColor = ConsoleColor.Green;

        if (int.TryParse(variant, out int t) && t > 0 && t < 3)
        {
            var list = instagram.GetUsersInfo(user.GetFollowersUsernames());
            ISave save;
            switch (t)
            {
                case 1:
                    save = new SaveSCV();
                    break;
                case 2:
                    save = new SaveJSON();
                    break;
                default:
                    throw new Exception("Incorrect number");
            }
            var path = save.SaveUsersInfo(list);
            Console.Write("   Save to: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(path);
            Console.ForegroundColor = ConsoleColor.Green;
        }
        else
        {
            throw new Exception("Incorrect number");
        }
    }
    else
    {
        throw new Exception("User is not defined");
    }
}


string GetConsolePassword()
{
    StringBuilder sb = new StringBuilder();
    while (true)
    {
        ConsoleKeyInfo cki = Console.ReadKey(true);
        if (cki.Key == ConsoleKey.Enter)
        {
            Console.WriteLine();
            break;
        }

        if (cki.Key == ConsoleKey.Backspace)
        {
            if (sb.Length > 0)
            {
                Console.Write("\b\0\b");
                sb.Length--;
            }

            continue;
        }

        Console.Write('*');
        sb.Append(cki.KeyChar);
    }

    return sb.ToString();
}