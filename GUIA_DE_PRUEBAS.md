# 🧪 Guía de Pruebas y Ejemplos

## Escenarios de Prueba por Tipo de Archivo

---

## 1️⃣ ARCHIVO DIRECTO - Control de Acceso

### Escenario 1: Registrar nuevo estudiante
```
Matrícula: 20240010
Nombre: Roberto
Apellido: Sánchez
Carrera: Ingeniería en Sistemas
Estado: ✓ Activo

Resultado esperado:
✅ Estudiante guardado en posición calculada por hash
⏱️ Tiempo: < 5ms
```

### Escenario 2: Validar acceso en entrada del campus
```
Input: Matrícula 20240001
Proceso:
1. Función hash calcula posición: hash("20240001") = 1234
2. Seek directo a byte 1234 * 256 = 316,416
3. Leer 256 bytes
4. Deserializar y validar estado

Output esperado:
✅ ACCESO CONCEDIDO
Nombre: Juan Pérez
Estado: ACTIVO ✓
⏱️ Tiempo: < 3ms (acceso O(1))
```

### Escenario 3: Validar acceso - estudiante inactivo
```
Input: Matrícula 20240003
Output esperado:
⛔ ACCESO DENEGADO
Nombre: Carlos Rodríguez
Estado: INACTIVO ✗
Razón: Estudiante no está inscrito
```

### Escenario 4: Estadísticas del sistema
```
Output esperado:
Total estudiantes: 5
├─ Activos: 4 (80%)
└─ Inactivos: 1 (20%)

Capacidad: 10,000 posiciones
Ocupación: 0.05%
```

---

## 2️⃣ ARCHIVO INDEXADO - Sistema de Calificaciones

### Escenario 1: Agregar calificación
```
Matrícula: 20240001
Materia: Estructuras de Datos II
Nota: 92.5
Periodo: 2024-2

Proceso:
1. Escribir en calificaciones.dat (append)
2. Actualizar índice en calificaciones.idx
3. Ordenar índice por matrícula

Resultado:
✅ Calificación agregada
Posición en archivo: 1,024 bytes
Entrada en índice: 20240001|1024
```

### Escenario 2: Consultar kárdex (búsqueda por índice)
```
Input: Matrícula 20240001

Proceso:
1. Buscar "20240001" en índice → posiciones [512, 768, 1024]
2. Seek a posición 512 → leer calificación
3. Seek a posición 768 → leer calificación
4. Seek a posición 1024 → leer calificación

Output esperado:
═══════════════════════════════════════
🎓 KÁRDEX DEL ESTUDIANTE: 20240001
═══════════════════════════════════════

MATERIA                        NOTA    PERIODO
--------------------------------------------------------
Estructuras de Datos           85.50   2024-1
Programación                   90.00   2024-1
Estructuras de Datos II        92.50   2024-2

PROMEDIO GENERAL: 89.33

⏱️ Tiempo de búsqueda: 15ms
📍 Búsqueda mediante ÍNDICE (3 seeks directos)
```

### Escenario 3: Generar acta final (lectura secuencial)
```
Input: Leer todas las calificaciones

Proceso:
1. Leer calificaciones.dat completo (línea por línea)
2. Ordenar en memoria por Matrícula + Materia
3. Mostrar lista completa

Output esperado:
═══════════════════════════════════════
📋 ACTA FINAL - TODAS LAS CALIFICACIONES
═══════════════════════════════════════

MATRÍCULA    MATERIA                        NOTA    PERIODO
--------------------------------------------------------------------
20240001     Estructuras de Datos           85.50   2024-1
20240001     Programación                   90.00   2024-1
20240002     Base de Datos                  95.00   2024-1
20240002     Redes                          88.50   2024-1
20240004     Algoritmos                     92.00   2024-1

Total de registros: 5
⏱️ Tiempo de lectura: 25ms
📍 Lectura SECUENCIAL ordenada

Uso: Impresión de actas, reportes de fin de semestre
```

### Escenario 4: Consultar calificaciones por materia
```
Input: Materia = "Base de Datos"

Proceso:
1. Leer todas las calificaciones secuencialmente
2. Filtrar en memoria por materia

Output esperado:
Estudiantes en Base de Datos:
- 20240002: 95.00
Promedio de la materia: 95.00
```

---

