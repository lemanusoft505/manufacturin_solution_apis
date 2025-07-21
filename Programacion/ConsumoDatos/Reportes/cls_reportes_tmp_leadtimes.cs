using System;
using System.Linq;

namespace manufacturin_solution_apis
{
    public class cls_reportes_tmp_leadtimes : IDisposable
    {

        #region Atributos
        public string CUSTOMER { get; set; }
        public string CUT { get; set; }
        public string STYLE { get; set; }
        public int ISSUED_UNITS { get; set; }
        public int INVOICED_UNITS { get; set; }
        public string ISSUED_DATE { get; set; }
        public string CUT_DATE { get; set; }
        public string SENT_TO_LINE { get; set; }
        public string EARLIEST_DATE_SEWN { get; set; }
        public string LATEST_DATE_SEWN { get; set; }
        public string FIRST_DATE_SENT_TO_INTEX { get; set; }
        public string LAST_DATE_SENT_TO_INTEX { get; set; }
        public string FIRST_DATE_RECEIVED { get; set; }
        public string LAST_DATE_RECEIVED { get; set; }
        public string FIRST_DATE_SENT_TO_PACK { get; set; }
        public string LAST_DATE_SENT_TO_PACK { get; set; }
        public string FIRST_DATE_RECEIVED_IN_PACKING { get; set; }
        public string LAST_DATE_RECEIVED_IN_PACKING { get; set; }
        public string LAST_DATE_SHIPP { get; set; }
        public int ISSUE_CUT { get; set; }
        public int CUT_SENT { get; set; }
        public int SENT_SEWN { get; set; }
        public int SEWNING_PACKING { get; set; }
        public int PACKING_SHIPP { get; set; }
        public int ISSUE_TO_SHIPP { get; set; }

        #endregion

        public void Nuevo()
        {
            this.CUSTOMER = string.Empty;
            this.CUT = string.Empty;
            this.STYLE = string.Empty;
            this.ISSUED_UNITS = 0;
            this.INVOICED_UNITS = 0;
            this.ISSUED_DATE = string.Empty;
            this.CUT_DATE = string.Empty;
            this.SENT_TO_LINE = string.Empty;
            this.EARLIEST_DATE_SEWN = string.Empty;
            this.LATEST_DATE_SEWN = string.Empty;
            this.FIRST_DATE_SENT_TO_INTEX = string.Empty;
            this.LAST_DATE_SENT_TO_INTEX = string.Empty;
            this.FIRST_DATE_RECEIVED = string.Empty;
            this.LAST_DATE_RECEIVED = string.Empty;
            this.FIRST_DATE_SENT_TO_PACK = string.Empty;
            this.LAST_DATE_SENT_TO_PACK = string.Empty;
            this.FIRST_DATE_RECEIVED_IN_PACKING = string.Empty;
            this.LAST_DATE_RECEIVED_IN_PACKING = string.Empty;
            this.LAST_DATE_SHIPP = string.Empty;
            this.ISSUE_CUT = 0;
            this.CUT_SENT = 0;
            this.SENT_SEWN = 0;
            this.SEWNING_PACKING = 0;
            this.PACKING_SHIPP = 0;
            this.ISSUE_TO_SHIPP = 0;
        }

        public cls_reportes_tmp_leadtimes()
        {
            Nuevo();
        }

        void IDisposable.Dispose()
        {
        }
    }
}