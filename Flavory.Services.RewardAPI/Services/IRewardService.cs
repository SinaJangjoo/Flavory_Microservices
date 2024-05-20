using Flavory.Services.RewardAPI.Message;

namespace Flavory.Services.RewardAPI.Services
{
    public interface IRewardService
    {
        Task UpdateRewards(RewardsMessage rewardsMessage);
    }
}
