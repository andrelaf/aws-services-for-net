using System.Text.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace Movies.Api;

public class DataSeeder
{
    public async Task ImportDataAsync()
    {
        var dynamoDb = new AmazonDynamoDBClient();
        var lines = await File.ReadAllLinesAsync("./movies.csv");
        for (int i = 0; i < lines.Length; i++)
        {
            if (i == 0)
            {
                continue; //Skip header
            }

            var line = lines[i];
            var commaSplit = line.Split(',');

            var title = commaSplit[0];
            var year = int.Parse(commaSplit[1]);
            var ageRestriction = int.Parse(commaSplit[2]);
            var rottenTomatoes = int.Parse(commaSplit[3]);

            var movie = new Movie
            {
                Id = Guid.NewGuid(),
                Title = title,
                AgeRestriction = ageRestriction,
                ReleaseYear = year,
                RottenTomatoesPercentage = rottenTomatoes
            };
            
            var movieAsJson = JsonSerializer.Serialize(movie);
            var itemAsDocument = Document.FromJson(movieAsJson);
            var itemAsAttributes = itemAsDocument.ToAttributeMap();

            var createItemRequest = new PutItemRequest
            {
                TableName = "movies",
                Item = itemAsAttributes
            };

            var response = await dynamoDb.PutItemAsync(createItemRequest);
            await Task.Delay(300);
        }
    }


    public async Task ImportDataSingleTransaction()
    {
        var newMovie1 = new Movie
        {
            Id = Guid.NewGuid(),
            Title = "21 Jump Street",
            AgeRestriction = 18,
            ReleaseYear = 2012,
            RottenTomatoesPercentage = 85
        };

        var newMovie2 = new Movie2
        {
            Id = Guid.NewGuid(),
            Title = "21 Jump Street",
            AgeRestriction = 18,
            ReleaseYear = 2012,
            RottenTomatoesPercentage = 85
        };

        var asJson1 = JsonSerializer.Serialize(newMovie1);
        var attributeMap1 = Document.FromJson(asJson1).ToAttributeMap();

        var asJson2 = JsonSerializer.Serialize(newMovie2);
        var attributeMap2 = Document.FromJson(asJson2).ToAttributeMap();

        var transactionRequest = new TransactWriteItemsRequest
        {
            TransactItems = new List<TransactWriteItem>
            {
                new()
                {
                    Put = new Put
                    {
                        TableName = "movies-year-title",
                        Item = attributeMap1
                    }
                },
                new()
                {
                   Put = new Put
                    {
                        TableName = "movies-year-rotten",
                        Item = attributeMap2
                    },
                },
            }
        };


        var dynamoDb = new AmazonDynamoDBClient();

        var response = await dynamoDb.TransactWriteItemsAsync(transactionRequest);


    }
}
