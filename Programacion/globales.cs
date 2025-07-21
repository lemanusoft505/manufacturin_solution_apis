using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Threading.Tasks;

/// <summary>
/// Leonardo Martínez Núñez
/// lmartinez@rocedes.com.ni
/// ROCEDES, S.A.
/// 25 de Abril 2022
/// </summary>
namespace manufacturin_solution_apis
{
    /// <summary>
    /// Leonardo Martínez Núñez
    /// lmartinez@rocedes.com.ni
    /// ROCEDES, S.A.
    /// 02 de junio 2022
    /// </summary>
    public static class globales
    {

        public static TCollection Convertir_desde_IEnumerable<TCollection, TItem>(
    this IEnumerable<TItem> items, TCollection collection)
    where TCollection : ICollection<TItem>
        {
            foreach (var myObj in items)
                collection.Add(myObj);
            return collection;
        }


        #region Notificaciones
        public static string MensajeNotificacion = string.Empty;
        #endregion

        #region Sistema y Seguridad

        public const string versionApp = "2025.07.21.5";
        public const String mssql_servidor = "sistema.rocedes.com.ni";
        public const String mssql_login = "sistema";
        public const String mssql_contraseña = "Yatebya..$2025";
        public const String mssql_basedatos = "reportes_gerencia";
        public const byte mssql_idempresa = 1;
        public const String mssql_App = "ROCEDES_REPORTES_GERENCIA";
        public const short mssql_minutos_sesion_activa = 15;
        public const string NombreComercialEmpresa = "ROCEDES, S.A.";
        
        public static String urlBase = string.Empty;
        public static String dirBase = string.Empty;
        public static String LayPadre = "_Layout";//"~/Views/Shared/_Layout.cshtml";
        public static string RutaAdjuntos = "\\Content\\Uploads\\";
        public static string RootPath = string.Empty;
        public static List<string> ListaFormatoImagen = new List<string> { ".BMP", ".PNG", ".GIF", ".JPG", ".JPEG" };
        public static List<string> ListaFormatoDocumentos = new List<string> { ".DOC", ".DOCX", ".XLS", ".XLSX" };
        public static List<string> ListaFormatoPDF = new List<string> { ".PDF" };

        public enum NFormatosRepotesAdjuntos : short
        {
            nXLS = 1,
            nXLSX = 2,
            nHTML = 3
        }

        public enum NTipoRecursos : byte
        {
            /// <summary>
            /// Just backing for recover it later
            /// </summary>
            nRespaldo = 1,
            /// <summary>
            /// Invoice = Shipping
            /// </summary>
            nInvoice = 2,
            /// <summary>
            /// Creating new Production Order
            /// </summary>
            nIssueUnit = 3,
            /// <summary>
            /// WIP - Work in progress
            /// </summary>
            nWorkInProcess = 4,
            /// <summary>
            /// cutted POrder
            /// </summary>
            nCutting = 5,
            /// <summary>
            /// sewing POrder
            /// </summary>
            nSewing = 6,
            /// <summary>
            /// washed POrder
            /// </summary>
            nWashing = 7,
            /// <summary>
            /// Packed POrder
            /// </summary>
            nPacking = 8,
            /// <summary>
            /// Failed items, PRODUCTO NO CONFORME
            /// </summary>
            nIrregular = 9,
            nPrecios_x_Estilos = 10,
            nPrecios_x_POrders = 11,
            nCuts_Canceled = 12
        }

        public static Utilities.Encryption objEnc = new Utilities.Encryption();

        public static cls_sistema_consql consql = new cls_sistema_consql();
        public static cls_sistema_empresa objEmpresa = new cls_sistema_empresa();
        //public static cls_seguridad_roles objRoles = new cls_seguridad_roles();

        public static int IdUsuario_Seleccionado = 0;

        #endregion

        public const byte Estado_Pendiente = 1;
        public const byte Estado_Anulado = 0;
        public const byte Estado_Aplicado = 2;


        #region Módulo Seguridad
        public const byte Permisos_Accion_Abrir_Form = 1;
        public const byte Permisos_Accion_Guardar = 2;
        public const byte Permisos_Accion_Eliminar = 3;
        public const byte Permisos_Accion_Anular = 4;
        public const byte Permisos_Accion_Vista_Previa = 5;
        public const byte Permisos_Accion_Aplica = 6;


