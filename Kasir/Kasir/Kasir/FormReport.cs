using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kasir
{
    public partial class FormReport : Form
    {
        string report;
        string id;
        public FormReport(string report)
        {
            InitializeComponent();
            this.report = report;
        }
        public FormReport(string report,string id)
        {
            InitializeComponent();
            this.report = report;
            this.id = id;
        }

        private void FormReport_Load(object sender, EventArgs e)
        {
            if (report == "totaltransaksi")
            {
                CrystalReport1 rpt = new CrystalReport1();
                rpt.SetDatabaseLogon(Program.uidVar, "", Program.serverVar, Program.databaseVar);
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();

            }
            else if (report == "sepatu")
            {
                CrystalReport2 rpt = new CrystalReport2();
                rpt.SetDatabaseLogon(Program.uidVar, "", Program.serverVar, Program.databaseVar);
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();

            }
            else if (report == "brand")
            {
                CrystalReport4 rpt = new CrystalReport4();
                rpt.SetDatabaseLogon(Program.uidVar, "", Program.serverVar, Program.databaseVar);
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();

            }
            else if (report == "tipe")
            {
                CrystalReport5 rpt = new CrystalReport5();
                rpt.SetDatabaseLogon(Program.uidVar, "", Program.serverVar, Program.databaseVar);
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();

            }
            else if (report == "user")
            {
                CrystalReport3 rpt = new CrystalReport3();
                rpt.SetDatabaseLogon(Program.uidVar, "", Program.serverVar, Program.databaseVar);
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();

            }
            else if (report == "receipt")
            {
                CrystalReport6 rpt = new CrystalReport6();
                rpt.SetDatabaseLogon(Program.uidVar, "", Program.serverVar, Program.databaseVar);
                rpt.SetParameterValue(0, Convert.ToInt32(id));
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();
            }
        }
    }
}
