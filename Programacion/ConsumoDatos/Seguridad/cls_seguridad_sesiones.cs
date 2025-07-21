using System;
using System.Data;

/// <summary>
/// Leonardo Martínez Núñez
/// lmartinez@rocedes.com.ni
/// ROCEDES, S.A.
/// 25 de Abril 2022
/// </summary>
namespace manufacturin_solution_apis
{
    public class cls_seguridad_sesiones : IDisposable
    {

        #region Atributos

        public System.Guid sesión { get; set; }
        public int numUsuario { get; set; }
        public string Usuario { get; set; }
        public short numrole { get; set; }
        public bool activa { get; set; }
        public DateTime FH_Inicia { get; set; }
        public DateTime FH_Válida_Hasta { get; set; }
        public string PC_nombre { get; set; }
        public string IP_V4 { get; set; }
        public string App { get; set; }
        public string Versión { get; set; }
        public cls_seguridad_usuario objUsuario { get; set; }

        #endregion

        public void Finalizar(string sSesión)
        {
            try
            {
                string strsql = string.Format("EXEC seguridad.sesiones_activas_Finalizar @sesión = '{0}';", globales.comillas(sSesión));
                globales.consql.EjecutarTSQL(strsql);
            }
            catch (Exception)
            { }
        }

        public void Mantener_Viva(string sSesión)
        {
            try
            {
                string strsql = string.Format("EXEC seguridad.sesiones_activas_MantenerActiva @sesión = '{0}', @minutos = {1};", globales.comillas(sSesión.ToString()), globales.mssql_minutos_sesion_activa);
                globales.consql.EjecutarTSQL(strsql);
            }
            catch (Exception)
            {

            }
        }

        public bool Recuperar(string sSesión)
        {
            try
            {
                bool rs = false;
                string strsql = string.Format("EXEC seguridad.sesiones_activas_Recuperar @sesión = '{0}';", globales.comillas(sSesión));
                DataTable tbl = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        this.sesión = System.Guid.Parse(row["sesión"].ToString());
                        this.numUsuario = int.Parse(row["#Usuario"].ToString());
                        this.Usuario = row["Usuario"].ToString();
                        this.numrole = short.Parse(row["#role"].ToString());
                        this.activa = bool.Parse(row["activa"].ToString());
                        this.FH_Inicia = DateTime.Parse(row["FH_Inicia"].ToString());
                        this.FH_Válida_Hasta = DateTime.Parse(row["FH_Válida_Hasta"].ToString());
                        this.PC_nombre = row["PC_nombre"].ToString();
                        this.IP_V4 = row["IP_V4"].ToString();
                        this.App = row["App"].ToString();
                        this.Versión = row["Versión"].ToString();
                        this.objUsuario.Recuperar(this.numUsuario);
                        rs = true;

                    }
                }
                tbl.Dispose();

                return rs;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Iniciar_Sesión(string sUsuario, string sClave, string sPc_Nombre, string sIpV4)
        {
            try
            {
                bool rs = false;
                string strsql = string.Format("EXEC seguridad.usuarios_IniciarSesión " +
"@Usuario = '{0}', " +
"@Clave = '{1}', " +
"@PC_nombre = '{2}', " +
"@IP_V4 = '{3}', " +
"@App = '{4}', " +
"@Versión = '{5}'; ",
                    globales.comillas(sUsuario),
                    globales.comillas(sClave),
                    globales.comillas(sPc_Nombre),
                    globales.comillas(sIpV4),
                    globales.comillas(globales.mssql_App),
                    globales.comillas(globales.versionApp));
                DataTable tbl = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        this.sesión = System.Guid.Parse(row["sesión"].ToString());
                        if (this.objUsuario.Recuperar(int.Parse(row["#Usuario"].ToString())))
                        {
                            this.numUsuario = this.objUsuario.num;
                            this.Usuario = this.objUsuario.Usuario;
                            this.numrole = this.objUsuario.numRole;

                        }
                        this.activa = false;
                        this.FH_Inicia = DateTime.Parse(row["FH_Inicia"].ToString());
                        this.FH_Válida_Hasta = DateTime.Parse(row["FH_Válida_Hasta"].ToString());

                        rs = true;
                    }
                }
                tbl.Dispose();

                return rs;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Nuevo()
        {
            this.sesión = System.Guid.Empty;
            this.numUsuario = 0;
            this.Usuario = string.Empty;
            this.numrole = 0;
            this.activa = false;
            this.FH_Inicia = DateTime.Now;
            this.FH_Válida_Hasta = DateTime.Now;
            this.PC_nombre = string.Empty;
            this.IP_V4 = string.Empty;
            this.App = string.Empty;
            this.Versión = string.Empty;
            if (this.objUsuario == null)
            {
                this.objUsuario = new cls_seguridad_usuario();
            }
            this.objUsuario.Nuevo();
        }

        public cls_seguridad_sesiones()
        {
            Nuevo();
        }

        void IDisposable.Dispose()
        {
        }
    }
}