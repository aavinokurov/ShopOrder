using System;
using System.Data.SqlClient;

namespace ShopOrders
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnectionStringBuilder strCon = new SqlConnectionStringBuilder() // Формирование строки подключения
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "ShopOrder",
                IntegratedSecurity = true,
                Pooling = false
            };

            try
            {
                using(SqlConnection connection = new SqlConnection(strCon.ConnectionString))
                {
                    connection.Open(); // Открываем соединение

                    SQLFunctions sQLFunctions = new SQLFunctions(connection);

                    bool isRun = true;

                    while (isRun)
                    {
                        Console.WriteLine("Введите имя клиента и нажмите Enter:");

                        string idCustomer = Console.ReadLine(); // Считываем с консоли имя пользователя

                        if(sQLFunctions.FindCustomer(idCustomer)) // Есть ли пользователь в базе данных
                        {
                            Customer customer = new Customer(idCustomer);

                            if (sQLFunctions.GetQuantityOrders(customer) > 0) // Есть ли у пользователя заказы
                            {
                                Order[] order = sQLFunctions.GetOrders(customer); // Формируем массив заказов

                                sQLFunctions.PrintCustomerOrder(order); // Выводим заказы в консоль
                            }
                            else
                            {
                                Console.WriteLine("У данного клиента еще нет заказов.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Данного клиента нет в базе данных!");
                        }

                        string answer;

                        do // Спрашиваем пользователя о продолжении работы
                        {
                            Console.WriteLine("Продолжить работу? (д/н)");
                            answer = Console.ReadLine();
                        } while (answer != "д" && answer != "н");

                        isRun = (answer == "д") ? true : false;

                        Console.Clear();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
