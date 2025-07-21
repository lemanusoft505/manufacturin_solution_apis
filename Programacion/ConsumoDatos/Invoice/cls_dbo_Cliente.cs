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
    public class cls_dbo_Cliente : IDisposable
    {
        public int Id_Cliente { get; set; }
        public string Cliente { get; set; }
        public bool Estado { get; set; }

        private string _strError { get; set; }
        public string strError { get => _strError; }

        public List<cls_dbo_Cliente> cmb(bool Activos = false)
        {
            List<cls_dbo_Cliente> lista = new List<cls_dbo_Cliente>() { };
            try
            {
                string strsql = "EXEC dbo.cliente_cmb_todos;";
                if (Activos)
                {
                    strsql = "EXEC dbo.cliente_cmb_activos;";
                }
                DataTable dt = new DataTable();
                globales.consql.llenar_datatable(strsql, ref dt);
                if (globales.consql.TieneDatos(dt))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        using (cls_dbo_Cliente tmp = new cls_dbo_Cliente())
                        {

                            tmp.Cliente = row["CUSTOMER"].ToString();
                            tmp.Id_Cliente = int.Parse(row["ID"].ToString());
                            tmp.Estado = true;
                            lista.Add(tmp);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _strError = e.Message;
                lista = null;
            }
            return lista;
        }

        public bool Guardar(int nId_Cliente,
            string sCliente,
            bool bEstado)
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("exec dbo.Cliente_Guardar " +
                    "@Id_Cliente = {0}," +
                    "@Cliente = '{1}'," +
                    "@Estado = '{2}';",
                    nId_Cliente,
                    globales.comillas(sCliente),
                    bEstado.ToString());
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
                string strsql = string.Format("EXEC dbo.Cliente_Recuperar @id_cliente={0};", nNum);
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
                    this.Id_Cliente = int.Parse(row["Id_Cliente"].ToString());
                    this.Cliente = row["Cliente"].ToString();
                    this.Estado = bool.Parse(row["Estado"].ToString());

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
            this.Id_Cliente = 0;
            this.Cliente = string.Empty;
            this.Estado = true;
        }

        public cls_dbo_Cliente()
        {
            Nuevo();
        }

        void IDisposable.Dispose()
        {

        }
    }
}