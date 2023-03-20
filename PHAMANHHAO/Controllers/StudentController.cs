using PHAMANHHAO.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHAMANHHAO.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        dbTest1DataContext data = new dbTest1DataContext();
        public ActionResult Index()
        {
            var all_student = from s in data.SinhViens select s;
            return View(all_student);
        }
        public ActionResult Details(string id)
        {
            var st = data.SinhViens.Where(s => s.MaSV == id).First();
            return View(st);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, SinhVien s)
        {
            var E_masv = collection["MaSV"];
            var E_ten = collection["Hoten"];
            var E_gioitinh = collection["GioiTinh"];
            var E_ngaysinh = Convert.ToDateTime(collection["NgaySinh"]);
            var E_hinh = collection["hinh"];
            var E_maNganh = collection["MaNganh"];

            if (string.IsNullOrEmpty(E_ten))
            {
                ViewData["Error"] = "Don't empty";
            }
            else
            {
                s.MaSV = E_masv;
                s.HoTen = E_ten.ToString();
                s.GioiTinh = E_gioitinh;
                s.NgaySinh = E_ngaysinh;
                s.Hinh = E_hinh.ToString();
                s.MaNganh = E_maNganh;
                data.SinhViens.InsertOnSubmit(s);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "Content/images/" + file.FileName;
        }
        public ActionResult Edit(string id)
        {
            var E_Sv = data.SinhViens.First(m => m.MaSV == id);
            return View(E_Sv);
        }
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            var s = data.SinhViens.First(m => m.MaSV == id);
            var E_ten = collection["Hoten"];
            var E_gioitinh = collection["GioiTinh"];
            var E_ngaysinh = Convert.ToDateTime(collection["NgaySinh"]);
            var E_hinh = collection["hinh"];
            var E_maNganh = collection["MaNganh"];
            s.MaSV = id;
            if (string.IsNullOrEmpty(E_ten))
            {
                ViewData["Error"] = "Don't empty";
            }
            else
            {
                s.HoTen = E_ten.ToString();
                s.GioiTinh = E_gioitinh;
                s.NgaySinh = E_ngaysinh;
                s.Hinh = E_hinh.ToString();
                s.MaNganh = E_maNganh;
                UpdateModel(s);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        public ActionResult Delete(string id)
        {
            var St = data.SinhViens.First(m => m.MaSV == id);
            return View(St);
        }
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            var student = data.SinhViens.Where(m => m.MaSV == id).First();
            data.SinhViens.DeleteOnSubmit(student);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult HocPhan()
        {
            var all_student = from s in data.HocPhans select s;
            return View(all_student);

        }

    }
}