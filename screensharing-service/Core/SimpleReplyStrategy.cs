using System.Collections.Generic;
using System.Linq;
using screensharing_service.Contracts.Strategies;
using screensharing_service.Entities.ScreenMirroring;

namespace screensharing_service.Core
{
    public class SimpleReplyStrategy:IReplyStrategy
    {
        public void f(IList<ScreenMirroringEvent> events)
        {
            var sortedEvents = events.OrderBy(p=>p.timestamp).ToList();
        }
    }
}