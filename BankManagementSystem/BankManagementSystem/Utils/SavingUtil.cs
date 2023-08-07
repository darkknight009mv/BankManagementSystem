namespace BankManagementSystem.Utils
{
    public class SavingUtil : IAccountUtils
    {
        private float interestRate = 4f;
        private int maxAmountToDeposit = 50000;
        private int maxWithdrawPerDay = 3;
        private int maxAmountWithdraw = 30000;
        private int minBalance = 2000;
        public float GetInterestRate()
        {
           return interestRate;
        }

        public int GetMaxBalanceToDeposit()
        {
            return maxAmountToDeposit;
        }

        public int GetMaxNumberOfWithdrwalPerDay()
        {
            return maxWithdrawPerDay;
        }

        public int GetMaxWithDrawalLimit()
        {
            return maxAmountWithdraw;
        }

        public int GetMinBalanceToBeMaintained()
        {
            return minBalance;
        }
    }
}
