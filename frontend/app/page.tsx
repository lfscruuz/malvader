"use client";
import Link from 'next/link';
import { useState } from "react";

export default function Home() {
  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100 p-8">
      <div className="bg-white p-10 rounded-2xl shadow-xl w-full max-w-lg text-center">
        <h1 className="text-4xl font-extrabold mb-8 text-black">
          Banco Malvader
        </h1>


        <div className="flex flex-col space-y-4">
          <Link
            href="/login"
            >
            
            <button
            className="w-full bg-blue-600 text-white py-3 rounded-xl hover:bg-blue-700 transition font-medium shadow">
              Fazer Login
            </button>
          </Link>

          <Link
          href="/signin">
            <button
            className="w-full bg-gray-200 text-black py-3 rounded-xl hover:bg-gray-300 transition font-medium shadow">
              Cadastrar
            </button>
          </Link>
        </div>
      </div>
    </div>
  );
}
