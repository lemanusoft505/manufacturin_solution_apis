using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace manufacturin_solution_apis
{
    /// <summary>
    /// Leonardo Martínez Núñez
    /// lmartinez@rocedes.com.ni
    /// lemanusoft@hotmail.com
    /// 2022.09.01
    /// </summary>
    public class cls_dbo_planta:IDisposable
    {
        #region Atributos
        public int id_planta { get; set; }
        public string descripcion { get; set; }
        public bool estado { get; set; }
        public string Alias { get; set; }
        public string refCostCenterPlanilla { get; set; }

        private string _strError { get; set; }
        public string strError { get => _strError; }


        public List<cls_dbo_planta> grd() {
            List<cls_dbo_planta> rs = new List<cls_dbo_planta>();
            try
            {
                DataTable tbl = new DataTable();
                globales.consql.llenar_datatable("exec dbo.Planta_grd;", ref tbl);
                foreach (DataRow r in tbl.Rows) {
                    rs.Add(new cls_dbo_planta() {
                        id_planta = int.Parse(r["id_planta"].ToString())
                        ,
                        descripcion = r["descripcion"].ToString()
                        ,
                        estado = bool.Parse(r["estado"].ToString())
                        ,
                        Alias = r["Alias"].ToString()
                        , 
                        refCostCenterPlanilla = r["refCostCenterPlanilla"].ToString()
                    });
                }
            }
                catch (Exception)
            {
                rs = null;
            }
            return rs;
        }

        public bool Recuperar(int nNum)
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("exec dbo.Planta_Recuperar @id_planta={0};", nNum);
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
                    this.id_planta = int.Parse(row["id_planta"].ToString());
                    this.descripcion = row["descripcion"].ToString();
                    this.estado = bool.Parse(row["estado"].ToString());
                    this.Alias = row["Alias"].ToString();
                    this.refCostCenterPlanilla = row["refCostCenterPlanilla"].ToString();
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

        void ir_nuevo()
        {
            try
            {
                id_planta = 0; descripcion = ""; estado = false; Alias = ""; this.refCostCenterPlanilla = "";
            }
            catch (Exception)
            {}
        }

        public cls_dbo_planta() {
            ir_nuevo();
        }

        public void Dispose()
        {
            
        }
        #endregion
    }
}