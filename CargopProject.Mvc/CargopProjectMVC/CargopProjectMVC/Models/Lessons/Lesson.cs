using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CargopProjectMVC.Models.Lessons
{
    /// <summary>
    /// Sen HTML tarafında her input’u @Html.TextBoxFor(model => model.DersKodu) gibi model property’sine bağladın.
    /// MVC, POST geldiğinde model binding sayesinde otomatik olarak Lesson lesson objesinin ilgili property’sine değerleri doldurdu.
    /// Yani controller’da tek tek lesson.DersKodu = TxtLessonCode.Text; gibi atama yapmana gerek yok.
    /// </summary>
    /// Dropdown inputları da model property’sine bağlansaydı bile seçenek listesini (IEnumerable<SelectListItem>) ViewBag veya ViewModel’den sağlaman gerekiyor.
    ///  DropDownListFor sadece seçilen değeri model property’sine atamaz, listeyi de görebilmesi gerekir.
    ///  Eğer liste null olursa, MVC “There is no ViewData item of type 'IEnumerable<SelectListItem>'…” hatası fırlatır.
    public class Lesson
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        public string DersKodu { get; set; }

        [Required(ErrorMessage = "Zorunlu")]
        public string DersAdi { get; set; }

        public string Sinif { get; set; }

        public string Gun { get; set; }

        public int Saat { get; set; }

        public string OgretmenAdi { get; set; }

        public int OgretmenKodu { get; set; }

        public DateTime? Log_dt { get; set; }



        ///<summary>
        /// ViewBag
        /// ASP.NET MVC’de controller’dan view’a veri taşımak için kullanılan dinamik bir nesnedir.
        /// Dinamik olduğu için hangi property’i eklemek istiyorsan ekleyebilirsin.
        /// Controller’dan view’a veri taşımak
        /// 
        /// 
        /// ViewModel
        /// Bir view için özel olarak oluşturulmuş C# class’ıdır.
        /// İçinde formdaki tüm inputları ve dropdown listelerini property olarak tutarsın.
        /// View’a özel sınıf, input ve dropdownları tutar
        /// public IEnumerable<SelectListItem> SinifList { get; set; }
        /// </summary>
    }
}
    