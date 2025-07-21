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
    public class cls_dbo_POrderClient_Status : IDisposable
    {
        #region Atributos
        public string POrderClient { get; set; }
        public int Cant_Issue { get; set; }
        public int Cant_Cutting { get; set; }
        public int Cant_Sweing { get; set; }
        public int Cant_Washing { get; set; }
        public int Cant_Packing { get; set; }
        public int Cant_Shipping { get; set; }
        public int Cant_Irregular { get; set; }
        public bool IsDone { get; set; }
        public bool Is_Canceled { get; set; }
        public decimal Net_Price { get; set; }
        public decimal Gross_Price { get; set; }
        public decimal Bake { get; set; }
        public decimal Wash { get; set; }
        public decimal Trims { get; set; }
        public decimal Embroidery { get; set; }
        public int Cant_Stock { get; set; }
        public int IdCliente { get; set; }
        public int Id_Style { get; set; }

        public cls_dbo_Cliente objCliente { get; set; }
        public cls_dbo_Styles objStyle { get; set; }
        private string _strError { get; set; }
        public string strError { get => _strError; }
        #endregion

        #region Métodos


        public bool Recuperar(string sPOrder)
        {
            bool rs = false;
            _strError = string.Empty;
            string strsql = string.Format("EXEC dbo.POrderClient_Status_Recuperar @POrderClient = '{0}'; ", globales.comillas(sPOrder));
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
                    this.POrderClient = row["POrderClient"].ToString();
                    this.Cant_Issue = int.Parse(row["Cant_Issue"].ToString());
                    this.Cant_Cutting = int.Parse(row["Cant_Cutting"].ToString());
                    this.Cant_Sweing = int.Parse(row["Cant_Sweing"].ToString());
                    this.Cant_Washing = int.Parse(row["Cant_Washing"].ToString());
                    this.Cant_Packing = int.Parse(row["Cant_Packing"].ToString());
                    this.Cant_Shipping = int.Parse(row["Cant_Shipping"].ToString());
                    this.Cant_Irregular = int.Parse(row["Cant_Irregular"].ToString());
                    this.IsDone = bool.Parse(row["IsDone"].ToString());
                    this.Is_Canceled = bool.Parse(row["Is_Canceled"].ToString());
                    this.Net_Price = decimal.Parse(row["Net Price"].ToString());
                    this.Gross_Price = decimal.Parse(row["Gross Price"].ToString());
                    this.Bake = decimal.Parse(row["Bake"].ToString());
                    this.Wash = decimal.Parse(row["Wash"].ToString());
                    this.Trims = decimal.Parse(row["Trims"].ToString());
                    this.Embroidery = decimal.Parse(row["Embroidery"].ToString());
                    this.Cant_Stock = int.Parse(row["Cant_Stock"].ToString());
                    this.IdCliente = int.Parse(row["Id_Cliente"].ToString());
                    this.Id_Style = int.Parse(row["IdStyle"].ToString());
                    objCliente.Recuperar(this.IdCliente);
                    objStyle.Recuperar(this.Id_Style);
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
            this.POrderClient = string.Empty;
            this.Cant_Issue = 0;
            this.Cant_Cutting = 0;
            this.Cant_Sweing = 0;
            this.Cant_Washing = 0;
            this.Cant_Packing = 0;
            this.Cant_Shipping = 0;
            this.Cant_Irregular = 0;
            this.IsDone = false;
            this.Is_Canceled = false;
            this.Net_Price = 0;
            this.Gross_Price = 0;
            this.Bake = 0;
            this.Wash = 0;
            this.Trims = 0;
            this.Embroidery = 0;
            this.Cant_Stock = 0;
            this.Id_Style = 0;
            if (objCliente == null)
            {
                objCliente = new cls_dbo_Cliente();
            }
            objCliente.Nuevo();

            if (objStyle == null)
            {
                objStyle = new cls_dbo_Styles();
            }
            objStyle.Nuevo();
        }

        public cls_dbo_POrderClient_Status()
        {
            Nuevo();
        }

        void IDisposable.Dispose()
        {
        }
        #endregion

    }
}