        public const int Formularios_SEGURIDAD_USUARIOS = 1;
        public const int Formularios_SEGURIDAD_PASSCHANGE = 2;
        public const int Formularios_SYSTEMA_COMPANY = 3;
        public const int Formularios_INGENIERIA_SINGLE = 4;
        public const int Formularios_SYSTEMA_MYFILES = 5;
        public const int Formularios_REPORTES_DASHBOARD = 6;
        public const int Formularios_REPORTS_TO_SEND = 7;
        public const int Formularios_PRODUCCION_PO_LIST = 8;
        public const int Formularios_SHIPPING_INVOICES = 9;
        public const int Formularios_SEDURIDAD_ROLES = 10;
        public const int Formularios_INGENIERIA_STYLES = 11;

        public const int Formularios_IDM_REPORTS_CUTED_TODAY = 32;


        public const int Permisos_Accion_OPEN = 1;
        public const int Permisos_Accion_SAVE = 2;
        public const int Permisos_Accion_DELETE = 3;
        public const int Permisos_Accion_AVOID = 4;
        public const int Permisos_Accion_PREVIEW = 5;
        public const int Permisos_Accion_APPLY = 6;


        public static cls_seguridad_usuario objUsuario = new cls_seguridad_usuario();
        public static cls_seguridad_sesiones objSesión = new cls_seguridad_sesiones();
        #endregion

        #region procedimientos globales


        public static void Recuperar2Nvalues(string strsql, ref int value1, ref decimal value2)
        {
            try
            {
                value1 = 0;
                value2 = 0;
                DataTable dtValues = new System.Data.DataTable();
                consql.llenar_datatable(strsql, ref dtValues);
                if (consql.TieneDatos(dtValues))
                {
                    foreach (DataRow row in dtValues.Rows)
                    {
                        value1 = int.Parse(row[0].ToString());
                        value2 = decimal.Parse(row[1].ToString());

                    }
                }
                dtValues.Dispose();
            }
            catch (Exception)
            {
            }
        }

        public static int WeeksAtDate(DateTime dtFecha)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dtFecha, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }

        public static async Task<bool> ir_Actualizar_DatosPO()
        {
            MensajeNotificacion = string.Empty;
            try
            {
                string sSesión = System.Guid.NewGuid().ToString();
                string strsql = "EXEC dbo.POrder_Status_inicializar;";

                //paso 1: actualizar universo de ordenes de producción
                globales.consql.EjecutarTSQL(strsql);

                //paso 2: obtener cutting en la línea o pendiente de enviar a la siguiente etapa               
                strsql = string.Format("EXEC dbo.POrder_Status_Cutting_refresh @Sesión = '{0}';", sSesión);
                globales.consql.EjecutarTSQL(strsql);


                // paso 6: marcar las PO terminadas
                strsql = "EXEC dbo.POrder_Status_IsDone_refresh;";
                globales.consql.EjecutarTSQL(strsql);

                //paso 7: fin
                return true;
            }
            catch (Exception e)
            {
                MensajeNotificacion = e.Message;
                return false;
            }
        }

