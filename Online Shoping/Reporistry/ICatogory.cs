﻿using Online_Shoping.Models;
using System.Collections.Generic;

namespace Online_Shoping.Reporistry
{
    public interface ICatogory:IReporistry<Category>
    {
        List<Category> GetAll();
        Category GetById(int id);
        List<Product> GetProductsByCatId(int id);
    }
}
