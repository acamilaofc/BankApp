using BankApp;

string? option = "";

while (true)
{
    Menu.ShowMenu();
    option = Console.ReadLine();

    switch (option?.ToUpper())
    {
        case "1":
            Menu.ListAccounts();
            break;
        case "2":
            Menu.NewAccount();
            break;
        case "3":
            Menu.Transfer();
            break;
        case "4":
            Menu.Withdraw();
            break;
        case "5":
            Menu.Deposit();
            break;
        case "C":
            Console.Clear();
            break;
        case "X":
            Console.WriteLine();
            Console.WriteLine("Obrigado por utilizar nossos serviços.");
            Thread.Sleep(5000);
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Opção inexistente!");
            break;
    }
}