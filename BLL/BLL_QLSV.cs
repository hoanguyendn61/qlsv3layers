using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using qlSV_3Layers.DTO;
using qlSV_3Layers.DAL;
namespace qlSV_3Layers.BLL
{
    public class BLL_QLSV
    {
        public delegate bool MyCompare(SV o1, SV o2);
        private static BLL_QLSV _Instance;
        public static BLL_QLSV Instance
        {
            get 
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_QLSV();
                }
                return _Instance;
            }
            private set { }
        }
        private BLL_QLSV()
        {
        }
        public List<LSH> GetListLSH_BLL()
        {
            return DAL_QLSV.Instance.GetListLSH_DAL();
        }
        public List<SV> GetListSV_BLL(int ID_Lop)
        {
            return DAL_QLSV.Instance.GetListSV_DAL(ID_Lop);
        }
        public bool AddSV_BLL(SV s)
        {
            return DAL_QLSV.Instance.AddSV_DAL(s);
        }
        public void EditSV_BLL(SV s)
        {
            DAL_QLSV.Instance.EditSV_DAL(s);
        }
        public void DeleteSV_BLL(SV s)
        {
            DAL_QLSV.Instance.DeleteSV_DAL(s);
        }
        public List<SV> GetListSVDGV(List<string> LMSSV)
        {
            List<SV> data = new List<SV>();
            foreach(string i in LMSSV)
            {
                data.Add(BLL_QLSV.Instance.GetSVByMSSV_BLL(i));
            }
            return data;
        }
        public SV GetSVByMSSV_BLL(string ms)
        {

            return DAL_QLSV.Instance.GetSVByMSSV_DAL(ms);
        }
        public List<SV> GetSVByName_BLL(List<string> LMSSV, string Name)
        {
            List<SV> data = new List<SV>();
            foreach (SV i in GetListSVDGV(LMSSV))
            {
                if (Name != "")
                {
                    if (i.NameSV.Contains(Name))
                    {
                        data.Add(new SV
                        {
                            NameSV = i.NameSV,
                            MSSV = i.MSSV,
                            Gender = i.Gender,
                            NgaySinh = i.NgaySinh,
                            ID_Lop = i.ID_Lop
                        });
                    }
                }
            }
            return data;
        }
        public List<SV> ListSVSort(List<SV> LSV, MyCompare cmp)
        {
            for (int i = 0; i < LSV.Count - 1; ++i)
            {
                for (int j = i + 1; j < LSV.Count; ++j)
                {
                    if (cmp(LSV[i], LSV[j]))
                    {
                        SV temp = LSV[i];
                        LSV[i] = LSV[j];
                        LSV[j] = temp;
                    }
                }
            }
            return LSV;
        }
    }
}
