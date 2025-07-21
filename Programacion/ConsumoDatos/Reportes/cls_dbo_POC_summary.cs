using System;
using System.Linq;

namespace manufacturin_solution_apis
{
    public class cls_dbo_POC_summary : IDisposable
    {
        public int IdPOrder { get; set; }
        public string POrder_Cliente { get; set; }
        public int Orden { get; set; }
        public int IdStep { get; set; }
        public string STEP { get; set; }
        public int QUANTITY { get; set; }
        public string START { get; set; }
        public string FINISH { get; set; }

        public cls_dbo_POC_summary()
        {
            this.FINISH = string.Empty;
            this.IdPOrder = 0;
            this.IdStep = 0;
            this.Orden = 0;
            this.POrder_Cliente = string.Empty;
            this.QUANTITY = 0;
            this.START = string.Empty;
            this.STEP = string.Empty;
        }

        void IDisposable.Dispose()
        {
        }
    }
}