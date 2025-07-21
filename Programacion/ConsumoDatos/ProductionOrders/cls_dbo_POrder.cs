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
    public class cls_dbo_POrder : IDisposable
    {
        #region Atributos
        public int Id_Order { get; set; }
        public string POrder { get; set; }
        public int Id_Cliente { get; set; }
        public string Cliente { get; set; }
        public int Id_Style { get; set; }
        public string Style { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int Bundles { get; set; }
        public int Id_Planta { get; set; }
        public int Id_Linea { get; set; }
        public int Semana { get; set; }
        public string Comments { get; set; }
        public int Id_Linea2 { get; set; }
        public string Describir { get; set; }
        public int AfterIntex { get; set; }
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }
        public string POrderClient { get; set; }
        public bool washed { get; set; }

        private string _strError { get; set; }
        public string strError { get => _strError; }
        public int POrderClient_count { get => _POrderClient_count(); }

        private int _POrderClient_count()
        {
            int rn = 0;
            try
            {
                string strsql = string.Format("EXEC dbo.POrder_Client_Count @POrderClient= '{0}';", globales.comillas(this.POrderClient));
                globales.consql.ejecutar_int(strsql, ref rn);
            }
            catch (Exception)
            { }
            return rn;
        }
        #endregion

        #region Métodos

        public bool Recuperar(DataTable tbl)
        {
            bool rs = false;
            try
            {
                foreach (DataRow row in tbl.Rows)
                {
                    this.Id_Order = int.Parse(row["Id_Order"].ToString());
                    this.POrder = row["POrder"].ToString();
                    this.Id_Cliente = int.Parse(row["Id_Cliente"].ToString());
                    this.Cliente = row["Cliente"].ToString();
                    this.Id_Style = int.Parse(row["Id_Style"].ToString());
                    this.Style = row["Style"].ToString();
                    this.Description = row["Description"].ToString();
                    this.Quantity = int.Parse(row["Quantity"].ToString());
                    this.Bundles = int.Parse(row["Bundles"].ToString());
                    this.Id_Planta = int.Parse(row["Id_Planta"].ToString());
                    this.Id_Linea = int.Parse(row["Id_Linea"].ToString());
                    this.Semana = int.Parse(row["Semana"].ToString());
                    this.Comments = row["Comments"].ToString();
                    this.Id_Linea2 = int.Parse(row["Id_Linea2"].ToString());
                    this.Describir = row["Describir"].ToString();
                    this.AfterIntex = int.Parse(row["AfterIntex"].ToString());
                    this.CreateDate = row["CreateDate"].ToString();
                    this.UpdateDate = row["UpdateDate"].ToString();
                    this.POrderClient = row["POrderClient"].ToString();
                    this.washed = bool.Parse(row["washed"].ToString());
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

        public bool Recuperar(string sPOrder)
        {
            bool rs = false;
            _strError = string.Empty;
            string strsql = string.Format("EXEC POrder_Recuperar_x_POrder @POrder = '{0}'; ", globales.comillas(sPOrder));
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
            string strsql = string.Format("EXEC POrder_Recuperar @Id_Order = {0}; ", nIdOrder);
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

        public void Nuevo()
        {
            this.Id_Order = 0;
            this.POrder = string.Empty;
            this.Id_Cliente = 0;
            this.Cliente = string.Empty;
            this.Id_Style = 0;
            this.Style = string.Empty;
            this.Description = string.Empty;
            this.Quantity = 0;
            this.Bundles = 0;
            this.Id_Planta = 0;
            this.Id_Linea = 0;
            this.Semana = 0;
            this.Comments = string.Empty;
            this.Id_Linea2 = 0;
            this.Describir = string.Empty;
            this.AfterIntex = 0;
            this.CreateDate = string.Empty;
            this.UpdateDate = string.Empty;
            this.POrderClient = string.Empty;
            this.washed = false;
        }

        public cls_dbo_POrder()
        {
            Nuevo();
        }

        void IDisposable.Dispose()
        {
        }
        #endregion


    }
}