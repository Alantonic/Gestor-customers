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

        // 🔹 MÉTODO AUXILIAR PARA GENERAR CUSTOMERID
        private string GenerarCustomerID()
        {
            long ticks = DateTime.Now.Ticks;
            string ticksStr = ticks.ToString();
            string idCorto = ticksStr.Substring(0, 4);
            return $"T{idCorto}";
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
            // Arrange
            var customerID = GenerarCustomerID();
            var nuevoCliente = new Customers
            {
                CustomerID = customerID,
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
            Assert.AreEqual("Prueba S.A.", clienteAgregado.CompanyName);
        }

        // 🔹 PRUEBA 3: VERIFICAR CLIENTE EXISTENTE
        [TestMethod]
        public void VerificarClienteExistente_ConCustomerIDValido_DeberiaRetornarTrue()
        {
            // Arrange - Crear un cliente para verificar
            var customerID = GenerarCustomerID();
            var nuevoCliente = new Customers
            {
                CustomerID = customerID,
                CompanyName = "Prueba Verificación"
            };
            repository.AltaCliente(nuevoCliente);

            // Act
            var existe = repository.VerificarClienteExistente(customerID);

            // Assert
            Assert.IsTrue(existe, $"El cliente con ID {customerID} debería existir");
        }

        // 🔹 PRUEBA 4: VERIFICAR CLIENTE INEXISTENTE
        [TestMethod]
        public void VerificarClienteExistente_ConCustomerIDInvalido_DeberiaRetornarFalse()
        {
            // Act
            var existe = repository.VerificarClienteExistente("ZZZZZ");

            // Assert
            Assert.IsFalse(existe, "El cliente NO debería existir");
        }

        // 🔹 PRUEBA 5: ELIMINAR CLIENTE
        [TestMethod]
        public void EliminarCliente_ConCustomerIDValido_DeberiaEliminar()
        {
            // Arrange
            var customerID = GenerarCustomerID();
            var nuevoCliente = new Customers
            {
                CustomerID = customerID,
                CompanyName = "Prueba Eliminar"
            };
            repository.AltaCliente(nuevoCliente);

            // Act
            repository.EliminarCliente(customerID);

            // Assert
            var existe = repository.VerificarClienteExistente(customerID);
            Assert.IsFalse(existe, "El cliente debería haber sido eliminado");
        }
    }
}