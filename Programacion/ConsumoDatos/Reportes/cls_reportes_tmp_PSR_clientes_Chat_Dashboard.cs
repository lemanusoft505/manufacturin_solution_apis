using System;
using System.Collections.Generic;
using System.Data;
namespace manufacturin_solution_apis
{
    public class cls_reportes_ontimelate_clientes_Chat_Dashboard : IDisposable
    {


        public string Cliente { get; set; }
        public string Status { get; set; }
        public decimal Pending { get; set; }

        public void Nuevo()
        {
            this.Cliente = string.Empty;
            this.Status = "ON TIME";
            this.Pending = 0;
        }

        public cls_reportes_ontimelate_clientes_Chat_Dashboard()
        {
            Nuevo();
        }

        public void Dispose()
        {

        }

        public List<cls_reportes_ontimelate_clientes_Chat_Dashboard> Recuperar(string sSsesión, string Cliente)
        {
            try
            {
                List<cls_reportes_ontimelate_clientes_Chat_Dashboard> tmp = new List<cls_reportes_ontimelate_clientes_Chat_Dashboard>() { };
                string strsql = string.Format("EXEC DBO.PORDERCLIENT_STATUS_PENDING_TO_SHIPP_ChartDashboard @sesion= '{0}', @cliente='{1}';", sSsesión, globales.comillas(Cliente));
                DataTable tbl = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        var sCliente = Cliente;
                        var sStatus = row["STATUS"];
                        var nPending = row["PENDING"];
                        using (cls_reportes_ontimelate_clientes_Chat_Dashboard t = new cls_reportes_ontimelate_clientes_Chat_Dashboard())
                        {
                            t.Cliente = sCliente.ToString();
                            t.Status = sStatus.ToString();
                            t.Pending = decimal.Parse(nPending.ToString());
                            tmp.Add(t);
                        }
                    }
                }
                tbl.Dispose();
                tbl = null;
                return tmp;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}