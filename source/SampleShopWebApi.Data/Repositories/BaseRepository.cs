using Microsoft.EntityFrameworkCore;

namespace SampleShopWebApi.Data.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly ShopDbContext ShopDbContext;

        protected BaseRepository(ShopDbContext shopDbContext)
        {
            this.ShopDbContext = shopDbContext;
        }

        public void SaveChanges()
        {
            try
            {
                this.ShopDbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // TODO: log and rollback
                throw ex;
            }
        }
    }
}
