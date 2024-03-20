using ClienteAPI.Domain.Core.Models;
using FluentValidation;
using System.Net;
using System.Text.RegularExpressions;

namespace ClienteAPI.Domain.Clientes
{
    public class Cliente : Entity<Cliente>
    {
        private string regexValidName = @"^[a-zA-ZÀ-ú\s'-]+ [a-zA-ZÀ-ú\s'-]+$";
        public Cliente()
        {
            RuleFor(c => c.NomeComleto)
                .NotEmpty()
                .MinimumLength(3)
                .Matches(regexValidName)
                .WithMessage("É nessário ao menos o primeiro e o ultimo nome");
            RuleFor(c => c.DtaNascimento)
                .NotEmpty()
                .LessThan(DateTime.UtcNow)
                .WithMessage("A data não corresponde à um período válido");
            RuleFor(c => c.CPF)
                .MaximumLength(11)
                .Matches("([0-9]{2}[\\.]?[0-9]{3}[\\.]?[0-9]{3}[\\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\\.]?[0-9]{3}[\\.]?[0-9]{3}[-]?[0-9]{2})")
                .WithMessage("CPF em formato inválido");
            RuleFor(c => c.UsuarioUuid)
                .NotEmpty()
                .NotNull()
                .Must(uuid => Guid.Empty != uuid)
                .WithMessage("Identificador do usuario obrigatória");
        }

        public string NomeComleto { get; set; } = string.Empty;
        public DateTime DtaNascimento { get; set; }
        public decimal ValRenda { get; set; }
        private string _cpf = string.Empty;
        public string CPF
        {
            get
            {
                return _cpf.Replace("-", "")
                    .Replace(".", "");
            }
            set { _cpf = value; }
        }
        public Guid UsuarioUuid { get; set; }
        public override bool IsValid()
        {
            var validatorResult = Validate(this);
            ValidationResult = validatorResult;
            return ValidationResult.IsValid;
        }
    }
}
