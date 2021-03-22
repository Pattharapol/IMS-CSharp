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
    public partial class frmSale : Form
    {
        DBconnection dbcon = new DBconnection();
        MySqlConnection cn = new MySqlConnection();
        MySqlCommand cm = new MySqlCommand();
        MySqlDataReader dr;
        string title = "C# dev by TIK";
        MySqlDataAdapter da;

        public frmSale()
        {
            InitializeComponent();
            cn = new MySqlConnection(dbcon.myConnection());
            dt1.Value = DateTime.Now;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }

        private void FrmSale_Load(object sender, EventArgs e)
        {
            loadUserList();
        }

        // load User -- START
        private void loadUserList()
        {
            DataTable dt = new DataTable("name");
            cn.Open();
            cm = new MySqlCommand("select name from tbl_user", cn);
            dr = cm.ExecuteReader();
            dt.Load(dr);
            txtSoldby.DataSource = dt;
            txtSoldby.DisplayMember = "name";
            dr.Close();
            cm.ExecuteNonQuery();
            cn.Close();
            txtSoldby.SelectedItem = null;
        }// load User -- END

        // load Product -- START
        public void loadProduct()
        {
            try
            {
                dgv1.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new MySqlCommand("select * from tbl_product as p inner join tbl_brand as b on p.bid = b.id inner join tbl_generic as g on p.gid = g.id inner join tbl_category as c on p.cid = c.id inner join tbl_formulation as f on p.fid = f.id inner join tbl_unit as u on p.uid = u.id where brand like '" + txtSearch.Text + "%' or generic like '" + txtSearch.Text + "%' order by brand ASC", cn);
                // [0]id, [1]bid, [2]gid, [3]cid, [4]fid, [5]qty, [6]uid, [7]maxstock, [8]minstock, [9]id, [10]brand, [11]id, [12]generic, [13]id, [14]category, [15]id, [16]formulation, [17]id, [18]unit
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgv1.Rows.Add(i, dr["id"].ToString(), dr["brand"].ToString(), dr["generic"].ToString(), dr["category"].ToString(), dr["formulation"].ToString(), dr["qty"].ToString(), dr["unit"].ToString(), dr["maxstock"].ToString(), "เบิก");
                }
                dr.Close();
                cm.ExecuteNonQuery();
                cn.Close();
            }
            catch
            {

            }
        }// load Product -- END


        // search by textchanged -- START
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            loadProduct();
        }// search by textchanged -- END


        // Pass value from dgv1 to dgv2 -- START
        private void Dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (txtSoldby.Text == string.Empty)
            {
                MessageBox.Show("Please input Sold By...", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoldby.Focus();
                return;
            }
            else
            {
                string colName = dgv1.Columns[e.ColumnIndex].Name;
                if (colName == "ADD")
                {
                    // [0]i, [1]id, [2]brand, [3]generic, [4]category, [5]formulation, [6]qty, [7]unit, [8]maxstock
                    dgv2.Rows.Add(dgv2.RowCount + 1, dgv1.CurrentRow.Cells[1].Value.ToString(), dgv1.CurrentRow.Cells[2].Value.ToString(), dgv1.CurrentRow.Cells[3].Value.ToString(), dgv1.CurrentRow.Cells[4].Value.ToString(), dgv1.CurrentRow.Cells[5].Value.ToString(), dgv1.CurrentRow.Cells[6].Value.ToString(), dgv1.CurrentRow.Cells[7].Value.ToString(), dgv1.CurrentRow.Cells[8].Value.ToString(), "0");
                }
            }
        }// Pass value from dgv1 to dgv2 -- END


        // remove row -- START
        private void Dgv2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgv2.Columns[e.ColumnIndex].Name;
            if (colName == "remove")
            {
                foreach (DataGridViewRow item in this.dgv2.SelectedRows)
                {
                    dgv2.Rows.RemoveAt(item.Index);
                }
            }

        }// remove row -- END

        // load Sales History -- START
        private void loadSalesHistory()
        {
            try
            {
                dgvSalesHistory.Rows.Clear();
                string date1 = dateTimePicker1.Value.ToString("dd/MM/yyyy");
                string date2 = dateTimePicker2.Value.ToString("dd/MM/yyyy");
                int i = 0;
                cn.Open();
                cm = new MySqlCommand("SELECT * FROM `tbl_sale` as s INNER JOIN tbl_product as p on s.pid = p.id INNER JOIN tbl_brand as b on p.bid = b.id inner JOIN tbl_category as c on p.cid = c.id INNER JOIN tbl_formulation as f on p.fid = f.id INNER join tbl_generic as g on p.gid = g.id INNER JOIN tbl_unit as u on p.uid = u.id where sdate between '" + date1 + "' and '" + date2 + "' order by s.id", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvSalesHistory.Rows.Add(i, dr[0].ToString(), dr["soldby"].ToString(), dr["brand"].ToString(), dr["generic"].ToString(), dr["category"].ToString(), dr["formulation"].ToString(), dr["qty"].ToString(), dr["unit"].ToString(), dr["sdate"].ToString());
                    // i, id, recievedby, brand, generic, category, formulation, stockin qty, unit <-(array)
                }
                dr.Close();
                cm.ExecuteNonQuery();
                cn.Close();
            }
            catch
            {

            }
        }// load Sales History -- END


        // Save Sale -- START
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSoldby.Text == string.Empty)
                {
                    MessageBox.Show("Please input Sold by...", title);
                    return;
                }

                if (dgv2.Rows.Count - 1 < 0)
                {
                    MessageBox.Show("Please do StockIn...", title);
                    return;
                }

                for (int i = 0; i < dgv2.Rows.Count; i++)
                {
                    if (Int32.Parse(dgv2.Rows[i].Cells[9].Value.ToString()) > Int32.Parse(dgv2.Rows[i].Cells[6].Value.ToString()))
                    {
                        MessageBox.Show("Your " + dgv2.Rows[i].Cells[2].Value.ToString() + " remaining is " + dgv2.Rows[i].Cells[6].Value.ToString() + " Please input less than your Stock!!!", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }


                for (int i = 0; i < dgv2.Rows.Count; i++)
                {
                    if (dgv2.Rows[i].Cells[9].Value.ToString() == "0" || dgv2.Rows[i].Cells[9].Value.ToString() == string.Empty)
                    {
                        MessageBox.Show("ข้อมูลแถวที่ " + (i + 1) + " มีค่าเป็น 0 กรุณากรอกจำนวนด้วยครับ...", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                string sdate = dt1.Value.ToString("dd/MM/yyyy");
                for (int i = 0; i < dgv2.Rows.Count; i++)
                {
                    cn.Open();
                    cm = new MySqlCommand("insert into tbl_sale (soldby, pid, brand, generic, formulation, category, unit, qty, sdate) values (@soldby, @pid, @brand, @generic, @formulation, @category, @unit, @qty, @sdate)", cn);
                    cm.Parameters.AddWithValue("@soldby", txtSoldby.Text);
                    cm.Parameters.AddWithValue("@pid", dgv2.Rows[i].Cells[1].Value.ToString());
                    cm.Parameters.AddWithValue("@brand", dgv2.Rows[i].Cells[2].Value.ToString());
                    cm.Parameters.AddWithValue("@generic", dgv2.Rows[i].Cells[3].Value.ToString());
                    cm.Parameters.AddWithValue("@formulation", dgv2.Rows[i].Cells[5].Value.ToString());
                    cm.Parameters.AddWithValue("@category", dgv2.Rows[i].Cells[4].Value.ToString());
                    cm.Parameters.AddWithValue("@unit", dgv2.Rows[i].Cells[7].Value.ToString());
                    cm.Parameters.AddWithValue("@qty", dgv2.Rows[i].Cells[9].Value.ToString());
                    cm.Parameters.AddWithValue("@sdate", sdate);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    cn.Open();
                    cm = new MySqlCommand("update tbl_product set qty = qty - '" + dgv2.Rows[i].Cells[9].Value.ToString() + "' where id like '" + dgv2.Rows[i].Cells[1].Value.ToString() + "' ", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                }

                MessageBox.Show("บันทึกข้อมูลสำเร็จแล้ว!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgv1.Rows.Clear();
                dgv2.Rows.Clear();
                txtSearch.Clear();
                txtSoldby.SelectedItem = null;
                txtSearch.Focus();
            }
            catch
            {
                MessageBox.Show("Exception Error", title);
            }
        }// Save Sale -- END


        // Search SalesHistory -- START
        private void BtnGo_Click(object sender, EventArgs e)
        {
            loadSalesHistory();
        }// Search SalesHistory -- END
    }
}
