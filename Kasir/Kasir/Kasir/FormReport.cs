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
        public FormReport(string report)
        {
            InitializeComponent();
            this.report = report;
        }

        private void FormReport_Load(object sender, EventArgs e)
        {
            if (report == "totaltransaksi")
            {
                CrystalReport1 rpt = new CrystalReport1();
                rpt.SetDatabaseLogon(Program.uidVar, "", Program.serverVar, Program.databaseVar);
                crystalReportViewer1.ReportSource = rpt;
            }else if (report == "sepatu")
            {
                CrystalReport2 rpt = new CrystalReport2();
                rpt.SetDatabaseLogon(Program.uidVar, "", Program.serverVar, Program.databaseVar);
                crystalReportViewer1.ReportSource = rpt;
            }
            else if (report == "brand")
            {
                CrystalReport4 rpt = new CrystalReport4();
                rpt.SetDatabaseLogon(Program.uidVar, "", Program.serverVar, Program.databaseVar);
                crystalReportViewer1.ReportSource = rpt;
            }
            else if (report == "tipe")
            {
                CrystalReport5 rpt = new CrystalReport5();
                rpt.SetDatabaseLogon(Program.uidVar, "", Program.serverVar, Program.databaseVar);
                crystalReportViewer1.ReportSource = rpt;
            }
            else if (report == "user")
            {
                CrystalReport3 rpt = new CrystalReport3();
                rpt.SetDatabaseLogon(Program.uidVar, "", Program.serverVar, Program.databaseVar);
                crystalReportViewer1.ReportSource = rpt;
            }
        }
    }
}
