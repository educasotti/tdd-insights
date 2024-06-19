using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Controllers;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunGroopWebApp.Tests.Controller
{
    public class ClubControllerTests
    {
        private ClubController _clubController;
        private IClubRepository _clubRepository;
        private IPhotoService _photoService;
        private IHttpContextAccessor _httpContextAccessor;

        public ClubControllerTests()
        {
            //dependencies
            _clubRepository = A.Fake<IClubRepository>();
            _photoService = A.Fake<IPhotoService>();
            _httpContextAccessor = A.Fake<HttpContextAccessor>();

            //sut
            _clubController = new ClubController(_clubRepository, _photoService);
        }

        [Fact]
        public void ClubController_Index_ReturnsSuccess()
        {
            //arrange - what do i need to bring in?
            var clubs = A.Fake<IEnumerable<Club>>();
            A.CallTo(() => _clubRepository.GetAll()).Returns(clubs);

            //act
            var result = _clubController.Index();

            //assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void ClubController_Detail_ReturnsSuccess()
        {
            //arrange
            var id = 1;
            var club = A.Fake<Club>();
            A.CallTo(() => _clubRepository.GetByIdAsync(id));

            //act
            var result = _clubController.DetailClub(id);
            //assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}
