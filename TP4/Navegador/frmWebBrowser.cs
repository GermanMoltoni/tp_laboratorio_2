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
using System.Threading;
using Hilo;

namespace Navegador
{
    public partial class frmWebBrowser : Form
    {
        private const string ESCRIBA_AQUI = "Escriba aquí...";
        Archivos.Texto archivos;

        public frmWebBrowser()
        {
            InitializeComponent();
        }

        private void frmWebBrowser_Load(object sender, EventArgs e)
        {
            this.txtUrl.SelectionStart = 0;  //This keeps the text
            this.txtUrl.SelectionLength = 0; //from being highlighted
            this.txtUrl.ForeColor = Color.Gray;
            this.txtUrl.Text = frmWebBrowser.ESCRIBA_AQUI;

            archivos = new Archivos.Texto(frmHistorial.ARCHIVO_HISTORIAL);
        }

        #region "Escriba aquí..."
        private void txtUrl_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.IBeam; //Without this the mouse pointer shows busy
        }

        private void txtUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.txtUrl.Text.Equals(frmWebBrowser.ESCRIBA_AQUI) == true)
            {
                this.txtUrl.Text = "";
                this.txtUrl.ForeColor = Color.Black;
            }
        }

        private void txtUrl_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.txtUrl.Text.Equals(null) == true || this.txtUrl.Text.Equals("") == true)
            {
                this.txtUrl.Text = frmWebBrowser.ESCRIBA_AQUI;
                this.txtUrl.ForeColor = Color.Gray;
            }
        }

        private void txtUrl_MouseDown(object sender, MouseEventArgs e)
        {
            this.txtUrl.SelectAll();
        }
        #endregion

        delegate void ProgresoDescargaCallback(int progreso);
        private void ProgresoDescarga(int progreso)
        {
            if (statusStrip.InvokeRequired)
            {
                ProgresoDescargaCallback d = new ProgresoDescargaCallback(ProgresoDescarga);
                this.Invoke(d, new object[] { progreso });
            }
            else
            {
                tspbProgreso.Value = progreso;
            }
            return;
        }
        delegate void FinDescargaCallback(string html);
        private void FinDescarga(string html)
        {
            if (rtxtHtmlCode.InvokeRequired)
            {
                FinDescargaCallback d = new FinDescargaCallback(FinDescarga);
                this.Invoke(d, new object[] { html });
            }
            else
            {
                rtxtHtmlCode.Text = html;
            }
            return;
        }

        private void btnIr_Click(object sender, EventArgs e)
        {
            if (this.txtUrl.Text.Length == 0 || this.txtUrl.Text.Contains(ESCRIBA_AQUI))
                MessageBox.Show("Se debe ingresar una URL");
            else
            {
                if (!this.txtUrl.Text.Contains("http://"))
                    this.txtUrl.Text = string.Concat("http://", this.txtUrl.Text);
                Descargador descarga = new Descargador(new Uri(this.txtUrl.Text));// Creo una descarga de la url pasada
                try
                {
                    
                    Thread descargador = new Thread(descarga.IniciarDescarga);// Creo un hilo del metodo IniciarDescarga 
                    descarga.ProgresoDescarga += new Descargador.DownloadProgress(this.ProgresoDescarga);
                    descarga.DescargaCompleta += new Descargador.DownloadCompleted(this.FinDescarga);
                    descargador.Start();
                    this.archivos.guardar(this.txtUrl.Text);//guardo la url
                }
                catch (Exception exc)
                {

                    MessageBox.Show("ERROR AL CARGAR LA PAGINA");
                }
                
            }  
        }

        private void mostrarTodoElHistorialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHistorial historial = new frmHistorial();//Creo un form historial
            try
            {
                historial.frmHistorial_Load(sender, e);//Cargo el form
                historial.Show();// Muestro el form
            }
            catch (Exception exc)
            {
                MessageBox.Show("El Historial no se pudo cargar!!!");
            }
            
        }
    }
}
