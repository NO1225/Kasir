using Kasir.Application.Common.Interfaces;
using Kasir.Application.Common.Models;
using Kasir.Application.Helpers;
using Kasir.Domain.Enums;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Kasir.Application.Files.Commands
{
    public class AddCountryImageCommand : IRequestWrapper<string>
    {
        public IFileStream CountryImage { get; set; }
    }


    public class AddCountryImageCommandHandler : IRequestHandlerWrapper<AddCountryImageCommand, string>
    {


        public async Task<ServiceResult<string>> Handle(AddCountryImageCommand request, CancellationToken cancellationToken)
        {
            int _maxFileSize = 8 * 1024 * 1024;
            int _minWidth = 0;
            int _minHeight = 0;
            int _resizeTo = 450;
            string _imagesFolderPath = UploadDownloadHelper.COUNTRY_IMAGE_FOLDER_PATH;

            if (request.CountryImage.ContentType.Contains("image") == false)
            {
                return new ServiceResult<string>(new ServiceError("image type is invalid", (int)ErrorCode.InvalidFileType));
            }

            if (request.CountryImage.Length > _maxFileSize)
            {
                return new ServiceResult<string>(new ServiceError("image size is too large", (int)ErrorCode.FileIsTooLarge));
            }

            var ext = Path.GetExtension(request.CountryImage.FileName);

            var name = Guid.NewGuid().ToString();

            var currentDirectory = Directory.GetCurrentDirectory();

            var path = Path.Combine(currentDirectory, UploadDownloadHelper.ROOT_PATH, _imagesFolderPath);

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            using var image = Image.Load(request.CountryImage.OpenReadStream());
            if (image.Width < _minWidth || image.Height < _minHeight)
            {
                return new ServiceResult<string>(new ServiceError("image size is too small", (int)ErrorCode.ImageTooSmall));
            }

            double aspect = (double)image.Width / (double)image.Height;
            if (image.Width > image.Height)
                image.Mutate(x => x.Resize(_resizeTo, (int)(_resizeTo / aspect), true));
            else
                image.Mutate(x => x.Resize((int)(_resizeTo * aspect), _resizeTo, true));

            await image.SaveAsync(path + name + ext, cancellationToken: cancellationToken);

            return ServiceResult.Success(name + ext);
        }
    }


}
