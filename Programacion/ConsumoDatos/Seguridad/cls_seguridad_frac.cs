using System;
using System.Collections.Generic;
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
    public class cls_seguridad_frac : IDisposable
    {
        public int numrole { get; set; }
        public int numformulario { get; set; }
        public int numacción { get; set; }
        public string Acción { get; set; }
        public bool aplica { get; set; }
        public bool Es_Opcional { get; set; }
        public bool Requiere_Contraseña { get; set; }
        public string Contraseña { get; set; }

        private string _strError { get; set; }
        public string strError { get => _strError; }

        private string sincomillas(string sTexto) { return globales.comillas(sTexto); }

        public List<cls_seguridad_frac> frac(string sForm, int nRole)
        {
            try
            {
                List<cls_seguridad_frac> objFRAC = new List<cls_seguridad_frac>() { };
                _strError = string.Empty;
                string strsql = string.Format("EXEC seguridad.frac_Recuperar_x_FormIdRole @formulario ='{0}',@#role={1};", sincomillas(sForm), nRole);
                DataTable dt = new DataTable();
                globales.consql.llenar_datatable(strsql, ref dt);
                if (globales.consql.TieneDatos(dt))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        using (cls_seguridad_frac tmp = new cls_seguridad_frac())
                        {
                            tmp.numrole = int.Parse(row["#role"].ToString());
                            tmp.numformulario = int.Parse(row["#formulario"].ToString());
                            tmp.numacción = int.Parse(row["#acción"].ToString());
                            tmp.Acción = row["Acción"].ToString();

                            tmp.aplica = bool.Parse(row["Aplica"].ToString());
                            tmp.Es_Opcional = bool.Parse(row["Es_Opcional"].ToString());
                            tmp.Requiere_Contraseña = bool.Parse(row["Es_Requiere_Contraseña"].ToString());

                            tmp.Contraseña = row["Contraseña"].ToString();

                            objFRAC.Add(tmp);
                        }
                    }
                }
                return objFRAC;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public List<cls_seguridad_frac> frac(int nForm, int nRole)
        {
            try
            {
                List<cls_seguridad_frac> objFRAC = new List<cls_seguridad_frac>() { };
                _strError = string.Empty;
                string strsql = string.Format("EXEC seguridad.frac_Recuperar_x_IdFormIdRole @#formulario ={0},@#role={1};", nForm, nRole);
                DataTable dt = new DataTable();
                globales.consql.llenar_datatable(strsql, ref dt);
                if (globales.consql.TieneDatos(dt))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        using (cls_seguridad_frac tmp = new cls_seguridad_frac())
                        {
                            tmp.numrole = int.Parse(row["#role"].ToString());
                            tmp.numformulario = int.Parse(row["#formulario"].ToString());
                            tmp.numacción = int.Parse(row["#acción"].ToString());
                            tmp.Acción = row["Acción"].ToString();

                            tmp.aplica = bool.Parse(row["Aplica"].ToString());
                            tmp.Es_Opcional = bool.Parse(row["Es_Opcional"].ToString());
                            tmp.Requiere_Contraseña = bool.Parse(row["Es_Requiere_Contraseña"].ToString());

                            tmp.Contraseña = row["Contraseña"].ToString();

                            objFRAC.Add(tmp);
                        }
                    }
                }
                return objFRAC;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Recuperar_FRAC(int nForm, int nRole)
        {
            bool rs = false;
            try
            {

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
            this.numacción = 0;
            this.numformulario = 0;
            this.numrole = 0;
            this.Acción = string.Empty;
            this.aplica = false;
            this.Es_Opcional = false;
            this.Requiere_Contraseña = false;
            this.Contraseña = string.Empty;
        }

        public cls_seguridad_frac()
        {
            Nuevo();
        }

        void IDisposable.Dispose()
        {
        }
    }
}