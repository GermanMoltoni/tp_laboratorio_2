using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1
{
    class Numero
    {
        #region Atributos
        private double _numero;
        #endregion
        #region Constructores

        /// <summary>
        /// Incializa el atributo de instancia numero en 0.
        /// </summary>
        public Numero()
        {
            this._numero=0;
        }

        /// <summary>
        /// Incializa el atributo de instancia numero a partir de un numero.
        /// </summary>
        /// <param name="numero">recibe un double</param>
        public Numero(double numero)
        {
           this._numero=numero;
        }

        /// <summary>
        /// Incializa el atributo de instancia numero a partir de un string.
        /// </summary>
        /// <param name="numero">recibe un string como numero</param>
        public Numero(string numero)
        {
           SetNumero(numero);
        }

        #endregion

        #region Metodos

        #region Clase

        /// <summary>
        /// Valida que se haya ingresado un numero.
        /// </summary>
        /// <param name="numeroString">numero ingresado de tipo string</param>
        /// <returns>numero validado o  0 si no es correcto</returns>
        private static double ValidarNumero(string numeroString)
        {
            double numero;
            if (double.TryParse(numeroString, out numero))
                return numero;
            else
                return 0;
        }

        #endregion
 
        #region Instancia

        /// <summary>
        /// Obtiene el  atributo de instancia numero.
        /// </summary>
        /// <returns>numero obtenido</returns>
        public double GetNumero()
        {
            return this._numero;
        }

        /// <summary>
        /// Convierte y asigna un numero al atributo de instancia numero.
        /// </summary>
        /// <param name="numero">numero de tipo string ingresado</param>
        private void SetNumero(string numero)
        {
            this._numero = ValidarNumero(numero);
        }

        #endregion

        #endregion
    }
}
