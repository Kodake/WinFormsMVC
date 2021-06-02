using Models;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers.Interfaces
{
    interface IAlumno
    {
        IEnumerable<AlumnoViewModel> GetStudents();
        Alumno GetStudentById(int id);
        string InsertStudent(string nombre);
        string UpdateStudent(int id, string nombre);
        string DeleteStudent(int id);
        List<string> ReadOCR(string pathImg);
    }
}
