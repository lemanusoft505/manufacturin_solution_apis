using System;
using System.Linq;

namespace manufacturin_solution_apis
{
    /// <summary>
    /// Leonardo Martínez Núñez
    /// lmartinez@rocedes.com.ni
    /// ROCEDES, S.A.
    /// 02 de junio 2022
    /// </summary>
    public class cls_dbo_invoice_dd : IDisposable
    {
        #region Atributos
        public string invoice { get; set; }
        public int id_cliente { get; set; }
        public int item { get; set; }
        public int Id_Order { get; set; }
        public string porder { get; set; }
        public int cantidad { get; set; }
        public decimal costo_unitario { get; set; }
        public decimal precio_unitario { get; set; }
        public decimal valor_descuento_unitario { get; set; }
        public decimal precio_neto { get; set; }
        public decimal total_venta_bruta { get; set; }
        public decimal total_descuento { get; set; }
        public decimal total_venta_neta { get; set; }
        public decimal margen_unitario { get; set; }
        public decimal total_margen_venta { get; set; }
        public string recurso { get; set; }

        private string _strError { get; set; }
        public string strError { get => _strError; }
        #endregion

        #region Métodos


        public void Nuevo()
        {
            this.invoice = string.Empty;
            this.id_cliente = 0;
            this.item = 0;
            this.Id_Order = 0;
            this.porder = string.Empty;
            this.cantidad = 0;
            this.costo_unitario = 0;
            this.precio_unitario = 0;
            this.valor_descuento_unitario = 0;
            this.precio_neto = 0;
            this.total_venta_bruta = 0;
            this.total_descuento = 0;
            this.total_venta_neta = 0;
            this.margen_unitario = 0;
            this.total_margen_venta = 0;
            this.recurso = string.Empty;
        }

        public cls_dbo_invoice_dd()
        {
            Nuevo();
        }

        void IDisposable.Dispose()
        {
        }
        #endregion

    }
}