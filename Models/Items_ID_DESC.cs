using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace manufacturin_solution_apis.Models
{
    public class Items_ID_DESC : IDisposable
    {

        public int ID { get; set; }
        public string DESCRIPTION { get; set; }

        public List<Items_ID_DESC> grd(string strsql)
        {
            List<Items_ID_DESC> rs = new List<Items_ID_DESC>();
            try
            {
                DataTable tbl = new DataTable();
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        rs.Add(new Items_ID_DESC()
                        {
                            ID = int.Parse(row[0].ToString()),
                            DESCRIPTION=row[1].ToString()
                        });
                    }
                }
                tbl.Dispose();
            }
            catch (Exception ex)
            {
                rs = null;
            }
            return rs;
        }

        public Items_ID_DESC()
        {
            ID = 0;DESCRIPTION = "";
        }

        public void Dispose()
        {
        }
    }
}
