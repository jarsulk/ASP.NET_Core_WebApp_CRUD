namespace WebAppMvc.Models
{
	public static class ViewModelFactory
	{
		public static ProductViewModel Details(Product product)
		{
			return new ProductViewModel
			{
				Product = product,
				Action = "Details",
				ReadOnly = true,
				Theme = "info",
				ShowAction = false,
				Categories = (product == null || product.Category == null) ?
						Enumerable.Empty<Category>() : new List<Category> { product.Category },
				Suppliers = (product == null || product.Supplier == null) ?
						Enumerable.Empty<Supplier>() : new List<Supplier> { product.Supplier },
				Title = "Informacje",
			};
		}

		public static ProductViewModel Create(Product product, IEnumerable<Category> categories,
				IEnumerable<Supplier> suppliers)
		{
			return new ProductViewModel
			{
				Product = product,
				Categories = categories,
				Suppliers = suppliers,
			};
		}

		public static ProductViewModel Edit(Product product, IEnumerable<Category> categories,
				IEnumerable<Supplier> suppliers)
		{
			return new ProductViewModel
			{
				Product = product,
				Categories = categories,
				Suppliers = suppliers,
				Theme = "warning",
				Action = "Edit",
			};
		}

		public static ProductViewModel Delete(Product product, IEnumerable<Category> categories,
				IEnumerable<Supplier> suppliers)
		{
			return new ProductViewModel
			{
				Product = product,
				Categories = categories,
				Suppliers = suppliers,
				Action = "Delete",
				ReadOnly = true,
				Theme = "danger",
			};
		}

		public static SupplierViewModel SupplierPageModel(string returnPage)
		{
			return new SupplierViewModel
			{
				ReturnPage = returnPage,
				Supplier = new Supplier(),
			};
		}
	}
}
