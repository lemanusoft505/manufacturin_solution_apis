using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace manufacturin_solution_apis
{
    public class cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos_operaciones : IDisposable
    {

        public string style { get; set; }
        public int orden { get; set; }
        public string operno { get; set; }
        public string descr { get; set; }
        public int Employees { get; set; }
        public int quantity { get; set; }
        public int tickets { get; set; }
        public decimal TOTAL_SAM { get; set; }
        public decimal Total_Pago { get; set; }


        public List<cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos_operaciones> grd(DataTable tbl)
        {
            List<cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos_operaciones> rs = new List<cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos_operaciones>();
            try
            {
                if (globales.consql.TieneDatos(tbl))
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        rs.Add(new cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos_operaciones()
                        {
                            descr = $"{row["descr"]}"
                            ,
                            orden= Employees = int.Parse($"{row["orden"]}")
                            ,
                            Employees = int.Parse($"{row["Employees"]}")
                            ,
                            operno=$"{row["operno"]}"
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



        public cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos_operaciones()
        {
            style = string.Empty;
            orden = 0;
            descr = string.Empty; 
            Employees = 0; 
            tickets = 0; 
            TOTAL_SAM = 0; 
            Total_Pago = 0;
            operno = string.Empty;
            quantity = 0;
        }

        public void Dispose()
        {
        }
    }
}