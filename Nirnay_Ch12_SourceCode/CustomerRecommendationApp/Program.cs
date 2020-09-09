using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CustomerRecommendationApp
{
    class Program
    {
        static readonly string SUBSCRIPTION_KEY = "YOUR KEY";
        static readonly string ENDPOINT = "YOUR ENDPOINT";
        static List<FaceAttributeType> returnFaceAttributes;

        static IFaceClient client;

        static async Task Main()
        {
            client = new FaceClient(new ApiKeyServiceClientCredentials(SUBSCRIPTION_KEY)) { Endpoint = ENDPOINT };

            returnFaceAttributes = new List<FaceAttributeType> { FaceAttributeType.Accessories, FaceAttributeType.Age, FaceAttributeType.Emotion, FaceAttributeType.Gender };
            var faceList = await DetectFacesAsync(client);

            foreach (var face in faceList)
            {
                Console.WriteLine($"A {face.FaceAttributes.Gender} id {face.FaceId} of age {face.FaceAttributes.Age} " +
                    $"at location {face.FaceRectangle.Left}, {face.FaceRectangle.Left}, " +
                    $"{face.FaceRectangle.Top + face.FaceRectangle.Width}, {face.FaceRectangle.Top + face.FaceRectangle.Height}" +
                    $" having Happiness {face.FaceAttributes.Emotion.Happiness * 100}%");
                Console.WriteLine("");
            }

            Console.ReadKey();
        }

        /// <summary>Detect faces with all attributes from local image</summary>
        /// <param name="client">Authenticated Face Client object</param>
        /// <returns></returns>
        private static async Task<IList<DetectedFace>> DetectFacesAsync(IFaceClient client)
        {
            var imageFilePath = @"<LocalImage>";
            using (Stream imageFileStream = File.OpenRead(imageFilePath))
            {
                return await client.Face.DetectWithStreamAsync(imageFileStream, true, false, returnFaceAttributes);
            }
        }

        /// <summary>Detect faces with all attributes from online image</summary>
        /// <param name="client">Authenticated Face Client object</param>
        /// <returns></returns>
        public static async Task<IList<DetectedFace>> DetectFacesAsync2(IFaceClient client)
        {
            var remoteImageUrl = @"https://images.pexels.com/photos/972995/pexels-photo-972995.jpeg";
            return await client.Face.DetectWithUrlAsync(remoteImageUrl, true, false, returnFaceAttributes, RecognitionModel.Recognition01);
        }
    }
}
