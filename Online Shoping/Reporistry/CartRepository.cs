using Online_Shoping.Models;
using System.Collections.Generic;
using System.Linq;

namespace Online_Shoping.Reporistry
{
    public class CartRepository: ICartRepository
    {
        Context context;
        public CartRepository(Context context)
        {
            this.context = context;
        }
        public List<Cart> GetAll()
        {
            return context.carts.ToList();
        }

        public int insert(Cart cart)
        {
            context.carts.Add(cart);
            return context.SaveChanges();
        }

        public Cart GetById(int id)
        {
            return context.carts.FirstOrDefault(x => x.Id == id);
        }

        public int update(int id, Cart cart)
        {
            Cart oldcart = GetById(id);
            if (oldcart != null)
            {
                oldcart.product_id = cart.product_id;
                oldcart.user_id = cart.user_id;
                oldcart.status = cart.status;
                return context.SaveChanges();
            }
            return 0;
        }

        public int delete(int id)
        {
            Cart oldcart = GetById(id);
            context.carts.Remove(oldcart);
            return context.SaveChanges();
        }
    }
}
