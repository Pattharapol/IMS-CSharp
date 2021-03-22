using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS
{
    public partial class frmLiveRecord : Form
    {
        DBconnection dbcon = new DBconnection();
        MySqlConnection cn = new MySqlConnection();
        MySqlCommand cm = new MySqlCommand();
        MySqlDataReader dr;
        string title = "C# dev by TIK";

        public frmLiveRecord()
        {
            InitializeComponent();
            cn = new MySqlConnection(dbcon.myConnection());
        }

        private void FrmLiveRecord_Load(object sender, EventArgs e)
        {
            loadRecords();
        }


        // load Records -- START
        private void loadRecords()
        {
            try
            {
                dgvRecord.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new MySqlCommand("select * from tbl_product as p inner join tbl_brand as b on p.bid = b.id inner join tbl_generic as g on p.gid = g.id inner join tbl_category as c on p.cid = c.id inner join tbl_formulation as f on p.fid = f.id inner join tbl_unit as u on p.uid = u.id ", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvRecord.Rows.Add(i, dr[0].ToString(), dr["brand"].ToString(), dr["generic"].ToString(), dr["category"].ToString(), dr["formulation"].ToString(), dr["qty"].ToString(), dr["unit"].ToString(), dr["maxstock"].ToString());
                    // [0]i, [1]id, [2]brand, [3]generic, [4]category, [5]formulation
                }
                dr.Close();
                cm.ExecuteNonQuery();
                cn.Close();

                lblCount.Text = dgvRecord.RowCount + " record(s) found";
            }
            catch
            {

            }
        }// load Records -- END


        // Search stock -> find by Percent -- START
        private void TxtPercent_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvRecord.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new MySqlCommand("select * from tbl_product as p inner join tbl_brand as b on p.bid = b.id inner join tbl_generic as g on p.gid = g.id inner join tbl_category as c on p.cid = c.id inner join tbl_formulation as f on p.fid = f.id inner join tbl_unit as u on p.uid = u.id where ((qty*100)/maxstock) <= '" + txtPercent.Text + "' ", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvRecord.Rows.Add(i, dr[0].ToString(), dr["brand"].ToString(), dr["generic"].ToString(), dr["category"].ToString(), dr["formulation"].ToString(), dr["qty"].ToString(), dr["unit"].ToString(), dr["maxstock"].ToString());
                    // [0]i, [1]id, [2]brand, [3]generic, [4]category, [5]formulation, [6]qty, [7]unit, [8]maxstock
                }
                dr.Close();
                cm.ExecuteNonQuery();
                cn.Close();

                lblCount.Text = "ข้อมูลทั้งหมด " + dgvRecord.RowCount + " รายการ";
            }
            catch
            {

            }
        }// Search stock find by Percent -- END
    }
}
