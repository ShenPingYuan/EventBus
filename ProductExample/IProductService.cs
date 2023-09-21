using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductExample
{
    public interface IProductService
    {
        Task UpdateProductPrice(int id, decimal price);
    }
}
