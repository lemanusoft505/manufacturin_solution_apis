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
    /// 2022.06.17
    /// </summary>
    public class cls_inv_product : IDisposable
    {

        #region Atributos

        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Code { get; set; }
        public short Cantidad_Piezas { get; set; }

        private string _strError { get; set; }
        public string strError { get => _strError; }
        string TablaDatos { get { return "inv.Product"; } }
        #endregion

        #region Métodos
        private string sincomillas(string sTexto) { return globales.comillas(sTexto); }

        public bool Eliminar(int nNum)
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("exec {0}_Eliminar @#={1};", this.TablaDatos, nNum);
                rs = globales.consql.EjecutarTSQLBool(strsql);
            }
            catch (Exception e)
            {
                rs = false;
                _strError = e.Message;
            }
            return rs;

        }

        public bool Guardar(
            int nIdProducto,
            string sNombre,
            string sCode,
            short nCantidad_Piezas
            )
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("exec {0}_guardar ", this.TablaDatos);
                strsql += string.Format(
                    "@IdProducto = {0}," +
                    "@Nombre = '{1}'," +
                    "@Code = '{2}', " +
                    "@Cantidad_Piezas={3};",
                    nIdProducto,
                    sincomillas(sNombre),
                    sincomillas(sCode),
                    nCantidad_Piezas);
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
                string strsql = string.Format("exec {0}_recuperar @IdProducto={1};", this.TablaDatos, nNum);
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
                    this.IdProducto = int.Parse(row["IdProducto"].ToString());
                    this.Nombre = row["Nombre"].ToString();
                    this.Code = row["Code"].ToString();
                    this.Cantidad_Piezas = short.Parse(row["Cantidad_Piezas"].ToString());
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
            this.IdProducto = 0;
            this.Nombre = string.Empty;
            this.Code = string.Empty;
            this.Cantidad_Piezas = 0;
        }

        public List<cls_inv_product> cmb()
        {
            List<cls_inv_product> lista = new List<cls_inv_product>() { };
            try
            {
                string strsql = "EXEC inv.Product_cmb;";
                DataTable dt = new DataTable();
                globales.consql.llenar_datatable(strsql, ref dt);
                if (globales.consql.TieneDatos(dt))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        using (cls_inv_product tmp = new cls_inv_product())
                        {

                            tmp.Nombre = row["PRODUCT"].ToString();
                            tmp.IdProducto = int.Parse(row["#"].ToString());
                            tmp.Code = row["CODE"].ToString();
                            tmp.Cantidad_Piezas = short.Parse(row["PIECES"].ToString());
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

        #region constructor y destructor
        public cls_inv_product()
        {
            Nuevo();
        }

        void IDisposable.Dispose()
        {
        }
        #endregion

    }
}
