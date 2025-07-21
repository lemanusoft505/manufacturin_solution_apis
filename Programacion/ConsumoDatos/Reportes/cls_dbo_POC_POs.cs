using System;
using System.Linq;

namespace manufacturin_solution_apis
{
    public class cls_dbo_POC_POs : IDisposable
    {
        public string CUT { get; set; }
        public string STYLE { get; set; }
        public string CUSTOMER { get; set; }
        public decimal PRICE_NET { get; set; }
        public decimal WASH { get; set; }
        public decimal TRIMS { get; set; }
        public decimal SAM { get; set; }
        public string DESCRIPTION { get; set; }
        public int QUANTITY { get; set; }
        public int BUNDLES { get; set; }
        public string LINE { get; set; }
        public string PLANT { get; set; }
        public string CUT_DATE { get; set; }

        public cls_dbo_POC_POs()
        {
            this.STYLE = string.Empty;
            this.CUT = string.Empty;
            this.SAM = 0;
            this.CUSTOMER = string.Empty;
            this.PRICE_NET = 0;
            this.WASH = 0;
            this.TRIMS = 0;
            this.DESCRIPTION = string.Empty;
            this.QUANTITY = 0;
            this.BUNDLES = 0;
            this.LINE = string.Empty;
            this.PLANT = string.Empty;
            this.CUT_DATE = string.Empty;
        }
        void IDisposable.Dispose()
        {
        }
    }
}