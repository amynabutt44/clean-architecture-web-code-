using System.ComponentModel.DataAnnotations;

namespace domain
{
	public class product
	{
		[Required]
		public string name { get; set; }
		public int price { get; set; }
		public string description { get; set; }

		public string image { get; set; }


		public int Id { get; set; }

		public int c_id { get; set; }
	}
}
