using System;
using System.Windows.Forms;
using Controllers;

namespace Views
{
    public partial class FormAlumnos : Form
    {
        AlumnosController alumnosController = new AlumnosController();
        string result;

        public FormAlumnos()
        {
            InitializeComponent();
        }

        private void FormAlumnos_Load(object sender, EventArgs e)
        {
            HabilitarBotones(true);
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
            HabilitarBotones(true);
            LimpiarCajas();
        }

        private void dgvAlumnos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            HabilitarBotones(false);
            int idAlumno = (int)dgvAlumnos.Rows[e.RowIndex].Cells["Id"].Value;
            var student = alumnosController.GetStudentById(idAlumno);
            lblPrimaryId.Text = student.Id.ToString();
            txtNombre.Text = student.Nombre;
        }

        private void HabilitarBotones(bool habilitado)
        {
            btnInsert.Enabled = habilitado;
            btnUpdate.Enabled = !habilitado;
            btnDelete.Enabled = !habilitado;
        }

        public void ListarAlumnos()
        {
            dgvAlumnos.DataSource = alumnosController.GetStudents();
        }

        public void InsertarAlumno()
        {
            result = alumnosController.InsertStudent(txtNombre.Text);
            MessageBox.Show(result, "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void EditarAlumno()
        {
            result = alumnosController.UpdateStudent(int.Parse(lblPrimaryId.Text), txtNombre.Text);
            MessageBox.Show(result, "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            HabilitarBotones(true);
        }

        public void EliminarAlumno()
        {
            result = alumnosController.DeleteStudent(int.Parse(lblPrimaryId.Text));
            MessageBox.Show(result, "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            HabilitarBotones(true);
        }

        public void LimpiarCajas()
        {
            txtNombre.Clear();
            lblPrimaryId.Text = "";
        }

        private void btnSubir_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                var ocr = alumnosController.ReadOCR(openFileDialog.FileName);
                string textOCR = string.Join("\n", ocr.ToArray());
                rchOCR.Text = textOCR;
            }
        }
    }
}