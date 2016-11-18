using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Archivos;
namespace Navegador
{
    public partial class frmHistorial : Form
    {
        public const string ARCHIVO_HISTORIAL = "historico.dat";

        public frmHistorial()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Carga las url contenidas en el archivo "historico.dat" en el lstHistorial del formulario del historial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void frmHistorial_Load(object sender, EventArgs e)
        {
            List<string> historial = new List<string>();
            Texto archivo = new Texto(frmHistorial.ARCHIVO_HISTORIAL);// Creo un objeto texto con el path del archivo del historial.
            try
            {
                archivo.leer(out historial);// Leo "historico.dat"
                lstHistorial.Items.Clear();//Borro contenido  lstHistorial
                if (historial.Count > 0)
                    lstHistorial.Items.AddRange(historial.ToArray());//Agrego el contenido de la lista al listbox del formulario
            }
            catch (Exception exc)
            {
                throw exc;
            }
            
        }
    }
}
