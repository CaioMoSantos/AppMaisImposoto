using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppMaisImposoto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000; // 1 segundo
            timer1.Start();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void guna2TextBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            // Permite usar o backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;

                // Ignora a tecla pressionada
            }

        }

        private void guna2TextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite usar o backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;

                // Ignora a tecla pressionada
            }
        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {
            // Salva a posição atual do cursor
            int cursorPosition = guna2TextBox3.SelectionStart;

            // Remove tudo que não for número
            string raw = new string(guna2TextBox3.Text.Where(char.IsDigit).ToArray());

            // Aplica a máscara de CPF no guna textbox
            if (raw.Length > 11) raw = raw.Substring(0, 11); // Limita a 11 dígitos

            string masked = raw;

            if (raw.Length >= 3)
                masked = raw.Insert(3, ".");

            if (raw.Length >= 6)
                masked = masked.Insert(7, ".");

            if (raw.Length >= 9)
                masked = masked.Insert(11, "-");

            // Atualiza a caixa de texto apenas se a string mudar
            if (guna2TextBox3.Text != masked)
            {
                guna2TextBox3.Text = masked;
                guna2TextBox3.SelectionStart = cursorPosition + 1; // Reposiciona o cursor
            }
        }

        private void guna2TextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite apenas números e teclas de controle
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {
            // Remove eventos recursivos
            guna2TextBox4.TextChanged -= guna2TextBox4_TextChanged;

            string raw = new string(guna2TextBox4.Text.Where(char.IsDigit).ToArray());

            if (string.IsNullOrEmpty(raw))
            {
                guna2TextBox4.Text = "0,00";
            }
            else
            {
                // Converte para decimal e formata como moeda sem o "R$"
                decimal valor = Convert.ToDecimal(raw) / 100;
                guna2TextBox4.Text = valor.ToString("N2");
            }

            // Posiciona o cursor no final
            guna2TextBox4.SelectionStart = guna2TextBox4.Text.Length;

            guna2TextBox4.TextChanged += guna2TextBox4_TextChanged;
        }

        //Ações do calculo do imposto de renda
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //Verificando o valor do salário

            double renda, imposto = 0.0, aliquota, rendaFinal;
            double rendaOriginal;

            if (double.TryParse(guna2TextBox4.Text, out renda) && renda > 0.00)
            {
                // Cálculo do imposto 
                
                rendaOriginal = renda; //guardar a rende incial

                if (renda > 4664.68)
                {
                    imposto += (renda - 4664.68) * 0.275;
                    renda = 4664.68;
                }
                if (renda > 3751.05)
                {
                    imposto += (renda - 3751.05) * 0.225;
                    renda = 3751.05;
                }
                if (renda > 2826.65)
                {
                    imposto += (renda - 2826.65) * 0.15;
                    renda = 2826.65;
                }
                if (renda > 1903.98)
                {
                    imposto += (renda - 1903.98) * 0.075;
                }

                rendaFinal = rendaOriginal - imposto;

                // Cálculo porcentagem da alíquota 
                aliquota = (imposto / rendaOriginal) * 100;

                //adicionando os valores nas textbox

                if (imposto == 0)
                {
                    guna2TextBox5.Text = ("R$ " + rendaOriginal.ToString("F2"));
                    guna2TextBox6.Text = "Isento";
                    guna2TextBox7.Text = "0,00%";
                    guna2TextBox8.Text = ("R$ " + rendaFinal.ToString("F2"));
                }
                else
                {

                    guna2TextBox5.Text = ("R$ " + rendaOriginal.ToString("F2"));

                    guna2TextBox6.Text = ("R$ " + imposto.ToString("F2"));

                    guna2TextBox7.Text = (aliquota.ToString("F2") + "%");

                    guna2TextBox8.Text = ("R$ " + rendaFinal.ToString("F2"));
                }
            }
            else
            {
                MessageBox.Show("Favor digite um número válido!");

            }
            

        }

        private void limpar()
        {
            guna2TextBox1.Text = ("");

            guna2TextBox2.Text = ("");

            guna2TextBox3.Text = ("");

            guna2TextBox4.Text = ("");

            guna2TextBox1.PlaceholderText = ("Nome do funcionário");

            guna2TextBox2.PlaceholderText = ("Código do funcionário");

            guna2TextBox3.PlaceholderText = ("CPF");

            guna2TextBox4.PlaceholderText = ("Salário do funcionário (R$)");

            guna2TextBox5.Text = ("0000");

            guna2TextBox6.Text = ("0000");

            guna2TextBox7.Text = ("0000");

            guna2TextBox8.Text = ("0000");
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            limpar();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label6.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}
