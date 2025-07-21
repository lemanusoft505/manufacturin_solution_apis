using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace manufacturin_solution_apis
{

    /// <summary>
    /// Leonardo Martínez Núñez
    /// lmartinez@rocedes.com.ni
    /// lemanusoft@hotmail.com
    /// 2024.05.10
    /// </summary>
    public class cls_dbo_tbl_medidas : IDisposable
    {
        #region Atributos

        public int idMedida { get; set; }
        public string Medida { get; set; }
        public decimal valor { get; set; }
        public byte Orden { get; set; }
        public string POM_Status { get; set; }

        private string _strError { get; set; }
        public string strError { get => _strError; }
        #endregion

        #region Métodos

        private string sincomillas(string sTexto) { return globales.comillas(sTexto); }

        public List<cls_dbo_tbl_medidas> tbl_grd() {
            List<cls_dbo_tbl_medidas> tbl = new List<cls_dbo_tbl_medidas>() { };
            try
            {
                string strsql = "EXEC dbo.tblMedidas_grd;";
                DataTable t = new DataTable();
                globales.consql.llenar_datatable(strsql, ref t);
                if (globales.consql.TieneDatos(t))
                {
                    foreach (DataRow r in t.Rows)
                    {
                        using (cls_dbo_tbl_medidas x = new cls_dbo_tbl_medidas() {
                            idMedida = int.Parse(r["ID"].ToString())
                            ,
                            Medida = r["MEDIDA"].ToString()
                            ,
                            valor = decimal.Parse(r["VALOR"].ToString())
                            ,
                            Orden = byte.Parse(r["ORDEN"].ToString())
                            ,
                            POM_Status = r["ESTADO"].ToString()
                    })
                        {
                            tbl.Add(x);
                        }
                    }
                }
                else {
                    tbl = null;
                }
            }
            catch (Exception)
            {

                tbl = null;
            }
            return tbl;
        }

        public bool Recuperar(int nNum)
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("exec dbo.tblMedidas_recuperar @#={0};", nNum);
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
                    this.idMedida = int.Parse(row["idMedida"].ToString());
                    this.Medida= row["Medida"].ToString();
                    this.valor = decimal.Parse(row["valor"].ToString());
                    this.Orden = byte.Parse(row["Orden"].ToString());
                    this.POM_Status = row["POM_Status"].ToString();
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
            this.idMedida = 0;
            this.Medida="";
            this.valor = 0;
            this.Orden = 0;
            this.POM_Status = "";
        }

        public cls_dbo_tbl_medidas()
        {
            Nuevo();
        }



        void IDisposable.Dispose()
        {
        }
        #endregion

    }
}