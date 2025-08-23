using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;
namespace ut_presentacion.Repositorios;
    
[TestClass]
public class BuildsComponentesPruebas
{
    private readonly IConexion? iConexion;
    private List<BuildComponentes>? lista;
    private BuildComponentes? entidad;
    public BuildsComponentesPruebas()
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
        this.lista = this.iConexion!.BuildComponentes!.ToList();
        return lista.Count > 0;
    }
    public bool Guardar()
    {
        var builds = this.iConexion!.Builds!.FirstOrDefault(x => x.IdBuild == 1);
        var componentes = this.iConexion!.Componentes!.FirstOrDefault(x => x.IdComponente == 1);
        this.entidad = EntidadesNucleo.BuildComponentes(builds, componentes)!;
        this.iConexion!.BuildComponentes!.Add(this.entidad);
        this.iConexion!.SaveChanges();
        return true;
    }
    /*public bool Modificar()
    {
        this.entidad!.Nombre = "Try unit test"; 
        var entry = this.iConexion!.Entry<BuildComponentes>(this.entidad);
        entry.State = EntityState.Modified;
        this.iConexion!.SaveChanges();
        return true;
    }*/
    public bool Borrar()
    {
        this.iConexion!.BuildComponentes!.Remove(this.entidad!);
        this.iConexion!.SaveChanges();
        return true;
    }
}