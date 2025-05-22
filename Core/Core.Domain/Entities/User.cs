using Amazon.DynamoDBv2.DataModel;

namespace Core.Domain.Entities
{
    [DynamoDBTable("User")]
    public class User
    {
        [DynamoDBHashKey]
        public string Id { get; set; }

        [DynamoDBProperty]
        public string Name { get; set; }

        [DynamoDBProperty]
        public string Password { get; set; }

        [DynamoDBProperty]
        public string Email { get; set; }
    }
}
