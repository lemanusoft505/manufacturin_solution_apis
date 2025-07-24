using System;
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
    public class cls_dbo_linea : IDisposable
    {
        #region Atributos

        public int id_linea { get; set; }
        public int id_planta { get; set; }
        public string planta { get; set; }
        public string numero { get; set; }
        public int idcliente { get; set; }
        public string cliente { get; set; }
        public bool Estado { get; set; }
        public string Alias { get; set; }
        public string Alias2 { get; set; }
        public string refCostCenterPlanilla { get; set; }

        private string _strError { get; set; }
        public string strError { get => _strError; }
        #endregion

        #region Métodos

        public System.Collections.Generic.List<cls_dbo_linea> grd(int nIdPlanta)
        {
            System.Collections.Generic.List<cls_dbo_linea> rs = new System.Collections.Generic.List<cls_dbo_linea>();
            try
            {
                DataTable tbl = new DataTable();
                globales.consql.llenar_datatable($"exec dbo.Linea_grd_x_planta @IdPlanta={nIdPlanta};", ref tbl);
                foreach (DataRow r in tbl.Rows)
                {
                    rs.Add(new cls_dbo_linea()
                    {
                        id_linea = int.Parse(r["#"].ToString()),
                        id_planta = int.Parse(r["id_planta"].ToString()),
                        planta = r["PLANT"].ToString(),
                        numero = r["LINE"].ToString(),
                        idcliente = int.Parse(r["ID CLIENTE"].ToString()),
                        cliente = r["CLIENTE"].ToString(),
                        Estado = bool.Parse(r["ESTADO"].ToString()),
                        Alias = r["ALIAS"].ToString(),
                        Alias2 = r["ALIAS2"].ToString(),
                        refCostCenterPlanilla = r["REF_COSTRCTR"].ToString()
                    });
                }
            }
            catch (Exception)
            {
                rs = null;
            }
            return rs;
        }
        private string sincomillas(string sTexto) { return globales.comillas(sTexto); }

        public bool Guardar_grd()
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                if (this.numero.Trim().Length > 0) { 
                string strsql = $"exec dbo.Linea_Guardar_grd " +
                    $"@id_linea={this.id_linea}, " +
                    $"@planta='{this.planta}', " +
                    $"@numero='{sincomillas(this.numero)}', " +
                    $"@idcliente={this.idcliente}, " +
                    $"@Estado='{this.Estado}', " +
                    $"@Alias='{sincomillas(this.Alias)}', " +
                    $"@Alias2 ='{sincomillas(this.Alias2)}';";
                int rn = 0;
                globales.consql.ejecutar_int(strsql, ref rn);
                rs = Recuperar(rn);
                }
                
            }
            catch (Exception e)
            {
                rs = false;
                _strError = e.Message;
            }
            return rs;
        }

        public bool Guardar()
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                if (this.numero.Trim().Length > 0)
                {
                    string strsql = $"exec dbo.Linea_Guardar " +
                        $"@id_linea={this.id_linea}, " +
                        $"@id_planta='{this.id_planta}', " +
                        $"@numero='{sincomillas(this.numero)}', " +
                        $"@idcliente={this.idcliente}, " +
                        $"@Estado='{this.Estado}', " +
                        $"@Alias='{sincomillas(this.Alias)}', " +
                        $"@Alias2 ='{sincomillas(this.Alias2)}';";
                    int rn = 0;
                    globales.consql.ejecutar_int(strsql, ref rn);
                    rs = Recuperar(rn);
                }

            }
            catch (Exception e)
            {
                rs = false;
                _strError = e.Message;
            }
            return rs;
        }

        public bool Guardar(int nNum, 
            int nid_planta,
            string snumero,
            int nidcliente,
            bool bEstado,
            string sAlias,
            string sAlias2)
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = $"exec dbo.Linea_Guardar " +
                    $"@id_linea={nNum}, " +
                    $"@id_planta={nid_planta}, " +
                    $"@numero='{sincomillas(snumero)}', " +
                    $"@idcliente={nidcliente}, " +
                    $"@Estado='{bEstado}', " +
                    $"@Alias='{sincomillas(sAlias)}', " +
                    $"@Alias2 ='{sincomillas(sAlias2)}';";
                int rn = 0;
                globales.consql.ejecutar_int(strsql, ref rn);
                rs = Recuperar(rn);
            }
            catch (Exception e)
            {
                rs = false;
                _strError = e.Message;
            }
            return rs;
        }

        public bool Recuperar(int nNum)
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("exec dbo.Linea_Recuperar @id_linea={0};", nNum);
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
                    this.id_linea = int.Parse(row["id_linea"].ToString());
                    this.id_planta = int.Parse(row["id_planta"].ToString());
                    this.planta = row["planta"].ToString();
                    this.numero = row["numero"].ToString();
                    this.idcliente = int.Parse(row["idcliente"].ToString());
                    this.cliente = row["cliente"].ToString();
                    this.Estado = bool.Parse(row["Estado"].ToString());
                    this.Alias = row["Alias"].ToString();
                    this.Alias2 = row["Alias2"].ToString();
                    this.refCostCenterPlanilla = row["ref_costctr"].ToString();
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
            this.id_linea = 0;
            this.id_planta = 0;
            this.planta = "";
            this.numero = "";
            this.idcliente = 0;
            this.Estado = false;
            this.Alias = "";
            this.Alias2 = "";
            this.planta = "";
            this.cliente = "";
            this.refCostCenterPlanilla = "";
        }

        public cls_dbo_linea()
        {
            Nuevo();
        }



        void IDisposable.Dispose()
        {
        }
        #endregion

    }
}