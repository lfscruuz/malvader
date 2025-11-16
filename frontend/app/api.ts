const API_URL = process.env.NEXT_PUBLIC_API_URL;

export async function apiFetch(endpoint: string, options: RequestInit = {}) {
  const res = await fetch(`${API_URL}/${endpoint}`, {
    headers: { "Content-Type": "application/json" },
    ...options,
  });
  const data = await res.json();
  return data;
}