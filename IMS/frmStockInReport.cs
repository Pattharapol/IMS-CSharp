using Microsoft.Reporting.WinForms;
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
    public partial class frmStockInReport : Form
    {
        DBconnection dbcon = new DBconnection();
        MySqlConnection cn = new MySqlConnection();
        MySqlCommand cm = new MySqlCommand();
        MySqlDataReader dr;
        MySqlDataAdapter da;
        string title = "C# dev by TIK";

        public frmStockInReport()
        {
            InitializeComponent();
            cn = new MySqlConnection(dbcon.myConnection());
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }

        private void FrmStockInReport_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

        private void BtnShowReport_Click(object sender, EventArgs e)
        {
            string date1 = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            string date2 = dateTimePicker2.Value.ToString("dd/MM/yyyy");

            cn.Open();
            cm = new MySqlCommand("select * from tbl_stockin where sdate between '" + date1 + "' and '" + date2 + "' ", cn);
            da = new MySqlDataAdapter(cm);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ReportDataSource rds = new ReportDataSource("DataSet2", dt); // 1.DataSet2 ที่เก็บค่าจาก tbl_stockin 2. DataTable
            reportViewer1.LocalReport.ReportPath = @"Reports\StockInReport.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();

            cn.Close();
        }
    }
}
