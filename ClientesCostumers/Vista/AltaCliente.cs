using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClienteNegocio.DTOs;
using ClienteNegocio.Management;
namespace ClientesCostumers.Vista
{
    public partial class AltaCliente : Form
    {
        public ClienteDTO clienteMod;

        private ClienteManagement management;
        // Constructor para ALTA
        public AltaCliente()

        { 
            InitializeComponent();
            management = new ClienteManagement();
        }
        // Constructor para MODIFICACIÓN
        public AltaCliente(ClienteDTO cliente)
        {
            clienteMod = cliente;
            InitializeComponent();
            management = new ClienteManagement();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (clienteMod != null)
            {
                // Cargar datos del cliente en los campos
                txtCustomerID.Text = clienteMod.CustomerID;
                txtContactCustomerID.Enabled = false; 
                txtCompanyName.Text = clienteMod.CompanyName;
                txtContactName.Text = clienteMod.ContactName;
                txtContactTitle.Text = clienteMod.ContactTitle;
                txtAddress.Text = clienteMod.Address;
                txtCity.Text = clienteMod.City;
                txtRegion.Text = clienteMod.Region;
                txtPostalCode.Text = clienteMod.PostalCode;
                txtCountry.Text = clienteMod.Country;
                txtPhone.Text = clienteMod.Phone;
                txtFax.Text = clienteMod.Fax;
            }
            else
            {
                // Es ALTA
                ClienteDTO nuevoCliente = new ClienteDTO();
                nuevoCliente.CustomerID = txtCustomerID.Text.ToUpper();
                nuevoCliente.CompanyName = txtCompanyName.Text;
                nuevoCliente.ContactName = txtContactName.Text;
                nuevoCliente.ContactTitle = txtContactTitle.Text;
                nuevoCliente.Address = txtAddress.Text;
                nuevoCliente.City = txtCity.Text;
                nuevoCliente.Region = txtRegion.Text;
                nuevoCliente.PostalCode = txtPostalCode.Text;
                nuevoCliente.Country = txtCountry.Text;
                nuevoCliente.Phone = txtPhone.Text;
                nuevoCliente.Fax = txtFax.Text;

                management.AltaCliente(nuevoCliente);
                MessageBox.Show("Cliente agregado correctamente", "Éxito");
            }
            //cierra el interfaz de formulario
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        private void AltaCliente_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            DialogResult resultado = MessageBox.Show(
                "¿Seguro que deseas cancelar? Los cambios no se guardarán.",
                "Confirmar cancelación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        
        }
    }
}
