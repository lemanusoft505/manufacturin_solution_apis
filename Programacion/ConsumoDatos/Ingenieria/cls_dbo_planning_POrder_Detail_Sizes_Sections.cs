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
    public class cls_dbo_planning_POrder_Detail_Sizes_Sections : IDisposable
    {
        #region Atributos

        public string POrder { get; set; }
        public string Size_Name { get; set; }
        public string Section_Name { get; set; }
        public int num { get; set; }
        public int Layers { get; set; }

        private string _strError { get; set; }
        public string strError { get => _strError; }
        #endregion

        #region Métodos

        string sincomillas(string stexto)
        {
            return globales.comillas(stexto);
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
                    this.Section_Name = row["Section_Name"].ToString();
                    this.num = int.Parse(row["num"].ToString());
                    this.Layers = int.Parse(row["Layers"].ToString());
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
            this.Section_Name = string.Empty;
            this.num = 0;
            this.Layers = 0;
        }

        public cls_dbo_planning_POrder_Detail_Sizes_Sections()
        {
            Nuevo();
        }

        void IDisposable.Dispose()
        {
        }

        #endregion


    }
}