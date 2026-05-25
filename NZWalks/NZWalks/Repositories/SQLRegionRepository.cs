using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Repositories
{
    public class SQLRegionRepository: IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;
        public SQLRegionRepository(NZWalksDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task<List<Region>> GetAllRegionsAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetRegionByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> AddRegionAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> UpdateRegionAsync(Guid id, Region region)
        {
            //get data from database and check if it exists
            var existingRegionById = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegionById == null) {
                return null;
            }

            //map dto to domain model
            existingRegionById.Code = region.Code;
            existingRegionById.Name = region.Name;
            existingRegionById.RegionImageUrl = region.RegionImageUrl;

            //save the data to database
            await dbContext.SaveChangesAsync();
            return existingRegionById;
        }

        public async Task<Region> DeleteRegionAsync(Guid id)
        {
            //get data from database and check if it exists
            var existingRegionById = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegionById == null)
            {
                return null;
            }
            dbContext.Regions.Remove(existingRegionById);
            await dbContext.SaveChangesAsync();
            return existingRegionById;
        }


    }
}
