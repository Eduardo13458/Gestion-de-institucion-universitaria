namespace Gestion_de_institucion_universitaria
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl = new TabControl();
            tabDirecto = new TabPage();
            btnValidarAcceso = new Button();
            btnEstadisticas = new Button();
            btnGuardarEstudiante = new Button();
            chkEstaInscrito = new CheckBox();
            txtCarrera = new TextBox();
            txtApellido = new TextBox();
            txtNombre = new TextBox();
            txtMatriculaDirecto = new TextBox();
            lblResultadoDirecto = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            tabIndexado = new TabPage();
            btnLeerSecuencial = new Button();
            btnBuscarCalificaciones = new Button();
            btnAgregarCalificacion = new Button();
            txtPromedio = new TextBox();
            label9 = new Label();
            lstCalificaciones = new ListBox();
            txtPeriodo = new TextBox();
            txtNota = new TextBox();
            txtMateria = new TextBox();
            txtMatriculaIndexado = new TextBox();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            tabSecuencial = new TabPage();
            btnGenerarEstadisticas = new Button();
            btnCorteCaja = new Button();
            btnRegistrarTransaccion = new Button();
            txtResultadoSecuencial = new TextBox();
            lstTransacciones = new ListBox();
            txtMonto = new TextBox();
            txtDescripcion = new TextBox();
            txtMatriculaSecuencial = new TextBox();
            cmbTipoTransaccion = new ComboBox();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            tabControl.SuspendLayout();
            tabDirecto.SuspendLayout();
            tabIndexado.SuspendLayout();
            tabSecuencial.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabDirecto);
            tabControl.Controls.Add(tabIndexado);
            tabControl.Controls.Add(tabSecuencial);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1090, 650);
            tabControl.TabIndex = 0;
            // 
            // tabDirecto
            // 
            tabDirecto.Controls.Add(btnValidarAcceso);
            tabDirecto.Controls.Add(btnEstadisticas);
            tabDirecto.Controls.Add(btnGuardarEstudiante);
            tabDirecto.Controls.Add(chkEstaInscrito);
            tabDirecto.Controls.Add(txtCarrera);
            tabDirecto.Controls.Add(txtApellido);
            tabDirecto.Controls.Add(txtNombre);
            tabDirecto.Controls.Add(txtMatriculaDirecto);
            tabDirecto.Controls.Add(lblResultadoDirecto);
            tabDirecto.Controls.Add(label4);
            tabDirecto.Controls.Add(label3);
            tabDirecto.Controls.Add(label2);
            tabDirecto.Controls.Add(label1);
            tabDirecto.Location = new Point(4, 29);
            tabDirecto.Name = "tabDirecto";
            tabDirecto.Padding = new Padding(3);
            tabDirecto.Size = new Size(1082, 617);
            tabDirecto.TabIndex = 0;
            tabDirecto.Text = "Archivo DIRECTO (Control de Acceso)";
            tabDirecto.UseVisualStyleBackColor = true;
            // 
            // btnValidarAcceso
            // 
            btnValidarAcceso.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnValidarAcceso.Location = new Point(450, 150);
            btnValidarAcceso.Name = "btnValidarAcceso";
            btnValidarAcceso.Size = new Size(250, 60);
            btnValidarAcceso.TabIndex = 12;
            btnValidarAcceso.Text = "🔍 Validar Acceso (Hash)";
            btnValidarAcceso.UseVisualStyleBackColor = true;
            btnValidarAcceso.Click += btnValidarAcceso_Click;
            // 
            // btnEstadisticas
            // 
            btnEstadisticas.Location = new Point(450, 220);
            btnEstadisticas.Name = "btnEstadisticas";
            btnEstadisticas.Size = new Size(250, 40);
            btnEstadisticas.TabIndex = 11;
            btnEstadisticas.Text = "Ver Estadísticas";
            btnEstadisticas.UseVisualStyleBackColor = true;
            btnEstadisticas.Click += btnEstadisticas_Click;
            // 
            // btnGuardarEstudiante
            // 
            btnGuardarEstudiante.Location = new Point(250, 220);
            btnGuardarEstudiante.Name = "btnGuardarEstudiante";
            btnGuardarEstudiante.Size = new Size(180, 40);
            btnGuardarEstudiante.TabIndex = 10;
            btnGuardarEstudiante.Text = "Guardar Estudiante";
            btnGuardarEstudiante.UseVisualStyleBackColor = true;
            btnGuardarEstudiante.Click += btnGuardarEstudiante_Click;
            // 
            // chkEstaInscrito
            // 
            chkEstaInscrito.AutoSize = true;
            chkEstaInscrito.Checked = true;
            chkEstaInscrito.CheckState = CheckState.Checked;
            chkEstaInscrito.Location = new Point(250, 180);
            chkEstaInscrito.Name = "chkEstaInscrito";
            chkEstaInscrito.Size = new Size(146, 24);
            chkEstaInscrito.TabIndex = 9;
            chkEstaInscrito.Text = "Estudiante Activo";
            chkEstaInscrito.UseVisualStyleBackColor = true;
            // 
            // txtCarrera
            // 
            txtCarrera.Location = new Point(250, 140);
            txtCarrera.Name = "txtCarrera";
            txtCarrera.Size = new Size(180, 27);
            txtCarrera.TabIndex = 8;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(250, 100);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(180, 27);
            txtApellido.TabIndex = 7;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(250, 60);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(180, 27);
            txtNombre.TabIndex = 6;
            // 
            // txtMatriculaDirecto
            // 
            txtMatriculaDirecto.Location = new Point(250, 20);
            txtMatriculaDirecto.Name = "txtMatriculaDirecto";
            txtMatriculaDirecto.Size = new Size(180, 27);
            txtMatriculaDirecto.TabIndex = 5;
            // 
            // lblResultadoDirecto
            // 
            lblResultadoDirecto.BorderStyle = BorderStyle.FixedSingle;
            lblResultadoDirecto.Font = new Font("Consolas", 10F);
            lblResultadoDirecto.Location = new Point(20, 280);
            lblResultadoDirecto.Name = "lblResultadoDirecto";
            lblResultadoDirecto.Size = new Size(950, 320);
            lblResultadoDirecto.TabIndex = 4;
            lblResultadoDirecto.Text = "Resultado aparecerá aquí...";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 143);
            label4.Name = "label4";
            label4.Size = new Size(60, 20);
            label4.TabIndex = 3;
            label4.Text = "Carrera:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 103);
            label3.Name = "label3";
            label3.Size = new Size(69, 20);
            label3.TabIndex = 2;
            label3.Text = "Apellido:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 63);
            label2.Name = "label2";
            label2.Size = new Size(67, 20);
            label2.TabIndex = 1;
            label2.Text = "Nombre:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 23);
            label1.Name = "label1";
            label1.Size = new Size(74, 20);
            label1.TabIndex = 0;
            label1.Text = "Matrícula:";
            // 
            // tabIndexado
            // 
            tabIndexado.Controls.Add(btnLeerSecuencial);
            tabIndexado.Controls.Add(btnBuscarCalificaciones);
            tabIndexado.Controls.Add(btnAgregarCalificacion);
            tabIndexado.Controls.Add(txtPromedio);
            tabIndexado.Controls.Add(label9);
            tabIndexado.Controls.Add(lstCalificaciones);
            tabIndexado.Controls.Add(txtPeriodo);
            tabIndexado.Controls.Add(txtNota);
            tabIndexado.Controls.Add(txtMateria);
            tabIndexado.Controls.Add(txtMatriculaIndexado);
            tabIndexado.Controls.Add(label8);
            tabIndexado.Controls.Add(label7);
            tabIndexado.Controls.Add(label6);
            tabIndexado.Controls.Add(label5);
            tabIndexado.Location = new Point(4, 29);
            tabIndexado.Name = "tabIndexado";
            tabIndexado.Padding = new Padding(3);
            tabIndexado.Size = new Size(992, 617);
            tabIndexado.TabIndex = 1;
            tabIndexado.Text = "Archivo INDEXADO (Calificaciones)";
            tabIndexado.UseVisualStyleBackColor = true;
            // 
            // btnLeerSecuencial
            // 
            btnLeerSecuencial.Location = new Point(450, 140);
            btnLeerSecuencial.Name = "btnLeerSecuencial";
            btnLeerSecuencial.Size = new Size(250, 40);
            btnLeerSecuencial.TabIndex = 13;
            btnLeerSecuencial.Text = "📋 Leer Todas (Secuencial)";
            btnLeerSecuencial.UseVisualStyleBackColor = true;
            btnLeerSecuencial.Click += btnLeerSecuencial_Click;
            // 
            // btnBuscarCalificaciones
            // 
            btnBuscarCalificaciones.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnBuscarCalificaciones.Location = new Point(450, 60);
            btnBuscarCalificaciones.Name = "btnBuscarCalificaciones";
            btnBuscarCalificaciones.Size = new Size(250, 70);
            btnBuscarCalificaciones.TabIndex = 12;
            btnBuscarCalificaciones.Text = "🔎 Buscar por Índice\r\n(Acceso Rápido)";
            btnBuscarCalificaciones.UseVisualStyleBackColor = true;
            btnBuscarCalificaciones.Click += btnBuscarCalificaciones_Click;
            // 
            // btnAgregarCalificacion
            // 
            btnAgregarCalificacion.Location = new Point(250, 170);
            btnAgregarCalificacion.Name = "btnAgregarCalificacion";
            btnAgregarCalificacion.Size = new Size(180, 40);
            btnAgregarCalificacion.TabIndex = 11;
            btnAgregarCalificacion.Text = "Agregar Calificación";
            btnAgregarCalificacion.UseVisualStyleBackColor = true;
            btnAgregarCalificacion.Click += btnAgregarCalificacion_Click;
            // 
            // txtPromedio
            // 
            txtPromedio.Location = new Point(800, 20);
            txtPromedio.Name = "txtPromedio";
            txtPromedio.ReadOnly = true;
            txtPromedio.Size = new Size(100, 27);
            txtPromedio.TabIndex = 10;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(720, 23);
            label9.Name = "label9";
            label9.Size = new Size(77, 20);
            label9.TabIndex = 9;
            label9.Text = "Promedio:";
            // 
            // lstCalificaciones
            // 
            lstCalificaciones.Font = new Font("Consolas", 9F);
            lstCalificaciones.FormattingEnabled = true;
            lstCalificaciones.ItemHeight = 18;
            lstCalificaciones.Location = new Point(20, 230);
            lstCalificaciones.Name = "lstCalificaciones";
            lstCalificaciones.Size = new Size(950, 364);
            lstCalificaciones.TabIndex = 8;
            // 
            // txtPeriodo
            // 
            txtPeriodo.Location = new Point(250, 130);
            txtPeriodo.Name = "txtPeriodo";
            txtPeriodo.Size = new Size(180, 27);
            txtPeriodo.TabIndex = 7;
            // 
            // txtNota
            // 
            txtNota.Location = new Point(250, 90);
            txtNota.Name = "txtNota";
            txtNota.Size = new Size(180, 27);
            txtNota.TabIndex = 6;
            // 
            // txtMateria
            // 
            txtMateria.Location = new Point(250, 50);
            txtMateria.Name = "txtMateria";
            txtMateria.Size = new Size(180, 27);
            txtMateria.TabIndex = 5;
            // 
            // txtMatriculaIndexado
            // 
            txtMatriculaIndexado.Location = new Point(250, 10);
            txtMatriculaIndexado.Name = "txtMatriculaIndexado";
            txtMatriculaIndexado.Size = new Size(180, 27);
            txtMatriculaIndexado.TabIndex = 4;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(20, 133);
            label8.Name = "label8";
            label8.Size = new Size(63, 20);
            label8.TabIndex = 3;
            label8.Text = "Periodo:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(20, 93);
            label7.Name = "label7";
            label7.Size = new Size(45, 20);
            label7.TabIndex = 2;
            label7.Text = "Nota:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(20, 53);
            label6.Name = "label6";
            label6.Size = new Size(63, 20);
            label6.TabIndex = 1;
            label6.Text = "Materia:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(20, 13);
            label5.Name = "label5";
            label5.Size = new Size(74, 20);
            label5.TabIndex = 0;
            label5.Text = "Matrícula:";
            // 
            // tabSecuencial
            // 
            tabSecuencial.Controls.Add(btnGenerarEstadisticas);
            tabSecuencial.Controls.Add(btnCorteCaja);
            tabSecuencial.Controls.Add(btnRegistrarTransaccion);
            tabSecuencial.Controls.Add(txtResultadoSecuencial);
            tabSecuencial.Controls.Add(lstTransacciones);
            tabSecuencial.Controls.Add(txtMonto);
            tabSecuencial.Controls.Add(txtDescripcion);
            tabSecuencial.Controls.Add(txtMatriculaSecuencial);
            tabSecuencial.Controls.Add(cmbTipoTransaccion);
            tabSecuencial.Controls.Add(label13);
            tabSecuencial.Controls.Add(label12);
            tabSecuencial.Controls.Add(label11);
            tabSecuencial.Controls.Add(label10);
            tabSecuencial.Location = new Point(4, 29);
            tabSecuencial.Name = "tabSecuencial";
            tabSecuencial.Size = new Size(992, 617);
            tabSecuencial.TabIndex = 2;
            tabSecuencial.Text = "Archivo SECUENCIAL (Transacciones/Logs)";
            tabSecuencial.UseVisualStyleBackColor = true;
            // 
            // btnGenerarEstadisticas
            // 
            btnGenerarEstadisticas.Location = new Point(450, 90);
            btnGenerarEstadisticas.Name = "btnGenerarEstadisticas";
            btnGenerarEstadisticas.Size = new Size(250, 40);
            btnGenerarEstadisticas.TabIndex = 12;
            btnGenerarEstadisticas.Text = "📊 Generar Estadísticas";
            btnGenerarEstadisticas.UseVisualStyleBackColor = true;
            btnGenerarEstadisticas.Click += btnGenerarEstadisticas_Click;
            // 
            // btnCorteCaja
            // 
            btnCorteCaja.Location = new Point(450, 40);
            btnCorteCaja.Name = "btnCorteCaja";
            btnCorteCaja.Size = new Size(250, 40);
            btnCorteCaja.TabIndex = 11;
            btnCorteCaja.Text = "💰 Corte de Caja (Hoy)";
            btnCorteCaja.UseVisualStyleBackColor = true;
            btnCorteCaja.Click += btnCorteCaja_Click;
            // 
            // btnRegistrarTransaccion
            // 
            btnRegistrarTransaccion.Location = new Point(250, 170);
            btnRegistrarTransaccion.Name = "btnRegistrarTransaccion";
            btnRegistrarTransaccion.Size = new Size(180, 40);
            btnRegistrarTransaccion.TabIndex = 10;
            btnRegistrarTransaccion.Text = "Registrar Transacción";
            btnRegistrarTransaccion.UseVisualStyleBackColor = true;
            btnRegistrarTransaccion.Click += btnRegistrarTransaccion_Click;
            // 
            // txtResultadoSecuencial
            // 
            txtResultadoSecuencial.Font = new Font("Consolas", 9F);
            txtResultadoSecuencial.Location = new Point(720, 10);
            txtResultadoSecuencial.Multiline = true;
            txtResultadoSecuencial.Name = "txtResultadoSecuencial";
            txtResultadoSecuencial.ReadOnly = true;
            txtResultadoSecuencial.ScrollBars = ScrollBars.Vertical;
            txtResultadoSecuencial.Size = new Size(250, 200);
            txtResultadoSecuencial.TabIndex = 9;
            // 
            // lstTransacciones
            // 
            lstTransacciones.Font = new Font("Consolas", 9F);
            lstTransacciones.FormattingEnabled = true;
            lstTransacciones.ItemHeight = 18;
            lstTransacciones.Location = new Point(20, 230);
            lstTransacciones.Name = "lstTransacciones";
            lstTransacciones.Size = new Size(950, 364);
            lstTransacciones.TabIndex = 8;
            // 
            // txtMonto
            // 
            txtMonto.Location = new Point(250, 130);
            txtMonto.Name = "txtMonto";
            txtMonto.Size = new Size(180, 27);
            txtMonto.TabIndex = 7;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(250, 90);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(180, 27);
            txtDescripcion.TabIndex = 6;
            // 
            // txtMatriculaSecuencial
            // 
            txtMatriculaSecuencial.Location = new Point(250, 50);
            txtMatriculaSecuencial.Name = "txtMatriculaSecuencial";
            txtMatriculaSecuencial.Size = new Size(180, 27);
            txtMatriculaSecuencial.TabIndex = 5;
            // 
            // cmbTipoTransaccion
            // 
            cmbTipoTransaccion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoTransaccion.FormattingEnabled = true;
            cmbTipoTransaccion.Items.AddRange(new object[] { "Pago Colegiatura", "Impresión", "Cafetería", "Biblioteca", "Beca" });
            cmbTipoTransaccion.Location = new Point(250, 10);
            cmbTipoTransaccion.Name = "cmbTipoTransaccion";
            cmbTipoTransaccion.Size = new Size(180, 28);
            cmbTipoTransaccion.TabIndex = 4;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(20, 133);
            label13.Name = "label13";
            label13.Size = new Size(56, 20);
            label13.TabIndex = 3;
            label13.Text = "Monto:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(20, 93);
            label12.Name = "label12";
            label12.Size = new Size(90, 20);
            label12.TabIndex = 2;
            label12.Text = "Descripción:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(20, 53);
            label11.Name = "label11";
            label11.Size = new Size(74, 20);
            label11.TabIndex = 1;
            label11.Text = "Matrícula:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(20, 13);
            label10.Name = "label10";
            label10.Size = new Size(42, 20);
            label10.TabIndex = 0;
            label10.Text = "Tipo:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1090, 650);
            Controls.Add(tabControl);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema Universitario - Gestión de Archivos";
            Load += Form1_Load;
            tabControl.ResumeLayout(false);
            tabDirecto.ResumeLayout(false);
            tabDirecto.PerformLayout();
            tabIndexado.ResumeLayout(false);
            tabIndexado.PerformLayout();
            tabSecuencial.ResumeLayout(false);
            tabSecuencial.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl;
        private TabPage tabDirecto;
        private TabPage tabIndexado;
        private TabPage tabSecuencial;
        private Label label1;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label lblResultadoDirecto;
        private TextBox txtMatriculaDirecto;
        private TextBox txtCarrera;
        private TextBox txtApellido;
        private TextBox txtNombre;
        private CheckBox chkEstaInscrito;
        private Button btnGuardarEstudiante;
        private Button btnEstadisticas;
        private Button btnValidarAcceso;
        private Button btnVerMatriculas;
        private Label label5;
        private Label label8;
        private Label label7;
        private Label label6;
        private TextBox txtMatriculaIndexado;
        private TextBox txtPeriodo;
        private TextBox txtNota;
        private TextBox txtMateria;
        private ListBox lstCalificaciones;
        private Label label9;
        private TextBox txtPromedio;
        private Button btnAgregarCalificacion;
        private Button btnBuscarCalificaciones;
        private Button btnLeerSecuencial;
        private Label label10;
        private Label label13;
        private Label label12;
        private Label label11;
        private ComboBox cmbTipoTransaccion;
        private TextBox txtMatriculaSecuencial;
        private TextBox txtDescripcion;
        private TextBox txtMonto;
        private ListBox lstTransacciones;
        private TextBox txtResultadoSecuencial;
        private Button btnRegistrarTransaccion;
        private Button btnCorteCaja;
        private Button btnGenerarEstadisticas;
    }
}
