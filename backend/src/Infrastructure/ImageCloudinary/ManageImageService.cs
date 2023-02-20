using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using System.Net;
using Tickets.Application.Contracts.Infrastructure;
using Tickets.Application.Messages;
using Tickets.Application.Models.ImageManagement;

namespace Tickets.Infrastructure.ImageCloudinary
{
    public class ManageImageService : IManageImageService
    {
        public CloudinarySettings _cloudinarySettings { get; }

        public ManageImageService(IOptions<CloudinarySettings> cloudinarySettings)
        {
            _cloudinarySettings = cloudinarySettings.Value;
        }

        public async Task<ImageResponse> UploadImage(ImageData imageStream)
        {
            var account = new Account(
              _cloudinarySettings.CloudName,
              _cloudinarySettings.ApiKey,
              _cloudinarySettings.ApiSecret
            );

            var cloudinary = new Cloudinary(account);

            var uploadImage = new ImageUploadParams()
            {
                File = new FileDescription(imageStream.Name, imageStream.ImageStream)
            };

            var uploadResult = await cloudinary.UploadAsync(uploadImage);

            if (uploadResult.StatusCode == HttpStatusCode.OK)
            {
                return new ImageResponse
                {
                    PublicId = uploadResult.PublicId,
                    Url = uploadResult.Url.ToString()
                };
            }

            throw new Exception(MessageLabel.ImageSaveError);
        }
    }
}
