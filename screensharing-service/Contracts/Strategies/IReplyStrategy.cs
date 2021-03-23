using System.Collections.Generic;
using screensharing_service.Entities.ScreenMirroring;

namespace screensharing_service.Contracts.Strategies
{
    public interface IReplyStrategy
    {
        public void f( IList<ScreenMirroringEvent> events);
    }
}