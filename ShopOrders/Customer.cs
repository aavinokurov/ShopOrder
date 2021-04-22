namespace ShopOrders
{
    public class Customer
    {
        #region Поля

        /// <summary>
        /// Имя клиента
        /// </summary>
        private string name;

        #endregion

        #region Свойства

        /// <summary>
        /// Имя клиента
        /// </summary>
        public string Name { get { return name; } }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Создание клиента
        /// </summary>
        /// <param name="name">Имя клиента</param>
        public Customer(string name)
        {
            this.name = name;
        }

        #endregion
    }
}
