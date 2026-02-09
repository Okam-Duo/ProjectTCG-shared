using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contents
{
    public enum IngameActionType
    {
        StartCardEffect,
        Damage,
        Heal,
        SetHealth,
        Die,
        DrawCard,
        DiscardCard,
        Destroy,
        Remove,
        AddBuff
    }
}
