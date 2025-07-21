using System;
using System.Collections.Generic;
using System.Data;

namespace manufacturin_solution_apis
{
    /// <summary>
    /// Leonardo Martínez Núñez
    /// lmartinez@rocedes.com.ni
    /// lemanusoft@hotmail.com
    /// 2022.06.17
    /// </summary>
    public class cls_dbo_Style_types : IDisposable
    {
        #region Atributos

        public short Num { get; set; }
        public string Stylye_Type { get; set; }
        public string Type_Description { get; set; }

        private string _strError { get; set; }
        public string strError { get => _strError; }
        #endregion

        #region Métodos
        private string sincomillas(string sTexto) { return globales.comillas(sTexto); }

        public bool Eliminar(short nNum)
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("exec dbo.Style_types_Eliminar @#={0};", nNum);
                rs = globales.consql.EjecutarTSQLBool(strsql);
            }
            catch (Exception e)
            {
                rs = false;
                _strError = e.Message;
            }
            return rs;

        }

        public bool Guardar(short nNum,
            string sStylye_Type,
            string sType_Description)
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("exec dbo.Style_types_guardar @#={0}, " +
                    "@Stylye_Type = '{1}'," +
                    "@Type_Description = '{2}';",
                    nNum,
                    sincomillas(sStylye_Type),
                    sincomillas(sType_Description));
                short rn = 0;
                globales.consql.ejecutar_short(strsql, ref rn);
                rs = Recuperar(rn);
            }
            catch (Exception e)
            {
                rs = false;
                _strError = e.Message;
            }
            return rs;
        }

        public bool Recuperar(short nNum)
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("exec dbo.Style_types_Recuperar @#={0};", nNum);
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
                    this.Num = short.Parse(row["num"].ToString());
                    this.Stylye_Type = row["Stylye_Type"].ToString();
                    this.Type_Description = row["Type_Description"].ToString();
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
            this.Num = 0;
            this.Stylye_Type = string.Empty;
            this.Type_Description = string.Empty;
        }


        public List<cls_dbo_Style_types> cmb()
        {
            List<cls_dbo_Style_types> lista = new List<cls_dbo_Style_types>() { };
            try
            {
                string strsql = "EXEC dbo.Style_types_cmb;";
                DataTable dt = new DataTable();
                globales.consql.llenar_datatable(strsql, ref dt);
                if (globales.consql.TieneDatos(dt))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        using (cls_dbo_Style_types tmp = new cls_dbo_Style_types())
                        {

                            tmp.Stylye_Type = row["Stylye_Type"].ToString();
                            tmp.Num = short.Parse(row["#"].ToString());
                            lista.Add(tmp);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _strError = e.Message;
                lista = null;
            }
            return lista;
        }
        #endregion

        public cls_dbo_Style_types()
        {
        }

        void IDisposable.Dispose()
        {
        }
    }
}