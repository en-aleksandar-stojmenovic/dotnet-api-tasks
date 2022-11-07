using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectSetup.Contracts.V1;
using ProjectSetup.Contracts.V1.Requests;
using ProjectSetup.Data;
using ProjectSetup.Domain;
using ProjectSetup.Exceptions;
using ProjectSetup.Exceptions.ExceptionFilters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectSetup.Controllers.V1
{
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public CategoryController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet(ApiRoutes.Categories.GetAll)]
		public async Task<ActionResult<List<Category>>> Get()
		{
			return Ok(await _context.Categories.ToListAsync());
		}

		[HttpGet(ApiRoutes.Categories.GetCategory)]
		[ServiceFilter(typeof(CategoryNotFoundExceptionFilter))]
		public async Task<ActionResult<Category>> Get([FromRoute] Guid categoryId)
		{
			var category = await _context.Categories.FindAsync(categoryId);

			if (category == null)
			{
				throw new CategoryNotFoundException("Category with Id: '" + categoryId + "' not found");
			}

			return Ok(category);
		}

		[HttpGet(ApiRoutes.Categories.GetCategoryByName)]
		[ServiceFilter(typeof(CategoryNotFoundExceptionFilter))]
		public async Task<ActionResult<Category>> GetCategoryByName([FromRoute] string categoryName)
		{
			var category = await _context.Categories.SingleOrDefaultAsync(x => x.Name == categoryName);

			if (category == null)
			{
				throw new CategoryNotFoundException("Category with Name: '" + categoryName + "' not found");
			}

			return Ok(category);
		}

		[HttpPost(ApiRoutes.Categories.Create)]
		[ServiceFilter(typeof(CategoryBadRequestExceptionFilter))]
		public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
		{
			var category = new Category { Id = Guid.NewGuid(), Name = postRequest.Name };

			if (await _context.Categories.SingleOrDefaultAsync(x => x.Name == category.Name) != null)
			{
				throw new CategoryBadRequestException("Category with Name: '" + category.Name + "' not found");
			}

			await _context.Categories.AddAsync(category);
			var created = await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(Get), new { id = category.Id}, category);
		}

		[HttpDelete(ApiRoutes.Categories.Delete)]
		[ServiceFilter(typeof(CategoryBadRequestExceptionFilter))]
		public async Task<IActionResult> DeleteCategoryById([FromRoute] Guid categoryId)
		{
			var category = await _context.Categories.FindAsync(categoryId);

			if (category == null)
			{
				throw new CategoryBadRequestException("Category with Id: '" + categoryId + "' not found");
			}

			_context.Categories.Remove(category);

			var deleted = await _context.SaveChangesAsync();

			return Ok(deleted > 0);
		}

		[HttpDelete(ApiRoutes.Categories.DeleteByName)]
		[ServiceFilter(typeof(CategoryBadRequestExceptionFilter))]
		public async Task<IActionResult> DeleteCategoryByName([FromRoute] string categoryName)
		{
			var category = await _context.Categories.SingleOrDefaultAsync(x => x.Name == categoryName);

			if (category == null)
			{
				throw new CategoryBadRequestException("Category with Name: '" + categoryName + "' not found");
			}

			_context.Categories.Remove(category);
			var deleted = await _context.SaveChangesAsync();

			return Ok(deleted > 0);
		}
	}
}
