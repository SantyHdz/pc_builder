import React from "react";

export default function Topbar() {
  return (
    <header className="flex items-center justify-between bg-white shadow p-4">
      <div className="flex items-center gap-2 w-1/2">
        <input
          type="text"
          placeholder="Buscar componentes..."
          className="w-full border rounded px-3 py-1 focus:outline-none"
        />
      </div>

      <div className="flex items-center gap-4">
        <div className="text-sm text-gray-600">Hola, Invitado</div>
        <img src="https://i.pravatar.cc/40" alt="User" className="w-8 h-8 rounded-full" />
      </div>
    </header>
  );
}
