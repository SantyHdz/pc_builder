import React, { useEffect, useState } from "react";
import Sidebar from "../components/Sidebar";
import Topbar from "../components/Topbar";
import { listarComponentes, guardarComponente, modificarComponente, borrarComponente } from "../services/componentes";
import EmptyState from "../components/EmptyState";

export default function ComponentsPage() {
  const [components, setComponents] = useState<any[]>([]);
  const [editing, setEditing] = useState<any | null>(null);
  const [form, setForm] = useState<any>({ Nombre: "", Marca: "", Precio: 0, Tipo: null });

  useEffect(() => {
    load();
  }, []);

  async function load() {
    const res = await listarComponentes();
    setComponents(res?.Entidades || []);
  }

  async function save() {
    if (editing) {
      await modificarComponente({ ...editing, ...form });
    } else {
      await guardarComponente({ ...form });
    }
    setForm({ Nombre: "", Marca: "", Precio: 0, Tipo: null });
    setEditing(null);
    load();
  }

  async function del(c: any) {
    if (!confirm("¿Eliminar componente?")) return;
    await borrarComponente(c);
    load();
  }

  return (
    <div className="flex min-h-screen">
      <Sidebar />
      <div className="flex-1 flex flex-col">
        <Topbar />
        <main className="p-6">
          <h1 className="text-2xl font-bold mb-4">Gestión de Componentes</h1>

          <div className="grid grid-cols-3 gap-6">
            <div className="card">
              <h3 className="font-semibold mb-3">{editing ? "Editar componente" : "Nuevo componente"}</h3>
              <input className="w-full p-2 mb-2 rounded" placeholder="Nombre" value={form.Nombre} onChange={(e) => setForm({ ...form, Nombre: e.target.value })} />
              <input className="w-full p-2 mb-2 rounded" placeholder="Marca" value={form.Marca} onChange={(e) => setForm({ ...form, Marca: e.target.value })} />
              <input type="number" className="w-full p-2 mb-2 rounded" placeholder="Precio" value={form.Precio} onChange={(e) => setForm({ ...form, Precio: Number(e.target.value) })} />
              <button onClick={save} className="btn-hibox w-full">{editing ? "Guardar cambios" : "Crear componente"}</button>
            </div>

            <div className="col-span-2">
              {components.length === 0 ? (
                <EmptyState message="No hay componentes. Agrega uno en el formulario." />
              ) : (
                <div className="space-y-3">
                  {components.map((c) => (
                    <div key={c.Id} className="flex items-center justify-between p-3 bg-white/5 rounded">
                      <div>
                        <div className="font-semibold">{c.Nombre}</div>
                        <div className="text-sm text-gray-400">{c.Marca} • ${c.Precio?.toLocaleString()}</div>
                      </div>
                      <div className="flex gap-2">
                        <button onClick={() => { setEditing(c); setForm({ Nombre: c.Nombre, Marca: c.Marca, Precio: c.Precio }); }} className="px-3 py-1 border rounded">Editar</button>
                        <button onClick={() => del(c)} className="px-3 py-1 border rounded">Eliminar</button>
                      </div>
                    </div>
                  ))}
                </div>
              )}
            </div>
          </div>
        </main>
      </div>
    </div>
  );
}
