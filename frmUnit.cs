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
    public partial class frmUnit : Form
    {
        DBconnection dbcon = new DBconnection();
        MySqlConnection cn = new MySqlConnection();
        MySqlCommand cm = new MySqlCommand();
        MySqlDataReader dr;
        string title = "C# dev by TIK";

        public frmUnit()
        {
            InitializeComponent();
            cn = new MySqlConnection(dbcon.myConnection());
        }

        private void FrmUnit_Load(object sender, EventArgs e)
        {
            loadUnitList();
        }


        // load Unit List -- START
        private void loadUnitList()
        {
            dgvUnit.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new MySqlCommand("select * from tbl_unit WHERE unit Like '%" + txtSearch.Text + "%' order by unit ASC", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvUnit.Rows.Add(i, dr["id"].ToString(), dr["unit"].ToString(), "EDIT", "DELETE");
            }
            dr.Close();
            cn.Close();
            lblCount.Text = "ข้อมูลทั้งหมด " + dgvUnit.RowCount + " รายการ";
        }// load Unit List -- END


        // Search by Textchanged -- START
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            loadUnitList();
        }// Search by Textchanged -- END

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            txtUnit.Clear();
            txtUnit.Focus();
            btnUpdate.Hide();
            btnSave.Show();
            btnCancel.Hide();
        }


        // Save and Check if its exists -- START
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUnit.Text == string.Empty)
                {
                    MessageBox.Show("กรุณาใส่ชื่อหน่วยนับ...", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUnit.Focus();
                    return;
                }

                cn.Open();
                cm = new MySqlCommand("select unit from tbl_unit where unit like @unit", cn);
                cm.Parameters.AddWithValue("@unit", txtUnit.Text);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    MessageBox.Show(txtUnit.Text + " มีข้อมูลนี้แล้วในระบบ กรุณาใส่ค่าใหม่...", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUnit.Clear();
                    txtUnit.Focus();
                    dr.Close();
                    cn.Close();
                }
                else
                {
                    dr.Close();
                    cn.Close();

                    cn.Open();
                    cm = new MySqlCommand("insert into tbl_unit (unit) values (@unit)", cn);
                    cm.Parameters.AddWithValue("@unit", txtUnit.Text);
                    txtUnit.Clear();
                    txtUnit.Focus();
                    MessageBox.Show(txtUnit.Text + " บันทึกสำเร็จแล้ว!!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    loadUnitList();
                }
            }
            catch
            {

            }
        }// Save and Check if its exists -- END


        // Edit and Delete by Column.Name -- START
        private void DgvUnit_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvUnit.Columns[e.ColumnIndex].Name;
            if (colName == "edit")
            {
                txtUnit.Text = dgvUnit.CurrentRow.Cells[2].Value.ToString();
                txtUnit.Focus();
                btnSave.Hide();
                btnUpdate.Show();
                btnCancel.Show();
            }
            else if (colName == "delete")
            {
                if (MessageBox.Show("คุณต้องการลบข้อมูลนี้ ใช่หรือไม่?", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new MySqlCommand("delete from tbl_unit where id like '" + dgvUnit.CurrentRow.Cells[1].Value.ToString() + "' ", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("ลบข้อมูลสำเร็จแล้ว!", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    loadUnitList();
                }
            }
        }// Edit and Delete by Column.Name -- END


        // Update Unit -- START
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("คุณต้องการแก้ไขข้อมูล ใช่หรือไม่?", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new MySqlCommand("update tbl_unit set unit = @unit where id like '" + dgvUnit.CurrentRow.Cells[1].Value.ToString() + "' ", cn);
                    cm.Parameters.AddWithValue("@unit", txtUnit.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("บันทึกการแก้ไขข้อมูลสำเร็จแล้ว!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUnit.Clear();
                    txtUnit.Focus();
                    btnCancel.Hide();
                    btnSave.Show();
                    btnUpdate.Hide();
                    loadUnitList();
                }
                else
                {
                    MessageBox.Show("Cannot update!!!", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch
            {

            }
        }// Update Unit -- END
    }
}
