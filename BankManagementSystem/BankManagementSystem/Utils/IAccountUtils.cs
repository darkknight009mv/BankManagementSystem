namespace BankManagementSystem.Utils
{
    public interface IAccountUtils
    {
        float GetInterestRate();

        int GetMaxNumberOfWithdrwalPerDay();

        int GetMinBalanceToBeMaintained();

        int GetMaxBalanceToDeposit();

        int GetMaxWithDrawalLimit();
    }
}