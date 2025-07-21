using System;
using System.Data;
using System.Linq;

namespace manufacturin_solution_apis
{
    /// <summary>
    /// Leonardo Martínez Núñez
    /// lmartinez@rocedes.com.ni
    /// ROCEDES, S.A.
    /// 02 de junio 2022
    /// </summary>
    public class cls_sistema_recursos : IDisposable
    {
        public int num { get; set; }
        public byte numtipo { get; set; }
        public string tipo_recurso { get; set; }
        public string usuario { get; set; }
        public string título { get; set; }
        public DateTime fecha { get; set; }
        public string archivo { get; set; }
        public string libro_xlsx { get; set; }
        public string tabla { get; set; }
        public string strError { get; set; }
        public bool Es_Aplicado_POStatus { get; set; }
        public string sFecha
        {
            get
            {
                return string.Format("{0:yyyy-MM-dd}", this.fecha);
            }
        }

        public bool Guardar(int nNum,
byte ntipo,
string susuario,
string stítulo,
DateTime dfecha,
string sarchivo,
string slibro_xlsx,
string stabla)
        {
            bool rs = false;
            this.strError = string.Empty;
            try
            {
                string strsql = string.Format("set dateformat ymd; EXEC Sistema.recursos_Guardar " +
                    "@# = {0}," +
"@#tipo = {1}," +
"@usuario = '{2}'," +
"@título = '{3}'," +
"@fecha = {4}," +
"@archivo = '{5}'," +
"@libro_xlsx = '{6}'," +
"@tabla = '{7}';",
            nNum,
            ntipo,
            globales.comillas(susuario),
            globales.comillas(stítulo),
            globales.sqldatetime(dfecha),
            globales.comillas(sarchivo),
            globales.comillas(slibro_xlsx),
            globales.comillas(stabla));
                int rn = 0;
                globales.consql.ejecutar_int(strsql, ref rn);
                if (rn > 0)
                {
                    rs = this.Recuperar(rn);
                }
            }
            catch (Exception e)
            {
                this.strError = e.Message;
            }
            return rs;
        }

        public bool Recuperar(int nNum)
        {
            bool rs = false;
            strError = string.Empty;
            try
            {
                string strsql = string.Format("EXEC Sistema.recursos_Recuperar @# = {0};", nNum);
                DataTable tbl = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        this.num = int.Parse(row["#"].ToString());
                        this.numtipo = byte.Parse(row["#tipo"].ToString());
                        this.tipo_recurso = row["tipo_recurso"].ToString();
                        this.usuario = row["usuario"].ToString();
                        this.título = row["título"].ToString();
                        this.fecha = DateTime.Parse(row["fecha"].ToString());
                        this.archivo = row["archivo"].ToString();
                        this.libro_xlsx = row["libro_xlsx"].ToString();
                        this.tabla = row["tabla"].ToString();
                        this.Es_Aplicado_POStatus = bool.Parse(row["Es_Aplicado_POStatus"].ToString());
                        rs = true;
                    }
                }
                tbl.Dispose();
            }
            catch (Exception e)
            {
                rs = false;
                this.strError = e.Message;
            }
            return rs;
        }

        public cls_sistema_recursos()
        {
            Nuevo();
        }

        public void Nuevo()
        {
            this.num = 0;
            this.numtipo = 0;
            this.tipo_recurso = string.Empty;
            this.usuario = string.Empty;
            this.título = string.Empty;
            this.fecha = DateTime.Now;
            this.archivo = string.Empty;
            this.libro_xlsx = string.Empty;
            this.tabla = string.Empty;
            this.strError = string.Empty;
            this.Es_Aplicado_POStatus = false;
        }

        void IDisposable.Dispose()
        {
        }
    }
}