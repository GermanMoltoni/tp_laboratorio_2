using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excepciones
{
    public class DniInvalidoException : Exception
    {
        private static string _mensajeBase = "El Dni ingresado no es valido";
        public DniInvalidoException():this(_mensajeBase)
        { }
        public DniInvalidoException(Exception e):this(_mensajeBase,e)
        { }
        public DniInvalidoException(string message):this(_mensajeBase + message, null)
        { }
        public DniInvalidoException(string message, Exception e):base(_mensajeBase + message,e)
        {  }
    }
}
