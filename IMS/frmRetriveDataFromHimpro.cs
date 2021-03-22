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
using System.Threading;

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
            dgvData.ClearSelection();
            progressBar1.Visible = false;
        }

        private void FrmRetriveDataFromHimpro_Load(object sender, EventArgs e)
        {
            
        }



        private void FillGrid()
        {
            
            cn = new MySqlConnection(dbcon.myConnectionHimpro());
            DataTable dt = new DataTable();
            
            try
            {
                // FIllGrid
                cn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter("select regdate, hn, orderno, namedrug, amount, room_order, recorder from opd.drug_order_opd WHERE regdate between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' ", cn);
                //select regdate, hn, orderno, namedrug, amount, room_order, recorder from opd.drug_order_opd WHERE regdate between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'  AND room_order = 'INV8'
                da.Fill(dt);
                setDataSource(dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dgvData.DataSource = dt;
                        updateStock();
                        dgvData.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                    else
                    {
                        dgvData.DataSource = null;
                    }
                }
                da.Dispose();
                cn.Close();

                label2.Text = dgvData.Rows.Count + " rows";

                
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
            }
            catch (Exception)
            {
                cn.Close();
            }
        }



        internal delegate void SetDataSourceDelegate(DataTable table);
        private void setDataSource(DataTable table)
        {
            // Invoke method if required: อันนี้คือ อ่านมาจากฝรั่ง ถ้า thread ตัวไหนทำงานอื่นอยู่ ให้มานี่ 55555 มาช่วยยนี่ก๊อนนนนนนนนน
            if (this.InvokeRequired)
            {
                this.Invoke(new SetDataSourceDelegate(setDataSource), table);
            }
            else
            {
                dgvData.DataSource = table;
                progressBar1.Visible = false;
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
           
        }


        private void BtnProcess_Click(object sender, EventArgs e)
        {
            //FillGrid();
            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
            System.Threading.Thread thread1 =
              new System.Threading.Thread(new System.Threading.ThreadStart(FillGrid));
            thread1.Start();
        }

        private void updateStock()
        {
            MySqlDataReader dr;
            for (int i = 0; i < dgvData.Rows.Count; i++)
            {


                cn = new MySqlConnection(dbcon.myConnection());
                cn.Open();
                cm = new MySqlCommand("select orderno, namedrug , regdate from tbl_datafromhimpro where orderno like @orderno and namedrug like @namedrug and regdate between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' ", cn);
                cm.Parameters.AddWithValue("@orderno", dgvData.Rows[i].Cells[2].Value.ToString());
                cm.Parameters.AddWithValue("@namedrug", dgvData.Rows[i].Cells[3].Value.ToString()); // just ike brand or productName
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    cn.Close();
                }
                else
                {
                    string date = dgvData.Rows[i].Cells[0].Value.ToString();
                    string date2 = Convert.ToDateTime(date).ToString("yyyy-MM-dd");
                    cn.Close();

                    cn = new MySqlConnection(dbcon.myConnection());
                    cn.Open();
                    cm = new MySqlCommand("insert into tbl_datafromhimpro (regdate, hn, orderno, namedrug, amout, room_order, recorder) values (@regdate, @hn, @orderno, @namedrug, @amout, @room_order, @recorder)", cn);
                    cm.Parameters.AddWithValue("@regdate", date2);
                    cm.Parameters.AddWithValue("@hn", dgvData.Rows[i].Cells[1].Value.ToString());
                    cm.Parameters.AddWithValue("@orderno", dgvData.Rows[i].Cells[2].Value.ToString());
                    cm.Parameters.AddWithValue("@namedrug", dgvData.Rows[i].Cells[3].Value.ToString());
                    cm.Parameters.AddWithValue("@amout", dgvData.Rows[i].Cells[4].Value.ToString());
                    cm.Parameters.AddWithValue("@room_order", dgvData.Rows[i].Cells[5].Value.ToString());
                    cm.Parameters.AddWithValue("@recorder", dgvData.Rows[i].Cells[6].Value.ToString());
                    cm.ExecuteNonQuery();
                    dr.Close();
                    cn.Close();


                    //update stock
                    cn = new MySqlConnection(dbcon.myConnection());
                    cn.Open();
                    cm = new MySqlCommand("update tbl_product set qty = qty - @qty where productName like @productName", cn);
                    cm.Parameters.AddWithValue("@qty", dgvData.Rows[i].Cells[4].Value.ToString()); // amount from himpro
                    cm.Parameters.AddWithValue("@productName", dgvData.Rows[i].Cells[3].Value.ToString());
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
                }
            }
        }
    }
}
