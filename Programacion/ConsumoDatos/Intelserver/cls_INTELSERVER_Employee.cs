using System;
using System.Data;

namespace manufacturin_solution_apis
{
    public class cls_INTELSERVER_Employee : IDisposable
    {
        #region Atributos

        public string EMPNO { get; set; }
        public string badgeno { get; set; }
        public string LOC1 { get; set; }
        public string LOC2 { get; set; }
        public string LOC3 { get; set; }
        public string LOC4 { get; set; }
        public string LOC5 { get; set; }
        public string SERVDATE { get; set; }
        public string STATE { get; set; }
        public string ZIPCODE { get; set; }
        public string PAYFREQ { get; set; }
        public string PAYTYPE { get; set; }
        public string EMPTYPE { get; set; }
        public string ESTATUS { get; set; }
        public string STATUS { get; set; }
        public string SEX { get; set; }
        public string COSTCTR { get; set; }
        public string MODULAR { get; set; }
        public string GLNUM { get; set; }
        public decimal SALARY { get; set; }
        public string paygroup { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        public string socsecnum { get; set; }
        public string position { get; set; }
        public string profession { get; set; }
        public string cedula { get; set; }
        public string cedulaloc { get; set; }
        public string municipality { get; set; }


        private string _strError { get; set; }
        public string strError { get => _strError; }
        string TablaDatos { get { return "INTELSERVER.Employee"; } }
        #endregion

        #region Métodos
        
        public bool Recuperar_x_Empno(string sEmpno)
        {
            bool rs = false;
            try
            {
                _strError = "";
                string strsql = string.Format("EXEC {0}_recuperar2 @EmpNo = '{1}';",
                    this.TablaDatos, globales.comillas(sEmpno));
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

        public bool Recuperar_x_Carnet(string sCarnet)
        {
            bool rs = false;
            try
            {
                _strError = "";
                string strsql = string.Format("EXEC {0}_recuperar_X_Carnet @carnet = '{1}';", 
                    this.TablaDatos, globales.comillas(sCarnet));
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
            _strError = "";
            try
            {
                foreach (DataRow row in tbl.Rows)
                {
                    this.EMPNO = row["EMPNO"].ToString();
                    this.badgeno = row["badgeno"].ToString();
                    this.LOC1 = row["LOC1"].ToString();
                    this.LOC2 = row["LOC2"].ToString();
                    this.LOC3 = row["LOC3"].ToString();
                    this.LOC4 = row["LOC4"].ToString();
                    this.LOC5 = row["LOC5"].ToString();
                    this.SERVDATE = row["SERVDATE"].ToString();
                    this.STATE = row["STATE"].ToString();
                    this.ZIPCODE = row["ZIPCODE"].ToString();
                    this.PAYFREQ = row["PAYFREQ"].ToString();
                    this.PAYTYPE = row["PAYTYPE"].ToString();
                    this.EMPTYPE = row["EMPTYPE"].ToString();
                    this.ESTATUS = row["ESTATUS"].ToString();
                    this.STATUS = row["STATUS"].ToString();
                    this.SEX = row["SEX"].ToString();
                    this.COSTCTR = row["COSTCTR"].ToString();
                    this.MODULAR = row["MODULAR"].ToString();
                    this.GLNUM = row["GLNUM"].ToString();
                    this.SALARY = decimal.Parse(row["SALARY"].ToString());
                    this.paygroup = row["paygroup"].ToString();
                    this.lastname = row["lastname"].ToString();
                    this.firstname = row["firstname"].ToString();
                    this.socsecnum = row["socsecnum"].ToString();
                    this.position = row["position"].ToString();
                    this.profession = row["profession"].ToString();
                    this.cedula = row["cedula"].ToString();
                    this.cedulaloc = row["cedulaloc"].ToString();
                    this.municipality = row["municipality"].ToString();
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
            _strError = "";
            this.EMPNO = "";
            this.badgeno = "";
            this.LOC1 = "";
            this.LOC2 = "";
            this.LOC3 = "";
            this.LOC4 = "";
            this.LOC5 = "";
            this.SERVDATE = "";
            this.STATE = "";
            this.ZIPCODE = "";
            this.PAYFREQ = "";
            this.PAYTYPE = "";
            this.EMPTYPE = "";
            this.ESTATUS = "";
            this.STATUS = "";
            this.SEX = "";
            this.COSTCTR = "";
            this.MODULAR = "";
            this.GLNUM = "";
            this.SALARY = 0;
            this.paygroup = "";
            this.lastname = "";
            this.firstname = "";
            this.socsecnum = "";
            this.position = "";
            this.profession = "";
            this.cedula = "";
            this.cedulaloc = "";
            this.municipality = "";
        }


        #endregion

        #region constructor y destructor
        public cls_INTELSERVER_Employee()
        {
            Nuevo();
        }

        void IDisposable.Dispose()
        {
        }
        #endregion

    }
}