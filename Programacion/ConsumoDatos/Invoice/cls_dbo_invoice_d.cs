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
    public class cls_dbo_invoice_d : IDisposable
    {

        #region Atributos
        public string invoice { get; set; }
        public int id_cliente { get; set; }
        public string nombre_cliente { get; set; }
        public DateTime fecha { get; set; }
        public string tipo { get; set; }
        public decimal costos_planta { get; set; }
        public decimal costos_lavado { get; set; }
        public decimal costos_varios { get; set; }
        public decimal costos_total { get; set; }
        public decimal venta_bruta { get; set; }
        public decimal utilidad_bruta { get; set; }

        private string _strError { get; set; }
        public string strError { get => _strError; }
        #endregion

        #region Métodos

        public List<cls_dbo_invoice_d> grd(short mes, short año)
        {
            List<cls_dbo_invoice_d> lista = new List<cls_dbo_invoice_d>() { };
            try
            {
                string strsql = string.Format("EXEC dbo.invoice_d_Recuperar_mes @mes={0}, @año={1};", mes, año);
                DataTable tbl = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        using (cls_dbo_invoice_d tmp = new cls_dbo_invoice_d())
                        {
                            tmp.invoice = row["invoice"].ToString();
                            tmp.id_cliente = int.Parse(row["id_cliente"].ToString());
                            tmp.nombre_cliente = row["nombre_cliente"].ToString();
                            tmp.fecha = DateTime.Parse(row["fecha"].ToString());
                            tmp.tipo = row["tipo"].ToString();
                            tmp.costos_planta = decimal.Parse(row["costos_planta"].ToString());
                            tmp.costos_lavado = decimal.Parse(row["costos_lavado"].ToString());
                            tmp.costos_varios = decimal.Parse(row["costos_varios"].ToString());
                            tmp.costos_total = decimal.Parse(row["costos_total"].ToString());
                            tmp.venta_bruta = decimal.Parse(row["venta_bruta"].ToString());
                            tmp.utilidad_bruta = decimal.Parse(row["utilidad_bruta"].ToString());
                            lista.Add(tmp);
                        }
                    }
                }
                tbl.Dispose();
            }
            catch (Exception e)
            {
                lista = null;
                _strError = e.Message;
            }
            return lista;
        }
        public bool Guardar(string sinvoice,
            int nid_cliente,
            string snombre_cliente,
            DateTime dfecha,
            string stipo)
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("EXEC invoice_d_Guardar " +
                    "@invoice='{0}', " +
                    "@id_cliente={1}, " +
                    "@nombre_cliente='{2}', " +
                    "@fecha={3}, " +
                    "@tipo='{4}';",
                    globales.comillas(sinvoice),
                    nid_cliente,
                    globales.comillas(snombre_cliente),
                    globales.sqldate(dfecha),
                    globales.comillas(stipo));
                int rn = 0;
                globales.consql.ejecutar_int(strsql, ref rn);
                if (rn > 0)
                {
                    rs = Recuperar(sinvoice, nid_cliente);
                }

            }
            catch (Exception e)
            {
                rs = false;
                _strError = e.Message;
            }
            return rs;
        }

        public bool Recuperar(string sInvoice, int nIdCliente)
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("EXEC invoice_d_Recuperar @invoice = '{0}', @id_cliente = {1};", globales.comillas(sInvoice), nIdCliente);
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
                    this.invoice = row["invoice"].ToString();
                    this.id_cliente = int.Parse(row["id_cliente"].ToString());
                    this.nombre_cliente = row["nombre_cliente"].ToString();
                    this.fecha = DateTime.Parse(row["fecha"].ToString());
                    this.tipo = row["tipo"].ToString();
                    this.costos_planta = decimal.Parse(row["costos_planta"].ToString());
                    this.costos_lavado = decimal.Parse(row["costos_lavado"].ToString());
                    this.costos_varios = decimal.Parse(row["costos_varios"].ToString());
                    this.costos_total = decimal.Parse(row["costos_total"].ToString());
                    this.venta_bruta = decimal.Parse(row["venta_bruta"].ToString());
                    this.utilidad_bruta = decimal.Parse(row["utilidad_bruta"].ToString());

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
            this.invoice = string.Empty;
            this.id_cliente = 0;
            this.nombre_cliente = string.Empty;
            this.fecha = DateTime.Now;
            this.tipo = string.Empty;
            this.costos_planta = 0;
            this.costos_lavado = 0;
            this.costos_varios = 0;
            this.costos_total = 0;
            this.venta_bruta = 0;
            this.utilidad_bruta = 0;
        }

        public cls_dbo_invoice_d()
        {
            Nuevo();
        }

        void IDisposable.Dispose()
        {
        }
        #endregion

    }
}