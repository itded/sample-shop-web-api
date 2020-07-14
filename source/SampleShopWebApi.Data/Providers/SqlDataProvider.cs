using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SampleShopWebApi.Data.Providers
{
    /// <summary>
    /// Represents Sql data provider.
    /// </summary>
    public class SqlDataProvider
    {
        public void CreateTablesWithSampleData(DbContext dbContext)
        {
            string script = ReadResource("Scripts.Seed.sql");
            dbContext.Database.ExecuteSqlRaw(script);
        }

        private string ReadResource(string name)
        {
            var assembly = GetType().Assembly;
            string resourceName = assembly.GetManifestResourceNames().First(x => x.EndsWith(name, StringComparison.InvariantCultureIgnoreCase));
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
