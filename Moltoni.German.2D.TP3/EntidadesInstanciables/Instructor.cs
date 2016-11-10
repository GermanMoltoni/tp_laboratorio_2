using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesAbstractas;
namespace EntidadesInstanciables
{
    [Serializable]
    public sealed class Instructor:PersonaGimnasio
    {
        #region Atributos
        private Queue<Gimnasio.EClases> _clasesDelDia;
        private static Random _random;
        #endregion

        #region Constructores
        /// <summary>
        /// Constructor por default Instructor, necesario para serializar
        /// </summary>
        public Instructor() : base()
        { }
        /// <summary>
        /// Inicia el atributo de clase.
        /// </summary>
        static Instructor()
        {
            _random = new Random();
        }
        /// <summary>
        /// Inicia un Instructor y se le asignan las clases que dará. Pasa parametros necesarios a clase base.
        /// </summary>
        /// <param name="id">identificador unico de Instructor</param>
        /// <param name="nombre">nombre de tipo string</param>
        /// <param name="apellido">apellido de tipo string</param>
        /// <param name="dni">dni de tipo int</param>
        /// <param name="nacionalidad">nacionalidad de tipo Enum</param>
        public Instructor(int id, string nombre, string apellido, string dni, ENacionalidad nacionalidad) : base(id, nombre, apellido, dni, nacionalidad)
        {
            this._clasesDelDia = new Queue<Gimnasio.EClases>();
            this._randomClases();
        }
        #endregion
        #region Metodos
        #region Override
        /// <summary>
        /// Obtengo las clases que da el instructor
        /// </summary>
        /// <returns>cadena con la clase que enseña el instructor</returns>
        protected override string ParticiparEnClase()
        {
            StringBuilder cadena = new StringBuilder();
            if (!object.Equals(this._clasesDelDia, null))
            {
                cadena.AppendLine("CLASES DEL DIA:");
                for (int i = 0; i < this._clasesDelDia.Count(); i++)
                    cadena.AppendLine(this._clasesDelDia.ElementAt(i).ToString());          
            }
            return cadena.ToString();
            
        }
        /// <summary>
        /// Implementacion de metodo de clase base que crea una cadena que contiene los datos del Instructor.
        /// </summary>
        /// <returns>cadena con los datos del Instructor</returns>
        protected override string MostrarDatos()
        {
            StringBuilder cadena = new StringBuilder();
            cadena.AppendLine("POR "+base.MostrarDatos());
            cadena.AppendLine(this.ParticiparEnClase());
            return cadena.ToString();
        }
        /// <summary>
        /// Hace posible obtener los datos de la instancia del Instructor.
        /// </summary>
        /// <returns>cadena con los datos del instructor</returns>
        public override string ToString()
        {
            return this.MostrarDatos();
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
        /// Asigna dos clases aleatorias al instructor
        /// </summary>
        private void _randomClases()
        {
            for(int i=0; i<2;i++)
                this._clasesDelDia.Enqueue((Gimnasio.EClases)_random.Next(4));
        }
        #endregion
        #region SobrecargaOperadores
        /// <summary>
        /// Verifica si el instructor da una clase.
        /// </summary>
        /// <param name="i">Objeto de tipo Instructor</param>
        /// <param name="clase">clase de tipo Enum</param>
        /// <returns>true si la da, false si no la da.</returns>
        public static bool operator ==(Instructor i, Gimnasio.EClases clase)
        {
            if (!object.Equals(i, null) && !object.Equals(clase, null))
                return i._clasesDelDia.Contains(clase);
            return false;
        }
        /// <summary>
        /// Verifica si el instructor no da una clase.
        /// </summary>
        /// <param name="i">Objeto de tipo Instructor</param>
        /// <param name="clase">clase de tipo Enum</param>
        /// <returns>true no la da, false si  la da.</returns>
        public static bool operator !=(Instructor i, Gimnasio.EClases clase)
        {
            return !(i == clase);
        }
        #endregion
    }
}
