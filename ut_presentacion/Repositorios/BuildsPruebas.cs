using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;
namespace ut_presentacion.Repositorios;
    
[TestClass]
public class BuildsPruebas
{
    private readonly IConexion? iConexion;
    private List<Builds>? lista;
    private Builds? entidad;
    public BuildsPruebas()
    {
        iConexion = new ConexionEF3.Conexion();
        iConexion.StringConexion = Configuracion.ObtenerValor("StringConexion");
    }
    [TestMethod]
    public void Ejecutar()
    {
        Assert.AreEqual(true, Guardar());
        Assert.AreEqual(true, Modificar());
        Assert.AreEqual(true, Listar());
        Assert.AreEqual(true, Borrar());
    }
    public bool Listar()
    {
        this.lista = this.iConexion!.Builds!.ToList();
        return lista.Count > 0;
    }
    public bool Guardar()
    {
        var usuarios = this.iConexion!.Usuarios!.FirstOrDefault(x => x.Id == 1);
        this.entidad = EntidadesNucleo.Builds(usuarios)!;
        this.iConexion!.Builds!.Add(this.entidad);
        this.iConexion!.SaveChanges();
        return true;
    }
    public bool Modificar()
    {
        this.entidad!.Nombre = "Try unit test";
        var entry = this.iConexion!.Entry<Builds>(this.entidad);
        entry.State = EntityState.Modified;
        this.iConexion!.SaveChanges();
        return true;
    }
    public bool Borrar()
    {
        this.iConexion!.Builds!.Remove(this.entidad!);
        this.iConexion!.SaveChanges();
        return true;
    }
}