        public static string ConvertDataTableToHTML_CurrentStock(string strsql)
        {
            string shtml = "<table id=\"tblData\" class='table table-bordered table-condensed table-responsive'>";
            DataTable dt = new DataTable();
            globales.consql.llenar_datatable(strsql, ref dt);
            if (!globales.consql.TieneDatos(dt))
            {
                shtml = "<h3>NO DATA</h3>";
            }
            else
            {
                //encabezado
                shtml += "<thead><tr>";
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string strValor = string.Empty;
                    string tipo = dt.Columns[i].DataType.Name;

                    switch (tipo.ToUpper())
                    {
                        case "STRING": strValor = "<td align=left>" + dt.Columns[i].ColumnName.ToUpper(); break;
                        case "INT": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                        case "INT32": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                        case "INT16": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                        case "INT8": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                        case "DATE": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                        case "DATETIME": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                        case "DECIMAL": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                        default: strValor = "<td align=left>" + dt.Columns[i].ColumnName.ToUpper(); break;
                    }

                    shtml += strValor + "</td>";
                }
                shtml += "</tr></thead>";
                //filas
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    shtml += "<tbody><tr>";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string strValor = string.Empty;
                        string tipo = dt.Rows[i][j].GetType().Name;
                        switch (tipo.ToUpper())
                        {
                            case "STRING": strValor = "<td align=left>" + dt.Rows[i][j].ToString(); break;
                            case "INT32":
                                if (j > 1)
                                {
                                    strValor = "<td align=right>" +
                                        "<form method=\"get\" action=\"/Reportes/CurrentStockDetail\">" +
                                        "<input type=\"hidden\" name=\"cliente\" id=\"cliente\" value=\"" + dt.Rows[i][1].ToString() + "\"/>" +
                                        "<input type=\"hidden\" name=\"etapa\" id=\"etapa\" value=\"" + dt.Columns[j].ColumnName.ToUpper() + "\"/>" +
                                        "<span>" +
                                        "<button type=\"submit\" class=\"fa fa-arrow-circle-right\"> " + string.Format("{0:n0}", dt.Rows[i][j]) + "</button>" +
string.Empty + "</span>" +
                                        "</form>";


                                }
                                else
                                {
                                    strValor = "<td align=right>" + string.Format("{0:n0}", dt.Rows[i][j]);
                                }

                                break;
                            case "INT": strValor = "<td align=right>" + string.Format("{0:n0}", dt.Rows[i][j]); break;
                            case "INT16": strValor = "<td align=right>" + string.Format("{0:n0}", dt.Rows[i][j]); break;
                            case "INT8": strValor = "<td align=right>" + string.Format("{0:n0}", dt.Rows[i][j]); break;
                            case "DATE": strValor = "<td align=right>" + string.Format("{0:yyyy-MM-dd}", dt.Rows[i][j]); break;
                            case "DATETIME": strValor = "<td align=right>" + string.Format("{0:yyyy-MM-dd}", dt.Rows[i][j]); break;
                            case "DECIMAL": strValor = "<td align=right>" + string.Format("{0:n2}", dt.Rows[i][j]); break;
                            default: strValor = "<td align=left>" + dt.Rows[i][j].ToString(); break;
                        }

                        shtml += strValor + "</td>";
                    }

