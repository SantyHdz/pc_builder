/*using lib_dominio.Entidades;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using ut_presentacion.Nucleo;
namespace ut_presentacion.Repositorios;
    
[TestClass]
public class CompatibilidadPruebas
{
    private readonly IConexion? iConexion;
    private List<Compatibilidad>? lista;
    private Compatibilidad? entidad;
    public CompatibilidadPruebas()
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
        this.lista = this.iConexion!.Compatibilidad!.ToList();
        return lista.Count > 0;
    }
    public bool Guardar()
    {
        var componentes = this.iConexion!.Componentes!.FirstOrDefault(x => x.Id == 1);
        this.entidad = EntidadesNucleo.Compatibilidad(componentes)!;
        this.iConexion!.Compatibilidad!.Add(this.entidad);
        this.iConexion!.SaveChanges();
        return true;
    }
    /*public bool Modificar()
    {
        this.entidad!.Nombre = "Try unit test"; 
        var entry = this.iConexion!.Entry<Compatibilidad>(this.entidad);
        entry.State = EntityState.Modified;
        this.iConexion!.SaveChanges();
        return true;
    }*/
    /*public bool Borrar()
    {
        this.iConexion!.Compatibilidad!.Remove(this.entidad!);
        this.iConexion!.SaveChanges();
        return true;
    }
}*/