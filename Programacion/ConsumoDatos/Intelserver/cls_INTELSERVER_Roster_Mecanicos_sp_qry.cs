using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace manufacturin_solution_apis
{
    public class cls_INTELSERVER_Roster_Mecanicos_sp_qry:IDisposable
    {
        public string ID { get; set; }
        public string OPERATOR { get; set; }
        public string JOB { get; set; }

        public List<cls_INTELSERVER_Roster_Mecanicos_sp_qry> cmb() {
            List<cls_INTELSERVER_Roster_Mecanicos_sp_qry> t = new List<cls_INTELSERVER_Roster_Mecanicos_sp_qry>() { };
            try
            {
                string strsql = "EXEC INTELSERVER.Roster_Mecanicos_sp_qry;";
                DataTable tbl = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl)) {
                    foreach (DataRow r in tbl.Rows) {
                        using (cls_INTELSERVER_Roster_Mecanicos_sp_qry tmp = new cls_INTELSERVER_Roster_Mecanicos_sp_qry()) {
                            tmp.ID = r["ID"].ToString();
                            tmp.OPERATOR = r["OPERATOR"].ToString();
                            tmp.JOB = r["JOB"].ToString();
                            t.Add(tmp);
                        }
                    }
                }
                tbl = null;
            }
            catch (Exception)
            {}
            return t;
        }

        public void Nuevo() {
            this.ID = string.Empty;
            this.OPERATOR = string.Empty;
            this.JOB = string.Empty;
        }

        public cls_INTELSERVER_Roster_Mecanicos_sp_qry()
        {
            Nuevo();
        }

        public void Dispose()
        {
        }
    }
}