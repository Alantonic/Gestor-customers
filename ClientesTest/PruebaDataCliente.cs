using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientesDatos.Repositories;
using ClientesDatos.Infraestructura;
using System.Linq;

namespace ClientesPruebas
{
    [TestClass]
    public class PruebaDataCliente
    {
        private ClienteRepository repository;

        [TestInitialize]
        public void Inicializar()
        {
            repository = new ClienteRepository();
        }

        // 🔹 PRUEBA 1: OBTENER LISTA DE CLIENTES
        [TestMethod]
        public void ObtenerClientes_DeberiaRetornarLista()
        {
            // Act
            var resultado = repository.ObtenerClientes();

            // Assert
            Assert.IsNotNull(resultado);
            Console.WriteLine($"Se encontraron {resultado.Count} clientes");
        }

        // 🔹 PRUEBA 2: AGREGAR CLIENTE CON DATOS VÁLIDOS
        [TestMethod]
        public void AltaCliente_ConDatosValidos_DeberiaAgregar()
        {
            // Arrange - Generar un CustomerID único de 5 caracteres
            var customerID = $"T{DateTime.Now.Ticks.ToString().Substring(0, 4)}"; // Ej: T1234

            var nuevoCliente = new Customers
            {
                CustomerID = customerID, // ← ASIGNAR EL ID ANTES DE GUARDAR
                CompanyName = "Prueba S.A.",
                ContactName = "Juan Prueba",
                ContactTitle = "Gerente de Pruebas",
                Address = "Calle Falsa 123",
                City = "Ciudad Test",
                Region = "Región Test",
                PostalCode = "12345",
                Country = "País Test",
                Phone = "123456789",
                Fax = "987654321"
            };

            // Act
            repository.AltaCliente(nuevoCliente);

            // Assert - Verificar que el cliente se agregó correctamente
            var clienteAgregado = repository.ObtenerClientes()
                .FirstOrDefault(c => c.CustomerID == customerID);

            Assert.IsNotNull(clienteAgregado, "El cliente debería existir en la base de datos");
            Assert.AreEqual("Prueba S.A.", clienteAgregado.CompanyName, "El nombre de la empresa no coincide");
        }

        // VERIFICAR CLIENTE EXISTENTE 
        [TestMethod]
        public void VerificarClienteExistente_ConCustomerIDValido_DeberiaRetornarTrue()
        {
            // Arrange - Primero crear un cliente para verificar
            var customerID = $"V{DateTime.Now.Ticks.ToString().Substring(0, 4)}";
            var nuevoCliente = new Customers
            {
                CustomerID = customerID,
                CompanyName = "Prueba Verificación",
                ContactName = "Contacto Test"
            };
            repository.AltaCliente(nuevoCliente);

            // Act - Verificar que existe
            var existe = repository.VerificarClienteExistente(customerID); // ← string, no int

            // Assert
            Assert.IsTrue(existe, $"El cliente con ID {customerID} debería existir");
        }

        // PRUEBA 4: VERIFICAR CLIENTE INEXISTENTE
        [TestMethod]
        public void VerificarClienteExistente_ConCustomerIDInvalido_DeberiaRetornarFalse()
        {
            // Act - Verificar un ID que no existe
            var existe = repository.VerificarClienteExistente("ZZZZZ"); 

            // Assert
            Assert.IsFalse(existe, "El cliente no debería existir");
        }
    }
}
