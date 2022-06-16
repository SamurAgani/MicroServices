using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Services;
using FreeCource.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Catalog.Controllers
{
    public class CategoryController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        [Route("/api/[controller]/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return CreateActionResultInstance(categories);  
        }

        [HttpGet]
        [Route("/api/[controller]/GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var category = await _categoryService.GetById(id);
            return CreateActionResultInstance(category);
        }

        [HttpPost]
        [Route("/api/[controller]/Create")]
        public async Task<IActionResult> CreateCategory(CategoryDto categoryDto)
        {
            var responcse = await _categoryService.CreateAsync(categoryDto);
            return CreateActionResultInstance(responcse);
        }

        
    }
}
