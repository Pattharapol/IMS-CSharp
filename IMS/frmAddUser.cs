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
    public partial class frmAddUser : Form
    {
        DBconnection dbcon = new DBconnection();
        MySqlConnection cn = new MySqlConnection();
        MySqlCommand cm = new MySqlCommand();
        MySqlDataReader dr;
        string title = "C# dev by TIK";

        public frmAddUser()
        {
            InitializeComponent();
            cn = new MySqlConnection(dbcon.myConnection());
            this.ActiveControl = txtName;
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            clearData();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUser.Text == string.Empty)
                {
                    MessageBox.Show("กรุณากรอกชื่อผู้ใช้งาน!!", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtPass.Text == string.Empty)
                {
                    MessageBox.Show("กรุณากรอกรหัสผ่าน!!", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtConfirm.Text == string.Empty)
                {
                    MessageBox.Show("กรุณากรอกรหัสยืนยัน!!", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cboType.Text == string.Empty)
                {
                    MessageBox.Show("กรุณาเลือกประเภทผู้ใช้งาน!!", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtName.Text == string.Empty)
                {
                    MessageBox.Show("กรุณากรอกชื่อ-สกุล!!", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtPass.Text != txtConfirm.Text)
                {
                    MessageBox.Show("รหัสผ่านกับรหัสยืนยัน ไม่ตรงกัน!!", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (MessageBox.Show("คุณต้องการบันทึก ใช่หรือไม่?", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new MySqlCommand("insert into tbl_user (username, password, name, user_type) values (@username, @password, @name, @user_type)", cn);
                    cm.Parameters.AddWithValue("@username", txtUser.Text);
                    cm.Parameters.AddWithValue("@password", txtPass.Text);
                    cm.Parameters.AddWithValue("@name", txtName.Text);
                    cm.Parameters.AddWithValue("@user_type", cboType.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("บันทึการเพิ่มผู้ใช้งานสำเร็จแล้ว", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmUserAccount frm = new frmUserAccount();
                    frm.loadUserList();
                    frm.Refresh();
                    clearData();

                }
            }
            catch
            {
                cn.Close();
            }
        }

        private void clearData()
        {
            txtUser.Clear();
            txtPass.Clear();
            txtConfirm.Clear();
            txtName.Clear();
            cboType.SelectedIndex = -1;
            txtName.Focus();
        }
    }
}
