![CFGS DAM](https://img.shields.io/badge/CFGS-Desenvolupament%20d%27Aplicacions%20Multiplataforma%20%28DAM%29-green?style=for-the-badge)
![DI](https://img.shields.io/badge/assignatura-Disseny%20d%27Interf%C3%ADcies-yellow?style=for-the-badge)


# 🐉 Dungeons & Dragons Character Editor (WPF)

![.NET](https://img.shields.io/badge/.NET-8.0-purple?style=for-the-badge)
![WPF](https://img.shields.io/badge/Framework-WPF-blue?style=for-the-badge)
![Language](https://img.shields.io/badge/Language-C%23-green?style=for-the-badge)

Pràctica 1 de la asignatura de Disseny d'Interficies del CFGS de Desenvolupament d'Aplicacions Multiplataforma.

Una aplicación de escritorio desarrollada en **WPF (Windows Presentation Foundation)** para la gestión, creación y edición de personajes de Dungeons & Dragons.

Este proyecto demuestra el uso de patrones de diseño modernos en WPF, enlace de datos (Data Binding), estilos personalizados y validación de formularios.

## ✨ Características Principales

* **Gestión de Personajes:**
    * Visualización de lista de personajes con tarjetas personalizadas.
    * Filtrado visual de "Mi Equipo" vs "Mis Personajes".
    * Creación, Edición y Eliminado de personajes.
* **Editor Completo:**
    * **Clase y Especie:** Selección dinámica basada en archivos JSON.
    * **Habilidades (Stats):** Sistema de "Point Buy" con validación (máximo 27 puntos).
    * **Cálculos Automáticos:** Los modificadores y puntuaciones finales se calculan automáticamente sumando los bonus raciales.
    * **Info Personal:** Validación de datos (Edad, Peso, Altura) y carga de imágenes.
* **Interfaz de Usuario (UI/UX):**
    * Estilo visual temático de fantasía (Fuentes, colores pergamino, bordes).
    * **Validación Reactiva:** Burbujas de error (ToolTips) personalizadas en rojo que guían al usuario.
    * **UserControls Personalizados:** `NumericUpDown` propio, `CharacterView`, etc.
* **Técnico:**
    * Implementación de `INotifyPropertyChanged` (usando Fody).
    * Navegación mediante ventanas modales.
    * Gestión de recursos locales e imágenes.


 ## 🛠️ Tecnologías Utilizadas
* **C# / .NET 8.0**: Lógica del backend.
* **WPF (XAML)**: Interfaz de usuario.
* **PropertyChanged.Fody**: Para la inyección automática de notificaciones de cambio en el modelo MVVM.
* **DependencyPropertyGenerator**: Para la creación limpia de propiedades de dependencia en UserControls.
* **JSON**: Almacenamiento de datos estáticos (Clases, Razas, Alineamientos).
