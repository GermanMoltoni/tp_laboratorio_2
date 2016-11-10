using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using Excepciones;
namespace Archivos
{
    public class Xml<T> : IArchivo<T>
    {
        /// <summary>
        /// Implementacion para serializar datos genericos
        /// </summary>
        /// <param name="archivo">ruta del archivo a crear</param>
        /// <param name="datos">datos a serializar</param>
        /// <returns>true si serializo, lanza excepcion si no pudo</returns>
        public bool guardar(string archivo, T datos)
        {
            try
            {
                using (XmlTextWriter arch = new XmlTextWriter(archivo, System.Text.Encoding.UTF8))
                {
                    
                    XmlSerializer serializacion = new XmlSerializer(typeof(T));
                    serializacion.Serialize(arch,datos);
                }
            }
            catch (Exception e)
            {
                throw new ArchivosException(e);
            }
            return true;
        }

        /// <summary>
        /// Implementacion para desserializar un archivo
        /// </summary>
        /// <param name="archivo">ruta del archivo</param>
        /// <param name="datos">datos desserializados</param>
        /// <returns>true si pudo desserializar, excepcion si no pudo</returns>
        public bool leer(string archivo, out T datos)
        {

            try
            {
                using (XmlTextReader arch = new XmlTextReader(archivo))
                {

                    XmlSerializer serializacion = new XmlSerializer(typeof(T));
                    datos=(T)serializacion.Deserialize(arch);
                }
            }
            catch (Exception e)
            {
                throw new ArchivosException(e);
            }
            return true;
        }

    }
}