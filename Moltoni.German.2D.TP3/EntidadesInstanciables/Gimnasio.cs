using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excepciones;
using Archivos;
namespace EntidadesInstanciables
{
    [Serializable]
    public class Gimnasio
    {
        #region Enumerado
        public enum EClases
        {
            Natacion,
            Pilates,
            Yoga,
            CrossFit
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Propiedad indexadora, devuelve la jornada solicitada de la lista de jornadas del Gimnasio.
        /// </summary>
        /// <param name="i">numero de jornada</param>
        /// <returns>Jornada solicitada</returns>
        public Jornada this[int i]
        {
            get { return this._jornada.ElementAt(i); }
        }
        #region Propiedades para Serializacion
        public List<Alumno> Alumnos
        {
            get { return this._alumnos; }
            set { this._alumnos = value; }
        }
        public List<Instructor> Instructores
        {
            get { return this._instructores; }
            set { this._instructores = value; }

        }
        public List<Jornada> Jornada
        {
            get { return this._jornada; }
            set { this._jornada = value; }
        }
        #endregion
        #endregion
        #region Atributos
        private List<Alumno> _alumnos;
        private List<Instructor> _instructores;
        private List<Jornada> _jornada;
        #endregion
        #region Constructores
        /// <summary>
        /// Inicicializa el gimnasio, creando listas de alumnos, instructores y jornadas
        /// </summary>
        public Gimnasio()
        {
            this._alumnos = new List<Alumno>();
            this._instructores = new List<Instructor>();
            this._jornada = new List<Jornada>();
        }
        #endregion
        #region Metodos
        /// <summary>
        /// Serializa  los datos de un Gimnasio.
        /// </summary>
        /// <param name="gim">gimnasio a serilizar</param>
        /// <returns>true si pudo serializar, ArchivosException si no pudo</returns>
        public static bool Guardar(Gimnasio gim)
        {
            Xml<Gimnasio> ser = new Xml<Gimnasio>();
            try
            {
                return  ser.guardar(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + '\\' + "Gim.Xml",gim);
            }
            catch (ArchivosException exc)
            {
                throw exc;
            }  
        }

