﻿using FreeCourse.Web.Models.Basket;
using FreeCourse.Web.Models.Discounts;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Web.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly ICatalogService catalogService;

        private readonly IBasketService basketService;

        public BasketController(ICatalogService catalogService, IBasketService basketService)
        {
            this.catalogService = catalogService;
            this.basketService = basketService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await basketService.Get());
        }

        public async Task<IActionResult> AddBasketItem(string courseId)
        {
            var course = await catalogService.GetByCourseId(courseId);
            var basketItem = new BasketItemViewModel { CourseId = course.Id, CourseName = course.Name, Price = course.Price };

            await basketService.AddBasketItem(basketItem);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteBasketItem(string courseId)
        {
            await basketService.RemoveBasketItem(courseId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ApplyDiscount(DiscountApplyInput discountApplyInput)
        {
            if (!ModelState.IsValid)
            {
                TempData["discountError"] = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).First();
                return RedirectToAction("Index");
            }
            var discountStatus = await basketService.ApplyDiscount(discountApplyInput.Code);
            TempData["discountStatus"] = discountStatus;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CancelApplyDiscount()
        {
            await basketService.CancelApplyDiscount();
            return RedirectToAction("Index");
        }
    }
}
