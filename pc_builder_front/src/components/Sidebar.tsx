import { NavLink } from "react-router-dom";

export default function Sidebar() {
  return (
    <aside className="w-64 bg-hibox-darker text-white flex flex-col justify-between min-h-screen p-4">
      <div>
        <h1 className="text-2xl font-bold mb-6">PC Builder</h1>
        <nav className="flex flex-col gap-2">
          <NavLink
            to="/"
            className={({ isActive }) =>
              `px-3 py-2 rounded-md ${isActive ? "bg-hibox-accent text-white" : "hover:bg-gray-800"}`
            }
          >
            Home
          </NavLink>
          <NavLink
            to="/builder"
            className={({ isActive }) =>
              `px-3 py-2 rounded-md ${isActive ? "bg-hibox-accent text-white" : "hover:bg-gray-800"}`
            }
          >
            Builder
          </NavLink>
          <NavLink
            to="/components"
            className={({ isActive }) =>
              `px-3 py-2 rounded-md ${isActive ? "bg-hibox-accent text-white" : "hover:bg-gray-800"}`
            }
          >
            Components
          </NavLink>
        </nav>
      </div>

      <div>
        <button className="mt-6 flex items-center gap-2 bg-hibox-accent px-4 py-2 rounded-lg hover:opacity-90 w-full">
          Create Build
        </button>
      </div>
    </aside>
  );
}