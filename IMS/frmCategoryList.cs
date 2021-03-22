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
    public partial class frmCategoryList : Form
    {
        DBconnection dbcon = new DBconnection();
        MySqlConnection cn = new MySqlConnection();
        MySqlCommand cm = new MySqlCommand();
        MySqlDataReader dr;
        string title = "C# dev by TIK";

        public frmCategoryList()
        {
            InitializeComponent();
            cn = new MySqlConnection(dbcon.myConnection());
        }


        private void FrmCategoryList_Load(object sender, EventArgs e)
        {
            loadRecordCategory();
        }


        // Save Category and Check if Category exists -- START
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCategory.Text == string.Empty)
                {
                    MessageBox.Show("กรุณาใส่ชื่อหมวดหมู่...", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCategory.Focus();
                    return;
                }
                else
                {
                    cn.Open();
                    cm = new MySqlCommand("select category from tbl_category where category like @category", cn);
                    cm.Parameters.AddWithValue("@category", txtCategory.Text);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        MessageBox.Show(txtCategory.Text + " มีชื่อหมวดหมู่นี้แล้วในระบบ", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCategory.Clear();
                        txtCategory.Focus();
                        dr.Close();
                        cn.Close();
                    }
                    else
                    {
                        dr.Close();
                        cn.Close();

                        cn.Open();
                        cm = new MySqlCommand("insert into tbl_category (category) values (@category)", cn);
                        cm.Parameters.AddWithValue("@category", txtCategory.Text);
                        txtCategory.Clear();
                        txtCategory.Focus();
                        MessageBox.Show(txtCategory.Text + " บันทึกสำเร็จแล้ว!!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        loadRecordCategory();
                    }
                }
            }
            catch
            {

            }
        }// Save Category and Check if Category exists -- END

        // Search by textchange -- START
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            loadRecordCategory();
        }// Search by textchange -- END


        // Column selectd for EDIT or DELETE -- START
        private void DgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCategory.Columns[e.ColumnIndex].Name;
            if (colName == "edit")
            {
                txtCategory.Text = dgvCategory.CurrentRow.Cells[2].Value.ToString();
                btnUpdate.Show();
                btnCancel.Show();
                btnSave.Hide();
                txtCategory.Focus();
            }
            else if (colName == "delete")
            {
                if (MessageBox.Show("คุณต้องการลบข้อมูล ใช่หรือไม่?", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new MySqlCommand("delete from tbl_category where id like @id", cn);
                    cm.Parameters.AddWithValue("@id", dgvCategory.CurrentRow.Cells[1].Value.ToString());
                    // [0]# [1]id [2]category [3]edit [4]delete
                    cm.ExecuteNonQuery();
                    cn.Close();
                    txtCategory.Clear();
                    txtCategory.Focus();
                    MessageBox.Show("ลบข้อมูลสำเร็จแล้ว!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadRecordCategory();
                }

            }
        }// Column selectd for EDIT or DELETE -- END


        // load Category -- START
        private void loadRecordCategory()
        {
            dgvCategory.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new MySqlCommand("select * from tbl_category where category like '%" + txtSearch.Text + "%' order by category ASC", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCategory.Rows.Add(i, dr["id"].ToString(), dr["category"].ToString(), "EDIT", "DELETE");
            }
            dr.Close();
            cn.Close();
            lblCount.Text = dgvCategory.RowCount + " รายการ";
        }// load Category -- END


        // Update Category -- START
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("คุณต้องการบันทึกการแก้ไข ใช่หรือไม่?", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new MySqlCommand("update tbl_category set category = @category where id like @id", cn);
                    cm.Parameters.AddWithValue("@category", txtCategory.Text);
                    cm.Parameters.AddWithValue("@id", dgvCategory.CurrentRow.Cells[1].Value.ToString());
                    cm.ExecuteNonQuery();
                    cn.Close();
                    txtCategory.Clear();
                    txtCategory.Focus();
                    MessageBox.Show(txtCategory.Text + " บันทึกการแก้ไขสำเร็จแล้ว!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancel.Hide();
                    btnUpdate.Hide();
                    btnSave.Show();
                    loadRecordCategory();
                }
            }
            catch
            {
                MessageBox.Show("Error!!!", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }// Update Category -- END


        // Cancel button -- START
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            txtCategory.Clear();
            txtCategory.Focus();
            btnCancel.Hide();
            btnSave.Show();
            btnUpdate.Hide();
        }// Cancel button -- END
    }
}
