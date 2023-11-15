using InternetBanking.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.InternetBanking.Controllers
{
    public class BeneficiaryController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBeneficiaryService _beneficiaryService;


        public BeneficiaryController(IBeneficiaryService beneficiaryService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _beneficiaryService = beneficiaryService;
        }

        public IActionResult Index()
        {
            var user = _httpContextAccessor.HttpContext.User.Identity;
            //Me quede aqui.....
            return View();
        }
    }
}
