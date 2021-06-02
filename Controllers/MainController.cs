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
        AlumnoViewModel alumno = new AlumnoViewModel();
        private static List<string> errors;
        private static readonly AlumnoValidator validator = new AlumnoValidator();
        private string result = "";
        private int initialCount = 0;

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

        public string InsertStudent(string nombre)
        {
            result = "";
            try
            {
                using (alumnosEntities db = new alumnosEntities())
                {
                    alumno.Nombre = nombre;
                    errors = validator.ValidarAlumno(alumno);
                    string errorMessage = string.Join("\n", errors.ToArray());
                    if (errors.Count > initialCount)
                    {
                        return errorMessage;
                    }
                    else
                    {
                        Alumno alumno = new Alumno()
                        {
                            Nombre = nombre
                        };
                        db.Alumno.Add(alumno);
                        db.Entry(alumno).State = EntityState.Added;
                        db.SaveChanges();

                        return result = "Alumno insertado satisfactoriamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                return result = ex.Message;
            }
        }

        public string UpdateStudent(int id, string nombre)
        {
            result = "";
            try
            {
                using (alumnosEntities db = new alumnosEntities())
                {
                    alumno.Id = id;
                    alumno.Nombre = nombre;
                    errors = validator.ValidarAlumno(alumno);
                    string errorMessage = string.Join("\n", errors.ToArray());
                    if (errors.Count > initialCount)
                    {
                        return errorMessage;
                    }
                    else
                    {
                        Alumno alumno = new Alumno()
                        {
                            Id = id,
                            Nombre = nombre
                        };
                        db.Alumno.Add(alumno);
                        db.Entry(alumno).State = EntityState.Modified;
                        db.SaveChanges();

                        return result = "Alumno editado satisfactoriamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                return result = ex.Message;
            }
        }

        public string DeleteStudent(int id)
        {
            result = "";
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

                    return result = "Alumno eliminado satisfactoriamente.";
                }
            }
            catch (Exception ex)
            {
                return result = ex.Message;
            }
        }

        public List<string> ReadOCR(string pathImg)
        {
            try
            {
                using (var objOCR = OcrApi.Create())
                {
                    objOCR.Init(Patagames.Ocr.Enums.Languages.Spanish);
                    string plainText = objOCR.GetTextFromImage(pathImg);
                    List<string> wordList = new List<string>();
                    string word = "";
                    foreach (var item in plainText)
                    {
                        if (item.ToString() != "\n")
                        {
                            word = word + item;
                        }
                        else
                        {
                            wordList.Add(word);
                            word = "";
                        }
                    }
                    return wordList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}