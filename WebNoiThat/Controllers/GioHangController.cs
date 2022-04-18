using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNoiThat.Models;

namespace WebNoiThat.Controllers
{
    public class GioHangController : Controller
    {
        DbNoiThatDataContext db = new DbNoiThatDataContext();
        // GET: GioHang
        public List<Giohang> LayGiohang()
        {
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            //Neu ton tai, gan vao gio hang
            if (lstGiohang == null)
                lstGiohang = new List<Giohang>();
            Session["Giohang"] = lstGiohang;
            return lstGiohang;
        }
        public ActionResult ThemGiohang(int iMADV, string strURL)
        {
            //lay sesssion
            List<Giohang> lstGiohang = LayGiohang();
            //kiemtra sach da chon vao gio hang hay chua
            Giohang sanpham = lstGiohang.Find(n => n.iMADV == iMADV);
            if (sanpham == null)
            {
                sanpham = new Giohang(iMADV);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSL++;
                return Redirect(strURL);
            }

        }
        private int TongSoluong()
        {
            int iTongSoluong = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongSoluong = lstGiohang.Sum(n => n.iSL);
            }
            return iTongSoluong;
        }
        private double TongTien()
        {
            double iTongTien = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongTien = lstGiohang.Sum(n => n.dThanhtien);
            }
            return iTongTien;
        }
        public ActionResult Giohang()
        {
            List<Giohang> lstGiohang = LayGiohang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "NoiThat");
                ViewBag.Tongsoluong = TongSoluong();
                ViewBag.Tongtien = TongTien();
            }
            return View(lstGiohang);
        }
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoluong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }
        public ActionResult XoaGiohang(int iMaSP)
        {
            // Lay gio hang tu Session
            List<Giohang> lstGiohang = LayGiohang();
            //Kiem tra sach da co trong Session["Giohang"]
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iMADV == iMaSP);
            //Neu ton tai thi cho sua Soluong
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.iMADV == iMaSP);
                return RedirectToAction("Giohang");
            }
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "NoiThat");
            }
            return RedirectToAction("Giohang");
        }
        public ActionResult CapnhatGiohang(int iMaSP, FormCollection f)
        {
            //Lay gio hang tu Session
            List<Giohang> lstGiohang = LayGiohang();
            //Kiem tra sach da co trong Session["Giohang"]
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iMADV == iMaSP);
            //Neu ton tai thi cho sua Soluong
            if (sanpham != null)
            {
                sanpham.iSL = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("Giohang");
        }
        //Hien thi View DatHang de cap nhat cac thong tin cho Don hang
        [HttpGet]
        public ActionResult DatHang()
        {
            //Kiem tra dang nhap
            if (Session["TenDN"] == null || Session["TenDN"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "NguoiDung");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "NoiThat");
            }
            //Lay gio hang tu Session
            List<Giohang> lstGiohang = LayGiohang();
            ViewBag.Tongsoluong = TongSoluong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        public ActionResult DatHang(FormCollection collection)
        {
            //Them Don hang
            HOADONDATHANG ddh = new HOADONDATHANG();
            KHACHHANG kh = (KHACHHANG)Session["TenDN"];
            List<Giohang> gh = LayGiohang();
            ddh.MAKH = kh.MAKH;
            ddh.NGAYDAT = DateTime.Now;
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["Ngaygiao"]);
            ddh.NGAYGIAO = DateTime.Parse(ngaygiao);


            ddh.DATHANHTOAN = false;
            db.HOADONDATHANGs.InsertOnSubmit(ddh);
            db.SubmitChanges();
            //Them chi tiet don hang
            foreach (var item in gh)
            {
                CHITIETDONTHANG ctdh = new CHITIETDONTHANG();
                ctdh.MAHDDATHANG= ddh.MAHDDATHANG;
                //ctdh.MADV = item.iMADV;
                ctdh.SL = item.iSL;
                ctdh.DONGIA = (decimal)item.dGIABANHT;
                //db.HOADONDATHANGs.InsertOnSubmit(ctdh);
                
            }
            db.SubmitChanges();
            Session["Giohang"] = null;
                return RedirectToAction("Xacnhandonhang", "Giohang");
        }
    }
}
