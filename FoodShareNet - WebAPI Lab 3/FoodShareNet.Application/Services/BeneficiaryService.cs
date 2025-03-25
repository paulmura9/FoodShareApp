using FoodShareNet.Application.Exceptions;
using FoodShareNet.Application.Interfaces;
using FoodShareNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;

namespace FoodShareNet.Application.Services
{
    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly IFoodShareDbContext _context;
        public BeneficiaryService(IFoodShareDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<List<Beneficiary>> GetAllBeneficiariesAsync()
        {
            var beneficiaries = await _context.Beneficiaries
           .Include(b => b.City)
           .ToListAsync();

            if (beneficiaries == null)
            {
                throw new BeneficiaryException("Beneficiaries do not exist");
            }

            return beneficiaries;
        }
        public async Task<Beneficiary> GetBeneficiaryAsync(int id)
        {
            var beneficiary = await _context.Beneficiaries.Include(b => b.City).Where(b => b.Id == id).FirstOrDefaultAsync();

            if (beneficiary == null)
            {
                throw new BeneficiaryException("Beneficiary does not exist");
            }

            return beneficiary;
        }
        public async Task<Beneficiary> CreateBeneficiaryAsync(Beneficiary beneficiary)
        {
    
            _context.Beneficiaries.Add(beneficiary);
            await _context.SaveChangesAsync();

            return beneficiary;
        }
        public async Task<bool> EditBeneficiaryAsync(int id, Beneficiary editBeneficiary)
        {
           

            var beneficiary = await _context.Beneficiaries.FirstOrDefaultAsync(b => b.Id == id);
            if (beneficiary == null)
            {
                throw new NotFoundException("Beneficiary", id);
            }

            beneficiary.Name = editBeneficiary.Name;
            beneficiary.Address = editBeneficiary.Address;
            beneficiary.CityId = editBeneficiary.CityId;
            beneficiary.Capacity = editBeneficiary.Capacity;

            await _context.SaveChangesAsync();

            return true;

        } 
        public async Task<bool> DeleteBeneficiaryAsync(int id)
        {
            var beneficiary = await _context.Beneficiaries.FindAsync(id);

            if (beneficiary == null)
            {
                throw new NotFoundException("Beneficiary", id);
            }

            _context.Beneficiaries.Remove(beneficiary);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
