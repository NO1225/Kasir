using Kasir.Api.Dto;
using Kasir.Api.Services;
using Kasir.Api.ViewModel;
using Kasir.Application.Common.Interfaces;
using Kasir.Application.Countries.Commands.Create;
using Kasir.Application.Countries.Commands.Delete;
using Kasir.Application.Countries.Commands.Update;
using Kasir.Application.Dto;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Kasir.Api.Controllers.Web
{
    public class CountriesController : BaseWebController
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;

        public CountriesController(IApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            await AddLanguagesAsync();

            var models = await applicationDbContext.Countries
                .Include(c => c.CountryLanguages)
                .ThenInclude(c => c.Language)
                .Select(c => new CountryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    ImagePath = c.ImagePath,
                    CountryLanguageViewModels = c.CountryLanguages.Select(cl => new CountryLanguageViewModel
                    {
                        CountryName = cl.Name,
                        Language = cl.Language.Name,
                        LanguageId = cl.LanguageId,
                    }).ToList()
                }).ToListAsync();

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await AddLanguagesAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCountryCommand command, [FromForm] UploadCommand fileCommand)
        {
            command.AddCountryImage(new FormFileProxy(fileCommand.File));
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


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            await AddLanguagesAsync();

            var country = await applicationDbContext.Countries
                .Include(c => c.CountryLanguages)
                .ThenInclude(c => c.Language)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (country == null)
            {
                return NotFound();
            }

            return View(new CountryViewModel
            {
                Id = country.Id,
                Name = country.Name,
                ImagePath = country.ImagePath,
                CountryLanguageViewModels = country.CountryLanguages.Select(cl => new CountryLanguageViewModel
                {
                    CountryName = cl.Name,
                    Language = cl.Language.Name,
                    LanguageId = cl.LanguageId,
                }).ToList()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] UpdateCountryCommand command, [FromForm] UploadCommand fileCommand)
        {
            if (fileCommand.File != null)
                command.AddCountryImage(new FormFileProxy(fileCommand.File));
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

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteCountryCommand { Id = id });

            return RedirectToAction("Index");

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
