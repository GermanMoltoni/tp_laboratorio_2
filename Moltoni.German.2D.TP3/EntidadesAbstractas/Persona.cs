using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excepciones;
using System.Text.RegularExpressions;
namespace EntidadesAbstractas
{
    [Serializable]
    public abstract class Persona
    {
        #region Enumerado
        public enum ENacionalidad
        {
            Argentino,
            Extranjero
        }
        #endregion
        #region Atributos
        private string _apellido;
        private int _dni;
        private ENacionalidad _nacionalidad;
        private string _nombre;
        #endregion
        #region Propiedades
        /// <summary>
        /// Obtengo o asigno un nombre valido
        /// </summary>
        public string Nombre
        {
            get { return this._nombre; }
            set { this._nombre = this.ValidarNombreApellido(value); }
        }
        /// <summary>
        /// Obtengo o asigno un apellido valido
        /// </summary>
        public string Apellido
        {
            get {return this._apellido; }
            set { this._apellido = this.ValidarNombreApellido(value); }
        }
        /// <summary>
        /// Obtengo o asigno Nacionalidad
        /// </summary>
        public ENacionalidad Nacionalidad
        {
            get { return this._nacionalidad; }
            set { this._nacionalidad = value; }
        }
        /// <summary>
        /// Asigno un dni a partir de una cadena.
        /// </summary>
        public string StringToDni
        {
            set
            {
                try
                {
                    this._dni = this.ValidarDni(this.Nacionalidad, value);
                }
                catch (NacionalidadInvalidaException excNac)
                {
                    throw excNac;
                }

            }
        }
        /// <summary>
        /// Obtengo o asigno un dni valido
        /// </summary>
        public int Dni
        {
            get { return this._dni; }
            set { this._dni = this.ValidarDni(this.Nacionalidad, value); }
        }
        
        
        
        #endregion
        #region Constructores
        /// <summary>
        /// Constructor por default de Persona, necesario para serialización
        /// </summary>
        public Persona()
        { }
        /// <summary>
        /// Inicializa una Persona, con nombre, apellido y nacionalidad
        /// </summary>
        /// <param name="nombre">nombre pasado por valor</param>
        /// <param name="apellido">apellido pasado por valor</param>
        /// <param name="nacionalidad">nacionalidad pasada por valor</param>
        public Persona(string nombre,string apellido,ENacionalidad nacionalidad)
        {
            this.Apellido = apellido;
            this.Nombre = nombre;
            this.Nacionalidad = nacionalidad;
        }
        /// <summary>
        /// Sobrecarga de constructor Persona , se inicia con un dni tipo entero.
        /// De no coincidir la nacionalidad y el dni  se lanza una excepcion.
        /// </summary>
        /// <param name="nombre">nombre pasado por valor</param>
        /// <param name="apellido">apellido pasado por valor</param>
        /// <param name="dni">dni de la persona pasado por valor</param>
        /// <param name="nacionalidad">nacionalidad pasada por valor</param>
        public Persona(string nombre, string apellido,int dni,ENacionalidad nacionalidad):this(nombre,apellido,nacionalidad)
        {
            try
            {
                this.Dni = dni;
            }
            catch (NacionalidadInvalidaException excNac)
            {
                throw excNac;
            }

        }
        /// <summary>
        /// Sobre carga de constructor Persona , se inicia con un dni tipo string.
        /// De no coincidir la nacionalidad y el dni o se ingresa un dni incorrecto, se lanza una excepcion
        /// </summary>
        /// <param name="nombre">nombre pasado por valor</param>
        /// <param name="apellido">apellido pasado por valor</param>
        /// <param name="dni">dni de la persona pasado por valor</param>
        /// <param name="nacionalidad">nacionalidad pasada por valor</param>
        public Persona(string nombre, string apellido,string dni,ENacionalidad nacionalidad) : this(nombre, apellido, nacionalidad)
        {
            try
            {
                this.StringToDni = dni;
            }
            catch (NacionalidadInvalidaException excNac)
            {
                throw excNac;
            }
            catch (DniInvalidoException excDni)
            {
                throw excDni;
            }
            
        }
        #endregion
        #region Metodos
        /// <summary>
        /// Valida que segun la nacionalidad el dni se encuentre dentro del rango permitido.
        /// De no coincidir lanza una excepcion.
        /// </summary>
        /// <param name="nacionalidad">nacionalidad de tipo Enum</param>
        /// <param name="dato">dni de tipo int</param>
        /// <returns>dni validado</returns>
        private int ValidarDni(ENacionalidad nacionalidad,int dato)
        {
            try
            {
                switch (nacionalidad)
                {
                    case ENacionalidad.Argentino:
                        if (dato < 1 || dato > 89999999)
                            throw new NacionalidadInvalidaException();
                        break;
                    case ENacionalidad.Extranjero:
                        if (dato < 89999999 || dato > 99999999)
                            throw new NacionalidadInvalidaException();
                        break;
                    default:
                        break;
                }
            }
            catch (NacionalidadInvalidaException nac)
            {
                throw nac;
            }
            return dato;
        }
        /// <summary>
        /// Valida un dni ingresado como string. Si tiene simbolos los elimina.
        /// Si el dni no es correcto lanza una excepcion
        /// Si la nacionalidad no coincide con el dni lanza una excepcion
        /// </summary>
        /// <param name="nacionalidad">nacionalidad de tipo Enum</param>
        /// <param name="dato">dni de tipo String</param>
        /// <returns>dni validado tipo entero</returns>
        private int ValidarDni(ENacionalidad nacionalidad, string dato)
        {
            int dni=0;
            if (dato.Length >= 9 && dato.Length <= 10)
            {
                Regex expReg = new Regex(@"[^\w]");
                dato = expReg.Replace(dato, "");
            }
            if (dato.Length >= 7 && dato.Length <= 8)
            {
                try
                {
                    if (int.TryParse(dato, out dni))
                        dni = this.ValidarDni(nacionalidad, dni);
                    else
                        throw new DniInvalidoException("Verificar el dni ingresado");

                }
                catch (DniInvalidoException exDni)
                {
                    throw exDni;
                }
                catch (NacionalidadInvalidaException exNac)
                {
                    throw exNac;
                }
            }
            else
                throw new DniInvalidoException("Error en el ingreso de dni");
            return dni;
        }
        /// <summary>
        /// Valida que el dato ingresado contenga caracteres validos para un nombre o apellido.
        /// </summary>
        /// <param name="dato">dato a validar</param>
        /// <returns>dato si es valido el mismo, caso contrario vacio.</returns>
        private string ValidarNombreApellido(string dato)
        {
            Regex expReg = new Regex(@"[a-zA-ZáéíóúÁÉÍÓÚ\s]*");
            Match match = expReg.Match(dato);
            if (match.Length == dato.Length)
                return match.Value;
            return "";
        }

        #region Override
        /// <summary>
        /// Sobre carga de ToString, muestra los datos de la persona.
        /// </summary>
        /// <returns>datos de la persona.</returns>
        public override string ToString()
        {
            StringBuilder cadena = new StringBuilder();
            cadena.AppendFormat("NOMBRE COMPLETO {0}, {1}", this.Apellido, this.Nombre);
            cadena.AppendLine();
            cadena.AppendLine("NACIONALIDAD: " + this._nacionalidad.ToString());
            return cadena.ToString();
        }
        #endregion
        #endregion

    }

}