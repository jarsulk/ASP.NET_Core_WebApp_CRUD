using Microsoft.AspNetCore.Mvc;

namespace WebAppMvc.Components
{
	public class CategoryEditor : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
