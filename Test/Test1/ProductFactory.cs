using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    class ProductFactory
    {
        public static IProductMgr getComponent()
        {
            return new ProductMgr();
        }
    }
}
