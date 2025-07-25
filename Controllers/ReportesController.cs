﻿using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using manufacturin_solution_apis.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using Telerik.Reporting;
using Telerik.Reporting.Processing;
using static Kendo.Mvc.UI.UIPrimitives;


namespace manufacturin_solution_apis.Controllers
{
    public class ReportesController : Controller
    {
        #region :::::::::::::  MODULAR
        public IActionResult Modular_Proceso(int nLinea)
        {
            try
            {
                cls_dbo_linea objLinea = new cls_dbo_linea();
                objLinea.Recuperar(nLinea);
                TempData["data_linea"] = objLinea;
                string sArchivo = $"{Guid.NewGuid()}".Replace(":","").Replace("-","");
                ReportPackager reportPackager = new ReportPackager();
                ReportProcessor reportProcessor = new ReportProcessor();
                Hashtable deviceInfo = new Hashtable();
                UriReportSource reportSource = new UriReportSource();
                string logoUrl = Path.Combine("wwwroot", "Content","media");
                logoUrl = Path.Combine(logoUrl, "logSM.png");
                reportSource.Uri = Path.Combine("wwwroot", "Content","Reporting");
                reportSource.Uri = Path.Combine(reportSource.Uri, "Bihorario_x_Planning.trdp");
                Telerik.Reporting.Report reportInstance = (Telerik.Reporting.Report)reportPackager.UnpackageDocument(System.IO.File.OpenRead(reportSource.Uri));
                var sqlDS = reportInstance.GetDataSources().OfType<SqlDataSource>();
                foreach (var sqlDataSource in sqlDS)
                {
                    sqlDataSource.ConnectionString = globales.consql.StrConSQL;
                    sqlDataSource.SelectCommand = $"EXEC INTELSERVER.voids_cupones_tlrk_01 @line = '{globales.comillas(objLinea.refCostCenterPlanilla)}';";
                }
                var reporte = new InstanceReportSource() { ReportDocument = reportInstance };
                reporte.Parameters.Add(new Telerik.Reporting.Parameter("MiEmpresa", $"{globales.objEmpresa.Empresa}"));
                reporte.Parameters.Add(new Telerik.Reporting.Parameter("MiEslogan", $"{globales.objEmpresa.Eslogan}"));
                reporte.Parameters.Add(new Telerik.Reporting.Parameter("MisGenerales", $"{globales.objEmpresa.Dirección_Local}\n{globales.objEmpresa.Teléfonos}\n{globales.objEmpresa.Email}"));
                reporte.Parameters.Add(new Telerik.Reporting.Parameter("LogoEmpresa", $"{logoUrl}"));
                RenderingResult result = reportProcessor.RenderReport("PDF", reporte, deviceInfo);
                if (result.DocumentBytes != null)
                {

                    //var downloadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Content", "Downloads");
                    //var filePath = Path.Combine(downloadsFolder, sArchivo);
                    //if (System.IO.File.Exists(filePath))
                    //{
                    //    System.IO.File.Delete(filePath);
                    //}
                    //System.IO.File.WriteAllBytes(filePath, result.DocumentBytes);
                    //byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                    //return File(fileBytes, "application/force-download", sArchivo);

                    //FileContentResult fileContent = new FileContentResult(result.DocumentBytes, "application/pdf");
                    //return fileContent;

                    //return File(fileContent.FileContents, "application/force-download", "Reporte.pdf");
                    return File(result.DocumentBytes, "application/octet-stream", "Reporte.pdf");

                }
                else
                {
                    TempData["strError"] = "No hay datos disponibles";
                    return RedirectToAction("Error", "Home");
                }
            }
            catch (Exception e)
            {
                TempData["strError"] = e.Message;
                return RedirectToAction("Error", "Home");
            }

            //using (cls_dbo_linea objLinea = new cls_dbo_linea())
            //{
            //    objLinea.Recuperar(nLinea);
            //    TempData["data_linea"] = objLinea;

            //    cls_dbo_planta objPlanta = new cls_dbo_planta();
            //    objPlanta.Recuperar(objLinea.id_planta);
            //    TempData["data_planta"] = objPlanta;

            //    using (Items_ID_DESC t = new Items_ID_DESC())
            //    {
            //        TempData["data_procesos"] = t.grd($"EXEC INTELSERVER.Voids_con_saldo_linea @Linea = '{globales.comillas(objLinea.refCostCenterPlanilla)}';");
            //    }
            //}            
            //return View();
        }

