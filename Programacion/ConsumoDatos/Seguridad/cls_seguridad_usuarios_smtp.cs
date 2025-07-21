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
    public class cls_seguridad_usuarios_smtp : IDisposable
    {
        public int num { get; set; }
        public string server_smtp { get; set; }
        public int server_port { get; set; }
        public bool server_ssl { get; set; }
        public string server_login { get; set; }
        public string server_password { get; set; }

        public cls_seguridad_usuarios_smtp()
        {
            Nuevo();
        }



        public bool Guardar(int nNum,
string sserver_smtp,
int nserver_port,
bool bserver_ssl,
string sserver_login,
string sserver_password)
        {
            try
            {
                bool rs = false;
                string strsql = string.Format("EXEC seguridad.usuarios_Guardar " +
                    "@# = {0}," +
                    "@server_smtp = '{1}'," +
                    "@server_port = {2}," +
                    "@server_ssl = '{3}'," +
                    "@server_login = '{4}'," +
                    "@server_password = '{5}';",
                    nNum,
                    globales.comillas(sserver_smtp),
                    nserver_port,
                    bserver_ssl.ToString(),
                    globales.comillas(sserver_login),
                    globales.comillas(globales.Encriptar(sserver_password)));
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
                string strsql = string.Format("EXEC seguridad.usuarios_smtp_Recuperar @# = {0};", nNum);
                DataTable tbl = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        this.num = int.Parse(row["#"].ToString());
                        this.server_smtp = row["server_smtp"].ToString();
                        this.server_port = int.Parse(row["server_port"].ToString());
                        this.server_ssl = bool.Parse(row["server_ssl"].ToString());
                        this.server_login = row["server_login"].ToString();
                        this.server_password = row["server_password"].ToString();
                        if (this.server_password.Trim().Length > 0)
                        {
                            this.server_password = globales.Desencriptar(this.server_password);
                        }
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
            this.server_smtp = string.Empty;
            this.server_port = 0;
            this.server_ssl = false;
            this.server_login = string.Empty;
            this.server_password = string.Empty;
        }

        void IDisposable.Dispose()
        {

        }
    }
}