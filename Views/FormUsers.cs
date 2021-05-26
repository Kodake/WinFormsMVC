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

            AlumnoValidator validator = new AlumnoValidator();

            foreach (ValidationFailure failure in validator.ValidarAlumno(alumno))
            {
                MessageBox.Show(failure.ErrorMessage, "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            mainController.InsertStudent(alumno);

            MessageBox.Show("Alumno insertado satisfactoriamente.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void EditarAlumno()
        {
            alumno.Id = int.Parse(lblPrimaryId.Text);
            alumno.Nombre = txtNombre.Text;

            AlumnoValidator validator = new AlumnoValidator();

            foreach (ValidationFailure failure in validator.ValidarAlumno(alumno))
            {
                MessageBox.Show(failure.ErrorMessage, "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtOCR.Text = mainController.ReadOCR(openFileDialog1.FileName);
            }
        }
    }
}
