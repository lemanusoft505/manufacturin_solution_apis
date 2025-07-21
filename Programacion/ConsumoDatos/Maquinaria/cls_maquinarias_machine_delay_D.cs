using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace manufacturin_solution_apis
{
    public class cls_maquinarias_machine_delay_D : IDisposable
    {
        #region Atributos

        public int num { get; set; }
        public DateTime Fecha { get; set; }
        public string Usuario { get; set; }
        public string Estado { get; set; }
        public string EmpNo_Reportado_Por { get; set; }
        public string EmpName_Reportado_Por { get; set; }
        public string EmpNo_Operario { get; set; }
        public string EmpName_Operario { get; set; }
        public byte Planta { get; set; }
        public short Línea { get; set; }
        public string Sección { get; set; }
        public short Fila { get; set; }
        public short Columna { get; set; }
        public string ID { get; set; }
        public string NameMachine { get; set; }
        public int IdMachine { get; set; }
        public string Tipo_Maquina { get; set; }
        public byte numTipoProblema { get; set; }
        public short numProblema { get; set; }
        public string Problema { get; set; }
        public string Comentarios { get; set; }
        public string FH_Inicia { get; set; }
        public string FH_Asigna { get; set; }
        public string FH_Prueba { get; set; }
        public string FH_Termina { get; set; }
        public string FH_Entrega { get; set; }
        public string Usuario_Gestiona { get; set; }
        public string EmpNo_Técnico_Asignado { get; set; }
        public string EmpName_Técnico_Asignado { get; set; }
        public string Estado_Final { get; set; }
        public decimal Minutos_Total_Transcurridos { get; set; }
        public int Unidades_Perdidas { get; set; }
        public string Comentarios_Técnico { get; set; }

        private string _strError { get; set; }
        public string strError { get => _strError; }
        string TablaDatos { get { return "maquinarias.machine_delay_D"; } }
        #endregion

        #region Procedimientos

        private string sincomillas(string sTexto) { return globales.comillas(sTexto); }


        public bool Guardar(int nNum,
            DateTime dFecha,
            string sEstado,
            string sEmpNo_Reportado_Por,
            string sEmpName_Reportado_Por,
            string sEmpNo_Operario,
            string sEmpName_Operario,
            byte nPlanta,
            short nLínea,
            string sSección,
            short nFila,
            short nColumna,
            string sID,
            string sNameMachine,
            int nIdMachine,
            string sTipo_Maquina,
            byte nTipoProblema,
            short nProblema,
            string sProblema,
            string sComentarios)
        {
            bool rs = false;
            try
            {
                _strError = "";
                string strsql = string.Format("exec {0}_guardar " +
                    "@#={1}, " +
                    "@Usuario = '{3}'," +
                    "@Estado = '{4}'," +
                    "@EmpNo_Reportado_Por = '{5}'," +
                    "@EmpName_Reportado_Por = '{6}'," +
                    "@EmpNo_Operario = '{7}'," +
                    "@EmpName_Operario = '{8}'," +
                    "@Planta = {9}," +
                    "@Línea = {10}," +
                    "@Sección = '{11}'," +
                    "@Fila = {12}," +
                    "@Columna = {13}," +
                    "@ID = '{14}'," +
                    "@NameMachine = '{15}'," +
                    "@IdMachine = {16}," +
                    "@Tipo_Maquina = '{17}'," +
                    "@#TipoProblema = {18}," +
                    "@#Problema = {19}," +
                    "@Problema = '{20}'," +
                    "@Comentarios = '{21}';",
                    this.TablaDatos,
                    nNum,
                    globales.sqldatetime(Fecha),
                    sincomillas(globales.objSesión.objUsuario.Usuario),
                    sincomillas(sEstado),
                    sincomillas(sEmpNo_Reportado_Por),
                    sincomillas(sEmpName_Reportado_Por),
                    sincomillas(sEmpNo_Operario),
                    sincomillas(sEmpName_Operario),
                    nPlanta,
                    nLínea,
                    sincomillas(sSección),
                    nFila,
                    nColumna,
                    sincomillas(sID),
                    sincomillas(sNameMachine),
                    nIdMachine,
                    sincomillas(sTipo_Maquina),
                    nTipoProblema,
                    nProblema,
                    sincomillas(sProblema),
                    sincomillas(sComentarios));
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

        public void Avoid(int nNum)
        {
            try
            {
                string strsql = $"Exec maquinarias.machine_delay_D_Avoid @#= {nNum}";
                globales.consql.EjecutarTSQL(strsql);
            }
            catch (Exception ex)
            {
                _strError = ex.Message;
            }
        }

        public void Canceled(int nNum)
        {
            try
            {
                string strsql = $"Exec maquinarias.machine_delay_D_Canceled @#= {nNum}";
                globales.consql.EjecutarTSQL(strsql);
            }
            catch (Exception ex)
            {
                _strError = ex.Message;
            }
        }

        public bool Recuperar(int nNum)
        {
            bool rs = false;
            try
            {
                _strError = "";
                string strsql = string.Format("exec {0}_recuperar @#={1};", this.TablaDatos, nNum);
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
            _strError = "";
            try
            {
                foreach (DataRow row in tbl.Rows)
                {
                    this.num = int.Parse(row["#"].ToString());
                    this.Fecha = DateTime.Parse(row["Fecha"].ToString());
                    this.Usuario = row["Usuario"].ToString();
                    this.Estado = row["Estado"].ToString();
                    this.EmpNo_Reportado_Por = row["EmpNo_Reportado_Por"].ToString();
                    this.EmpName_Reportado_Por = row["EmpName_Reportado_Por"].ToString();
                    this.EmpNo_Operario = row["EmpNo_Operario"].ToString();
                    this.EmpName_Operario = row["EmpName_Operario"].ToString();
                    this.Planta = byte.Parse(row["Planta"].ToString());
                    this.Línea = short.Parse(row["Línea"].ToString());
                    this.Sección = row["Sección"].ToString();
                    this.Fila = short.Parse(row["Fila"].ToString());
                    this.Columna = short.Parse(row["Columna"].ToString());
                    this.ID = row["ID"].ToString();
                    this.NameMachine = row["NameMachine"].ToString();
                    this.IdMachine = int.Parse(row["IdMachine"].ToString());
                    this.Tipo_Maquina = row["Tipo_Maquina"].ToString();
                    this.numTipoProblema = byte.Parse(row["#TipoProblema"].ToString());
                    this.numProblema = short.Parse(row["#Problema"].ToString());
                    this.Problema = row["Problema"].ToString();
                    this.Comentarios = row["Comentarios"].ToString();
                    this.FH_Inicia = row["FH_Inicia"].ToString();
                    this.FH_Asigna = row["FH_Asigna"].ToString();
                    this.FH_Prueba = row["FH_Prueba"].ToString();
                    this.FH_Termina = row["FH_Termina"].ToString();
                    this.FH_Entrega = row["FH_Entrega"].ToString();
                    this.Usuario_Gestiona = row["Usuario_Gestiona"].ToString();
                    this.EmpNo_Técnico_Asignado = row["EmpNo_Técnico_Asignado"].ToString();
                    this.EmpName_Técnico_Asignado = row["EmpName_Técnico_Asignado"].ToString();
                    this.Estado_Final = row["Estado_Final"].ToString();
                    this.Minutos_Total_Transcurridos = decimal.Parse(row["Minutos_Total_Transcurridos"].ToString());
                    this.Unidades_Perdidas = int.Parse(row["Unidades_Perdidas"].ToString());
                    this.Comentarios_Técnico = row["Comentarios_Técnico"].ToString();
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
            _strError = "";
            this.num = 0;
            this.Fecha = DateTime.Now;
            this.Usuario = "";
            this.Estado = "Pend";
            this.EmpNo_Reportado_Por = "";
            this.EmpName_Reportado_Por = "";
            this.EmpNo_Operario = "";
            this.EmpName_Operario = "";
            this.Planta = 0;
            this.Línea = 0;
            this.Sección = "";
            this.Fila = 0;
            this.Columna = 0;
            this.ID = "";
            this.NameMachine = "";
            this.IdMachine = 0;
            this.Tipo_Maquina = "";
            this.numTipoProblema = 0;
            this.numProblema = 0;
            this.Problema = "";
            this.Comentarios = "";
            this.FH_Inicia = "";
            this.FH_Asigna = "";
            this.FH_Prueba = "";
            this.FH_Termina = "";
            this.FH_Entrega = "";
            this.Usuario_Gestiona = "";
            this.EmpNo_Técnico_Asignado = "";
            this.EmpName_Técnico_Asignado = "";
            this.Estado_Final = "";
            this.Minutos_Total_Transcurridos = 0;
            this.Unidades_Perdidas = 0;
            this.Comentarios_Técnico = "";
        }


        #endregion

        public cls_maquinarias_machine_delay_D()
        {
        }

        public void Dispose()
        {
        }
    }
}