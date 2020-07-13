using Microsoft.EntityFrameworkCore;

namespace SampleShopWebApi.Data.Repositories
{
    /// <summary>
    /// Represents a base repository class. Performs commits (todo: and rollbacks if commit was unsuccessful).
    /// </summary>
    public abstract class BaseRepository
    {
        protected readonly ShopDbContext ShopDbContext;

        /// <summary>
        /// .Ctor
        /// </summary>
        /// <param name="shopDbContext">Database context.</param>
        protected BaseRepository(ShopDbContext shopDbContext)
        {
            this.ShopDbContext = shopDbContext;
        }

        /// <summary>
        /// Saves all changes made in the <see cref="ShopDbContext"/> context.
        /// </summary>
        public void SaveChanges()
        {
            try
            {
                this.ShopDbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // TODO: log and rollback
                throw;
            }
        }
    }
}
