using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Core.Domain.Entities;
using Core.Infrastructure.Data.Interfaces;

namespace Core.Infrastructure.Data
{
    public class DynamoDbContext : IDynamoDbContext
    {
        private readonly IAmazonDynamoDB _dynamoDbClient;
        private readonly DynamoDBContext _context;

        public DynamoDbContext(IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoDbClient = dynamoDbClient;
            _context = new DynamoDBContext(_dynamoDbClient);
        }

        public async Task<User?> GetUserByNameAsync(string name)
        {
            var conditions = new List<ScanCondition>
            {
                new ScanCondition("Name", ScanOperator.Equal, name)
            };

            var search = _context.ScanAsync<User>(conditions);
            var results = await search.GetNextSetAsync();
            return results.FirstOrDefault();
        }
    }
}
