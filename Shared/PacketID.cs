using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public enum PacketID
    {
        Null = 0,
        C_ResourceInfoReq = 1,
        S_ResourceInfoRes = 2,
        C_ShopInfoReq = 3,
        S_ShopInfoRes = 4,
        C_BuyShopItemReq = 5,
        S_BuyShopItemRes = 6,
    }
}
