

namespace manufacturin_solution_apis
{
    /// <summary>
    /// Bryan Navarro
    /// bryan-navarro@hotmail.com
    /// 2022-08-19
    /// Resumen Semanal de la Cartera por Dia
    /// </summary>
    public class cls_Contabilidad_Cartera
    {
        #region propiedades
        public short Año { get; set; }
        public byte Semana { get; set; }
        public string Cliente { get; set; }
        public decimal Saldo_Inicial { get; set; }
        public decimal CxC_al_dia { get; set; }
        public decimal Descuento_Factoring_80 { get; set; }

        /// <summary>
        /// Calculado: CxC_al_dia + Descuento_Factoring_80
        /// </summary>
        public decimal Neto_CXC_al_dia { get => CxC_al_dia + Descuento_Factoring_80; }
        public decimal Total_FXC_al_dia { get; set; }
        public decimal Descuento_Reserva_Factoring_20 { get; set; }

        /// <summary>
        /// Calculado: Total_FXC_al_dia + Descuento_Reserva_Factoring_20
        /// </summary>
        public decimal Neto_FxC_al_dia { get => Total_FXC_al_dia + Descuento_Reserva_Factoring_20; }

        /// <summary>
        /// Calculado: Neto_CXC_al_dia + Neto_FxC_al_dia
        /// </summary>
        public decimal Total_Caja_Neto_al_dia { get => Neto_CXC_al_dia + Neto_FxC_al_dia; }
        public decimal Pagos_Recibidos_CxC_al_dia { get; set; }
        public decimal Pagos_FxC_al_dia { get; set; }

        /// <summary>
        /// Calculado: Pagos_Recibidos_CxC_al_dia + Pagos_FxC_al_dia
        /// </summary>
        public decimal Total_Pagos_al_dia { get => Pagos_Recibidos_CxC_al_dia + Pagos_FxC_al_dia; }

        /// <summary>
        /// Calculado: Total_Caja_Neto_al_dia - Total_Pagos_al_dia
        /// </summary>
        public decimal Total_Caja_Neto_pendiente_al_dia { get => Total_Caja_Neto_al_dia - Total_Pagos_al_dia; }
        public decimal CxC_CREDIT_Pendiente { get; set; }
        public decimal CxC_CREDIT_en_tramite { get; set; }

        /// <summary>
        /// Calculado: Total_Caja_Neto_pendiente_al_dia - CxC_CREDIT_Pendiente - CxC_CREDIT_en_tramite
        /// </summary>
        public decimal Saldo_Pendiente { get => Total_Caja_Neto_pendiente_al_dia - CxC_CREDIT_Pendiente - CxC_CREDIT_en_tramite; }
        #endregion

    }
}