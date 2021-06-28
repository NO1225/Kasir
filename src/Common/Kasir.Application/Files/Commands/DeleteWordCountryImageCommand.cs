using Kasir.Application.Common.Interfaces;
using Kasir.Application.Common.Models;
using Kasir.Application.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Kasir.Application.Files.Commands
{
    public class DeleteWordCountryImageCommand : IRequestWrapper
    {
        public string OldImageName { get; set; }
    }


    public class DeleteWordCountryImageCommandHandler : IRequestHandlerWrapper<DeleteWordCountryImageCommand>
    {
        private readonly ILogger<DeleteWordCountryImageCommand> logger;

        public DeleteWordCountryImageCommandHandler(ILogger<DeleteWordCountryImageCommand> logger)
        {
            this.logger = logger;
        }
        public async Task<ServiceResult> Handle(DeleteWordCountryImageCommand request, CancellationToken cancellationToken)
        {

            string _imagesFolderPath = UploadDownloadHelper.WORD_IMAGE_FOLDER_PATH;

            var currentDirectory = Directory.GetCurrentDirectory();

            var path = Path.Combine(currentDirectory, UploadDownloadHelper.ROOT_PATH, _imagesFolderPath);

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

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

            return new ServiceResult();
        }
    }
}
