import { Link } from "react-router-dom";

export default function NotFound() {
  return (
    <div className="h-screen flex items-center justify-center flex-col">
      <h1 className="text-6xl font-bold mb-2">404</h1>
      <p className="text-gray-400 mb-4">Página no encontrada</p>
      <Link to="/" className="btn-hibox">Ir al inicio</Link>
    </div>
  );
}
