using System;
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
    public class cls_INTELSERVER_oper : IDisposable
    {
        #region Atributos

        public string operno { get; set; }
        public string descr { get; set; }
        public string descr2 { get; set; }
        public string ctdescr { get; set; }
        public string section { get; set; }
        public string mclass { get; set; }
        public string jobgroup { get; set; }
        public string coupctrl { get; set; }
        public string BASE {get; set;}
        public string optype { get; set; }
        public string active { get; set; }
        public string remote { get; set; }
        public string lot { get; set; }
        public string couptype { get; set; }
        public string dateup { get; set; }
        public string datecr { get; set; }
        public string costing { get; set; }
        public short enggroupidx { get; set; }
        public string extra { get; set; }

    private string _strError { get; set; }
        public string strError { get => _strError; }
        #endregion

        #region Métodos

        private string sincomillas(string sTexto) { return globales.comillas(sTexto); }

        

        public bool Recuperar(string sOperNo)
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("exec INTELSERVER.oper_recuperar @operno='{0}';", sincomillas(sOperNo));
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
                    this.operno = row["operno"].ToString();
                    this.descr = row["descr"].ToString();
                    this.descr2 = row["descr2"].ToString();
                    this.ctdescr = row["ctdescr"].ToString();
                    this.section = row["section"].ToString();
                    this.mclass = row["mclass"].ToString();
                    this.jobgroup = row["jobgroup"].ToString();
                    this.coupctrl = row["coupctrl"].ToString();
                    this.BASE = row["base"].ToString();
                    this.optype = row["optype"].ToString();
                    this.active = row["active"].ToString();
                    this.remote = row["remote"].ToString();
                    this.lot = row["lot"].ToString();
                    this.couptype = row["couptype"].ToString();
                    this.dateup = row["dateup"].ToString();
                    this.datecr = row["datecr"].ToString();
                    this.costing = row["costing"].ToString();
                    this.enggroupidx = short.Parse(row["enggroupidx"].ToString());
                    this.extra = row["extra"].ToString();
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
            this.operno = "";
            this.descr = "";
            this.descr2 = "";
            this.ctdescr = "";
            this.section = "";
            this.mclass = "";
            this.jobgroup = "";
            this.coupctrl = "";
            this.BASE = "";
            this.optype = "";
            this.active = "";
            this.remote = "";
            this.lot = "";
            this.couptype = "";
            this.dateup = "";
            this.datecr = "";
            this.costing = "";
            this.enggroupidx = 0;
            this.extra = "";
        }

        public cls_INTELSERVER_oper()
        {
            Nuevo();
        }



        void IDisposable.Dispose()
        {
        }
        #endregion

    }
}