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
    public class DonorService:IDonorService
    {
        private readonly IFoodShareDbContext _context;
        public DonorService(IFoodShareDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Donor>> GetAllDonorsAsync()
        {
            var donors = await _context.Donors
           .Include(b => b.City)
           .Include(b => b.Donations)
           .ThenInclude(d => d.Product) 

           .Include(b => b.Donations)
           .ThenInclude(d => d.Status)

           .ToListAsync();

            if (donors == null)
            {
                throw new DonorException("Donors do not exist");
            }

            return donors;
        }
        public async Task<Donor> GetDonorAsync(int id)
        {
            var donor = await _context.Donors
            .Include(b => b.City)
            .Include(b => b.Donations)
            .ThenInclude(d => d.Product)

            .Include(b => b.Donations)
            .ThenInclude(d => d.Status)
            .Where(b => b.Id == id)
            
            .FirstOrDefaultAsync();

            if (donor == null)
            {
                throw new DonorException("Donor does not exist");
            }

            return donor;
        }
        public async Task<Donor> CreateDonorAsync(Donor donor)
        {

            _context.Donors.Add(donor);
            await _context.SaveChangesAsync();

            return donor;
        }
        public async Task<bool> EditDonorAsync(int id, Donor editDonor)
        {
          

            var donor = await _context.Donors.FirstOrDefaultAsync(b => b.Id == id);
            if (donor == null)
            {
                throw new NotFoundException("Donor", id);
            }

            donor.Name = editDonor.Name;
            donor.Address = editDonor.Address;
            donor.CityId = editDonor.CityId;
           
            await _context.SaveChangesAsync();

            return true;

        }
        public async Task<bool> DeleteDonorAsync(int id)
        {
            var donor = await _context.Donors.FindAsync(id);

            if (donor == null)
            {
                throw new NotFoundException("Donor", id);
            }

            _context.Donors.Remove(donor);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
