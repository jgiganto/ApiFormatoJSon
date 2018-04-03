using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiFormatoJSon.Models
{
    public class ModeloEmpleados
    {
        ContextoEmpleados contexto;
        public ModeloEmpleados()
        {
            this.contexto = new ContextoEmpleados();
        }
        public List<EMP> GetEmpleados()
        {
            var consulta = from datos in contexto.EMP
                           select datos;
            return consulta.ToList();
        }

        public EMP BuscarEmpleados(int empno)
        {
            var consulta = from datos in contexto.EMP
                           where datos.EMP_NO == empno
                           select datos;
            return consulta.FirstOrDefault();
        }
    }
}