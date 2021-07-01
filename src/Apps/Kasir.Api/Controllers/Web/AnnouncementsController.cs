
using Kasir.Api.Dto;
using Kasir.Api.Services;
using Kasir.Api.ViewModel;
using Kasir.Application.Common.Interfaces;
using Kasir.Application.Announcements.Commands.Create;
using Kasir.Application.Dto;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Kasir.Api.Controllers.Web
{
    public class AnnouncementsController : BaseWebController
    {
        private readonly IApplicationDbContext applicationDbContext;

        public AnnouncementsController(IApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var models = await applicationDbContext.Announcements
                .Select(c => new AnnouncementViewModel
                {
                    CreatedAt = c.CreateDate,
                    Body = c.Body,
                    Title = c.Title
                }).ToListAsync();

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAnnouncementCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(command);
            }
        }


    }
}
