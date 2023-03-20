using PHAMANHHAO.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHAMANHHAO.Controllers
{
    public class UserController : Controller
    {
        dbTest1DataContext data = new dbTest1DataContext();
        // GET: User
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();  
        }
        [HttpPost]
        public ActionResult LogIn(FormCollection collection)
        {
            var masv = collection["masv"];
            SinhVien sv = data.SinhViens.SingleOrDefault(n => n.MaSV == masv);
            if (sv != null)
            {
                ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                Session["SinhVien"] =sv ;
                return RedirectToAction("HocPhan", "Student");
            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng ";
            }

            return RedirectToAction("HocPhan", "Student");
        }
    }
}