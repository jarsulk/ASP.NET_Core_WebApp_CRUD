using Microsoft.AspNetCore.Mvc;

namespace WebAppMvc.Models
{
	public class SupplierViewModel
	{
		[BindProperty]
		public Supplier? Supplier { get; set; }

		public string? ReturnPage { get; set; }
		public string? ProductId { get; set; }
	}
}
