using Amazon.S3;
using Amazon.S3.Model;
using System.Text;

var s3Cliente = new AmazonS3Client();

await using var inputStream = new FileStream("./mister.jpg", FileMode.Open, FileAccess.Read);

var putObjectRequest = new PutObjectRequest
{
    BucketName = "andre-aws-service",
    Key = "images/mister.jpg",
    ContentType = "image/jpeg",
    InputStream = inputStream
};


await s3Cliente.PutObjectAsync(putObjectRequest);

await using var inputStreamCsv = new FileStream("./movies.csv", FileMode.Open, FileAccess.Read);

var putObjectRequestCsv = new PutObjectRequest
{
    BucketName = "andre-aws-service",
    Key = "files/movies.csv",
    ContentType = "text/csv",
    InputStream = inputStreamCsv
};

await s3Cliente.PutObjectAsync(putObjectRequestCsv);

var getObjectRequest = new GetObjectRequest
{
    BucketName = "andre-aws-service",
    Key = "files/movies.csv"
};

var response = await s3Cliente.GetObjectAsync(getObjectRequest);

using var memoryStream = new MemoryStream();
response.ResponseStream.CopyTo(memoryStream);

var text = Encoding.Default.GetString(memoryStream.ToArray());

Console.WriteLine(text);
