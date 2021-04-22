using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qlSV_3Layers.DTO
{
    public class SV
    {
        public string MSSV { get; set; }    
        public string NameSV { get; set; }
        public bool Gender { get; set; }
        public DateTime NgaySinh { get; set; }
        public string TenLop { get; set; }
        public int ID_Lop { get; set; }
        public SV() { }
        public SV(string ms, string n, bool gender, DateTime ns, int id)
        {
            MSSV = ms;
            NameSV = n;
            Gender = gender;
            NgaySinh = ns;
            ID_Lop = id;
        }
        public static bool Compare_NameZA(SV o1, SV o2)
        {
            if (string.Compare(o1.NameSV, o2.NameSV) < 0)
                return true;
            else
                return false;
        }
        public static bool Compare_NameAZ(SV o1, SV o2)
        {
            if (string.Compare(o1.NameSV, o2.NameSV) > 0)
                return true;
            else
                return false;
        }
    }
}
