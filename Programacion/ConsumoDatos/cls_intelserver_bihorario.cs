using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace manufacturin_solution_apis
{
    public class cls_intelserver_bihorario : IDisposable
    {
        public byte Id_Bihorario { get; set; }
        public string Bihorario { get; set; }
        public DateTime Desde { get; set; }
        public DateTime hasta { get; set; }
        public bool activo { get; set; }
        public short Minutes { get; set; }
        public decimal Minutos_Netos { get; set; }
        private string _strError = string.Empty;
        public string strError => _strError;

        public List<cls_intelserver_bihorario> cmb()
        {
            try
            {
                List<cls_intelserver_bihorario> lst = new List<cls_intelserver_bihorario>() { };
                const string strsql = "EXEC INTELSERVER.Bihorario_grd ;";
                DataTable tmpd = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tmpd);
                if (globales.consql.TieneDatos(tmpd))
                {
                    foreach (DataRow row in tmpd.Rows)
                    {
                        using (cls_intelserver_bihorario tmp = new cls_intelserver_bihorario())
                        {
                            tmp.Id_Bihorario = byte.Parse(row["Id_Bihorario"].ToString());
                            tmp.Bihorario = row["Bihorario"].ToString();
                            tmp.Desde = DateTime.Parse(row["Desde"].ToString());
                            tmp.hasta = DateTime.Parse(row["hasta"].ToString());
                            tmp.activo = bool.Parse(row["activo"].ToString());
                            tmp.Minutes = short.Parse(row["Minutes"].ToString());
                            tmp.Minutos_Netos = decimal.Parse(row["Minutos_Netos"].ToString());
                            lst.Add(tmp);
                        }
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Recuperar(int nNum)
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("EXEC INTELSERVER.Bihorario_Recuperar @Id_Bihorario={0};", nNum);
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
                    this.Id_Bihorario = byte.Parse(row["Id_Bihorario"].ToString());
                    this.Bihorario = row["Bihorario"].ToString();
                    this.Desde = DateTime.Parse(row["Desde"].ToString());
                    this.hasta = DateTime.Parse(row["hasta"].ToString());
                    this.activo = bool.Parse(row["activo"].ToString());
                    this.Minutes = short.Parse(row["Minutes"].ToString());
                    this.Minutos_Netos = decimal.Parse(row["Minutos_Netos"].ToString());
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

        public cls_intelserver_bihorario()
        {
        }

        public void Dispose()
        {
        }
    }
}