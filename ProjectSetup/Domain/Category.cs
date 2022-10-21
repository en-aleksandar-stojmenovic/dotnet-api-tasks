﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectSetup.Domain
{
	public class Category
	{
		[Key]
		public Guid Id { get; set; }
		public string Name { get; set; }
	}
}
