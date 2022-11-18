using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WebAppMvc.Models;

namespace WebAppMvc.Controllers
{
	public class HomeController : Controller
	{
		private DataContext context;

		private IEnumerable<Category> Categories => context.Categories;
		private IEnumerable<Supplier> Suppliers => context.Suppliers;

		public HomeController(DataContext context)
		{
			this.context = context;
		}

		public IActionResult Index()
		{
			return View(context.Products.
					Include(p => p.Category).
					Include(p => p.Supplier));
		}

		public async Task<IActionResult> Details(long id)
		{
			Product? product = await context.Products.
					Include(p => p.Category).
					Include(p => p.Supplier).
					FirstOrDefaultAsync(p => p.ProductId == id) ?? new Product();
			ProductViewModel model = ViewModelFactory.Details(product);
			return View("ProductEditor", model);
		}

		[HttpGet]
		public IActionResult Create()
		{
			Product product = TempData.ContainsKey("product") ?
					JsonSerializer.Deserialize<Product>((TempData["product"] as string)!)! :
					new Product();
			return View("ProductEditor", ViewModelFactory.Create(product, Categories, Suppliers));
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromForm] Product product)
		{
			await CheckNewCategory(product);
			if (ModelState.IsValid)
			{
				product.Category = default;
				product.Supplier = default;
				context.Products.Add(product);
				await context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View("ProductEditor", ViewModelFactory.Create(new Product(), Categories, Suppliers));
		}

		[HttpGet]
		public async Task<IActionResult> Edit(long id)
		{
			Product? product = TempData.ContainsKey("product") ?
					JsonSerializer.Deserialize<Product>((TempData["product"] as string)!) :
					await context.Products.FindAsync(id);
			if (product != null)
			{
				ProductViewModel model = ViewModelFactory.Edit(product, Categories, Suppliers);
				return View("ProductEditor", model);
			}
			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> Edit([FromForm] Product product)
		{
			await CheckNewCategory(product);
			if (ModelState.IsValid)
			{
				product.Category = default;
				product.Supplier = default;
				context.Products.Update(product);
				await context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View("ProductEditor", ViewModelFactory.Edit(product, Categories, Suppliers));
		}

		[HttpGet]
		public async Task<IActionResult> Delete(long id)
		{
			Product? product = await context.Products.FindAsync(id);
			if (product != null)
			{
				ProductViewModel model = ViewModelFactory.Delete(product, Categories, Suppliers);
				return View("ProductEditor", model);
			}
			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> Delete(Product product)
		{
			context.Products.Remove(product);
			await context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private async Task CheckNewCategory(Product product)
		{
			if (product.CategoryId == -1 && !string.IsNullOrEmpty(product.Category?.Name))
			{
				context.Categories.Add(product.Category);
				await context.SaveChangesAsync();
				product.CategoryId = product.Category.CategoryId;
				ModelState.Clear();
				TryValidateModel(product);
			}
		}

		[HttpGet]
		public IActionResult SupplierBreakOut([FromQuery(Name = "Product")] Product product,
				string returnPage)
		{
			SupplierViewModel model = ViewModelFactory.SupplierPageModel(returnPage);
			TempData["product"] = JsonSerializer.Serialize(product);
			TempData["returnAction"] = returnPage;
			TempData["productId"] = product.ProductId.ToString();
			return View("SupplierBreakOut", model);
		}

		[HttpPost]
		public async Task<IActionResult> SupplierBreakOut([FromForm] Supplier supplier)
		{
			if (ModelState.IsValid && supplier != null)
			{
				context.Suppliers.Add(supplier);
				await context.SaveChangesAsync();
				Product? product = (TempData == null || TempData["product"] == null) ? null :
						JsonSerializer.Deserialize<Product>((TempData["product"] as string)!);
				if (product != null)
				{
					product.SupplierId = supplier.SupplierId;
					TempData["product"] = JsonSerializer.Serialize(product);
					string? id = TempData["productId"] as string;
					return RedirectToAction(TempData["returnAction"] as string, new { id = id });
				}
			}
			return View();
		}
	}
}
