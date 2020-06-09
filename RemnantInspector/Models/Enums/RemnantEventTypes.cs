using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemnantInspector.Models.Enums
{
    public enum RemnantEventTypes
    {
        UNKNOWN = 0x0000,
        POI = 0x0001,
        MINIBOSS = 0x0002,
        ITEMDROP = 0x0004,
        SIEGE = 0x0006,
        WORLDBOSS = 0x0008,
        DUNGEON = 0x0010,
        EVENT = 0x0012,
        EVENTRNG = 0x0014,
        EVENTQS = 0x0016
    }
}
