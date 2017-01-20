using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reviewIt.Core.ViewModels
{
   public class EventViewModel
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string EventDetails { get; set; }
        public string Location { get; set; }
        public string Schedule { get; set; }

    }
}
