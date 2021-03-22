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
    public partial class frmProductList : Form
    {
        DBconnection dbcon = new DBconnection();
        MySqlConnection cn = new MySqlConnection();
        MySqlCommand cm = new MySqlCommand();
        MySqlDataReader dr;
        string title = "C# dev by TIK";

        public frmProductList()
        {
            InitializeComponent();
            cn = new MySqlConnection(dbcon.myConnection());
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            frmAddProduct frm = new frmAddProduct();
            frm.lblContext.Text = "เพิ่มรายการยา เวชภัณฑ์ วัสดุการแพทย์ อุปกรณ์ต่าง ๆ ";
            frm.btnUpdate.Hide();
            frm.Show();
        }

        // load Product -- START
        public void loadProduct()
        {
            try
            {
                dgvProduct.Rows.Clear();
                int i = 0;
                cn.Open();
                cm = new MySqlCommand("select * from tbl_product as p inner join tbl_brand as b on p.bid = b.id inner join tbl_generic as g on p.gid = g.id inner join tbl_category as c on p.cid = c.id inner join tbl_formulation as f on p.fid = f.id inner join tbl_unit as u on p.uid = u.id where brand like '%" + txtSearch.Text + "%' order by brand ASC", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvProduct.Rows.Add(i, dr["id"].ToString(), dr["brand"].ToString(), dr["generic"].ToString(), dr["category"].ToString(), dr["formulation"].ToString(), dr["qty"].ToString(), dr["unit"].ToString(), dr["maxstock"].ToString(), dr["minstock"].ToString(), "ลบ");
                }
                lblCount.Text = dgvProduct.RowCount + " รายการ";
                dr.Close();
                cm.ExecuteNonQuery();
                cn.Close();
            }
            catch
            {

            }
        }// load Product -- END

        private void FrmProductList_Load(object sender, EventArgs e)
        {
            loadProduct();
        }


        // Search by TextChange -- START
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            loadProduct();
        }// Search by TextChange -- END


        // Edit and Delete by Column.Name -- START
        private void DgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            frmAddProduct frmAddProduct = new frmAddProduct();
            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colName == "delete")
            {
                try
                {
                    if (MessageBox.Show("คุณต้องการลบข้อมูล ใช่หรือไม่?", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();
                        cm = new MySqlCommand("delete from tbl_product where id like '" + dgvProduct.CurrentRow.Cells[1].Value.ToString() + "' ", cn);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("ลบข้อมูลสำเร็จแล้ว!", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        loadProduct();
                    }

                }
                catch
                {

                }
            }

        }// Edit and Delete by Column.Name -- END

        private void Button1_Click(object sender, EventArgs e)
        {
            loadProduct();
        }
    }
}
