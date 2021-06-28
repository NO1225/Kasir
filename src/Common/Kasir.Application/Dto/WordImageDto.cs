using Kasir.Application.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Kasir.Application.Dto
{
    public class WordImageDto
    {
        public int CountryId { get; set; }

        public IFileStream WordImage;

        public void AddWordImage(IFileStream fileStream)
        {
            WordImage = fileStream;
        }

    }
}
