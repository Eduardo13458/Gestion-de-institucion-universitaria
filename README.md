# Sistema Universitario - Gestión de Archivos
## Proyecto de Estructura de Archivos (Directo, Indexado y Secuencial)

Este proyecto demuestra la implementación de los **3 tipos principales de organización de archivos** aplicados a un sistema universitario real en **.NET 8**.

---

## 📁 Tipos de Archivos Implementados

### 1. 🎯 **Archivo DIRECTO (Acceso por Hash)**
**Ubicación:** `estudiantes.dat`

#### Aplicación Real:
**Control de acceso en la entrada del campus o biblioteca**

#### Características:
- **Función Hash**: Convierte la matrícula en una posición física en el disco
- **Acceso O(1)**: Tiempo constante, sin importar cuántos estudiantes haya
- **Tamaño Fijo**: Cada registro ocupa 256 bytes
- **Capacidad**: 10,000 posiciones preallocadas

#### ¿Por qué aquí?
Cuando un estudiante pasa su credencial por el lector, el sistema debe validar su estatus (si está inscrito o no) en **menos de un segundo**. No se puede buscar secuencialmente entre 10,000 alumnos mientras hay fila en la entrada.

#### Código de ejemplo:
```csharp
// Función Hash simple
private int CalcularHash(string matricula)
{
    int suma = 0;
    foreach (char c in matricula)
        suma += c;
    return suma % 10000; // Posición entre 0-9999
}

// Buscar estudiante en tiempo constante
public Estudiante BuscarEstudiante(string matricula)
{
    int posicion = CalcularHash(matricula);
    long offset = posicion * 256; // Ir directamente a la posición
    
    using (var fs = new FileStream(_rutaArchivo, FileMode.Open))
    {
        fs.Seek(offset, SeekOrigin.Begin); // ⚡ Acceso directo
        // ... leer datos
    }
}
```

---

### 2. 📚 **Archivo SECUENCIAL INDEXADO (ISAM)**
**Ubicación:** `calificaciones.dat` + `calificaciones.idx`

#### Aplicación Real:
**Sistema de consulta de calificaciones y kárdex**

#### Características:
- **Doble archivo**: Datos (.dat) + Índice (.idx)
- **Acceso Indexado**: Búsqueda rápida por matrícula usando el índice
- **Acceso Secuencial**: Lectura ordenada para reportes
- **Flexibilidad**: Combina velocidad de búsqueda con capacidad de recorrido

#### Usos:
1. **Indexado**: Cuando un alumno entra a su portal para ver sus notas, el sistema usa el índice para saltar rápidamente a sus datos
2. **Secuencial**: Al finalizar el semestre, el coordinador necesita imprimir una lista de todos los alumnos ordenados por apellido para el acta final

#### ¿Por qué aquí?
Permite la **flexibilidad** de buscar a una persona específica (índice) y, al mismo tiempo, recorrer grupos de datos en orden alfabético o numérico (secuencial) sin tener que reordenar todo el archivo.

#### Código de ejemplo:
```csharp
// Estructura del índice
private class EntradaIndice
{
    public string Clave { get; set; }      // Matrícula
    public long Posicion { get; set; }     // Posición en archivo de datos
}

// Búsqueda por índice (rápida)
public List<Calificacion> BuscarPorMatricula(string matricula)
{
    var indices = CargarIndices(); // Cargar índice en memoria
    var posiciones = indices.Where(i => i.Clave == matricula)
                           .Select(i => i.Posicion);
    
    // Ir directamente a las posiciones indicadas
    foreach (var pos in posiciones)
    {
        fs.Seek(pos, SeekOrigin.Begin);
        // ... leer calificación
    }
}

// Lectura secuencial (para reportes)
public List<Calificacion> LeerTodasSecuencial()
{
    // Leer todo el archivo de principio a fin
    return File.ReadAllLines(_rutaArchivoDatos)
               .Select(DeserializarCalificacion)
               .OrderBy(c => c.Matricula)
               .ToList();
}
```

---

### 3. 📝 **Archivo SECUENCIAL**
**Ubicación:** `transacciones.log`

#### Aplicación Real:
**Respaldo histórico de transacciones (Logs) y procesamiento de corte de caja**

#### Características:
- **Escritura al final**: Cada transacción se agrega (append) al final del archivo
- **Lectura completa**: Procesamiento desde el inicio hasta el final
- **Orden cronológico**: Los registros se mantienen en el orden en que ocurrieron
- **Procesamiento por lotes**: Ideal para auditorías y reportes

#### Usos:
1. **Registro**: Cada vez que alguien hace un pago en caja o imprime un documento, se genera una línea en el archivo
2. **Procesamiento**: Al final del día, este archivo se procesa de principio a fin para generar el corte de caja diario

#### ¿Por qué aquí?
Los registros se escriben conforme ocurren en el tiempo. Para la **auditoría contable nocturna**, es más eficiente leer el 100% del archivo en el orden en que sucedieron los hechos (uno tras otro) que estar saltando aleatoriamente por el disco.