        public IActionResult Modular_Linea(int nPlanta)
        {
            cls_dbo_planta objPlanta = new cls_dbo_planta();
            objPlanta.Recuperar(nPlanta);
            TempData["data_planta"] = objPlanta;
            using (cls_dbo_linea t = new cls_dbo_linea())
            {
                TempData["data_lineas"] = t.grd(nPlanta);
            }
            return View(); 
        }

        public IActionResult Modular() {
            //todo: cargar plantas y líneas y módulos
            using(cls_dbo_planta t = new cls_dbo_planta())
            {
                TempData["data_plantas"] = t.grd();
            }
            return View();
        }
        #endregion
        #region :::::::::::::  BIHORARIOS

        public IActionResult Bihorario_estilos(DateTime dtFecha, string sStyle)
        {
            if (sStyle == null)
                sStyle = "";
            ViewBag.dtFecha = dtFecha;
            ViewBag.sStyle = sStyle;

            //Ejecutando consulta global que devolverá 4 tablas 
            DataSet tbls = new DataSet();
            string strsql = $"EXEC intelserver.Ticket_escaneados_BH_x_operaciones_portal @fecha = {globales.sqldate(dtFecha)};";
            globales.consql.llenar_dataset(strsql, ref tbls);
            if (globales.consql.TieneDatos(tbls))
            {
                using (cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos t = new cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos())
                {
                    //obteniendo los estilos según BiHorario
                    ViewBag.dtFecha = dtFecha;
                    TempData["data_generales_bihorario_estilos"] = t.grd(tbls.Tables[0]);
                }

                using (cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos_operaciones t = new cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos_operaciones())
                {
                    //obteniendo todas las operaciones x estilos del día según cupones del bihorario
                    TempData["data_generales_bihorario_estilos_operaciones"] = t.grd(tbls.Tables[1]);
                }
                using (cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos_operaciones_prod t = new cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos_operaciones_prod())
                {
                    //obteniendo todas las operaciones x estilos del día según cupones
                    TempData["data_generales_bihorario_estilos_operaciones_prod"] = t.grd(tbls.Tables[3]);

                }

                if (sStyle.Trim().Length > 0)
                {

                    if (tbls.Tables[1].Select($"style='{globales.comillas(sStyle)}'").Length > 0)
                    {
                        DataTable tt = tbls.Tables[1].Select($"style='{globales.comillas(sStyle)}'").CopyToDataTable();

                        var ttO = (
                            from t1 in tt.AsEnumerable()
                            group new { t1 }
                            by new
                            {
                                style = t1["STYLE"].ToString(),
                                orden = int.Parse(t1["ORDEN"].ToString()),
                                operno = t1["operno"].ToString(),
                                descr = t1["descr"].ToString()
                            }
                            into grp
                            select new
                            {
                                style = grp.Key.style,
                                orden = grp.Key.orden,
                                operno = grp.Key.operno,
                                descr = grp.Key.descr,
                                Employees = grp.Sum(x => int.Parse(x.t1["Employees"].ToString())),
                                quantity = grp.Sum(x => int.Parse(x.t1["quantity"].ToString())),
                                tickets = grp.Sum(x => int.Parse(x.t1["tickets"].ToString())),
                                TOTAL_SAM = grp.Sum(x => decimal.Parse(x.t1["TOTAL_SAM"].ToString())),
                                Total_Pago = grp.Sum(x => decimal.Parse(x.t1["Total_Pago"].ToString()))
                            }
                            ).ToList();

                        List<cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos_operaciones> xOps = new List<cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos_operaciones>();
                        foreach (var tto in ttO)
                        {
                            xOps.Add(new cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos_operaciones()
                            {
                                orden = tto.orden
                                ,
                                descr = tto.descr
                                ,
                                Employees = tto.Employees
                                ,
                                operno = tto.operno
                                ,
                                quantity = tto.quantity
                                ,
                                style = tto.style
                                ,
                                tickets = tto.tickets
                                ,
                                Total_Pago = tto.Total_Pago
                                ,
                                TOTAL_SAM = tto.TOTAL_SAM
                            });
                        }
                        TempData["data_operaciones_seleccionado"] = xOps;
                    }


                    //obteniendo información del estilo seleccionado
                    cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos data_estilo_seleccionado = new cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos();
                    if (tbls.Tables[0].Select($"style='{globales.comillas(sStyle)}'").Length > 0)
                    {
                        DataTable tt = tbls.Tables[0].Select($"style='{globales.comillas(sStyle)}'").CopyToDataTable();
                        data_estilo_seleccionado.recuperar(tt);
                        TempData["data_estilo_seleccionado"] = data_estilo_seleccionado;
                    }
                    using (cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos_prod t = new cls_INTELSERVER_Ticket_escaneados_BH_x_operaciones_portal_estilos_prod())
                    {
                        //Obteniendo todas las órdenes de producción del estilo seleccionado
                        if (tbls.Tables[2].Select($"style='{globales.comillas(sStyle)}'").Length > 0)
                        {
                            DataTable tt = tbls.Tables[2].Select($"style='{globales.comillas(sStyle)}'").CopyToDataTable();
                            TempData["data_generales_bihorario_estilos_prod"] = t.grd(tt);
                        }
                    }
                }
            }
            return View();
        }

