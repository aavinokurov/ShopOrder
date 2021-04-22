namespace ShopOrders
{
    public class Product
    {
        #region Поля

        /// <summary>
        /// Номер продукта
        /// </summary>
        private int numProduct;

        /// <summary>
        /// Имя продукта
        /// </summary>
        private string nameProduct;

        /// <summary>
        /// Цена продукта
        /// </summary>
        private double price;

        #endregion

        #region Свойства

        /// <summary>
        /// Номер продукта
        /// </summary>
        public int NumProduct { get { return numProduct; } }

        /// <summary>
        /// Имя продукта
        /// </summary>
        public string NameProduct { get { return nameProduct; } }

        /// <summary>
        /// Цена продукта
        /// </summary>
        public double Price { get { return price; } }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Создание продукта
        /// </summary>
        /// <param name="numProduct">Номер продукта</param>
        /// <param name="nameProduct">Имя продукта</param>
        /// <param name="price">Цена</param>
        public Product(int numProduct, string nameProduct, double price)
        {
            this.numProduct = numProduct;
            this.nameProduct = nameProduct;
            this.price = price;
        }

        #endregion
    }
}
