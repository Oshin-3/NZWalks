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

        public async Task<List<Walk>> GetAllWalksAsync(string? filterOn = null, string? filterQuery = null, 
            string? sortBy = null, bool? isAscending = true, int? pageNumber = 1, int? pageSize = 1000)
        {
            //return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            //filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }
            //sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending == true ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if(sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending == true ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }
            //pagination
            if(pageNumber != null && pageSize != null)
            {
                var skipResults = (pageNumber.Value - 1) * pageSize.Value;
                return await walks.Skip(skipResults).Take(pageSize.Value).ToListAsync();
            }

            return await walks.ToListAsync();
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
