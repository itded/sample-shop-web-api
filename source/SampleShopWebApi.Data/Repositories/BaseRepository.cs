using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SampleShopWebApi.Data.Repositories
{
    /// <summary>
    /// Represents a base repository class. Performs commits (todo: and rollbacks if commit was unsuccessful).
    /// </summary>
    public abstract class BaseRepository
    {
        protected readonly ShopDbContext ShopDbContext;
        private readonly ILogger<BaseRepository> logger;

        /// <summary>
        /// .Ctor
        /// </summary>
        /// <param name="shopDbContext">Database context.</param>
        /// <param name="logger">Logger.</param>
        protected BaseRepository(ShopDbContext shopDbContext, ILogger<BaseRepository> logger)
        {
            this.ShopDbContext = shopDbContext;
            this.logger = logger;
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
                this.logger.LogError(ex.Message);

                // TODO: rollback
                throw;
            }
        }
    }
}
