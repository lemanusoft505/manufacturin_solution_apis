using manufacturin_solution_apis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace manufacturin_solution_apis.Models
{
    public class cls_items_treeview_bihorario : IDisposable
    {
        public int id { get; set; }
        public int? ParendID { get; set; }
        public bool hasChildren { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Tipo { get; set; }
        public bool expanded { get; set; }
        public string urlImagen { get; set; }


        string setImagen(string sTipo)
        {
            string ui = "../../Content/media/24x24/star_full.png";
            if (sTipo == "02-PLANTA") { ui = "../../Content/media/24x24/home.png"; }
            if (sTipo == "03-LINEA") { ui = "../../Content/media/24x24/female_male_users.png"; }
            return ui;
        }
        public List<cls_items_treeview_bihorario> getData()
        {
            List<cls_items_treeview_bihorario> data = new List<cls_items_treeview_bihorario>() { };
            try
            {
                DataTable tbl = new DataTable();
                string strsql = "EXEC  dbo.Bihorario_treeview_Plantas_Líneas;";
                globales.consql.llenar_datatable(strsql, ref tbl);
                if (globales.consql.TieneDatos(tbl))
                {
                    DataTable tt = tbl.Select("", "Orden").CopyToDataTable();
                    foreach (DataRow row in tt.Rows)
                    {
                        data.Add(new cls_items_treeview_bihorario()
                        {
                            id = int.Parse($"{row["ID"]}")
                            ,
                            ParendID = row.Field<int?>("ParendID")
                            ,
                            hasChildren = row.Field<bool>("HasChildren")
                            ,
                            Code = row.Field<string>("Code")
                            ,
                            Name = row.Field<string>("Name")
                            ,
                            Tipo = row.Field<string>("Tipo")
                            ,
                            expanded = false
                            ,
                            urlImagen = setImagen(row.Field<string>("Tipo"))
                        });
                    }
                }
            }
            catch (Exception)
            {
                data = null;
            }
            return data;
        }



        public cls_items_treeview_bihorario()
        {
            id = 1; ParendID = null; hasChildren = false; Code = ""; Name = ""; Tipo = ""; expanded = false; urlImagen = string.Empty;
        }

        public void Dispose()
        {
        }
    }
}