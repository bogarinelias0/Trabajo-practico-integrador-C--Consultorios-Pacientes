using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Main : Form
    {

        private businessLogicLayer _businessLogicLayer;
        public Main()
        {
            InitializeComponent();
            _businessLogicLayer = new businessLogicLayer();
        }



        #region Eventos

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenContacDialog();
        }


        #endregion
       

        #region PRIVATE Methods

        private void OpenContacDialog()
        {
            ContacDetails contacDetails = new ContacDetails();
            contacDetails.ShowDialog(this);
        }


        #endregion

        private void Main_Load(object sender, EventArgs e)
        {
            mostrarContactos();
        }
        public void mostrarContactos(string buscarText = null)
        {

            List<ContacModel> contactos = _businessLogicLayer.GetContactos(buscarText);
            dataGridView1.DataSource = contactos;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewLinkCell celda = (DataGridViewLinkCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (celda.Value.ToString() == "editar" ) 
            { 
            ContacDetails contacDetails1 = new ContacDetails();
                contacDetails1.LoadContactos(new ContacModel
                {
                    id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value .ToString()),
                    nombre= dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    apellido = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString(),
                    edad= dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    correo = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString(),
                });
                contacDetails1.ShowDialog(this);
            }
            else if (celda.Value.ToString() == "eliminar") 
            
            {
                ElimnarContacto(int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                mostrarContactos();
            }



            
        }

        private void ElimnarContacto(int id) 
        {
            _businessLogicLayer.EliminarContac(id);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            mostrarContactos(txtSearch.Text);
            txtSearch.Text = string.Empty;
        }
    }
        
}
