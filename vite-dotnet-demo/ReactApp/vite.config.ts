import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  build: {
    outDir: "../wwwroot",
    emptyOutDir: false,
    rollupOptions: {
      input: "src/main.tsx",
    },
    manifest: "ReactApp/manifest.json",
  },
  server: {
    hmr: {
      protocol: "ws",
    },
    origin: "http://localhost:5173",
  },
});
