using manufacturin_solution_apis;
using System;
using System.Collections.Generic;
using System.Data;

namespace manufacturin_solution_apis.Models
{
    public class tbl_kpi_tickets_escaneados_x_dia_Operaciones : IDisposable
    {
        public string LINEA { get; set; }
        public string UBICACION { get; set; }
        public string STATUS { get; set; }
        public string OPERNO { get; set; }
        public string OPERDESCR { get; set; }

        public int TICKETS { get; set; }
        public int CANTIDAD { get; set; }
        public decimal TOTAL_SAM { get; set; }
        public decimal PORC_SAM { get; set; }
        public int OPERARIOS { get; set; }
        public decimal PORC_OPERARIOS { get; set; }
        public decimal SAM_OPERARIOS { get; set; }
        public decimal EFICIENCIA { get; set; }
        public DateTime dtFecha { get; set; }



        public List<tbl_kpi_tickets_escaneados_x_dia_Operaciones> grd_x_linea(string sLinea)
        {
            List<tbl_kpi_tickets_escaneados_x_dia_Operaciones> rs = new List<tbl_kpi_tickets_escaneados_x_dia_Operaciones>();
            try
            {
                string strsql = $"EXEC intelserver.kpi_tickets_escaneados_tbl_x_Linea_x_operaciones @Linea='{globales.comillas(sLinea)}', @dtFecha = {globales.sqldate(dtFecha)};";
                DataTable tbl = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    foreach (DataRow r in tbl.Rows)
                    {
                        rs.Add(new tbl_kpi_tickets_escaneados_x_dia_Operaciones()
                        {
                            OPERNO = $"{r["OPERNO"]}"
                        ,
                            OPERDESCR = $"{r["OPERDESCR"]}"
                        ,
                            UBICACION = $"{r["UBICACION"]}"
                        ,
                            LINEA = sLinea
                        ,
                            STATUS = $"{r["STATUS"]}"
                        ,
                            TICKETS = int.Parse($"{r["TICKETS"]}")
                        ,
                            CANTIDAD = int.Parse($"{r["CANTIDAD"]}")
                        ,
                            EFICIENCIA = decimal.Parse($"{r["EFICIENCIA"]}")
                        ,
                            OPERARIOS = int.Parse($"{r["OPERARIOS"]}")
                        ,
                            PORC_OPERARIOS = decimal.Parse($"{r["PORC_OPERARIOS"]}")
                        ,
                            PORC_SAM = decimal.Parse($"{r["PORC_SAM"]}")
                        ,
                            SAM_OPERARIOS = decimal.Parse($"{r["SAM_OPERARIOS"]}")
                        ,
                            TOTAL_SAM = decimal.Parse($"{r["TOTAL_SAM"]}")
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


        public tbl_kpi_tickets_escaneados_x_dia_Operaciones()
        {
            LINEA = string.Empty;
            TICKETS = 0;
            TOTAL_SAM = 0;
            PORC_SAM = 0;
            PORC_OPERARIOS = 0;
            SAM_OPERARIOS = 0;
            EFICIENCIA = 0;
            CANTIDAD = 0;
            OPERARIOS = 0;
            dtFecha = System.DateTime.Now;
            OPERDESCR = string.Empty;
            OPERNO = string.Empty;
            UBICACION = string.Empty;
            STATUS = "";
        }

        public void Dispose()
        {
        }
    }
}