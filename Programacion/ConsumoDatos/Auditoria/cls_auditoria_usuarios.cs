using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace manufacturin_solution_apis
{
    public class cls_auditoria_usuarios : IDisposable
    {
        public string ID { get; set; }
        public string SISTEMA { get; set; }
        public string USUARIO { get; set; }
        public string CONTRASEÑA { get; set; }
        public bool ES_ENCRIPTADA { get; set; }
        public string EMAIL { get; set; }
        public bool BLOQUEADA { get; set; }
        public int INTENTOS_FALLIDOS { get; set; }

        public void Guardar()
        {
            try
            {
                string strsql = $"EXEC dbo.usuarios_grd_portal_guardar " +
                    $"@ID='{this.ID}', " +
                    $"@SISTEMA='{this.SISTEMA.Replace("'", "''")}', " +
                    $"@USUARIO='{this.USUARIO.Replace("'", "''")}', " +
                    $"@CONTRASEÑA='{this.CONTRASEÑA.Replace("'", "''")}', " +
                    $"@ES_ENCRIPTADA='{this.ES_ENCRIPTADA.ToString()}', " +
                    $"@EMAIL='{this.EMAIL.Replace("'", "''")}', " +
                    $"@BLOQUEADA='{this.BLOQUEADA.ToString()}', " +
                    $"@INTENTOS_FALLIDOS={this.INTENTOS_FALLIDOS};";
                globales.consql.EjecutarTSQL(strsql);
            }
            catch (Exception)
            { }
        }

        public List<cls_auditoria_usuarios> RecuperarCmb()
        {
            try
            {
                List<cls_auditoria_usuarios> lst = new List<cls_auditoria_usuarios>() { };
                DataTable tbl = new DataTable();
                string strsql = "EXEC dbo.usuarios_grd_portal;";
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        using (cls_auditoria_usuarios tmp = new cls_auditoria_usuarios()
                        {
                            ID = row["ID"].ToString()
                            ,
                            SISTEMA = row["SISTEMA"].ToString()
                            ,
                            USUARIO = row["USUARIO"].ToString()
                            ,
                            CONTRASEÑA = row["CONTRASEÑA"].ToString()
                            ,
                            EMAIL = row["EMAIL"].ToString()
                            ,
                            INTENTOS_FALLIDOS = int.Parse(row["INTENTOS FALLIDOS"].ToString())
                            ,
                            BLOQUEADA = bool.Parse(row["BLOQUEADA"].ToString())
                            ,
                            ES_ENCRIPTADA = int.Parse(row["ES ENCRIPTADA"].ToString()) == 1
                        })
                        {
                            lst.Add(tmp);
                        }
                    }
                }
                tbl = null;
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public cls_auditoria_usuarios()
        {
            Guid gid = new Guid();
            gid = System.Guid.Empty;
            this.ID = gid.ToString();
            this.SISTEMA = string.Empty;
            this.USUARIO = string.Empty;
            this.CONTRASEÑA = string.Empty;
            this.ES_ENCRIPTADA = false;
            this.EMAIL = string.Empty;
            this.BLOQUEADA = false;
            this.INTENTOS_FALLIDOS = 0;
        }

        public void Dispose()
        { }
    }
}