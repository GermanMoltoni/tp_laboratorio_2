using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Excepciones;
namespace Archivos
{
    public class Texto : IArchivo<string>
    {
        #region Implementacion Interfaz IArchivo<string>
        /// <summary>
        /// Implementación para crear un archivo de texto con los datos de la Jornada
        /// </summary>
        /// <param name="archivo">nombre del archivo a crear</param>
        /// <param name="datos">datos a grabar </param>
        /// <returns>true si lo creó correctamente, false si no pudo crear</returns>
        public bool guardar(string archivo, string datos)
        {
            try
            {
                using (StreamWriter arch = new StreamWriter(archivo))
                    arch.WriteLine(datos);
            }
            catch (Exception e)
            {
                throw new ArchivosException(e);
            }
            return true;
        }

        /// <summary>
        /// Implementación para leer un archivo de texto 
        /// </summary>
        /// <param name="archivo">nombre del archivo a leer</param>
        /// <param name="datos">salida de datos leidos </param>
        /// <returns>true si leyo correctamente, false si no pudo leer</returns>
       public bool leer(string archivo, out string datos)
        {
            string line = null;
            StringBuilder cadena = new StringBuilder();
            try
            {
                using (StreamReader arch = new StreamReader(archivo))
                {
                    while ((line = arch.ReadLine()) != null)
                        cadena.AppendLine(line);
                }
                datos = cadena.ToString();
                return true;
            }
            catch (Exception e)
            {
                throw new ArchivosException(e);
            }
        }
        #endregion
    }

}
