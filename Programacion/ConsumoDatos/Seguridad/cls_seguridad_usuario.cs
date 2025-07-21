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
    public class cls_seguridad_usuario : IDisposable
    {

        #region Atributos

        public int num { get; set; }
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public short numRole { get; set; }
        public bool Activo { get; set; }
        public bool Es_Debe_Cambiar_Clave { get; set; }
        public string Email { get; set; }
        public string Teléfonos { get; set; }
        public string Ultima_sesión { get; set; }
        public int Sesiones { get; set; }
        public DateTime FH_registro { get; set; }

        public string Role { get; set; }
        public string Es_Activo { get => this.Activo == true ? "SI" : "NO"; }

        public cls_seguridad_usuarios_smtp objSMTP = new cls_seguridad_usuarios_smtp();
        #endregion


        
        public bool Cambiar_Clave(int nNumUsuario, string txtClave)
        {
            try
            {
                bool rs = false;
                string strsql = string.Format("EXEC seguridad.usuarios_cambiar_clave @#={0}, @Clave='{1}';",
                    nNumUsuario, globales.comillas(txtClave));
                globales.consql.EjecutarTSQL(strsql);
                rs = true;
                return rs;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Guardar(int nUsuario,
            string sUsuario,
            string sNombre,
            string sClave,
            short nRole,
            bool bActivo,
            bool bEs_Debe_Cambiar_Clave,
            string sEmail,
            string sTeléfonos)
        {
            try
            {
                bool rs = false;
                if (sClave.Trim().Length > 0)
                {
                    sClave = globales.Encriptar(sClave);
                }
                string strsql = string.Format("EXEC seguridad.usuarios_Guardar " +
                    "@# = {0}," +
                    "@Usuario = '{1}', " +
                    "@Nombre = '{2}', " +
                    "@Clave = '{3}', " +
                    "@#Role = {4}," +
                    "@Activo = '{5}', " +
                    "@Es_Debe_Cambiar_Clave = '{6}', " +
                    "@Email = '{7}', " +
                    "@Teléfonos = '{8}';",
                    nUsuario,
                    globales.comillas(sUsuario),
                    globales.comillas(sNombre),
                    globales.comillas(sClave),
                    nRole,
                    bActivo.ToString(),
                    bEs_Debe_Cambiar_Clave.ToString(),
                    globales.comillas(sEmail),
                    globales.comillas(sTeléfonos));
                int rn = 0;
                globales.consql.ejecutar_int(strsql, ref rn);
                rs = this.Recuperar(rn);
                return rs;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Recuperar(int nNum)
        {
            try
            {
                bool rs = false;
                string strsql = string.Format("EXEC seguridad.usuarios_Recuperar @# = {0};", nNum);
                DataTable tbl = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        this.num = int.Parse(row["#"].ToString());
                        this.Usuario = row["Usuario"].ToString();
                        this.Nombre = row["Nombre"].ToString();
                        this.Clave = row["Clave"].ToString();
                        this.numRole = short.Parse(row["#Role"].ToString());
                        this.Activo = bool.Parse(row["Activo"].ToString());
                        this.Es_Debe_Cambiar_Clave = bool.Parse(row["Es_Debe_Cambiar_Clave"].ToString());
                        this.Email = row["Email"].ToString();
                        this.Teléfonos = row["Teléfonos"].ToString();
                        this.Ultima_sesión = row["Ultima_sesión"].ToString();
                        this.Sesiones = int.Parse(row["Sesiones"].ToString());
                        this.FH_registro = DateTime.Parse(row["FH_registro"].ToString());

                        this.objSMTP.Recuperar(this.num);
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

        public bool Recuperar_x_usuario(string sUsuario)
        {
            try
            {
                bool rs = false;
                string strsql = string.Format("EXEC seguridad.usuarios_Recuperar_x_usuario @usuario = '{0}';", globales.comillas(sUsuario));
                DataTable tbl = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        this.num = int.Parse(row["#"].ToString());
                        this.Usuario = row["Usuario"].ToString();
                        this.Nombre = row["Nombre"].ToString();
                        this.Clave = row["Clave"].ToString();
                        this.numRole = short.Parse(row["#Role"].ToString());
                        this.Activo = bool.Parse(row["Activo"].ToString());
                        this.Es_Debe_Cambiar_Clave = bool.Parse(row["Es_Debe_Cambiar_Clave"].ToString());
                        this.Email = row["Email"].ToString();
                        this.Teléfonos = row["Teléfonos"].ToString();
                        this.Ultima_sesión = row["Ultima_sesión"].ToString();
                        this.Sesiones = int.Parse(row["Sesiones"].ToString());
                        this.FH_registro = DateTime.Parse(row["FH_registro"].ToString());

                        this.objSMTP.Recuperar(this.num);
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
            this.num = 0;
            this.Usuario = string.Empty;
            this.Nombre = string.Empty;
            this.Clave = string.Empty;
            this.numRole = 0;
            this.Activo = false;
            this.Es_Debe_Cambiar_Clave = false;
            this.Email = string.Empty;
            this.Teléfonos = string.Empty;
            this.Ultima_sesión = string.Empty;
            this.Sesiones = 0;
            this.FH_registro = DateTime.Now;
            this.Role = string.Empty;
        }

        public cls_seguridad_usuario()
        {
            Nuevo();
        }

        void IDisposable.Dispose()
        {
        }
    }
}