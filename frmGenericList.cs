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
    public partial class frmGenericList : Form
    {
        DBconnection dbcon = new DBconnection();
        MySqlConnection cn = new MySqlConnection();
        MySqlCommand cm = new MySqlCommand();
        MySqlDataReader dr;
        string title = "C# dev by TIK";

        public frmGenericList()
        {
            InitializeComponent();
            cn = new MySqlConnection(dbcon.myConnection());
        }

        private void FrmGenericList_Load(object sender, EventArgs e)
        {
            loadGenericList();
        }


        // load Generic -- START
        private void loadGenericList()
        {
            dgvGeneric.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new MySqlCommand("select * from tbl_generic WHERE generic Like '%" + txtSearch.Text + "%' order by generic ASC", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvGeneric.Rows.Add(i, dr["id"].ToString(), dr["generic"].ToString(), "EDIT", "DELETE");
            }
            dr.Close();
            cn.Close();
            lblCount.Text = dgvGeneric.RowCount + " record(s) found";
        }// load Generic -- END


        // Edit and Delete by Column.Name -- START
        private void DgvGeneric_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvGeneric.Columns[e.ColumnIndex].Name;
            if (colName == "edit")
            {
                txtGeneric.Text = dgvGeneric.CurrentRow.Cells[2].Value.ToString();
                txtGeneric.Focus();
                btnSave.Hide();
                btnUpdate.Show();
                btnCancel.Show();
            }
            else if (colName == "delete")
            {
                if (MessageBox.Show("คุณต้องการลบข้อมูลนี้ ใช่หรือไม่?", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new MySqlCommand("delete from tbl_generic where id like '" + dgvGeneric.CurrentRow.Cells[1].Value.ToString() + "' ", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("ลบข้อมูลสำเร็จแล้ว!", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    loadGenericList();
                }
            }
        }// Edit and Delete by Column.Name -- END


        // Update Generic -- START
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("คุณต้องการแก้ไขข้อมูลนี้ ใช่หรือไม่?", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new MySqlCommand("update tbl_generic set generic = @generic where id like '" + dgvGeneric.CurrentRow.Cells[1].Value.ToString() + "' ", cn);
                    cm.Parameters.AddWithValue("@generic", txtGeneric.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("บันทึกการแก้ไขข้อมูลสำเร็จแล้ว!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGeneric.Clear();
                    txtGeneric.Focus();
                    btnUpdate.Hide();
                    btnSave.Show();
                    btnCancel.Hide();
                    loadGenericList();
                }
                else
                {
                    MessageBox.Show("Cannot update!!!", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch
            {

            }
        }// Update Generic -- END


        // Save and Check if its exists -- START
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtGeneric.Text == string.Empty)
                {
                    MessageBox.Show("กรุณาใส่ชื่อสามัญ...", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGeneric.Focus();
                    return;
                }

                cn.Open();
                cm = new MySqlCommand("select generic from tbl_generic where generic like @generic", cn);
                cm.Parameters.AddWithValue("@generic", txtGeneric.Text);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    MessageBox.Show(txtGeneric.Text + " มีชื่อสามัญนี้แล้วในระบบ...", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGeneric.Clear();
                    txtGeneric.Focus();
                    dr.Close();
                    cn.Close();
                }
                else
                {
                    dr.Close();
                    cn.Close();

                    cn.Open();
                    cm = new MySqlCommand("insert into tbl_generic (generic) values (@generic)", cn);
                    cm.Parameters.AddWithValue("@generic", txtGeneric.Text);
                    txtGeneric.Clear();
                    txtGeneric.Focus();
                    MessageBox.Show(txtGeneric.Text + " บันทึกข้อมูลเรียบร้อยแล้ว!!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    loadGenericList();
                }
            }
            catch
            {

            }
        }// Save and Check if its exists -- END


        // btnCancel -- START
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            txtGeneric.Clear();
            txtGeneric.Focus();
            btnCancel.Hide();
            btnSave.Show();
            btnUpdate.Hide();
        }// btnCancel -- END


        // Search by textchange -- START
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            loadGenericList();
        }// Search by textchange -- END
    }
}
