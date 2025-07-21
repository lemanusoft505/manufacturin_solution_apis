using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace manufacturin_solution_apis.Controllers
{
    public class ReportesController : Controller
    {
        public IActionResult Dashboard2(string txtUsuario, string txtClave, bool EsSesionIniciada)
        {
            return View();
        }

        public IActionResult Dashboard(string txtUsuario, string txtClave, bool EsSesionIniciada)
        {
            try
            {
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
                        TempData["mySesión"] = tmpSesión.sesión;
                        TempData["myObjUsuario"] = tmpSesión.objUsuario;
                        TempData["myObjSesión"] = tmpSesión;

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
                    return View();
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
                    bool esOK = false;
                    try
                    {
                        if (globales.objSesión.Iniciar_Sesión(txtUsuario, txtClave, hName, ipHost))
                        {

                            TempData["myClave"] = globales.objSesión.objUsuario.Clave;
                            TempData["mySesión"] = globales.objSesión.sesión;
                            TempData["myObjUsuario"] = globales.objSesión.objUsuario;
                            TempData["myObjSesión"] = globales.objSesión;
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

                        }
                        if (esOK)
                        {
                            return View();
                        }
                        else
                        {
                            Response.Cookies.Append("strMsgNtotificacion", "No fue posible iniciar sesión. Verificar Usuario y contraseña", new Microsoft.AspNetCore.Http.CookieOptions
                            {
                                HttpOnly = true,
                                Secure = true,
                                Expires = DateTime.Now.AddDays(1)
                            });                            
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.strError = ex.Message;
                        return View("Error", "Home");
                    }
                }
            }
            catch (System.Exception ee)
            {
                ViewBag.strError = ee.Message;
                return View("Error", "Home");
            }
           
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
