using Amazon.DynamoDBv2.DataModel;

namespace Core.Domain.Entities.Base
{
    public class BaseEntity
    {
        [DynamoDBHashKey]
        public string Id { get; set; }
    }
}
