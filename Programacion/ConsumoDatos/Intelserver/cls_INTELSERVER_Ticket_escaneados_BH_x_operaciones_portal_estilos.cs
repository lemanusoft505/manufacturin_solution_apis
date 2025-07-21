using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace manufacturin_solution_apis
{
    public class cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos : IDisposable
    {
        /*
         					
         */
        public string style { get; set; }
        public string descr { get; set; }
        public int Employees { get; set; }
        public int tickets { get; set; }
        public decimal TOTAL_SAM { get; set; }
        public decimal Total_Pago { get; set; }
        public string BULLETIN { get; set; }
        public int ID_STYLE { get; set; }
        public string CLIENTE { get; set; }
        public decimal SAM { get; set; }
        public decimal RATE { get; set; }
        public int CANTIDAD_WIP { get; set; }

        public void recuperar(DataTable tbl)
        {
            try
            {
                if (globales.consql.TieneDatos(tbl))
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        descr = $"{row["descr"]}";
                        Employees = int.Parse($"{row["Employees"]}");
                        style = $"{row["style"]}";
                        tickets = int.Parse($"{row["tickets"]}");
                        Total_Pago = decimal.Parse($"{row["Total_Pago"]}");
                        TOTAL_SAM = decimal.Parse($"{row["TOTAL_SAM"]}");
                        BULLETIN = $"{row["BULLETIN"]}";
                        ID_STYLE = int.Parse($"{row["ID_STYLE"]}");
                        CLIENTE = $"{row["CLIENTE"]}";
                        SAM = decimal.Parse($"{row["SAM"]}");
                        RATE = decimal.Parse($"{row["RATE"]}");
                        CANTIDAD_WIP = int.Parse($"{row["CANTIDAD_WIP"]}");
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }

        public List<cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos> grd(DataTable tbl) {
            List<cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos> rs = new List<cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos>();
            try
            {
                if (globales.consql.TieneDatos(tbl)) {
                    foreach (DataRow row in tbl.Rows) {
                        rs.Add(new cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos() { 
                            descr = $"{row["descr"]}"
                            , Employees = int.Parse($"{row["Employees"]}")
                            , style = $"{row["style"]}"
                            , tickets =int.Parse( $"{row["tickets"]}")
                            , Total_Pago = decimal.Parse($"{row["Total_Pago"]}")
                            , TOTAL_SAM = decimal.Parse($"{row["TOTAL_SAM"]}")
                            , BULLETIN = $"{row["BULLETIN"]}"
                            , ID_STYLE = int.Parse($"{row["ID_STYLE"]}")
                            ,  CLIENTE= $"{row["CLIENTE"]}"
                            , RATE= decimal.Parse($"{row["RATE"]}")
                            , SAM= decimal.Parse($"{row["SAM"]}")
                            , CANTIDAD_WIP = int.Parse($"{row["CANTIDAD_WIP"]}")
                        });
                    }
                }
            }
            catch (Exception)
            {
                rs=null;
            }
            return rs;
        }

        public cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos()
        {
            style = string.Empty;
            descr = string.Empty;
            Employees = 0;
            tickets = 0;
            TOTAL_SAM = 0;
            Total_Pago = 0; 
            BULLETIN = "";
            ID_STYLE = 0;
            CLIENTE = "";
            SAM = 0;
            RATE = 0;
            CANTIDAD_WIP = 0;
        }

        public void Dispose()
        {
            
        }
    }
}