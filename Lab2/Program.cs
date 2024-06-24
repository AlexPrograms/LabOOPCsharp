using System.Globalization;

namespace Lab2;

public interface IBankAccount
{
    string AccountNumber { get; set; }
    string Owner { get; set; }
    decimal Balance { get; }

    void DisplayBalance();
    void Deposit(decimal amount);
    void Withdraw(decimal amount);
}


public interface IDepositAccount : IBankAccount
{
    void AddInterest();
}


public interface ICurrentAccount : IBankAccount
{
    void SetCreditLimit(decimal newCreditLimit);
}


public class DepositAccount : IDepositAccount
{
    public string AccountNumber { get; set; }
    public string Owner { get; set; }
    public decimal Balance { get; private set; }
    private decimal InterestRate;

    public DepositAccount(string accountNumber, string owner, decimal initialBalance, decimal interestRate)
    {
        AccountNumber = accountNumber;
        Owner = owner;
        Balance = initialBalance;
        InterestRate = interestRate;
    }

    public void DisplayBalance()
    {
        Console.WriteLine($"Deposit Account Balance: {Balance:C}");
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
        Console.WriteLine($"Deposited {amount:C}, new balance is {Balance:C}");
    }

    public void Withdraw(decimal amount)
    {
        if (Balance >= amount)
        {
            Balance -= amount;
            Console.WriteLine($"Withdrew {amount:C}, new balance is {Balance:C}");
        }
        else
        {
            Console.WriteLine("Insufficient funds for withdrawal.");
        }
    }

    public void AddInterest()
    {
        decimal interest = Balance * InterestRate / 100;
        Balance += interest;
        Console.WriteLine($"Added interest {interest:C}, new balance is {Balance:C}");
    }
}


public class CurrentAccount : ICurrentAccount
{
    public string AccountNumber { get; set; }
    public string Owner { get; set; }
    public decimal Balance { get; private set; }
    private decimal CreditLimit;

    public CurrentAccount(string accountNumber, string owner, decimal initialBalance, decimal creditLimit)
    {
        AccountNumber = accountNumber;
        Owner = owner;
        Balance = initialBalance;
        CreditLimit = creditLimit;
    }

    public void DisplayBalance()
    {
        Console.WriteLine($"Current Account Balance: {Balance:C}");
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
        Console.WriteLine($"Deposited {amount:C}, new balance is {Balance:C}");
    }

    public void Withdraw(decimal amount)
    {
        if (Balance + CreditLimit >= amount)
        {
            Balance -= amount;
            Console.WriteLine($"Withdrew {amount:C}, new balance is {Balance:C}");
        }
        else
        {
            Console.WriteLine("Exceeds credit limit.");
        }
    }

    public void SetCreditLimit(decimal newCreditLimit)
    {
        CreditLimit = newCreditLimit;
        Console.WriteLine($"Credit limit set to {CreditLimit:C}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        var cultureInfo = new CultureInfo("en-US");
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        while (true)
        {
            Console.WriteLine("Choose account type to open (1. Deposit Account, 2. Current Account) or press 'q' to quit:");
            string choice = Console.ReadLine();

            if (choice == "q")
            {
                break;
            }

            IBankAccount account = null;

            switch (choice)
            {
                case "1":
                    account = CreateDepositAccount();
                    break;
                case "2":
                    account = CreateCurrentAccount();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    continue;
            }

            while (true)
            {
                Console.WriteLine("\nChoose operation: (1. Display Balance, 2. Deposit, 3. Withdraw, 4. Add Interest (Deposit Account), 5. Set Credit Limit (Current Account), 'b' to go back, 'q' to quit)");
                string operation = Console.ReadLine();

                if (operation == "q")
                {
                    return;
                }

                if (operation == "b")
                {
                    break;
                }

                switch (operation)
                {
                    case "1":
                        account.DisplayBalance();
                        break;
                    case "2":
                        Console.Write("Enter amount to deposit: ");
                        decimal depositAmount = decimal.Parse(Console.ReadLine());
                        account.Deposit(depositAmount);
                        break;
                    case "3":
                        Console.Write("Enter amount to withdraw: ");
                        decimal withdrawAmount = decimal.Parse(Console.ReadLine());
                        account.Withdraw(withdrawAmount);
                        break;
                    case "4":
                        if (account is IDepositAccount depositAccount)
                        {
                            depositAccount.AddInterest();
                        }
                        else
                        {
                            Console.WriteLine("This operation is not available for your account.");
                        }
                        break;
                    case "5":
                        if (account is ICurrentAccount currentAccount)
                        {
                            Console.Write("Enter new credit limit: ");
                            decimal newCreditLimit = decimal.Parse(Console.ReadLine());
                            currentAccount.SetCreditLimit(newCreditLimit);
                        }
                        else
                        {
                            Console.WriteLine("This operation is not available for your account.");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid operation. Try again.");
                        break;
                }
            }
        }
    }

    static IDepositAccount CreateDepositAccount()
    {
        Console.Write("Enter account number: ");
        string accountNumber = Console.ReadLine();
        Console.Write("Enter owner name: ");
        string owner = Console.ReadLine();
        Console.Write("Enter initial balance: ");
        decimal initialBalance = decimal.Parse(Console.ReadLine());
        Console.Write("Enter interest rate: ");
        decimal interestRate = decimal.Parse(Console.ReadLine());

        return new DepositAccount(accountNumber, owner, initialBalance, interestRate);
    }

    static ICurrentAccount CreateCurrentAccount()
    {
        Console.Write("Enter account number: ");
        string accountNumber = Console.ReadLine();
        Console.Write("Enter owner name: ");
        string owner = Console.ReadLine();
        Console.Write("Enter initial balance: ");
        decimal initialBalance = decimal.Parse(Console.ReadLine());
        Console.Write("Enter credit limit: ");
        decimal creditLimit = decimal.Parse(Console.ReadLine());

        return new CurrentAccount(accountNumber, owner, initialBalance, creditLimit);
    }
}

