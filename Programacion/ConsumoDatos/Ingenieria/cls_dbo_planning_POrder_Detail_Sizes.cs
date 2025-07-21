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
    public class cls_dbo_planning_POrder_Detail_Sizes : IDisposable
    {
        #region Atributos

        public string POrder { get; set; }
        public string Size_Name { get; set; }
        public int Quantity_Origin { get; set; }
        public int Quantity_Mod { get; set; }
        public int Quantity_Mod_Adjusted { get; set; }
        public int Remaing { get; set; }
        public string usuario { get; set; }
        public DateTime fh { get; set; }

        private string _strError { get; set; }
        public string strError { get => _strError; }
        #endregion

        #region Métodos

        string sincomillas(string stexto)
        {
            return globales.comillas(stexto);
        }

        public bool Guardar(string sPOrder,
            string sSize_Name,
            int nQuantity_Origin,
            string sUsuario)
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("EXEC dbo.planning_POrder_Detail_Sizes_Guardar " +
                    "@POrder = '{0}', " +
                    "@Size_Name = '{1}', " +
                    "@Quantity_Origin = {2}, " +
                    "@usuario = '{3}';",
                    sincomillas(sPOrder),
                    sincomillas(sSize_Name),
                    nQuantity_Origin,
                    sincomillas(sUsuario));
                globales.consql.EjecutarTSQL(strsql);
                rs = Recuperar(sPOrder, sSize_Name);
            }
            catch (Exception e)
            {
                rs = false;
                _strError = e.Message;
            }
            return rs;
        }

        public bool Recuperar(string sPOrder, string sSize_Name)
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
                    this.Size_Name = row["Size_Name"].ToString();
                    this.Quantity_Origin = int.Parse(row["Quantity_Origin"].ToString());
                    this.Quantity_Mod = int.Parse(row["Quantity_Mod"].ToString());
                    this.Quantity_Mod_Adjusted = int.Parse(row["Quantity_Mod_Adjusted"].ToString());
                    this.Remaing = int.Parse(row["Remaing"].ToString());
                    this.usuario = row["usuario"].ToString();
                    this.fh = DateTime.Parse(row["fh"].ToString());
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
            this.Size_Name = string.Empty;
            this.Quantity_Origin = 0;
            this.Quantity_Mod = 0;
            this.Quantity_Mod_Adjusted = 0;
            this.Remaing = 0;
            this.usuario = string.Empty;
            this.fh = DateTime.Now;
        }

        public cls_dbo_planning_POrder_Detail_Sizes()
        {
            Nuevo();
        }

        void IDisposable.Dispose()
        {
        }

        #endregion

    }
}