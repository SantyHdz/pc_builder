import api from "../services/api";

export interface Rol {
  Id?: number;
  Nombre?: string;
}

export interface Usuario {
  Id?: number;
  Nombre?: string;
  Correo?: string;
  ContrasenaHash?: string;
  Direccion?: string;
  RolId?: number;
  _RolId?: Rol; // Relación
}

export interface ApiRespuesta<T> {
  Entidad?: T;
  Entidades?: T[];
  Respuesta?: string;
  Error?: string;
  Fecha?: string;
}

// Listar todos los usuarios
export async function listarUsuarios(): Promise<ApiRespuesta<Usuario>> {
  const res = await api.post("/Usuarios/Listar", {});
  return res.data;
}

// Buscar usuario por correo
export async function usuariosPorCorreo(correo: string): Promise<ApiRespuesta<Usuario>> {
  const res = await api.post("/Usuarios/PorCorreo", {
    Entidad: { Correo: correo },
  });
  return res.data;
}

// Guardar (insertar nuevo)
export async function guardarUsuario(usuario: Usuario): Promise<ApiRespuesta<Usuario>> {
  const res = await api.post("/Usuarios/Guardar", { Entidad: usuario });
  return res.data;
}

// Modificar existente
export async function modificarUsuario(usuario: Usuario): Promise<ApiRespuesta<Usuario>> {
  const res = await api.post("/Usuarios/Modificar", { Entidad: usuario });
  return res.data;
}

// Borrar usuario
export async function borrarUsuario(usuario: Usuario): Promise<ApiRespuesta<Usuario>> {
  const res = await api.post("/Usuarios/Borrar", { Entidad: usuario });
  return res.data;
}