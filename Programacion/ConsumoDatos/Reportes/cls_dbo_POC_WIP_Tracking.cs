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
    /// 2022.09.01
    /// </summary>
    public class cls_dbo_POC_WIP_Tracking : IDisposable
    {
        //DATE	WEEK	P. ORDER	STYLE	LINE	PLANT	STYLE BASE	QUANTITY	WIP	PENDING	COVER	COMMENT
        #region Atributos
        public DateTime DATE { get; set; }
        public string WEEK { get; set; }
        public string PORDER { get; set; }
        public string STYLE { get; set; }
        public string LINE { get; set; }
        public string PLANT { get; set; }
        public string STYLE_BASE { get; set; }
        public int QUANTITY { get; set; }
        public string WIP { get; set; }
        public string PENDING { get; set; }
        public string COVER { get; set; }
        public string COMMENT { get; set; }


        private string _strError { get; set; }
        public string strError { get => _strError; }
        #endregion

        #region Métodos

        private string sincomillas(string sTexto) { return globales.comillas(sTexto); }

        public List<cls_dbo_POC_WIP_Tracking> Recuperar(string sPO)
        {
            List<cls_dbo_POC_WIP_Tracking> rs = new List<cls_dbo_POC_WIP_Tracking>() { };
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("EXEC reportes.wip_cuts_tracking @pocs = '{0}';", sincomillas(sPO));
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
                rs = null;
                _strError = e.Message;
            }
            return rs;
        }

        List<cls_dbo_POC_WIP_Tracking> Recuperar(DataTable tbl)
        {
            List<cls_dbo_POC_WIP_Tracking> rs = new List<cls_dbo_POC_WIP_Tracking>() { };
            _strError = string.Empty;
            try
            {
                foreach (DataRow row in tbl.Rows)
                {
                    using (cls_dbo_POC_WIP_Tracking tmp = new cls_dbo_POC_WIP_Tracking())
                    {
                        tmp.DATE = DateTime.Parse(row["DATE"].ToString());
                        tmp.WEEK = row["WEEK"].ToString();
                        tmp.PORDER = row["P. ORDER"].ToString();
                        tmp.STYLE = row["STYLE"].ToString();
                        tmp.LINE = row["LINE"].ToString();
                        tmp.PLANT = row["PLANT"].ToString();
                        tmp.STYLE_BASE = row["STYLE BASE"].ToString();
                        tmp.QUANTITY = int.Parse(row["QUANTITY"].ToString());
                        tmp.WIP = row["WIP"].ToString();
                        tmp.PENDING = row["PENDING"].ToString();
                        tmp.COVER = row["COVER"].ToString();
                        tmp.COMMENT = row["COMMENT"].ToString();
                        rs.Add(tmp);
                    }
                }
            }
            catch (Exception e)
            {
                _strError = e.Message;
                rs = null;
            }
            return rs;
        }

        public void Nuevo()
        {
            _strError = string.Empty;
            this.DATE = DateTime.Now;
            this.WEEK = string.Empty;
            this.PORDER = string.Empty;
            this.STYLE = string.Empty;
            this.LINE = string.Empty;
            this.PLANT = string.Empty;
            this.STYLE_BASE = string.Empty;
            this.QUANTITY = 0;
            this.WIP = string.Empty;
            this.PENDING = string.Empty;
            this.COVER = string.Empty;
            this.COMMENT = string.Empty;
        }

        public cls_dbo_POC_WIP_Tracking()
        {
            Nuevo();
        }



        void IDisposable.Dispose()
        {
        }
        #endregion

    }
}