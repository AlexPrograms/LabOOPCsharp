using System;
using System.Collections.Generic;
using System.Globalization;

public interface ILoginProvider
{
    bool Validate(string login, string password);
}

public class GmailAuthProvider : ILoginProvider
{
    private string email;
    private string password;

    public GmailAuthProvider(string email, string password)
    {
        this.email = email;
        this.password = password;
    }

    public bool Validate(string login, string password)
    {
        return this.email == login && this.password == password;
    }
}

public class Privat24AuthProvider : ILoginProvider
{
    private string phoneNumber;
    private string password;

    public Privat24AuthProvider(string phoneNumber, string password)
    {
        this.phoneNumber = phoneNumber;
        this.password = password;
    }

    public bool Validate(string login, string password)
    {
        return this.phoneNumber == login && this.password == password;
    }
}

public class UnauthorizedAccessException : Exception
{
    public UnauthorizedAccessException(string message) : base(message) { }
}

public interface IDigitalWallet
{
    List<string> GetTransactionLog();
    decimal CheckBalance();
    bool Withdraw(decimal amount);
    void Deposit(decimal amount);
    void SetAuthProvider(ILoginProvider authProvider);
    void Unlogin();
}

public class DigitalWallet : IDigitalWallet
{
    private decimal balance;
    private string login;
    private string hashedPassword;
    private List<string> transactionLog;
    private ILoginProvider authProvider;

    public DigitalWallet(decimal initialBalance)
    {
        this.balance = initialBalance;
        this.transactionLog = new List<string>();
    }

    public List<string> GetTransactionLog()
    {
        EnsureAuthenticated();
        return transactionLog;
    }

    public decimal CheckBalance()
    {
        EnsureAuthenticated();
        return balance;
    }

    public bool Withdraw(decimal amount)
    {
        EnsureAuthenticated();
        if (amount > balance)
        {
            transactionLog.Add($"Failed withdrawal of {amount:C} - insufficient funds");
            return false;
        }

        balance -= amount;
        transactionLog.Add($"Withdrew {amount:C}");
        return true;
    }

    public void Deposit(decimal amount)
    {
        EnsureAuthenticated();
        balance += amount;
        transactionLog.Add($"Deposited {amount:C}");
    }

    public void SetAuthProvider(ILoginProvider authProvider)
    {
        this.authProvider = authProvider;
    }

    public void SetCredentials(string login, string password)
    {
        this.login = login;
        this.hashedPassword = password;
    }

    public void Unlogin()
    {
        this.authProvider = null;
        this.login = null;
        this.hashedPassword = null;
    }

    private void EnsureAuthenticated()
    {
        if (authProvider == null || !authProvider.Validate(login, hashedPassword))
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }
    }
}

public class Program
{
    private static DigitalWallet wallet;

    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Digital Wallet");
        
        InitializeWallet();
        AuthenticateUser();
        ShowMainMenu();
    }

    private static void InitializeWallet()
    {
        wallet = new DigitalWallet(1000.00M);
    }

    private static void AuthenticateUser()
    {
        Console.WriteLine("Choose authentication method:");
        Console.WriteLine("1. Gmail");
        Console.WriteLine("2. Privat24");
        int choice = int.Parse(Console.ReadLine());

        Console.Write("Enter login: ");
        string login = Console.ReadLine();

        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        switch (choice)
        {
            case 1:
                wallet.SetAuthProvider(new GmailAuthProvider("hello@gmail.com", "123"));
                break;
            case 2:
                wallet.SetAuthProvider(new Privat24AuthProvider("+123456", "1234"));
                break;
            default:
                Console.WriteLine("Invalid choice. Exiting.");
                Environment.Exit(0);
                break;
        }

        wallet.SetCredentials(login, password);

        try
        {
            wallet.CheckBalance();
            Console.WriteLine("Authentication successful");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine(ex.Message);
            Environment.Exit(0);
        }
    }

    private static void ShowMainMenu()
    {
        var cultureInfo = new CultureInfo("en-US");
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        while (true)
        {
            Console.WriteLine("\nMain Menu:");
            Console.WriteLine("1. Check Balance");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. View Transaction Log");
            Console.WriteLine("5. Unlogin");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    CheckBalance();
                    break;
                case 2:
                    Deposit();
                    break;
                case 3:
                    Withdraw();
                    break;
                case 4:
                    ViewTransactionLog();
                    break;
                case 5:
                    Unlogin();
                    break;
                case 6:
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private static void CheckBalance()
    {
        try
        {
            Console.WriteLine($"Your balance is: {wallet.CheckBalance():C}");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void Deposit()
    {
        try
        {
            Console.Write("Enter amount to deposit: ");
            decimal amount = decimal.Parse(Console.ReadLine());
            wallet.Deposit(amount);
            Console.WriteLine($"Deposited {amount:C} successfully.");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void Withdraw()
    {
        try
        {
            Console.Write("Enter amount to withdraw: ");
            decimal amount = decimal.Parse(Console.ReadLine());
            if (wallet.Withdraw(amount))
            {
                Console.WriteLine($"Withdrew {amount:C} successfully.");
            }
            else
            {
                Console.WriteLine("Failed to withdraw. Insufficient funds.");
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void ViewTransactionLog()
    {
        try
        {
            Console.WriteLine("Transaction Log:");
            foreach (var log in wallet.GetTransactionLog())
            {
                Console.WriteLine(log);
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void Unlogin()
    {
        wallet.Unlogin();
        Console.WriteLine("You have been unlogged.");
        AuthenticateUser();
    }
}
