using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace p1final2024
{
    public partial class Articulos : Form
    {
        ArticuloDAO articuloDAO = new ArticuloDAO();
        public Articulos()
        {
            InitializeComponent();
            listarArticulos();
        }

        private void listarArticulos()
        {
            dgvArticulo.DataSource = articulodAO.ReadAll();
            dgvArticulos.Columns["image"].Visible = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Archivos de imagen (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|Todos los archivos (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Obtiene la ruta completa del archivo seleccionado

                    // Carga la imagen en el PictureBox
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            guardarNuevo();
            listarArticulos();
        }

        private void guardarNuevo()
        {
            Articulo articulo = new Articulo
            {
                Nombre = txtNombre.Text,
                Descripcion = TxtDescripcion.Text,
                Precio = Convert.ToDecimal(txtPrecio.Text),
                Imagen = null
            };

            articuloDAO.Create(articulo);
        }

        private byte[] obtenerImagen()
        {
            if (pcbImagen.Image == null)
            {
                MessageBox.Show("Por favor, selecciona una imagen.");
                return null;
            }
            byte[] imagenBytes;
            using (var ms = new System.IO.MemoryStream())
            {
                pcbImagen.Image.Save(ms, pcbImagen.Image.RawFormat);
                imagenBytes = ms.ToArray();
            }
            return imagenBytes;
        }

        
    }
}
