using CargopProjectMVC.Services.Sql;
using CargopProjectMVC.Models.Lessons;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System;
using System.Web.Mvc;
using System.Web;
using System.Linq;

namespace CargopProjectMVC.Services.LessonServices
{
    public class LessonService
    {
        BaseDB db = new BaseDB();
        #region Metod
        public bool Add(Lesson lesson)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@dersKodu",lesson.DersKodu),
                new SqlParameter("@dersAdi", lesson.DersAdi),
                new SqlParameter("@sinif", lesson.Sinif),
                new SqlParameter("@gun", lesson.Gun),
                new SqlParameter("@saat", lesson.Saat),
                new SqlParameter("@ogretmenAdi", lesson.OgretmenAdi),
                new SqlParameter("@ogretmenKodu", lesson.OgretmenKodu),
                new SqlParameter("@log_dt", lesson.Log_dt)

            };

            return db.Crud("spAdd", sqlParameters);
        }
        //Drop işlemlerimde veritabanında veriyi çekmek istediğimde kullanabileceğim bir metod

        /// <summary>
        /// LessonControl içerisinde VT  sınıf ve Gün çekmek için kullanabileceğim bir metod içeriği
        ///  Yane her satırı bir listenin öğresin çevirdik
        /// </summary>
        /// <returns></returns>
        /// 

        //public void DropDownAll(DropDownList drop, string spName, string textField, string textValue)
        //{
        //    DataTable dataTable = db.GetAll(spName);
        //    drop.DataSource = dataTable;
        //    drop.DataTextField = textField;
        //    drop.DataValueField = textValue;
        //    drop.DataBind();
        //}

        public List<SelectListItem> GetDropdown(string spName, string textColumn, string valueColumn)
        {
            DataTable dataTable = db.GetAll(spName);
            return dataTable.AsEnumerable()
            .Select(model => new SelectListItem
            {
                Text = model[textColumn].ToString(),
                Value = model[valueColumn].ToString()
            }).ToList();
        }
        public List<Lesson> GetAlllList()
        {
            List<Lesson> lessons = new List<Lesson>();
            DataTable dataTable = db.GetAll("spSelectLesson");
            foreach (DataRow row in dataTable.Rows)
            {
                lessons.Add(new Lesson()
                {// Id datatable de gelen ıd eğer null ise 0 al değilse int çevirerek Listeye ekle
                    Id = (row["Id"]) == DBNull.Value ? 0 : Convert.ToInt32(row["Id"]),
                    DersAdi = (row["DersAdi"]).ToString(),
                    DersKodu = (row["DersKodu"]).ToString(),
                    Sinif = (row["Sinif"]).ToString(),
                    Gun = (row["Gun"]).ToString(),
                    OgretmenAdi = (row["OgretmenAdi"]).ToString(),
                    OgretmenKodu = (row["OgretmenKodu"]) == DBNull.Value ? 0 : Convert.ToInt32(row["OgretmenKodu"]),
                    Saat = (row["Saat"]) == DBNull.Value ? 0 : Convert.ToInt32(row["Id"]),
                    Log_dt = Convert.ToDateTime(row["log_dt"])
                });

            }

            return lessons;
        }
        public bool Delete(Lesson lesson)
        {
            SqlParameter[] sp =
            {
                new SqlParameter("@id" , lesson.Id)
            };
            return db.Crud("spDeleteAcces", sp);
        }
        public bool Update(Lesson lesson)
        {
            SqlParameter[] sp =
            {
                new SqlParameter("@id" , lesson.Id),
                new SqlParameter("@dersKodu",lesson.DersKodu),
                new SqlParameter("@dersAdi", lesson.DersAdi),
                new SqlParameter("@sinif", lesson.Sinif),
                new SqlParameter("@gun", lesson.Gun),
                new SqlParameter("@saat", lesson.Saat),
                new SqlParameter("@ogretmenAdi", lesson.OgretmenAdi),
                new SqlParameter("@ogretmenKodu", lesson.OgretmenKodu)
            };
            return db.Crud("spUpdateAcces", sp);
        }
        //Veritabanınddan  çekip bilgiler lesson nesneme ekleyerek view sayfamın dolu gelmesi için kullanılan bir metod (View kısmında hangi toolboxlar gelecekleri söylerim) 
        public Lesson GetById(int id)
        {
            Lesson lesson = null;
            DataTable dataTable = db.GetAll("spGetId", new SqlParameter("@id", id));
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                //Yani bu satır, DataTable içindeki ilk satırı alıp row değişkenine atıyor.
                DataRow row = dataTable.Rows[0];
                lesson = new Lesson()
                {
                    Id = (row["Id"]) == DBNull.Value ? 0 : Convert.ToInt32(row["Id"]),
                    DersAdi = (row["DersAdi"]).ToString(),
                    DersKodu = (row["DersKodu"]).ToString(),
                    Sinif = (row["Sinif"]).ToString(),
                    Gun = (row["Gun"]).ToString(),
                    OgretmenAdi = (row["OgretmenAdi"]).ToString(),
                    OgretmenKodu = (row["OgretmenKodu"]) == DBNull.Value ? 0 : Convert.ToInt32(row["OgretmenKodu"]),
                    Saat = (row["Saat"]) == DBNull.Value ? 0 : Convert.ToInt32(row["Id"]),
                    Log_dt = Convert.ToDateTime(row["log_dt"])

                };
            }

            return lesson;
        }

        #endregion
    }
}