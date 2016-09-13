using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOperar_Click(object sender, EventArgs e)
        {
            // Creo objeto de tipo Numero recibiendo el string de txtNumero1.
            Numero numero1 = new Numero(txtNumero1.Text);
            // Creo objeto de tipo Numero recibiendo el string de txtNumero2.
            Numero numero2 = new Numero(txtNumero2.Text);
            // Tomo el string que contiene la operacion de cmbOperacion y lo valido.
            string operador = Calculadora.ValidarOperador(cmbOperacion.Text);
            // Muestro el string validado.
            cmbOperacion.Text = operador;
            //Realizo la operacion con el metodo Operar de Calculadora, el resultado lo convierto a string y lo muestro en lblResultado.
            lblResultado.Text = Convert.ToString(Calculadora.Operar(numero1, numero2, operador));
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Calculadora.Limpiar(txtNumero1, txtNumero2, cmbOperacion, lblResultado);
        }

    }
}
