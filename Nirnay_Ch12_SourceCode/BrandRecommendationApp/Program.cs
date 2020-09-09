using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BrandRecommendationApp
{
    class Program
    {
        static readonly string SUBSCRIPTION_KEY = "YOUR KEY";
        static readonly string ENDPOINT = "YOUR ENDPOINT";
        static List<VisualFeatureTypes> returnAttributes;

        static async Task Main()
        {
            ComputerVisionClient client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(SUBSCRIPTION_KEY)) { Endpoint = ENDPOINT };
            returnAttributes = new List<VisualFeatureTypes>() { VisualFeatureTypes.Brands };

            var analysis = await AnalyzeImageAsync(client);
            Console.WriteLine($"Brands identified are: {String.Join(", ", analysis.Brands.Select(x => x.Name.ToString()).ToArray())}");
            DisplayResults(analysis);

            Console.ReadKey();
        }

        /// <summary>Analyze a local image</summary>
        /// <param name="client">Authenticated Computer Vision Client</param>
        /// <returns></returns>
        private static async Task<ImageAnalysis> AnalyzeImageAsync(ComputerVisionClient client)
        {
            string localImagePath = @".\Untitled5.png";
            using (Stream imageStream = File.OpenRead(localImagePath))
            {
                return await client.AnalyzeImageInStreamAsync(imageStream, returnAttributes);
            }
        }

        /// <summary>Analyze a remote image</summary>
        /// <param name="client">Authenticated Computer Vision Client</param>
        /// <returns></returns>
        private static async Task<ImageAnalysis> AnalyzeImageAsync2(ComputerVisionClient client)
        {
            var remoteImageUrl = @"https://images.pexels.com/photos/972995/pexels-photo-972995.jpeg";
            return await client.AnalyzeImageAsync(remoteImageUrl, returnAttributes);
        }


        // Display the most relevant caption for the image
        private static void DisplayResults(ImageAnalysis analysis)
        {
            foreach (var brand in analysis.Brands)
            {
                Console.WriteLine($"Logo of {brand.Name} at location " +
                                $"{brand.Rectangle.X}, {brand.Rectangle.Y}, " +
                                $"{brand.Rectangle.X + brand.Rectangle.W}, " +
                                $"{brand.Rectangle.Y + brand.Rectangle.H}");
            }
        }
    }
}
