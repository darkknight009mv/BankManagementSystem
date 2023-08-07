namespace BankManagementSystem.Utils
{
    public class CurrentUtil : IAccountUtils
    {
        private float interestRate = 2f;
        private int maxAmountToDeposit = 100000;
        private int maxWithdrawPerDay = 7;
        private int maxAmountWithdraw = 50000;
        private int minBalance = 0;
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
