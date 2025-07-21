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
    public class cls_reportes_reportes : IDisposable
    {

        public int num { get; set; }
        public string Titulo { get; set; }
        public int IdUsuario_Envia { get; set; }
        public string strSQL { get; set; }
        public string Destinatarios { get; set; }
        public DateTime Hora_Enviar { get; set; }
        public string FH_Ultimo_Envio { get; set; }
        public bool Activo { get; set; }
        public bool Enviado { get; set; }
        public string Resultado { get; set; }
        public string Asunto { get; set; }
        public string Contenido { get; set; }
        public bool Es_HTML_Contenido { get; set; }
        public bool Es_Adjuntar_Datos { get; set; }
        public string Formato_Datos { get; set; }
        public string ArchivoRPT { get; set; }

        public List<cls_reportes_reportes> cmb = new List<cls_reportes_reportes>() { };

        public List<cls_reportes_reportes> Pendientes()
        {
            List<cls_reportes_reportes> lista = new List<cls_reportes_reportes>() { };
            try
            {
                string strsql = "EXEC reportes.reportes_Pendientes;";
                DataTable tbl = new DataTable();
                {
                    globales.consql.llenar_datatable(strsql, ref tbl);
                    if (globales.consql.TieneDatos(tbl))
                    {
                        foreach (DataRow row in tbl.Rows)
                        {
                            int nRepo = (int)row["#"];
                            using (cls_reportes_reportes tmpR = new cls_reportes_reportes())
                            {
                                if (tmpR.Recuperar(nRepo))
                                {
                                    lista.Add(tmpR);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return lista;
        }

        public void Marcar_Enviado(int nNUmReporte, string sResultado = "")
        {
            try
            {
                string strsql = string.Format("EXEC reportes.reportes_MarcarEnviado @#={0}, @resultado='{1}'",
                    nNUmReporte,
                    globales.comillas(sResultado));
                globales.consql.EjecutarTSQL(strsql);
            }
            catch (Exception)
            {
            }
        }

        public bool Enviar(int nNumReporte)
        {
            bool rs = false;
            try
            {
                cls_seguridad_usuarios_smtp tmpS = new cls_seguridad_usuarios_smtp();
                cls_seguridad_usuario tmpU = new cls_seguridad_usuario();
                cls_reportes_reportes tmpR = new cls_reportes_reportes();

                if (tmpR.Recuperar(nNumReporte))
                {
                    if (tmpU.Recuperar(tmpR.IdUsuario_Envia))
                    {
                        List<string> sHtml = new List<string>() { };
                        string newStrSQL = tmpR.strSQL;
                        if (tmpR.Formato_Datos == "HTML")
                        {
                            DataSet ds = new DataSet();
                            {

                                newStrSQL += string.Format(" @fecha='{0:yyyy-MM-dd}';", globales.consql.Last_CN_time);
                                globales.consql.llenar_dataset(newStrSQL, ref ds);
                                if (globales.consql.TieneDatos(ds))
                                {
                                    foreach (DataTable tbl in ds.Tables)
                                    {
                                        sHtml.Add(globales.ConvertDataTableToHTML(tbl, true));
                                    }
                                }
                            }
                            if (ds != null)
                            {
                                ds.Dispose();
                            }
                        }
                        string sContenido = "<html><body>";
                        sContenido += string.Format("<br><p>{0}</p><br>", tmpR.Contenido);
                        if (tmpR.Formato_Datos == "HTML")
                        {
                            foreach (string tabla in sHtml)
                            {
                                sContenido += tabla;
                            }
                        }
                        sContenido += "</body></html>";

                        if (tmpR.Formato_Datos == "XLSX")
                        {
                            //todo: agregar los asjuntos
                        }
                        if (tmpR.Formato_Datos == "PDF")
                        {
                            //todo: agregar los adjuntos
                        }
                        tmpS.Recuperar(tmpU.num);
                        var messageToSend = new MimeMessage
                        {
                            Sender = new MailboxAddress(tmpU.Nombre, tmpU.Email),
                            Subject = string.Format("{0} {1}", tmpR.Asunto, globales.WeeksAtDate(globales.consql.Last_CN_time))
                        };
                        messageToSend.ReplyTo.Add(new MailboxAddress(tmpU.Email, tmpU.Email));
                        messageToSend.Body = new TextPart(TextFormat.Html) { Text = sContenido };
                        string sDestinos = ";" + tmpR.Destinatarios + ";";
                        string[] arrayDestinos = sDestinos.Split(';');
                        foreach (var sD in arrayDestinos)
                        {
                            if (sD.Trim().Length > 0)
                            {
                                messageToSend.To.Add(new MailboxAddress(sD.Trim(), sD.Trim()));
                            }
                        }
                        using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                        {
                            try
                            {
                                smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                                smtp.Connect(tmpS.server_smtp, tmpS.server_port, SecureSocketOptions.Auto);
                                smtp.Authenticate(tmpS.server_login, tmpS.server_password);
                                smtp.Send(messageToSend);
                                string sResultado = "OK";
                                rs = true;
                                Marcar_Enviado(tmpR.num, "OK: " + sResultado);
                                smtp.Disconnect(true);
                                using (cls_reportes_log_reportes rlog = new cls_reportes_log_reportes())
                                {
                                    rlog.Guardar(tmpR.num, tmpU.num, tmpS.server_login, rs, "OK: " + sResultado, messageToSend.Subject, tmpR.Destinatarios, newStrSQL, messageToSend.Body.ToString());
                                }
                            }
                            catch (Exception ex)
                            {
                                using (cls_reportes_log_reportes rlog = new cls_reportes_log_reportes())
                                {
                                    rlog.Guardar(tmpR.num, tmpU.num, tmpS.server_login, false, "ERROR: " + ex.Message, messageToSend.Subject, tmpR.Destinatarios, newStrSQL, messageToSend.Body.ToString());
                                }
                            }
                        }
                    }
                }


            }
            catch (Exception e)
            {
                Marcar_Enviado(nNumReporte, "ERROR: " + e.Message);
            }
            return rs;
        }


        public bool Generar_cmb_todos()
        {
            try
            {
                this.cmb.Clear();
                bool rs = false;
                string strsql = "EXEC  reportes.reportes_Todos;";
                DataTable tbl = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        using (cls_reportes_reportes tmp = new cls_reportes_reportes())
                        {
                            tmp.num = int.Parse(row["#"].ToString());
                            tmp.Titulo = row["Titulo"].ToString();
                            tmp.IdUsuario_Envia = int.Parse(row["IdUsuario_Envia"].ToString());
                            tmp.strSQL = row["strSQL"].ToString();
                            tmp.Destinatarios = row["Destinatarios"].ToString();
                            tmp.Hora_Enviar = DateTime.Parse(row["Hora_Enviar"].ToString());
                            tmp.FH_Ultimo_Envio = row["FH_Ultimo_Envio"].ToString();
                            tmp.Activo = bool.Parse(row["Activo"].ToString());
                            tmp.Enviado = bool.Parse(row["Enviado"].ToString());
                            tmp.Resultado = row["Resultado"].ToString();
                            tmp.Asunto = row["Asunto"].ToString();
                            tmp.Contenido = row["Contenido"].ToString();
                            tmp.Es_HTML_Contenido = bool.Parse(row["Es_HTML_Contenido"].ToString());
                            tmp.Es_Adjuntar_Datos = bool.Parse(row["Es_Adjuntar_Datos"].ToString());
                            tmp.Formato_Datos = row["Formato_Datos"].ToString();
                            this.cmb.Add(tmp);
                            rs = true;
                        }
                    }
                }
                tbl.Dispose();

                return rs;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Guardar(int nNum,
            string sTitulo,
            int nIdUsuario_Envia,
            string sstrSQL,
            string sDestinatarios,
            DateTime dHora_Enviar,
            bool bActivo,
            string sAsunto,
            string sContenido,
            bool bEs_HTML_Contenido,
            bool bEs_Adjuntar_Datos,
            string sFormato_Datos,
            string sArchivoRPT)
        {
            try
            {
                bool rs = false;
                string strsql = string.Format("SET DATEFORMAT ymd; " +
                    "EXEC reportes.reportes_Guardar " +
                    "@# = {0}," +
                    "@Titulo = '{1}'," +
                    "@IdUsuario_Envia = {2}," +
                    "@strSQL = '{3}'," +
                    "@Destinatarios = '{4}'," +
                    "@Hora_Enviar = {5}," +
                    "@Activo = '{6}', " +
                    "@Asunto = '{7}', " +
                    "@Contenido = '{8}'," +
                    "@Es_HTML_Contenido = '{9}'," +
                    "@Es_Adjuntar_Datos = '{10}'," +
                    "@Formato_Datos = '{11}', " +
                    "@ArchivoRPT='{12}';",
                    nNum,
                    globales.comillas(sTitulo),
                    nIdUsuario_Envia,
                    globales.comillas(sstrSQL),
                    globales.comillas(sDestinatarios),
                    globales.sqldatetime(dHora_Enviar),
                    bActivo.ToString(),
                    globales.comillas(sAsunto),
                    globales.comillas(sContenido),
                    bEs_HTML_Contenido.ToString(),
                    bEs_Adjuntar_Datos.ToString(),
                    globales.comillas(sFormato_Datos),
                    globales.comillas(sArchivoRPT));
                int rn = 0;
                globales.consql.ejecutar_int(strsql, ref rn);
                rs = this.Recuperar(rn);
                return rs;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Recuperar(int nNum)
        {
            try
            {
                bool rs = false;
                string strsql = string.Format("EXEC reportes.reportes_Recuperar @# = {0};", nNum);
                DataTable tbl = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        this.num = int.Parse(row["#"].ToString());
                        this.Titulo = row["Titulo"].ToString();
                        this.IdUsuario_Envia = int.Parse(row["IdUsuario_Envia"].ToString());
                        this.strSQL = row["strSQL"].ToString();
                        this.Destinatarios = row["Destinatarios"].ToString();
                        this.Hora_Enviar = DateTime.Parse(row["Hora_Enviar"].ToString());
                        this.FH_Ultimo_Envio = row["FH_Ultimo_Envio"].ToString();
                        this.Activo = bool.Parse(row["Activo"].ToString());
                        this.Enviado = bool.Parse(row["Enviado"].ToString());
                        this.Resultado = row["Resultado"].ToString();
                        this.Asunto = row["Asunto"].ToString();
                        this.Contenido = row["Contenido"].ToString();
                        this.Es_HTML_Contenido = bool.Parse(row["Es_HTML_Contenido"].ToString());
                        this.Es_Adjuntar_Datos = bool.Parse(row["Es_Adjuntar_Datos"].ToString());
                        this.Formato_Datos = row["Formato_Datos"].ToString();
                        this.ArchivoRPT = row["ArchivoRPT"].ToString();
                        rs = true;
                    }
                }
                tbl.Dispose();

                return rs;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Nuevo()
        {
            this.num = 0;
            this.Titulo = string.Empty;
            this.IdUsuario_Envia = 0;
            this.strSQL = string.Empty;
            this.Destinatarios = string.Empty;
            this.Hora_Enviar = DateTime.Now;
            this.FH_Ultimo_Envio = string.Empty;
            this.Activo = false;
            this.Enviado = false;
            this.Resultado = string.Empty;
            this.Asunto = string.Empty;
            this.Contenido = string.Empty;
            this.Es_HTML_Contenido = false;
            this.Es_Adjuntar_Datos = false;
            this.Formato_Datos = string.Empty;
            this.ArchivoRPT = string.Empty;
            this.cmb.Clear();
        }

        public cls_reportes_reportes()
        {
            Nuevo();
        }

        void IDisposable.Dispose()
        {
        }
    }
}