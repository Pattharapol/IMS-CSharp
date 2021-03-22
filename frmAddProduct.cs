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
    public partial class frmAddProduct : Form
    {
        DBconnection dbcon = new DBconnection();
        MySqlConnection cn = new MySqlConnection();
        MySqlCommand cm = new MySqlCommand();
        MySqlDataReader dr;
        string title = "C# dev by TIK";
        frmProductList frmProductList = new frmProductList();

        public frmAddProduct()
        {
            InitializeComponent();
            cn = new MySqlConnection(dbcon.myConnection());
        }

        private void FrmAddProduct_Load(object sender, EventArgs e)
        {
            loadBrand();
            loadGeneric();
            loadCategory();
            loadUnit();
            loadFormulation();
        }


        // Add Product to table -- START
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBrand.Text == string.Empty)
                {
                    MessageBox.Show("กรุณาใส่ชื่อการค้า...", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtGeneric.Text == string.Empty)
                {
                    MessageBox.Show("กรุณาใส่ชื่อสามัญ...", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtCategory.Text == string.Empty)
                {
                    MessageBox.Show("กรุณาใส่หมวดหมู่...", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtQty.Text == string.Empty)
                {
                    MessageBox.Show("กรุณาใส่จำนวนที่ต้องการรับเข้า...", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtMaxstock.Text == string.Empty)
                {
                    MessageBox.Show("กรุณาใส่ค่าคงคลังที่มากที่สุด...", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtMinstock.Text == string.Empty)
                {
                    MessageBox.Show("กรุณาใส่ค่าคงคลังที่น้อยที่สุด...", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                cn.Open();
                cm = new MySqlCommand("insert into tbl_product (bid, productName,gid, cid, fid, qty, uid, maxstock, minstock) values (@bid, @productName, @gid, @cid, @fid, @qty, @uid, @maxstock, @minstock)", cn);
                cm.Parameters.AddWithValue("@bid", Int32.Parse(lblbrand.Text));
                cm.Parameters.AddWithValue("@productName", txtBrand.Text);
                cm.Parameters.AddWithValue("@gid", Int32.Parse(lblgeneric.Text));
                cm.Parameters.AddWithValue("@cid", Int32.Parse(lblcategory.Text));
                cm.Parameters.AddWithValue("@fid", Int32.Parse(lblformulation.Text));
                cm.Parameters.AddWithValue("@qty", txtQty.Text);
                cm.Parameters.AddWithValue("@uid", Int32.Parse(lblunit.Text));
                cm.Parameters.AddWithValue("@maxstock", txtMaxstock.Text);
                cm.Parameters.AddWithValue("@minstock", txtMinstock.Text);
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("บันทึกข้อมูลสำเร็จแล้ว!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearData();

                
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }// Add Product to table -- END

        // Clear data -- START
        private void clearData()
        {
            txtBrand.SelectedItem = null;
            txtGeneric.SelectedItem = null;
            txtCategory.SelectedItem = null;
            txtFormulation.SelectedItem = null;
            txtUnit.SelectedItem = null;
            txtQty.Text = "";
            txtMaxstock.Text = "";
            txtMinstock.Text = "";
            btnSave.Show();
            btnUpdate.Hide();
            btnCancel.Hide();
            txtBrand.Focus();
        }// Clear data -- END

        // load Brand -- START
        private void loadBrand()
        {
            try
            {
                DataTable dt = new DataTable("brand");
                cn.Open();
                cm = new MySqlCommand("select brand from tbl_brand", cn);
                dr = cm.ExecuteReader();
                dt.Load(dr);
                txtBrand.DataSource = dt;
                txtBrand.DisplayMember = "brand";
                dr.Close();
                cm.ExecuteNonQuery();
                cn.Close();
                txtBrand.SelectedItem = null;
                //cn.Open();
                //cm = new MySqlCommand("select * from tbl_brand order by brand ASC", cn);
                //DataSet ds = new DataSet();
                //MySqlDataAdapter da = new MySqlDataAdapter(cm);
                //da.Fill(ds, "brand"); // fill dataset , sourceTable
                //AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                //for (int i = 0; i < ds.Tables[0].Rows.Count - 1; i++)
                //{
                //    col.Add(ds.Tables[0].Rows[i]["brand"].ToString());
                //}
                //txtBrand.AutoCompleteSource = AutoCompleteSource.CustomSource;
                //txtBrand.AutoCompleteCustomSource = col;
                //txtBrand.AutoCompleteMode = AutoCompleteMode.Suggest;
                //da.Dispose();
                //cm.ExecuteNonQuery();
                //cn.Close();
            }
            catch
            {

            }
        }// load Brand -- END

        // load Generic -- START
        private void loadGeneric()
        {
            try
            {
                DataTable dt = new DataTable("generic");
                cn.Open();
                cm = new MySqlCommand("select generic from tbl_generic", cn);
                dr = cm.ExecuteReader();
                dt.Load(dr);
                txtGeneric.DataSource = dt;
                txtGeneric.DisplayMember = "generic";
                dr.Close();
                cm.ExecuteNonQuery();
                cn.Close();
                txtGeneric.SelectedItem = null;
                //cn.Open();
                //cm = new MySqlCommand("select * from tbl_generic order by generic ASC", cn);
                //DataSet ds = new DataSet();
                //MySqlDataAdapter da = new MySqlDataAdapter(cm);
                //da.Fill(ds, "generic"); // fill dataset , sourceTable
                //AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                //for (int i = 0; i < ds.Tables[0].Rows.Count - 1; i++)
                //{
                //    col.Add(ds.Tables[0].Rows[i]["generic"].ToString());
                //}
                //txtGeneric.AutoCompleteSource = AutoCompleteSource.CustomSource;
                //txtGeneric.AutoCompleteCustomSource = col;
                //txtGeneric.AutoCompleteMode = AutoCompleteMode.Suggest;
                //da.Dispose();
                //cm.ExecuteNonQuery();
                //cn.Close();
            }
            catch
            {

            }
        }// load Generic -- END

        // load Category -- START
        private void loadCategory()
        {
            try
            {
                DataTable dt = new DataTable("category");
                cn.Open();
                cm = new MySqlCommand("select category from tbl_category", cn);
                dr = cm.ExecuteReader();
                dt.Load(dr);
                txtCategory.DataSource = dt;
                txtCategory.DisplayMember = "category";
                dr.Close();
                cm.ExecuteNonQuery();
                cn.Close();
                txtCategory.SelectedItem = null;
                //cn.Open();
                //cm = new MySqlCommand("select * from tbl_category order by category ASC", cn);
                //DataSet ds = new DataSet();
                //MySqlDataAdapter da = new MySqlDataAdapter(cm);
                //da.Fill(ds, "category"); // fill dataset , sourceTable
                //AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                //for (int i = 0; i < ds.Tables[0].Rows.Count - 1; i++)
                //{
                //    col.Add(ds.Tables[0].Rows[i]["category"].ToString());
                //}
                //txtCategory.AutoCompleteSource = AutoCompleteSource.CustomSource;
                //txtCategory.AutoCompleteCustomSource = col;
                //txtCategory.AutoCompleteMode = AutoCompleteMode.Suggest;
                //da.Dispose();
                //cm.ExecuteNonQuery();
                //cn.Close();
            }
            catch
            {

            }
        }// load Category -- END

        // load Formulation -- START
        private void loadFormulation()
        {
            try
            {
                DataTable dt = new DataTable("formulation");
                cn.Open();
                cm = new MySqlCommand("select formulation from tbl_formulation", cn);
                dr = cm.ExecuteReader();
                dt.Load(dr);
                txtFormulation.DataSource = dt;
                txtFormulation.DisplayMember = "formulation";
                dr.Close();
                cm.ExecuteNonQuery();
                cn.Close();
                txtFormulation.SelectedItem = null;
                //cn.Open();
                //cm = new MySqlCommand("select * from tbl_formulation order by formulation ASC", cn);
                //DataSet ds = new DataSet();
                //MySqlDataAdapter da = new MySqlDataAdapter(cm);
                //da.Fill(ds, "formulation"); // fill dataset , sourceTable
                //AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                //for (int i = 0; i < ds.Tables[0].Rows.Count - 1; i++)
                //{
                //    col.Add(ds.Tables[0].Rows[i]["formulation"].ToString());
                //}
                //txtFormulation.AutoCompleteSource = AutoCompleteSource.CustomSource;
                //txtFormulation.AutoCompleteCustomSource = col;
                //txtFormulation.AutoCompleteMode = AutoCompleteMode.Suggest;
                //da.Dispose();
                //cm.ExecuteNonQuery();
                //cn.Close();
            }
            catch
            {

            }
        }// load Formulation -- END

        // load Unit -- START
        private void loadUnit()
        {
            try
            {
                DataTable dt = new DataTable("unit");
                cn.Open();
                cm = new MySqlCommand("select unit from tbl_unit", cn);
                dr = cm.ExecuteReader();
                dt.Load(dr);
                txtUnit.DataSource = dt;
                txtUnit.DisplayMember = "unit";
                dr.Close();
                cm.ExecuteNonQuery();
                cn.Close();
                txtUnit.SelectedItem = null;
                //cn.Open();
                //cm = new MySqlCommand("select * from tbl_unit order by unit ASC", cn);
                //DataSet ds = new DataSet();
                //MySqlDataAdapter da = new MySqlDataAdapter(cm);
                //da.Fill(ds, "unit"); // fill dataset , sourceTable
                //AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                //for (int i = 0; i < ds.Tables[0].Rows.Count - 1; i++)
                //{
                //    col.Add(ds.Tables[0].Rows[i]["unit"].ToString());
                //}
                //txtUnit.AutoCompleteSource = AutoCompleteSource.CustomSource;
                //txtUnit.AutoCompleteCustomSource = col;
                //txtUnit.AutoCompleteMode = AutoCompleteMode.Suggest;
                //da.Dispose();
                //cm.ExecuteNonQuery();
                //cn.Close();
            }
            catch
            {

            }
        }// load Unit -- END


        // for identify brand id -- START
        private void TxtBrand_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                cm = new MySqlCommand("select id from tbl_brand where brand like @brand", cn);
                cm.Parameters.AddWithValue("@brand", txtBrand.Text);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    lblbrand.Text = dr[0].ToString();
                }
                else
                {
                    lblbrand.Text = "nothing";
                }
                dr.Close();
                cm.ExecuteNonQuery();
                cn.Close();
            }
            catch
            {

            }
        }// for identify brand id -- END


        // for identify generic id -- START
        private void TxtGeneric_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                cm = new MySqlCommand("select id from tbl_generic where generic like @generic", cn);
                cm.Parameters.AddWithValue("@generic", txtGeneric.Text);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    lblgeneric.Text = dr[0].ToString();
                }
                else
                {
                    lblgeneric.Text = "nothing";
                }
                dr.Close();
                cm.ExecuteNonQuery();
                cn.Close();
            }
            catch
            {

            }
        }// for identify generic id -- END


        // for identify category id -- START
        private void TxtCategory_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                cm = new MySqlCommand("select id from tbl_category where category like @category", cn);
                cm.Parameters.AddWithValue("@category", txtCategory.Text);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    lblcategory.Text = dr[0].ToString();
                }
                else
                {
                    lblcategory.Text = "nothing";
                }
                dr.Close();
                cm.ExecuteNonQuery();
                cn.Close();
            }
            catch
            {

            }
        }// for identify category id -- END


        // for identify formulation id -- START
        private void TxtFormulation_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                cm = new MySqlCommand("select id from tbl_formulation where formulation like @formulation", cn);
                cm.Parameters.AddWithValue("@formulation", txtFormulation.Text);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    lblformulation.Text = dr[0].ToString();
                }
                else
                {
                    lblformulation.Text = "nothing";
                }
                dr.Close();
                cm.ExecuteNonQuery();
                cn.Close();
            }
            catch
            {

            }
        }// for identify formulation id -- END


        // for identify unit id -- START
        private void TxtUnit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                cm = new MySqlCommand("select id from tbl_unit where unit like @unit", cn);
                cm.Parameters.AddWithValue("@unit", txtUnit.Text);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    lblunit.Text = dr[0].ToString();
                }
                else
                {
                    lblunit.Text = "nothing";
                }
                dr.Close();
                cm.ExecuteNonQuery();
                cn.Close();
            }
            catch
            {

            }
        } // for identify unit id -- END


        // Cancel button -- START
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            txtBrand.SelectedItem = null;
            txtGeneric.SelectedItem = null;
            txtCategory.SelectedItem = null;
            txtFormulation.SelectedItem = null;
            txtUnit.SelectedItem = null;
            txtMaxstock.Text = "";
            txtMinstock.Text = "";
            txtQty.Text = "";
            txtBrand.Focus();
        }// Cancel button -- END


        // Update Product -- START
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBrand.Text == string.Empty)
                {
                    MessageBox.Show("กรุณาเลือกชื่อการค้า...", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtGeneric.Text == string.Empty)
                {
                    MessageBox.Show("กรุณาเลือกชื่อสามัญ...", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtCategory.Text == string.Empty)
                {
                    MessageBox.Show("กรุณาเลือกหมวดหมู่...", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtFormulation.Text == string.Empty)
                {
                    MessageBox.Show("กรุณาเลือกค่าความเข้มข้น...", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtQty.Text == string.Empty)
                {
                    MessageBox.Show("กรุณาใส่จำนวนที่ต้องการรับเข้า...", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtMaxstock.Text == string.Empty)
                {
                    MessageBox.Show("กรุณาใส่ค่าคงคลังที่มากที่สุด...", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtMinstock.Text == string.Empty)
                {
                    MessageBox.Show("กรุณาใส่ค่าคงคลังที่น้อยที่สุด...", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    cn.Open();
                    cm = new MySqlCommand("update tbl_product set bid = @bid, gid = @gid, cid = @cid, fid = @fid, qty = @qty, uid = @uid, maxstock = @maxstock, minstock = @minstock where id like '" + frmProductList.dgvProduct.CurrentRow.Cells[1].Value.ToString() + "' ", cn);
                    cm.Parameters.AddWithValue("@bid", Int32.Parse(lblbrand.Text));
                    cm.Parameters.AddWithValue("@gid", Int32.Parse(lblgeneric.Text));
                    cm.Parameters.AddWithValue("@cid", Int32.Parse(lblcategory.Text));
                    cm.Parameters.AddWithValue("@fid", lblformulation.Text);
                    cm.Parameters.AddWithValue("@qty", txtQty.Text);
                    cm.Parameters.AddWithValue("@uid", Int32.Parse(lblunit.Text));
                    cm.Parameters.AddWithValue("@maxstock", txtMaxstock.Text);
                    cm.Parameters.AddWithValue("@minstock", txtMinstock.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("บันทึกข้อมูลสำเร็จแล้ว!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmProductList.loadProduct();
                    clearData();
                }
            }
            catch
            {
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Error");
            }
        }// Update Product -- END
    }
 }



