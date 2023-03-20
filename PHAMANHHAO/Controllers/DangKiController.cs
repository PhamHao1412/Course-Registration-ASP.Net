using PHAMANHHAO.Models;
using PHAMANHHAO.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHAMANHHAO.Controllers
{
    public class DangKiController : Controller
    {
        // GET: DangKi
        dbTest1DataContext data = new dbTest1DataContext();
        public List<Hocphan> Laygiohang()
        {
            List<Hocphan> lstGiohang = Session["HocPhan"] as List<Hocphan>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<Hocphan>();
                Session["HocPhan"] = lstGiohang;

            }
            return lstGiohang;
        }

        public ActionResult ThemGioHang(string id, string strURL)
        {
            List<Hocphan> lstGiohang = Laygiohang();
            Hocphan sanpham = lstGiohang.Find(n => n.MaHP == id);
            if (sanpham == null)
            {
                sanpham = new Hocphan(id);
                lstGiohang.Add(sanpham);
            }
            else
            {
                sanpham.SoTC++;
            }

            ViewBag.TongSoTinChi = TongSoTinChi();
            if (Request.IsAjaxRequest())
            {
                return PartialView("GioHangPartial");
            }
            else
            {
                return Redirect(strURL);
            }
        }


        private int SoHocPhan()
        {
            int tsl = 0;
            List<Hocphan> lstGiohang = Session["HocPhan"] as List<Hocphan>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Count;
            }
            return tsl;
        }
        private int TongSoTinChi()
        {
            int tsl = 0;
            List<Hocphan> lstGiohang = Session["HocPhan"] as List<Hocphan>;
            if (lstGiohang != null)
            {
                tsl = (int)lstGiohang.Sum(n => n.SoTC);

            }
            return tsl;
        }

        public ActionResult DangKi()
        {
            List<Hocphan> lstGiohang = Laygiohang();
            ViewBag.SoHocPhan = SoHocPhan();
            ViewBag.TongSoTinChi = TongSoTinChi();
            return View(lstGiohang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.SoHocPhan = SoHocPhan();
            ViewBag.TongSoTinChi = TongSoTinChi();
            return PartialView();
        }
        public ActionResult XoaGiohang(string id)
        {
            List<Hocphan> lstGiohang = Laygiohang();
            Hocphan sanpham = lstGiohang.SingleOrDefault(n => n.MaHP == id);
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.MaHP == id);
                return RedirectToAction("DangKi");
            }
            return RedirectToAction("DangKi");
        }
       
        public ActionResult XoaTatCaGioHang()
        {
            List<Hocphan> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("DangKi");
        }
        [HttpGet]
        public ActionResult LuuDangKi()
        {
            if (Session["SinhVien"] == null || Session["SinhVien"].ToString() == "")
            {
                return RedirectToAction("LogIn", "User");
            }
            List<Hocphan> lstGiohang = Laygiohang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "Student");
            }
            ViewBag.SoHocPhan = SoHocPhan();
            ViewBag.TongSoTinChi = TongSoTinChi();
            return View(lstGiohang);
        }
        [HttpPost]

        public ActionResult LuuDangKi(FormCollection collection)
        {

            //DangKy dk = new DangKy();
            //SinhVien sv = (SinhVien) Session["SinhVien"];
            //List<Hocphan> lstGiohang = Laygiohang();
            //var ngayDk = String.Format("{0:MM/dd/yyyy}", collection["NgayDangKy"]);
            //dk.NgayDK = DateTime.Parse(ngayDk);
            //dk.MaSV = sv.MaSV;
            //data.SubmitChanges();
            //data.DangKies.InsertOnSubmit(dk);
            //foreach(var item in lstGiohang)
            //{
            //    ChiTietDangKy ctdk = new ChiTietDangKy();
            //    ctdk.MaDK = dk.MaDK;
            //    ctdk.MaHP = item.MaHP;    
            //    data.SubmitChanges();
            //    data.ChiTietDangKies.InsertOnSubmit(ctdk);
            //}
            //data.SubmitChanges();
            //Session["HocPhan"] = null;
            //return RedirectToAction("XacNhanDangKy", "DangKi");
            // Thêm dữ liệu vào bảng DangKy
            DangKy dk = new DangKy();
            SinhVien sv = (SinhVien)Session["SinhVien"];
            List<Hocphan> lstGiohang = Laygiohang();
            var ngayDk = String.Format("{0:MM/dd/yyyy}", collection["NgayDangKy"]);
            dk.NgayDK = DateTime.Parse(ngayDk);
            dk.MaSV = sv.MaSV;
            data.DangKies.InsertOnSubmit(dk);
            data.SubmitChanges(); // SubmitChanges ở đây


            // Thêm dữ liệu vào bảng ChiTietDangKy
            foreach (var item in lstGiohang)
            {
                ChiTietDangKy ctdk = new ChiTietDangKy();
                ctdk.MaDK = dk.MaDK;
                ctdk.MaHP = item.MaHP;
                HocPhan hp = data.HocPhans.Single(n => n.MaHP == item.MaHP);
                hp.SoLuong--;
                
                data.ChiTietDangKies.InsertOnSubmit(ctdk);
            }
            data.SubmitChanges();

            // Xóa dữ liệu giỏ hàng và chuyển hướng đến trang xác nhận đăng ký
            Session["HocPhan"] = null;
            return RedirectToAction("XacNhanDangKi", "DangKi");


        }
        public ActionResult XacNhanDangKi()
        {
            return View();
        }


    }
}