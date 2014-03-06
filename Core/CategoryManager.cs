using System;
using System.Collections.Generic;

namespace Todooy.Core {
    /// Manager classes are an abstraction on the data access layers
    /// </summary>
    public static class CategoryManager {
        static CategoryManager ()
        {

        }
        
        public static Category GetCategory(int id)
        {
            return RepositoryADO.GetCategory(id);
        }
        
        public static IList<Category> GetCategories ()
        {
            return new List<Category>(RepositoryADO.GetCategories());
        }
        
        public static int SaveCategory (Category item)
        {
            return RepositoryADO.SaveCategory(item);
        }
        
        public static int DeleteCategory(int id)
        {
            return RepositoryADO.DeleteCategory(id);
        }
    }
}