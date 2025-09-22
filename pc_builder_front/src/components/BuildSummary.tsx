import React from "react";

interface Props {
  items: { comp: any; cantidad?: number }[];
  onSave: (payload: { Nombre: string; UsuarioId?: number; PrecioTotal: number; ConsumoEnergeticoTotal: number }) => void;
}

export default function BuildSummary({ items, onSave }: Props) {
  const totalPrice = items.reduce((s, i) => s + (i.comp.Precio || 0) * (i.cantidad || 1), 0);
  const totalWatts = items.reduce((s, i) => s + (i.comp.ConsumoEnergetico || 0) * (i.cantidad || 1), 0);

  return (
    <aside className="bg-white/5 p-4 w-72 rounded-xl">
      <h2 className="text-lg font-bold mb-2">Resumen de la Build</h2>
      <ul className="space-y-2 max-h-80 overflow-auto mb-4">
        {items.map((it, idx) => (
          <li key={idx} className="flex justify-between">
            <span>{it.comp.Nombre}</span>
            <span>${(it.comp.Precio || 0).toLocaleString()}</span>
          </li>
        ))}
      </ul>

      <div className="border-t pt-3">
        <p className="flex justify-between font-semibold">
          <span>Total:</span>
          <span>${totalPrice.toLocaleString()}</span>
        </p>
        <p className="flex justify-between text-sm text-gray-300 mt-1">
          <span>Consumo:</span>
          <span>{totalWatts} W</span>
        </p>

        <button
          onClick={() => onSave({ Nombre: "Mi Build", PrecioTotal: totalPrice, ConsumoEnergeticoTotal: totalWatts })}
          className="mt-4 w-full bg-hibox-accent text-white py-2 rounded"
        >
          Guardar Build
        </button>
      </div>
    </aside>
  );
}
