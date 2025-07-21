using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace manufacturin_solution_apis
{
    public class cls_dbo_TOLERANCIAS_INTEX_GRD : IDisposable
    {
        public int NUM { get; set; }
        public int ID_STYLE { get; set; }
        public string STYLE { get; set; }
        public int ID_PUNTO_MEDIDA { get; set; }
        public string PUNTO_MEDIDA { get; set; }
        public bool ACTIVO { get; set; }
        public int TMIN { get; set; }
        public string TOLERANCIA_MINIMA { get; set; }
        public int TMAX { get; set; }
        public string TOLERANCIA_MAXIMA { get; set; }
        public string PLATAFORMA { get; set; }

         public void Guardar() {
            try
            {
                string strsql = $"EXEC dbo.PuntoTolerancia_x_Estilo_Guardar2 " +
                    $"@Plataforma='{this.PLATAFORMA.Replace("'", "''")}'," +
                    $"@id=0," +
                    $"@idEstilo={this.ID_STYLE}," +
                    $"@idPuntoMedida={this.ID_PUNTO_MEDIDA}," +
                    $"@ValorTolMin={this.TMIN}," +
                    $"@ValorTolMax={this.TMAX}," +
                    $"@Activo='{this.ACTIVO}';";
                globales.consql.EjecutarTSQL(strsql);
            }
            catch (Exception)
            {}
        }

        public List<cls_dbo_TOLERANCIAS_INTEX_GRD> Generar_tbl() {
            List<cls_dbo_TOLERANCIAS_INTEX_GRD> tbl = new List<cls_dbo_TOLERANCIAS_INTEX_GRD>() { };
            try
            {
                string strsql = $"EXEC DBO.TOLERANCIAS_{this.PLATAFORMA}_GRD;";
                DataTable t = new DataTable();
                globales.consql.llenar_datatable(strsql,ref t);
                if (globales.consql.TieneDatos(t))
                {
                    foreach (DataRow r in t.Rows) {
                        using (cls_dbo_TOLERANCIAS_INTEX_GRD tmp = new cls_dbo_TOLERANCIAS_INTEX_GRD()
                        {
                            NUM = int.Parse($"{r["#"]}")
                            ,
                            ID_STYLE = int.Parse($"{r["ID ESTILO"]}")
                            ,
                            STYLE = $"{r["ESTILO"]}"
                            ,
                            ID_PUNTO_MEDIDA = int.Parse($"{r["ID PUNTO MEDIDA"]}")
                            ,
                            PUNTO_MEDIDA = $"{r["PUNTO MEDIDA"]}"
                            ,
                            ACTIVO = bool.Parse($"{r["ACTIVO"]}")
                            ,
                            TMIN = int.Parse($"{r["TMIN"]}")
                            ,
                            TOLERANCIA_MINIMA = $"{r["TOLERANCIA MINIMA"]}"
                            ,
                            TMAX = int.Parse($"{r["TMAX"]}")
                            ,
                            TOLERANCIA_MAXIMA = $"{r["TOLERANCIA MAXIMA"]}"
                            , 
                            PLATAFORMA = this.PLATAFORMA
                        }) {
                            tbl.Add(tmp);
                        }
                    }
                }
                else 
                    tbl = null;
                t.Dispose();
            }
            catch (Exception)
            {
                tbl = null;
            }
            return tbl;
        }

        public cls_dbo_TOLERANCIAS_INTEX_GRD()
        {
            NUM = 0;
            ID_STYLE = 0;
            STYLE = "";
            ID_PUNTO_MEDIDA = 0;
            PUNTO_MEDIDA = "";
            ACTIVO = false;
            TMIN = 0;
            TOLERANCIA_MINIMA = "";
            TMAX = 0;
            TOLERANCIA_MAXIMA = "";
            this.PLATAFORMA = "INTEX";
        }

        public void Dispose()
        {
        }
    }
}