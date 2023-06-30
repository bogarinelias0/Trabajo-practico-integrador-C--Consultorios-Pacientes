using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{

   
    public partial class ContacDetails : Form
    {

        private businessLogicLayer _businessLogicLayer;
        private ContacModel _contacModel;
        public ContacDetails()
        {
            InitializeComponent();

            _businessLogicLayer = new businessLogicLayer(); 

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ContacModel contacModel = new ContacModel();
            contacModel.nombre = txtNombre.Text;
            contacModel.apellido = txtApellido.Text;
            contacModel.edad = txtEdad.Text;
            contacModel.correo = txtCorreo.Text;
            contacModel.id = _contacModel != null ? _contacModel.id : 0;
            _businessLogicLayer.guardarContac(contacModel);

            this.Close();
            ((Main) this.Owner).mostrarContactos();
        }

        private void ContacDetails_Load(object sender, EventArgs e)
        {

        }
        
        public void LoadContactos(ContacModel contactos) 
        {
        if (contactos != null)
            {
                LimpiarCajas();
                _contacModel = contactos;
                txtNombre.Text = contactos.nombre;
                txtApellido.Text = contactos.apellido;
                txtEdad.Text = contactos.edad;
                txtCorreo.Text = contactos.correo;  


            }
        }


        private void LimpiarCajas()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtEdad.Text = string.Empty;
            txtCorreo.Text = string.Empty;
        }



    }
}
