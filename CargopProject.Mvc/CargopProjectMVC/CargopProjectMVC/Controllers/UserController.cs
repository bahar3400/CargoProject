using CargopProjectMVC.Services.UserServices;
using CargoProject.Web.Services.Users;
using System.Web.Mvc;

namespace CargopProjectMVC.Controllers
{
    public class UserController : Controller
    {
        //Yane aslında benim artık controllerım serviceme modelleme erişerek null ifadesin kaldırmış oluyor
        private UserService _userService;

        public UserController() // parametresiz constructor
        {
            _userService = new UserService(); // servisi burada örnekle
        }

        // GET: User

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User model)
        {
           if(ModelState.IsValid)
            {
                var user  = _userService.Login(model);

                if(user!=null)
                {

                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ViewBag.ErrorMessage = "Kullanıcı Bulunamadı";
                }
            }
            //kullanıcının girdiği veriyi tekrar View’a yolluyoruz, böylece form dolu olarak geri gelir ve hata mesajlarıyla birlikte gösterilebilir.
            return View(model);
        }
       


    }
} 