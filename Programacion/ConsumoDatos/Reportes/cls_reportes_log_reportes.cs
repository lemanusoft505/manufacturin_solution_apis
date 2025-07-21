using System;
using System.Data;
using System.Linq;

namespace manufacturin_solution_apis
{
    /// <summary>
    /// Leonardo Martínez Núñez
    /// lmartinez@rocedes.com.ni
    /// lemanusoft@hotmail.com
    /// 2022.06.20
    /// </summary>
    public class cls_reportes_log_reportes : IDisposable
    {
        #region Atributos
        public System.Guid IDLog { get; set; }
        public int numreporte { get; set; }
        public DateTime FH { get; set; }
        public int IdUsuario { get; set; }
        public string EnviadoPor { get; set; }
        public bool Enviado { get; set; }
        public string Resultado { get; set; }
        public string Título { get; set; }
        public string Destinatarios { get; set; }
        public string strsql { get; set; }
        public string Contenido { get; set; }

        private string _strError { get; set; }
        public string strError { get => _strError; }
        #endregion

        private string sincomillas(string sTexto) { return globales.comillas(sTexto); }

        public bool Guardar(int nReporte,
            int nIdUsuario,
            string sEnviadoPor,
            bool bEnviado,
            string sResultado,
            string sTítulo,
            string sDestinatarios,
            string sstrsql,
            string sContenido)
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("exec reportes.log_reportes_guardar " +
                    "@IDLog = NULL," +
                    "@#reporte = {0}," +
                    "@IdUsuario = {1}," +
                    "@EnviadoPor = '{2}'," +
                    "@Enviado = '{3}'," +
                    "@Resultado = '{4}'," +
                    "@Título = '{5}'," +
                    "@Destinatarios = '{6}'," +
                    "@strsql = '{7}'," +
                    "@Contenido = '{8}';",
                    nReporte,
                    nIdUsuario,
                    sincomillas(sEnviadoPor),
                    bEnviado,
                    sincomillas(sResultado),
                    sincomillas(sTítulo),
                    sincomillas(sDestinatarios),
                    sincomillas(sstrsql),
                    sincomillas(sContenido));
                int rn = 0;
                globales.consql.ejecutar_int(strsql, ref rn);
                rs = Recuperar(rn);
            }
            catch (Exception e)
            {
                rs = false;
                _strError = e.Message;
            }
            return rs;
        }

        public bool Recuperar(int nNum)
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("exec reportes.log_reportes_recuperar @IDLog='{0}';", nNum);
                DataTable tbl = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    rs = this.Recuperar(tbl);
                }
                tbl.Dispose();
            }
            catch (Exception e)
            {
                rs = false;
                _strError = e.Message;
            }
            return rs;
        }

        public bool Recuperar(DataTable tbl)
        {
            bool rs = false;
            _strError = string.Empty;
            try
            {
                foreach (DataRow row in tbl.Rows)
                {
                    this.IDLog = System.Guid.Parse(row["IDLog"].ToString());
                    this.numreporte = int.Parse(row["numreporte"].ToString());
                    this.FH = DateTime.Parse(row["FH"].ToString());
                    this.IdUsuario = int.Parse(row["IdUsuario"].ToString());
                    this.EnviadoPor = row["EnviadoPor"].ToString();
                    this.Enviado = bool.Parse(row["Enviado"].ToString());
                    this.Resultado = row["Resultado"].ToString();
                    this.Título = row["Título"].ToString();
                    this.Destinatarios = row["Destinatarios"].ToString();
                    this.strsql = row["strsql"].ToString();
                    this.Contenido = row["Contenido"].ToString();
                    rs = true;
                }
            }
            catch (Exception e)
            {
                _strError = e.Message;
                rs = false;
            }
            return rs;
        }

        public void Nuevo()
        {
            _strError = string.Empty;
            this.IDLog = System.Guid.NewGuid();
            this.numreporte = 0;
            this.FH = DateTime.Now;
            this.IdUsuario = 0;
            this.EnviadoPor = string.Empty;
            this.Enviado = false;
            this.Resultado = string.Empty;
            this.Título = string.Empty;
            this.Destinatarios = string.Empty;
            this.strsql = string.Empty;
            this.Contenido = string.Empty;
        }

        public cls_reportes_log_reportes()
        {
            Nuevo();
        }

        void IDisposable.Dispose()
        {
        }
    }
}