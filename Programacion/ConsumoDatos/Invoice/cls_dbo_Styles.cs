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
    /// 2022.05.20
    /// </summary>
    public class cls_dbo_Styles : IDisposable
    {
        #region Atributos
        public int Id_Style { get; set; }
        public string Style { get; set; }
        public bool washed { get; set; }
        public decimal Price { get; set; }
        public int Id_Cliente { get; set; }
        public int idtipoTendido { get; set; }
        public short IdCategoria { get; set; }
        public decimal Price_Wash { get; set; }
        public decimal Price_Trims { get; set; }
        public decimal Price_Gross { get; set; }
        public int IdProducto { get; set; }
        public decimal SAM { get; set; }

        public cls_inv_product Producto
        {
            get
            {
                using (cls_inv_product objProducto = new cls_inv_product())
                {
                    objProducto.Recuperar(IdProducto);
                    return objProducto;
                }
            }
        }

        public cls_dbo_Cliente Cliente
        {
            get
            {
                using (cls_dbo_Cliente objCliente = new cls_dbo_Cliente())
                {
                    objCliente.Recuperar(Id_Cliente);
                    return objCliente;
                }
            }
        }

        private string _strError { get; set; }
        public string strError { get => _strError; }
        #endregion

        public bool Guardar(int nId_Style,
            string sStyle,
            bool bwashed,
            decimal nPrice,
            int nId_Cliente,
            int nidtipoTendido,
            int nStyle_Type,
            decimal nPrice_Wash,
            decimal nPrice_Trims,
            int nIdProducto,
            decimal nSAM)
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("exec dbo.Style_Guardar " +
                    "@Id_Style = {0}," +
                    "@Style = '{1}'," +
                    "@washed = '{2}'," +
                    "@Price = {3}," +
                    "@Id_Cliente = {4}," +
                    "@idtipoTendido = {5}," +
                    "@#Style_Type = {6}, " +
                    "@Price_Wash={7}, " +
                    "@Price_Trims={8}, " +
                    "@IdProducto={9}, " +
                    "@SAM={10};",
                    nId_Style,
                    globales.comillas(sStyle),
                    bwashed.ToString(),
                    nPrice,
                    nId_Cliente,
                    nidtipoTendido,
                    nStyle_Type,
                    nPrice_Wash,
                    nPrice_Trims,
                    nIdProducto,
                    nSAM);

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


        public List<cls_dbo_Styles> cmb(int IdCliente)
        {
            List<cls_dbo_Styles> lista = new List<cls_dbo_Styles>() { };
            try
            {
                string strsql = "EXEC dbo.Style_cmb @idcliente=null;";
                if (IdCliente > 0)
                {
                    strsql = string.Format("EXEC dbo.Style_cmb @idcliente={0};", IdCliente);
                }
                DataTable dt = new DataTable();
                globales.consql.llenar_datatable(strsql, ref dt);
                if (globales.consql.TieneDatos(dt))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        using (cls_dbo_Styles tmp = new cls_dbo_Styles())
                        {
                            tmp.Id_Style = int.Parse(row["Id_Style"].ToString());
                            if (row["Style"] != null)
                            {
                                tmp.Style = row["Style"].ToString();
                            }
                            if (row["washed"] != null)
                            {
                                tmp.washed = bool.Parse(row["washed"].ToString());
                            }
                            if (row["Price"] != null)
                            {
                                tmp.Price = decimal.Parse(row["Price"].ToString());
                            }
                            if (row["Id_Cliente"] != null)
                            {
                                tmp.Id_Cliente = int.Parse(row["Id_Cliente"].ToString());
                            }
                            if (row["idtipoTendido"] != null)
                            {
                                tmp.idtipoTendido = int.Parse(row["idtipoTendido"].ToString());
                            }
                            tmp.IdCategoria = short.Parse(row["#Style_Type"].ToString());
                            tmp.Price_Wash = decimal.Parse(row["Price_Wash"].ToString());
                            tmp.Price_Trims = decimal.Parse(row["Price_Trims"].ToString());
                            tmp.Price_Gross = decimal.Parse(row["Price_Gross"].ToString());
                            tmp.IdProducto = int.Parse(row["IdProducto"].ToString());
                            tmp.SAM = decimal.Parse(row["SAM"].ToString());

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

        public bool Recuperar(int nIdStyle)
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("exec dbo.Style_Recuperar @Id_Style={0};", nIdStyle);
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
                    Nuevo();
                    this.Id_Style = int.Parse(row["Id_Style"].ToString());
                    if (row["Style"] != null)
                    {
                        this.Style = row["Style"].ToString();
                    }
                    if (row["washed"] != null)
                    {
                        this.washed = bool.Parse(row["washed"].ToString());
                    }
                    if (row["Price"] != null)
                    {
                        this.Price = decimal.Parse(row["Price"].ToString());
                    }
                    if (row["Id_Cliente"] != null)
                    {
                        this.Id_Cliente = int.Parse(row["Id_Cliente"].ToString());
                    }
                    if (row["idtipoTendido"] != null)
                    {
                        this.idtipoTendido = int.Parse(row["idtipoTendido"].ToString());
                    }

                    this.IdCategoria = short.Parse(row["#Style_Type"].ToString());
                    this.Price_Wash = decimal.Parse(row["Price_Wash"].ToString());
                    this.Price_Trims = decimal.Parse(row["Price_Trims"].ToString());
                    this.Price_Gross = decimal.Parse(row["Price_Gross"].ToString());
                    this.IdProducto = int.Parse(row["IdProducto"].ToString());
                    this.SAM = decimal.Parse(row["SAM"].ToString());
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
            this.Id_Style = 0;
            this.Style = string.Empty;
            this.washed = false;
            this.Price = 0;
            this.Id_Cliente = 0;
            this.idtipoTendido = 0;
            this.IdCategoria = 1;
            this.Price_Wash = 0;
            this.Price_Trims = 0;
            this.Price_Gross = 0;
            this.IdProducto = 0;
            this.SAM = 0;
        }

        public cls_dbo_Styles()
        {
            Nuevo();
        }

        void IDisposable.Dispose()
        {
        }
    }
}