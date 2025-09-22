export interface Usuario {
  Id?: number;
  Nombre?: string;
  Correo?: string;
  Direccion?: string;
  RolId?: number;
}

export interface TipoComponente {
  Id?: number;
  Nombre?: string;
}

export interface ComponenteModel {
  Id?: number;
  Nombre?: string;
  Tipo?: number;
  Marca?: string;
  Precio?: number;
  ConsumoEnergetico?: number;
  Especificaciones?: string;
  Imagen?: string;
  Socket?: string;
  TipoRAM?: string;
  Formato?: string;
}

export interface BuildModel {
  Id?: number;
  UsuarioId?: number;
  Nombre?: string;
  PrecioTotal?: number;
  ConsumoEnergeticoTotal?: number;
  FechaCreacion?: string;
}
