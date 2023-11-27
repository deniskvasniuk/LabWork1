using LabWork1;
using LabWork1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;

public class NavbarViewComponent : ViewComponent
{
    private readonly IHtmlLocalizer<LangResource> _localizer;

    public NavbarViewComponent(IHtmlLocalizer<LangResource> localizer)
    {
        _localizer = localizer;
    }

    public IViewComponentResult Invoke()
    {
        var navbarItems = new[]
        {
            new NavbarItem { Controller = "home", Action = "index", Text = _localizer["Home"].Value, IsActive = true },
            new NavbarItem { Controller = "about", Action = "index", Text = _localizer["About"].Value },
            new NavbarItem { Controller = "service", Action = "index", Text = _localizer["Services"].Value },
            new NavbarItem { Controller = "news", Action = "index", Text = _localizer["News"].Value },
            new NavbarItem { Controller = "contact", Action = "index", Text = _localizer["ContactUs"].Value },
            new NavbarItem { Controller = "bookings", Action = "index", Text = _localizer["Admin"].Value }
        };

        return View("NavbarViewComponent.cshtml", navbarItems);
    }
}