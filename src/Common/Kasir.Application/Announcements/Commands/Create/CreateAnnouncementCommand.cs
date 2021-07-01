using Kasir.Application.Common.Interfaces;
using Kasir.Application.Common.Models;
using Kasir.Domain.Entities;
using Kasir.Domain.Event;
using System.Threading;
using System.Threading.Tasks;

namespace Kasir.Application.Announcements.Commands.Create
{
    public class CreateAnnouncementCommand : IRequestWrapper<Announcement>
    {
        public string Title { get; set; }


        public string Body { get; set; }

    }

    public class CreateAnnouncementCommandHandler : IRequestHandlerWrapper<CreateAnnouncementCommand, Announcement>
    {
        private readonly IApplicationDbContext _context;

        public CreateAnnouncementCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<Announcement>> Handle(CreateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var entity = new Announcement
            {
                Title = request.Title,
                Body = request.Body,

            };

            await _context.Announcements.AddAsync(entity, cancellationToken);

            entity.DomainEvents.Add(new AnnouncementCreatedEvent(entity));

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(entity);
        }
    }
}
