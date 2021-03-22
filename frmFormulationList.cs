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
    public partial class frmFormulationList : Form
    {
        DBconnection dbcon = new DBconnection();
        MySqlConnection cn = new MySqlConnection();
        MySqlCommand cm = new MySqlCommand();
        MySqlDataReader dr;
        string title = "C# dev by TIK";

        public frmFormulationList()
        {
            InitializeComponent();
            cn = new MySqlConnection(dbcon.myConnection());
        }

        private void FrmFormulationList_Load(object sender, EventArgs e)
        {
            loadFormulationList();
        }


        // Update Formulation -- START
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("คุณต้องการบันทึกการแก้ไขข้อมูล ใช่หรือไม่?", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new MySqlCommand("update tbl_formulation set formulation = @formulation where id like '" + dgvFormulation.CurrentRow.Cells[1].Value.ToString() + "' ", cn);
                    cm.Parameters.AddWithValue("@formulation", txtformulation.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("บันทึกการแก้ไขข้อมูลสำเร็จแล้ว!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtformulation.Clear();
                    txtformulation.Focus();
                    btnUpdate.Hide();
                    btnSave.Show();
                    btnCancel.Hide();
                    loadFormulationList();
                }
                else
                {
                    MessageBox.Show("Cannot update!!!", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch
            {

            }
        }// Update Formulation -- END


        // load Formulation -- START
        private void loadFormulationList()
        {
            dgvFormulation.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new MySqlCommand("select * from tbl_formulation WHERE formulation Like '%" + txtSearch.Text + "%' order by formulation ASC", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvFormulation.Rows.Add(i, dr["id"].ToString(), dr["formulation"].ToString(), "EDIT", "DELETE");
            }
            dr.Close();
            cn.Close();
            lblCount.Text = dgvFormulation.RowCount + " รายการ";
        }// load Formulation -- END


        // Search by textchange -- START
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            loadFormulationList();
        }// Search by textchange -- END


        // Edit and Delete by Column.Name -- START
        private void DgvFormulation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvFormulation.Columns[e.ColumnIndex].Name;
            if (colName == "edit")
            {
                txtformulation.Text = dgvFormulation.CurrentRow.Cells[2].Value.ToString();
                txtformulation.Focus();
                btnSave.Hide();
                btnUpdate.Show();
                btnCancel.Show();
            }
            else if (colName == "delete")
            {
                if (MessageBox.Show("คุณต้องการลบข้อมูลนี้ ใช่หรือไม่?", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new MySqlCommand("delete from tbl_formulation where id like '" + dgvFormulation.CurrentRow.Cells[1].Value.ToString() + "' ", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("ลบข้อมูลสำเร็จแล้ว!", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    loadFormulationList();
                }
            }
        }// Edit and Delete by Column.Name -- END


        // btnCancel -- START
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            txtformulation.Clear();
            txtformulation.Focus();
            btnCancel.Hide();
            btnSave.Show();
            btnUpdate.Hide();
        }// btnCancel -- END


        // Save and Check if its exists -- START
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtformulation.Text == string.Empty)
                {
                    MessageBox.Show("กรุณาใส่ค่าความเข้มข้น...", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtformulation.Focus();
                    return;
                }
                else if (txtformulation.Text != string.Empty)
                {
                    cn.Open();
                    cm = new MySqlCommand("select formulation from tbl_formulation where formulation like @formulation", cn);
                    cm.Parameters.AddWithValue("@formulation", txtformulation.Text);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        MessageBox.Show(txtformulation.Text + "มีค่าความเข้มข้นนี้แล้วในระบบ...", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtformulation.Clear();
                        txtformulation.Focus();
                        dr.Close();
                        cn.Close();
                    }
                    else
                    {
                        dr.Close();
                        cn.Close();

                        cn.Open();
                        cm = new MySqlCommand("insert into tbl_formulation (formulation) values (@formulation)", cn);
                        cm.Parameters.AddWithValue("@formulation", txtformulation.Text);
                        txtformulation.Clear();
                        txtformulation.Focus();
                        MessageBox.Show(txtformulation.Text + " บันทึกข้อมูลสำเร็จแล้ว!!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        loadFormulationList();
                    }
                }

            }
            catch
            {

            }
        }// Save and Check if its exists -- END
    }
}
