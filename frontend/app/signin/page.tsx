'use client';
import { useState } from 'react';
import { apiFetch } from '../api';
import {
  TipoUsuario,
  Cargo,
  RegisterRequest,
  ClienteRegister,
  FuncionarioRegister,
} from '@/types/auth';
import Link from 'next/link';

export default function Signin() {
  const [tipoUsuario, setTipoUsuario] = useState<TipoUsuario>(TipoUsuario.CLIENTE);
  const [nome, setNome] = useState("");
  const [cpf, setCpf] = useState("");
  const [dataNascimento, setDataNascimento] = useState("");
  const [telefone, setTelefone] = useState("");
  const [senha, setSenha] = useState("");
  const [confirmSenha, setConfirmSenha] = useState("");

  // cliente
  const [scoreCredito, setScoreCredito] = useState(0);

  // funcionario
  const [codigoAgencia, setCodigoAgencia] = useState("");
  const [cargo, setCargo] = useState<Cargo>(Cargo.ESTAGIARIO);
  const [codigoSupervisor, setCodigoSupervisor] = useState("");

  const [message, setMessage] = useState("");

  const endpointMap = {
    [TipoUsuario.CLIENTE]: "usuario/cliente",
    [TipoUsuario.FUNCIONARIO]: "usuario/funcionario",
  };

  const handleRegister = async (e: React.FormEvent) => {
    e.preventDefault();

    if (senha !== confirmSenha) {
      setMessage("As senhas não coincidem.");
      return;
    }

    try {
      let payload: RegisterRequest;

      if (tipoUsuario === TipoUsuario.CLIENTE) {
        payload = {
          nome,
          cpf,
          dataNascimento: new Date(dataNascimento),
          telefone,
          tipoUsuario,
          senha,
          scoreCredito,
        } as ClienteRegister;
      } else {
        payload = {
          nome,
          cpf,
          dataNascimento: new Date(dataNascimento),
          telefone,
          tipoUsuario,
          senha,
          cargo,
          ...(codigoAgencia && { codigoAgencia }),
          ...(codigoSupervisor && { codigoSupervisor })
        } as FuncionarioRegister;
      }

      const endpoint = endpointMap[tipoUsuario];

      const data = await apiFetch(endpoint, {
        method: "POST",
        body: JSON.stringify(payload),
      });

      if (data.success) {
        setMessage("Registrado com sucesso!");
      } else {
        setMessage(data.message);
      }
    } catch (err) {
      setMessage("Um erro ocorreu. Por favor, tente novamente.");
      console.error(err);
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100">
      <div className="bg-white p-8 rounded-lg shadow-lg w-full max-w-md">
        <h1 className="text-2xl font-bold mb-6 text-black text-center">Registrar Conta</h1>

        {message && (
          <div className="mb-4 text-red-600 font-medium text-center">{message}</div>
        )}

        <form onSubmit={handleRegister} className="space-y-4">
          {/* Tipo de usuário */}
          <div>
            <label className="block mb-1 font-medium text-black">Tipo de usuário</label>
            <select
              value={tipoUsuario}
              onChange={(e) => setTipoUsuario(Number(e.target.value))}
              className="w-full border border-gray-300 rounded px-3 py-2 focus:outline-none focus:ring focus:border-blue-500"
            >
              <option value={TipoUsuario.CLIENTE}>Cliente</option>
              <option value={TipoUsuario.FUNCIONARIO}>Funcionario</option>
            </select>
          </div>

          {/* Common fields */}
          <div>
            <label className="block mb-1 font-medium text-black">Nome</label>
            <input
              type="text"
              value={nome}
              onChange={(e) => setNome(e.target.value)}
              required
              className="w-full border border-gray-300 rounded px-3 py-2 focus:outline-none focus:ring focus:border-blue-500"
            />
          </div>

          <div>
            <label className="block mb-1 font-medium text-black">CPF</label>
            <input
              type="text"
              value={cpf}
              onChange={(e) => setCpf(e.target.value)}
              required
              className="w-full border border-gray-300 rounded px-3 py-2 focus:outline-none focus:ring focus:border-blue-500"
            />
          </div>

          <div>
            <label className="block mb-1 font-medium text-black">Data de Nascimento</label>
            <input
              type="date"
              value={dataNascimento}
              onChange={(e) => setDataNascimento(e.target.value)}
              required
              className="w-full border border-gray-300 rounded px-3 py-2 focus:outline-none focus:ring focus:border-blue-500"
            />
          </div>

          <div>
            <label className="block mb-1 font-medium text-black">Telefone</label>
            <input
              type="text"
              value={telefone}
              onChange={(e) => setTelefone(e.target.value)}
              required
              className="w-full border border-gray-300 rounded px-3 py-2 focus:outline-none focus:ring focus:border-blue-500"
            />
          </div>

          <div>
            <label className="block mb-1 font-medium text-black">Senha</label>
            <input
              type="password"
              value={senha}
              onChange={(e) => setSenha(e.target.value)}
              required
              className="w-full border border-gray-300 rounded px-3 py-2 focus:outline-none focus:ring focus:border-blue-500"
            />
          </div>

          <div>
            <label className="block mb-1 font-medium text-black">Confirmar Senha</label>
            <input
              type="password"
              value={confirmSenha}
              onChange={(e) => setConfirmSenha(e.target.value)}
              required
              className="w-full border border-gray-300 rounded px-3 py-2 focus:outline-none focus:ring focus:border-blue-500"
            />
          </div>

          {/* Conditional fields */}
          {tipoUsuario === TipoUsuario.CLIENTE && (
            <div>
              <label className="block mb-1 font-medium text-black">Score de Crédito</label>
              <input
                type="number"
                value={scoreCredito}
                onChange={(e) => setScoreCredito(Number(e.target.value))}
                required
                className="w-full border border-gray-300 rounded px-3 py-2 focus:outline-none focus:ring focus:border-blue-500"
              />
            </div>
          )}

          {tipoUsuario === TipoUsuario.FUNCIONARIO && (
            <>
              <div>
                <label className="block mb-1 font-medium text-black">Agência ID</label>
                <input
                  type="number"
                  value={codigoAgencia}
                  onChange={(e) => setCodigoAgencia(e.target.value)}
                  required
                  className="w-full border border-gray-300 rounded px-3 py-2 focus:outline-none focus:ring focus:border-blue-500"
                />
              </div>

              <div>
                <label className="block mb-1 font-medium text-black">Cargo</label>
                <select
                  value={cargo}
                  onChange={(e) => setCargo(Number(e.target.value))}
                  required
                  className="w-full border border-gray-300 rounded px-3 py-2 focus:outline-none focus:ring focus:border-blue-500"
                >
                  <option value={Cargo.ESTAGIARIO}>Estagiário</option>
                  <option value={Cargo.ATENDENTE}>Atendente</option>
                  <option value={Cargo.GERENTE}>Gerente</option>
                </select>
              </div>

              <div>
                <label className="block mb-1 font-medium text-black">Supervisor ID</label>
                <input
                  type="number"
                  value={codigoSupervisor}
                  onChange={(e) => setCodigoSupervisor(e.target.value)}
                  required
                  className="w-full border border-gray-300 rounded px-3 py-2 focus:outline-none focus:ring focus:border-blue-500"
                />
              </div>
            </>
          )}

          <button
            type="submit"
            className="w-full bg-green-600 text-white py-2 rounded hover:bg-green-700 transition"
          >
            Registrar
          </button>
        </form>

        <div className="mt-4 text-center">
          <Link href="/">
            <button className="text-blue-600 hover:underline">Voltar ao Login</button>
          </Link>
        </div>
      </div>
    </div>
  );
}
