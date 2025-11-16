'use client';
import { useState, useEffect } from 'react';
import { apiFetch } from '../api';
import { LoginRequest, LoginResponse } from '@/types/auth';
import Link from 'next/link';

export default function Login() {
  const [cpf, setCpf] = useState("");
  const [senha, setSenha] = useState("");
  const [message, setMessage] = useState("");

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      const payload: LoginRequest = { cpf, senha };
      const res: LoginResponse = await apiFetch("auth/login", {
        method: "POST",
        body: JSON.stringify(payload),
      });

      if (!res.success) {
        setMessage(res.message);
        return;
      }
      
      localStorage.setItem("jwt", res.token!);
    } catch (err) {
      setMessage("Um erro ocorreu. Por favor, tente novamente.");
      console.error(err);
    }
  };

  const handleRegister = () => {
    console.log("Redirecting to register...");
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100">
      <div className="bg-white p-8 rounded-lg shadow-lg w-full max-w-md">
        <h1 className="text-2xl font-bold mb-6 text-black text-center">
          Bem vindo de volta!
        </h1>
         {message && (
          <div className="mb-4 text-red-600 font-medium text-center">
            {message}
          </div>
        )}
        <form onSubmit={handleLogin} className="space-y-4">
          <div>
            <label htmlFor="cpf" className="block mb-1 font-medium text-black">
              CPF
            </label>
            <input
              type="text"
              id="cpf"
              value={cpf}
              onChange={(e) => setCpf(e.target.value)}
              required
              className="w-full border border-gray-300 rounded px-3 py-2 focus:outline-none focus:ring focus:border-blue-500"
            />
          </div>
          <div>
            <label htmlFor="senha" className="block mb-1 font-medium text-black">
              Senha
            </label>
            <input
              type="password"
              id="senha"
              value={senha}
              onChange={(e) => setSenha(e.target.value)}
              required
              className="w-full border border-gray-300 rounded-xl px-3 py-2 focus:outline-none focus:ring focus:border-blue-500"
            />
          </div>
          <button
            type="submit"
            className="w-full bg-blue-600 text-white py-2 rounded-xl hover:bg-blue-700 transition"
          >
            Login
          </button>
        </form>
        <div className="mt-4 text-center">
          <Link href="/signin">
            <button onClick={handleRegister} className="text-blue-600 hover:underline">
              Registrar
            </button>
          </Link>
        </div>
      </div>
    </div>
  );
}