                    shtml += "</tr></tbody>";
                }
            }

            shtml += "</table>";
            return shtml;
        }


        public static string ConvertDataTableToHTML(string strsql)
        {
            string shtml = "<table id=\"tblData\" class=\"table table-bordered table-condensed table-responsive\">";
            DataTable dt = new DataTable();
            globales.consql.llenar_datatable(strsql, ref dt);
            if (!globales.consql.TieneDatos(dt))
            {
                shtml = "<h3>NO DATA</h3>";
            }
            else
            {
                //encabezado
                shtml += "<thead><tr>";
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string strValor = string.Empty;
                    string tipo = dt.Columns[i].DataType.Name;

                    switch (tipo.ToUpper())
                    {
                        case "STRING": strValor = "<td align=left>" + dt.Columns[i].ColumnName.ToUpper(); break;
                        case "INT": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                        case "INT32": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                        case "INT16": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                        case "INT8": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                        case "DATE": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                        case "DATETIME": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                        case "DECIMAL": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                        default: strValor = "<td align=left>" + dt.Columns[i].ColumnName.ToUpper(); break;
                    }

                    shtml += strValor + "</td>";
                }
                shtml += "</tr></thead><tbody>";
                //filas
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    shtml += "<tr>";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string strValor = string.Empty;
                        string tipo = dt.Rows[i][j].GetType().Name;
                        switch (tipo.ToUpper())
                        {
                            case "STRING": strValor = "<td align=left>" + dt.Rows[i][j].ToString(); break;
                            case "INT": strValor = "<td align=right>" + string.Format("{0:n0}", dt.Rows[i][j]); break;
                            case "INT32": strValor = "<td align=right>" + string.Format("{0:n0}", dt.Rows[i][j]); break;
                            case "INT16": strValor = "<td align=right>" + string.Format("{0:n0}", dt.Rows[i][j]); break;
                            case "INT8": strValor = "<td align=right>" + string.Format("{0:n0}", dt.Rows[i][j]); break;
                            case "DATE": strValor = "<td align=right>" + string.Format("{0:yyyy-MM-dd}", dt.Rows[i][j]); break;
                            case "DATETIME": strValor = "<td align=right>" + string.Format("{0:yyyy-MM-dd}", dt.Rows[i][j]); break;
                            case "DECIMAL": strValor = "<td align=right>" + string.Format("{0:n2}", dt.Rows[i][j]); break;
                            default: strValor = "<td align=left>" + dt.Rows[i][j].ToString(); break;
                        }

                        shtml += strValor + "</td>";
                    }


                }

                shtml += "</tr></tbody><tfoot><tr>";

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string strValor = string.Empty;
                    string tipo = dt.Columns[i].DataType.Name;

                    switch (tipo.ToUpper())
                    {
                        case "STRING": strValor = "<td align=left>" + dt.Columns[i].ColumnName.ToUpper(); break;
                        case "INT": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                        case "INT32": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                        case "INT16": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                        case "INT8": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                        case "DATE": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                        case "DATETIME": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                        case "DECIMAL": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                        default: strValor = "<td align=left>" + dt.Columns[i].ColumnName.ToUpper(); break;
                    }

                    shtml += strValor + "</td>";
                }
                shtml += "</tr></tfoot>";
            }

            shtml += "</table>";
            return shtml;
        }


        public static string ConvertDataTableToHTML(DataTable dt, bool Es_Decimal_Money = false)
        {
            string shtml = "<table class='table table-bordered table-condensed table-responsive' border=1 cellspacing=1 cellpadding=1>";
            //encabezado
            shtml += "<thead><tr style='height:15.0pt'>";
            string strStyleTD = "nowrap valign=bottom style='border:none;border-top:solid black 1.0pt;background:black;padding:0cm 5.4pt 0cm 5.4pt;height:15.0pt'";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                string strValor = string.Empty;
                string tipo = dt.Columns[i].DataType.Name;

                switch (tipo.ToUpper())
                {
                    case "STRING":
                        strValor = $"<td align=left {strStyleTD}><span style='color:white'>"
                 + dt.Columns[i].ColumnName.ToUpper() + "</span>"; break;
                    case "INT":
                        strValor = $"<td align=right {strStyleTD}><span style='color:white'>"
                    + dt.Columns[i].ColumnName.ToUpper() + "</span>"; break;
                    case "INT32":
                        strValor = $"<td align=right {strStyleTD}><span style='color:white'>"
                  + dt.Columns[i].ColumnName.ToUpper() + "</span>"; break;
                    case "INT16":
                        strValor = $"<td align=right {strStyleTD}><span style='color:white'>"
                  + dt.Columns[i].ColumnName.ToUpper() + "</span>"; break;
                    case "INT8":
                        strValor = $"<td align=right {strStyleTD}><span style='color:white'>" +
                   dt.Columns[i].ColumnName.ToUpper() + "</span>"; break;
                    case "DATE":
                        strValor = $"<td align=right {strStyleTD}><span style='color:white'>"
                   + dt.Columns[i].ColumnName.ToUpper() + "</span>"; break;
                    case "DATETIME":
                        strValor = $"<td align=right {strStyleTD}><span style='color:white'>"
               + dt.Columns[i].ColumnName.ToUpper() + "</span>"; break;
                    case "DECIMAL":
                        strValor = $"<td align=right {strStyleTD}><span style='color:white'>"
                + dt.Columns[i].ColumnName.ToUpper() + "</span>"; break;
                    default:
                        strValor = $"<td align=left {strStyleTD}><span style='color:white'>"
                       + dt.Columns[i].ColumnName.ToUpper() + "</span>"; break;
                }

                shtml += strValor + "</td>";
            }
            shtml += "</tr></thead><tbody>";
            //filas
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                shtml += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string strValor = string.Empty;
                    string tipo = dt.Rows[i][j].GetType().Name;
                    switch (tipo.ToUpper())
                    {
                        case "STRING": strValor = "<td align=left>" + dt.Rows[i][j].ToString(); break;
                        case "INT": strValor = "<td align=right>" + string.Format("{0:n0}", dt.Rows[i][j]); break;
                        case "INT32": strValor = "<td align=right>" + string.Format("{0:n0}", dt.Rows[i][j]); break;
                        case "INT16": strValor = "<td align=right>" + string.Format("{0:n0}", dt.Rows[i][j]); break;
                        case "INT8": strValor = "<td align=right>" + string.Format("{0:n0}", dt.Rows[i][j]); break;
                        case "DATE": strValor = "<td align=right>" + string.Format("{0:yyyy-MM-dd}", dt.Rows[i][j]); break;
                        case "DATETIME": strValor = "<td align=right>" + string.Format("{0:yyyy-MM-dd}", dt.Rows[i][j]); break;
                        case "DECIMAL":
                            if (Es_Decimal_Money)
                            {
                                strValor = "<td align=right>" + string.Format("${0:n2}", dt.Rows[i][j]);
                            }
                            else
                            {
                                strValor = "<td align=right>" + string.Format("{0:n2}", dt.Rows[i][j]);
                            }

                            break;
                        default: strValor = "<td align=left>" + dt.Rows[i][j].ToString(); break;
                    }

                    shtml += strValor + "</td>";
                }

                shtml += "</tr></tbody>";
            }
            shtml += "</table>";
            return shtml;
        }

        public static string ConvertDataTableToHTML(DataTable dt, int nTop)
        {
            string shtml = "<table class='table table-bordered table-condensed table-responsive'>";
            //encabezado
            shtml += "<thead><tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                string strValor = string.Empty;
                string tipo = dt.Columns[i].DataType.Name;

                switch (tipo.ToUpper())
                {
                    case "STRING": strValor = "<td align=left>" + dt.Columns[i].ColumnName.ToUpper(); break;
                    case "INT": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                    case "INT32": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                    case "INT16": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                    case "INT8": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                    case "DATE": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                    case "DATETIME": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                    case "DECIMAL": strValor = "<td align=right>" + dt.Columns[i].ColumnName.ToUpper(); break;
                    default: strValor = "<td align=left>" + dt.Columns[i].ColumnName.ToUpper(); break;
                }

                shtml += strValor + "</td>";
            }
            shtml += "</tr></thead><tbody>";
            //filas
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                shtml += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string strValor = string.Empty;
                    string tipo = dt.Rows[i][j].GetType().Name;
                    switch (tipo.ToUpper())
                    {
                        case "STRING": strValor = "<td align=left>" + dt.Rows[i][j].ToString(); break;
                        case "INT": strValor = "<td align=right>" + string.Format("{0:n0}", dt.Rows[i][j]); break;
                        case "INT32": strValor = "<td align=right>" + string.Format("{0:n0}", dt.Rows[i][j]); break;
                        case "INT16": strValor = "<td align=right>" + string.Format("{0:n0}", dt.Rows[i][j]); break;
                        case "INT8": strValor = "<td align=right>" + string.Format("{0:n0}", dt.Rows[i][j]); break;
                        case "DATE": strValor = "<td align=right>" + string.Format("{0:yyyy-MM-dd}", dt.Rows[i][j]); break;
                        case "DATETIME": strValor = "<td align=right>" + string.Format("{0:yyyy-MM-dd}", dt.Rows[i][j]); break;
                        case "DECIMAL": strValor = "<td align=right>" + string.Format("{0:n2}", dt.Rows[i][j]); break;
                        default: strValor = "<td align=left>" + dt.Rows[i][j].ToString(); break;
                    }

                    shtml += strValor + "</td>";
                }

                shtml += "</tr>";
                if (i >= nTop)
                {
                    break;
                }
            }
            shtml += "</tbody></table>";
            return shtml;
        }


        public static String hColor(System.Drawing.Color c)
        {
            // 
            String rtn = String.Empty;
            try
            {

                rtn = "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
            }
            catch (Exception)
            {

            }

            return rtn;
        }

        public static String PonerCeros(int nNum, int nCeros)
        {
            String rs = String.Format("{0}", nNum).PadLeft(nCeros, '0');
            return rs.Substring(rs.Length - nCeros);
        }

        public static string Encriptar(String sFrase)
        {
            String rs = string.Empty;
            try
            {
                objEnc.FraseOrigen = sFrase;
                rs = objEnc.Encrypt();
            }
            catch (Exception)
            {
                rs = string.Empty;
            }
            return rs;
        }

        public static string Desencriptar(String sFrase)
        {
            String rs = string.Empty;
            try
            {
                objEnc.FraseOrigen = sFrase;
                rs = objEnc.Decrypt();
            }
            catch (Exception)
            {
                rs = string.Empty;
            }
            return rs;
        }

        public static string sqldate(DateTime fecha)
        {
            return string.Format("'{0}-{1}-{2}'", fecha.Year, fecha.Month, fecha.Day);
        }

        public static string sqldate_hasta(DateTime fecha)
        {
            return string.Format("'{0}-{1}-{2} 23:59:59'", fecha.Year, fecha.Month, fecha.Day);
        }

        public static string sqldatetime(DateTime fecha)
        {
            return string.Format("'{0}-{1}-{2} {3}:{4}:{5}'", fecha.Year, fecha.Month, fecha.Day, fecha.Hour, fecha.Minute, fecha.Second);
        }

        public static string sqltime(DateTime fecha)
        {
            return string.Format("'{0}:{1}:{2}'", fecha.Hour, fecha.Minute, fecha.Second);
        }

        public static string comillas(string sTexto)
        {
            if (sTexto == null)
            {
                return string.Empty;
            }
            else
            {
                return sTexto.Replace("'", "''");
            }

        }

        public static bool IsNumeric(this string text) => double.TryParse(text, out _);

        #endregion
    }
}
