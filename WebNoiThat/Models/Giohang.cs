using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNoiThat.Models;

namespace WebNoiThat.Models
{
    public class Giohang
    {
        DbNoiThatDataContext db = new DbNoiThatDataContext();
        public int iMADV { set; get; }
        public string sTENDV { set; get; }
        public string sHIANH { set; get; }
        public Double dGIABANHT { set; get; }
        public int iSL { set; get; }
        public Double dThanhtien
        {
            get { return iSL * dGIABANHT; }
        }
        public Giohang(int MADV)
        {
            iMADV = MADV;
            DoVatNT dv = db.DoVatNTs.Single(n => n.MADV == iMADV);
            sTENDV = dv.TENDV;
            sHIANH = dv.HIANH;
            dGIABANHT = double.Parse(dv.GIABANHT.ToString());
            iSL = 1;
        }
    }
}