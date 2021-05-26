using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Models.EF;
using System.Data.Entity;
using Patagames.Ocr;

namespace Controllers
{
    public class MainController
    {
        public IEnumerable<AlumnoViewModel> GetStudents()
        {
            using (alumnosEntities db = new alumnosEntities())
            {
                IEnumerable<AlumnoViewModel> student = (from d in db.Alumno
                                                        select new AlumnoViewModel
                                                        {
                                                            Id = d.Id,
                                                            Nombre = d.Nombre
                                                        }).ToList();
                return student;
            }
        }

        public Alumno GetStudentById(int id)
        {
            using (alumnosEntities db = new alumnosEntities())
            {
                Alumno student = (from d in db.Alumno
                                  where d.Id == id
                                  select d).FirstOrDefault<Alumno>();

                return student;
            }
        }

        public EntityState InsertStudent(AlumnoViewModel objAlumno)
        {
            try
            {
                using (alumnosEntities db = new alumnosEntities())
                {
                    Alumno alumno = new Alumno()
                    {
                        Nombre = objAlumno.Nombre
                    };

                    db.Alumno.Add(alumno);

                    db.Entry(alumno).State = EntityState.Added;

                    db.SaveChanges();

                    return db.Entry(alumno).State;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EntityState UpdateStudent(AlumnoViewModel objAlumno)
        {
            try
            {
                using (alumnosEntities db = new alumnosEntities())
                {
                    Alumno alumno = new Alumno()
                    {
                        Id = objAlumno.Id,
                        Nombre = objAlumno.Nombre
                    };

                    db.Alumno.Add(alumno);

                    db.Entry(alumno).State = EntityState.Modified;

                    db.SaveChanges();

                    return db.Entry(alumno).State;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EntityState DeleteStudent(int id)
        {
            try
            {
                Alumno alumno = new Alumno()
                {
                    Id = id
                };

                using (alumnosEntities db = new alumnosEntities())
                {
                    db.Entry(alumno).State = EntityState.Deleted;

                    db.SaveChanges();

                    return db.Entry(alumno).State;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ReadOCR(string pathImg)
        {
            try
            {
                using (var objOCR = OcrApi.Create())
                {
                    objOCR.Init(Patagames.Ocr.Enums.Languages.English);

                    string plainText = objOCR.GetTextFromImage(pathImg);

                    return plainText;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
