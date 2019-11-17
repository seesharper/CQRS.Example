using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;
using CQRS.Example.Database;
using DbReader;

namespace CQRS.Example.Server.Customers
{
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand>
    {
        private readonly IDbConnection dbConnection;
        private readonly ISqlProvider sqlProvider;

        public CreateCustomerCommandHandler(IDbConnection dbConnection, ISqlProvider sqlProvider)
        {
            this.dbConnection = dbConnection;
            this.sqlProvider = sqlProvider;
        }

        public async Task HandleAsync(CreateCustomerCommand command, CancellationToken cancellationToken = default(CancellationToken))
        {
            await dbConnection.ExecuteAsync(sqlProvider.InsertCustomer, command).ConfigureAwait(false);
        }
    }

    public class CreateCustomerCommand
    {

        [JsonIgnore]
        public long Id { get; set; }

        [Required()]
        [MaxLength(255)]
        public string FirstName { get; set; } = string.Empty;

        [Required()]
        [MaxLength(255)]
        public string LastName { get; set; } = string.Empty;

        [Required()]
        // [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required()]
        [MaxLength(255)]
        public string Address { get; set; } = string.Empty;

        [Required()]
        [MaxLength(50)]
        public string PostalCode { get; set; } = string.Empty;

        [Required()]
        [MaxLength(50)]
        public string City { get; set; } = string.Empty;

        [Required()]
        [MaxLength(50)]
        public string Country { get; set; } = string.Empty;
    }
}