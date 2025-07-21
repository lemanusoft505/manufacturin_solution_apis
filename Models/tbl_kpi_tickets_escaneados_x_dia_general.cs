using manufacturin_solution_apis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace manufacturin_solution_apis.Models
{
    public class tbl_kpi_tickets_escaneados_x_dia_general : IDisposable
    {
        public string PLANTA { get; set; }
        public string LINEA { get; set; }
        public string EMPNO { get; set; }
        public string OPERARIO { get; set; }
        public string UBICACION { get; set; }

        public int TICKETS { get; set; }
        public decimal TOTAL_SAM { get; set; }
        public decimal PORC_SAM { get; set; }
        public int OPERARIOS { get; set; }
        public decimal PORC_OPERARIOS { get; set; }
        public decimal SAM_OPERARIOS { get; set; }
        public decimal EFICIENCIA { get; set; }
        public DateTime dtFecha { get; set; }

        public List<tbl_kpi_tickets_escaneados_x_dia_general> grd()
        {
            List<tbl_kpi_tickets_escaneados_x_dia_general> rs = new List<tbl_kpi_tickets_escaneados_x_dia_general>();
            try
            {
                string strsql = $"EXEC intelserver.kpi_tickets_escaneados_tbl @dtFecha = {globales.sqldate(dtFecha)};";
                DataTable tbl = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    foreach (DataRow r in tbl.Rows)
                    {
                        rs.Add(new tbl_kpi_tickets_escaneados_x_dia_general()
                        {
                            PLANTA = $"{r["PLANTA"]}"
                        ,
                            TICKETS = int.Parse($"{r["TICKETS"]}")
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

        public List<tbl_kpi_tickets_escaneados_x_dia_general> grd_x_planta(string sPlanta)
        {
            List<tbl_kpi_tickets_escaneados_x_dia_general> rs = new List<tbl_kpi_tickets_escaneados_x_dia_general>();
            try
            {
                string strsql = $"EXEC intelserver.kpi_tickets_escaneados_tbl_x_Planta @planta='{globales.comillas(sPlanta)}', @dtFecha = {globales.sqldate(dtFecha)};";
                DataTable tbl = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    foreach (DataRow r in tbl.Rows)
                    {
                        rs.Add(new tbl_kpi_tickets_escaneados_x_dia_general()
                        {
                            PLANTA = $"{r["LINEA"]}"
                        ,
                            TICKETS = int.Parse($"{r["TICKETS"]}")
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

        public List<tbl_kpi_tickets_escaneados_x_dia_general> grd_x_linea(string sLinea)
        {
            List<tbl_kpi_tickets_escaneados_x_dia_general> rs = new List<tbl_kpi_tickets_escaneados_x_dia_general>();
            try
            {
                string strsql = $"EXEC intelserver.kpi_tickets_escaneados_tbl_x_Linea @Linea='{globales.comillas(sLinea)}', @dtFecha = {globales.sqldate(dtFecha)};";
                DataTable tbl = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    foreach (DataRow r in tbl.Rows)
                    {
                        rs.Add(new tbl_kpi_tickets_escaneados_x_dia_general()
                        {
                            EMPNO = $"{r["EMPNO"]}"
                        ,
                            OPERARIO = $"{r["EMPLEADO"]}"
                        ,
                            UBICACION = $"{r["UBICACION"]}"
                        ,
                            LINEA = sLinea
                        ,
                            TICKETS = int.Parse($"{r["TICKETS"]}")
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

        public tbl_kpi_tickets_escaneados_x_dia_general()
        {
            PLANTA = string.Empty;
            TICKETS = 0;
            TOTAL_SAM = 0;
            PORC_SAM = 0;
            PORC_OPERARIOS = 0;
            SAM_OPERARIOS = 0;
            EFICIENCIA = 0;
            dtFecha = System.DateTime.Now;
            LINEA = string.Empty;
            OPERARIO = string.Empty;
            EMPNO = string.Empty;
            UBICACION = string.Empty;

        }

        public void Dispose()
        {
        }
    }
}