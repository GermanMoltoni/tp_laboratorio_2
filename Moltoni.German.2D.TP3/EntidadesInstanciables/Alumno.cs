using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesAbstractas;
namespace EntidadesInstanciables
{
    [Serializable]
    public sealed class Alumno : PersonaGimnasio
    {
        #region Atributos
        private Gimnasio.EClases _claseQueToma;
        private EEstadoCuenta _estadoCuenta;
        #endregion
        #region Constructores
        /// <summary>
        /// Constructor por default de Alumno, necesario para serializar
        /// </summary>
        public Alumno()
            : base()
        {

        }
        /// <summary>
        /// Inicia un Alumno con sus datos,establece la clase que toma . Pasa valores correspondientes a constructor de clase base
        /// </summary>
        /// <param name="id">identificador unico de Alumno</param>
        /// <param name="nombre">nombre de tipo string</param>
        /// <param name="apellido">apellido de tipo string</param>
        /// <param name="dni">dni de tipo int</param>
        /// <param name="nacionalidad">nacionalidad de tipo Enum</param>
        /// <param name="claseQueToma">clase que toma  de tipo Enum</param>
        public Alumno(int id,string nombre,string apellido,string dni,ENacionalidad nacionalidad, Gimnasio.EClases claseQueToma):base(id,nombre,apellido,dni,nacionalidad)
        {
            this._claseQueToma = claseQueToma;
        }
        /// <summary>
        /// Inicia un Alumno con sus datos, establece el estado de cuenta. Pasa valores correspondientes a constructor de Alumno.
        /// </summary>
        /// <param name="id">identificador unico de Alumno</param>
        /// <param name="nombre">nombre de tipo string</param>
        /// <param name="apellido">apellido de tipo string</param>
        /// <param name="dni">dni de tipo int</param>
        /// <param name="nacionalidad">nacionalidad de tipo Enum</param>
        /// <param name="claseQueToma">clase que toma  de tipo Enum</param>
        /// <param name="estadoCuenta">estado de cuenta de tipo EEstadoCuenta</param>
        public Alumno(int id, string nombre, string apellido, string dni, ENacionalidad nacionalidad, Gimnasio.EClases claseQueToma,EEstadoCuenta estadoCuenta):this(id,nombre,apellido,dni,nacionalidad,claseQueToma)
        {
            this._estadoCuenta = estadoCuenta;
        }
        #endregion
        #region Metodos
        #region Override
        /// <summary>
        /// Implementacion de metodo de clase base que crea una cadena que contiene los datos del Alumno.
        /// </summary>
        /// <returns>cadena con los datos del alumno/returns>
        protected override string MostrarDatos()
        {
            StringBuilder cadena = new StringBuilder();
            cadena.AppendLine(base.MostrarDatos());//Llama al metodo de la clase base, recibe una cadena con los datos de la PersonaGimnasio
            cadena.AppendLine();
            cadena.AppendLine("ESTADO DE CUENTA: "+ this._estadoCuenta.ToString());
            cadena.AppendLine(this.ParticiparEnClase());
            return cadena.ToString();   
        }
        /// <summary>
        /// Hace posible obtener los datos de la instancia del Alumno.
        /// </summary>
        /// <returns>cadena con los datos del alumno</returns>
        public override string ToString()
        {
            return this.MostrarDatos();
        }    
       /// <summary>
       /// Obtengo las clases que toma el alumno
       /// </summary>
       /// <returns>cadena con la clase que toma el alumno</returns>
        protected override string ParticiparEnClase()
        {
            return "TOMA CLASE DE " + this._claseQueToma;
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
        #endregion
        #region Sobrecarga Operadores
        /// <summary>
        /// Verifica si un alumno toma una clase determinada y no es deudor.
        /// </summary>
        /// <param name="a">Objeto de tipo Alumno</param>
        /// <param name="clase">clase de tipo Enum</param>
        /// <returns>true si la toma, false no la toma.</returns>
        public static bool operator ==(Alumno a, Gimnasio.EClases clase)
        {
            if (!object.Equals(a, null) && !object.Equals(clase, null))
            {
                if (a._claseQueToma == clase && a._estadoCuenta != EEstadoCuenta.Deudor)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Verifica si un alumno no toma una clase determinada.
        /// </summary>
        /// <param name="a">Objeto de tipo Alumno</param>
        /// <param name="clase">clase de tipo Enum</param>
        /// <returns>true no la toma, false si la toma.</returns>
        public static bool operator !=(Alumno a, Gimnasio.EClases clase)
        {
            if(!object.Equals(a,null) && a != clase)
                return true;
            return false;
        }
        #endregion
        #region Enumerado
        public enum EEstadoCuenta
        {
            Deudor,
            MesPrueba,
            AlDia

        }
        #endregion
    }
}