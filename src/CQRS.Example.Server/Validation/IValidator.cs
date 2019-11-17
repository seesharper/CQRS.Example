namespace CQRS.Example.Server.Validation
{
    public interface IValidator<T>
    {
        void Validate(T value);
    }
}