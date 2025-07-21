using System;
using System.Linq;

namespace manufacturin_solution_apis
{
    /// <summary>
    /// Leonardo Martínez Núñez
    /// lmartinez@rocedes.com.ni
    /// ROCEDES, S.A.
    /// 02 de junio 2022
    /// </summary>
    public class cls_seguridad_formularios : IDisposable
    {
        public int num { get; set; }
        public string Formulario { get; set; }
        public string Descripcion { get; set; }
        public short numMódulo { get; set; }
        public string Módulo { get; set; }
        public bool Es_Visible { get; set; }
        public bool Es_Aplica_Acciones { get; set; }


        public string Visible { get => this.Es_Visible ? "YES" : "NO"; }
        public string Aplica_Acciones { get => this.Es_Aplica_Acciones ? "YES" : "NO"; }



        public void Nuevo()
        {
            this.num = 0;
            this.Formulario = string.Empty;
            this.Descripcion = string.Empty;
            this.numMódulo = 0;
            this.Módulo = string.Empty;
            this.Es_Visible = false;
            this.Es_Aplica_Acciones = false;
        }

        public cls_seguridad_formularios()
        {
            Nuevo();
        }

        void IDisposable.Dispose()
        {
        }
    }
}