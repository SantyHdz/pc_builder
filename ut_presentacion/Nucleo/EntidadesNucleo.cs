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
}