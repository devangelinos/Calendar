using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    [Serializable]
    public struct Day
    {
        public string Caption { get; set; }
        public List<Event> Events { get; set; }
    }
}
