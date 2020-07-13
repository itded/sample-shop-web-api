namespace SampleShopWebApi.Data.Entities
{
    /// <summary>
    /// Represents a Product type.
    /// Configured using <see cref="Configurations.ProductConfiguration"/>.
    /// </summary>
    public class Product : BaseEntity
    {
        /// <summary>
        /// Product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Product image uri.
        /// </summary>
        public string ImgUri { get; set; }

        /// <summary>
        /// Product price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Product description.
        /// </summary>
        public string Description { get; set; }
    }
}