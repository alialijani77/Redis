using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis.Domain.Entities
{
	public class Book
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public int Rating { get; set; }
	}
}
