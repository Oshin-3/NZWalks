using Microsoft.EntityFrameworkCore;
using NZWalks.Models.Domain;

namespace NZWalks.Data
{
    public class NZWalksDbContext: DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options) : base(options)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Seed data for Difficulties
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("4cee7041-0184-4cf6-89c3-cb9807d81dff"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("01b66037-49ed-4425-91ed-8ba53a1c9960"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("810aeabe-33fe-4cc6-8a90-6fe81240b8a2"),
                    Name = "Hard"
                }
            };
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //seed data for Regions
            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("32940b4e-3e60-448b-8d6f-177ce640542d"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3e/Auckland_skyline_from_Mt_Eden.jpg/2560px-Auckland_skyline_from_Mt_Eden.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("fd00093f-3493-445f-bd87-fb9b2ff1297b"),
                    Name = "Wellington",
                    Code = "WLG",
                    RegionImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5e/Wellington_City.jpg/2560px-Wellington_City.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("8d7790f4-321a-4676-9445-d5145ac31f90"),
                    Name = "Canterbury",
                    Code = "CAN"
                },
                new Region()
                {
                    Id = Guid.Parse("63273344-59d7-46b4-a85b-f9fbfbd0c78e"),
                    Name = "Otago",
                    Code = "OTA"
                },
                new Region()
                {
                    Id = Guid.Parse("dd7ace6f-18ec-4b07-9248-1847786c04a2"),
                    Name = "Bay of Plenty",
                    Code = "BOP"
                },
                new Region()
                {
                    Id = Guid.Parse("4c4b19d3-91ea-4994-94f4-abbda20a95d7"),
                    Name = "Waikato",
                    Code = "WAI",
                    RegionImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5e/Waikato_River.jpg/2560px-Waikato_River.jpg"
                },
                 new Region()
                 {
                    Id = Guid.Parse("9939da5a-f9d2-4400-a7cb-0f222c6f46ac"),
                    Name = "Taranaki",
                    Code = "TAR",
                    RegionImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4e/Mt_Taranaki.jpg/2560px-Mt_Taranaki.jpg"
                 }
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
