namespace ShoppingMongo.Settings
{
    public interface IDatabaseSettings
    {
        /// <summary>
        /// Veritabanı ayarlarını temsil eder.
        /// Uygulamada bağımlılık enjeksiyonu ile yapılandırma sınıfına uygulanır.
        /// </summary>

        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CategoryCollectionName { get; set; }
        public string ProductCollectionName { get; set; }
    }
}