        /// <summary>
        /// Desserializa un archivo que contiene un gimnasio
        /// </summary>
        /// <param name="path">Ruta del archivo </param>
        /// <param name="gim">gimnasio con datos</param>
        /// <returns>true si pudo desserializar, exception si no pudo</returns>
        public static bool Leer(string path,out Gimnasio gim)
        {
            Xml<Gimnasio> desSer = new Xml<Gimnasio>();
            try
            {
                desSer.leer(path, out gim);
                return true;
            }
            catch (ArchivosException exc)
            {
                throw exc;
            }

        }
        /// <summary>
        /// Muestra las Jornadas del Gimnasio.
        /// </summary>
        /// <param name="gim">gimnasio a mostrar</param>
        /// <returns>cadena con los datos de las jornadas</returns>
        private static string MostrarDatos(Gimnasio gim)
        {
            StringBuilder cadena = new StringBuilder();
            cadena.AppendLine("JORNADA:");
            foreach (Jornada j in gim._jornada)
            {
                cadena.AppendLine(j.ToString());
                cadena.AppendLine("<------------------------------>");
            }
            return cadena.ToString();
        }
        #region Override
        /// <summary>
        /// Hace posible obtener los datos de la instancia del gimnasio.
        /// </summary>
        /// <returns>cadena con los datos del gimnasio</returns>
        public override string ToString()
        {
            return Gimnasio.MostrarDatos(this);
        }
        #endregion
        #endregion
        #region Sobrecarga Operadores
        /// <summary>
        /// Comprueba si un alumno se encuentra inscripto en el gimnasio
        /// </summary>
        /// <param name="g">Objeto de tipo Gimnasio</param>
        /// <param name="a">Objeto de tipo Alumno</param>
        /// <returns>True si el alumno esta inscripto, False si no lo esta</returns>
        public static bool operator ==(Gimnasio g,Alumno a)
        {
            if (!object.Equals(g, null) && !object.Equals(a, null))
            {
                foreach(Alumno alum in g._alumnos)
                {
                    if (alum == a)
                        return true;
                }
            }
            return false;
        }
        public static bool operator !=(Gimnasio g, Alumno a)
        {
            return !(g == a);
        }
        /// <summary>
        /// Comprueba si un instructor se encuentra dando clases en el gimnasio
        /// </summary>
        /// <param name="g">Objeto de tipo Gimnasio</param>
        /// <param name="i">Objeto de tipo Instructor</param>
        /// <returns>True si el instructor da clase, False si no da</returns>
        public static bool operator ==(Gimnasio g, Instructor i)
        {
            if (!object.Equals(g, null) && !object.Equals(i, null))
            {
                foreach (Instructor ins in g._instructores)
                {
                    if (ins == i)
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Comprueba si un instructor no se encuentra dando clases en el gimnasio
        /// </summary>
        /// <param name="g">Objeto de tipo Gimnasio</param>
        /// <param name="i">Objeto de tipo Instructor</param>
        /// <returns>True si el instructor no da clase, False si da</returns>
        public static bool operator !=(Gimnasio g, Instructor i)
        {
            return !(g == i);
        }
        /// <summary>
        ///  Inscribe a un alumno en el gimnasio.
        ///  Si se encuentra repetido lanza excepcion
        /// </summary>
        /// <param name="g">Objeto de tipo Gimnasio</param>
        /// <param name="a">Objeto de tipo Alumno</param>
        /// <returns>gimnasio con alumno inscripto</returns>
        public static Gimnasio operator +(Gimnasio g, Alumno a)
        {
            if (!object.Equals(g, null) && !object.Equals(a, null))
            {
                if (g != a)
                    g._alumnos.Add(a);
                else
                    throw new AlumnoRepetidoException();
                return g;
            }
            return null;
        }
        /// <summary>
        /// Crea una Jornada y la agrega al gimnasio.
        /// El instructor debe dar esa clase, los alumnos deben tomar la misma.
        /// Si no se encuentra un instructor, se lanza una excepcion.
        /// </summary>
        /// <param name="g">Objeto de tipo Gimnasio</param>
        /// <param name="clase">clase de tipo Enum</param>
        /// <returns>Gimnacio con la jornada agregada</returns>
        public static Gimnasio operator +(Gimnasio g, EClases clase)
        {
            Jornada j = null;
            if(!object.Equals(g,null))
            {
                try
                {
                    j = new Jornada(clase, g == clase);
                    foreach (Alumno a in g._alumnos)
                    {
                        if (a == clase)
                            j += a;
                    }
                    g._jornada.Add(j);
                    return g;
                }
                catch (SinInstructorException sinIns)
                {
                    throw sinIns;
                }
            }
            return null;
        }


        /// <summary>
        /// Devuelve el primer instructor que de una clase.
        /// Si no lo hay lanza excepcion
        /// </summary>
        /// <param name="g">Objeto de tipo Gimnasio</param>
        /// <param name="clase">clase de tipo Enum</param>
        /// <returns>instructor si hay uno</returns>
        public static Instructor operator ==(Gimnasio g,EClases clase)
        {
            if(!object.Equals(g,null))
            {
                foreach (Instructor instructor in g._instructores)
                {
                    if (instructor == clase)
                        return instructor;
                }
                throw new SinInstructorException();
            }
            return null;
        }
        /// <summary>
        /// Devuelve el primer instructor que no de la clase.
        /// Si no  lo hay lanza excepcion
        /// </summary>
        /// <param name="g">Objeto de tipo Gimnasio</param>
        /// <param name="clase">clase de tipo Enum</param>
        /// <returns>instructor que no de la clase</returns>
        public static Instructor operator !=(Gimnasio g, EClases clase)
        {
            if (!object.Equals(g, null))
            {
                foreach (Instructor instructor in g._instructores)
                {
                    if (instructor != clase)
                        return instructor;
                }
                throw new SinInstructorException();
            }
            return null;
        }
        /// <summary>
        /// Agrega un instructor al gimnasio
        /// </summary>
        /// <param name="g">Objeto de tipo Gimnasio</param>
        /// <param name="i">Objeto de tipo Instructor</param>
        /// <returns>gimnasio con instructor agregado si no lo estaba</returns>
        public static Gimnasio operator +(Gimnasio g,Instructor i)
        {
            if (!object.Equals(g,null) && !object.Equals(i, null))
            {
                if (g != i)
                    g._instructores.Add(i);
                return g;
            }
            return null;
        }
        #endregion
        
    }
}
