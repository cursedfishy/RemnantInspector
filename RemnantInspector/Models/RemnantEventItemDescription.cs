using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemnantInspector.Models
{
    public class RemnantEventItemDescription
    {
        public string EventName { get; set; }
        public List<RemnantEventItemDescriptionBlock> ItemDescriptionBlocks { get; set; }

        public RemnantEventItemDescription()
        {
            EventName = string.Empty;
            ItemDescriptionBlocks = new List<RemnantEventItemDescriptionBlock>();
        }
    }
}
