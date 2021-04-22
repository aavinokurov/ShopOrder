using System;

namespace ShopOrders
{
    public class Order
    {
        #region Поля

        /// <summary>
        /// Номер заказа
        /// </summary>
        private int numOrder;

        /// <summary>
        /// Имя клиента
        /// </summary>
        private Customer сustomer;

        /// <summary>
        /// Дата заказа
        /// </summary>
        private DateTime dateOrder;

        /// <summary>
        /// Продукты
        /// </summary>
        private Product[] product;

        /// <summary>
        /// Кол-во продуктов в заказе
        /// </summary>
        private int[] quantityProducts;

        #endregion

        #region Свойства

        /// <summary>
        /// Номер заказа
        /// </summary>
        public int NumOrder { get { return numOrder; } }

        /// <summary>
        /// Имя клиента
        /// </summary>
        public string NameCustomer { get { return сustomer.Name; } }

        /// <summary>
        /// Дата заказа
        /// </summary>
        public DateTime DateOrder { get { return dateOrder; } }

        /// <summary>
        /// Продукты
        /// </summary>
        public Product[] Product { get { return product; } }

        /// <summary>
        /// Кол-во продуктов в заказе
        /// </summary>
        public int[] QuantityProducts { get { return quantityProducts; } }


        #endregion

        #region Конструкторы

        /// <summary>
        /// Создание заказа
        /// </summary>
        /// <param name="numOrder">Номер заказа</param>
        /// <param name="сustomer">Клиент</param>
        /// <param name="dateOrder">Дата заказа</param>
        public Order(int numOrder, Customer сustomer, DateTime dateOrder, Product[] product, int[] quantityProducts)
        {
            this.numOrder = numOrder;
            this.сustomer = сustomer;
            this.dateOrder = dateOrder;
            this.product = product;
            this.quantityProducts = quantityProducts;
        }

        #endregion
    }
}
