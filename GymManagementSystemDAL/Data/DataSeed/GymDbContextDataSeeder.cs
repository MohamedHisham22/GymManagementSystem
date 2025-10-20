using GymManagementSystemDAL.Data.DbContexts;
using GymManagementSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Data.DataSeed
{
    public static class GymDbContextDataSeeder
    {
        public static bool SeedData(GymDbContext dbContext) 
        {
            try 
            {
                var hasPlans = dbContext.Plans.Any();
                var hasCategories = dbContext.Categories.Any();

                if (hasPlans && hasCategories) return false;

                #region Plans Seeding
                if (!hasPlans) 
                {
                    var plans = JsonToList<Plan>("plans.json");
                    if (plans.Any()) 
                    {
                        dbContext.AddRange(plans);
                    }
                }
                #endregion

                #region Categories Seeding
                if (!hasCategories) 
                {
                    var categories = JsonToList<Category>("categories.json");
                    if (categories.Any()) 
                    {
                        dbContext.AddRange(categories);
                    }
                }
                #endregion

                return dbContext.SaveChanges() > 0;

            }
            catch 
            {
                return false;
            }
            
        }

        private static List<T> JsonToList<T>(string fileName) 
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" , "Files" , fileName);
            if (!File.Exists(filePath)) throw new FileNotFoundException();
            var jsonText = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };
            return JsonSerializer.Deserialize<List<T>>(jsonText, options) ?? new List<T>();
            
        }
    }
}
