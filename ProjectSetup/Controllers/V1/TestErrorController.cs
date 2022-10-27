using Microsoft.AspNetCore.Mvc;
using ProjectSetup.Contracts.V1;
using ProjectSetup.Contracts.V1.Responses;
using ProjectSetup.Data;
using ProjectSetup.Domain;
using ProjectSetup.Services;
using System;

namespace ProjectSetup.Controllers.V1
{
	[ApiController]
	public class TestErrorController: ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly IFileService _fileService;

		public TestErrorController(ApplicationDbContext context, IFileService fileService)
		{
			_context = context;
			_fileService = fileService;
		}

		[HttpGet(ApiRoutes.Errors.NotFound)]
		public ActionResult<Category> GetNotFound()
		{
			var thing = _context.Categories.Find(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66efa6"));
			if (thing == null) return NotFound(_fileService.LogErrorsAndReturnResponse(new ApiResponse(404)));
			return thing;
		}

		[HttpGet(ApiRoutes.Errors.ServerError)]
		public ActionResult<string> GetServerError()
		{
			var thing = _context.Categories.Find(-1);
			var thingToReturn = thing.ToString();
			return thingToReturn;

		}

		[HttpGet(ApiRoutes.Errors.BadRequest)]
		public ActionResult<string> GetBadRequest()
		{
			return BadRequest(_fileService.LogErrorsAndReturnResponse(new ApiResponse(400)));
		}
	}
}
