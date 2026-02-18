using Gestion_de_institucion_universitaria.FileManagers;
using Gestion_de_institucion_universitaria.Models;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Gestion_de_institucion_universitaria
{
    public partial class Form1 : Form
    {
        private ArchivoDirecto? archivoDirecto;
        private ArchivoSecuencialIndexado? archivoIndexado;
        private ArchivoSecuencial? archivoSecuencial;

        private readonly string rutaDirecto = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "estudiantes.dat");
        private readonly string rutaIndexado = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "calificaciones");
        private readonly string rutaSecuencial = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "transacciones.log");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Inicializar manejadores de archivos
            archivoDirecto = new ArchivoDirecto(rutaDirecto);
            archivoIndexado = new ArchivoSecuencialIndexado(rutaIndexado);
            archivoSecuencial = new ArchivoSecuencial(rutaSecuencial);

            // Configurar valores predeterminados
            cmbTipoTransaccion.SelectedIndex = 0;
            txtPeriodo.Text = "2024-1";

            // Cargar datos de ejemplo si no existen
            CargarDatosEjemplo();

            MostrarMensajeBienvenida();
        }

        private void MostrarMensajeBienvenida()
        {
            lblResultadoDirecto.Text = @"═══════════════════════════════════════════════════════
🎓 SISTEMA UNIVERSITARIO - GESTIÓN DE ARCHIVOS
═══════════════════════════════════════════════════════

✅ ARCHIVO DIRECTO (Hash): Control de acceso rápido
   - Validación en tiempo real O(1)
   - Función hash para ubicar estudiantes

✅ ARCHIVO INDEXADO (ISAM): Sistema de calificaciones
   - Búsqueda rápida por índice
   - Lectura secuencial ordenada

✅ ARCHIVO SECUENCIAL: Logs de transacciones
   - Registro cronológico de eventos
   - Procesamiento por lotes

Archivos ubicados en: " + AppDomain.CurrentDomain.BaseDirectory;
        }

        private void CargarDatosEjemplo()
        {
            try
            {
                // Estudiantes de ejemplo
                var estudiantes = new[]
                {
                    new Estudiante("20240001", "Juan", "Pérez", "Informática", true),
                    new Estudiante("20240002", "María", "González", "Sistemas", true),
                    new Estudiante("20240003", "Carlos", "Rodríguez", "Informática", false),
                    new Estudiante("20240004", "Ana", "Martínez", "Software", true),
                    new Estudiante("20240005", "Luis", "López", "Datos", true)
                };

                // Guardar en archivo directo
                foreach (var est in estudiantes)
                {
                    if (archivoDirecto!.BuscarEstudiante(est.Matricula) == null)
                    {
                        archivoDirecto.GuardarEstudiante(est);
                    }
                }

                // Calificaciones de ejemplo
                var calificaciones = new[]
                {
                    new Calificacion("20240001", "Estructuras de Datos", 85.5, "2024-1"),
                    new Calificacion("20240001", "Programación", 90.0, "2024-1"),
                    new Calificacion("20240002", "Base de Datos", 95.0, "2024-1"),
                    new Calificacion("20240002", "Redes", 88.5, "2024-1"),
                    new Calificacion("20240004", "Algoritmos", 92.0, "2024-1")
                };

                // Verificar si ya hay calificaciones
                if (archivoIndexado!.LeerTodasSecuencial().Count == 0)
                {
                    foreach (var cal in calificaciones)
                    {
                        archivoIndexado.AgregarCalificacion(cal);
                    }
                }

                // Transacciones de ejemplo
                if (archivoSecuencial!.LeerTodasTransacciones().Count == 0)
                {
                    archivoSecuencial.RegistrarTransaccion(
                        new Transaccion("Pago Colegiatura", "20240001", "Colegiatura mensual", 5000));
                    archivoSecuencial.RegistrarTransaccion(
                        new Transaccion("Impresión", "20240002", "20 páginas", 40));
                    archivoSecuencial.RegistrarTransaccion(
                        new Transaccion("Cafetería", "20240001", "Desayuno", 85.50m));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando datos de ejemplo: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== ARCHIVO DIRECTO ====================

        private void btnGuardarEstudiante_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMatriculaDirecto.Text))
                {
                    MessageBox.Show("Ingrese la matrícula", "Validación", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var estudiante = new Estudiante(
                    txtMatriculaDirecto.Text,
                    txtNombre.Text,
                    txtApellido.Text,
                    txtCarrera.Text,
                    chkEstaInscrito.Checked
                );

                archivoDirecto!.GuardarEstudiante(estudiante);

                lblResultadoDirecto.Text = $@"✅ Estudiante guardado exitosamente

Matrícula: {estudiante.Matricula}
Nombre: {estudiante.Nombre} {estudiante.Apellido}
Carrera: {estudiante.Carrera}
Estado: {(estudiante.EstaInscrito ? "ACTIVO ✓" : "INACTIVO ✗")}

La función hash ubicó el registro en la posición física del disco
para permitir acceso directo en O(1) tiempo constante.";

                // Limpiar campos
                txtMatriculaDirecto.Clear();
                txtNombre.Clear();
                txtApellido.Clear();
                txtCarrera.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnValidarAcceso_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMatriculaDirecto.Text))
                {
                    MessageBox.Show("Ingrese la matrícula para validar", "Validación", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var inicio = DateTime.Now;
                var estudiante = archivoDirecto!.BuscarEstudiante(txtMatriculaDirecto.Text);
                var tiempo = (DateTime.Now - inicio).TotalMilliseconds;

                if (estudiante == null)
                {
                    lblResultadoDirecto.Text = $@"❌ ACCESO DENEGADO

Matrícula: {txtMatriculaDirecto.Text}
Estado: NO ENCONTRADO

⏱️ Tiempo de búsqueda: {tiempo:F2} ms
📍 Acceso directo mediante función HASH";
                }
                else
                {
                    bool accesoConcedido = estudiante.EstaInscrito;

                    lblResultadoDirecto.Text = $@"{(accesoConcedido ? "✅ ACCESO CONCEDIDO" : "⛔ ACCESO DENEGADO")}

Matrícula: {estudiante.Matricula}
Nombre: {estudiante.Nombre} {estudiante.Apellido}
Carrera: {estudiante.Carrera}
Estado: {(estudiante.EstaInscrito ? "ACTIVO ✓" : "INACTIVO ✗")}
Fecha Inscripción: {estudiante.FechaInscripcion:dd/MM/yyyy}

⏱️ Tiempo de búsqueda: {tiempo:F2} ms
📍 Acceso DIRECTO mediante función HASH
🚀 Complejidad: O(1) - Tiempo constante

Explicación: La función hash convierte la matrícula en una
posición física en el disco, permitiendo ir directamente
al registro sin buscar secuencialmente.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEstadisticas_Click(object sender, EventArgs e)
        {
            try
            {
                var (total, activos, inactivos) = archivoDirecto!.ObtenerEstadisticas();

                lblResultadoDirecto.Text = $@"📊 ESTADÍSTICAS DEL ARCHIVO DIRECTO

Total de estudiantes registrados: {total}
├─ Estudiantes ACTIVOS: {activos}
└─ Estudiantes INACTIVOS: {inactivos}

Capacidad del archivo: 10,000 posiciones
Ocupación: {(total * 100.0 / 10000):F2}%

Ubicación: {rutaDirecto}

Características del Archivo Directo:
• Cada registro ocupa 256 bytes fijos
• Función hash para cálculo de posición
• Acceso en tiempo constante O(1)
• Ideal para validaciones rápidas";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== ARCHIVO INDEXADO ====================

        private void btnAgregarCalificacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMatriculaIndexado.Text) || 
                    string.IsNullOrWhiteSpace(txtMateria.Text) ||
                    !double.TryParse(txtNota.Text, out double nota))
                {
                    MessageBox.Show("Complete todos los campos correctamente", "Validación", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var calificacion = new Calificacion(
                    txtMatriculaIndexado.Text,
                    txtMateria.Text,
                    nota,
                    txtPeriodo.Text
                );

                archivoIndexado!.AgregarCalificacion(calificacion);

                MessageBox.Show("✅ Calificación agregada exitosamente\n\n" +
                    "Se actualizó el archivo de datos y el índice.", 
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar
                txtMateria.Clear();
                txtNota.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscarCalificaciones_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMatriculaIndexado.Text))
                {
                    MessageBox.Show("Ingrese la matrícula", "Validación", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var inicio = DateTime.Now;
                var calificaciones = archivoIndexado!.BuscarPorMatricula(txtMatriculaIndexado.Text);
                var tiempo = (DateTime.Now - inicio).TotalMilliseconds;

                lstCalificaciones.Items.Clear();
                lstCalificaciones.Items.Add("═══════════════════════════════════════════════════════");
                lstCalificaciones.Items.Add($"🎓 KÁRDEX DEL ESTUDIANTE: {txtMatriculaIndexado.Text}");
                lstCalificaciones.Items.Add("═══════════════════════════════════════════════════════");
                lstCalificaciones.Items.Add("");

                if (calificaciones.Count == 0)
                {
                    lstCalificaciones.Items.Add("No se encontraron calificaciones");
                }
                else
                {
                    lstCalificaciones.Items.Add($"{"MATERIA",-30} {"NOTA",8} {"PERIODO",12}");
                    lstCalificaciones.Items.Add(new string('-', 60));

                    foreach (var cal in calificaciones.OrderBy(c => c.Materia))
                    {
                        lstCalificaciones.Items.Add(
                            $"{cal.Materia,-30} {cal.Nota,8:F2} {cal.Periodo,12}");
                    }

                    lstCalificaciones.Items.Add("");
                    double promedio = archivoIndexado.CalcularPromedio(txtMatriculaIndexado.Text);
                    txtPromedio.Text = promedio.ToString("F2");
                    lstCalificaciones.Items.Add($"PROMEDIO GENERAL: {promedio:F2}");
                }

                lstCalificaciones.Items.Add("");
                lstCalificaciones.Items.Add($"⏱️ Tiempo de búsqueda: {tiempo:F2} ms");
                lstCalificaciones.Items.Add("📍 Búsqueda mediante ÍNDICE (acceso rápido)");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLeerSecuencial_Click(object sender, EventArgs e)
        {
            try
            {
                var inicio = DateTime.Now;
                var calificaciones = archivoIndexado!.LeerTodasSecuencial();
                var tiempo = (DateTime.Now - inicio).TotalMilliseconds;

                lstCalificaciones.Items.Clear();
                lstCalificaciones.Items.Add("═══════════════════════════════════════════════════════");
                lstCalificaciones.Items.Add("📋 LECTURA SECUENCIAL - TODAS LAS CALIFICACIONES");
                lstCalificaciones.Items.Add("═══════════════════════════════════════════════════════");
                lstCalificaciones.Items.Add("");
                lstCalificaciones.Items.Add($"{"MATRÍCULA",12} {"MATERIA",-30} {"NOTA",8} {"PERIODO",12}");
                lstCalificaciones.Items.Add(new string('-', 70));

                foreach (var cal in calificaciones)
                {
                    lstCalificaciones.Items.Add(
                        $"{cal.Matricula,12} {cal.Materia,-30} {cal.Nota,8:F2} {cal.Periodo,12}");
                }

                lstCalificaciones.Items.Add("");
                lstCalificaciones.Items.Add($"Total de registros: {calificaciones.Count}");
                lstCalificaciones.Items.Add($"⏱️ Tiempo de lectura: {tiempo:F2} ms");
                lstCalificaciones.Items.Add("📍 Lectura SECUENCIAL ordenada");
                lstCalificaciones.Items.Add("");
                lstCalificaciones.Items.Add("Uso: Para reportes, actas finales, listados completos");

                txtPromedio.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== ARCHIVO SECUENCIAL ====================

        private void btnRegistrarTransaccion_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbTipoTransaccion.SelectedIndex < 0 || 
                    string.IsNullOrWhiteSpace(txtMatriculaSecuencial.Text) ||
                    !decimal.TryParse(txtMonto.Text, out decimal monto))
                {
                    MessageBox.Show("Complete todos los campos correctamente", "Validación", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var transaccion = new Transaccion(
                    cmbTipoTransaccion.SelectedItem!.ToString()!,
                    txtMatriculaSecuencial.Text,
                    txtDescripcion.Text,
                    monto
                );

                archivoSecuencial!.RegistrarTransaccion(transaccion);

                MessageBox.Show($"✅ Transacción registrada\n\n{transaccion}", 
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Actualizar lista
                ActualizarListaTransacciones();

                // Limpiar
                txtDescripcion.Clear();
                txtMonto.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCorteCaja_Click(object sender, EventArgs e)
        {
            try
            {
                var reporte = archivoSecuencial!.GenerarCorteCaja(DateTime.Today);

                txtResultadoSecuencial.Text = $@"💰 CORTE DE CAJA DIARIO
═══════════════════════════════

Fecha: {reporte["Fecha"]}
Total Transacciones: {reporte["TotalTransacciones"]}

Ingresos: ${reporte["TotalIngresos"]:F2}
Egresos: ${reporte["TotalEgresos"]:F2}
═══════════════════════════════
Saldo Neto: ${reporte["SaldoNeto"]:F2}

Procesamiento SECUENCIAL:
Se leyó todo el archivo desde el
inicio hasta el final para generar
el reporte del día.";

                ActualizarListaTransacciones();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerarEstadisticas_Click(object sender, EventArgs e)
        {
            try
            {
                string estadisticas = archivoSecuencial!.GenerarEstadisticas();
                txtResultadoSecuencial.Text = estadisticas + 
                    "\n\nProcesamiento por lotes:\nSe procesó el archivo completo\nde forma secuencial.";

                ActualizarListaTransacciones();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarListaTransacciones()
        {
            try
            {
                var transacciones = archivoSecuencial!.LeerTodasTransacciones()
                    .OrderByDescending(t => t.FechaHora)
                    .Take(50)
                    .ToList();

                lstTransacciones.Items.Clear();
                lstTransacciones.Items.Add("═══════════════════════════════════════════════════════════════════");
                lstTransacciones.Items.Add("📝 LOG DE TRANSACCIONES (Últimas 50)");
                lstTransacciones.Items.Add("═══════════════════════════════════════════════════════════════════");

                foreach (var trans in transacciones)
                {
                    lstTransacciones.Items.Add(trans.ToString());
                }

                lstTransacciones.Items.Add("");
                lstTransacciones.Items.Add("Características del Archivo Secuencial:");
                lstTransacciones.Items.Add("• Escritura al FINAL del archivo (append)");
                lstTransacciones.Items.Add("• Lectura desde el INICIO hasta el FINAL");
                lstTransacciones.Items.Add("• Ideal para logs, auditorías, procesamiento por lotes");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error actualizando lista: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

