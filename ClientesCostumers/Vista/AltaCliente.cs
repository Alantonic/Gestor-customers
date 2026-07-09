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
            try
            {
                if (clienteMod != null) 
                {
                    
                    clienteMod.CompanyName = txtCompanyName.Text;
                    clienteMod.ContactName = txtContactName.Text;
                    clienteMod.ContactTitle = txtContactTitle.Text;
                    clienteMod.Address = txtAddress.Text;
                    clienteMod.City = txtCity.Text;
                    clienteMod.Region = txtRegion.Text;
                    clienteMod.PostalCode = txtPostalCode.Text;
                    clienteMod.Country = txtCountry.Text;
                    clienteMod.Phone = txtPhone.Text;
                    clienteMod.Fax = txtFax.Text;

                 
                    management.ModificarCliente(clienteMod);

                    MessageBox.Show("Cliente modificado correctamente", "Éxito");
                }
                else //  ES ALTA
                {
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

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error");
            }
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
