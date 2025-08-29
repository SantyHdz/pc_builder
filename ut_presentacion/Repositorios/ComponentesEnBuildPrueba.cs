using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;
namespace ut_presentacion.Repositorios;
    
[TestClass]
public class ComponentesEnBuildPruebas
{
    private readonly IConexion? iConexion;
    private List<ComponentesEnBuild>? lista;
    private ComponentesEnBuild? entidad;
    public ComponentesEnBuildPruebas()
    {
        iConexion = new ConexionEF3.Conexion();
        iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
    }
    [TestMethod]
    public void Ejecutar()
    {
        Assert.AreEqual(true, Guardar());
        //Assert.AreEqual(true, Modificar());
        Assert.AreEqual(true, Listar());
        Assert.AreEqual(true, Borrar());
    }
    public bool Listar()
    {
        this.lista = this.iConexion!.ComponentesEnBuild!.ToList();
        return lista.Count > 0;
    }
    public bool Guardar()
    {
        var builds = this.iConexion!.Builds!.FirstOrDefault(x => x.Id == 1);
        var componentes = this.iConexion!.Componentes!.FirstOrDefault(x => x.Id == 1);
        this.entidad = EntidadesNucleo.ComponentesEnBuild(builds, componentes)!;
        this.iConexion!.ComponentesEnBuild!.Add(this.entidad);
        this.iConexion!.SaveChanges();
        return true;
    }
    /*public bool Modificar()
    {
        this.entidad!.Nombre = "Try unit test";
        var entry = this.iConexion!.Entry<ComponentesEnBuild>(this.entidad);
        entry.State = EntityState.Modified;
        this.iConexion!.SaveChanges();
        return true;
    }*/
    public bool Borrar()
    {
        this.iConexion!.ComponentesEnBuild!.Remove(this.entidad!);
        this.iConexion!.SaveChanges();
        return true;
    }
}