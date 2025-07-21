using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace manufacturin_solution_apis
{
    /// <summary>
    /// Leonardo Martínez Núñez
    /// lmartinez@rocedes.com.ni
    /// lemanusoft@hotmail.com
    /// 2022.09.01
    /// </summary>
    public class cls_INTELSERVER_ticket_escaneados : IDisposable
    {
        #region Atributos

        public Int64 num { get; set; }
        public string Usuario { get; set; }
        public string empno { get; set; }
        public string LOC1 { get; set; }
        public string LOC2 { get; set; }
        public string LOC3 { get; set; }
        public string COSTCR { get; set; }
        public string Prodno { get; set; }
        public string Style { get; set; }
        public string Customer { get; set; }
        public DateTime FH { get; set; }
        public DateTime FH_Bihorario { get; set; }
        public string Dispositivo { get; set; }
        public int Quant_Issued { get; set; }
        public string Carnet { get; set; }
        public string Employe_Name { get; set; }
        public string OperNo { get; set; }
        public int Bundle { get; set; }
        public short Quant_Bundle { get; set; }
        public int Id_POrder { get; set; }
        public bool Es_Procesado { get; set; }
        public string Prod_Descr { get; set; }
        public string Oper_Descr { get; set; }
        public string Section_Descr { get; set; }
        public string Line_Device { get; set; }
        public string Plant_Device { get; set; }
        public string POrderClient { get; set; }
        public byte Id_BiHorario { get; set; }
        public string Rango_BiHorario { get; set; }
        public decimal rate { get; set; }
        public int Norma { get; set; }
        public decimal SAM { get; set; }
        public decimal Pago { get; set; }
        public int Ticket { get; set; }
        public decimal Duracion { get; set; }
        public string Inicia { get; set; }
        public string Termina { get; set; }

        private string _strError { get; set; }
        public string strError { get => _strError; }

        public decimal TOTAL_SAM { get =>decimal.Parse($"{this.Quant_Bundle}") * this.SAM; }
        public decimal TOTAL_PAGO { get => this.Pago; }

        #endregion

        #region Métodos

        private string sincomillas(string sTexto) { return globales.comillas(sTexto); }

        public List<cls_INTELSERVER_ticket_escaneados> Tickets_X_Operacion(string sLinea, string sOperNo, DateTime dtFecha)
        {
            _strError = "";
            List<cls_INTELSERVER_ticket_escaneados> rs = new List<cls_INTELSERVER_ticket_escaneados>();
            try
            {
                string strsql = $"EXEC INTELSERVER.Ticket_escaneados_x_Linea_x_Operacion_Fecha " +
                    $"@linea = '{sincomillas(sLinea)}'," +
                    $"@operno = '{sincomillas(sOperNo)}'," +
                    $"@fecha = {globales.sqldate(dtFecha)};";
                DataTable tbl = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        rs.Add(new cls_INTELSERVER_ticket_escaneados()
                        {
                            num = Int64.Parse(row["#"].ToString()),
                            Usuario = row["Usuario"].ToString(),
                            empno = row["empno"].ToString(),
                            LOC1 = row["LOC1"].ToString(),
                            LOC2 = row["LOC2"].ToString(),
                            LOC3 = row["LOC3"].ToString(),
                            COSTCR = row["COSTCR"].ToString(),
                            Prodno = row["Prodno"].ToString(),
                            Style = row["Style"].ToString(),
                            Customer = row["Customer"].ToString(),
                            FH = DateTime.Parse(row["FH"].ToString()),
                            FH_Bihorario = DateTime.Parse(row["FH_Bihorario"].ToString()),
                            Dispositivo = row["Dispositivo"].ToString(),
                            Quant_Issued = int.Parse(row["Quant_Issued"].ToString()),
                            Carnet = row["Carnet"].ToString(),
                            Employe_Name = row["Employe_Name"].ToString(),
                            OperNo = row["OperNo"].ToString(),
                            Bundle = int.Parse(row["Bundle"].ToString()),
                            Quant_Bundle = short.Parse(row["Quant_Bundle"].ToString()),
                            Id_POrder = int.Parse(row["Id_POrder"].ToString()),
                            Es_Procesado = bool.Parse(row["Es_Procesado"].ToString()),
                            Prod_Descr = row["Prod_Descr"].ToString(),
                            Oper_Descr = row["Oper_Descr"].ToString(),
                            Section_Descr = row["Section_Descr"].ToString(),
                            Line_Device = row["Line_Device"].ToString(),
                            Plant_Device = row["Plant_Device"].ToString(),
                            POrderClient = row["POrderClient"].ToString(),
                            Id_BiHorario = byte.Parse(row["Id_BiHorario"].ToString()),
                            Rango_BiHorario = $"{row["Rango_BiHorario"]}",
                            rate = decimal.Parse(row["rate"].ToString()),
                            Norma = int.Parse(row["Norma"].ToString()),
                            SAM = decimal.Parse(row["SAM"].ToString()),
                            Pago = decimal.Parse(row["Pago"].ToString()),
                            Ticket = int.Parse(row["Ticket"].ToString()),
                            Duracion = Convert.IsDBNull(row["Duracion"]) ? 0 : decimal.Parse(row["Duracion"].ToString()),
                            Inicia = $"{row["Inicia"]}",
                            Termina = $"{row["Termina"]}"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _strError = ex.Message;
                rs = null;
            }
            return rs;
        }

        public List<cls_INTELSERVER_ticket_escaneados> Tickets(string sEmpNo, DateTime dtFecha) {
            _strError = "";
            List <cls_INTELSERVER_ticket_escaneados> rs = new List<cls_INTELSERVER_ticket_escaneados>();
            try
            {
                string strsql = $"EXEC INTELSERVER.Ticket_escaneados_x_EmpNo_Fecha " +
                    $"@empno = '{sincomillas(sEmpNo)}'," +
                    $"@fecha = {globales.sqldate(dtFecha)};";
                DataTable tbl = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl)) {
                    foreach (DataRow row in tbl.Rows) {
                        rs.Add(new cls_INTELSERVER_ticket_escaneados() {
                            num = Int64.Parse(row["#"].ToString()),
                            Usuario = row["Usuario"].ToString(),
                            empno = row["empno"].ToString(),
                            LOC1 = row["LOC1"].ToString(),
                            LOC2 = row["LOC2"].ToString(),
                            LOC3 = row["LOC3"].ToString(),
                            COSTCR = row["COSTCR"].ToString(),
                            Prodno = row["Prodno"].ToString(),
                            Style = row["Style"].ToString(),
                            Customer = row["Customer"].ToString(),
                            FH = DateTime.Parse(row["FH"].ToString()),
                            FH_Bihorario = DateTime.Parse(row["FH_Bihorario"].ToString()),
                            Dispositivo = row["Dispositivo"].ToString(),
                            Quant_Issued = int.Parse(row["Quant_Issued"].ToString()),
                            Carnet = row["Carnet"].ToString(),
                            Employe_Name = row["Employe_Name"].ToString(),
                            OperNo = row["OperNo"].ToString(),
                            Bundle = int.Parse(row["Bundle"].ToString()),
                            Quant_Bundle = short.Parse(row["Quant_Bundle"].ToString()),
                            Id_POrder = int.Parse(row["Id_POrder"].ToString()),
                            Es_Procesado = bool.Parse(row["Es_Procesado"].ToString()),
                            Prod_Descr = row["Prod_Descr"].ToString(),
                            Oper_Descr = row["Oper_Descr"].ToString(),
                            Section_Descr = row["Section_Descr"].ToString(),
                            Line_Device = row["Line_Device"].ToString(),
                            Plant_Device = row["Plant_Device"].ToString(),
                            POrderClient = row["POrderClient"].ToString(),
                            Id_BiHorario = byte.Parse(row["Id_BiHorario"].ToString()),
                            Rango_BiHorario = $"{row["Rango_BiHorario"]}",
                            rate = decimal.Parse(row["rate"].ToString()),
                            Norma = int.Parse(row["Norma"].ToString()),
                            SAM = decimal.Parse(row["SAM"].ToString()),
                            Pago = decimal.Parse(row["Pago"].ToString()),
                            Ticket = int.Parse(row["Ticket"].ToString()),
                            Duracion = Convert.IsDBNull(row["Duracion"]) ? 0 :decimal.Parse(row["Duracion"].ToString()),
                            Inicia = $"{row["Inicia"]}",
                            Termina = $"{row["Termina"]}"
                        }) ;
                    }
                }
            }
            catch (Exception ex)
            {
                _strError = ex.Message;
                rs = null;
            }
            return rs;
        }

        public bool Recuperar(int nNum)
        {
            bool rs = false;
            try
            {
                _strError = string.Empty;
                string strsql = string.Format("exec dbo.plantilla_recuperar @#={0};", nNum);
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
                    this.num = Int64.Parse(row["#"].ToString());                  
                    this.Usuario = row["Usuario"].ToString();
                    this.empno = row["empno"].ToString();
                    this.LOC1 = row["LOC1"].ToString();
                    this.LOC2 = row["LOC2"].ToString();
                    this.LOC3 = row["LOC3"].ToString();
                    this.COSTCR = row["COSTCR"].ToString();
                    this.Prodno = row["Prodno"].ToString();
                    this.Style = row["Style"].ToString();
                    this.Customer = row["Customer"].ToString();
                    this.FH = DateTime.Parse(row["FH"].ToString());
                    this.FH_Bihorario = DateTime.Parse(row["FH_Bihorario"].ToString());
                    this.Dispositivo = row["Dispositivo"].ToString();
                    this.Quant_Issued = int.Parse(row["Quant_Issued"].ToString());
                    this.Carnet = row["Carnet"].ToString();
                    this.Employe_Name = row["Employe_Name"].ToString();
                    this.OperNo = row["OperNo"].ToString();
                    this.Bundle = int.Parse(row["Bundle"].ToString());
                    this.Quant_Bundle = short.Parse(row["Quant_Bundle"].ToString());
                    this.Id_POrder = int.Parse(row["Id_POrder"].ToString());
                    this.Es_Procesado = bool.Parse(row["Es_Procesado"].ToString());
                    this.Prod_Descr = row["Prod_Descr"].ToString();
                    this.Oper_Descr = row["Oper_Descr"].ToString();
                    this.Section_Descr = row["Section_Descr"].ToString();
                    this.Line_Device = row["Line_Device"].ToString();
                    this.Plant_Device = row["Plant_Device"].ToString();
                    this.POrderClient = row["POrderClient"].ToString();
                    this.Id_BiHorario = byte.Parse(row["Id_BiHorario"].ToString());
                    this.Rango_BiHorario = row["Rango_BiHorario"].ToString();
                    this.rate = decimal.Parse(row["rate"].ToString());
                    this.Norma = int.Parse(row["Norma"].ToString());
                    this.SAM = decimal.Parse(row["SAM"].ToString());
                    this.Pago = decimal.Parse(row["Pago"].ToString());
                    this.Ticket = int.Parse(row["Ticket"].ToString());
                    this.Duracion = decimal.Parse(row["Duracion"].ToString());
                    this.Inicia = row["Inicia"].ToString();
                    this.Termina = row["Termina"].ToString();
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
            _strError = string.Empty;
            this.num=0;
            this.Usuario = "";
            this.empno = "";
            this.LOC1 = "";
            this.LOC2 = "";
            this.LOC3 = "";
            this.COSTCR = "";
            this.Prodno = "";
            this.Style = "";
            this.Customer = "";
            this.FH = DateTime.Now;
            this.FH_Bihorario = DateTime.Now;
            this.Dispositivo = "";
            this.Quant_Issued = 0;
            this.Carnet = "";
            this.Employe_Name = "";
            this.OperNo = "";
            this.Bundle = 0;
            this.Quant_Bundle = 0;
            this.Id_POrder = 0;
            this.Es_Procesado = false;
            this.Prod_Descr = "";
            this.Oper_Descr = "";
            this.Section_Descr = "";
            this.Line_Device = "";
            this.Plant_Device = "";
            this.POrderClient = "";
            this.Id_BiHorario = 0;
            this.Rango_BiHorario = "";
            this.rate = 0;
            this.Norma = 0;
            this.SAM = 0;
            this.Pago = 0;
            this.Ticket = 0;
            this.Duracion = 0;
            this.Inicia = "";
            this.Termina = "";
        }

        public cls_INTELSERVER_ticket_escaneados()
        {
            Nuevo();
        }



        void IDisposable.Dispose()
        {
        }
        #endregion

    }
}