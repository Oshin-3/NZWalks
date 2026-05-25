using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllRegionsAsync();
        Task<Region> GetRegionByIdAsync(Guid id);
        Task<Region> AddRegionAsync(Region region);
        Task<Region> UpdateRegionAsync(Guid id, Region region);
        Task<Region> DeleteRegionAsync(Guid id);
    }
}
