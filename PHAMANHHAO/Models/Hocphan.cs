using PHAMANHHAO.Scripts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PHAMANHHAO.Models
{
    public class Hocphan
    {
        dbTest1DataContext data = new dbTest1DataContext();

        public string MaHP { get; set; }
        [Display(Name = "Tên Học Phần")]

        public string TenHP { get; set; }
        [Display(Name = "Số tín chỉ")]

        public int SoTC { get; set; }
        [Display(Name = "Số Lượng dự kiến")]

        public int SlDuKien { get; set; }



        public Hocphan(string ma)
        {
            MaHP = ma;
            HocPhan hp = data.HocPhans.Single(n => n.MaHP == MaHP);
            TenHP = hp.TenHP;
            SoTC =(int) hp.SoTinChi;

        }
    }
}