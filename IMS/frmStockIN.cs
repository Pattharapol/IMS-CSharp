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
    public partial class frmStockIN : Form
    {
        DBconnection dbcon = new DBconnection();
        MySqlConnection cn = new MySqlConnection();
        MySqlCommand cm = new MySqlCommand();
        MySqlDataReader dr;
        string title = "C# dev by TIK";

        public frmStockIN()
        {
            InitializeComponent();
            cn = new MySqlConnection(dbcon.myConnection());
            this.ActiveControl = txtSearch;
            dt1.Value = DateTime.Now;
        }

        private void FrmStockIN_Load(object sender, EventArgs e)
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
            txtRecievedby.DataSource = dt;
            txtRecievedby.DisplayMember = "name";
            dr.Close();
            cm.ExecuteNonQuery();
            cn.Close();
            txtRecievedby.SelectedItem = null;
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
                    dgv1.Rows.Add(i, dr["id"].ToString(), dr["brand"].ToString(), dr["generic"].ToString(), dr["category"].ToString(), dr["formulation"].ToString(), dr["qty"].ToString(), dr["unit"].ToString(), dr["maxstock"].ToString(), "ADD");
                    //[0]i, [1]id, [2]brand, [3]generic, [4]category, [5]formulation, [6]qty, [7]unit, [8]maxstock
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
        }// search by textchanged -- START


        // Pass value from dgv1 to dgv2 -- START
        private void Dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (txtRecievedby.Text == string.Empty)
            {
                MessageBox.Show("กรุณาใส่ชื่อผู้บันทึก...", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRecievedby.Focus();
                return;
            }
            else
            {
                string colName = dgv1.Columns[e.ColumnIndex].Name;
                if (colName == "ADD")
                {
                    dgv2.Rows.Add(dgv1.CurrentRow.Cells[0].Value.ToString(), dgv1.CurrentRow.Cells[1].Value.ToString(), dgv1.CurrentRow.Cells[2].Value.ToString(), dgv1.CurrentRow.Cells[3].Value.ToString(), dgv1.CurrentRow.Cells[4].Value.ToString(), dgv1.CurrentRow.Cells[5].Value.ToString(), dgv1.CurrentRow.Cells[6].Value.ToString(), dgv1.CurrentRow.Cells[7].Value.ToString(), dgv1.CurrentRow.Cells[8].Value.ToString(), "0", "ลบ");
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

        // load StockIn History -- START
        private void loadStockInHistory()
        {
            try
            {
                dgvStockInHistory.Rows.Clear();
                string date1 = dateTimePicker1.Value.ToString("dd/MM/yyyy");
                string date2 = dateTimePicker2.Value.ToString("dd/MM/yyyy");
                int i = 0;
                cn.Open();
                cm = new MySqlCommand("SELECT * FROM tbl_stockin where sdate between '" + date1 + "' and '" + date2 + "' ", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvStockInHistory.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr["qty"].ToString(), dr["unit"].ToString(), dr["sdate"].ToString());

                }
                dr.Close();
                cm.ExecuteNonQuery();
                cn.Close();
            }
            catch
            {

            }
        }// load StockIn History -- END


        // Search StockIn History by Date -- START
        private void BtnGo_Click(object sender, EventArgs e)
        {
            loadStockInHistory();
        }// Search StockIn History by Date -- END


        // Save StockIn -- START
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRecievedby.Text == string.Empty)
                {
                    MessageBox.Show("กรุณาใส่ชื่อผู้บันทึก...", title);
                    return;
                }

                // ถ้าไม่มีแถว โปรดทำรายการ
                if (dgv2.RowCount - 1 < 0)
                {
                    MessageBox.Show("กรุณาทำรายการก่อนบักทึก...!!!", title);
                    return;
                }

                // เช็ค ถ้าจำนวนที่เบิก + กับของเก่า แล้วเกิน Max ที่ตั้งไว้
                for (int i = 0; i < dgv2.Rows.Count; i++)
                {
                    int StockIN = Int32.Parse(dgv2.Rows[i].Cells[9].Value.ToString());
                    int Stock = Int32.Parse(dgv2.Rows[i].Cells[6].Value.ToString());
                    int MaxStock = Int32.Parse(dgv2.Rows[i].Cells[8].Value.ToString());
                    if ((Stock + StockIN) > MaxStock)
                    {
                        MessageBox.Show("คุณสามารถเบิก " + dgv2.Rows[i].Cells[2].Value.ToString() + " ได้ไม่เกิน " + (MaxStock - Stock) + " !!!", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }


                // ถ้าเป็น 0 หรือ เป็นค่าว่าง 
                for (int j = 0; j < dgv2.RowCount; j++)
                {
                    if (dgv2.Rows[j].Cells[9].Value.ToString() == "0" || dgv2.Rows[j].Cells[9].Value.ToString() == string.Empty)
                    {
                        MessageBox.Show("ในข้อมูลแถวที่ " + (j + 1) + " มีค่าเป็น 0 กรุณากรอกจำนวนด้วยครับ", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                string sdate = dt1.Value.ToString("dd/MM/yyyy");
                for (int i = 0; i < dgv2.RowCount; i++)
                {
                    cn.Open();
                    cm = new MySqlCommand("insert into tbl_stockin (recievedby, pid, brand, generic, formulation, category, unit, qty, sdate) values (@recievedby, @pid, @brand, @generic, @formulation, @category, @unit, @qty, @sdate)", cn);
                    cm.Parameters.AddWithValue("@recievedby", txtRecievedby.Text);
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
                    cm = new MySqlCommand("update tbl_product set qty = qty + '" + dgv2.Rows[i].Cells[9].Value.ToString() + "' where id like '" + dgv2.Rows[i].Cells[1].Value.ToString() + "' ", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();


                }


                //MySqlDataAdapter da;
                //DataSet ds = new DataSet();
                //cn.Open();
                //report.Load(@"C:\Users\IT\source\repos\ER_Inventory v.1\ER_Inventory v.1\my_rpt.rpt");

                //da = new MySqlDataAdapter("select * from tbl_stockin where sdate like '" + dt1.Value.ToString("yyyy-MM-dd") + "' ", cn);
                //da.Fill(ds, "tbl_stockin");
                //report.SetDataSource(ds);

                //PrintDialog printDialog = new PrintDialog();
                //if (printDialog.ShowDialog() == DialogResult.OK)
                //{
                //    CrystalDecisions.CrystalReports.Engine.ReportDocument reportDocument = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                //    reportDocument.Load(@"C:\Users\IT\source\repos\ER_Inventory v.1\ER_Inventory v.1\my_rpt.rpt");
                //    reportDocument.PrintOptions.PrinterName = printDialog.PrinterSettings.PrinterName;
                //    reportDocument.PrintToPrinter(printDialog.PrinterSettings.Copies, printDialog.PrinterSettings.Collate, printDialog.PrinterSettings.FromPage, printDialog.PrinterSettings.ToPage);
                //}

                MessageBox.Show("บันทึกข้อมูลสำเร็จแล้ว!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                dgv1.Rows.Clear();
                dgv2.Rows.Clear();
                //txtSearch.Clear();
                txtRecievedby.SelectedItem = null;
                //txtSearch.Focus();


            }
            catch
            {
                MessageBox.Show("Exception Error", title);
            }
        }// Save StockIn -- END
    }
}
