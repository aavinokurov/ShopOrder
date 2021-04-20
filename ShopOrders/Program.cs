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

                    bool isRun = true;

                    while (isRun)
                    {
                        Console.WriteLine("Введите имя клиента и нажмите Enter:");

                        string idCustomer = Console.ReadLine(); // Считываем с консоли имя пользователя

                        string sqlCommand = "SELECT " + // Формируем запрос
                                 "[Order].idOrder,[Order].date,Product.[name],Product.price,OrderProduct.quantity " +
                                 "FROM [Order] JOIN OrderProduct ON([Order].idOrder = OrderProduct.idOrder) " +
                                 "JOIN Product ON(OrderProduct.idProduct = Product.idProduct) " +
                                 $"WHERE idCustomer = N'{idCustomer}'";

                        SqlCommand command = new SqlCommand(sqlCommand, connection); 

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows) // Если есть записи, то выводим в консоль
                        {
                            int numOrder = -1;
                            double sum = 0;

                            while (reader.Read())
                            {
                                int order = reader.GetInt32(0);

                                if (numOrder != order) // Если номер заказа неравен текущему, то выводим шапку заказа
                                {
                                    if (sum != 0)
                                    {
                                        Console.WriteLine($"Общая стоимость: {sum}руб.");
                                    }

                                    Console.WriteLine("--------");
                                    Console.WriteLine($"Заказ №{reader[0]}, " +
                                                      $"Дата Создания {reader.GetDateTime(1).ToShortDateString()}");

                                    Console.WriteLine("Состав:");

                                    numOrder = order;

                                    sum = 0;
                                }

                                double price = Convert.ToDouble(reader[3]); // Стоимость товара
                                int quatity = Convert.ToInt32(reader[4]); // Кол-во

                                sum += price * quatity; // Стоимоть продукта в заказе

                                Console.WriteLine($"{reader[2]} - {price}руб. ({quatity}) - {price * quatity}руб.");
                            }

                            Console.WriteLine($"Общая стоимость: {sum}руб.");
                        }
                        else // Если нет, то сообщаем, что данного пользователя нет в базе
                        {
                            Console.WriteLine("Данного пользователя нет в базе!");
                        }

                        reader.Close();

                        string answer;

                        do // Спрашиваем пользователя о продолжении работы
                        {
                            Console.WriteLine("Продолжить работу? (д/н)");
                            answer = Console.ReadLine();
                        } while (answer != "д" && answer != "н");

                        isRun = answer == "д" ? true : false;

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
