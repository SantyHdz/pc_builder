using lib_dominio.Entidades;
using lib_repositorios.Interfaces;

namespace ut_presentacion.Nucleo;

public class EntidadesNucleo
{

    public static Categorias? Categorias()
    {
        var entidad = new Categorias();
        entidad.Nombre = "Disco Duro";
        return entidad;
    }
    public static Componentes? Componentes(Categorias? categorias)
    {
        var entidad = new Componentes();
        entidad.Nombre = "Disco Duro 1TB";
        entidad.Marca = "Disco Duro 1TB";
        entidad.Precio = 150.00M;
        entidad.IdCategoria = categorias.IdCategoria;
        entidad.Especificaciones = "Disco duro de 1TB, 7200RPM, SATA III";
        return entidad;
    }

    public static Builds? Builds(Usuarios? usuarios)
    {
        var entidad = new Builds();
        entidad.Nombre = "Build Gamer";
        entidad.IdUsuario = usuarios.Id;
        entidad.FechaCreacion = new DateTime(2022, 5, 20);
        return entidad;
    }
    public static BuildComponentes? BuildComponentes(Builds? builds, Componentes? componentes)
    {
        var entidad = new BuildComponentes();
        entidad.IdBuild = builds.IdBuild;
        entidad.IdComponente = componentes.IdComponente;
        return entidad;
    }
}