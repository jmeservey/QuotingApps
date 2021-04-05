using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace XMLDemoWinformsApp
{
    public partial class Form1 : Form
    {
        private string XmlFilePath = @"C:\Users\Joshua.meservey\Desktop\Product.xml";
        private string XmlFilePath2 = @"C:\Users\Joshua.meservey\Desktop\product (2).xml";
        private string XmlFilePathReport = @"\\s-fs1-smdrv\mydocs$\Joshua.Meservey\Scott Wolf\Quote Project\Report_923125346.xml";
        private string XmlFilePathReport2 = @"C:\Users\Joshua.meservey\AppData\Local\Temp\Report_11910545.xml";

        public Form1()
        {
            InitializeComponent();
        }

        private void ReadXMLNodeWise()
        {
            XmlDataDocument xmldoc = new XmlDataDocument();
            XmlNodeList xmlnode;
            int i = 0;
            string str = null;
            FileStream fs = new FileStream(XmlFilePath2, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            xmlnode = xmldoc.GetElementsByTagName("Product");
            for (i = 0; i <= xmlnode.Count - 1; i++)
            {
                xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                str = xmlnode[i].ChildNodes.Item(0).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(1).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(2).InnerText.Trim();
                MessageBox.Show(str);
            }
        }

        private void XMLReader()
        {
            int lineCount = 0;
            string line;
            XmlReader xmlReader = XmlReader.Create(XmlFilePathReport); // "http://www.ecb.int/stats/eurofxref/eurofxref-daily.xml"
            while (xmlReader.Read())
            {
                if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "Field"))
                {
                    if (xmlReader.HasAttributes)
                    {
                        line = "";
                        line = $"{xmlReader.GetAttribute("Name")}: ";

                        xmlReader.ReadToFollowing("Value");

                        line = $"{++lineCount}: {line} Value: {xmlReader.ReadElementContentAsString()}";

                        listBox1.Items.Add(line);

                        Console.WriteLine(line);
                    }
                        
                }
            }
        }
        private void XMLReader2()
        {
            XmlDocument xmldoc = new XmlDocument();

            FileStream fs = new FileStream(XmlFilePath2, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            XmlReader xmlReader = XmlReader.Create(new StringReader(xmldoc.ToString()));
            while (xmlReader.Read())
            {
                switch (xmlReader.NodeType)
                {
                    case XmlNodeType.Element:
                        listBox1.Items.Add("<" + xmlReader.Name + ">");
                        break;
                    case XmlNodeType.Text:
                        listBox1.Items.Add(xmlReader.Value);
                        break;
                    case XmlNodeType.EndElement:
                        listBox1.Items.Add("");
                        break;
                }
            }
        }
        private void XMLReaderLinq()
        {
            StringBuilder result = new StringBuilder();
            foreach (XElement level1Element in XElement.Load(XmlFilePath).Elements("Brand"))
            {
                result.AppendLine(level1Element.Attribute("name").Value);
                foreach (XElement level2Element in level1Element.Elements("product"))
                {
                    result.AppendLine("  " + level2Element.Attribute("name").Value);
                }
            }
            MessageBox.Show(result.ToString());
        }

        private void XMLReaderLinq2()
        {
            StringBuilder result = new StringBuilder();
            foreach (XElement level1Element in XElement.Load(XmlFilePathReport).Elements("CrystalReport"))
            {
                Console.WriteLine("Start:");
                Console.WriteLine($"{level1Element.Attribute("xmlns").Value} {level1Element.Attribute("Value").Value}");
                //result.AppendLine($"{level1Element.Attribute("name").Value} {level1Element.Attribute("Value").Value}");
                //foreach (XElement level2Element in level1Element.Elements("Value"))
                //{
                //    result.AppendLine("  " + level2Element.Attribute("name").Value);
                //}
            }
            MessageBox.Show(result.ToString());
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            try
            {
                XMLReader();
                //XMLReaderLinq2();

                //ReadXMLNodeWise();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace);
            }
        }
    }
}
