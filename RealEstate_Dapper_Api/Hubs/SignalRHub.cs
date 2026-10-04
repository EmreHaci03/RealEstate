using Microsoft.AspNetCore.SignalR;
using RealEstate_Dapper_Api.Repository.Statistic;

namespace RealEstate_Dapper_Api.Hubs
{
    public class SignalRHub:Hub
    {
        private readonly IStatisticRepository _statisticRepository;

        public SignalRHub(IStatisticRepository statisticRepository)
        {
            _statisticRepository = statisticRepository;
        }

        public async Task SendStatistics()
        {
            var values = await _statisticRepository.GetStatisticsAsync();
            await Clients.All.SendAsync("ReceiveStatistics", values);
        }
    }
}
