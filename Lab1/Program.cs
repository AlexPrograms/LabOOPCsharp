using System.Globalization;

namespace Labs;

public abstract class BankAccount
{
    public string AccountNumber { get; set; }
    public string Owner { get; set; }
    public decimal Balance { get; protected set; }
    

    public BankAccount(string accountNumber, string owner, decimal initialBalance)
    {
        AccountNumber = accountNumber;
        Owner = owner;
        Balance = initialBalance;
    }

    public abstract void DisplayBalance();
    public abstract void Deposit(decimal amount);
    public abstract void Withdraw(decimal amount);
}

public class DepositAccount : BankAccount
{
    private decimal InterestRate;

    public DepositAccount(string accountNumber, string owner, decimal initialBalance, decimal interestRate)
        : base(accountNumber, owner, initialBalance)
    {
        InterestRate = interestRate;
    }

    public override void DisplayBalance()
    {
        Console.WriteLine($"Deposit Account Balance: {Balance:C}");
    }

    public override void Deposit(decimal amount)
    {
        Balance += amount;
        Console.WriteLine($"Deposited {amount:C}, new balance is {Balance:C}");
    }

    public override void Withdraw(decimal amount)
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


public class CurrentAccount : BankAccount
{
    private decimal CreditLimit;

    public CurrentAccount(string accountNumber, string owner, decimal initialBalance, decimal creditLimit)
        : base(accountNumber, owner, initialBalance)
    {
        CreditLimit = creditLimit;
    }

    public override void DisplayBalance()
    {
        Console.WriteLine($"Current Account Balance: {Balance:C}");
    }

    public override void Deposit(decimal amount)
    {
        Balance += amount;
        Console.WriteLine($"Deposited {amount:C}, new balance is {Balance:C}");
    }

    public override void Withdraw(decimal amount)
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
        
        DepositAccount depositAccount = new DepositAccount("12345", "Alice", 1000, 5);
        depositAccount.DisplayBalance();
        depositAccount.Deposit(500);
        depositAccount.AddInterest();
        depositAccount.Withdraw(200);
        depositAccount.DisplayBalance();

        Console.WriteLine();

        CurrentAccount currentAccount = new CurrentAccount("67890", "Bob", 500, 1000);
        currentAccount.DisplayBalance();
        currentAccount.Deposit(300);
        currentAccount.Withdraw(700);
        currentAccount.SetCreditLimit(1500);
        currentAccount.Withdraw(1800);
        currentAccount.Withdraw(1200);
        currentAccount.DisplayBalance();
    }
}

