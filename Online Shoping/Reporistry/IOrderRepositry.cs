
using Online_Shoping.Models;

namespace Online_Shoping.Reporistry
{
    public interface IOrderRepositry : IReporistry<Order>
    {
        Order GetById(int id);
    }
}
