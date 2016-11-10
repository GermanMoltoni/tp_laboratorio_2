using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesAbstractas
{
    [Serializable]
    public abstract class PersonaGimnasio:Persona
    {
        #region Atributos
        private int _identificador;
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor por default de PersonaGimnasio, necesario para serializar
        /// </summary>
        public PersonaGimnasio()
            : base()
        { }
        /// <summary>
        /// Inicializa una PersonaGimnasio, pasa parametros correspondiente al constructor de clase base.
        /// </summary>
        /// <param name="id">identificador unico de PersonaGimnasio</param>
        /// <param name="nombre">nombre de tipo string</param>
        /// <param name="apellido">apellido de tipo string</param>
        /// <param name="dni">dni de tipo int</param>
        /// <param name="nacionalidad">nacionalidad de tipo Enum</param>
        public PersonaGimnasio(int id,string nombre,string apellido,string dni,ENacionalidad nacionalidad):base(nombre,apellido,dni,nacionalidad)
        {
            this._identificador = id;
        }
        #endregion
        #region Metodos
        /// <summary>
        /// Crea una cadena que contiene los datos de la PersonaGimnasio.
        /// </summary>
        /// <returns>cadena con los datos de la PersonaGimnasio</returns>
        protected virtual string MostrarDatos()
        {
            StringBuilder cadena = new StringBuilder();
            cadena.Append(base.ToString());//Llama a la sobrecarga de ToString de la clase base para obtener la cadena con los datos de la Persona
            cadena.AppendLine();
            cadena.AppendFormat("CARNET NÚMERO: {0}",this._identificador);
            return cadena.ToString();
        }
        /// <summary>
        /// Metodo abstracto, se implementara en las clases derivadas
        /// </summary>
        /// <returns>cadena con datos de la clase en que participa</returns>
        protected abstract string ParticiparEnClase();

        #region Override
        /// <summary>
        /// SobreEscritura de Equals compara si la instancia es igual a la  PersonaGimnasio pasada por valor.
        /// </summary>
        /// <param name="obj">PersonaGimnacio a comparar</param>
        /// <returns>true si son iguales, false si no lo son</returns>
        public override bool Equals(object obj)
        {
            return (this == (PersonaGimnasio)obj);
        }
        #region
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
        #endregion
        #endregion
        #region Sobrecarga Operadores
        /// <summary>
        /// Verifica si dos PersonaGimnacio son iguales, comparando el tipo y su id o dni
        /// </summary>
        /// <param name="pg1">Objeto de tipo personaGimnasio </param>
        /// <param name="pg2">Objeto de tipo personaGimnasio</param>
        /// <returns>true si son iguales, flase si no lo son</returns>
        public static bool operator ==(PersonaGimnasio pg1, PersonaGimnasio pg2)
        {
            if (!object.Equals(pg1, null) || !object.Equals(pg2, null))
            {
                if (pg1.GetType() == pg2.GetType() && (pg1._identificador == pg2._identificador || pg1.Dni == pg2.Dni))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Verifica si dos PersonaGimnasio son distintas
        /// </summary>
        /// <param name="pg1">Objeto de tipo personaGimnasio </param>
        /// <param name="pg2">Objeto de tipo personaGimnasio</param>
        /// <returns>true si son iguales, flase si no lo son</returns>
        public static bool operator !=(PersonaGimnasio pg1, PersonaGimnasio pg2)
        {
            return !(pg1 == pg2);
        }
        #endregion
        
    }
}
