using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ClientesDatos.Infraestructura;

namespace ClientesDatos.Repositories
{
    public class ClienteRepository
    {
        // OBTENER TODOS LOS CLIENTES
        public List<Northwind> ObtenerClientes()
        {
            List<Northwind> listadoretorno = new List<Northwind>();
            try
            {
                using (var context = new CustomersEntities1())
                {
                    listadoretorno = context.Northwind.ToList();
                }
                return listadoretorno;
            }
            catch
            {
                return listadoretorno;
            }
        }

        // OBTENER CLIENTE POR ID
        public Northwind ObtenerClientePorId(string customerId)
        {
            try
            {
                using (var context = new CustomersEntities1())
                {
                    return context.Northwind.FirstOrDefault(c => c.CustomerID == customerId);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // ALTA DE CLIENTE
        public void AltaCliente(Northwind cliente)
        {
            using (var context = new CustomersEntities1())
            {
                context.Northwind.Add(cliente);
                context.SaveChanges();
            }
        }

        // MODIFICAR CLIENTE
        public void ModificarCliente(Northwind clienteMod)
        {
            try
            {
                using (var context = new CustomersEntities1())
                {
                    Northwind clienteOriginal = context.Northwind
                        .FirstOrDefault(c => c.CustomerID == clienteMod.CustomerID);

                    if (clienteOriginal != null)
                    {
                        clienteOriginal.CompanyName = clienteMod.CompanyName;
                        clienteOriginal.ContactName = clienteMod.ContactName;
                        clienteOriginal.ContactTitle = clienteMod.ContactTitle;
                        clienteOriginal.Address = clienteMod.Address;
                        clienteOriginal.City = clienteMod.City;
                        clienteOriginal.Region = clienteMod.Region;
                        clienteOriginal.PostalCode = clienteMod.PostalCode;
                        clienteOriginal.Country = clienteMod.Country;
                        clienteOriginal.Phone = clienteMod.Phone;
                        clienteOriginal.Fax = clienteMod.Fax;

                        context.Entry(clienteOriginal).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // ELIMINAR CLIENTE
        public void EliminarCliente(string customerId)
        {
            try
            {
                using (var context = new CustomersEntities1())
                {
                    Northwind clienteEliminar = context.Northwind
                        .FirstOrDefault(c => c.CustomerID == customerId);

                    if (clienteEliminar != null)
                    {
                        context.Northwind.Remove(clienteEliminar);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // VERIFICAR CLIENTE EXISTENTE
        public bool VerificarClienteExistente(string customerId)
        {
            using (var context = new CustomersEntities1())
            {
                // DEBE SER "Northwind" (LA TABLA DONDE GUARDAS)
                return context.Northwind.Any(c => c.CustomerID == customerId);
            }
        }

        // 🔹 VERIFICAR CLIENTE POR NOMBRE
        public bool VerificarClientePorNombre(string companyName)
        {
            using (var context = new CustomersEntities1())
            {
                return context.Northwind.Any(c => c.CompanyName == companyName);
            }
        }
    }
}