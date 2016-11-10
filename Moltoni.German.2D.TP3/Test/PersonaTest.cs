using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntidadesInstanciables;
using Excepciones;
using EntidadesAbstractas;
namespace Test
{
    [TestClass]
    public class PersonaTest
    {
        [TestMethod]
        public void Verifico_Dni()
        {
            //Verifico que se lanze excepcion si el dni ingresado no corresponde a la nacionalidad
            string dniAlumno= "91233222";
            try
            {
                Alumno a1 = new Alumno(1, "Juan", "Lopez", dniAlumno,
                EntidadesAbstractas.Persona.ENacionalidad.Argentino, Gimnasio.EClases.Natacion,
                Alumno.EEstadoCuenta.Deudor);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(NacionalidadInvalidaException));
            }

            //Verifico que el dni ingresado con caracteres entre los numeros sea convertido a un numero.
            string dni = "123/233.22";
            Alumno a = new Alumno(2, "Juana", "Martinez", dni,EntidadesAbstractas.Persona.ENacionalidad.Argentino, Gimnasio.EClases.Natacion,Alumno.EEstadoCuenta.Deudor);
            Assert.AreEqual(a.Dni, 12323322);

            //Verifico que no se permitan caracteres que no sean numeros 
            string dni2 = "56..,.,";
            try
            {
                Alumno a2 = new Alumno(3, "Juana", "Martinez", dni2,
                EntidadesAbstractas.Persona.ENacionalidad.Extranjero, Gimnasio.EClases.Natacion,
                Alumno.EEstadoCuenta.Deudor);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(DniInvalidoException)); 
            }
            return;
        }


        [TestMethod]
        public void Verifico_No_Null()
        {
            //Verifico si el gimnasio creo la lista de alumnos cuando se inicia.
            Gimnasio gim = new Gimnasio();
            Assert.IsNotNull(gim.Alumnos);
            //Verifico si el instructor fue cargado correctamente en el gimnasio
            Instructor i1 = new Instructor(1, "Juan", "Lopez", "12234456",
            EntidadesAbstractas.Persona.ENacionalidad.Argentino);
            gim += i1;
            Assert.IsNotNull(gim.Instructores[0]);
            Assert.AreEqual(i1, gim.Instructores[0]);
        }


    }
}
