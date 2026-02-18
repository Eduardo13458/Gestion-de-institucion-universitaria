# 🎓 Sistema Universitario - Gestión de Archivos

## Implementación Completa de los 3 Tipos de Organización de Archivos

Este proyecto demuestra de manera práctica y educativa la implementación de:

### ✅ **Archivo DIRECTO (Hash)** - Control de Acceso
- Función hash para acceso O(1)
- Validación de credenciales en tiempo real
- Ideal para: Entrada del campus, biblioteca, control de acceso

### ✅ **Archivo INDEXADO (ISAM)** - Sistema de Calificaciones  
- Índice para búsquedas rápidas
- Lectura secuencial para reportes
- Ideal para: Kárdex, consulta de notas, actas finales

### ✅ **Archivo SECUENCIAL** - Logs de Transacciones
- Escritura cronológica (append)
- Procesamiento por lotes
- Ideal para: Auditorías, cortes de caja, logs del sistema

---

## 🚀 Inicio Rápido

1. **Abrir el proyecto** en Visual Studio 2022
2. **Compilar** (Ctrl + Shift + B)
3. **Ejecutar** (F5)

El sistema cargará automáticamente datos de ejemplo para que puedas probar cada funcionalidad.

---

## 📁 Estructura de Archivos Generados

Al ejecutar el sistema, se crearán automáticamente:

```
📂 bin/Debug/net8.0-windows/
├── 📄 estudiantes.dat          (2.56 MB - Archivo DIRECTO)
├── 📄 calificaciones.dat       (Variable - Archivo de datos)
├── 📄 calificaciones.idx       (Variable - Archivo de índice)
└── 📄 transacciones.log        (Variable - Archivo SECUENCIAL)
```

---

## 🎯 Características Principales

### Tab 1: Archivo DIRECTO - Control de Acceso
```
🔹 Guardar estudiantes con función hash
🔹 Validar acceso instantáneo (< 5ms)
🔹 Ver matrículas registradas (lista completa)
🔹 Ver estadísticas del sistema
🔹 Capacidad: 10,000 estudiantes
```

### Tab 2: Archivo INDEXADO - Calificaciones
```
🔹 Registrar calificaciones
🔹 Buscar por matrícula (con índice)
🔹 Leer todas secuencialmente
🔹 Calcular promedios automáticamente
```

### Tab 3: Archivo SECUENCIAL - Transacciones
```
🔹 Registrar transacciones del día
🔹 Generar corte de caja diario
🔹 Estadísticas completas (procesamiento batch)
🔹 Visualizar logs cronológicos
```

---

## 💡 Ejemplos de Uso

### Ejemplo 1: Validar entrada al campus
1. Ir al **Tab "Archivo DIRECTO"**
2. Ingresar matrícula: `20240001`
3. Clic en **"🔍 Validar Acceso (Hash)"**
4. Resultado en < 3ms: ✅ ACCESO CONCEDIDO o ⛔ ACCESO DENEGADO

### Ejemplo 2: Consultar calificaciones
1. Ir al **Tab "Archivo INDEXADO"**
2. Ingresar matrícula: `20240001`
3. Clic en **"🔎 Buscar por Índice"**
4. Ver kárdex completo con promedio

### Ejemplo 3: Registrar pago
1. Ir al **Tab "Archivo SECUENCIAL"**
2. Seleccionar tipo: `Pago Colegiatura`
3. Ingresar datos y monto
4. Clic en **"Registrar Transacción"**
5. La transacción se agrega al log cronológico

---

## 📊 Análisis de Complejidad

| Operación | Directo | Indexado | Secuencial |
|-----------|---------|----------|------------|
| Búsqueda | **O(1)** ⚡ | O(log n) | O(n) |
| Inserción | O(1) | O(log n) | **O(1)** ⚡ |
| Lectura completa | O(n) | **O(n)** | **O(n)** |
| Ordenamiento | ❌ | ✅ | ✅ (cronológico) |

---

## 🎓 Conceptos Demostrados

- ✅ Función hash y manejo de colisiones
- ✅ Índices y búsqueda binaria
- ✅ Acceso directo vs secuencial
- ✅ Trade-offs: velocidad vs espacio vs ordenamiento
- ✅ Serialización de objetos
- ✅ File I/O en .NET
- ✅ Windows Forms

---

## 📚 Documentación Adicional

- **README.md** - Explicación detallada de cada tipo de archivo
- **GUIA_DE_PRUEBAS.md** - Casos de prueba y ejemplos prácticos

---

## 🛠️ Tecnologías

- **.NET 8**
- **C# 12**
- **Windows Forms**
- **Binary File I/O**
- **FileStream, StreamReader/Writer**

---

## ⚡ Performance

El sistema está optimizado para:
- Validar acceso en **< 5ms** (archivo directo)
- Buscar calificaciones en **< 20ms** (archivo indexado)
- Procesar 1000+ transacciones en **< 200ms** (archivo secuencial)

---

## 📖 Referencias Educativas

Este proyecto implementa conceptos de:
- Estructuras de Archivos (Folk, Zoellick)
- Organización de Archivos y Bases de Datos
- Algoritmos de Hash
- ISAM (Indexed Sequential Access Method)

---

**Desarrollado con fines educativos para demostrar implementaciones prácticas de estructuras de archivos en sistemas reales.**

🚀 ¡Explora, prueba y aprende! 🎓
