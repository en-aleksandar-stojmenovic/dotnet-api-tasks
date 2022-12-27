using System;
using System.ComponentModel.DataAnnotations;

namespace Twitter.Domain
{
	public class Category
	{
		[Key]
		public Guid Id { get; set; }
		public string Name { get; set; }
	}
}
