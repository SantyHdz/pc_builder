import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import Home from "./pages/Home";
import Builder from "./pages/Builder";
import ComponentsPage from "./pages/ComponentsPage";
import Login from "./pages/Login";
import NotFound from "./pages/NotFound";

export default function App() {
  return (
    <Routes>
      <Route path="/login" element={<Login />} />
      <Route path="/" element={<Navigate to="/login" replace />} />
      <Route path="/builder" element={<Builder />} />
      <Route path="/components" element={<ComponentsPage />} />
      <Route path="*" element={<NotFound />} />
    </Routes>
  );
}
