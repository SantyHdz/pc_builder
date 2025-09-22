import React, { useEffect, useState } from "react";
import Sidebar from "../components/Sidebar";
import Topbar from "../components/Topbar";
import ComponentCard from "../components/ComponentCard";
import BuildSummary from "../components/BuildSummary";
import EmptyState from "../components/EmptyState";
import { listarComponentes, obtenerCompatibles } from "../services/componentes";
import { listarTiposComponentes } from "../services/tiposComponentes";
import { guardarBuild } from "../services/builds";

export default function Builder() {
  const [available, setAvailable] = useState<any[]>([]);
  const [filtered, setFiltered] = useState<any[]>([]);
  const [selectedTipo, setSelectedTipo] = useState<number | null>(null);
  const [items, setItems] = useState<{ comp: any; cantidad?: number }[]>([]);
  const [tipos, setTipos] = useState<any[]>([]);

  useEffect(() => {
    async function load() {
      try {
        const compsRes = await listarComponentes();
        setAvailable(compsRes?.Entidades || []);
        setFiltered(compsRes?.Entidades || []);
        const tiposRes = await listarTiposComponentes();
        setTipos(tiposRes?.Entidades || []);
      } catch (err) {
        console.error("Error cargando componentes", err);
      }
    }
    load();
  }, []);

  useEffect(() => {
    if (selectedTipo == null) {
      setFiltered(available);
    } else {
      setFiltered(available.filter((c) => c.Tipo === selectedTipo));
    }
  }, [selectedTipo, available]);

  // agregar componente (valida compatibilidad básica: si hay otro componente seleccionado
  // y existe compatibilidad registrada, podemos pedir compatibilidad al backend)
  const addComponent = async (comp: any) => {
    // ejemplo de validación: pedir al backend compatibles de este componente
    try {
      const compats = await obtenerCompatibles(comp.Id);
      // compats.Entidades es lista de componentes compatibles con comp
      // lógica simple: si ya hay seleccionados que no estén en la lista, mostrar alerta
      // (omito reglas complejas — las adaptas según tu modelo)
      setItems((prev) => [...prev, { comp, cantidad: 1 }]);
    } catch (err) {
      console.error("Error validando compatibilidad", err);
      // aun así agrego
      setItems((prev) => [...prev, { comp, cantidad: 1 }]);
    }
  };

  const saveBuild = async (payload: { Nombre: string; PrecioTotal: number; ConsumoEnergeticoTotal: number; UsuarioId?: number }) => {
    try {
      // arma el objeto Build para enviar al backend
      const build = {
        UsuarioId: payload.UsuarioId || 1, // por ahora usuario 1 (ajusta según auth)
        Nombre: payload.Nombre,
        PrecioTotal: payload.PrecioTotal,
        ConsumoEnergeticoTotal: payload.ConsumoEnergeticoTotal,
      };
      const res = await guardarBuild(build);
      if (res?.Respuesta === "OK") {
        alert("Build guardada correctamente");
      } else {
        alert("Respuesta del servidor: " + JSON.stringify(res));
      }
    } catch (err) {
      console.error("Error guardando build", err);
      alert("Error guardando build");
    }
  };

  return (
    <div className="flex min-h-screen">
      <Sidebar />
      <div className="flex-1 flex flex-col">
        <Topbar />
        <main className="p-6 flex gap-6">
          {/* Left: filtros + lista */}
          <div className="w-1/3 space-y-4">
            <div className="card">
              <h3 className="font-bold mb-2">Filtros</h3>
              <select value={selectedTipo ?? ""} onChange={(e) => setSelectedTipo(e.target.value ? Number(e.target.value) : null)} className="w-full p-2 rounded">
                <option value="">Todos los tipos</option>
                {tipos.map((t: any) => (
                  <option key={t.Id} value={t.Id}>
                    {t.Nombre}
                  </option>
                ))}
              </select>
            </div>

            <div className="space-y-3 max-h-[60vh] overflow-auto">
              {filtered.map((c) => (
                <ComponentCard key={c.Id} comp={c} onAdd={addComponent} />
              ))}
            </div>
          </div>

          {/* Center: canvas */}
          <div className="flex-1 bg-white/5 rounded-xl p-6 flex items-center justify-center">
            {items.length === 0 ? (
              <EmptyState message="Tu canvas está vacío. Agrega componentes desde la izquierda." />
            ) : (
              <div>
                <h3 className="text-xl font-semibold mb-4">Preview</h3>
                {/* aquí podrías mostrar un layout visual del ensamblaje */}
                <ul className="space-y-2">
                  {items.map((it, idx) => (
                    <li key={idx} className="flex justify-between">
                      <span>{it.comp.Nombre}</span>
                      <span>${(it.comp.Precio || 0).toLocaleString()}</span>
                    </li>
                  ))}
                </ul>
              </div>
            )}
          </div>

          {/* Right: resumen */}
          <BuildSummary
            items={items}
            onSave={({ Nombre, PrecioTotal, ConsumoEnergeticoTotal }) =>
              saveBuild({ Nombre, PrecioTotal, ConsumoEnergeticoTotal, UsuarioId: 1 })
            }
          />
        </main>
      </div>
    </div>
  );
}