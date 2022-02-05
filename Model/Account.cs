namespace BankApp.Model
{
    public class Account
    {
        public int Id { get; private set; }
        public string Owner { get; private set; }
        public double Balance { get; private set; }
        public double Credit { get; private set; }

        public Account(string owner, double balance)
        {
            this.Id = default;
            this.Owner = owner;
            this.Balance = balance;
            this.Credit = (double)Math.Floor(balance * 0.20);
        }

        private void CreditReadjustment()
        {
            this.Credit = (double)Math.Floor(this.Balance * 0.20);
        }

        public void Withdraw(double value)
        {
            this.Balance -= value;
            CreditReadjustment();
        }
        public void Deposit(double value)
        {
            this.Balance += value;
            CreditReadjustment();
        }


        public override string ToString()
        {
            string @return = $"Bem vindo(a) {this.Owner}" + Environment.NewLine;
            @return += "As informações sobre sua conta são:" + Environment.NewLine;
            @return += $"ID #{this.Id}" + Environment.NewLine;
            @return += $"Saldo: R$ {this.Balance.ToString("0.00")}" + Environment.NewLine;
            @return += $"Crédito: R$ {this.Credit.ToString("0.00")}" + Environment.NewLine;
            return @return;
        }
    }
}