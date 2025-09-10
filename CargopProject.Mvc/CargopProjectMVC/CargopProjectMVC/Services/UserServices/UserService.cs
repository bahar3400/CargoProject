using CargopProjectMVC.Services.Sql;
using CargoProject.Web.Services.Users;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CargopProjectMVC.Services.UserServices
{
    public class UserService : BaseDB
    {
        #region Metodlar
        public bool Add(User user)
        {
            SqlParameter[] sp =
            {
                new SqlParameter("@name",user.Name),
                new SqlParameter("@password",user.Password)
            };

            return Crud("spAddUser", sp);
        }
        //veritabanında var mı yok mu kontrol ediyor.
        public User Login(User user)
        {
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@name",user.Name),
                new SqlParameter("@password", user.Password)
            };
            DataTable dataTable = GetAll("spSelectNewUser", sqlParameters);
            foreach (DataRow row in dataTable.Rows)
            {
                //return direkt ilk satırda çıkış yaptığı için tek kullanıcı döndürür.
                return new User
                {
                    Name = row["Name"].ToString(),
                    Password = row["Password"].ToString()
                };
            }
            return null;

        }
        //Google ile giriş yapan bir kullanıcının veritabanında kayıtlı olup olmadığını kontrol etmek.
        public User GetBYGoogleId(string googleId, string eMail)
        {
            DataTable data = GetAllId("spGoogleId", CommandType.StoredProcedure, new SqlParameter("@GoogleId", googleId), new SqlParameter("@Email", eMail));

            if (data != null && data.Rows.Count > 0)
            {
                //İlk satırı alıyor: data.Rows[0].
                DataRow row = data.Rows[0];
                return new User
                {
                    ///<summary>
                    ///Satırdan (row) bilgileri alıyor.
                    /// Yeni bir User nesnesi oluşturuyor.
                    /// İçine veritabanındaki değerleri dolduruyor.
                    /// </summary>
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString(),
                    Password = row["Password"].ToString(),
                    Email = row["Email"].ToString(),
                    GoogleId = row["GoogleId"].ToString()
                };
            }

            return null;
        }

        #endregion

    }
}