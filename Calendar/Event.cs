using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    [Serializable]
    public class Event
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public Color Color { get; set; }
        public string Sound { get; set; }
        public DateTime DateTime { get; set; }
    }
}
