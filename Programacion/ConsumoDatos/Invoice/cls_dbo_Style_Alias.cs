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
    /// 2022.08.31
    /// </summary>
    public class cls_dbo_Style_Alias : IDisposable
    {
        #region Atributos

        public int Id_Style { get; set; }
        public string Style { get; set; }
        public string Style_Alias { get; set; }
        public string Descripción { get; set; }
        public string pkrow { get; set; }

        private string _strError { get; set; }
        public string strError { get => _strError; }
        #endregion

        #region Métodos

        private string sincomillas(string sTexto) { return globales.comillas(sTexto); }

        public bool Guardar(int nId_Style,
            string sStyle,
            string sStyle_Alias,
            string sDescripción,
            string spkwrow)
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("EXEC Auditoria.dbo.Style_Alias_Guardar " +
                    "@Style = '{0}', " +
                    "@Style_Alias = '{1}', " +
                    "@Descripción = '{2}', " +
                    "@pkwrow='{3}';",
                    sincomillas(sStyle.Trim()),
                    sincomillas(sStyle_Alias.Trim()),
                    sincomillas(sDescripción.Trim()),
                    spkwrow);
                int rn = 0;
                globales.consql.ejecutar_int(strsql, ref rn);
                rs = Recuperar(sStyle, sStyle_Alias);
            }
            catch (Exception e)
            {
                rs = false;
                _strError = e.Message;
            }
            return rs;
        }

        public bool Guardar()
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("EXEC Auditoria.dbo.Style_Alias_Guardar " +
                    "@Style = '{0}', " +
                    "@Style_Alias = '{1}', " +
                    "@Descripción = '{2}', " +
                    "@pkwrow='{3}';",
                    sincomillas(Style.Trim()),
                    sincomillas(Style_Alias.Trim()),
                    sincomillas(Descripción.Trim()),
                    pkrow);
                int rn = 0;
                globales.consql.ejecutar_int(strsql, ref rn);
                rs = Recuperar(Style, Style_Alias);
            }
            catch (Exception e)
            {
                rs = false;
                _strError = e.Message;
            }
            return rs;
        }

        public bool Eliminar()
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("EXEC Auditoria.dbo.Style_Alias_Eliminar " +
                    "@Style = '{0}', " +
                    "@Style_Alias = '{1}';",
                    sincomillas(Style.Trim()),
                    sincomillas(Style_Alias.Trim()));
                globales.consql.EjecutarTSQL(strsql);
                rs = !Recuperar(Style, Style_Alias);
            }
            catch (Exception e)
            {
                rs = false;
                _strError = e.Message;
            }
            return rs;
        }

        public bool Recuperar(string sStyle,
            string sStyle_Alias)
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("EXEC dbo.Style_Alias_Recuperar " +
                    "@style = '{0}', " +
                    "@Style_Alias='{1}';",
                    sincomillas(sStyle.Trim()),
                    sincomillas(sStyle_Alias.Trim()));
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

        public List<cls_dbo_Style_Alias> RecuperarCmb(string sStyle)
        {
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("EXEC dbo.Style_alias_recuperarCMB @style = '{0}';", sincomillas(sStyle.TrimEnd().TrimStart()));
                DataTable tbl = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    return RecuperarCmb(tbl);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                _strError = e.Message;
                return null;
            }
        }

        public bool Recuperar(DataTable tbl)
        {
            bool rs = false;
            _strError = string.Empty;
            try
            {
                foreach (DataRow row in tbl.Rows)
                {
                    this.Id_Style = int.Parse(row["Id_Style"].ToString());
                    this.Style = row["Style"].ToString();
                    this.Style_Alias = row["Style_Alias"].ToString();
                    this.Descripción = row["Descripción"].ToString();
                    this.pkrow = row["pkrow"].ToString();
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

        public List<cls_dbo_Style_Alias> RecuperarCmb(DataTable tbl)
        {
            List<cls_dbo_Style_Alias> cmb = new List<cls_dbo_Style_Alias>() { };
            _strError = string.Empty;
            try
            {
                foreach (DataRow row in tbl.Rows)
                {
                    using (cls_dbo_Style_Alias tmp = new cls_dbo_Style_Alias())
                    {
                        tmp.Id_Style = int.Parse(row["Id_Style"].ToString());
                        tmp.Style = row["Style"].ToString();
                        tmp.Style_Alias = row["Style_Alias"].ToString();
                        tmp.Descripción = row["Descripción"].ToString();
                        tmp.pkrow = row["pkrow"].ToString();
                        cmb.Add(tmp);
                    }
                }
            }
            catch (Exception e)
            {
                _strError = e.Message;
            }
            return cmb;
        }

        public void Nuevo()
        {
            _strError = string.Empty;
            this.Id_Style = 0;
            this.Style = string.Empty;
            this.Style_Alias = string.Empty;
            this.Descripción = string.Empty;
            this.pkrow = string.Empty;
        }

        public cls_dbo_Style_Alias()
        {
            Nuevo();
        }

        void IDisposable.Dispose()
        {
        }
        #endregion

    }
}