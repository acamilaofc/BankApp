using BankApp.Model;

namespace BankApp
{
    public static class Menu
    {
        public static void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("BankApp a seu dispor!!!");

            Console.WriteLine("1 - Listar contas");
            Console.WriteLine("2 - Inserir nova conta");
            Console.WriteLine("3 - Transferir");
            Console.WriteLine("4 - Sacar");
            Console.WriteLine("5 - Depositar");
            Console.WriteLine("C - Limpar Tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine();
            Console.Write("Informe a opção desejada: ");

        }
        public static void ListAccounts()
        {
            List<Account> accounts = new List<Account>();
            using (var context = new Context.AccountContext())
            {
                accounts = context.Accounts.ToList();
            }

            if (accounts.Count == 0) Console.WriteLine("Nenhuma conta cadastrada");
            else
                foreach (Account item in accounts)
                {
                    Console.WriteLine(item.ToString());
                    Console.WriteLine();
                }
        }
        public static bool NewAccount()
        {
            Console.WriteLine("Inserir nova conta");

            Console.Write("Digite o Nome do Cliente: ");
            string? entryOwner = Console.ReadLine();

            if (entryOwner == null || entryOwner.Length <= 1)
            {
                Console.WriteLine("Inválido");
                return false;
            }

            Console.Write("Digite o saldo inicial: ");
            if (!(double.TryParse(Console.ReadLine(), out double entryBalance)))
            {
                entryBalance = 0.0;
                Console.WriteLine("Saldo inválido");
            }

            Account account = new Account(entryOwner, entryBalance);
            if (PersistNewAccount(account)) return true;
            else return false;
        }
        public static bool Transfer()
        {
            Console.Write("Digite o ID da conta que deseja movimentar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var ownerAccount = Read(id);
                Console.Write("Digite a quantidade que deseja transferir: ");
                if (ownerAccount != null && double.TryParse(Console.ReadLine(), out double value))
                {
                    Console.Write("Digite o ID da conta destino: ");
                    if (int.TryParse(Console.ReadLine(), out int destinyID))
                    {
                        var destinyAccount = Read(destinyID);
                        if (destinyAccount != null)
                        {
                            ownerAccount.Withdraw(value);
                            destinyAccount.Deposit(value);

                            SaveAccountChanges(ownerAccount);
                            SaveAccountChanges(destinyAccount);

                            return true;
                        }
                    }
                }
            }
            Console.WriteLine("Ops... Algo deu errado, tente novamente.");
            return false;
        }

        public static bool Withdraw()
        {
            Console.Write("Digite o ID da conta que deseja movimentar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var account = Read(id);
                Console.Write("Digite a quantidade que deseja sacar: ");
                if (account != null && double.TryParse(Console.ReadLine(), out double value))
                {
                    account.Withdraw(value);
                    SaveAccountChanges(account);
                    return true;
                }
            }
            Console.WriteLine("Ops... Algo deu errado, tente novamente.");
            return false;
        }
        public static bool Deposit()
        {
            Console.Write("Digite o ID da conta que deseja movimentar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var account = Read(id);
                Console.Write("Digite a quantidade que deseja depositar: ");
                if (account != null && double.TryParse(Console.ReadLine(), out double value))
                {
                    account.Deposit(value);
                    SaveAccountChanges(account);
                    return true;
                }
            }
            Console.WriteLine("Ops... Algo deu errado, tente novamente.");
            return false;
        }
        public static Account? Read(int id)
        {
            Account? account = null;
            using (var context = new Context.AccountContext())
            {
                account = context.Accounts?.SingleOrDefault(a => a.Id.Equals(id));
            }
            if (account != null && account.Id == id) return account;
            return null;
        }

        private static bool PersistNewAccount(Account account)
        {
            using (var context = new Context.AccountContext())
            {
                try
                {
                    context.Add(account);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        private static bool SaveAccountChanges(Account account)
        {
            using (var context = new Context.AccountContext())
            {
                try
                {
                    context.Update(account);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}