namespace CatalogService.Domain.Exceptions;
internal class InvalidAmountException : Exception
{
    public InvalidAmountException(string param)
        : base($"{param} must be a positive number.")
    {
    }
}
