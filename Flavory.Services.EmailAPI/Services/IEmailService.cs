using Flavory.Services.EmailAPI.Message;
using Flavory.Services.EmailAPI.Models.Dto;

namespace Flavory.Services.EmailAPI.Services
{
    public interface IEmailService
    {
        Task EmailCartAndLog(CartDto cartDto);
        Task RegisterUserEmailAndLog(string email);
        Task LogOrderPlaced(RewardsMessage rewardsDto);
    }
}
