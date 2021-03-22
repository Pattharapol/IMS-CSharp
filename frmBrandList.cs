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
    public partial class frmBrandList : Form
    {
        DBconnection dbcon = new DBconnection();
        MySqlConnection cn = new MySqlConnection();
        MySqlCommand cm = new MySqlCommand();
        MySqlDataReader dr;
        string title = "C# dev by TIK";

        public frmBrandList()
        {
            InitializeComponent();
            cn = new MySqlConnection(dbcon.myConnection());
            // สร้างทางการเชื่อมต่อ ไปที่ Class DBconnection
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadBrandList();
        }


        // load Brand -- START
        private void loadBrandList()
        {
            dgvBrand.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new MySqlCommand("select * from tbl_brand WHERE brand Like '%" + txtSearch.Text + "%' order by brand ASC", cn);
            // ใช้ DataReader เข้าไปอ่านค่าจาก DB
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                // DGV เพิ่มค่าเข้าไปทีละแถว สามารถระบุค่า column จาก DB ได้ หรือ จะใส่เป็น array ก็ได้ เช่น dr[0].tostring()
                dgvBrand.Rows.Add(i, dr["id"].ToString(), dr["brand"].ToString(), "EDIT", "DELETE");
            }

            dr.Close();
            cn.Close();
            // เปิดหลังปิดก่อน เปืิดก่อนปิดหลัง

            // นับแถวของ DGV 
            lblCount.Text = dgvBrand.RowCount + " รายการ";
        }// load Brand -- END


        // Cancel button -- START
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            txtBrand.Clear();
            txtBrand.Focus();
            btnCancel.Hide();
            btnUpdate.Hide();
            btnSave.Show();
        }// Cancel button -- END


        // Update Brand -- START
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // ถามก่อนว่า ต้องการจะแก้ไขจริงหรือไม่
                if (MessageBox.Show("คุณต้องการแก้ไขข้อมูล ใช่หรือไม่?", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new MySqlCommand("update tbl_brand set brand = @brand where id like '" + dgvBrand.CurrentRow.Cells[1].Value.ToString() + "' ", cn);
                    cm.Parameters.AddWithValue("@brand", txtBrand.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("แก้ไขข้อมูลสำเร็จ!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBrand.Clear();
                    txtBrand.Focus();
                    btnSave.Show();
                    btnUpdate.Hide();
                    btnCancel.Hide();
                    loadBrandList();
                    // แก้ไขเสร็จแล้ว ดึงค่ามาแสดงใหม่
                }
                else
                {
                    MessageBox.Show("Cannot update!!!", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch
            {

            }
        }// Update Brand -- END


        // Save and Check if it exists -- START
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBrand.Text == string.Empty) // เช็คว่า ถ้ามีค่าว่างให้แจ้ง แล้วหยุดการทำงานทันที
                {
                    MessageBox.Show("กรุณาใส่ชื่อการค้า...", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBrand.Focus();
                    return;
                }

                // ตรวจสอบว่า มีชื่อ brand ซ้ำกันมั้ย ถ้ามี ให้แจ้งทันที
                cn.Open();
                cm = new MySqlCommand("select brand from tbl_brand where brand like @brand", cn);
                cm.Parameters.AddWithValue("@brand", txtBrand.Text);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows) // dr.HasRows ถ้ามีแถว แสดงว่า มีค่าตรงตามเงื่อนไขดังกล่าว จะแจ้งเตือนทันที
                {
                    MessageBox.Show(txtBrand.Text + " มีชื่อนี้แล้วในระบบ...กรุณาใส่ชื่อการค้าใหม่...", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBrand.Clear();
                    txtBrand.Focus();
                    dr.Close();
                    cn.Close();
                }
                else
                {
                    // ถ้าไม่มีชื่อ brand ซ้ำ จะทำการบันทึกลงฐานข้อมูลโดยปกติ
                    dr.Close();
                    cn.Close();

                    cn.Open();
                    cm = new MySqlCommand("insert into tbl_brand (brand) values (@brand)", cn);
                    cm.Parameters.AddWithValue("@brand", txtBrand.Text);
                    txtBrand.Clear();
                    txtBrand.Focus();
                    MessageBox.Show(txtBrand.Text + "บันทึกสำเร็จแล้ว!!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    // เสร็จแล้วดึงค่ามาแสดงใหม่
                    loadBrandList();
                }
            }
            catch
            {

            }
        }// Save and Check if it exists -- END


        // Edit and Delete by DGVcolumn.Name -- START
        private void DgvBrand_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // ถ้าเราคลิกภายใน DGV ตรงกับ column ที่ตั้งชื่อไว้ แล้วทำงานดังนี้
            string colName = dgvBrand.Columns[e.ColumnIndex].Name;
            // สำหรับ แก้ไข
            if (colName == "edit")
            {
                txtBrand.Text = dgvBrand.CurrentRow.Cells[2].Value.ToString();
                txtBrand.Focus();
                btnSave.Hide();
                btnUpdate.Show();
                btnCancel.Show();
            }
            // สำหรับ ลบ
            else if (colName == "delete")
            {
                if (MessageBox.Show("คุณต้องการแก้ไข ใช่หรือไม่?", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // ลบโดยชี้ไปที่ id ที่ cells[1]
                    cn.Open();
                    cm = new MySqlCommand("delete from tbl_brand where id like '" + dgvBrand.CurrentRow.Cells[1].Value.ToString() + "' ", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("ลบข้อมูลสำเร็จ!", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    // เสร็จแล้วโหลดค่ามาแสดงใหม่
                    loadBrandList();
                }
            }
        }// Edit and Delete by DGVcolumn.Name -- END


        // Search by TExtChange -- START
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            loadBrandList();
        }// Search by TExtChange -- END
    }
}
