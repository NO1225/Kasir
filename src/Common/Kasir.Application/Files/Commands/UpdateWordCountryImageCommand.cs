using Kasir.Application.Common.Interfaces;
using Kasir.Application.Common.Models;
using Kasir.Application.Helpers;
using Kasir.Domain.Enums;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Kasir.Application.Files.Commands
{
    public class UpdateWordCountryImageCommand : IRequestWrapper<string>
    {
        public string OldImageName { get; set; }

        public IFileStream WordImage { get; set; }
    }

    public class UpdateWordCountryImageCommandHandler : IRequestHandlerWrapper<UpdateWordCountryImageCommand, string>
    {
        private readonly ILogger<UpdateWordCountryImageCommand> logger;

        public UpdateWordCountryImageCommandHandler(ILogger<UpdateWordCountryImageCommand> logger)
        {
            this.logger = logger;
        }

        public async Task<ServiceResult<string>> Handle(UpdateWordCountryImageCommand request, CancellationToken cancellationToken)
        {
            int _maxFileSize = 2 * 1024 * 1024;
            int _minWidth = 450;
            int _minHeight = 450;
            int _resizeTo = 450;
            string _imagesFolderPath = UploadDownloadHelper.WORD_IMAGE_FOLDER_PATH;

            if (request.WordImage.ContentType.Contains("image") == false)
            {
                return new ServiceResult<string>(new ServiceError("image type is invalid", (int)ErrorCode.InvalidFileType));
            }

            if (request.WordImage.Length > _maxFileSize)
            {
                return new ServiceResult<string>(new ServiceError("image size is too large", (int)ErrorCode.FileIsTooLarge));
            }

            var ext = Path.GetExtension(request.WordImage.FileName);

            var name = Guid.NewGuid().ToString();

            var currentDirectory = Directory.GetCurrentDirectory();

            var path = Path.Combine(currentDirectory, UploadDownloadHelper.ROOT_PATH, _imagesFolderPath);

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            using var image = Image.Load(request.WordImage.OpenReadStream());
            if (image.Width < _minWidth || image.Height < _minHeight)
            {
                return new ServiceResult<string>(new ServiceError("image size is too small", (int)ErrorCode.ImageTooSmall));
            }

            if (File.Exists(path + request.OldImageName))
            {
                try
                {
                    File.Delete(path + request.OldImageName);
                }
                catch (Exception ex)
                {
                    logger.LogWarning("File deletion failed: " + ex.Message);
                }
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
