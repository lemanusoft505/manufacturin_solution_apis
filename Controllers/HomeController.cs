﻿using manufacturin_solution_apis.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace manufacturin_solution_apis.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Downloads() { return View(); }

        public IActionResult Descargar(string sArchivo) {
            try
            {
                var filePath = GetFilePathFromId(sArchivo);
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, "application/force-download", sArchivo);
            }
            catch (Exception e)
            {
                TempData["strError"] = e.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                cls_seguridad_sesiones tmpSesión = new cls_seguridad_sesiones();

                string cookieSesión = "";
                Request.Cookies.TryGetValue("cookSesion", out cookieSesión);
                if (cookieSesión != null)
                {
                    tmpSesión = new cls_seguridad_sesiones();

                    if (tmpSesión.Recuperar($"{cookieSesión}"))
                    {
                        TempData["myClave"] = tmpSesión.objUsuario.Clave;
                        TempData["mySesión"] = tmpSesión.sesión;
                        TempData["myUsuario"] = tmpSesión.objUsuario.Usuario;
                        globales.objSesión = tmpSesión;
                        return RedirectToAction("Dashboard", "Reportes", new { txtUsuario = tmpSesión.objUsuario.Usuario, txtClave = tmpSesión.objUsuario.Clave, EsSesionIniciada = "true" });
                    }
                }
            }
            catch (Exception)
            {}
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Método auxiliar para obtener la ruta física del archivo a partir del identificador
        public string GetFilePathFromId(string sArchivo)
        {
            var downloadsFolder = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "Content", "Downloads");
            var filePath = Path.Combine(downloadsFolder, sArchivo);

            if (System.IO.File.Exists(filePath))
                return filePath;

            return null;
        }
    }
}
