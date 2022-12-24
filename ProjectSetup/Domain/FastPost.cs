using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectSetup.Domain
{
	public class FastPost
	{
		[Key]
		public Guid Id { get; set; }
		public string Text { get; set; }
		public DateTime Created { get; set; }
		public Guid CategoryId { get; set; }
		public Guid CreatedBy { get; set; }
	}
}