## 3️⃣ ARCHIVO SECUENCIAL - Logs de Transacciones

### Escenario 1: Registrar pago de colegiatura
```
Tipo: Pago Colegiatura
Matrícula: 20240001
Descripción: Colegiatura mensual - Enero 2024
Monto: $5,000.00

Proceso:
1. Crear registro con timestamp actual
2. Append al final de transacciones.log
3. No se modifica nada existente

Output en archivo:
2024-01-15 08:30:45|Pago Colegiatura|20240001|Colegiatura mensual - Enero 2024|5000.00

✅ Transacción registrada
Posición: Final del archivo
Operación: O(1) - escritura directa al final
```

### Escenario 2: Registrar múltiples transacciones del día
```
08:30:45 | Pago Colegiatura | 20240001 | Enero 2024        | $5,000.00
09:15:20 | Impresión        | 20240002 | 20 páginas        |     $40.00
10:05:33 | Cafetería        | 20240001 | Desayuno          |     $85.50
11:20:18 | Biblioteca       | 20240003 | Multa por retraso |    $150.00
12:45:22 | Impresión        | 20240004 | 10 páginas        |     $20.00
14:30:10 | Cafetería        | 20240002 | Comida            |    $120.00
```

### Escenario 3: Generar corte de caja diario
```
Input: Fecha = 15/01/2024

Proceso:
1. Abrir transacciones.log
2. Leer TODAS las líneas de principio a fin
3. Filtrar por fecha = 15/01/2024
4. Calcular totales

Output esperado:
═══════════════════════════════
💰 CORTE DE CAJA DIARIO
═══════════════════════════════

Fecha: 15/01/2024
Total Transacciones: 6

Por tipo:
├─ Pago Colegiatura: 1 ($5,000.00)
├─ Impresión: 2 ($60.00)
├─ Cafetería: 2 ($205.50)
└─ Biblioteca: 1 ($150.00)

Ingresos: $5,415.50
Egresos: $0.00
═══════════════════════════════
Saldo Neto: $5,415.50

⏱️ Procesamiento: 35ms
📍 Lectura SECUENCIAL completa (6 registros)

Procesamiento SECUENCIAL:
Se leyó todo el archivo desde el inicio 
hasta el final para generar el reporte del día.
```

### Escenario 4: Auditoría nocturna (estadísticas completas)
```
Proceso:
1. Leer transacciones.log completo
2. Agrupar por tipo
3. Calcular totales
4. Generar reporte completo

Output esperado:
═══════════════════════════════════════
📊 ESTADÍSTICAS DE TRANSACCIONES
═══════════════════════════════════════

Total de transacciones: 50
Período: 01/01/2024 - 15/01/2024

Por tipo de transacción:
  Pago Colegiatura: 20 transacciones, Total: $100,000.00
  Impresión: 15 transacciones, Total: $600.00
  Cafetería: 10 transacciones, Total: $1,250.00
  Biblioteca: 5 transacciones, Total: $750.00

Total Ingresos: $102,600.00
Total Egresos: $0.00
Saldo Neto: $102,600.00

⏱️ Tiempo de procesamiento: 120ms
📍 Procesamiento por LOTES (batch)

Ideal para:
✓ Auditorías nocturnas
✓ Reportes mensuales
✓ Backup de transacciones
✓ Análisis histórico
```

### Escenario 5: Buscar transacciones de un estudiante
```
Input: Matrícula = 20240001

Proceso:
1. Leer TODAS las transacciones (secuencial)
2. Filtrar en memoria por matrícula
3. Ordenar por fecha

Output esperado:
═══════════════════════════════════════
📋 HISTORIAL DE TRANSACCIONES
Estudiante: 20240001
═══════════════════════════════════════

2024-01-05 08:30:00 | Pago Colegiatura | $5,000.00
2024-01-08 10:15:00 | Cafetería        |    $85.50
2024-01-12 14:20:00 | Impresión        |    $40.00
2024-01-15 08:30:45 | Pago Colegiatura | $5,000.00

Total: $10,125.50

⚠️ Nota: Búsqueda en archivo secuencial requiere
    lectura completa del archivo O(n)
    Para búsquedas frecuentes, usar archivo indexado
```

---

## 🎯 Casos de Prueba de Performance

### Test 1: Comparar búsqueda en 10,000 estudiantes

