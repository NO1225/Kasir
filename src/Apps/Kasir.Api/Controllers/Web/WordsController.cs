using Kasir.Api.Services;
using Kasir.Api.ViewModel;
using Kasir.Application.Common.Interfaces;
using Kasir.Application.Dto;
using Kasir.Application.Words.Commands.Create;
using Kasir.Application.Words.Commands.Delete;
using Kasir.Application.Words.Commands.Update;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Kasir.Api.Controllers.Web
{
    public class WordsController : BaseWebController
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;

        public WordsController(IApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            await AddLanguagesAsync();
            await AddCountriesAsync();

            var models = await applicationDbContext.Words
                .Include(c => c.WordLanguages)
                .ThenInclude(c => c.Language)
                .Include(c => c.WordCountries)
                .ThenInclude(c => c.Country)
                .Select(c => new WordViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    ImagePath = c.ImageName,
                    WordLanguageViewModels = c.WordLanguages.Select(cl => new WordLanguageViewModel
                    {
                        WordTitle = cl.Title,
                        WordInformation = cl.Information,
                        Language = cl.Language.Name,
                        LanguageId = cl.LanguageId,
                    }).ToList(),
                    WordCountryViewModels = c.WordCountries.Select(cl => new WordCountryViewModel
                    {
                        Country = cl.Country.Name,
                        CountryId = cl.CountryId,
                        Checked = true,
                    }).ToList()
                }).ToListAsync();

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await AddLanguagesAsync();
            await AddCountriesAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWordViewModel viewModel)
        {

            var command = new CreateWordCommand
            {
                Name = viewModel.Name,
                WordCountriesDtos = viewModel.WordCountryDtos,
                WordLanguageDtos = viewModel.WordLanguageDtos,
            };
            if (viewModel.WordImage != null)
                command.AddWordImage(new FormFileProxy(viewModel.WordImage));
            var result = await Mediator.Send(command);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Create");

            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            await AddLanguagesAsync();
            await AddCountriesAsync();

            var Word = await applicationDbContext.Words
                .Include(c => c.WordLanguages)
                .ThenInclude(c => c.Language)
                .Include(c => c.WordCountries)
                .ThenInclude(c => c.Country)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (Word == null)
            {
                return NotFound();
            }

            return View(new WordViewModel
            {
                Id = Word.Id,
                Name = Word.Name,
                ImagePath = Word.ImageName,
                WordLanguageViewModels = Word.WordLanguages.Select(cl => new WordLanguageViewModel
                {
                    WordTitle = cl.Title,
                    WordInformation = cl.Information,
                    Language = cl.Language.Name,
                    LanguageId = cl.LanguageId,
                }).ToList(),
                WordCountryViewModels = Word.WordCountries.Select(cl => new WordCountryViewModel
                {
                    Country = cl.Country.Name,
                    CountryId = cl.CountryId,
                    Checked = true
                }).ToList()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditWordViewModel viewModel)
        {
            var command = new UpdateWordCommand
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                WordCountryDtos = viewModel.WordCountryDtos,
                WordLanguageDtos = viewModel.WordLanguageDtos,
            };
            if (viewModel.WordImage != null)
                command.AddWordImage(new FormFileProxy(viewModel.WordImage));
            var result = await Mediator.Send(command);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                await AddLanguagesAsync();
                await AddCountriesAsync();

                return View(command);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteWordCommand { Id = id });

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            await AddLanguagesAsync();
            await AddCountriesAsync();

            var Word = await applicationDbContext.Words
                .Include(c => c.WordLanguages)
                .ThenInclude(c => c.Language)
                .Include(c => c.WordCountries)
                .ThenInclude(c => c.Country)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (Word == null)
            {
                return NotFound();
            }

            return View(new WordViewModel
            {
                Id = Word.Id,
                Name = Word.Name,
                ImagePath = Word.ImageName,
                WordLanguageViewModels = Word.WordLanguages.Select(cl => new WordLanguageViewModel
                {
                    WordTitle = cl.Title,
                    WordInformation = cl.Information,
                    Language = cl.Language.Name,
                    LanguageId = cl.LanguageId,
                }).ToList(),
                WordCountryViewModels = Word.WordCountries.Select(cl => new WordCountryViewModel
                {
                    Id = cl.Id,
                    Country = cl.Country.Name,
                    CountryId = cl.CountryId,
                    Checked = true,
                }).ToList()
            });
        }
        private async Task AddLanguagesAsync()
        {
            ViewBag.Languages = await applicationDbContext
                .Languages
                .ProjectToType<LanguageDto>()
                .ToListAsync();
        }


        private async Task AddCountriesAsync()
        {
            ViewBag.Countries = await applicationDbContext
                .Countries
                .ProjectToType<CountryDto>()
                .ToListAsync();
        }
    }
}
