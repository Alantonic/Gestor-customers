using System;
using System.Collections.Generic;
using ClienteNegocio.DTOs;
using ClientesDatos.Repositories;
using ClientesDatos.Infraestructura;

namespace ClienteNegocio.Management
{
    public class ClienteManagement
    {
        // OBTENER TODOS LOS CLIENTES
        public List<ClienteDTO> ObtenerCliente()
        {
            // Obtener los Customers desde el Repository (Northwind)
            List<Customers> clientesDatos = new ClientesDatos.Repositories.ClienteRepository().ObtenerClientes();
            List<ClienteDTO> listadoRetorno = new List<ClienteDTO>();

            foreach (var item in clientesDatos)
            {
                var dto = new ClienteDTO();
                dto.CustomerID = item.CustomerID;
                dto.CompanyName = item.CompanyName;
                dto.ContactName = item.ContactName;
                dto.ContactTitle = item.ContactTitle;
                dto.Address = item.Address;
                dto.City = item.City;
                dto.Region = item.Region;
                dto.PostalCode = item.PostalCode;
                dto.Country = item.Country;
                dto.Phone = item.Phone;
                dto.Fax = item.Fax;

                listadoRetorno.Add(dto);
            }

            return listadoRetorno;
        }

        // ALTA DE CLIENTE
        public void AltaCliente(ClienteDTO cliente)
        {
            if (cliente == null) return;


            Customers clienteDB = new Customers();
            clienteDB.CustomerID = cliente.CustomerID;   
            clienteDB.CompanyName = cliente.CompanyName;
            clienteDB.ContactName = cliente.ContactName;
            clienteDB.ContactTitle = cliente.ContactTitle;
            clienteDB.Address = cliente.Address;
            clienteDB.City = cliente.City;
            clienteDB.Region = cliente.Region;
            clienteDB.PostalCode = cliente.PostalCode;
            clienteDB.Country = cliente.Country;
            clienteDB.Phone = cliente.Phone;
            clienteDB.Fax = cliente.Fax;

            new ClientesDatos.Repositories.ClienteRepository().AltaCliente(clienteDB);
        }

        // MODIFICAR CLIENTE
        public void ModificarCliente(ClienteDTO clienteMod)
        {
            if (clienteMod == null) return;

            // Convierte los DTOs a Customers (Northwind)
            Customers clienteDB = new Customers();
            clienteDB.CustomerID = clienteMod.CustomerID;
            clienteDB.CompanyName = clienteMod.CompanyName;
            clienteDB.ContactName = clienteMod.ContactName;
            clienteDB.ContactTitle = clienteMod.ContactTitle;
            clienteDB.Address = clienteMod.Address;
            clienteDB.City = clienteMod.City;
            clienteDB.Region = clienteMod.Region;
            clienteDB.PostalCode = clienteMod.PostalCode;
            clienteDB.Country = clienteMod.Country;
            clienteDB.Phone = clienteMod.Phone;
            clienteDB.Fax = clienteMod.Fax;

            new ClientesDatos.Repositories.ClienteRepository().ModificarCliente(clienteDB);
        }

        // ELIMINAR CLIENTE
        public void EliminarCliente(ClienteDTO clienteEliminar)
        {
            if (clienteEliminar == null) return;

            // Pasar el CustomerID (string) al Repository
            new ClientesDatos.Repositories.ClienteRepository().EliminarCliente(clienteEliminar.CustomerID);
        }

        // VERIFICAR CLIENTE EXISTENTE
        public bool VerificarCliente(string customerId) 
        {
            return new ClientesDatos.Repositories.ClienteRepository().VerificarClienteExistente(customerId);
        }
    }
}



