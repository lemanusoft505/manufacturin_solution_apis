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
        public int idcliente { get; set; }
        public string cliente { get; set; }
        public bool Estado { get; set; }
        private string _sError { get; set; }
        public string strError { get => _sError; }

        public bool Eliminar(int nNum)
        {
            bool rs = false;
            try
            {
                _sError = "";
                string strsql = string.Format("EXEC dbo.Cliente_Eliminar @id_cliente={0};", nNum);
                int rn = 0;
                globales.consql.ejecutar_int(strsql, ref rn);
                if (rn > 0)
                {
                    rs = true;
                }
            }
            catch (Exception e)
            {
                rs = false;
                _sError = e.Message;
            }
            return rs;
        }


        public List<cls_dbo_Cliente> grd()
        {
            List<cls_dbo_Cliente> lista = new List<cls_dbo_Cliente>() { };
            try
            {
                string strsql = "EXEC dbo.cliente_cmb_todos;";
                DataTable dt = new DataTable();
                globales.consql.llenar_datatable(strsql, ref dt);
                if (globales.consql.TieneDatos(dt))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        using (cls_dbo_Cliente tmp = new cls_dbo_Cliente())
                        {

                            tmp.cliente = row["CUSTOMER"].ToString();
                            tmp.idcliente = int.Parse(row["ID"].ToString());
                            tmp.Estado = true;
                            lista.Add(tmp);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _sError = e.Message;
                lista = null;
            }
            return lista;
        }

        public bool Guardar(int nId_Cliente,
            string sCliente,
            bool bEstado,
            string sAlias)
        {
            bool rs = false;
            try
            {
                _sError = "";
                string strsql = string.Format("exec dbo.Cliente_Guardar " +
                    "@Id_Cliente = {0}," +
                    "@Cliente = '{1}'," +
                    "@Estado = '{2}', " +
                    "@Alias='{3}';",
                    nId_Cliente,
                    globales.comillas(sCliente),
                    bEstado.ToString(),
                    globales.comillas(sAlias));
                int rn = 0;
                globales.consql.ejecutar_int(strsql, ref rn);
                rs = Recuperar(rn);
            }
            catch (Exception e)
            {
                rs = false;
                _sError = e.Message;
            }
            return rs;
        }

        public bool Recuperar(int nNum)
        {
            bool rs = false;
            try
            {
                _sError = "";
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
                _sError = e.Message;
            }
            return rs;
        }

        public bool Recuperar(DataTable tbl)
        {
            bool rs = false;
            _sError = "";
            try
            {
                foreach (DataRow row in tbl.Rows)
                {
                    this.idcliente = int.Parse(row["Id_Cliente"].ToString());
                    this.cliente = row["Cliente"].ToString();
                    this.Estado = bool.Parse(row["Estado"].ToString());

                    rs = true;
                }
            }
            catch (Exception e)
            {
                _sError = e.Message;
                rs = false;
            }
            return rs;
        }

        public void Nuevo()
        {
            this.idcliente = 0;
            this.cliente = "";
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