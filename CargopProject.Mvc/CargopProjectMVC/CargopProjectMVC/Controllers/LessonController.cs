using CargopProjectMVC.Models.Lessons;
using CargopProjectMVC.Services.LessonServices;
using System;
using System.Web.Mvc;


namespace CargopProjectMVC.Controllers
{
    public class LessonController : Controller
    {
        private LessonService _lessonService = new LessonService();
        // GET: Lesson
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.PageTitle = "Ders Ekleme Programı";
            var classList = _lessonService.GetDropdown("spGetClass", "Sinif", "Sinif");
            ViewBag.Sinif = classList;
            var dayList = _lessonService.GetDropdown("spGetDay", "Gun", "Gun");
            ViewBag.Gun = dayList;
            return View();
        }

        /// <summary>
        /// İlk set: Model validasyonu başarısız → view tekrar gösterilecek
        /// İkinci set: Veritabanına ekleme sonrası → view tekrar gösterilecek
        /// </summary>
        [HttpPost]
        public ActionResult Create(Lesson lesson)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Sinif = _lessonService.GetDropdown("spGetClass", "Sinif", "Sinif");
                ViewBag.Gun = _lessonService.GetDropdown("spGetDay", "Gun", "Gun");
                return View(lesson);
            }

            var control = _lessonService.Add(lesson);
            if (control == true)
            {
                ViewBag.ErrorMessage = "Ekleme Yapıldı";
            }
            else
            {
                ViewBag.ErrorMessage = "Ekleme Yapılamadı";
            }

            ViewBag.Sinif = _lessonService.GetDropdown("spGetClass", "Sinif", "Sinif");
            ViewBag.Gun = _lessonService.GetDropdown("spGetDay", "Gun", "Gun");

            return View(lesson);
        }

    }
}