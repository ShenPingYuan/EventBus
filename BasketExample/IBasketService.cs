using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BasketExample
{
    public interface IBasketService
    {
        Task UpdateBasketProductPrice(decimal oldPrice, decimal newPrice);
    }
}
