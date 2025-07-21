using System;
using System.Collections.Generic;
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
    public class cls_seguridad_usuarios_lista : IDisposable
    {
        public List<cls_seguridad_usuario> cmb = new List<cls_seguridad_usuario> { };

        public void Generar()
        {
            try
            {
                cmb.Clear();
                const string strsql = "EXEC seguridad.usuarios_grd;";
                DataTable tmpd = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tmpd);
                if (globales.consql.TieneDatos(tmpd))
                {
                    foreach (DataRow row in tmpd.Rows)
                    {
                        using (cls_seguridad_usuario tmpUser = new cls_seguridad_usuario())
                        {
                            tmpUser.num = int.Parse(row["#"].ToString());
                            tmpUser.Usuario = row["USUARIO"].ToString();
                            tmpUser.Nombre = row["NOMBRE"].ToString();
                            tmpUser.Clave = globales.Desencriptar(row["CLAVE"].ToString());
                            tmpUser.Activo = bool.Parse(row["ACTIVO"].ToString());
                            tmpUser.FH_registro = DateTime.Parse(row["REGISTRO"].ToString());
                            tmpUser.Role = row["ROLE"].ToString();
                            tmpUser.Ultima_sesión = row["ULTIMA SESION"].ToString();
                            cmb.Add(tmpUser);
                        }

                    }
                }
                tmpd.Dispose();
            }
            catch (Exception)
            { }
        }

        public cls_seguridad_usuarios_lista()
        {
        }

        void IDisposable.Dispose()
        {

        }
    }
}