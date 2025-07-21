using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace manufacturin_solution_apis
{
    public class cls_dbo_AccesoLineasConteoLavanderia_SP_QRY : IDisposable
    {
        public string USUARIO { get; set; }
        public string PLANTA { get; set; }
        public string ID { get; set; }
        public string LINEA { get; set; }
        public bool APLICA { get; set; }

        public void Guardar2()
        {
            try
            {
                string strsql = $"EXEC dbo.AccesoLineasTrasladosLavanderia_SP_QRY_guardar " +
                    $"@usuario = '{this.USUARIO.Replace("'", "''")}', " +
                    $"@IdLinea = {this.ID}, " +
                    $"@Aplica = '{this.APLICA}';";
                globales.consql.EjecutarTSQL(strsql);
            }
            catch (Exception)
            { }
        }

        public void Guardar()
        {
            try
            {
                string strsql = $"EXEC dbo.AccesoLineasConteoLavanderia_SP_QRY_guardar " +
                    $"@usuario = '{this.USUARIO.Replace("'", "''")}', " +
                    $"@IdLinea = {this.ID}, " +
                    $"@Aplica = '{this.APLICA}';";
                globales.consql.EjecutarTSQL(strsql);
            }
            catch (Exception)
            { }
        }

        public void GuardarMedidasBB(string Role)
        {
            try
            {
                string strsql = $"EXEC dbo.AccesoLineasMedidas_SP_QRY_guardar " +
                    $"@usuario = '{this.USUARIO.Replace("'", "''")}', " +
                    $"@IdLinea = {this.ID}, " +
                    $"@Aplica = '{this.APLICA}'," +
                    $"@Role = '{Role.Replace("'", "''")}';";
                globales.consql.EjecutarTSQL(strsql);
            }
            catch (Exception)
            { }
        }

        public List<cls_dbo_AccesoLineasConteoLavanderia_SP_QRY> RecuperarTrasladosLavanderiaGrd(string sUsuario)
        {
            try
            {
                List<cls_dbo_AccesoLineasConteoLavanderia_SP_QRY> lst = new List<cls_dbo_AccesoLineasConteoLavanderia_SP_QRY>() { };
                DataTable tbl = new DataTable();
                string strsql = $"EXEC dbo.AccesoLineasTrasladosLavanderia_SP_QRY @usuario='{sUsuario.Replace("'", "''")}';";
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        using (cls_dbo_AccesoLineasConteoLavanderia_SP_QRY tmp = new cls_dbo_AccesoLineasConteoLavanderia_SP_QRY()
                        {
                            USUARIO = sUsuario
                            ,
                            ID = row["ID"].ToString()
                            ,
                            PLANTA = row["PLANTA"].ToString()
                            ,
                            LINEA = row["LINEA"].ToString()
                            ,
                            APLICA = bool.Parse(row["APLICA"].ToString())
                        })
                        {
                            lst.Add(tmp);
                        }
                    }
                }
                tbl = null;
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public List<cls_dbo_AccesoLineasConteoLavanderia_SP_QRY> RecuperarGrd(string sUsuario)
        {
            try
            {
                List<cls_dbo_AccesoLineasConteoLavanderia_SP_QRY> lst = new List<cls_dbo_AccesoLineasConteoLavanderia_SP_QRY>() { };
                DataTable tbl = new DataTable();
                string strsql = $"EXEC dbo.AccesoLineasConteoLavanderia_SP_QRY @usuario='{sUsuario.Replace("'", "''")}';";
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        using (cls_dbo_AccesoLineasConteoLavanderia_SP_QRY tmp = new cls_dbo_AccesoLineasConteoLavanderia_SP_QRY()
                        {
                            USUARIO = sUsuario
                            ,
                            ID = row["ID"].ToString()
                            ,
                            PLANTA = row["PLANTA"].ToString()
                            ,
                            LINEA = row["LINEA"].ToString()
                            ,
                            APLICA = bool.Parse(row["APLICA"].ToString())
                        })
                        {
                            lst.Add(tmp);
                        }
                    }
                }
                tbl = null;
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<cls_dbo_AccesoLineasConteoLavanderia_SP_QRY> RecuperarGrdMedidas(string sUsuario, string sRole)
        {
            try
            {
                List<cls_dbo_AccesoLineasConteoLavanderia_SP_QRY> lst = new List<cls_dbo_AccesoLineasConteoLavanderia_SP_QRY>() { };
                DataTable tbl = new DataTable();
                string strsql = $"EXEC dbo.AccesoLineasMedidas_SP_QRY @usuario='{sUsuario.Replace("'", "''")}', @role = '{sRole.Replace("'", "''")}';";
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        using (cls_dbo_AccesoLineasConteoLavanderia_SP_QRY tmp = new cls_dbo_AccesoLineasConteoLavanderia_SP_QRY()
                        {
                            USUARIO = sUsuario
                            ,
                            ID = row["ID"].ToString()
                            ,
                            PLANTA = row["PLANTA"].ToString()
                            ,
                            LINEA = row["LINEA"].ToString()
                            ,
                            APLICA = bool.Parse(row["APLICA"].ToString())
                        })
                        {
                            lst.Add(tmp);
                        }
                    }
                }
                tbl = null;
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public cls_dbo_AccesoLineasConteoLavanderia_SP_QRY()
        {
            this.USUARIO = string.Empty;
            this.PLANTA = string.Empty;
            this.ID = string.Empty;
            this.LINEA = string.Empty;
            this.APLICA = false;
        }

        public void Dispose()
        { }
    }
}