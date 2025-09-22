using asp_servicios.Nucleo;
using lib_aplicaciones.Interfaces;
using lib_dominio.Entidades;
using lib_dominio.Nucleo;
using Microsoft.AspNetCore.Mvc;

namespace asp_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TiposComponentesController : ControllerBase
    {
        private ITiposComponentesAplicacion? iAplicacion = null;
        private TokenController? tokenController = null;

        public TiposComponentesController(ITiposComponentesAplicacion? iAplicacion,
            TokenController tokenController)
        {
            this.iAplicacion = iAplicacion;
            this.tokenController = tokenController;
        }

        private Dictionary<string, object> ObtenerDatos()
        {
            var datos = new StreamReader(Request.Body).ReadToEnd().ToString();
            if (string.IsNullOrEmpty(datos))
                datos = "{}";

            return JsonConversor.ConvertirAObjeto(datos);
        }

        [HttpPost]
        public string Listar()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                if (!tokenController!.Validate(datos))
                {
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));
                respuesta["Entidades"] = iAplicacion.Listar();
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now.ToString();

                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message;
                return JsonConversor.ConvertirAString(respuesta);
            }
        }

        [HttpPost]
        public string Guardar()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                if (!tokenController!.Validate(datos))
                {
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                var entidad = JsonConversor.ConvertirAObjeto<TiposComponentes>(
                    JsonConversor.ConvertirAString(datos["Entidad"]));

                iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));
                entidad = iAplicacion.Guardar(entidad);

                respuesta["Entidad"] = entidad!;
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now.ToString();

                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message;
                return JsonConversor.ConvertirAString(respuesta);
            }
        }

        [HttpPost]
        public string Modificar()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                if (!tokenController!.Validate(datos))
                {
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                var entidad = JsonConversor.ConvertirAObjeto<TiposComponentes>(
                    JsonConversor.ConvertirAString(datos["Entidad"]));

                iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));
                entidad = iAplicacion.Modificar(entidad);

                respuesta["Entidad"] = entidad!;
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now.ToString();

                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message;
                return JsonConversor.ConvertirAString(respuesta);
            }
        }

        [HttpPost]
        public string Borrar()
        {
            var respuesta = new Dictionary<string, object>();
            try
            {
                var datos = ObtenerDatos();
                if (!tokenController!.Validate(datos))
                {
                    respuesta["Error"] = "lbNoAutenticacion";
                    return JsonConversor.ConvertirAString(respuesta);
                }

                var entidad = JsonConversor.ConvertirAObjeto<TiposComponentes>(
                    JsonConversor.ConvertirAString(datos["Entidad"]));

                iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));
                entidad = iAplicacion.Borrar(entidad);

                respuesta["Entidad"] = entidad!;
                respuesta["Respuesta"] = "OK";
                respuesta["Fecha"] = DateTime.Now.ToString();

                return JsonConversor.ConvertirAString(respuesta);
            }
            catch (Exception ex)
            {
                respuesta["Error"] = ex.Message;
                return JsonConversor.ConvertirAString(respuesta);
            }
        }
    }
}
