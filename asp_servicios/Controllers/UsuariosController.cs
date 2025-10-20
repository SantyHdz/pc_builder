    using asp_servicios.Nucleo;
    using lib_aplicaciones.Implementaciones;
    using lib_aplicaciones.Interfaces;
    using lib_dominio.Entidades;
    using lib_dominio.Nucleo;
    using Microsoft.AspNetCore.Mvc;

    namespace asp_servicios.Controllers
    {
        [ApiController]
        [Route("[controller]/[action]")]
        public class UsuariosController : ControllerBase
        {
            private IUsuariosAplicacion? iAplicacion = null;
            private TokenController? tokenController = null;

            public UsuariosController(IUsuariosAplicacion? iAplicacion,
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

                    this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion")); 
                    var usuarios = this.iAplicacion!.Listar();
                    
                    foreach (var usuario in usuarios)
                    {
                        usuario.ContrasenaHash = null;
                    }

                    respuesta["Entidades"] = usuarios;
                    respuesta["Respuesta"] = "OK";
                    respuesta["Fecha"] = DateTime.Now.ToString();
                    return JsonConversor.ConvertirAString(respuesta);
                }
                catch (Exception ex)
                {
                    respuesta["Error"] = ex.Message.ToString();
                    return JsonConversor.ConvertirAString(respuesta);
                }
            }
            
            [HttpPost]
            public string Registrar()
            {
                var respuesta = new Dictionary<string, object>();
                try
                {
                    var datos = ObtenerDatos();
        
                    var entidad = JsonConversor.ConvertirAObjeto<Usuarios>(
                        JsonConversor.ConvertirAString(datos["Entidad"]));

                    // Validaciones básicas
                    if (string.IsNullOrEmpty(entidad.Correo))
                    {
                        respuesta["Error"] = "El correo es obligatorio";
                        return JsonConversor.ConvertirAString(respuesta);
                    }

                    if (string.IsNullOrEmpty(entidad.ContrasenaHash))
                    {
                        respuesta["Error"] = "La contraseña es obligatoria";
                        return JsonConversor.ConvertirAString(respuesta);
                    }

                    // Verificar si el correo ya existe
                    this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));
                    var usuarioExistente = this.iAplicacion!.PorCorreo(entidad);
        
                    if (usuarioExistente != null && usuarioExistente.Count > 0)
                    {
                        respuesta["Error"] = "El correo ya está registrado";
                        return JsonConversor.ConvertirAString(respuesta);
                    }
                    
                    entidad.ContrasenaHash = HashUtil.ComputeSha256Hash(entidad.ContrasenaHash);

                    
                    entidad = this.iAplicacion!.Guardar(entidad);

                    
                    entidad.ContrasenaHash = null;

                    respuesta["Entidad"] = entidad!;
                    respuesta["Respuesta"] = "OK";
                    respuesta["Fecha"] = DateTime.Now.ToString();
                    return JsonConversor.ConvertirAString(respuesta);
                }
                catch (Exception ex)
                {
                    respuesta["Error"] = ex.Message.ToString();
                    return JsonConversor.ConvertirAString(respuesta);
                }
            }
            
            [HttpPost]
            public string Login()
            {
                var respuesta = new Dictionary<string, object>();
                try
                {
                    var datos = ObtenerDatos();
        
                    var correo = datos.ContainsKey("Correo") ? datos["Correo"].ToString() : "";
                    var contrasena = datos.ContainsKey("Contrasena") ? datos["Contrasena"].ToString() : "";

                    if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena))
                    {
                        respuesta["Error"] = "Correo y contraseña son obligatorios";
                        return JsonConversor.ConvertirAString(respuesta);
                    }

                    
                    var contrasenaHash = HashUtil.ComputeSha256Hash(contrasena);

                    this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));
                    var usuarios = this.iAplicacion!.PorCorreo(new Usuarios { Correo = correo });

                    if (usuarios == null || usuarios.Count == 0)
                    {
                        respuesta["Error"] = "Usuario no encontrado";
                        return JsonConversor.ConvertirAString(respuesta);
                    }

                    var usuario = usuarios[0];

                    
                    if (usuario.ContrasenaHash != contrasenaHash)
                    {
                        respuesta["Error"] = "Contraseña incorrecta";
                        return JsonConversor.ConvertirAString(respuesta);
                    }
                    
                    
                    usuario.ContrasenaHash = null;

                    respuesta["Entidad"] = usuario;
                    respuesta["Respuesta"] = "OK";
                    respuesta["Fecha"] = DateTime.Now.ToString();
                    return JsonConversor.ConvertirAString(respuesta);
                }
                catch (Exception ex)
                {
                    respuesta["Error"] = ex.Message.ToString();
                    return JsonConversor.ConvertirAString(respuesta);
                }
            }

            [HttpPost]
            public string PorCorreo()
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

                    var entidad = JsonConversor.ConvertirAObjeto<Usuarios>(
                        JsonConversor.ConvertirAString(datos["Entidad"]));

                    this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion")); 
                    var usuarios = this.iAplicacion!.PorCorreo(entidad);

                    
                    foreach (var usuario in usuarios)
                    {
                        usuario.ContrasenaHash = null;
                    }

                    respuesta["Entidades"] = usuarios;
                    respuesta["Respuesta"] = "OK";
                    respuesta["Fecha"] = DateTime.Now.ToString();
                    return JsonConversor.ConvertirAString(respuesta);
                }
                catch (Exception ex)
                {
                    respuesta["Error"] = ex.Message.ToString();
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

                    var entidad = JsonConversor.ConvertirAObjeto<Usuarios>(
                        JsonConversor.ConvertirAString(datos["Entidad"]));

                    this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion")); 
                    entidad = this.iAplicacion!.Guardar(entidad);

                    respuesta["Entidad"] = entidad!;
                    respuesta["Respuesta"] = "OK";
                    respuesta["Fecha"] = DateTime.Now.ToString();
                    return JsonConversor.ConvertirAString(respuesta);
                }
                catch (Exception ex)
                {
                    respuesta["Error"] = ex.Message.ToString();
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

                    var entidad = JsonConversor.ConvertirAObjeto<Usuarios>(
                        JsonConversor.ConvertirAString(datos["Entidad"]));


                    this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion")); 
                    entidad = this.iAplicacion!.Modificar(entidad);

                    respuesta["Entidad"] = entidad!;
                    respuesta["Respuesta"] = "OK";
                    respuesta["Fecha"] = DateTime.Now.ToString();
                    return JsonConversor.ConvertirAString(respuesta);
                }
                catch (Exception ex)
                {
                    respuesta["Error"] = ex.Message.ToString();
                    return JsonConversor.ConvertirAString(respuesta);
                }
            }
            
            [HttpPost]
