using Kasir.Api.ViewModel;
using Kasir.Application.AppInfos.Commands.Update;
using Kasir.Application.Common.Interfaces;
using Kasir.Application.Dto;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Kasir.Api.Controllers.Web
{
    public class AppInfosController : BaseWebController
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;

        public AppInfosController(IApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            await AddLanguagesAsync();

            var appInfo = await applicationDbContext.AppInfos
                .Include(c => c.AppInfoLanguages)
                .ThenInclude(c => c.Language)
                .FirstOrDefaultAsync();

            if (appInfo == null)
            {
                return NotFound();
            }

            return View(new AppInfoViewModel
            {

                AppInfoLanguageViewModels = appInfo.AppInfoLanguages
                .Select(cl => new AppInfoLanguageViewModel
                {
                    Description = cl.Description,
                    Title = cl.Title,
                    Language = cl.Language.Name,
                    LanguageId = cl.LanguageId,
                }).ToList()
            });
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            await AddLanguagesAsync();

            var appInfo = await applicationDbContext.AppInfos
                .Include(c => c.AppInfoLanguages)
                .ThenInclude(c => c.Language)
                .FirstOrDefaultAsync();

            if (appInfo == null)
            {
                return NotFound();
            }

            return View(new AppInfoViewModel
            {

                AppInfoLanguageViewModels = appInfo.AppInfoLanguages
                .Select(cl => new AppInfoLanguageViewModel
                {
                    Description = cl.Description,
                    Title = cl.Title,
                    Language = cl.Language.Name,
                    LanguageId = cl.LanguageId,
                }).ToList()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] UpdateAppInfoCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                await AddLanguagesAsync();

                return View(command);
            }
        }

        private async Task AddLanguagesAsync()
        {

            ViewBag.Languages = await applicationDbContext
                .Languages
                .ProjectToType<LanguageDto>()
                .ToListAsync();
        }
    }
}


