namespace Lab8;

public class CurrentAccount
{
    public decimal Balance { get; private set; }

    public CurrentAccount(decimal initialBalance)
    {
        Balance = initialBalance;
    }

    public void Credit(decimal amount)
    {
        Balance += amount;
    }

    public void Debit(decimal amount)
    {
        if (Balance >= amount)
        {
            Balance -= amount;
        }
        else
        {
            throw new System.InvalidOperationException("Insufficient funds.");
        }
    }
}
