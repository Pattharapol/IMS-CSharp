using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Timers;


namespace IMS
{
    public partial class frmRetriveDataFromHimpro : Form
    {
        
        private string _bid;
        private string _brand;
        private string _orderno;
        private string _hn;

        MySqlConnection cn = new MySqlConnection();
        DBconnection dbcon = new DBconnection();
        MySqlCommand cm;
        public frmRetriveDataFromHimpro()
        {
            
            InitializeComponent();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            dgvRetriveData.ClearSelection();
        }

        private void FrmRetriveDataFromHimpro_Load(object sender, EventArgs e)
        {
            FillGrid();
        }



        public void FillGrid()
        {
            cn = new MySqlConnection(dbcon.myConnectionHimpro());
            DataTable dt = new DataTable();
            
            try
            {
                // FIllGrid
                cn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter("select regdate, hn, orderno, namedrug, amount, room_order, recorder from opd.drug_order_opd WHERE regdate between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'  AND room_order = 'INV8' ", cn);
                da.Fill(dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dgvRetriveData.DataSource = dt;
                        dgvRetriveData.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                    else
                    {
                        dgvRetriveData.DataSource = null;
                    }
                }
                da.Dispose();
                cn.Close();


                // Check DB if record is exist
                //MySqlCommand cm = new MySqlCommand();
                //MySqlDataReader dr;
                //cn = new MySqlConnection(dbcon.myConnection());
                //cn.Open();
                //cm.ExecuteNonQuery();
                //cm = new MySqlCommand("select id, regdate, hn, orderno, namedrug, amout, room_order, recorder from tbl_datafromhimpro WHERE orderno = @orderno and  namedrug = @namedrug ", cn);
                //cm.Parameters.AddWithValue("@orderno", dgvRetriveData.CurrentRow.Cells[3].Value.ToString());
                //cm.Parameters.AddWithValue("@namedrug", dgvRetriveData.CurrentRow.Cells[4].Value.ToString());
                //dr = cm.ExecuteReader();
                //if(dr.HasRows)
                //{
                //    return;
                //}
                //else
                //{

                MySqlDataReader dr;
                

                for (int i = 0; i < dgvRetriveData.Rows.Count; i++)
                {
                    
                    cn = new MySqlConnection(dbcon.myConnection());
                    cn.Open();
                    cm = new MySqlCommand("select orderno, namedrug , regdate from tbl_datafromhimpro where orderno like @orderno and namedrug like @namedrug and regdate between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' ", cn);
                    cm.Parameters.AddWithValue("@orderno", dgvRetriveData.Rows[i].Cells[2].Value.ToString());
                    cm.Parameters.AddWithValue("@namedrug", dgvRetriveData.Rows[i].Cells[3].Value.ToString()); // just ike brand or productName
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if(dr.HasRows)
                    {
                        cn.Close();
                    }
                    else
                    {
                        string date = dgvRetriveData.Rows[i].Cells[0].Value.ToString();
                        string date2 = Convert.ToDateTime(date).ToString("yyyy-MM-dd");
                        cn.Close();

                        cn = new MySqlConnection(dbcon.myConnection());
                        cn.Open();
                        cm = new MySqlCommand("insert into tbl_datafromhimpro (regdate, hn, orderno, namedrug, amout, room_order, recorder) values (@regdate, @hn, @orderno, @namedrug, @amout, @room_order, @recorder)", cn);
                        cm.Parameters.AddWithValue("@regdate", date2);
                        cm.Parameters.AddWithValue("@hn", dgvRetriveData.Rows[i].Cells[1].Value.ToString());
                        cm.Parameters.AddWithValue("@orderno", dgvRetriveData.Rows[i].Cells[2].Value.ToString());
                        cm.Parameters.AddWithValue("@namedrug", dgvRetriveData.Rows[i].Cells[3].Value.ToString());
                        cm.Parameters.AddWithValue("@amout", dgvRetriveData.Rows[i].Cells[4].Value.ToString());
                        cm.Parameters.AddWithValue("@room_order", dgvRetriveData.Rows[i].Cells[5].Value.ToString());
                        cm.Parameters.AddWithValue("@recorder", dgvRetriveData.Rows[i].Cells[6].Value.ToString());
                        cm.ExecuteNonQuery();
                        dr.Close();
                        cn.Close();


                        //cn = cn = new MySqlConnection(dbcon.myConnection());
                        //cn.Open();
                        //cm = new MySqlCommand("SELECT pro.bid, bra.brand, orderno, hn FROM tbl_product AS pro INNER JOIN tbl_brand AS bra ON bra.id = pro.bid inner join tbl_datafromhimpro AS dhp ON bra.brand = dhp.namedrug where dhp.orderno = @orderno and dhp.hn = @hn and bra.brand = @brand", cn);
                        //cm.Parameters.AddWithValue("@orderno", dgvRetriveData.Rows[i].Cells[3].Value.ToString());
                        //cm.Parameters.AddWithValue("@hn", dgvRetriveData.Rows[i].Cells[2].Value.ToString());
                        //cm.Parameters.AddWithValue("@brand", dgvRetriveData.Rows[i].Cells[4].Value.ToString());
                        //dr = cm.ExecuteReader();
                        //if (dr.HasRows)
                        //{
                        //    _bid = dr["bid"].ToString();
                        //    _brand = dr["brand"].ToString();
                        //    _orderno = dr["orderno"].ToString();
                        //    _hn = dr["hn"].ToString();
                        //}
                        //dr.Close();
                        //cn.Close();


                        //update stock
                        cn = new MySqlConnection(dbcon.myConnection());
                        cn.Open();
                        cm = new MySqlCommand("update tbl_product set qty = qty - @qty where productName like @productName", cn);
                        cm.Parameters.AddWithValue("@qty", dgvRetriveData.Rows[i].Cells[4].Value.ToString()); // amount from himpro
                        cm.Parameters.AddWithValue("@productName", dgvRetriveData.Rows[i].Cells[3].Value.ToString());
                        cm.ExecuteNonQuery();
                        dr.Close();
                        cn.Close();

                    }
                }

                lblCount.Text = dgvRetriveData.RowCount + " รายการ";
                cn.Close();
            }
            catch (Exception)
            {
                cn.Close();
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            FillGrid();
        }
    }
}
