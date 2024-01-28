﻿using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _nZWalksDb;

        public RegionRepository(NZWalksDbContext nZWalksDb)
        {
            _nZWalksDb = nZWalksDb;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            _nZWalksDb.Regions.Add(region);
            await _nZWalksDb.SaveChangesAsync();

            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await _nZWalksDb.Regions.FirstOrDefaultAsync(r => r.Id == id);
            
            if (region == null)
            {
                return null;
            }

            _nZWalksDb?.Regions.Remove(region);
            await _nZWalksDb?.SaveChangesAsync();

            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await _nZWalksDb.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await _nZWalksDb.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region> UpdateAsync(Region updateRegionObj)
        {
            var region = await _nZWalksDb.Regions.FirstOrDefaultAsync(r => r.Id == updateRegionObj.Id);
            
            if (region == null)
            {
                return null;
            }

            _nZWalksDb.Update(region);
            await _nZWalksDb.SaveChangesAsync();

            return region;
        }
    }
}
