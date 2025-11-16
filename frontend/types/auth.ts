//ENUMS
export enum TipoUsuario {
  FUNCIONARIO,
  CLIENTE
};

export enum Cargo {
  ESTAGIARIO,
  ATENDENTE,
  GERENTE
};

//INTERFACES
export interface LoginRequest {
  cpf: string;
  senha: string;
};

export interface RegisterRequest {
  nome: string,
  cpf: string,
  dataNascimento: Date,
  telefone: string,
  tipoUsuario: TipoUsuario,
  senha: string
};

export interface ClienteRegister extends RegisterRequest {

};

export interface FuncionarioRegister extends RegisterRequest {
  codigoAgencia: string,
  cargo: Cargo,
  codigoSupervisor: string
};

//TYPES

export type LoginUsuarioResponse = {
  id: number;
  nome: string;
  cpf: string;
  dataNascimento: string;
  telefone: string;
  tipoUsuario: TipoUsuario;
};

export type LoginClienteResponse = {
  id: number;
  success: boolean;
  message: string;
  scoreCredito: number;
  usuario: LoginUsuarioResponse;
};

export type LoginFuncionarioResponse = {
  id: number;
  success: boolean;
  message: string;
  agenciaId: number;
  codigoFuncionario: string;
  cargo: number;
  supervisorId: number;
  usuario: LoginUsuarioResponse;
};

export interface LoginResponse {
  success: boolean;
  message: string;
  token: string | null;
  tipoUsuario: TipoUsuario | null;
  data: LoginClienteResponse | LoginFuncionarioResponse | null;
}