using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Archivos;
using Excepciones;
namespace EntidadesInstanciables
{
    [Serializable]
    public class Jornada
    {
        #region Atributos
        private List<Alumno> _alumnos;
        private Gimnasio.EClases _clase;
        private Instructor _instructor;
        #endregion

        #region Propiedades de Serializacion
        public List<Alumno> Alumnos
        {
            get{return this._alumnos;}
            set { this._alumnos = value; }
        }
        public Gimnasio.EClases Clase
        {
            get{ return this._clase;}
            set { this._clase = value;}

        }
        public Instructor Instructor
        {
            get {return this._instructor;}
            set { this._instructor = value; }
        }
        #endregion
        #region Constructores
        /// <summary>
        /// Inicializa la lista de alumnos que contiene la jornada.
        /// </summary>
        private Jornada()
        {
            this._alumnos = new List<Alumno>();
        }
        /// <summary>
        /// Inicializa una jornada
        /// </summary>
        /// <param name="clase">Nombre de la clase de tipo Enum</param>
        /// <param name="instructor">instructor disponible para la jornada</param>
        public Jornada(Gimnasio.EClases clase,Instructor instructor):this()
        {
            this._clase = clase;
            this._instructor=instructor;
        }
        #endregion
        #region Sobrecarga Operadores
        /// <summary>
        /// Verifica si un alumno está inscripto en la jornada
        /// </summary>
        /// <param name="j">Objeto de tipo Jornada</param>
        /// <param name="a">Objeto de tipo alumno</param>
        /// <returns>true si esta inscripto, false si no lo esta</returns>
        public static bool operator ==(Jornada j, Alumno a)
        {
            if (!object.Equals(j, null) && !object.Equals(a, null))
            {
                foreach(Alumno alum in j._alumnos)
                {
                    if (alum == a)
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Verifica si un alumno no está inscripto en la jornada
        /// </summary>
        /// <param name="j">Objeto de tipo Jornada</param>
        /// <param name="a">Objeto de tipo alumno</param>
        /// <returns>true si no esta inscripto, false si lo esta</returns>
        public static bool operator !=(Jornada j, Alumno a)
        {
            return !(j == a);
        }
        /// <summary>
        /// Agrega un alumno a la jornada
        /// </summary>
        /// <param name="j">Objeto de tipo Jornada</param>
        /// <param name="a">Objeto de tipo alumno</param>
        /// <returns></returns>
        public static Jornada operator +(Jornada j, Alumno a)
        {
            if (!object.Equals(j, null)) 
            {
                if(!object.Equals(a, null) && j != a)
                    j._alumnos.Add(a);
                return j;
            }
            return null;
        }
        #endregion
        #region Metodos
        #region Override
        /// <summary>
        /// Hace posible obtener los datos de la instancia de la jornada
        /// </summary>
        /// <returns>cadena con los datos de la jornada</returns>
        public override string ToString()
        {
            StringBuilder cadena = new StringBuilder();
            cadena.AppendFormat("CLASE DE {0} ",this._clase.ToString());
            cadena.Append(this._instructor.ToString());
            cadena.AppendLine("ALUMNOS: ");
            foreach (Alumno a in this._alumnos)
                cadena.Append(a.ToString());
            return cadena.ToString();
        }
        #region
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
        #endregion
        /// <summary>
        /// Guarda como texto los datos de la jornada
        /// </summary>
        /// <param name="jornada">Jornada a guardar</param>
        /// <returns>true si pudo completar la operacion</returns>
        public static bool Guardar(Jornada jornada)
        {
            Texto cadena = new Texto();
            try
            {
                return cadena.guardar(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+'\\'+"Jornada.txt", jornada.ToString());
            }
            catch(ArchivosException exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Lee los datos del archivo de texto que contiene los datos de la jornada
        /// <param name="path">Es la ruta del archivo a leer</param>
        /// <param name="datos">Cadena con datos de jornada</param>
        /// <returns>true si pudo completar la operacion, ArchivosException si no pudo</returns>

        public static bool Leer(string path,out string datos)
        {
            Texto cadena = new Texto();
            try
            {
                cadena.leer(path, out datos);
                return true;
            }
            catch (ArchivosException exc)
            {
                throw exc;
            }
        }

        #endregion
    }
}
