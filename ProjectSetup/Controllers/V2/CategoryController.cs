using Microsoft.AspNetCore.Mvc;
using ProjectSetup.Contracts.V2;
using ProjectSetup.Domain;
using System;
using System.Collections.Generic;

namespace ProjectSetup.Controllers.V2
{
	public class CategoryController : Controller
	{
		private List<Category> _categories;
		public CategoryController()
		{
			_categories = new List<Category>();

			_categories.Add(new Category { Id = Guid.NewGuid().ToString(), Name = "Life" });
			_categories.Add(new Category { Id = Guid.NewGuid().ToString(), Name = "People" });
			_categories.Add(new Category { Id = Guid.NewGuid().ToString(), Name = "Everyday" });
			_categories.Add(new Category { Id = Guid.NewGuid().ToString(), Name = "News" });
			_categories.Add(new Category { Id = Guid.NewGuid().ToString(), Name = "Travel" });
		}

		[HttpGet(ApiRoutes.Categories.GetAll)]
		public IActionResult Get()
		{
			return Ok(_categories);
		}
	}
}
