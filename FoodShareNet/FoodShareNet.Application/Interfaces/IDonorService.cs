using FoodShareNet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShareNet.Application.Interfaces
{
    public interface IDonorService
    {
        Task<List<Donor>> GetAllDonorsAsync();
        Task<Donor> GetDonorAsync(int id);
        Task<Donor> CreateDonorAsync(Donor donor);
        Task<bool> EditDonorAsync(int id, Donor donor);
        Task<bool> DeleteDonorAsync(int id);
    }
}
