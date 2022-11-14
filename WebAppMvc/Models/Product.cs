using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAppRazor.Validation;

namespace WebAppRazor.Models
{
	public class Product
	{
		public long ProductId { get; set; }

		[Required]
		[Display(Name = "Produkt")]
		public string? Name { get; set; }

		[Column(TypeName = "decimal(8,2)")]
		[Required(ErrorMessage = "Proszę podać cenę.")]
		[Range(1, 999999, ErrorMessage = "Proszę podać wartość dodatnią")]
		public decimal Price { get; set; }

		[PrimaryKey(ContextType = typeof(DataContext), DataType = typeof(Category))]
		public long CategoryId { get; set; }
		public Category? Category { get; set; }

		[PrimaryKey(ContextType = typeof(DataContext), DataType = typeof(Supplier))]
		public long SupplierId { get; set; }
		public Supplier? Supplier { get; set; }
	}
}
