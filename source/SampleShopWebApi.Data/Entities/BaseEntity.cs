namespace SampleShopWebApi.Data.Entities
{
    /// <summary>
    /// Represents a Base entity type with identity key.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Identity key.
        /// </summary>
        public int Id { get; set; }
    }
}