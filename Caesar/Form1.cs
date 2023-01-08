using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Caesar
{
    public partial class Form1 : Form
    {
        string text;
        int shift = 0;
        bool randomize = true;
        bool fromFile = false;
        string docPath = Directory.GetCurrentDirectory() + @"\";
        string fileName = "file.txt";
        string[] words;
        string[] ENwords = { "love", "word", "lorem", "have", "from", "ipsum", "any", "none",
            "from", "you", "they", "their", "them", "with", "that", "this", "would", "make", "take",
            "pick", "about", "know", "time", "there", "year", "when", "which", "some", "people", "into",
            "him", "your", "could", "these", "want", "day", "because", "more", "look", "thing", "very", "lol",
            "be", "of", "the", "and", "he", "she", "it", "we", "our", "but", "must", "hello", "hi" };
        string[] HUwords = { "szia", "és", "vagy", "vagyok", "te", "köszi", "meg", "volt", "még", "már",
            "kell", "mint", "az", "akkor", "sem", "lehet", "minden", "mert", "lehet", "nem", "olyan",
            "szerint", "pedig", "ez", "így", "után", "úgy", "nagy", "fel", "majd", "két", "nagyon",
            "aki", "most", "több", "lesz", "itt", "ami", "között", "hanem", "nincs", "más", "illetve" };
        string[] abc = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k",
            "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", " ", ",", "'",
            "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "/", "@", "#", "!", "-", "=", "+",
            "*", "$", "%", "^", "&", "(", ")", "|", "{", "}", "[", "]", "ö", "ü", "ó", "ő", "ú", "é",
            "á", "ű", "í", "<", ">", "?", "." };
        public Form1()
        {
            InitializeComponent();
            Init();
        }
        void Init()
        {
            if (comboBox1.Text == "EN")
            {
                words = ENwords;
            }
            else if (comboBox1.Text == "HU")
            {
                words = HUwords;
            }
            /*for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                if (comboBox1.Text == comboBox1.Items[i].ToString())
                {
                    words = //how to assign variable with only knowing a string of the variable name
                }
            }*/
            #region textbox
            textBox3.ReadOnly = true;
            textBox3.BorderStyle = 0;
            textBox3.BackColor = this.BackColor;
            textBox3.TabStop = false;
            //
            textBox6.ReadOnly = true;
            textBox6.BorderStyle = 0;
            textBox6.BackColor = this.BackColor;
            textBox6.TabStop = false;

            textBox2.Text = "0";
            textBox5.Text = "0";
            #endregion
            if (fromFile == true)
            {
                int i = 0;
                foreach (char c in System.IO.File.ReadAllText(docPath + fileName))
                {
                    if (i < abc.Length)
                    {
                        abc[i] = c.ToString();
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else if (randomize == true)
            {
                abc = Randomize(abc);
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, fileName)))
                {
                    outputFile.WriteLine(String.Join("", abc));
                }
            }
            WordsFormatChange();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "EN")
            {
                words = ENwords;
            }
            else if (comboBox1.Text == "HU")
            {
                words = HUwords;
            }
            /*for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                if (comboBox1.Text == comboBox1.Items[i].ToString())
                {
                    words = //how to assign variable with only knowing a string of the variable name
                }
            }*/
            WordsFormatChange();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "0")
            {
                shift = 0;
            }
            text = textBox1.Text;
            text = text.ToLower();
            textBox3.Text = CaesarCode(text, shift);
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (Int32.TryParse(textBox2.Text, out shift))
            {
                shift = Int32.Parse(textBox2.Text);
            }
        }
        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox5.Text == "0")
            {
                shift = 0;
            }
            text = textBox4.Text;
            text = text.ToLower();
            if (checkBox1.Checked == true)
            {
                textBox6.Text = CaesarDecodeWithoutShiftValue(text);
            }
            else
            {
                textBox6.Text = CaesarDecode(text, shift);
            }
        }
        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {
            if (Int32.TryParse(textBox5.Text, out shift))
            {
                shift = Int32.Parse(textBox5.Text);
            }
        }
        string CaesarCode(string texttocode, int letshift)
        {
            int i = 0;
            string[] codedText = new string[texttocode.Length];
            foreach (char c in texttocode)
            {
                string current;
                int index = Array.IndexOf(abc, c.ToString());
                if (index + letshift > abc.Length - 1)
                {
                    index -= abc.Length;
                }
                current = abc[index + letshift];
                codedText[i] = current;
                i++;
            }
            return string.Join("", codedText);
        }
        string CaesarDecode(string texttodecode, int letshift)
        {
            int i = 0;
            string[] decodedText = new string[texttodecode.Length];
            foreach (char c in texttodecode)
            {
                string current;
                int index = Array.IndexOf(abc, c.ToString());
                if (index - letshift < 0)
                {
                    index += abc.Length;
                }
                current = abc[index - letshift];
                decodedText[i] = current;
                i++;
            }
            return string.Join("", decodedText);
        }
        string CaesarDecodeWithoutShiftValue(string text)
        {
            for (int i = 0; i < abc.Length; i++)
            {
                for (int j = 0; j < words.Length; j++)
                {
                    if (CaesarDecode(text, i).Contains(words[j]))
                    {
                        textBox5.Text = i.ToString();
                        return CaesarDecode(text, i);
                    }
                }
            }
            return null;
        }
        public string[] Randomize(string[] input)
        {
            List<string> inputList = input.ToList();
            string[] output = new string[input.Length];
            Random r = new Random();
            int i = 0;

            while (inputList.Count > 0)
            {
                int index = r.Next(inputList.Count);
                output[i++] = inputList[index];
                inputList.RemoveAt(index);
            }
            return (output);
        }
        public void WordsFormatChange()
        {
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = " " + words[i].ToLower() + " ";
            }
        }
    }
}
