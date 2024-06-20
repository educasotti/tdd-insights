using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RunGroopWebApp.Data;
using RunGroopWebApp.Models;
using RunGroopWebApp.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunGroopWebApp.Tests.Repository
{
    public class ClubRepositoryTests
    {
        private async Task<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Clubs.CountAsync() < 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Clubs.Add(
                                        new Models.Club()
                                        {
                                            Title = "Club 1",
                                            Image = "",
                                            Description = "This is the description",
                                            ClubCategory = Data.Enum.ClubCategory.City,
                                            Address = new Models.Address
                                            {
                                                Street = "123 Main St",
                                                City = "Charlotte",
                                                State = "NC"
                                            }
                                        });
                    await databaseContext.SaveChangesAsync();
                }

            }
            return databaseContext;
        }

        [Fact]
        public async void ClubRepository_Add_ReturnsBool()
        {
            //arrange
            var club = new Club
            {
                Title = "Club 1",
                Image = "",
                Description = "This is the description",
                ClubCategory = Data.Enum.ClubCategory.City,
                Address = new Models.Address
                {
                    Street = "123 Main St",
                    City = "Charlotte",
                    State = "NC"
                }
            };
            var dbContext = await GetDbContext();
            dbContext.Clubs.AsNoTracking();
            var clubRepository = new ClubRepository(dbContext);

            //act
            var result = clubRepository.Add(club);

            //assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void ClubRepository_GetByIdAsync_ReturnsClub()
        {
            //arrange
            var id = 1;
            var dbContext = await GetDbContext();
            var clubRepository = new ClubRepository(dbContext);

            //act
            var result = clubRepository.GetByIdAsync(id);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<Club>>();
        }

    }
}
