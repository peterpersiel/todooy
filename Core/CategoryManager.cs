using System.Collections.Generic;

namespace Todooy.Core {

    public static class CategoryManager {
        static CategoryManager () {}

        public static Category GetCategory(int id) {
            return RepositoryADO.GetCategory(id);
        }

        public static List<Category> GetCategories () {
            return new List<Category>(RepositoryADO.GetCategories());
        }

        public static int SaveCategory (Category item) {
            return RepositoryADO.SaveCategory(item);
        }

        public static int DeleteCategory(int id) {
            return RepositoryADO.DeleteCategory(id);
        }
    }
}