public string RecuperarContrasena()
{
    var respuesta = new Dictionary<string, object>();
    try
    {
        
        var datos = ObtenerDatos();
        var correo = datos.ContainsKey("Correo") ? datos["Correo"].ToString() : "";
        var nuevaContrasena = datos.ContainsKey("NuevaContrasena") ? datos["NuevaContrasena"].ToString() : "";

        
        if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(nuevaContrasena))
        {
            respuesta["Error"] = "Correo y nueva contraseña son obligatorios";
            return JsonConversor.ConvertirAString(respuesta);
        }

        
        if (nuevaContrasena.Length < 6)
        {
            respuesta["Error"] = "La contraseña debe tener al menos 6 caracteres";
            return JsonConversor.ConvertirAString(respuesta);
        }

        
        this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion"));
        var usuarios = this.iAplicacion!.PorCorreo(new Usuarios { Correo = correo });

        if (usuarios == null || usuarios.Count == 0)
        {
            respuesta["Error"] = "No se encontró un usuario con ese correo";
            return JsonConversor.ConvertirAString(respuesta);
        }

        
        var usuario = usuarios[0];
        usuario.ContrasenaHash = HashUtil.ComputeSha256Hash(nuevaContrasena);

        usuario = this.iAplicacion!.Modificar(usuario);

        
        usuario.ContrasenaHash = null;

        respuesta["Entidad"] = usuario;
        respuesta["Respuesta"] = "OK";
        respuesta["Fecha"] = DateTime.Now.ToString();
        return JsonConversor.ConvertirAString(respuesta);
    }
    catch (Exception ex)
    {
        respuesta["Error"] = ex.Message.ToString();
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

                    var entidad = JsonConversor.ConvertirAObjeto<Usuarios>(
                        JsonConversor.ConvertirAString(datos["Entidad"]));

                    this.iAplicacion!.Configurar(Configuracion.ObtenerValor("StringConexion")); 
                    entidad = this.iAplicacion!.Borrar(entidad);

                    respuesta["Entidad"] = entidad!;
                    respuesta["Respuesta"] = "OK";
                    respuesta["Fecha"] = DateTime.Now.ToString();
                    return JsonConversor.ConvertirAString(respuesta);
                }
                catch (Exception ex)
                {
                    respuesta["Error"] = ex.Message.ToString();
                    return JsonConversor.ConvertirAString(respuesta);
                }
            }
        }
    }