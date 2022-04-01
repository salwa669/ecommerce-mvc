using Online_Shoping.Models;
using System.Collections.Generic;
using System.Linq;

namespace Online_Shoping.Reporistry
{
    public class CatogeryReporsitry : ICatogory
    {
        Context context;
        public CatogeryReporsitry(Context c)
        {
            context = c;
        }
        public int delete(int id)
        {
            Category category = GetById(id);
            context.categories.Remove(category);
            return context.SaveChanges();
        }

        public List<Category> GetAll()
        {
            return context.categories.ToList();
        }

        public Category GetById(int id)
        {
            return context.categories.FirstOrDefault(c => c.id == id);
        }

        public int insert(Category x)
        {
            context.categories.Add(x);
            return context.SaveChanges();
        }

        public int update(int id, Category x)
        {
            Category category = GetById(id);
            if (category != null)
            {
                category.name = x.name;
                return context.SaveChanges();
            }
            return 0;
        }
        public List<Product> GetProductsByCatId(int catid)
        {
            List<Product> products =
            context.products.Where(c => c.category_id == catid).ToList();
            return products;
        }
    }
}