#### Código de ejemplo:
```csharp
// Registrar transacción (escritura al final)
public void RegistrarTransaccion(Transaccion transaccion)
{
    using (var sw = new StreamWriter(_rutaArchivo, append: true))
    {
        sw.WriteLine(SerializarTransaccion(transaccion));
    }
}

// Corte de caja (lectura secuencial completa)
public Dictionary<string, object> GenerarCorteCaja(DateTime fecha)
{
    var transacciones = File.ReadAllLines(_rutaArchivo)
        .Select(DeserializarTransaccion)
        .Where(t => t.FechaHora.Date == fecha.Date)
        .ToList();
    
    return new Dictionary<string, object>
    {
        ["TotalIngresos"] = transacciones.Where(t => t.Monto > 0).Sum(t => t.Monto),
        ["TotalEgresos"] = transacciones.Where(t => t.Monto < 0).Sum(t => t.Monto),
        // ...
    };
}
```

---

## 🏗️ Estructura del Proyecto

```
Gestion de institucion universitaria/
├── Models/
│   ├── Estudiante.cs          # Modelo para archivo directo
│   ├── Calificacion.cs        # Modelo para archivo indexado
│   └── Transaccion.cs         # Modelo para archivo secuencial
│
├── FileManagers/
│   ├── ArchivoDirecto.cs      # Implementación de acceso por hash
│   ├── ArchivoSecuencialIndexado.cs  # Implementación ISAM
│   └── ArchivoSecuencial.cs   # Implementación secuencial
│
├── Form1.cs                    # Interfaz gráfica principal
├── Form1.Designer.cs
└── Program.cs
```

---

## 🚀 Características de la Interfaz

### Tab 1: Archivo DIRECTO
- ✅ Guardar estudiantes con función hash
- 🔍 **Validar acceso en tiempo real** (simula lector de credenciales)
- 📊 Ver estadísticas del archivo

### Tab 2: Archivo INDEXADO
- ➕ Agregar calificaciones
- 🔎 **Buscar por matrícula** (usando índice - rápido)
- 📋 **Leer todas secuencialmente** (para reportes)
- 📈 Calcular promedio automático

### Tab 3: Archivo SECUENCIAL
- 📝 Registrar transacciones (append)
- 💰 **Generar corte de caja diario**
- 📊 **Estadísticas completas** (procesamiento por lotes)
- 📜 Visualizar log de transacciones

---

## 📊 Comparativa de Performance

| Operación | Directo (Hash) | Indexado | Secuencial |
|-----------|----------------|----------|------------|
| Búsqueda 1 registro | **O(1)** ⚡ | O(log n) | O(n) |
| Inserción | O(1) | O(log n) + escritura | **O(1)** ⚡ |
| Lectura completa | O(n) | **O(n)** 📋 | **O(n)** 📋 |
| Actualización | **O(1)** ⚡ | O(log n) + escritura | O(n) |
| Ordenamiento | ❌ No ordenado | ✅ Ordenado por índice | ✅ Orden cronológico |

---

## 💡 Casos de Uso Reales

### ✅ Usar Archivo DIRECTO cuando:
- Necesitas **acceso ultra-rápido** por clave única
- Las búsquedas son la operación más frecuente
- Ejemplo: Validación de acceso, autenticación, control de inventario

### ✅ Usar Archivo INDEXADO cuando:
- Necesitas **búsqueda rápida Y lectura secuencial**
- Los datos deben estar ordenados
- Ejemplo: Sistemas académicos, catálogos, bases de datos simples

### ✅ Usar Archivo SECUENCIAL cuando:
- Los datos se procesan **en orden cronológico**
- Necesitas **procesar todo** el archivo (batch processing)
- Ejemplo: Logs, auditorías, backups, procesamiento nocturno

---

## 🎓 Conceptos Aprendidos

1. **Función Hash**: Cómo convertir una clave en una dirección física
2. **Colisiones**: Manejo de claves que generan la misma posición
3. **Índices**: Separación de datos de sus claves de búsqueda
4. **Acceso Directo vs Secuencial**: Ventajas y desventajas
5. **Trade-offs**: Velocidad vs Espacio vs Ordenamiento

---

## 🛠️ Requisitos Técnicos

- **.NET 8** (o superior)
- **Windows Forms**
- **C# 12**

---

## 🎯 Cómo Ejecutar

1. Abrir el proyecto en Visual Studio 2022
2. Compilar (Ctrl + Shift + B)
3. Ejecutar (F5)
4. Probar cada tipo de archivo en su respectivo tab

---

## 📝 Notas del Desarrollador

Este proyecto fue diseñado con fines **educativos** para demostrar:

- Implementación práctica de estructuras de archivos
- Diferencias entre tipos de acceso
- Aplicaciones reales en un sistema universitario
- Análisis de complejidad temporal

**Tecnologías utilizadas:**
- Binary File I/O
- StreamReader/StreamWriter
- FileStream con Seek
- Serialización de datos
- Windows Forms para UI

---

## 📖 Referencias

- Estructuras de Archivos (Folk, Zoellick)
- Organización de Archivos y Bases de Datos
- Algoritmos de Hash
- ISAM (Indexed Sequential Access Method)

---

## 👤 Autor

Proyecto educativo - Sistema de Gestión Universitaria
.NET 8 | C# | Windows Forms

---

¡Experimenta con cada tipo de archivo y observa las diferencias en rendimiento y uso! 🚀
