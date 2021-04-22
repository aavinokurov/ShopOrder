using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ShopOrders
{
    public class SQLFunctions
    {
        #region Поля

        /// <summary>
        /// Соединение с SQL
        /// </summary>
        private SqlConnection connection;

        #endregion

        #region Конструкторы

        public SQLFunctions(SqlConnection connection)
        {
            this.connection = connection;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Возраащет True - если клиент есть в базе данных
        /// </summary>
        /// <param name="nameCustomer">Имя клиента</param>
        /// <returns></returns>
        public bool FindCustomer(string nameCustomer)
        {
            // Формируем запрос
            string sqlCommand = $"SELECT * FROM [Customer] WHERE [customer_name] = N'{nameCustomer}'";

            SqlCommand command = new SqlCommand(sqlCommand, connection);

            // Если строка пустая возращаем False, иначе True
            return !string.IsNullOrEmpty((string)command.ExecuteScalar());
        }

        /// <summary>
        /// Возращает кол-во заказов у клиента
        /// </summary>
        /// <param name="customer">Клиент</param>
        /// <returns></returns>
        public int GetQuantityOrders(Customer customer)
        {
            // Формируем запрос
            string sqlCommand = $"SELECT COUNT([idOrder]) FROM [Order] WHERE [idCustomer] = N'{customer.Name}'";

            SqlCommand command = new SqlCommand(sqlCommand, connection);

            return (int)command.ExecuteScalar();
        }

        /// <summary>
        /// Возращает массив заказов клиента
        /// </summary>
        /// <param name="customer">Клиент</param>
        /// <returns></returns>
        public Order[] GetOrders (Customer customer)
        {
            List<Order> orders = new List<Order>();
            
            string sqlCommand = "SELECT " + // Формируем запрос
                     "[Order].[idOrder]," +
                     "[Order].[date]," +
                     "[Product].[idProduct]," +
                     "[Product].[name]," +
                     "[Product].[price]," +
                     "[OrderProduct].[quantity] " +
                     "FROM [Order] JOIN [OrderProduct] ON([Order].[idOrder] = [OrderProduct].[idOrder]) " +
                     "JOIN [Product] ON([OrderProduct].[idProduct] = [Product].[idProduct]) " +
                     $"WHERE idCustomer = N'{customer.Name}'";

            SqlCommand command = new SqlCommand(sqlCommand, connection); 

            SqlDataReader reader = command.ExecuteReader();

            int currentOrder = -1; // Текущий заказ

            List<Product> products = null;
            List<int> quantityProducts = null;

            int idOrder;
            DateTime date = new DateTime();
            int idProduct;
            string nameProduct;
            double priceProduct;
            int quantity;

            while (reader.Read())
            {
                idOrder = reader.GetInt32(0);

                if (currentOrder != idOrder) // Если текущий заказ не равен нынешнему, 
                                             //то создаем новый заказ
                {
                    if (currentOrder != -1)
                    {
                        orders.Add(new Order(currentOrder, customer, date, products.ToArray(), quantityProducts.ToArray()));
                    }

                    products = new List<Product>();

                    quantityProducts = new List<int>();

                    currentOrder = idOrder;
                }

                date = reader.GetDateTime(1);
                idProduct = reader.GetInt32(2);
                nameProduct = reader.GetString(3);
                priceProduct = Convert.ToDouble(reader[4]);
                quantity = reader.GetInt32(5);

                products.Add(new Product(idProduct, nameProduct, priceProduct));

                quantityProducts.Add(quantity);

            }

            reader.Close();

            orders.Add(new Order(currentOrder, customer, date, products.ToArray(), quantityProducts.ToArray()));

            return orders.ToArray();
        }

        /// <summary>
        /// Выводит в консоль информацию о заказе клиента
        /// </summary>
        /// <param name="orders">Массив заказов клиента</param>
        public void PrintCustomerOrder(Order[] orders)
        {
            Console.WriteLine($"Найдено заказов: {orders.Length}");

            double sum;

            for (int i = 0; i < orders.Length; i++)
            {
                Console.WriteLine($"Заказ №{orders[i].NumOrder}, " +
                                  $"Дата Создания {orders[i].DateOrder.ToShortDateString()}");

                Console.WriteLine("Состав:");

                sum = 0;

                for (int j = 0; j < orders[i].Product.Length; j++)
                {
                    double price = orders[i].Product[j].Price;

                    int quantity = orders[i].QuantityProducts[j];

                    Console.WriteLine($"{orders[i].Product[j].NameProduct} " +
                                      $"– {price}руб " +
                                      $"({quantity}шт) " +
                                      $"– {price * quantity}руб");

                    sum += price * quantity;
                }

                Console.WriteLine($"Общая стоимость: {sum}руб");

                Console.WriteLine("------");
            }
        }
        
        #endregion
    }
}
