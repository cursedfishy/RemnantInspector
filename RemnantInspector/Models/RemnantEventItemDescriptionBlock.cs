using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemnantInspector.Models
{
    public class RemnantEventItemDescriptionBlock
    {
        public string EventItemGroupIndex { get; set; }
        public string GroupIndexDescription { get; set; }
        public bool IsFree { get; set; }

        public RemnantEventItemDescriptionBlock()
        {
            EventItemGroupIndex = "1";
            GroupIndexDescription = string.Empty;
            IsFree = true;
        }
    }
}
