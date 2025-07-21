

namespace manufacturin_solution_apis
{
    /// <summary>
    /// Bryan Navarro
    /// bryan-navarro@hotmail.com
    /// 2022-08-19
    /// Flujo de ingresos y gastos en la cuenta del banco
    /// </summary>
    public class cls_Contabilidad_FlujoSaldosBancos
    {
        #region Atributos o Propiedades
        public short Año { get; set; }
        public byte Semana { get; set; }
        public decimal Saldo_Inicial { get; set; }
        public decimal Cobranza { get; set; }
        public decimal Otros { get; set; }
        public decimal Planillas { get; set; }
        public decimal CxP { get; set; }
        public decimal Prestamos { get; set; }

        /// <summary>
        /// Calculado: Saldo_Inicial + Cobranza + Otros - Planillas - CxP - Prestamos
        /// </summary>
        public decimal Saldo_Final { get => Saldo_Inicial + Cobranza + Otros - Planillas - CxP - Prestamos; }

        #endregion

        #region Procedimientos o Funciones

        #endregion
    }
}