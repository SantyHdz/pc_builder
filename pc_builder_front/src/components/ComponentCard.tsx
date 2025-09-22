import React from "react";

interface Props {
  comp: {
    Id?: number;
    Nombre?: string;
    Imagen?: string;
    Precio?: number;
    ConsumoEnergetico?: number;
    Especificaciones?: string;
  };
  onAdd: (comp: any) => void;
}

export default function ComponentCard({ comp, onAdd }: Props) {
  return (
    <div className="card flex flex-col items-center text-center">
      <img src={comp.Imagen || "/placeholder.png"} alt={comp.Nombre} className="h-24 mb-2 object-contain" />
      <h3 className="font-semibold">{comp.Nombre}</h3>
      <p className="text-sm text-gray-300">{comp.Especificaciones}</p>
      <p className="text-lg font-bold mt-2">${comp.Precio?.toLocaleString()}</p>
      <button
        onClick={() => onAdd(comp)}
        className="mt-3 bg-hibox-accent text-white px-4 py-2 rounded hover:opacity-90"
      >
        Agregar
      </button>
    </div>
  );
}
