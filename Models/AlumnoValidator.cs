using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;

namespace Models
{
    public class AlumnoValidator : AbstractValidator<AlumnoViewModel>
    {
        public AlumnoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty()
                .WithMessage("No puede dejar el nombre del estudiante vacío.")
                .MaximumLength(30)
                .WithMessage("La longitud máxima del nombre es de 30 caracteres.")
                .MinimumLength(2)
                .WithMessage("La longitud mínima del nombre es de 2 caracteres.")
                .Matches(@"^[a-zA-Z ']*$")
                .WithMessage("Sólo se aceptan letras, no números.");
        }

        public List<string> ValidarAlumno(AlumnoViewModel alumno)
        {
            AlumnoValidator validator = new AlumnoValidator();
            ValidationResult results = validator.Validate(alumno);
            IList<ValidationFailure> failures = results.Errors;
            List<string> errors = new List<string>();

            foreach (ValidationFailure failure in failures)
            {
                errors.Add(failure.ToString());
            }

            return errors;
        }
    }
}
