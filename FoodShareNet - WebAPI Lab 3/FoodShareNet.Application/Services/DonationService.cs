using FoodShareNet.Application.Exceptions;
using FoodShareNet.Application.Interfaces;
using FoodShareNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShareNet.Application.Services
{
    public class DonationService:IDonationService
    {
        private readonly IFoodShareDbContext _context;
        public DonationService(IFoodShareDbContext dbContext)
        {
            _context = dbContext;
        }

        
        public async Task<Donation> GetDonationAsync(int id)
        {
            var donation = await _context.Donations.Include(b=>b.Donor).Include(b => b.Product).Include(b => b.Status).Where(b => b.Id == id).FirstOrDefaultAsync();

            if (donation == null)
            {
                throw new DonationException("Donation does not exist");
            }

            return donation;
        }
        public async Task<List<Donation>> GetDonationsByCityIdAsync(int cityId)
        {
            var donations = await _context.Donations.Include(b => b.Donor).Include(b => b.Product).Include(b => b.Status).Where(b => b.Donor.CityId == cityId).ToListAsync();

            if (donations == null)
            {
                throw new DonationException("Donations do not exist");
            }

            return donations;
        }
        public async Task<Donation> CreateDonationAsync(Donation donation)
        {

            _context.Donations.Add(donation);
            await _context.SaveChangesAsync();

            return donation;
        }
    }
}