        public IActionResult Bihorario(DateTime dtFecha, string Seleccion = "", string Valor = "")
        {
            ViewBag.dtFecha = dtFecha;
            ViewBag.Seleccion_Bihorario = Seleccion;
            ViewBag.Valor_Seleccion_Bihorario = Valor;
            using (tbl_kpi_tickets_escaneados_x_dia_general t = new tbl_kpi_tickets_escaneados_x_dia_general())
            {

                if (Seleccion == "PLANTA")
                {
                    t.dtFecha = dtFecha;
                    TempData["kpi_generales_bihorario"] = t.grd_x_planta(Valor);
                }
                else if (Seleccion == "LINEA")
                {
                    t.dtFecha = dtFecha;
                    TempData["kpi_generales_bihorario"] = t.grd_x_linea(Valor);

                    using (tbl_kpi_tickets_escaneados_x_dia_Operaciones tto = new tbl_kpi_tickets_escaneados_x_dia_Operaciones())
                    {
                        tto.dtFecha = dtFecha;
                        TempData["kpi_operaciones_bihorario"] = tto.grd_x_linea(Valor);
                    }
                }
                else
                {
                    t.dtFecha = dtFecha;
                    TempData["kpi_generales_bihorario"] = t.grd();
                }
            }

            return View();
        }

        #endregion

        #region :::::::::::::  HOME
        public IActionResult Home()
        {
            return View();
        }
        #endregion

        #region :::::::::::::  DASHBOARD
        public IActionResult Dashboard(string txtUsuario, string txtClave, bool EsSesionIniciada)
        {
            bool esOK = false;

            try
            {
                if (txtClave == null) { txtClave = ""; }
                TempData["myUsuario"] = txtUsuario;
                TempData["myClave"] = string.Empty;
                string hName = Dns.GetHostEntry(HttpContext.Connection.RemoteIpAddress).HostName;
                string ipHost = HttpContext.Connection.RemoteIpAddress.ToString();
                if (EsSesionIniciada)
                {
                    cls_seguridad_sesiones tmpSesión = new cls_seguridad_sesiones();
                    if (globales.objSesión != null && globales.objSesión.activa)
                    {
                        tmpSesión = globales.objSesión;
                        TempData["myClave"] = tmpSesión.objUsuario.Clave;
                        TempData["mySesión"] = tmpSesión.sesión.ToString();
                        TempData["myUsuario"] = tmpSesión.objUsuario.Usuario;

                        Response.Cookies.Append("cookClave", tmpSesión.objUsuario.Clave, new Microsoft.AspNetCore.Http.CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            Expires = DateTime.Now.AddDays(1)
                        });
                        Response.Cookies.Append("cookUsuario", globales.Encriptar(txtUsuario), new Microsoft.AspNetCore.Http.CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            Expires = DateTime.Now.AddDays(1)
                        });

                        Response.Cookies.Append("cookSesion", globales.objSesión.sesión.ToString(), new Microsoft.AspNetCore.Http.CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            Expires = DateTime.Now.AddDays(1)
                        });


                        ViewBag.tmpSesión = tmpSesión;
                    }
                    esOK = true;
                }
                else
                {
                    if (txtClave.Trim().Length > 0)
                    {
                        Response.Cookies.Append("cookClave", txtClave, new Microsoft.AspNetCore.Http.CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            Expires = DateTime.Now.AddDays(1)
                        });
                    } 

