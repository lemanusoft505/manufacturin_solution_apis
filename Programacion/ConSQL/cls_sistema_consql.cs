using System;
using System.Data;
using Microsoft.Data.SqlClient;// System.Data.SqlClient;

/// <summary>
/// Leonardo Martínez Núñez
/// lmartinez@rocedes.com.ni
/// ROCEDES, S.A.
/// 25 de Abril 2022
/// </summary>
namespace manufacturin_solution_apis
{
    public class cls_sistema_consql : IDisposable

    {
        #region atributos de la conexión al MS-SQL

        /// <summary>
        /// conexión a la base de datos de mssql
        /// </summary>
        private SqlConnection _cn;

        private String _strConSQL = string.Empty;

        private String _Error_cnx = string.Empty;
        public String Error_cnx { get { return _Error_cnx; } }

        private DateTime _lastCnTime = System.DateTime.Now;
        public DateTime Last_CN_time { get => _lastCnTime; }
        #endregion

        #region Variables de Conexión proporcionadas por el Usuario

        #endregion

        #region Variables obtenidas después de conectarse



        #endregion

        #region atributos públicos
        /// <summary>
        /// conexión actual al mssql  de tipo ADO.NET
        /// </summary>
        public SqlConnection Cn { get => _cn; }

        /// <summary>
        /// Login de la conexión al mssql, primeras 3 capas de seguridad: Login, User, Role de Servidor y Base de Datos
        /// </summary>
        public string UsuarioSQL { get; } = globales.mssql_login;

        /// <summary>
        /// palabra clave para autenticar login del usuario mssql
        /// </summary>
        public string ContraseñaSQL { get; } = globales.mssql_contraseña;

        /// <summary>
        /// nombre o dirección ip del servidor mssql, indicar puerto si es diferente al 1433
        /// ejemplo   localhost:5555
        /// </summary>
        public string ServidorSQL { get; } = globales.mssql_servidor;

        /// <summary>
        /// nombre de la base de datos a la cual estableceremos conexión mssql
        /// </summary>
        public string BaseDatosSQL { get; } = globales.mssql_basedatos;

        /// <summary>
        /// código único obtenido al abrir una conexión a la base de datos
        /// </summary>
        public int IdLogin { get; private set; } = 0;

        /// <summary>
        /// Usuario con el que se identifica el usuario para ingresar al ERP, 4ta capa de seguridad
        /// </summary>
        public string Usuario_ERP { get; } = string.Empty;

        /// <summary>
        /// Nivel de acceso o Role dentro del ERP
        /// </summary>
        public short codRole { get; } = 0;


        /// <summary>
        /// objeto Sesión de la conexión, obtiene un GUID
        /// </summary>

        //todo: añadir la clase cls_seguridad_sesiones
        //public cls_seguridad_sesiones ObjSesión { get; } = new cls_seguridad_sesiones();


        #endregion

        #region funciones

