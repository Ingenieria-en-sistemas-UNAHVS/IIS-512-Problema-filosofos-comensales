using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProblemaFilosofosGrafico
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(txtCantidad.Text, out int cantidad)) {
                    if (cantidad <= 0 || cantidad > 10)
                        MessageBox.Show("La cantidad debe ser entre 1 y 10", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    imgMesa.SendToBack();
                    PictureBox picture = new PictureBox();
                    picture.Size = new Size(50, 50);
                    picture.Location = new Point(520, 320);
                    picture.ImageLocation = Path.GetFullPath(@"..\..\attachments\fork.png");
                    picture.SizeMode = PictureBoxSizeMode.StretchImage;
                    picture.BringToFront();
                    this.Controls.Add(picture);
                }
                else
                {
                    MessageBox.Show("Cantidad inválida","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un error inesperado","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //string relativePath = @"..\..\attachments\roundTable.png";
            //string absolutePath = Path.GetFullPath(relativePath);
            //imgMesa.ImageLocation = absolutePath;
            //imgMesa.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
