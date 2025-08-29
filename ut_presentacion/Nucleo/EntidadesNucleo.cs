using lib_dominio.Entidades;
using lib_repositorios.Interfaces;

namespace ut_presentacion.Nucleo;

public class EntidadesNucleo
{

    public static TiposComponentes? TiposComponentes()
    {
        TiposComponentes? entidad = new TiposComponentes();
        entidad.Nombre = "Unit Test Tipo Componente";
        return entidad;     
    }
    
    public static Componentes? Componentes(TiposComponentes? tipoComponente)
    {
        Componentes? entidad = new Componentes();
        entidad.Nombre = "Unit Test Componente";
        entidad.Tipo = tipoComponente.Id;
        entidad.Marca = "Unit Test Marca";
        entidad.Precio = 100.00m;
        entidad.ConsumoEnergetico = 5;
        entidad.Especificaciones = "Unit Test Especificaciones";
        entidad.Imagen = "Unit Test Imagen";
        entidad.Socket = "Unit Test Socket";
        entidad.TipoRAM = "Unit Test Tipo RAM";
        entidad.Formato = "Unit Test Formato";
        return entidad;
    }
    
    /*public static Compatibilidad? Compatibilidad(Componentes? componente)
    {
        Compatibilidad? entidad = new Compatibilidad();
        entidad.ComponenteId = componente.Id;
        entidad.ComponenteCompatibleId = componente.Id;
        return entidad;
    }*/
    
    public static Builds? Builds(Usuarios? usuario)
    {
        Builds? entidad = new Builds();
        entidad.UsuarioId = usuario.Id;
        entidad.Nombre = "Unit Test Build";
        entidad.PrecioTotal = 500.00m;
        entidad.ConsumoEnergeticoTotal = 300;
        entidad.FechaCreacion = DateTime.Now;
        return entidad;
    }
    public static ComponentesEnBuild? ComponentesEnBuild(Builds? build, Componentes? componente)
    {
        ComponentesEnBuild? entidad = new ComponentesEnBuild();
        entidad.BuildId = build.Id;
        entidad.ComponenteId = componente.Id;
        return entidad; 
    }
    
}