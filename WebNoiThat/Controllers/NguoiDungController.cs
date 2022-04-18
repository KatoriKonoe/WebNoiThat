using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNoiThat.Models;



namespace WebNoiThat.Controllers
{
    public class NguoiDungController : Controller
    {
        // GET: NguoiDung
        DbNoiThatDataContext db = new DbNoiThatDataContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangky(FormCollection collection, KHACHHANG kh)
        {
            var hoten = collection["HoTenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            var matkhautralai = collection["Matkhautralai"];
            var diachi = collection["Diachi"];
            var email = collection["Email"];
            var dienthoai = collection["Dienthoai"];
            var ngaysinh = String.Format("{0:MM/dd/yy}", collection["Ngaysinh"]);
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Ho ten khong duoc de trong";
            }
            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Dang nhap khong duoc de trong";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Phai nhap mat khau";
            }
            else if (String.IsNullOrEmpty(matkhautralai))
            {
                ViewData["Loi4"] = "Phai nhap lai mat khau";
            }
            else if (String.IsNullOrEmpty(diachi))
            {
                ViewData["Loi5"] = "Phai nhap dia chi";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi6"] = "Phai nhap dia chi Email";
            }
            else if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi7"] = "Phai nhap dien thoai";
            }
            else
            {
                //ganvaodata
                kh.TENKH = hoten;
                kh.TAIKHOANG = tendn;
                kh.MATKHAU = matkhau;
                kh.DIACHI = diachi;
                kh.EMAIL = email;
                kh.DT = dienthoai;
                db.KHACHHANGs.InsertOnSubmit(kh);
                db.SubmitChanges();
            }
            return this.Dangky();
        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(FormCollection collection)
        {
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = " Ten dang nhap khong duoc de trong";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Mat khau khong duoc de trong";
            }
            else
            {
                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.TAIKHOANG==tendn && n.MATKHAU==matkhau);
                if (kh != null)
                {
                    ViewBag.ThongBao = "ban da dang nhap thanh cong";
                    Session["TenDN"] = kh;
                }
                else
                    ViewBag.ThongBao = "sai tai khoan mat khau";
            }
            return View();
        }
    }
}