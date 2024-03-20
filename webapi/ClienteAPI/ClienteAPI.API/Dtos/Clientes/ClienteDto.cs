namespace ClienteAPI.API.Dtos.Clientes
{
    public record ClienteDto(string NomeComleto,
        DateTime DtaNascimento,
        decimal ValRenda,
        string CPF,
        string UserUuid,
        DateTime? CreateAt = null,
        DateTime? UpdateAt = null,
        string? Uuid = null)
    {
    }
}
