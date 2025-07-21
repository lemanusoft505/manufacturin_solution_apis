using System;
using System.Data;
using System.IO;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace manufacturin_solution_apis
{
    /// <summary>
    /// Leonardo Martínez Núñez
    /// lmartinez@rocedes.com.ni
    /// ROCEDES, S.A.
    /// 02 de junio 2022
    /// </summary>
    public class cls_sistema_empresa : IDisposable
    {
        #region Atributos
        public byte num { get; set; }
        public string Empresa { get; set; }
        public string Nombre_Comercial { get; set; }
        public string Eslogan { get; set; }
        public string RUC_NIT { get; set; }
        public string Representante_Legal { get; set; }
        public string Dirección_Local { get; set; }
        public string Teléfonos { get; set; }
        public string Email { get; set; }
        public string Sitio_Web { get; set; }


        private string _strError;
        public string strError { get => _strError; }

        
        private byte[] _imagen;
        #endregion


        public bool Guardar(byte nNum,
            string sEmpresa,
            string sNombre_Comercial,
            string sEslogan,
            string sRUC_NIT,
            string sRepresentante_Legal,
            string sDirección_Local,
            string sTeléfonos,
            string sEmail,
            string sSitio_Web)
        {
            bool rs = false;
            try
            {
                string strsql = "EXEC Sistema.empresa_Guardar " +
                    "@#, @Empresa, @Nombre_Comercial, @Eslogan, " +
                    "@RUC_NIT, @Representante_Legal, @Dirección_Local, " +
                    "@Teléfonos, @Email, @Sitio_Web, @Logo";
                if (globales.consql.EsConectado)
                {
                    using (SqlCommand tmpCmd = new SqlCommand(strsql, globales.consql.Cn))
                    {
                        tmpCmd.Parameters.AddWithValue("@#", nNum);
                        tmpCmd.Parameters.AddWithValue("@Empresa", sEmpresa);
                        tmpCmd.Parameters.AddWithValue("@Nombre_Comercial", sNombre_Comercial);
                        tmpCmd.Parameters.AddWithValue("@Eslogan", sEslogan);
                        tmpCmd.Parameters.AddWithValue("@RUC_NIT", sRUC_NIT);
                        tmpCmd.Parameters.AddWithValue("@Representante_Legal", sRepresentante_Legal);
                        tmpCmd.Parameters.AddWithValue("@Dirección_Local", sDirección_Local);
                        tmpCmd.Parameters.AddWithValue("@Teléfonos", sTeléfonos);
                        tmpCmd.Parameters.AddWithValue("@Email", sEmail);
                        tmpCmd.Parameters.AddWithValue("@Sitio_Web", sSitio_Web);
                        tmpCmd.Parameters.Add("@Logo", SqlDbType.Image);                        
                        byte rn = (byte)tmpCmd.ExecuteScalar();
                        if (rn > 0)
                        {
                            rs = Recuperar(rn);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _strError = e.Message;
                rs = false;
            }
            return rs;
        }

        public bool Recuperar(byte nNum = 1)
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("EXEC Sistema.empresa_Recuperar @#={0};", nNum);
                DataTable tbl = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        this.num = nNum;
                        this.Empresa = row["Empresa"].ToString();
                        this.Nombre_Comercial = row["Nombre_Comercial"].ToString();
                        this.Eslogan = row["Eslogan"].ToString();
                        this.RUC_NIT = row["RUC_NIT"].ToString();
                        this.Representante_Legal = row["Representante_Legal"].ToString();
                        this.Dirección_Local = row["Dirección_Local"].ToString();
                        this.Teléfonos = row["Teléfonos"].ToString();
                        this.Email = row["Email"].ToString();
                        this.Sitio_Web = row["Sitio_Web"].ToString();

                        rs = true;
                    }
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
            this.num = 0;
            this.Empresa = string.Empty;
            this.Nombre_Comercial = string.Empty;
            this.Eslogan = string.Empty;
            this.RUC_NIT = string.Empty;
            this.Representante_Legal = string.Empty;
            this.Dirección_Local = string.Empty;
            this.Teléfonos = string.Empty;
            this.Email = string.Empty;
            this.Sitio_Web = string.Empty;
            _strError = string.Empty;
        }

        public cls_sistema_empresa()
        {
            Nuevo();
        }

        void IDisposable.Dispose()
        {

        }
    }
}