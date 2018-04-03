using ApiFormatoJSon.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
//espacio de nombres para tareas Async
using System.Threading; //Generico(de toda la vida..un poco antiguo)
//nuevo espacio para TASK(moderno)
using System.Threading.Tasks;

namespace ApiFormatoJSon.Controllers
{
    public class EmpleadosController : ApiController
    {
        ModeloEmpleados modelo;
        public EmpleadosController()
        {
            this.modelo = new ModeloEmpleados();
        }

        //cuando devolvemos datos, debemos utilizar TASK<>
        public Task<List<EMP>> GetEmpleados()
        {
            List<EMP> empleados = modelo.GetEmpleados();
            //para devolver datos en TASK, se utuliza el metodo .fromResult<tipodato>(objeto)
            return Task.FromResult<List<EMP>>(empleados);
        }

        public async Task MetodoNormal()//si el metodo es un void solamente usaremos TASK
        {
            //en el codigo interno no hago ninguna peticion asincrona, pero nos permite poder
            //llamar al metodo, vamos que hay q poner async para llamar al metodo. 

        }



        //public EMP Get(int id)
        //{
        //    return modelo.BuscarEmpleados(id);
        //}

        public HttpResponseMessage Get(int id) //get del word
        {
            EMP empleado = modelo.BuscarEmpleados(id);

            var formato = new JsonMediaTypeFormatter();

            formato.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
            formato.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            formato.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            formato.SerializerSettings.Culture = new CultureInfo("es-ES");

            return Request.CreateResponse(HttpStatusCode.OK, empleado, formato);
        }

    }
}
