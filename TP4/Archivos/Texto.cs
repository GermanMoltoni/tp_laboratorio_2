using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Archivos
{
    public class Texto : IArchivo<string>
    {
        private string _archivo;
        
        /// <summary>
        /// Constructor de Texto
        /// </summary>
        /// <param name="archivo">path del archivo</param>
        public Texto(string archivo)
        {
            this._archivo = archivo;
        }
        /// <summary>
        /// Guarda un dato en un archivo de texto, si el archivo existe agrega datos,
        /// si el archivo no existe, lo crea.
        /// </summary>
        /// <param name="datos">dato a grabar</param>
        /// <returns>true si pudo grabar, excepcion si no pudo</returns>
        public bool guardar(string datos)
        {

            if (File.Exists(this._archivo))
            {
                try
                {
                    using (StreamWriter arch = new StreamWriter(this._archivo,true))
                        arch.WriteLine(datos);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            else
            {
                try
                {
                    using (StreamWriter arch = File.CreateText(this._archivo))
                        arch.WriteLine(datos);
                }
                catch (Exception e)
                {
                    throw e;
                }
           
            }
            return true;
        }
        /// <summary>
        /// Lee un archivo de texto
        /// </summary>
        /// <param name="datos">retorna una lista de string</param>
        /// <returns>true si pudo leer, excepcion si no pudo</returns>
        public bool leer(out List<string> datos)
        {
            string line = null;
            List<string> historial = new List<string>();
            try
            {
                using (StreamReader arch = new StreamReader(this._archivo))
                {
                    while ((line = arch.ReadLine()) != null)
                        historial.Add(line);
                }
                datos = historial;
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
