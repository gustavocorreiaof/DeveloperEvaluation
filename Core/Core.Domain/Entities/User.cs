using Amazon.DynamoDBv2.DataModel;
using Core.Domain.Entities.Base;

namespace Core.Domain.Entities
{
    [DynamoDBTable("User")]
    public class User: BaseEntity
    {
        [DynamoDBProperty]
        public string Name { get; set; }

        [DynamoDBProperty]
        public string Password { get; set; }

        [DynamoDBProperty]
        public string Email { get; set; }
    }
}
