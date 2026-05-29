using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> AddWalkAsync(Walk walk);
        Task<List<Walk>> GetAllWalksAsync(string? filterOn, string? filterQuery, string? sortBy, bool? isAscending,
            int? pageNumber, int? pageSize);
        Task<Walk> GetWalkByIdAsync(Guid id);
        Task<Walk> UpdateWalkAsync(Guid id, Walk walk); 
        Task<Walk> DeleteWalkAsync(Guid id);
        
    }
}
