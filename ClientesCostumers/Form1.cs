using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClienteNegocio.Management;
using ClientesCostumers.Vista;
using ClienteNegocio.DTOs;
using System.Windows.Forms; 

namespace ClientesCostumers
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AltaCliente ventanda = new AltaCliente();
            ventanda.ShowDialog();
            dataGridView1.DataSource = new ClienteNegocio.Management.ClienteManagement().ObtenerCliente();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                ClienteNegocio.DTOs.ClienteDTO clienteSeleccionado = dataGridView1.CurrentRow.DataBoundItem as ClienteNegocio.DTOs.ClienteDTO;

                if (new ClienteNegocio.Management.ClienteManagement().VerificarCliente(clienteSeleccionado.CustomerID))
                {
                    DialogResult respuesta = MessageBox.Show("Es seguro de eliminar el cliente", "validación", MessageBoxButtons.YesNo);
                    if (respuesta == DialogResult.Yes)
                    {
                        new ClienteNegocio.Management.ClienteManagement().EliminarCliente(clienteSeleccionado);

                    }
                }
                dataGridView1.DataSource = new ClienteNegocio.Management.ClienteManagement().ObtenerCliente();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClienteNegocio.DTOs.ClienteDTO ClienteSeleccionado = dataGridView1.CurrentRow.DataBoundItem as ClienteNegocio.DTOs.ClienteDTO;
            AltaCliente altaCliente = new AltaCliente();
            altaCliente.ShowDialog();
            dataGridView1.DataSource = new ClienteNegocio.Management.ClienteManagement().ObtenerCliente();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
