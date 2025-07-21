using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace manufacturin_solution_apis
{
    public class cls_reportes_ontimelate : IDisposable
    {
        public string CLIENTE { get; set; }
        public string CLIENTE_ALIAS { get; set; }
        public string CUT { get; set; }
        public int ISSUES_UNITS { get; set; }
        public int CUTED_UNITS { get; set; }
        public int SEWED_UNITS { get; set; }
        public int WASHED_UNITS { get; set; }
        public int PACKED_UNITS { get; set; }
        public int INVOICED_UNITS { get; set; }
        public string LINE { get; set; }
        public string IssueDate { get; set; }
        public string Issue_Date { get; set; }
        public int ELAPSED_DAYS { get; set; }
        public string EXIT_FACTORY_DATE { get; set; }
        public int DELAYED_DAYS { get; set; }
        public int BALANCE { get; set; }
        public int PENDING { get; set; }
        public string STATUS { get; set; }
        public string STATUS2 { get; set; }
        public string COD_STATUS { get; set; }

        public List<cls_reportes_ontimelate> grd(string Sesión)
        {
            List<cls_reportes_ontimelate> rs = new List<cls_reportes_ontimelate>() { };
            string strsql = string.Format("exec reportes.tmp_PSR_grd @sesión = '{0}'", Sesión.Replace("'", "''"));
            DataTable tbl = new DataTable();
            globales.consql.llenar_datatable(strsql, ref tbl);
            if (globales.consql.TieneDatos(tbl))
            {
                foreach (DataRow row in tbl.Rows)
                {
                    using (cls_reportes_ontimelate tmp = new cls_reportes_ontimelate())
                    {
                        tmp.CLIENTE = row["CLIENTE"].ToString();
                        tmp.CLIENTE_ALIAS = row["CLIENTE ALIAS"].ToString();
                        tmp.CUT = row["CUT"].ToString();
                        tmp.ISSUES_UNITS = int.Parse(row["ISSUES UNITS"].ToString());
                        tmp.CUTED_UNITS = int.Parse(row["CUTED UNITS"].ToString());
                        tmp.SEWED_UNITS = int.Parse(row["SEWED UNITS"].ToString());
                        tmp.WASHED_UNITS = int.Parse(row["WASHED UNITS"].ToString());
                        tmp.PACKED_UNITS = int.Parse(row["PACKED UNITS"].ToString());
                        tmp.INVOICED_UNITS = int.Parse(row["INVOICED UNITS"].ToString());
                        tmp.LINE = row["LINE"].ToString();
                        tmp.IssueDate = row["IssueDate"].ToString();
                        tmp.Issue_Date = row["Issue Date"].ToString();
                        tmp.ELAPSED_DAYS = int.Parse(row["ELAPSED DAYS"].ToString());
                        tmp.EXIT_FACTORY_DATE = row["EXIT FACTORY DATE"].ToString();
                        tmp.DELAYED_DAYS = int.Parse(row["DELAYED DAYS"].ToString());
                        tmp.BALANCE = int.Parse(row["BALANCE"].ToString());
                        tmp.PENDING = int.Parse(row["PENDING"].ToString());
                        tmp.STATUS = row["STATUS"].ToString();
                        tmp.STATUS2 = row["STATUS2"].ToString();
                        tmp.COD_STATUS = row["COD_STATUS"].ToString();
                        rs.Add(tmp);
                    }
                }
            }
            tbl.Dispose();
            tbl = null;
            return rs;
        }

        public void Nuevo()
        {
            this.CLIENTE = string.Empty;
            this.CLIENTE_ALIAS = string.Empty;
            this.CUT = string.Empty;
            this.ISSUES_UNITS = 0;
            this.CUTED_UNITS = 0;
            this.SEWED_UNITS = 0;
            this.WASHED_UNITS = 0;
            this.PACKED_UNITS = 0;
            this.INVOICED_UNITS = 0;
            this.LINE = string.Empty;
            this.IssueDate = string.Empty;
            this.Issue_Date = string.Empty;
            this.ELAPSED_DAYS = 0;
            this.EXIT_FACTORY_DATE = string.Empty;
            this.DELAYED_DAYS = 0;
            this.BALANCE = 0;
            this.PENDING = 0;
            this.STATUS = string.Empty;
            this.STATUS2 = string.Empty;
            this.COD_STATUS = string.Empty;
        }

        public cls_reportes_ontimelate()
        {
            Nuevo();
        }

        public void Dispose()
        {

        }
    }
}