                        try
                        {
                            if (globales.objSesión.Iniciar_Sesión(txtUsuario, globales.Encriptar(txtClave), hName, ipHost))
                            {

                                TempData["myClave"] = globales.objSesión.objUsuario.Clave;
                                TempData["mySesión"] = globales.objSesión.sesión;
                                TempData["myUsuario"] = globales.objSesión.objUsuario.Usuario;
                                ViewBag.tmpSesión = globales.objSesión;
                                esOK = true;

                                Response.Cookies.Append("cookClave", globales.objSesión.objUsuario.Clave, new Microsoft.AspNetCore.Http.CookieOptions
                                {
                                    HttpOnly = true,
                                    Secure = true,
                                    Expires = DateTime.Now.AddDays(1)
                                });
                                Response.Cookies.Append("cookUsuario", globales.Encriptar(txtUsuario), new Microsoft.AspNetCore.Http.CookieOptions
                                {
                                    HttpOnly = true,
                                    Secure = true,
                                    Expires = DateTime.Now.AddDays(1)
                                });

                                Response.Cookies.Append("cookSesion", globales.objSesión.sesión.ToString(), new Microsoft.AspNetCore.Http.CookieOptions
                                {
                                    HttpOnly = true,
                                    Secure = true,
                                    Expires = DateTime.Now.AddDays(1)
                                });
                                esOK = true;
                            }
                            else
                            {
                                Response.Cookies.Append("strMsgNtotificacion", "No fue posible iniciar sesión. Verificar Usuario y contraseña", new Microsoft.AspNetCore.Http.CookieOptions
                                {
                                    HttpOnly = true,
                                    Secure = true,
                                    Expires = DateTime.Now.AddDays(1)
                                });
                                esOK = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            TempData["strError"] = ex.Message;
                            esOK = false;
                        }
                }
            }
            catch (System.Exception ee)
            {
                TempData["strError"] = ee.Message;
                esOK = false;
            }
           
            if (esOK)
            {
                return View();
            }
            else {
                return RedirectToAction("Error", "Shared");
            }
        }
        #endregion

        #region :::::::::::::  INDEX
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region ::::::::::::::::::  DATA
        public IActionResult cupones_operacion(DateTime dtFecha, string operno, string linea)
        {
            try
            {
                ViewBag.operno = operno;
                ViewBag.linea = linea;
                ViewBag.dtFecha = dtFecha;
                using (cls_INTELSERVER_Employee te = new cls_INTELSERVER_Employee())
                {
                    te.Recuperar_x_Empno(operno);
                    ViewBag.objEmpleado = te;
                }
                using (cls_INTELSERVER_ticket_escaneados tt = new cls_INTELSERVER_ticket_escaneados())
                {
                    ViewBag.objTickets = tt.Tickets_X_Operacion(linea, operno, dtFecha);
                }
                using (cls_INTELSERVER_oper oper = new cls_INTELSERVER_oper())
                {
                    oper.Recuperar(operno);
                    ViewBag.objOper = oper;
                }
            }
            catch (Exception)
            { }
            return View();
        }

        public IActionResult cupones_operario(DateTime dtFecha, string empno, string linea)
        {
            try
            {
                ViewBag.empno = empno;
                ViewBag.linea = linea;
                using (cls_INTELSERVER_Employee te = new cls_INTELSERVER_Employee())
                {
                    te.Recuperar_x_Empno(empno);
                    ViewBag.objEmpleado = te;
                }
                using (cls_INTELSERVER_ticket_escaneados tt = new cls_INTELSERVER_ticket_escaneados())
                {
                    ViewBag.objTickets = tt.Tickets(empno, dtFecha);
                }
            }
            catch (Exception)
            { }
            return View();
        }

        public IActionResult tbl_kpi_tickets_escaneados_x_dia_general_generar([DataSourceRequest] DataSourceRequest request)
        {
            List<tbl_kpi_tickets_escaneados_x_dia_general> tmpObj = new List<tbl_kpi_tickets_escaneados_x_dia_general>() { };
            tmpObj = TempData["kpi_generales_bihorario"] as List<tbl_kpi_tickets_escaneados_x_dia_general>;
            return Json(tmpObj.ToDataSourceResult(request));
        }

        public JsonResult Elementos_TreeView_Bihorario(int? id)
        {
            try
            {
                cls_items_treeview_bihorario tt = new cls_items_treeview_bihorario();
                var xdata = tt.getData().ToList()
                    .Where(x => id.HasValue ? x.ParendID == id : x.ParendID == null)
                    .Select(item => new {
                        id = item.id,
                        Name = item.Name,
                        expanded = item.expanded,
                        hasChildren = item.hasChildren,
                        parentID = item.ParendID,
                        tipo = item.Tipo,
                        code = item.Code,
                        ItemImage = item.urlImagen
                    });

                return Json(xdata);
            }
            catch (Exception)
            {
                return null;
            }

        }

        #endregion

        #region ::::::::::::::::::  TELERIK EXPORT TO EXCEL
        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        #endregion ::::::::::::::: 
    }
}
