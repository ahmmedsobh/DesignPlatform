﻿using DesignPlatform.Data;
using DesignPlatform.Enums;
using DesignPlatform.Helpers;
using DesignPlatform.Models;
using DesignPlatform.ViewModels.CustomerHomeViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DesignPlatform.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public HomeController(ILogger<HomeController> logger , ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_logger = logger;
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

		public async  Task<IActionResult> Index()
		{
			var viewModel = await context.Settings.Where(i=> true).Select(i=> new CustomerHomeViewModel()
			{
				Packages = context.Packagees.Where(p=> p.IsActive).Select(p=> new CustomerHomePackageViewModel()
				{
					Id = p.Id,
					Name = p.Name,
					Image = AppHost.Url + p.ImagePath,
					Rate = p.Rate,
					ReviewsCount = 250,
					Features = p.PackageFeatures.Select(f=> f.Name).ToList()
				}).ToList(),
			}).FirstOrDefaultAsync();

			
			return View(viewModel);
		}

		public async Task<IActionResult> Package(int Id)
		{
			if(Id == 0)
			{
				return BadRequest();
			}

            var Package = await context.Packagees.Where(i => i.Id == Id).Select(p => new CustomerHomePackageViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Image = AppHost.Url + p.ImagePath,
                Rate = p.Rate,
                ReviewsCount = 250,
                Features = p.PackageFeatures.Select(f => f.Name).ToList(),
				PlaceOrder = new PlaceOrderViewModel()
				{
					PackageId = Id,
					PackageName = p.Name,
					PackageImage = AppHost.Url + p.ImagePath,
					BackYardPrice = p.BackYardPrice,
					FrontYardPrice = p.FrontYardPrice,
					FrontBackYardPrice = p.FrontBackYardPrice,
					Area = "Back Yard"
                },
            }).FirstOrDefaultAsync();

			return View(Package);


		}

		[HttpPost]
		public async Task<IActionResult> PlaceOrder(PlaceOrderViewModel model)
		{

			if(ModelState.IsValid)
			{
				//add client 

				var client = await context.Users.FirstOrDefaultAsync(i => i.Email == model.Email || i.PhoneNumber == model.Phone);

				if(client == null)
				{
                    client = new ApplicationUser()
                    {
                        Email = model.Email,
                        UserName = model.Email,
                        PhoneNumber = model.Phone,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Address = model.StreetAddress,
                        ZipCode = model.ZipCode,
                    };

					var UserResult = await userManager.CreateAsync(client,"123456");

					if(UserResult.Succeeded)
					{
						await userManager.AddToRoleAsync(client,Roles.Client.ToString());
					}
                }

				var package = await context.Packagees.FirstOrDefaultAsync(i=> i.Id == model.PackageId);

				if(package == null)
				{
					return BadRequest();
				}

				// add project 
				var project = new Project()
				{
					Phone = model.Phone,
					ClientId = client.Id,
					Area = model.Area,
					Price = model.Area == "Back Yard" ? package.BackYardPrice : (model.Area == "Front Yard" ? package.FrontYardPrice : package.FrontBackYardPrice),
                    Date = DateTime.Now,
					ProjectPackages = new List<ProjectPackage>()
					{
						new ProjectPackage() 
						{
							PackageId = model.PackageId,
						}
					}
				};

				await context.AddAsync(project);
				await context.SaveChangesAsync();


                //login 
                await signInManager.PasswordSignInAsync(client, "123456", true, true);

                return Redirect("/customer");

				

			}

			return RedirectToAction(nameof(Package),new {Id = model.PackageId });

		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}