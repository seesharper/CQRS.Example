namespace CQRS.Example.Database
{
    public interface ISqlProvider
    {
        string InsertCustomer { get; }

        string GetCustomerById { get; }
    }
}