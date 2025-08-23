using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;
namespace ut_presentacion.Repositorios;
    
[TestClass]
public class CategoriasPruebas
{
    private readonly IConexion? iConexion;
    private List<Categorias>? lista;
    private Categorias? entidad;
    public CategoriasPruebas()
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
        this.lista = this.iConexion!.Categorias!.ToList();
        return lista.Count > 0;
    }
    public bool Guardar()
    {
        this.entidad = EntidadesNucleo.Categorias()!;
        this.iConexion!.Categorias!.Add(this.entidad);
        this.iConexion!.SaveChanges();
        return true;
    }
    public bool Modificar()
    {
        this.entidad!.Nombre = "Try unit test"; 
        var entry = this.iConexion!.Entry<Categorias>(this.entidad);
        entry.State = EntityState.Modified;
        this.iConexion!.SaveChanges();
        return true;
    }
    public bool Borrar()
    {
        this.iConexion!.Categorias!.Remove(this.entidad!);
        this.iConexion!.SaveChanges();
        return true;
    }
}