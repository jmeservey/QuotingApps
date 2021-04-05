using Google.Cloud.Vision.V1;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace PrintDimensionCounterWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public System.Windows.Media.ImageSource PrintPicture { get; set; }
        public Image PrintImage { get; set; }
        private List<BoxPoints> BoxPointsList { get; set; } = new List<BoxPoints>();

        public MainWindow()
        {
            InitializeComponent();
            string credential_path = @".\My First Project-f1ddfcb27717.json";
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credential_path);
            this.Title = GetTitle();
        }
        private string GetTitle()
        {
            //System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            //FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            return $"Print Dimension Counter v.{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";
        }
        private class BoxPoints
        {
            public int X1 { get; set; }
            public int Y1 { get; set; }
            public int X3 { get; set; }
            public int Y3 { get; set; }
        }
        private int CountDimensions(Image image)
        {
            int dimensionCount = 0;
            string wordText = "", paragraphText = "";
            bool addBox = false;
            Regex rgx = new Regex(@"\b^(\d*)\s?(x)(.+)\b", RegexOptions.IgnoreCase);
            Regex rgx2 = new Regex(@"\d{2,3}");
            Regex rgx3 = new Regex(@"\b^(.*)(\d+)(pl)\b", RegexOptions.IgnoreCase);
            Regex rgx4 = new Regex(@"\d\s*,\s*\d", RegexOptions.IgnoreCase);

            ImageAnnotatorClient client = ImageAnnotatorClient.Create();

            TextAnnotation text = client.DetectDocumentText(image);
            //ConsoleManager.Show();
            //Trace.WriteLine($"Text: {text.Text}");
            foreach (var page in text.Pages)
            {
                foreach (var block in page.Blocks)
                {
                    string box = string.Join(" - ", block.BoundingBox.Vertices.Select(v => $"({v.X}, {v.Y})"));
                    //Trace.WriteLine($"Block {block.BlockType} at {box}");
                    foreach (var paragraph in block.Paragraphs)
                    {
                        box = string.Join(" - ", paragraph.BoundingBox.Vertices.Select(v => $"({v.X}, {v.Y})"));
                        //Trace.WriteLine($"  Paragraph at {box}");

                        paragraphText = "";

                        foreach (var word in paragraph.Words)
                        {
                            wordText = string.Join("", word.Symbols.Select(s => s.Text));

                            if (paragraphText.Length > 0)
                            {
                                paragraphText += $" {wordText}";
                            }
                            else
                            {
                                paragraphText += wordText;
                            }

                            //Trace.WriteLine($"    Word: {wordText}");
                        }

                        addBox = false;

                        if (rgx4.IsMatch(paragraphText))
                        {
                            dimensionCount += (paragraphText.Split(',').Count() - 1);
                            Trace.WriteLine($"{paragraphText} has multiples. {paragraphText.Split(',').Count() - 1} added. Total: {dimensionCount}");

                            addBox = true;
                        }

                        //Trace.WriteLine($"{paragraphText}");

                        if (rgx2.IsMatch(paragraphText))
                        {
                            dimensionCount++;
                            Trace.WriteLine($"{paragraphText} has measurements. 1 added. Total: {dimensionCount}");
                            //Trace.WriteLine($"{paragraph.BoundingBox.Vertices[0].X} {paragraph.BoundingBox.Vertices[0].X}");
                            addBox = true;
                        }

                        var match = rgx.Match(paragraphText);

                        if (match.Success)
                        {
                            if (int.TryParse(match.Groups[1].Value, out int result))
                            {
                                Trace.WriteLine($"{paragraphText} has multiples. {result - 1} added.  Total: {dimensionCount}");
                                dimensionCount += result - 1;

                                addBox = true;
                            }


                            //Trace.WriteLine($"{paragraph.BoundingBox.Vertices[0].X} {paragraph.BoundingBox.Vertices[0].X}");
                        }

                        var match3 = rgx3.Match(paragraphText);

                        if (match3.Success)
                        {
                            dimensionCount += int.Parse(match3.Groups[2].Value) - 1;

                            Trace.WriteLine($"{paragraphText} has multiples. {int.Parse(match3.Groups[2].Value) - 1} added.  Total: {dimensionCount}");
                            //Trace.WriteLine($"{paragraph.BoundingBox.Vertices[0].X} {paragraph.BoundingBox.Vertices[0].X}");

                            addBox = true;
                        }

                        if (addBox == true)
                        {
                            BoxPointsList.Add(new BoxPoints
                            {
                                X1 = paragraph.BoundingBox.Vertices[0].X,
                                Y1 = paragraph.BoundingBox.Vertices[0].Y,
                                X3 = paragraph.BoundingBox.Vertices[2].X,
                                Y3 = paragraph.BoundingBox.Vertices[3].Y
                            });
                        }
                    }
                }
            }

            Trace.WriteLine($"Dimension Count: {dimensionCount}");

            return dimensionCount;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //LoadBoundingBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\n\n{ex.StackTrace}");
            }
        }
        private void LoadBoundingBoxes()
        {
            GeometryGroup geometryGroup = new GeometryGroup();

            //0, 0, 500, 200, 100, 300, 500, 300
            //geometryGroup.Children.Add(CreateRectangle(2653, 1717, 2765, 1717, 2765, 1811, 2653, 300));

            //geometryGroup.Children.Add(CreateRectangle(2653, 1717, 2765, 1811));
            //geometryGroup.Children.Add(CreateRectangle(4109, 1868, 4366, 1908));

            foreach (var boundingBox in BoxPointsList)
            {
                geometryGroup.Children.Add(CreateRectangle(boundingBox.X1, boundingBox.Y1, boundingBox.X3, boundingBox.Y3));
            }

            BoxPointsList.Clear();

            System.Windows.Shapes.Path myPath = new System.Windows.Shapes.Path();

            myPath.Fill = Brushes.Transparent;
            myPath.Stroke = Brushes.Orange;
            myPath.StrokeThickness = 4;
            myPath.Data = geometryGroup;

            LoadedImage.Children.Clear();

            LoadedImage.Children.Add(myPath);
        }
        private RectangleGeometry CreateRectangle(double x1, double y1, double x3, double y3)
        {
            /**
                (x1, y1) ------------- (x2, y2)
                    |                      |
                    |                      |
                    |                      |
                (x4, y4) ------------- (x3, y3)
            **/
            RectangleGeometry rectangleGeometry = new RectangleGeometry();

            Rect rct = new Rect();

            rct.X = x1;
            rct.Y = y1;

            rct.Width = x3 - x1;
            rct.Height = y3 - y1;

            rectangleGeometry.Rect = rct;

            return rectangleGeometry;
        }
        private bool HasWriteAccessToFolder(string folderPath)
        {
            try
            {
                // Attempt to get a list of security permissions from the folder. 
                // This will raise an exception if the path is read only or do not have access to view the permissions. 
                System.Security.AccessControl.DirectorySecurity ds = (new DirectoryInfo(folderPath)).GetAccessControl(); ;
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }
        private void CountDimensionsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DimensionCountLabel.Content = CountDimensions(PrintImage);
                LoadBoundingBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void GetPrintButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                // \\s-fs1-smdrv\mydocs$\Joshua.Meservey\Scott Wolf\Read Drawings
                // C:\Users\Joshua.meservey\Pictures

                string initialFilePath = @"C:\Users\Joshua.meservey\Pictures"; // C:\Users\Joshua.meservey\Pictures // L:\Dept\QUOTING\quote

                if (!HasWriteAccessToFolder(initialFilePath))
                {
                    MessageBox.Show($"You do not have access to {initialFilePath}");
                }
                else
                {
                    openFileDialog.InitialDirectory = initialFilePath;
                }

                if (openFileDialog.ShowDialog() == true)
                {
                    var printFilePath = openFileDialog.FileName;

                    this.Title = $"{GetTitle()} - {printFilePath}";

                    DimensionCountLabel.Content = 0;

                    PrintImage = Image.FromFile(printFilePath);

                    LoadedImage.Children.Clear();

                    BitmapImage printBitmap = new BitmapImage();

                    printBitmap.BeginInit();
                    printBitmap.UriSource = new Uri(printFilePath);
                    printBitmap.EndInit();

                    CanvasGrid.Height = printBitmap.PixelHeight;
                    CanvasGrid.Width = printBitmap.PixelWidth;

                    CanvasBackground.ImageSource = printBitmap;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            // for .NET Core you need to add UseShellExecute = true
            // see https://docs.microsoft.com/dotnet/api/system.diagnostics.processstartinfo.useshellexecute#property-value            

            var psi = new ProcessStartInfo
            {
                FileName = e.Uri.AbsoluteUri,
                UseShellExecute = true
            };

            Process.Start(psi);

            e.Handled = true;
        }
    }
}
