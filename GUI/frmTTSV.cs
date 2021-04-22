using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using qlSV_3Layers.BLL;
using qlSV_3Layers.DTO;
namespace qlSV_3Layers.GUI
{
    public partial class frmTTSV : Form
    {
        public delegate void MyDel(int id);
        public MyDel d { get; set; }
        private string MSSV;
        public frmTTSV(string ms)
        {
            InitializeComponent();
            LoadCBB();
            if (ms != null)
            {
                MSSV = ms;
                txtMSSV.Enabled = false;
                SetGUI();
            }
        }
        private void SetGUI()
        {
            if (BLL_QLSV.Instance.GetSVByMSSV_BLL(MSSV) != null)
            {
                // Binding
                SV s = BLL_QLSV.Instance.GetSVByMSSV_BLL(MSSV);
                txtMSSV.Text = s.MSSV;
                txtName.Text = s.NameSV;
                dTPNS.Value = s.NgaySinh;
                if (s.Gender == true)
                {
                    rBM.Checked = true;
                }
                else
                {
                    rBF.Checked = true;
                }
                cbLSH.SelectedItem = cbLSH.Items[s.ID_Lop-1];
            }
        }
        private void LoadCBB()
        {
            cbLSH.DataSource = BLL_QLSV.Instance.GetListLSH_BLL();
            cbLSH.DisplayMember = "NameLop";
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            string MSSV = txtMSSV.Text;
            string NameSV = txtName.Text;
            bool Gender = rBM.Checked;
            DateTime BD = Convert.ToDateTime(dTPNS.Value);
            int id = cbLSH.SelectedIndex + 1;
            SV s = new SV(MSSV, NameSV, Gender, BD, id);
            if (BLL_QLSV.Instance.AddSV_BLL(s))
            {
                MessageBox.Show("Thêm SV thành công");
            } else
            {
                BLL_QLSV.Instance.EditSV_BLL(s);
                MessageBox.Show("Edit SV thành công"); 
            }
            d(0);
            this.Dispose();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
