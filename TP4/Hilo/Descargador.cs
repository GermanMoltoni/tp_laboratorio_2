using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net; // Avisar del espacio de nombre
using System.ComponentModel;

namespace Hilo
{
    public class Descargador
    {
        private string html;
        private Uri direccion;
        public Descargador(Uri direccion)
        {
            this.html=null;
            this.direccion = direccion;
        }
        public override string ToString()
        {
            return this.html;
        }
        public void IniciarDescarga()
        {
            try
            {
                using (WebClient cliente = new WebClient())
                {
                    cliente.DownloadProgressChanged += new DownloadProgressChangedEventHandler(WebClientDownloadProgressChanged);
                    cliente.DownloadStringCompleted += new DownloadStringCompletedEventHandler(WebClientDownloadCompleted);
                    cliente.DownloadStringAsync(this.direccion);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void WebClientDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            ProgresoDescarga(e.ProgressPercentage);// Porcentaje de descarga.
        }
        private void WebClientDownloadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            this.html = e.Result;// Codigo de la pagina 
            DescargaCompleta(this.html);   
        }

        public delegate void DownloadProgress(int progress);
        public event DownloadProgress ProgresoDescarga;
        public delegate void DownloadCompleted(string html);
        public event DownloadCompleted DescargaCompleta;
    }
}
