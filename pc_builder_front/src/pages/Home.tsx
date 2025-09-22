import { Link } from "react-router-dom";
import Sidebar from "../components/Sidebar";
import Topbar from "../components/Topbar";

export default function Home() {
  return (
    <div className="flex min-h-screen">
      <Sidebar />
      <div className="flex-1 flex flex-col">
        <Topbar />
        <main className="p-10">
          <section className="grid grid-cols-2 gap-8 items-center">
            <div>
              <h1 className="text-5xl font-bold mb-4">PC Builder</h1>
              <p className="mb-6 text-gray-300 max-w-xl">
                Arma tu PC, valida compatibilidad, calcula consumo y guarda tus builds. Exporta en PDF o comparte tus configs.
              </p>

              <Link to="/builder" className="btn-hibox">
                Empezar a construir
              </Link>
            </div>
            <div className="flex justify-center">
              <img src="https://illustrations.popsy.co/gray/computer.svg" alt="pc" className="w-96" />
            </div>
          </section>
        </main>
      </div>
    </div>
  );
}
