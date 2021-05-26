using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Controllers;
using FluentValidation.Results;
using Models;

namespace Views
{
    public partial class FormAlumnos : Form
    {
        MainController mainController = new MainController();
        AlumnoViewModel alumno = new AlumnoViewModel();
        AlumnoValidator validator = new AlumnoValidator();
        List<string> errors = new List<string>();
        int initialCount = 0;

        public FormAlumnos()
        {
            InitializeComponent();
        }

        private void FormAlumnos_Load(object sender, EventArgs e)
        {
            LimpiarBotones();
            ListarAlumnos();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            InsertarAlumno();
            ListarAlumnos();
            LimpiarCajas();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            EditarAlumno();
            ListarAlumnos();
            LimpiarCajas();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            EliminarAlumno();
            ListarAlumnos();
            LimpiarCajas();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarBotones();
            LimpiarCajas();
        }

        private void dgvAlumnos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnInsert.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            int idAlumno = (int)dgvAlumnos.Rows[e.RowIndex].Cells["Id"].Value;
            var student = mainController.GetStudentById(idAlumno);
            lblPrimaryId.Text = student.Id.ToString();
            txtNombre.Text = student.Nombre;
        }

        public void ListarAlumnos()
        {
            dgvAlumnos.DataSource = mainController.GetStudents();
        }

        public void InsertarAlumno()
        {
            alumno.Nombre = txtNombre.Text;

            errors = validator.ValidarAlumno(alumno);

            string errorMessage = string.Join("\n", errors.ToArray());

            if (errors.Count > initialCount)
            {
                MessageBox.Show(errorMessage, "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            mainController.InsertStudent(alumno);

            MessageBox.Show("Alumno insertado satisfactoriamente.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void EditarAlumno()
        {
            alumno.Id = int.Parse(lblPrimaryId.Text);
            alumno.Nombre = txtNombre.Text;

            errors = validator.ValidarAlumno(alumno);

            string errorMessage = string.Join("\n", errors.ToArray());

            if (errors.Count > initialCount)
            {
                MessageBox.Show(errorMessage, "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            mainController.UpdateStudent(alumno);

            MessageBox.Show("Alumno editado satisfactoriamente.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            LimpiarBotones();
        }

        public void EliminarAlumno()
        {
            mainController.DeleteStudent(int.Parse(lblPrimaryId.Text));

            MessageBox.Show("Alumno eliminado satisfactoriamente.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            LimpiarBotones();
        }

        public void LimpiarCajas()
        {
            txtNombre.Clear();
            lblPrimaryId.Text = "";
        }

        public void LimpiarBotones()
        {
            btnInsert.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void btnSubir_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                var ocr =  mainController.ReadOCR(openFileDialog.FileName);
                string textOCR = string.Join("\n", ocr.ToArray());
                rchOCR.Text = textOCR;
            }
        }
    }
}
