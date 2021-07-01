using Kasir.Domain.Common;
using Kasir.Domain.Entities;

namespace Kasir.Domain.Event
{
    public class AnnouncementCreatedEvent : DomainEvent
    {
        public AnnouncementCreatedEvent(Announcement announcement)
        {
            Announcement = announcement;
        }

        public Announcement Announcement { get; }
    }
}
