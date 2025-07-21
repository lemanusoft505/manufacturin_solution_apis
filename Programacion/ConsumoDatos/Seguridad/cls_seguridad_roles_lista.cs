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
    public class cls_seguridad_roles_lista : IDisposable
    {
        public List<cls_seguridad_roles> cmb = new List<cls_seguridad_roles>() { };

        public void Generar()
        {
            try
            {
                cmb.Clear();
                const string strsql = "EXEC seguridad.roles_cmb;";
                DataTable tmpd = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tmpd);
                if (globales.consql.TieneDatos(tmpd))
                {
                    foreach (DataRow row in tmpd.Rows)
                    {
                        using (cls_seguridad_roles tmp = new cls_seguridad_roles())
                        {
                            tmp.num = short.Parse(row["#"].ToString());
                            tmp.Role_Descripcion = row["ROLE"].ToString();
                            cmb.Add(tmp);
                        }
                    }
                }
                tmpd.Dispose();
            }
            catch (Exception)
            { }
        }

        public cls_seguridad_roles_lista() { }

        void IDisposable.Dispose() { }
    }
}