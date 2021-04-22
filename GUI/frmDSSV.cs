using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using qlSV_3Layers.DTO;
using qlSV_3Layers.BLL;
using qlSV_3Layers.GUI;
namespace qlSV_3Layers
{
    public partial class frmDSSV : Form
    {
        public frmDSSV()
        {
            InitializeComponent();
            ShowCBB();
        }
        private void Show(int id)
        {
            this.dtgDSSV.DataSource = BLL_QLSV.Instance.GetListSV_BLL(id);
            this.dtgDSSV.Columns["MSSV"].Visible = false;
            this.dtgDSSV.Columns["ID_Lop"].Visible = false;
        }

        public void ShowCBB()
        {
        
            cbLSH.Items.Add(new  CBBItem{ Value = 0, Text = "All" });
            foreach (LSH i in BLL_QLSV.Instance.GetListLSH_BLL())
            {
                cbLSH.Items.Add(new CBBItem
                {
                    Value = i.ID_Lop,
                    Text = i.NameLop
                });
            }
            cbLSH.SelectedIndex = 0;
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            int ID_Lop = ((CBBItem)cbLSH.Items[cbLSH.SelectedIndex]).Value;
            Show(ID_Lop);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmTTSV f = new frmTTSV(null);
            f.d = new frmTTSV.MyDel(this.Show);
            f.ShowDialog();
        }
        
        private void btnSort_Click(object sender, EventArgs e)
        {
            List<string> LMSSV = new List<string>();
            foreach (DataGridViewRow i in dtgDSSV.Rows)
            {
                LMSSV.Add(i.Cells[0].Value.ToString());
            }
            switch (cbSort.SelectedIndex)
            {
                case 0:
                    dtgDSSV.DataSource = BLL_QLSV.Instance.ListSVSort(BLL_QLSV.Instance.GetListSVDGV(LMSSV), SV.Compare_NameAZ);
                    break;
                case 1:
                    dtgDSSV.DataSource = BLL_QLSV.Instance.ListSVSort(BLL_QLSV.Instance.GetListSVDGV(LMSSV), SV.Compare_NameZA);
                    break;
                default:
                    break;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection data = dtgDSSV.SelectedRows;
            if (data.Count == 1)
            {
                string MSSV = data[0].Cells["MSSV"].Value.ToString();
                frmTTSV fTT = new frmTTSV(MSSV);
                fTT.d = new frmTTSV.MyDel(this.Show);
                fTT.ShowDialog();
            }
        }
        private void btnDel_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection data = dtgDSSV.SelectedRows;
            if (data.Count == 1)
            {
                string MSSV = data[0].Cells["MSSV"].Value.ToString();
                BLL_QLSV.Instance.DeleteSV_BLL(BLL_QLSV.Instance.GetSVByMSSV_BLL(MSSV));
            }
            Show(cbLSH.SelectedIndex);
        }
        // SEARCH ko su dung Like % -> Tra ve List BLL SV search tren List<SV>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            int ID_Lop = ((CBBItem)cbLSH.Items[cbLSH.SelectedIndex]).Value;
            Show(ID_Lop);
            List<string> LMSSV = new List<string>();
            foreach (DataGridViewRow i in dtgDSSV.Rows)
            {
                LMSSV.Add(i.Cells[0].Value.ToString());
            }
            dtgDSSV.DataSource = BLL_QLSV.Instance.GetSVByName_BLL(LMSSV, txtSearch.Text);
        }
    }
}
