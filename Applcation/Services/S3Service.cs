using Amazon.S3;
using Amazon.S3.Model;
using Application.Interfaces;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Services
{
    public class S3Service: IS3Service
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public S3Service(IAmazonS3 s3Client, string bucketName)
        {
            _s3Client = s3Client;
            _bucketName = bucketName;
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType)
        {
            var putRequest = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = fileName,
                InputStream = fileStream,
                ContentType = contentType,
                //CannedACL = S3CannedACL.PublicRead // Set the ACL as needed
            };

            var response = await _s3Client.PutObjectAsync(putRequest);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return $"https://{_bucketName}.s3.amazonaws.com/{fileName}";
            }
            else
            {   
                throw new Exception("Error uploading file to S3");
            }
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = fileName
            };

            var response = await _s3Client.DeleteObjectAsync(deleteRequest);

            if (response.HttpStatusCode != System.Net.HttpStatusCode.NoContent)
            {
                throw new Exception("Error deleting file from S3");
            }
        }

        /// <summary>
        /// Extracts the S3 object path from a given URL.
        /// </summary>
        /// <param name="url">The URL of the S3 object.</param>
        /// <returns>The path of the S3 object within the bucket.</returns>
        /// <exception cref="ArgumentException">Thrown when the URL does not match the expected S3 bucket host.</exception>
        /// <example>
        /// <code>
        /// string url = "https://mybucket.s3.amazonaws.com/myfolder/myfile.txt";
        /// string path = s3Service.GetS3ObjectPath(url);
        /// path will be "myfolder/myfile.txt"
        /// </code>
        /// </example>
        public string GetS3ObjectPath(string url)
        {
            // Construct the regex pattern to match the URL
            string pattern = $@"https://{_bucketName}\.s3\.amazonaws\.com/(.+)";

            // Match the URL against the pattern
            Match match = Regex.Match(url, pattern);

            // Check if the match is successful
            if (match.Success)
            {
                // Return the captured group which contains the path
                return match.Groups[1].Value;
            }
            else
            {
                throw new ArgumentException("The URL does not match the expected S3 bucket host.");
            }
        }
    }
}
