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
    public partial class frmSecurity : Form
    {
        DBconnection dbcon = new DBconnection();
        MySqlConnection cn = new MySqlConnection();
        MySqlCommand cm = new MySqlCommand();
        MySqlDataReader dr;
        string title = "C# dev by TIK";
        public string strUser, strName, strPass, strUserType, strStatus;

        // Login -- START
        private void TxtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                //MessageBox.Show(" Enter pressed ");
                bool found = false;
                try
                {
                    if (txtUser.Text == string.Empty)
                    {
                        MessageBox.Show("กรุณากรอกชื่อผู้ใช้งาน...!!", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (txtPass.Text == string.Empty)
                    {
                        MessageBox.Show("กรุณากรอกรหัสผ่าน...!!", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    cn.Open();
                    cm = new MySqlCommand("select * from tbl_user where username like @username and password like @password", cn);
                    cm.Parameters.AddWithValue("@username", txtUser.Text);
                    cm.Parameters.AddWithValue("@password", txtPass.Text);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        strUser = dr["username"].ToString();
                        strName = dr["name"].ToString();
                        strPass = dr["password"].ToString();
                        strUserType = dr["user_type"].ToString();
                        strStatus = dr["status"].ToString();
                        found = true;
                    }
                    else
                    {
                        strUser = "";
                        strName = "";
                        strPass = "";
                        strUserType = "";
                        strStatus = "";
                        found = false;
                    }
                    dr.Close();
                    cn.Close();

                    if (found == true)
                    {
                        if (strStatus == "Active")
                        {
                            if (strUserType == "User")
                            {
                                MessageBox.Show("ยินดีต้อนรับคุณ " + strName + " เข้าสู่ระบบ", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                frmMain frmMain = new frmMain();
                                frmMain.btnMaintainance.Hide();
                                frmMain.btnAccount.Hide();
                                frmMain.btnSystemLock.Hide();
                                frmMain.lblUserLogin.Text = strName;
                                frmMain.lblPass.Text = strPass;
                                frmMain.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("ยินดีต้อนรับคุณ " + strName + " เข้าสู่ระบบ", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                frmMain frmMain = new frmMain();
                                frmMain.lblUserLogin.Text = strName;
                                frmMain.lblPass.Text = strPass;
                                frmMain.Show();
                                this.Hide();
                            }
                        }
                        else
                        {
                            MessageBox.Show("ไม่สามารถเข้าใช้งานได้ กรุณาติดต่อผู้ดูแลระบบ...", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("ไม่สามารถเข้าใช้งานได้ กรุณาติดต่อผู้ดูแลระบบ...", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch
                {
                    cn.Close();
                }
            }
        }// Login -- END


        // Login -- START
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            bool found = false;
            try
            {
                if (txtUser.Text == string.Empty)
                {
                    MessageBox.Show("กรุณากรอกชื่อผู้ใช้งาน...!!", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtPass.Text == string.Empty)
                {
                    MessageBox.Show("กรุณากรอกรหัสผ่าน...!!", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                cn.Open();
                cm = new MySqlCommand("select * from tbl_user where username like @username and password like @password", cn);
                cm.Parameters.AddWithValue("@username", txtUser.Text);
                cm.Parameters.AddWithValue("@password", txtPass.Text);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    strUser = dr["username"].ToString();
                    strName = dr["name"].ToString();
                    strPass = dr["password"].ToString();
                    strUserType = dr["user_type"].ToString();
                    strStatus = dr["status"].ToString();
                    found = true;
                }
                else
                {
                    strUser = "";
                    strName = "";
                    strPass = "";
                    strUserType = "";
                    strStatus = "";
                    found = false;
                }
                dr.Close();
                cn.Close();

                if (found == true)
                {
                    if (strStatus == "Active")
                    {
                        if (strUserType == "User")
                        {
                            MessageBox.Show("ยินดีต้อนรับคุณ " + strName + " เข้าสู่ระบบ", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmMain frmMain = new frmMain();
                            frmMain.btnMaintainance.Hide();
                            frmMain.btnAccount.Hide();
                            frmMain.btnSystemLock.Hide();
                            frmMain.lblUserLogin.Text = strName;
                            frmMain.lblPass.Text = strPass;
                            frmMain.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("ยินดีต้อนรับคุณ " + strName + " เข้าสู่ระบบ", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmMain frmMain = new frmMain();
                            frmMain.lblUserLogin.Text = strName;
                            frmMain.lblPass.Text = strPass;
                            frmMain.Show();
                            this.Hide();
                        }
                    }
                    else
                    {
                        MessageBox.Show("ไม่สามารถเข้าใช้งานได้ กรุณาติดต่อผู้ดูแลระบบ...", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("ไม่สามารถเข้าใช้งานได้ กรุณาติดต่อผู้ดูแลระบบ...", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                cn.Close();
            }
        }// Login -- END

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Application.Exit();
        }

        // btnCancel -- START
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            txtUser.Clear();
            txtPass.Clear();
            txtUser.Focus();
        }// btnCancel -- END

        public frmSecurity()
        {
            InitializeComponent();
            cn = new MySqlConnection(dbcon.myConnection());
        }
    }
}
