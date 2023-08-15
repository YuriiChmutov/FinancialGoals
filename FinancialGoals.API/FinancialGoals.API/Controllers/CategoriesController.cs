using System.Security.Claims;
using FinancialGoals.API.Filters;
using FinancialGoals.Core.DTOs;
using FinancialGoals.Data.Data;
using FinancialGoals.Data.Repository.CategoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FinancialGoals.Core.DTOs.Category;
using FinancialGoals.Core.Models;
using FinancialGoals.Data.Repository.TransactionService;
using FinancialGoals.Services;

namespace FinancialGoals.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly BlobStorageService _blobStorageService;
        private readonly ITransactionService _transactionService;

        private readonly IMapper _mapper;

        public CategoriesController(
            ICategoryService categoryService, 
            IMapper mapper, 
            ITransactionService transactionService, 
            BlobStorageService blobStorageService)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _transactionService = transactionService;
            _blobStorageService = blobStorageService;
        }

        // GET: api/Categories
        [HttpGet]
        //[TypeFilter(typeof(ApiKeyAttribute))]
        public async Task<ActionResult<IEnumerable<CategoryToReturn>>> GetCategories()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var categoriesFromRepo = 
                await _categoryService.GetCategoriesByUserIdAsync(int.Parse(userId)); 

            var categoriesToReturn = _mapper.Map<List<CategoryToReturn>>(categoriesFromRepo);
            
            return Ok(categoriesToReturn);
        }
        
        [HttpGet("categories-for-account/{accountId}")]
        public async Task<ActionResult<IEnumerable<CategoryToReturn>>> GetCategoriesByAccountId(int accountId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var categoriesFromRepo = 
                await _categoryService.GetCategoriesForAccountAsync(accountId);

            var categoriesToReturn = _mapper.Map<List<CategoryToReturn>>(categoriesFromRepo);
            
            return Ok(categoriesToReturn);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryToReturn>> GetCategory(int id)
        {
            var user = User; // to get user claims (its not my user, its ClaimsPrincipal), just to see at debug mode
            var categoryFromRepo = await _categoryService.GetCategoryAsync(id);
        
            if (categoryFromRepo == null)
            {
                return NotFound();
            }

            var categoryToReturn = _mapper.Map<CategoryToReturn>(categoryFromRepo);
        
            return categoryToReturn;
        }
        
        [HttpGet("{categoryId}/image")]
        public async Task<IActionResult> GetImage(int categoryId)
        {
            string imageName = "categories/food.png";

            byte[] imageData = await _blobStorageService.GetImageAsync(imageName);
            return File(imageData, "image/png");
        }
        
        // PUT: api/Categories/5
        [HttpPut("{accountId}/{id}")]
        public async Task<IActionResult> PutCategory(int accountId, int id, CategoryToUpdate request)
        {
            if (id != request.CategoryId)
            {
                return BadRequest();
            }
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            request.FilePath = $"user_{userId}/acc_{accountId}";
            var categoryToUpdate = _mapper.Map<Category>(request);
            categoryToUpdate.FinancialAccountId = accountId;
            
            try
            {
                await _categoryService.UpdateCategoryAsync(id, categoryToUpdate);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _categoryService.CategoryExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        
            return Ok();
        }
        
        // POST: api/Categories
        [HttpPost("{accountId}")]
        [Authorize]
        public async Task<ActionResult<Category>> PostCategory(CategoryToCreate request, int accountId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            request.FilePath = $"user_{userId}/acc_{accountId}";
            var category = _mapper.Map<Core.Models.Category>(request);
            await _categoryService.AddCategoryForAccountAsync(category, Int32.Parse(userId), accountId);

            var categoryToReturn = _mapper.Map<Category>(category);
            
            return CreatedAtAction("GetCategory", new { id = categoryToReturn.CategoryId }, categoryToReturn);
        }
        
        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
