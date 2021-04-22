using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using qlSV_3Layers.DTO;
namespace qlSV_3Layers.DAL
{
    public class DAL_QLSV
    {
        private static DAL_QLSV _Instance;
        public static DAL_QLSV Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DAL_QLSV();
                }
                return _Instance;
            }
            private set { }
        }
        private DAL_QLSV()
        {
        }
        public List<SV> GetListSV_DAL()
        {
            List<SV> data = new List<SV>();
            //string query = "Select * from dbo.SV";
            string query = "SELECT MSSV, NameSV, Gender, NS, LopSH.NameLop, SV.ID_Lop FROM dbo.SV, dbo.LopSH WHERE SV.ID_Lop = LopSH.ID_Lop";
            foreach(DataRow i in DBHelper.Instance.GetRecords(query).Rows)
            {
                data.Add(GetSV(i));   
                //SV sv = new SV(i);
                //data.Add(sv);
            }
            return data;
        }
        public List<SV> GetListSV_DAL(int ID_Lop)
        {
            List<SV> data = new List<SV>();
            if (ID_Lop == 0)
            {
                data = GetListSV_DAL();
            }
            else
            {
                foreach (SV i in GetListSV_DAL())
                {
                        if (i.ID_Lop == ID_Lop)
                        {
                        data.Add(new SV
                        {
                            NameSV = i.NameSV,
                            MSSV = i.MSSV,
                            Gender = i.Gender,
                            NgaySinh = i.NgaySinh,
                            TenLop = i.TenLop,
                            ID_Lop = i.ID_Lop
                        });
                        }
                }
            }
            return data;
        }
        public List<LSH> GetListLSH_DAL()
        {
            List<LSH> data = new List<LSH>();
            string query = "SELECT * FROM dbo.LopSH";
            foreach (DataRow i in DBHelper.Instance.GetRecords(query).Rows)
            {
                data.Add(GetLSH(i));
            }
            return data;
        }
        public LSH GetLSH(DataRow i)
        {
            return new LSH
            {
                ID_Lop = Convert.ToInt32(i["ID_Lop"].ToString()),
                NameLop = i["NameLop"].ToString()
            };
        }
        public SV GetSV(DataRow i)
        {
            return new SV
            {
                MSSV = i["MSSV"].ToString(),
                NameSV = i["NameSV"].ToString(),
                Gender = Convert.ToBoolean(i["Gender"].ToString()),
                NgaySinh = Convert.ToDateTime(i["NS"]),
                TenLop = i["NameLop"].ToString(),
                ID_Lop = Convert.ToInt32(i["ID_Lop"].ToString())
                //NameSV = i["NameSV"].ToString(),
                //Gender = Convert.ToBoolean(i["Gender"].ToString()),
                //NgaySinh = Convert.ToDateTime(i["NS"]),
                //TenLop = i["NameLop"].ToString()
            };
        }
        public bool AddSV_DAL(SV s)
        {
            string sqlFormattedDate = s.NgaySinh.ToString("yyyy-MM-dd");
            string query = "INSERT INTO dbo.SV VALUES ('"+s.MSSV+"', N'"+s.NameSV+ "', '" + s.Gender + "', '" + sqlFormattedDate + "' , " + s.ID_Lop+")";
            return DBHelper.Instance.ExecuteDB(query);
        }
        public void EditSV_DAL(SV s)
        {
            string sqlFormattedDate = s.NgaySinh.ToString("yyyy-MM-dd");
            string sql = String.Format("Update dbo.SV SET NameSV = N'" + s.NameSV + "', Gender='" + s.Gender + "', NS='" + sqlFormattedDate + "', ID_Lop = " + s.ID_Lop + " where MSSV ='" + s.MSSV + "'");
            DBHelper.Instance.ExecuteDB(sql);
        }
        public void DeleteSV_DAL(SV s)
        {
            string sql = "Delete dbo.SV where MSSV ='" + s.MSSV + "'";
            DBHelper.Instance.ExecuteDB(sql);
        }
        public SV GetSVByMSSV_DAL(string ms)
        {
            //string query = "SELECT * from SV WHERE MSSV = '"+ms+"'";
            string query = "SELECT MSSV, NameSV, Gender, NS, LopSH.NameLop, SV.ID_Lop FROM dbo.SV, dbo.LopSH WHERE SV.ID_Lop = LopSH.ID_Lop AND MSSV = '" + ms + "'"; 
            return GetSV(DBHelper.Instance.GetRecords(query).Rows[0]);
        }
    }
}
