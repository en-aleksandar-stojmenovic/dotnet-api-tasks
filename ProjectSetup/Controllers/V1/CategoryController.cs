using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectSetup.Contracts.V1;
using ProjectSetup.Contracts.V1.Requests;
using ProjectSetup.Contracts.V1.Responses;
using ProjectSetup.Data;
using ProjectSetup.Domain;
using ProjectSetup.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectSetup.Controllers.V1
{
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly IFileService _fileService;

		public CategoryController(ApplicationDbContext context, IFileService fileService)
		{
			_context = context;
			_fileService = fileService;
		}

		[HttpGet(ApiRoutes.Categories.GetAll)]
		public async Task<ActionResult<List<Category>>> Get()
		{
			return Ok(await _context.Categories.ToListAsync());
		}

		[HttpGet(ApiRoutes.Categories.GetCategory)]
		public async Task<ActionResult<Category>> Get([FromRoute] Guid categoryId)
		{
			var category = await _context.Categories.FindAsync(categoryId);
			if (category == null) return NotFound(_fileService.LogErrorsAndReturnResponse(new ApiResponse(404, "Category with Id: '" + categoryId + "' not found")));

			return Ok(category);
		}

		[HttpGet(ApiRoutes.Categories.GetCategoryByName)]
		public async Task<ActionResult<Category>> GetCategoryByName([FromRoute] string categoryName)
		{
			var category = await _context.Categories.SingleOrDefaultAsync(x => x.Name == categoryName);
			if (category == null) return NotFound(_fileService.LogErrorsAndReturnResponse(new ApiResponse(404, "Category with Name: '" + categoryName + "' not found")));

			return Ok(category);
		}

		[HttpPost(ApiRoutes.Categories.Create)]
		public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
		{

			var category = new Category { Id = Guid.NewGuid(), Name = postRequest.Name };

			if (await _context.Categories.SingleOrDefaultAsync(x => x.Name == category.Name) != null)
			{
				return BadRequest(_fileService.LogErrorsAndReturnResponse(new ApiResponse(400, "Category with Name: '" + category.Name + "' already exists")));
			}

			await _context.Categories.AddAsync(category);
			var created = await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(Get), new { id = category.Id}, category);
		}

		[HttpDelete(ApiRoutes.Categories.Delete)]
		public async Task<IActionResult> DeleteCategoryById([FromRoute] Guid categoryId)
		{
			var category = await _context.Categories.FindAsync(categoryId);

			if (category == null)
				return BadRequest(_fileService.LogErrorsAndReturnResponse(new ApiResponse(400, "Category with Id: '" + categoryId + "' doesn't exists")));

			_context.Categories.Remove(category);

			var deleted = await _context.SaveChangesAsync();

			return Ok(deleted > 0);
		}

		[HttpDelete(ApiRoutes.Categories.DeleteByName)]
		public async Task<IActionResult> DeleteCategoryByName([FromRoute] string categoryName)
		{
			var category = await _context.Categories.SingleOrDefaultAsync(x => x.Name == categoryName);

			if (category == null)
				return BadRequest(_fileService.LogErrorsAndReturnResponse(new ApiResponse(400, "Category with Name: '" + categoryName + "' doesn't exists")));

			_context.Categories.Remove(category);
			var deleted = await _context.SaveChangesAsync();

			return Ok(deleted > 0);
		}
	}
}
