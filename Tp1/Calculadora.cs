using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Tp1
{
    class Calculadora
    {

        #region Metodos

        #region Clase
        /// <summary>
        /// Recibe los parametros para realizar la operacion elegida.
        /// </summary>
        /// <param name="numero1">operando 1 de tipo numero</param>
        /// <param name="numero2">operando 2 de tipo numero</param>
        /// <param name="operador">string de operador elegido </param>
        /// <returns>0 si se divide por 0 o no se ingresan numeros  o   valor de operacion</returns>
        public static double Operar(Numero numero1, Numero numero2, string operador)
        {
            double retorno=0;
            switch(operador)
            {
                case "/" :
                    if (numero2.GetNumero() != 0)
                        retorno= numero1.GetNumero() / numero2.GetNumero();
                    break;
                case "*":
                    retorno = numero1.GetNumero() * numero2.GetNumero();
                    break;
                case "+":
                    retorno = numero1.GetNumero() + numero2.GetNumero();
                    break;
                case "-":
                    retorno = numero1.GetNumero() - numero2.GetNumero();
                    break;
                default:
                    break;
            }
            return retorno;
        }

        /// <summary>
        /// Se verifica si el operador ingresado es correcto.
        /// </summary>
        /// <param name="operador">operador ingresado de tipo string</param>
        /// <returns>operador validado, si no se ingreso opreador o es invalido devuelve + </returns>
        public static string ValidarOperador(string operador)
        {
            switch (operador)
            {
                case "/":
                    break;
                case "*":
                    break;
                case "+":
                    break;
                case "-":
                    break;
                default:
                    operador = "+";
                    break;
            }
            return operador;
        }

        /// <summary>
        /// Borra contenido mostrado o ingresado en la calculadora.
        /// </summary>
        /// <param name="txtNumero1">Atributo de TextBox</param>
        /// <param name="txtNumero2">Atributo de tipo TextBox</param>
        /// <param name="cmbOperacion">Atributo de tipo ComboBox</param>
        /// <param name="lblResultado">Atributo de tipo Label</param>
        public static void Limpiar(TextBox txtNumero1, TextBox txtNumero2, ComboBox cmbOperacion, Label lblResultado)
        {
            txtNumero1.Text = " ";
            txtNumero2.Text = " ";
            lblResultado.Text = " ";
            cmbOperacion.Text = " ";
        }

        #endregion

        #endregion
        

    }
}
