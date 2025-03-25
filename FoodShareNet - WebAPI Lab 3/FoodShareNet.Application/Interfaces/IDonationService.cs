using FoodShareNet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShareNet.Application.Interfaces
{
    public interface IDonationService
    {
        Task<Donation> CreateDonationAsync(Donation donation);
        Task<Donation> GetDonationAsync(int id);
        Task<List<Donation>> GetDonationsByCityIdAsync(int cityId);
    }
}
