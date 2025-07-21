using System;
using System.Data;
using System.Linq;

namespace manufacturin_solution_apis
{
    /// <summary>
    /// Leonardo Martínez Núñez
    /// lmartinez@rocedes.com.ni
    /// lemanusoft@hotmail.com
    /// 2022.05.25
    /// </summary>
    public class cls_dbo_planning_POrder : IDisposable
    {
        #region Atributos
        public string POrder { get; set; }
        public bool IsBlocked { get; set; }
        public int IdStyle { get; set; }
        public string Style { get; set; }
        public int IdCliente { get; set; }
        public string Cliente { get; set; }
        public string Color_Name { get; set; }
        public string Color_Code { get; set; }
        public int Quantity_Origin { get; set; }
        public int Quantity_Mod { get; set; }
        public decimal Average_Yield_yrds { get; set; }
        public decimal Average_Yield_Increase { get; set; }
        public decimal Average_Yield_Total_yrds { get; set; }
        public decimal Min_Cut_Plat_yrds { get; set; }
        public decimal Max_Cut_Plat_yrds { get; set; }
        public int Min_Cut_Layers { get; set; }
        public int Max_Cut_Layers { get; set; }
        public string usuario { get; set; }
        public DateTime fh { get; set; }
        public decimal Quantitu_Increase { get; set; }

        private string _strError { get; set; }
        public string strError { get => _strError; }

        //public DataTable tblPlan = new DataTable();
        #endregion

        #region Métodos

        string sincomillas(string stexto)
        {
            return globales.comillas(stexto);
        }

        public DataTable Recuperar_Pllaning(string POrder)
        {
            DataTable tbl = new DataTable();
            try
            {
                string strsql = string.Format("EXEC dbo.planning_POrder_Grid_Secciones @porder = '{0}';", sincomillas(POrder));
                globales.consql.llenar_datatable(strsql, ref tbl);
            }
            catch (Exception e)
            {
                _strError = e.Message;
                tbl = null;
            }
            return tbl;
        }

        public bool Guardar(string sPOrder,
            bool bIsBlocked,
            int nIdStyle,
            int nIdCliente,
            string sColor_Name,
            string sColor_Code,
            decimal nQuantitu_Increase,
            decimal nAverage_Yield_yrds,
            decimal nAverage_Yield_Increase,
            decimal nAverage_Yield_Total_yrds,
            decimal nMin_Cut_Plat_yrds,
            decimal nMax_Cut_Plat_yrds,
            int nMin_Cut_Layers,
            int nMax_Cut_Layers,
            string sUsuario)
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("exec dbo.planning_POrder_Guardar " +
                    "@POrder = '{0}'," +
                    "@IsBlocked = '{1}'," +
                    "@IdStyle = {2}," +
                    "@IdCliente = {3}," +
                    "@Color_Name = '{4}'," +
                    "@Color_Code = '{5}'," +
                    "@Quantitu_Increase = {6}," +
                    "@Average_Yield_yrds = {7}," +
                    "@Average_Yield_Increase = {8}," +
                    "@Average_Yield_Total_yrds = {9}," +
                    "@Min_Cut_Plat_yrds = {10}," +
                    "@Max_Cut_Plat_yrds = {11}," +
                    "@Min_Cut_Layers = {12}," +
                    "@Max_Cut_Layers = {13}," +
                    "@usuario = '{14}';",
                    sincomillas(sPOrder),
                    bIsBlocked.ToString(),
                    nIdStyle,
                    nIdCliente,
                    sincomillas(sColor_Name),
                    sincomillas(sColor_Code),
                    nQuantitu_Increase,
                    nAverage_Yield_yrds,
                    nAverage_Yield_Increase,
                    nAverage_Yield_Total_yrds,
                    nMin_Cut_Plat_yrds,
                    nMax_Cut_Plat_yrds,
                    nMin_Cut_Layers,
                    nMax_Cut_Layers,
                    sincomillas(sUsuario));
                globales.consql.EjecutarTSQL(strsql);
                rs = Recuperar(sPOrder);
            }
            catch (Exception e)
            {
                rs = false;
                _strError = e.Message;
            }
            return rs;
        }

        public bool Recuperar(string sPOrder, bool bRecoverPlan = false)
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("exec dbo.planning_POrder_Recuperar @POrder='{0}';", globales.comillas(sPOrder));
                DataTable tbl = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    rs = this.Recuperar(tbl);
                    if (rs && bRecoverPlan)
                    {
                        //this.tblPlan = Recuperar_Pllaning(sPOrder);
                    }
                    else
                    {
                        //this.tblPlan = null;
                    }
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
                    this.POrder = row["POrder"].ToString();
                    this.IsBlocked = bool.Parse(row["IsBlocked"].ToString());
                    this.IdStyle = int.Parse(row["IdStyle"].ToString());
                    this.Style = row["Style"].ToString();
                    this.IdCliente = int.Parse(row["IdCliente"].ToString());
                    this.Cliente = row["Cliente"].ToString();
                    this.Color_Name = row["Color_Name"].ToString();
                    this.Color_Code = row["Color_Code"].ToString();
                    this.Quantity_Origin = int.Parse(row["Quantity_Origin"].ToString());
                    this.Quantity_Mod = int.Parse(row["Quantity_Mod"].ToString());
                    this.Average_Yield_yrds = decimal.Parse(row["Average_Yield_yrds"].ToString());
                    this.Average_Yield_Increase = decimal.Parse(row["Average_Yield_Increase"].ToString());
                    this.Average_Yield_Total_yrds = decimal.Parse(row["Average_Yield_Total_yrds"].ToString());
                    this.Min_Cut_Plat_yrds = decimal.Parse(row["Min_Cut_Plat_yrds"].ToString());
                    this.Max_Cut_Plat_yrds = decimal.Parse(row["Max_Cut_Plat_yrds"].ToString());
                    this.Min_Cut_Layers = int.Parse(row["Min_Cut_Layers"].ToString());
                    this.Max_Cut_Layers = int.Parse(row["Max_Cut_Layers"].ToString());
                    this.usuario = row["usuario"].ToString();
                    this.fh = DateTime.Parse(row["fh"].ToString());
                    this.Quantitu_Increase = decimal.Parse(row["Quantitu_Increase"].ToString());
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
            this.POrder = string.Empty;
            this.IsBlocked = false;
            this.IdStyle = 0;
            this.Style = string.Empty;
            this.IdCliente = 0;
            this.Cliente = string.Empty;
            this.Color_Name = string.Empty;
            this.Color_Code = string.Empty;
            this.Quantity_Origin = 0;
            this.Quantity_Mod = 0;
            this.Average_Yield_yrds = 0;
            this.Average_Yield_Increase = 0;
            this.Average_Yield_Total_yrds = 0;
            this.Min_Cut_Plat_yrds = 0;
            this.Max_Cut_Plat_yrds = 0;
            this.Min_Cut_Layers = 0;
            this.Max_Cut_Layers = 0;
            this.usuario = string.Empty;
            this.fh = DateTime.Now;
            this.Quantitu_Increase = 0;


        }

        public cls_dbo_planning_POrder()
        {
            Nuevo();
        }

        void IDisposable.Dispose()
        {
        }

        #endregion


    }
}