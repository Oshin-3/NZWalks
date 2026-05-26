using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;
        public SQLWalkRepository(NZWalksDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<Walk> AddWalkAsync(Walk walk)
        {
            var walkDomainModel = new Walk
            {
                Id = Guid.NewGuid(),
                Name = walk.Name,
                Description = walk.Description,
                LengthInKm = walk.LengthInKm,
                WalkImageUrl = walk.WalkImageUrl,
                RegionId = walk.RegionId,
                DifficultyId = walk.DifficultyId
            };

            await dbContext.Walks.AddAsync(walkDomainModel);
            await dbContext.SaveChangesAsync();

            return walkDomainModel;
        }

        public async Task<List<Walk>> GetAllWalksAsync()
        {
     
            return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk> GetWalkByIdAsync(Guid id)
        {
            //check if the walk exists in database
            var existingWalkById = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalkById == null)
            {
                return null;
            }

            return existingWalkById;

        }

        public async Task<Walk> UpdateWalkAsync(Guid id, Walk walk)
        {
            //check if the walk exists in database
            var existingWalkById = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalkById == null)
            {
                return null;
            }

            existingWalkById.Name = walk.Name;
            existingWalkById.Description = walk.Description;
            existingWalkById.LengthInKm = walk.LengthInKm;
            existingWalkById.WalkImageUrl = walk.WalkImageUrl;
            existingWalkById.RegionId = walk.RegionId;
            existingWalkById.DifficultyId = walk.DifficultyId;

            await dbContext.SaveChangesAsync();
            return existingWalkById;

        }

        public async Task<Walk> DeleteWalkAsync(Guid id)
        {
            //check if the walk exists in database
            var existingWalkById = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalkById == null)
            {
                return null;
            }

            dbContext.Walks.Remove(existingWalkById);
            await dbContext.SaveChangesAsync();

            return existingWalkById;
        }
    }
}