        public void conectar()
        {
            try
            {
                this._strConSQL = String.Format("SERVER={0};DATABASE={1};USER ID={2};PASSWORD={3};APP={4};",
                this.ServidorSQL.Replace("'", string.Empty),
                this.BaseDatosSQL.Replace("'", string.Empty),
                this.UsuarioSQL.Replace("'", string.Empty),
                this.ContraseñaSQL.Replace("'", string.Empty),
                globales.mssql_App);

                if (_cn == null)
                {
                    _cn = new SqlConnection(_strConSQL);
                    _cn.Open();
                    using (SqlCommand tmpCmd = new SqlCommand("SET DATEFIRST 1;", _cn))
                    {
                        tmpCmd.ExecuteNonQuery();
                    }
                    //Obtener el IdLogin
                    if (this.IdLogin == 0)
                    {
                        //todo: objeto Login
                        //this.ObjLogin.iniciar(DateTime.Now.ToLongDateString());
                        //this.IdLogin = this.ObjLogin.IdLogin;
                    }
                }
                else
                {
                    if (_cn.State == ConnectionState.Closed)
                    {
                        _cn.ConnectionString = this.StrConSQL;
                        _cn.Open();
                    }
                }


            }
            catch (Exception e)
            {
                _Error_cnx = e.Message;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Vuelve a abrir la bd
        /// </summary>
        public void Reconectar()
        {
            try
            {
                if (!EsConectado)
                {
                    conectar();
                }
            }
            catch (Exception e)
            {
                this._Error_cnx = e.Message;
            }
            finally
            {
            }
        }

        public Boolean EsConectado
        {
            get
            {
                try
                {
                    Boolean rs = false;
                    if (_cn != null)
                    {
                        rs = (_cn.State.Equals(ConnectionState.Open));
                        if (rs)
                        {
                            string strsql = "select  1 as prueba, getdate() as fh;SET DATEFIRST 1;";
                            int rsn = 0;
                            using (SqlCommand tmpCmd = new SqlCommand(strsql, _cn) { CommandTimeout = 600 })
                            {
                                DataTable tbl0 = new DataTable();
                                tmpCmd.CommandTimeout = 60;
                                using (SqlDataAdapter tmpDa = new SqlDataAdapter(tmpCmd))
                                {
                                    tmpDa.Fill(tbl0);
                                    if (TieneDatos(tbl0))
                                    {
                                        foreach (DataRow row in tbl0.Rows)
                                        {
                                            rsn = int.Parse(row[0].ToString());
                                            _lastCnTime = DateTime.Parse(row[1].ToString());
                                        }
                                    }
                                }
                                tbl0.Dispose();
                            }
                            rs = rsn == 1;
                        }
                    }
                    return rs;
                }
                catch (Exception e)
                {
                    _cn = null;
                    _cn = new SqlConnection();
                    this._Error_cnx = e.Message;
                    return false;
                }
                finally
                {

                }

            }
        }

        public string StrConSQL
        {
            get => _strConSQL;
        }

        public static string CreateTABLE(DataTable table, string tableName)
        {
            string sqlsc;
            sqlsc = "CREATE TABLE " + tableName.Trim() + "(";
            for (int i = 0; i < table.Columns.Count; i++)
            {
                sqlsc += "\n [" + table.Columns[i].ColumnName.Trim() + "] ";
                string columnType = table.Columns[i].DataType.ToString();
                switch (columnType)
                {
                    case "System.Boolean":
                        sqlsc += " bit ";
                        break;
                    case "System.Int32":
                        sqlsc += " int ";
                        break;
                    case "System.Int64":
                        sqlsc += " bigint ";
                        break;
                    case "System.Int16":
                        sqlsc += " smallint";
                        break;
                    case "System.Byte":
                        sqlsc += " tinyint";
                        break;
                    case "System.Decimal":
                    case "System.Double":
                        sqlsc += " decimal(35, 6) ";
                        break;
                    case "System.DateTime":
                        sqlsc += " datetime ";
                        break;
                    case "System.String":
                    default:
                        sqlsc += string.Format(" nvarchar({0}) ", table.Columns[i].MaxLength == -1 ? "max" : table.Columns[i].MaxLength.ToString());
                        break;
                }
                if (table.Columns[i].AutoIncrement)
                    sqlsc += " IDENTITY(" + table.Columns[i].AutoIncrementSeed.ToString() + "," + table.Columns[i].AutoIncrementStep.ToString() + ") ";
                if (!table.Columns[i].AllowDBNull)
                    sqlsc += " NOT NULL ";
                sqlsc += ",";
            }
            sqlsc += " Es_Aplicado bit not null default 0 ";
            return sqlsc + "\n)";
        }

        public bool GuardarDataTable_en_BD(DataTable tbl, string strEsquema, string strTabla)
        {
            bool rs = false;
            try
            {
                Reconectar();

                string strsql = string.Format("IF OBJECT_ID('[{0}].[{1}]') IS NOT NULL" +
                    "    SELECT 1 AS rs " +
                    " ELSE     " +
                    "SELECT 0 AS rs", strEsquema, strTabla);
                int nExiste = 0;
                this.ejecutar_int(strsql, ref nExiste);
                if (nExiste == 0)
                {
                    this.EjecutarTSQL(CreateTABLE(tbl, string.Format("[{0}].[{1}]", strEsquema, strTabla)));
                }
                using (SqlConnection tmpCn = new SqlConnection(this.StrConSQL))
                {
                    tmpCn.Open();
                    using (SqlBulkCopy tmpBC = new SqlBulkCopy(tmpCn))
                    {
                        tmpBC.DestinationTableName = string.Format("[{0}].[{1}]", strEsquema, strTabla);
                        tmpBC.WriteToServer(tbl);
                    }
                }
            }
            catch (Exception e)
            {
                this._Error_cnx = e.Message;
                rs = false;
            }
            return rs;
        }

       
        public bool GuardarDataTable_en_BD_Recursos(DataTable tbl, string strTabla)
        {
            bool rs = false;
            try
            {
                Reconectar();

                string strsql = string.Format("IF OBJECT_ID('recursos.[{0}]') IS NOT NULL" +
                    "    SELECT 1 AS rs " +
                    " ELSE     " +
                    "SELECT 0 AS rs", strTabla);
                int nExiste = 0;
                this.ejecutar_int(strsql, ref nExiste);
                if (nExiste > 0)
                {
                    strsql = string.Format("DROP TABLE recursos.[{0}]", strTabla);
                    this.EjecutarTSQL(strsql);
                }
                this.EjecutarTSQL(CreateTABLE(tbl, string.Format("recursos.[{0}]", strTabla)));

                using (SqlConnection tmpCn = new SqlConnection(this.StrConSQL))
                {
                    tmpCn.Open();
                    using (SqlBulkCopy tmpBC = new SqlBulkCopy(tmpCn))
                    {
                        tmpBC.DestinationTableName = string.Format("recursos.[{0}]", strTabla);
                        tmpBC.WriteToServer(tbl);
                    }
                }
            }
            catch (Exception e)
            {
                this._Error_cnx = e.Message;
                rs = false;
            }
            return rs;
        }

        public void EjecutarTSQL(String strsql)
        {
            try
            {
                Reconectar();

                if (EsConectado)
                {
                    using (SqlConnection tmpCn = new SqlConnection(this.StrConSQL))
                    {
                        tmpCn.Open();
                        using (SqlCommand cmd = new SqlCommand(strsql, tmpCn) { CommandTimeout = 600 })
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }

                }
            }
            catch (Exception e)
            {
                this._Error_cnx = e.Message;
            }
            finally
            {

            }
        }

        public void ejecutar_short(String strSql, ref short rn)
        {
            try
            {
                this._Error_cnx = string.Empty;
                Reconectar();

                using (SqlConnection tmpCn = new SqlConnection(this.StrConSQL))
                {
                    tmpCn.Open();
                    using (SqlCommand tmpcmd = new SqlCommand(strSql, tmpCn) { CommandTimeout = 600 })
                    {
                        tmpcmd.CommandTimeout = 600;
                        rn = short.Parse(tmpcmd.ExecuteScalar().ToString());
                    }
                }

            }
            catch (Exception e)
            {
                this._Error_cnx = e.Message;
                rn = 0;
            }
            finally
            {
            }
        }

        public Boolean EjecutarTSQLBool(String strsql)
        {
            Boolean rs = false;
            try
            {
                Reconectar();

                if (EsConectado)
                {
                    using (SqlConnection tmpCn = new SqlConnection(this.StrConSQL))
                    {
                        tmpCn.Open();
                        using (SqlCommand cmd = new SqlCommand(strsql, tmpCn) { CommandTimeout = 600 })
                        {
                            cmd.ExecuteNonQuery();
                            rs = true;
                        }
                    }
                }

                return rs;
            }
            catch (Exception e)
            {
                this._Error_cnx = e.Message;
                return false;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Verifica que el DataTable tenga registros.
        /// </summary>
        /// <param name="tbl"></param>
        /// <returns></returns>
        public bool TieneDatos(DataTable tbl)
        {
            bool rs = false;
            if (tbl != null)
            {
                if (tbl.Rows.Count > 0)
                {
                    rs = true;
                }
            }
            return rs;
        }

        public bool TieneDatos(DataSet ds)
        {
            bool rs = false;
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        rs = true;
                    }
                }

            }
            return rs;
        }

        /// <summary>
        /// Ejecuta una t-sql que retorna una tabla
        /// </summary>
        /// <param name="strSQL">instruccióni tsql: SELECT o EXEC PROCEDURE</param>
        /// <returns>Tabla dataset</returns>
        public DataTable Ejecutar_DataTable(String strSQL)
        {
            try
            {
                DataTable rs = new DataTable();
                Reconectar();

                using (SqlConnection tmpCn = new SqlConnection(this.StrConSQL))
                {
                    tmpCn.Open();
                    using (SqlCommand tmpCmd = new SqlCommand(strSQL, tmpCn) { CommandTimeout = 600 })
                    {
                        tmpCmd.CommandTimeout = 60;
                        using (SqlDataAdapter tmpDa = new SqlDataAdapter(tmpCmd))
                        {
                            tmpDa.Fill(rs);
                        }
                    }
                }

                return rs;
            }
            catch (Exception e)
            {
                this._Error_cnx = e.Message;
                return null;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Ejecuta una t-sql que retorna un Dataset
        /// </summary>
        /// <param name="strSQL">instruccióni tsql: SELECT o EXEC PROCEDURE</param>
        /// <returns>DataSeto conjunto de tablas</returns>
        public DataSet Ejecutar_DataSet(String strSQL)
        {
            try
            {
                DataSet rs = new DataSet();
                Reconectar();

                using (SqlConnection tmpCn = new SqlConnection(this.StrConSQL))
                {
                    tmpCn.Open();
                    using (SqlCommand tmpCmd = new SqlCommand(strSQL, tmpCn) { CommandTimeout = 600 })
                    {
                        tmpCmd.CommandTimeout = 60;
                        using (SqlDataAdapter tmpDa = new SqlDataAdapter(tmpCmd))
                        {
                            tmpDa.Fill(rs);
                        }
                    }
                }

                return rs;
            }
            catch (Exception e)
            {
                this._Error_cnx = e.Message;
                return null;
            }
            finally
            {
            }
        }


        public DataTable llenar_datatable(String strSql)
        {
            try
            {
                DataTable tmpDt = new DataTable();
                Reconectar();

                using (SqlConnection tmpCn = new SqlConnection(this.StrConSQL))
                {
                    tmpCn.Open();
                    using (SqlCommand tmpCmd = new SqlCommand(strSql, tmpCn) { CommandTimeout = 600 })
                    {
                        using (SqlDataAdapter tmpDa = new SqlDataAdapter(tmpCmd))
                        {
                            tmpDa.Fill(tmpDt);
                        }
                    }
                }

                return tmpDt;
            }
            catch (Exception e)
            {
                this._Error_cnx = e.Message;
                return null;
            }
            finally
            {
            }
        }

        public void llenar_datatable(String strSql, ref DataTable tmpDt)
        {
            try
            {
                if (tmpDt == null)
                {
                    tmpDt = new DataTable();
                }
                tmpDt.Clear();

                Reconectar();

                using (SqlConnection tmpCn = new SqlConnection(this.StrConSQL))
                {
                    tmpCn.Open();
                    using (SqlCommand tmpCmd = new SqlCommand(strSql, tmpCn) { CommandTimeout = 600 })
                    {
                        using (SqlDataAdapter tmpDa = new SqlDataAdapter(tmpCmd))
                        {
                            tmpDa.Fill(tmpDt);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                this._Error_cnx = e.Message;
            }
            finally
            {
            }
        }

        public void llenar_dataset(String strSql, ref DataSet tmpDt)
        {
            try
            {
                if (tmpDt == null)
                {
                    tmpDt = new DataSet();
                }
                tmpDt.Clear();
                Reconectar();

                using (SqlConnection tmpCn = new SqlConnection(this.StrConSQL))
                {
                    tmpCn.Open();
                    using (SqlCommand tmpCmd = new SqlCommand(strSql, tmpCn) { CommandTimeout = 600 })
                    {
                        using (SqlDataAdapter tmpDa = new SqlDataAdapter(tmpCmd))
                        {
                            tmpDa.Fill(tmpDt);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                tmpDt = null;
                this._Error_cnx = e.Message;
            }
            finally
            {
            }
        }


        public void ejecutar_int(String strSql, ref int rn)
        {
            try
            {
                this._Error_cnx = string.Empty;
                Reconectar();

                using (SqlConnection tmpCn = new SqlConnection(this.StrConSQL))
                {
                    tmpCn.Open();
                    using (SqlCommand tmpcmd = new SqlCommand(strSql, tmpCn) { CommandTimeout = 600 })
                    {
                        tmpcmd.CommandTimeout = 600;
                        rn = int.Parse(tmpcmd.ExecuteScalar().ToString());
                    }
                }

            }
            catch (Exception e)
            {
                this._Error_cnx = e.Message;
                rn = 0;
            }
            finally
            {
            }
        }

        public void ejecutar_int64(String strSql, ref Int64 rn)
        {
            try
            {
                Reconectar();

                using (SqlConnection tmpCn = new SqlConnection(this.StrConSQL))
                {
                    tmpCn.Open();
                    using (SqlCommand tmpcmd = new SqlCommand(strSql, tmpCn) { CommandTimeout = 600 })
                    {
                        tmpcmd.CommandTimeout = 600;
                        rn = Int64.Parse(tmpcmd.ExecuteScalar().ToString());
                    }
                }

            }
            catch (Exception e)
            {
                this._Error_cnx = e.Message;
                rn = 0;
            }
            finally
            {
            }
        }
        public void ejecutar_Decimal(String strSql, ref decimal rn)
        {
            try
            {
                Reconectar();

                using (SqlConnection tmpCn = new SqlConnection(this.StrConSQL))
                {
                    tmpCn.Open();
                    using (SqlCommand tmpcmd = new SqlCommand(strSql, tmpCn) { CommandTimeout = 600 })
                    {
                        tmpcmd.CommandTimeout = 600;
                        rn = decimal.Parse(tmpcmd.ExecuteScalar().ToString());
                    }
                }

            }
            catch (Exception e)
            {
                this._Error_cnx = e.Message;
                rn = 0;
            }
            finally
            {
            }
        }

        public void ejecutar_str(String strSql, ref String rs)
        {
            try
            {
                Reconectar();

                using (SqlConnection tmpCn = new SqlConnection(this.StrConSQL))
                {
                    tmpCn.Open();
                    using (SqlCommand tmpcmd = new SqlCommand(strSql, tmpCn) { CommandTimeout = 600 })
                    {
                        tmpcmd.CommandTimeout = 600;
                        rs = tmpcmd.ExecuteScalar().ToString();
                    }
                }

            }
            catch (Exception e)
            {
                this._Error_cnx = e.Message;
                rs = string.Empty;
            }
            finally
            {

            }
        }

        public void ejecutar_datetime(String strSql, ref DateTime rd)
        {
            try
            {
                Reconectar();
                using (SqlConnection tmpCn = new SqlConnection(this.StrConSQL))
                {
                    tmpCn.Open();
                    using (SqlCommand tmpcmd = new SqlCommand(strSql, tmpCn) { CommandTimeout = 600 })
                    {
                        tmpcmd.CommandTimeout = 600;
                        rd = DateTime.Parse(tmpcmd.ExecuteScalar().ToString());
                    }
                }

            }
            catch (Exception e)
            {
                this._Error_cnx = e.Message;
            }
            finally
            {

            }
        }

        public void ejecutar_guid(String strSql, ref Guid rn)
        {
            try
            {
                DataTable tmptbl = new DataTable();
                Reconectar();

                tmptbl = Ejecutar_DataTable(strSql);
                if (TieneDatos(tmptbl))
                {
                    foreach (DataRow row in tmptbl.Rows)
                    {
                        rn = Guid.Parse(row[0].ToString());
                    }
                }
                else
                {
                    rn = Guid.Empty;
                }
            }
            catch (Exception e)
            {
                this._Error_cnx = e.Message;
                rn = Guid.Empty;
            }
            finally
            {

            }
        }


        #endregion

        #region Constructor y Destructor


        public cls_sistema_consql()
        {
            if (EsConectado)
            {
                this._cn.Close();
                _cn.Dispose();
            }
            _cn = null;
        }
        #endregion

        #region implementando dispose para consumir con  using

        public void Dispose()
        {

        }
    }
    #endregion
}