#### Archivo DIRECTO (Hash):
```
Matrículas a buscar: 10 aleatorias
Tiempo promedio: 2.5ms por búsqueda
Total: 25ms
Complejidad: O(1) × 10 = O(10)
```

#### Archivo SECUENCIAL:
```
Matrículas a buscar: 10 aleatorias
Tiempo promedio: 850ms por búsqueda
Total: 8,500ms (8.5 segundos)
Complejidad: O(n) × 10 = O(10n) donde n=10,000
```

**Conclusión:** Archivo directo es **340 veces más rápido** para búsquedas

---

### Test 2: Generar reporte de 1,000 registros

#### Archivo INDEXADO (Lectura secuencial ordenada):
```
Tiempo: 150ms
Ventaja: Ya está ordenado por índice
```

#### Archivo SECUENCIAL:
```
Tiempo: 145ms
Ventaja: Lectura simple sin índices
```

**Conclusión:** Similares para lectura completa

---

### Test 3: Insertar 1,000 registros

#### Archivo DIRECTO:
```
Tiempo: 450ms (0.45ms por registro)
Ventaja: Acceso directo a posición
```

#### Archivo SECUENCIAL:
```
Tiempo: 180ms (0.18ms por registro)
Ventaja: Solo append al final
```

**Conclusión:** Archivo secuencial es **2.5 veces más rápido** para inserciones

---

## 📊 Tabla Comparativa de Operaciones

| Operación | Directo | Indexado | Secuencial |
|-----------|---------|----------|------------|
| Buscar 1 registro | 2.5ms ⚡ | 8ms | 850ms |
| Buscar 10 registros | 25ms ⚡ | 75ms | 8.5s |
| Leer todos (1000) | 600ms | 150ms ⚡ | 145ms ⚡ |
| Insertar 1 registro | 0.45ms | 0.35ms | 0.18ms ⚡ |
| Insertar 1000 registros | 450ms | 380ms | 180ms ⚡ |
| Actualizar 1 registro | 2.5ms ⚡ | 10ms | 850ms |
| Generar reporte | 650ms | 180ms ⚡ | 160ms ⚡ |

---

## 🎓 Lecciones Aprendidas

### ✅ Usar Archivo DIRECTO para:
- ✓ Validación de acceso en tiempo real
- ✓ Control de inventario
- ✓ Autenticación de usuarios
- ✓ Cualquier búsqueda por clave única

### ✅ Usar Archivo INDEXADO para:
- ✓ Sistemas académicos (calificaciones, kárdex)
- ✓ Catálogos de productos
- ✓ Cuando necesitas búsqueda rápida Y reportes ordenados
- ✓ Datos que cambian moderadamente

### ✅ Usar Archivo SECUENCIAL para:
- ✓ Logs y auditorías
- ✓ Transacciones financieras
- ✓ Procesamiento nocturno (batch)
- ✓ Datos históricos inmutables
- ✓ Backups y archivos

---

## 🔥 Tips de Optimización

### Para Archivo DIRECTO:
1. **Función hash eficiente**: Distribuir uniformemente
2. **Manejo de colisiones**: Usar encadenamiento o probing
3. **Tamaño adecuado**: Balance entre espacio y colisiones
4. **Factor de carga**: Mantener < 70% ocupación

### Para Archivo INDEXADO:
1. **Índice en memoria**: Cargar índice completo en RAM
2. **Actualización eficiente**: Batch updates del índice
3. **Reconstrucción periódica**: Reorganizar índice
4. **Compresión**: Usar claves más cortas en índice

### Para Archivo SECUENCIAL:
1. **Buffer grande**: Leer en bloques grandes
2. **Archivado**: Mover datos antiguos a archivo histórico
3. **Compresión**: Comprimir archivos antiguos
4. **Rotación**: Logs rotativos por día/semana

---

## 🚨 Errores Comunes a Evitar

❌ **Usar archivo secuencial para búsquedas frecuentes**
✅ Usar archivo directo o indexado

❌ **Archivo directo con alta tasa de colisiones**
✅ Mejorar función hash o aumentar tamaño

❌ **No actualizar índice al agregar datos**
✅ Siempre mantener índice sincronizado

❌ **Leer archivo completo en cada búsqueda**
✅ Usar estructuras apropiadas para el caso de uso

---

¡Experimenta con estos escenarios en el sistema! 🚀
