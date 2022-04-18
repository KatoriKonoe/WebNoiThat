using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNoiThat.Models;
using System.Configuration;

//using PagedList;
//using PagedList.Mvc;

namespace WebNoiThat.Controllers
{
    public class NoiThatController : Controller
    {
        DbNoiThatDataContext db = new DbNoiThatDataContext();
        // GET: NoiThat
        public ActionResult Index(/*int? page*/)
        {
            
            //int pagesize = 3;
            //int pageNum = (page ?? 1);
            List<DoVatNT> domoi = LaydoNT(8);
            return View(domoi/*.ToPagedList(pageNum,pagesize)*/);
        }
        private List<DoVatNT> LaydoNT(int count)
        {
            return db.DoVatNTs.OrderByDescending(a => a.NGAYCAPNHAP).Take(count).ToList();
        }
        public ActionResult NCC()
        {
            var nhacc = from ncc in db.NHACUNGCAPs select ncc;

            return View(nhacc);
        }
        public ActionResult TheLoai()
        {
            var theloai = from tl in db.THELOAIs select tl;

            return View(theloai);
        }
        public ActionResult Detail(int id)
        {
            var noithat = from nt in db.DoVatNTs where nt.MADV == id select nt;

            return View(noithat.Single());
        }

        public ActionResult Liehe()
        {


            return View();
        }

    }
}