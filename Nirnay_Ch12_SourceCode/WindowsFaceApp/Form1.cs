using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFaceApp2
{
    public partial class Form1 : Form
    {
        static readonly string SUBSCRIPTION_KEY = "YOUR KEY";
        static readonly string ENDPOINT = "YOUR ENDPOINT";

        public Form1()
        {
            InitializeComponent();

            IFaceClient client = new FaceClient(new ApiKeyServiceClientCredentials(SUBSCRIPTION_KEY)) { Endpoint = ENDPOINT };

            var imageFilePath = @"women3.jpeg";
            IList<DetectedFace> faceList = DetectFacesAsync(client).Result;

            DisplayFeatures(imageFilePath, faceList);
        }

        private static async Task<IList<DetectedFace>> DetectFacesAsync(IFaceClient client)
        {
            var imageFilePath = @"women3.jpeg";
            using (Stream imageFileStream = File.OpenRead(imageFilePath))
            {
                return await client.Face.DetectWithStreamAsync(imageFileStream, true, true);
            }
        }

        private void DisplayFeatures(string imageFilePath, IList<DetectedFace> detectedFaces)
        {
            Bitmap glassImage;
            var glassFilePath = @"glass.png";
            using (FileStream glassStream = new FileStream(glassFilePath, FileMode.Open, FileAccess.Read))
            {
                glassImage = new Bitmap(Image.FromStream(glassStream));
            }

            Bitmap finalImage;
            using (FileStream pngStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
            {
                finalImage = new Bitmap(Image.FromStream(pngStream));
                using (Graphics g = Graphics.FromImage(finalImage))
                {
                    var face = detectedFaces[0];
                    int x1 = (int)face.FaceLandmarks.EyebrowLeftOuter.X;
                    int y1 = (int)face.FaceLandmarks.EyebrowLeftOuter.Y;
                    int x2 = (int)face.FaceLandmarks.EyebrowRightOuter.X;
                    int y2 = (int)face.FaceLandmarks.EyeRightBottom.Y;

                    g.DrawImage(glassImage, new Rectangle(x1 - 5, y1 - 20, x2 - x1 + 10, y2 - y1 + 50));
                }
            }

            //Draw the final image in the pictureBox
            pictureBox1.Image = finalImage;
        }
    }
}
