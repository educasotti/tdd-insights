﻿using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.ViewModels;
using System.Security.Claims;

namespace RunGroopWebApp.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRespository;
        private readonly IPhotoService _photoService;

        public DashboardController(IDashboardRepository dashboardRespository, IPhotoService photoService)
        {
            _dashboardRespository = dashboardRespository;
            _photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            var userRaces = await _dashboardRespository.GetAllUserRaces(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userClubs = await _dashboardRespository.GetAllUserClubs(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var dashboardViewModel = new DashboardViewModel()
            {
                Races = userRaces,
                Clubs = userClubs
            };
            return View(dashboardViewModel);
        }
    }
}