using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using WinFormsTextBox = System.Windows.Forms.TextBox;

namespace INTEL8086Symulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeRegisterLists();

            AH.KeyPress += textBoxHex_KeyPress;
            AL.KeyPress += textBoxHex_KeyPress;
            BH.KeyPress += textBoxHex_KeyPress;
            BL.KeyPress += textBoxHex_KeyPress;
            CH.KeyPress += textBoxHex_KeyPress;
            CL.KeyPress += textBoxHex_KeyPress;
            DH.KeyPress += textBoxHex_KeyPress;
            DL.KeyPress += textBoxHex_KeyPress;

        }

        private void InitializeRegisterLists()
        {
            List<string> registers = new List<string> { "AH", "BH", "CH", "DH", "AL", "BL", "CL", "DL" };

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            foreach (var register in registers)
            {
                comboBox1.Items.Add(register);
                comboBox2.Items.Add(register);
            }
        }

        private void textBoxHex_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= '0' && e.KeyChar <= '9') ||
      (e.KeyChar >= 'A' && e.KeyChar <= 'F') ||
      (e.KeyChar >= 'a' && e.KeyChar <= 'f') ||
      e.KeyChar == '\b'))
            {
                e.Handled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }


        //high
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        //low
        private void textBox8_TextChanged(object sender, EventArgs e) { }
        private void textBox5_TextChanged(object sender, EventArgs e) { }
        private void textBox6_TextChanged(object sender, EventArgs e) { }
        private void textBox7_TextChanged(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null)
            {
                string sourceRegister = comboBox1.SelectedItem.ToString();
                string targetRegister = comboBox2.SelectedItem.ToString();

                System.Windows.Forms.TextBox sourceTextBox = null;
                System.Windows.Forms.TextBox targetTextBox = null;

                switch (sourceRegister)
                {
                    case "AH":
                        sourceTextBox = AH;
                        break;
                    case "BH":
                        sourceTextBox = BH;
                        break;
                    case "CH":
                        sourceTextBox = CH;
                        break;
                    case "DH":
                        sourceTextBox = DH;
                        break;
                    case "AL":
                        sourceTextBox = AL;
                        break;
                    case "BL":
                        sourceTextBox = BL;
                        break;
                    case "CL":
                        sourceTextBox = CL;
                        break;
                    case "DL":
                        sourceTextBox = DL;
                        break;
                }

                switch (targetRegister)
                {
                    case "AH":
                        targetTextBox = AH;
                        break;
                    case "BH":
                        targetTextBox = BH;
                        break;
                    case "CH":
                        targetTextBox = CH;
                        break;
                    case "DH":
                        targetTextBox = DH;
                        break;
                    case "AL":
                        targetTextBox = AL;
                        break;
                    case "BL":
                        targetTextBox = BL;
                        break;
                    case "CL":
                        targetTextBox = CL;
                        break;
                    case "DL":
                        targetTextBox = DL;
                        break;
                }

                if (sourceTextBox != null && targetTextBox != null)
                {
                    targetTextBox.Text = sourceTextBox.Text;
                    sourceTextBox.Text = "";
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null) return;

            string selectedRegister1 = comboBox1.SelectedItem.ToString();
            string selectedRegister2 = comboBox2.SelectedItem.ToString();

            System.Windows.Forms.TextBox textBox1 = GetTextBoxByName(selectedRegister1);
            System.Windows.Forms.TextBox textBox2 = GetTextBoxByName(selectedRegister2);

            if (textBox1 == null || textBox2 == null) return;

            string temp = textBox1.Text;
            textBox1.Text = textBox2.Text;
            textBox2.Text = temp;
        }

        private System.Windows.Forms.TextBox GetTextBoxByName(string registerName)
        {
            switch (registerName)
            {
                case "AH": return AH;
                case "AL": return AL;
                case "BH": return BH;
                case "BL": return BL;
                case "CH": return CH;
                case "CL": return CL;
                case "DH": return DH;
                case "DL": return DL;
                default: return null;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<System.Windows.Forms.TextBox> textBoxes = new List<System.Windows.Forms.TextBox> { AH, AL, BH, BL, CH, CL, DH, DL };

            foreach (var textBox in textBoxes)
            {
                if (!string.IsNullOrWhiteSpace(textBox.Text) && IsHex(textBox.Text))
                {
                    try
                    {
                        int value = Convert.ToInt32(textBox.Text, 16);
                        int invertedValue = ~value & 0xFF;
                        textBox.Text = invertedValue.ToString("X2");
                    }
                    catch (System.FormatException)
                    {
                    }
                }
            }
        }

        private bool IsHex(string input)
        {
            return input.All(c => (c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f'));
        }



        private string InvertBinaryString(string binaryString)
        {
            return new string(binaryString.Select(c => c == '0' ? '1' : '0').ToArray());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<System.Windows.Forms.TextBox> textBoxes = new List<System.Windows.Forms.TextBox> { AH, AL, BH, BL, CH, CL, DH, DL };

            foreach (var textBox in textBoxes)
            {
                if (!string.IsNullOrWhiteSpace(textBox.Text) && IsHex(textBox.Text))
                {
                    try
                    {
                        int value = Convert.ToInt32(textBox.Text, 16);
                        int negatedValue = (0xFF - value + 1) & 0xFF; textBox.Text = negatedValue.ToString("X2");
                    }
                    catch (System.FormatException)
                    {
                    }
                }
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            List<System.Windows.Forms.TextBox> textBoxes = new List<System.Windows.Forms.TextBox> { AH, AL, BH, BL, CH, CL, DH, DL };

            foreach (var textBox in textBoxes)
            {
                textBox.Text = String.Empty;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(AH.Text))
            {
                if (int.TryParse(AH.Text, System.Globalization.NumberStyles.HexNumber, null, out int value))
                {
                    value += 1;

                    AH.Text = value.ToString("X2");
                }
                else
                {
                    MessageBox.Show("Wartość w AH nie jest poprawną liczbą szesnastkową.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("TextBox AH jest pusty.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void button6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(AH.Text))
            {
                if (int.TryParse(AH.Text, System.Globalization.NumberStyles.HexNumber, null, out int value))
                {
                    value -= 1;

                    value = Math.Max(0, value);

                    AH.Text = value.ToString("X2");
                }
                else
                {
                    MessageBox.Show("Wartość w AH nie jest poprawną liczbą szesnastkową.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("TextBox AH jest pusty.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(BH.Text))
            {
                if (int.TryParse(BH.Text, System.Globalization.NumberStyles.HexNumber, null, out int value))
                {
                    value += 1;

                    BH.Text = value.ToString("X2");
                }
                else
                {
                    MessageBox.Show("Wartość w BH nie jest poprawną liczbą szesnastkową.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("TextBox BH jest pusty.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(BH.Text))
            {
                if (int.TryParse(BH.Text, System.Globalization.NumberStyles.HexNumber, null, out int value))
                {
                    value -= 1;

                    value = Math.Max(0, value);

                    BH.Text = value.ToString("X2");
                }
                else
                {
                    MessageBox.Show("Wartość w BH nie jest poprawną liczbą szesnastkową.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("TextBox BH jest pusty.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CH.Text))
            {
                if (int.TryParse(CH.Text, System.Globalization.NumberStyles.HexNumber, null, out int value))
                {
                    value += 1;

                    CH.Text = value.ToString("X2");
                }
                else
                {
                    MessageBox.Show("Wartość w CH nie jest poprawną liczbą szesnastkową.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("TextBox CH jest pusty.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CH.Text))
            {
                if (int.TryParse(CH.Text, System.Globalization.NumberStyles.HexNumber, null, out int value))
                {
                    value -= 1;

                    value = Math.Max(0, value);

                    CH.Text = value.ToString("X2");
                }
                else
                {
                    MessageBox.Show("Wartość w CH nie jest poprawną liczbą szesnastkową.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("TextBox CH jest pusty.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DH.Text))
            {
                if (int.TryParse(DH.Text, System.Globalization.NumberStyles.HexNumber, null, out int value))
                {
                    value += 1;

                    DH.Text = value.ToString("X2");
                }
                else
                {
                    MessageBox.Show("Wartość w DH nie jest poprawną liczbą szesnastkową.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("TextBox CH jest pusty.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DH.Text))
            {
                if (int.TryParse(DH.Text, System.Globalization.NumberStyles.HexNumber, null, out int value))
                {
                    value -= 1;

                    value = Math.Max(0, value);

                    DH.Text = value.ToString("X2");
                }
                else
                {
                    MessageBox.Show("Wartość w DH nie jest poprawną liczbą szesnastkową.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("TextBox DH jest pusty.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(AL.Text))
            {
                if (int.TryParse(AL.Text, System.Globalization.NumberStyles.HexNumber, null, out int value))
                {
                    value += 1;

                    AL.Text = value.ToString("X2");
                }
                else
                {
                    MessageBox.Show("Wartość w AL nie jest poprawną liczbą szesnastkową.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("TextBox AL jest pusty.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(AL.Text))
            {
                if (int.TryParse(AL.Text, System.Globalization.NumberStyles.HexNumber, null, out int value))
                {
                    value -= 1;

                    value = Math.Max(0, value);

                    AL.Text = value.ToString("X2");
                }
                else
                {
                    MessageBox.Show("Wartość w AL nie jest poprawną liczbą szesnastkową.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("TextBox AL jest pusty.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(BL.Text))
            {
                if (int.TryParse(BL.Text, System.Globalization.NumberStyles.HexNumber, null, out int value))
                {
                    value += 1;

                    BL.Text = value.ToString("X2");
                }
                else
                {
                    MessageBox.Show("Wartość w BL nie jest poprawną liczbą szesnastkową.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("TextBox BL jest pusty.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(BL.Text))
            {
                if (int.TryParse(BL.Text, System.Globalization.NumberStyles.HexNumber, null, out int vBLue))
                {
                    vBLue -= 1;

                    vBLue = Math.Max(0, vBLue);

                    BL.Text = vBLue.ToString("X2");
                }
                else
                {
                    MessageBox.Show("Wartość w BL nie jest poprawną liczbą szesnastkową.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("TextBox BL jest pusty.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CL.Text))
            {
                if (int.TryParse(CL.Text, System.Globalization.NumberStyles.HexNumber, null, out int value))
                {
                    value += 1;

                    CL.Text = value.ToString("X2");
                }
                else
                {
                    MessageBox.Show("Wartość w CL nie jest poprawną liczbą szesnastkową.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("TextBox CL jest pusty.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CL.Text))
            {
                if (int.TryParse(CL.Text, System.Globalization.NumberStyles.HexNumber, null, out int vCLue))
                {
                    vCLue -= 1;

                    vCLue = Math.Max(0, vCLue);

                    CL.Text = vCLue.ToString("X2");
                }
                else
                {
                    MessageBox.Show("Wartość w CL nie jest poprawną liczbą szesnastkową.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("TextBox CL jest pusty.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DL.Text))
            {
                if (int.TryParse(DL.Text, System.Globalization.NumberStyles.HexNumber, null, out int value))
                {
                    value += 1;

                    DL.Text = value.ToString("X2");
                }
                else
                {
                    MessageBox.Show("Wartość w DL nie jest poprawną liczbą szesnastkową.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("TextBox DL jest pusty.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DL.Text))
            {
                if (int.TryParse(DL.Text, System.Globalization.NumberStyles.HexNumber, null, out int vDLue))
                {
                    vDLue -= 1;

                    vDLue = Math.Max(0, vDLue);

                    DL.Text = vDLue.ToString("X2");
                }
                else
                {
                    MessageBox.Show("Wartość w DL nie jest poprawną liczbą szesnastkową.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("TextBox DL jest pusty.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
