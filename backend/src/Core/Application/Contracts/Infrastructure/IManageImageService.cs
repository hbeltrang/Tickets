using Tickets.Application.Models.ImageManagement;

namespace Tickets.Application.Contracts.Infrastructure
{
    public interface IManageImageService
    {
        Task<ImageResponse> UploadImage(ImageData imageStream);
    }
}
