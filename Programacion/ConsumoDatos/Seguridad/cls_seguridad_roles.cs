using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace manufacturin_solution_apis
{
    /// <summary>
    /// Leonardo Martínez Núñez
    /// lmartinez@rocedes.com.ni
    /// lemanusoft@hotmail.com
    /// 2022.06.01
    /// </summary>
    public class cls_seguridad_roles : IDisposable
    {

        public short num { get; set; }
        public string Role_Descripcion { get; set; }
        public bool Activo { get; set; }
        public string Es_Activo { get => this.Activo == true ? "YES" : "NO"; }

        private string _strError { get; set; }
        public string strError { get => _strError; }


        public List<cls_seguridad_roles> Lista()
        {
            List<cls_seguridad_roles> tmp = new List<cls_seguridad_roles>() { };
            try
            {
                string strsql = "exec seguridad.roles_Lista;";
                DataTable tbl = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        using (cls_seguridad_roles obj = new cls_seguridad_roles())
                        {
                            obj.num = short.Parse(row["#"].ToString());
                            obj.Role_Descripcion = row["Role_Descripcion"].ToString();
                            obj.Activo = bool.Parse(row["Activo"].ToString());
                            tmp.Add(obj);
                        }
                    }
                }
                else
                {
                    tmp = null;
                }
                tbl.Dispose();
            }
            catch (Exception e)
            {
                _strError = e.Message;
                tmp = null;
            }
            return tmp;
        }

        public bool Guardar(int nNum,
            string sRole_Descripcion,
            bool bActivo)
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("EXEC seguridad.roles_Guardar " +
                    "@# = {0}, " +
                    "@Role_Descripcion = '{1}', " +
                    "@Activo = '{2}';",
                    nNum,
                    globales.comillas(sRole_Descripcion),
                    bActivo.ToString());
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
                string strsql = string.Format("EXEC seguridad.roles_Recuperar @# = {0};", nNum);
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
                    this.num = short.Parse(row["#"].ToString());
                    this.Role_Descripcion = row["Role_Descripcion"].ToString();
                    this.Activo = bool.Parse(row["Activo"].ToString());
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
            this.num = 0;
            this.Role_Descripcion = string.Empty;
            this.Activo = true;
        }

        public cls_seguridad_roles()
        {
            Nuevo();
        }


        void IDisposable.Dispose()
        {
        }
    }
}