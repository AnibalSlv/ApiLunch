# ApiLunch 🚀

**ApiLunch** es una utilidad de escritorio diseñada para centralizar y simplificar el flujo de trabajo de los desarrolladores backend. Su función principal es servir como un lanzador (launcher) ligero que permite ejecutar múltiples APIs locales de forma simultánea desde una interfaz única y moderna.

---

## El Problema
Como desarrolladores, a menudo trabajamos con microservicios o múltiples proyectos de API que requieren abrir varias terminales, navegar por directorios y ejecutar comandos repetitivos cada vez que iniciamos el entorno de desarrollo.

## La Solución
ApiLunch automatiza este proceso. Con un solo clic, puedes levantar tus servicios, manteniendo tu espacio de trabajo organizado y ahorrando tiempo en tareas repetitivas de configuración.

---

## Stack Tecnológico

El proyecto utiliza una arquitectura híbrida para combinar la potencia del sistema con una experiencia de usuario moderna:

- **Backend:** C# con .NET y WPF (Windows Presentation Foundation).
- **Frontend:** React + TypeScript (alojado mediante Vite).
- **Interoperabilidad:** **WebView2**, permitiendo la comunicación fluida entre la lógica de C# y la interfaz web.
- **Gestión de Procesos:** Uso de `System.Diagnostics` para el control de instancias de consola externas.

---

## Características principales

- **Lanzamiento centralizado:** Ejecuta tus APIs con un solo botón.
- **Interfaz Moderna:** UI construida en React para una experiencia fluida.
- **Arquitectura Desacoplada:** El núcleo del sistema (C#) está separado de la vista (React), facilitando el mantenimiento y la escalabilidad.
- **Ligero:** Consumo mínimo de recursos en comparación con otras herramientas de orquestación más pesadas.

---

## Estructura del Proyecto

El repositorio se organiza de la siguiente manera:

- `ApiLunch_Desktop/`: Aplicación host construida con WPF y C# que gestiona el ciclo de vida de los procesos.
- `apiLunch_web/`: Interfaz de usuario desarrollada en React + Vite + TS.

---

## Configuración y Uso (Próximamente)

*Nota: Actualmente el proyecto se encuentra en desarrollo activo.*

1. Clona el repositorio.
2. Configura las rutas de tus ejecutables en la sección de configuración.
3. Lanza tus APIs
