using System;
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
    public class cls_dbo_POrder_Status : IDisposable
    {
        #region Atributos
        public int Id_Order { get; set; }
        public string POrder { get; set; }
        public string POrderClient { get; set; }
        public string Last_Status { get; set; }
        public int Cant_Issue { get; set; }
        public int Cant_Cutting { get; set; }
        public int Cant_Sweing { get; set; }
        public int Cant_Washing { get; set; }
        public int Cant_Packing { get; set; }
        public int Cant_Shipping { get; set; }
        public int Cant_Irregular { get; set; }
        public string FH_First_Cutting { get; set; }
        public string FH_Last_Cutting { get; set; }
        public string FH_First_Sewin { get; set; }
        public string FH_Last_Sewin { get; set; }
        public string FH_First_Washing { get; set; }
        public string FH_Last_Washing { get; set; }
        public string FH_First_Packing { get; set; }
        public string FH_Last_Packing { get; set; }
        public string FH_First_Shipping { get; set; }
        public string FH_Last_Shipping { get; set; }
        public string FH_First_Irregular { get; set; }
        public string FH_Last_Irregular { get; set; }
        public bool IsDone { get; set; }
        public bool Is_Canceled { get; set; }
        public decimal Net_Price { get; set; }
        public decimal Gross_Price { get; set; }
        public decimal Bake { get; set; }
        public decimal Wash { get; set; }
        public decimal Trims { get; set; }
        public decimal Embroidery { get; set; }
        public int Cant_Stock { get; set; }

        private string _strError { get; set; }
        public string strError { get => _strError; }
        #endregion

        #region Métodos

        public bool Recuperar(string sPOrder)
        {
            bool rs = false;
            _strError = string.Empty;
            string strsql = string.Format("EXEC POrder_Status_Recuperar_x_POrderClient @POrder = '{0}'; ", globales.comillas(sPOrder));
            try
            {
                DataTable tbl = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    rs = Recuperar(tbl);
                }
                tbl.Dispose();
            }
            catch (Exception e)
            {
                _strError = e.Message;
                rs = false;
            }
            return rs;
        }


        public bool Recuperar(int nIdOrder)
        {
            bool rs = false;
            _strError = string.Empty;
            string strsql = string.Format("EXEC POrder_Status_Recuperar @Id_Order = {0}; ", nIdOrder);
            try
            {
                DataTable tbl = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    rs = Recuperar(tbl);
                }
                tbl.Dispose();
            }
            catch (Exception e)
            {
                _strError = e.Message;
                rs = false;
            }
            return rs;
        }


        public bool Recuperar(DataTable tbl)
        {
            bool rs = false;
            try
            {
                foreach (DataRow row in tbl.Rows)
                {
                    this.Id_Order = int.Parse(row["Id_Order"].ToString());
                    this.POrder = row["POrder"].ToString();
                    this.POrderClient = row["POrderClient"].ToString();
                    this.Last_Status = row["Last_Status"].ToString();
                    this.Cant_Issue = int.Parse(row["Cant_Issue"].ToString());
                    this.Cant_Cutting = int.Parse(row["Cant_Cutting"].ToString());
                    this.Cant_Sweing = int.Parse(row["Cant_Sweing"].ToString());
                    this.Cant_Washing = int.Parse(row["Cant_Washing"].ToString());
                    this.Cant_Packing = int.Parse(row["Cant_Packing"].ToString());
                    this.Cant_Shipping = int.Parse(row["Cant_Shipping"].ToString());
                    this.Cant_Irregular = int.Parse(row["Cant_Irregular"].ToString());
                    this.FH_First_Cutting = row["FH_First_Cutting"].ToString();
                    this.FH_Last_Cutting = row["FH_Last_Cutting"].ToString();
                    this.FH_First_Sewin = row["FH_First_Sewin"].ToString();
                    this.FH_Last_Sewin = row["FH_Last_Sewin"].ToString();
                    this.FH_First_Washing = row["FH_First_Washing"].ToString();
                    this.FH_Last_Washing = row["FH_Last_Washing"].ToString();
                    this.FH_First_Packing = row["FH_First_Packing"].ToString();
                    this.FH_Last_Packing = row["FH_Last_Packing"].ToString();
                    this.FH_First_Shipping = row["FH_First_Shipping"].ToString();
                    this.FH_Last_Shipping = row["FH_Last_Shipping"].ToString();
                    this.FH_First_Irregular = row["FH_First_Irregular"].ToString();
                    this.FH_Last_Irregular = row["FH_Last_Irregular"].ToString();
                    this.IsDone = bool.Parse(row["IsDone"].ToString());
                    this.Is_Canceled = bool.Parse(row["Is_Canceled"].ToString());
                    this.Net_Price = decimal.Parse(row["Net Price"].ToString());
                    this.Gross_Price = decimal.Parse(row["Gross Price"].ToString());
                    this.Bake = decimal.Parse(row["Bake"].ToString());
                    this.Wash = decimal.Parse(row["Wash"].ToString());
                    this.Trims = decimal.Parse(row["Trims"].ToString());
                    this.Embroidery = decimal.Parse(row["Embroidery"].ToString());
                    this.Cant_Stock = int.Parse(row["Cant_Stock"].ToString());
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

        public cls_dbo_POrder_Status()
        {
            Nuevo();
        }

        public void Nuevo()
        {
            this.Id_Order = 0;
            this.POrder = string.Empty;
            this.POrderClient = string.Empty;
            this.Last_Status = string.Empty;
            this.Cant_Issue = 0;
            this.Cant_Cutting = 0;
            this.Cant_Sweing = 0;
            this.Cant_Washing = 0;
            this.Cant_Packing = 0;
            this.Cant_Shipping = 0;
            this.Cant_Irregular = 0;
            this.FH_First_Cutting = string.Empty;
            this.FH_Last_Cutting = string.Empty;
            this.FH_First_Sewin = string.Empty;
            this.FH_Last_Sewin = string.Empty;
            this.FH_First_Washing = string.Empty;
            this.FH_Last_Washing = string.Empty;
            this.FH_First_Packing = string.Empty;
            this.FH_Last_Packing = string.Empty;
            this.FH_First_Shipping = string.Empty;
            this.FH_Last_Shipping = string.Empty;
            this.FH_First_Irregular = string.Empty;
            this.FH_Last_Irregular = string.Empty;
            this.IsDone = false;
            this.Is_Canceled = false;
            this.Net_Price = 0;
            this.Gross_Price = 0;
            this.Bake = 0;
            this.Wash = 0;
            this.Trims = 0;
            this.Embroidery = 0;
            this.Cant_Stock = 0;
        }

        void IDisposable.Dispose()
        {
        }
        #endregion

    }
}