using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace manufacturin_solution_apis
{
    public class cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos_prod : IDisposable
    {
        public string style { get; set; }
        public string prodno { get; set; }
        public string descr { get; set; }
        public int Employees { get; set; }
        public int quantity { get; set; }
        public int tickets { get; set; }
        public decimal TOTAL_SAM { get; set; }
        public decimal Total_Pago { get; set; }
        public int Cantidad_WIP { get; set; }



        public List<cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos_prod> grd(DataTable tbl)
        {
            List<cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos_prod> rs = new List<cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos_prod>();
            try
            {
                if (globales.consql.TieneDatos(tbl))
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        rs.Add(new cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos_prod()
                        {
                            descr = $"{row["descr"]}"
                            ,
                            prodno = $"{row["prodno"]}"
                            ,
                            Employees = int.Parse($"{row["Employees"]}")
                            ,
                            quantity = int.Parse($"{row["quantity"]}")
                            ,
                            style = $"{row["style"]}"
                            ,
                            tickets = int.Parse($"{row["tickets"]}")
                            ,
                            Total_Pago = decimal.Parse($"{row["Total_Pago"]}")
                            ,
                            TOTAL_SAM = decimal.Parse($"{row["TOTAL_SAM"]}")
                            , 
                            Cantidad_WIP= Convert.IsDBNull(row["Cantidad_WIP"])?0: int.Parse($"{row["Cantidad_WIP"]}")
                        });
                    }
                }
            }
            catch (Exception)
            {
                rs = null;
            }
            return rs;
        }

        public cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos_prod()
        {
            style = string.Empty;
            descr = string.Empty;
            Employees = 0;
            tickets = 0;
            TOTAL_SAM = 0;
            Total_Pago = 0;
            prodno = string.Empty;
            quantity = 0;
            Cantidad_WIP = 0;
        }

        public void Dispose()
        {
        }
    }
}