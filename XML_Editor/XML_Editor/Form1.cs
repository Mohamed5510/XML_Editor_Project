using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XML_Editor
{
    public partial class Form1 : Form
    {
        string xml_text;    // XML input

        // to store location and size of the form and controls
        private Rectangle textBox1Rect;
        private Rectangle textBox2Rect;
        private Rectangle button1Rect;
        private Rectangle button2Rect;
        private Rectangle button3Rect;
        private Rectangle button4Rect;
        private Rectangle button5Rect;
        private Rectangle button6Rect;
        private Rectangle button7Rect;
        private Rectangle button8Rect;
        private Rectangle button9Rect;
        private Size formSize;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            formSize = this.Size;
            textBox1Rect = new Rectangle(textBox1.Location.X, textBox1.Location.Y, textBox1.Size.Width, textBox1.Size.Height);
            textBox2Rect = new Rectangle(textBox2.Location.X, textBox2.Location.Y, textBox2.Size.Width, textBox2.Size.Height);
            button1Rect = new Rectangle(button1.Location.X, button1.Location.Y, button1.Size.Width, button1.Size.Height);
            button2Rect = new Rectangle(button2.Location.X, button2.Location.Y, button2.Size.Width, button2.Size.Height);
            button3Rect = new Rectangle(button3.Location.X, button3.Location.Y, button3.Size.Width, button3.Size.Height);
            button4Rect = new Rectangle(button4.Location.X, button4.Location.Y, button4.Size.Width, button4.Size.Height);
            button5Rect = new Rectangle(button5.Location.X, button5.Location.Y, button5.Size.Width, button5.Size.Height);
            button6Rect = new Rectangle(button6.Location.X, button6.Location.Y, button6.Size.Width, button6.Size.Height);
            button7Rect = new Rectangle(button7.Location.X, button7.Location.Y, button7.Size.Width, button7.Size.Height);
            button8Rect = new Rectangle(button8.Location.X, button8.Location.Y, button8.Size.Width, button8.Size.Height);
            button9Rect = new Rectangle(button9.Location.X, button9.Location.Y, button9.Size.Width, button9.Size.Height);
        }

        // function to dynamically resize specific control according to resizing the form
        private void resize_control(Rectangle rec, Control ctrl)
        {
            float xRatio = this.Width / (float)(formSize.Width);
            float yRatio = this.Height / (float)(formSize.Height);
            int newX = (int)(rec.X * xRatio);
            int newY = (int)(rec.Y * yRatio);
            int newWidth = (int)(rec.Width * xRatio);
            int newHeight = (int)(rec.Height * yRatio);
            ctrl.Location = new Point(newX, newY);
            ctrl.Size = new Size(newWidth, newHeight);
        }

        // function to resize all used controls
        private void resize_controls()
        {
            // we have 2 text boxes and 11 buttond in the application
            resize_control(textBox1Rect, textBox1);
            resize_control(textBox2Rect, textBox2);
            resize_control(button1Rect, button1);
            resize_control(button2Rect, button2);
            resize_control(button3Rect, button3);
            resize_control(button4Rect, button4);
            resize_control(button5Rect, button5);
            resize_control(button6Rect, button6);
            resize_control(button7Rect, button7);
            resize_control(button8Rect, button8);
            resize_control(button9Rect, button9);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            resize_controls();
        }

        // Browse Button
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Import File";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string d = dialog.FileName;
                try
                {
                    xml_text = File.ReadAllText(d);
                    textBox1.Text = xml_text;
                }
                catch (IOException)
                {
                }
            }
        }

        // check Button
        private void button2_Click(object sender, EventArgs e)
        {
            bool isCorrect = consistency.checkConsistency(textBox1.Text);
            if (isCorrect)
            {
                MessageBox.Show("The file is correct");
            }
            else
                MessageBox.Show("The file is incorrect");
        }

        // correct Button
        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = consistency.correct(textBox1.Text);
            textBox2.Text = textBox2.Text.Replace("\t", "    ");
            textBox2.Text = textBox2.Text.Replace("\r\n", "\n").Replace("\n", Environment.NewLine);
        }

        // JSON Button
        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Text = json.XMLToJson(0, TreeParsing.ParseToTree(textBox1.Text));
            textBox2.Text = textBox2.Text.Replace("\t", "    ");
            textBox2.Text = textBox2.Text.Replace("\r\n", "\n").Replace("\n", Environment.NewLine);
        }

        // compress Button
        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "bin files (*.bin)|*.bin";
            dialog.Title = "Save Project";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.FileName != "")
                {
                    HuffmanTree t = new HuffmanTree();
                    BitArray arr = new BitArray(textBox1.Text.Length);
                    arr = t.Encode(textBox1.Text);
                    byte[] b = new byte[(arr.Length - 1) / 8 + 1];
                    arr.CopyTo(b, 0);
                    using (BinaryWriter w = new BinaryWriter(File.Create(dialog.FileName)))
                    {
                        w.Write(b);
                    }
                }
            }
        }

        // decompress Button
        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Import File";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                List<byte> b = new List<byte>();
                using (BinaryReader r = new BinaryReader(File.Open(dialog.FileName, FileMode.Open)))
                {
                    while(r.BaseStream.Position != r.BaseStream.Length)
                    {
                        b.Add(r.ReadByte());
                    }
                }
                BitArray arr = new BitArray(b.ToArray());
                HuffmanTree t = new HuffmanTree();
                textBox2.Text = t.Decode(arr);
            }
        }

        // allign Button
        private void button7_Click(object sender, EventArgs e)
        {
            textBox2.Text = Formatting.format(textBox1.Text);
            textBox2.Text = textBox2.Text.Replace("\t", "    ");
            textBox2.Text = textBox2.Text.Replace("\r\n", "\n").Replace("\n", Environment.NewLine);
        }

        // Save Button
        private void button8_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*|xml file (*.xml)|*.xml";
            dialog.Title = "Save Project";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.FileName != "")
                {
                    TextWriter txt = new StreamWriter(dialog.FileName);
                    txt.Write(textBox2.Text);
                    txt.Close();
                }
            }

        }

        // visualize Button
        private void button9_Click(object sender, EventArgs e)
        {
            Node root = TreeParsing.ParseToTree(textBox1.Text);
            Node r = root.getChildren()[0];
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            foreach (Node user in r.getChildren())
            {
                string userId = "";
                foreach (Node property in user.getChildren())
                {
                    if (property.getTagName() == "id")
                    {
                        userId = property.getTagData();
                        break;
                    }
                }
                foreach (Node property in user.getChildren())
                {
                    if (property.getTagName() == "followers")
                    {
                        foreach (Node follower in property.getChildren())
                        {
                            graph.AddEdge(userId, follower.getChildren()[0].getTagData());
                        }
                    }
                }
            }
            viewer.Graph = graph;
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();
            form.ShowDialog();
        }
    }
}
