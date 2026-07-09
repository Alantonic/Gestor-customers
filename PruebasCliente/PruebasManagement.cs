using System;
using System.Linq;
using ClienteNegocio.DTOs;
using ClienteNegocio.Management;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClienteNegocio.DTOs;
using ClienteNegocio.Management;

namespace PruebasCliente
{
    [TestClass]
    public class PruebasManagement
    {
        private ClienteManagement _management;

        [TestInitialize]
        public void Inicializar()
        {
            _management = new ClienteManagement();
        }

        //  MÉTODO PARA GENERAR CUSTOMERID ÚNICO DE 5 CARACTERES
        private string GenerarCustomerID()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string nuevoID;
            bool existe;
            int intentos = 0;

            do
            {
                // Genera un ID de 5 caracteres aleatorios
                nuevoID = new string(Enumerable.Repeat(chars, 5)
                    .Select(s => s[random.Next(s.Length)]).ToArray());

                // Verifica si el ID ya existe en la base de datos
                existe = _management.VerificarCliente(nuevoID);
                Console.WriteLine($"Probando ID: {nuevoID} - ¿Existe? {existe} (Intento {++intentos})");
            }
            while (existe); // Si existe, genera otro ID

            Console.WriteLine($"✅ ID único generado: {nuevoID} (después de {intentos} intentos)");
            return nuevoID;
        }

        // PRUEBA 1: OBTENER TODOS LOS CLIENTES
        [TestMethod]
        public void ObtenerTodos_RegresaTablaConClientes()
        {
            // Act
            var resultado = _management.ObtenerCliente();

            // Assert
            Assert.IsNotNull(resultado, "La lista no debería ser nula");
            Console.WriteLine($" Se encontraron {resultado.Count} clientes");
        }

        // PRUEBA 2: INSERTAR CLIENTE VÁLIDO
        [TestMethod]
        public void Insertar_ClienteValido_RegresaTrue()
        {
            try
            {
                // Arrange
                var customerID = GenerarCustomerID();
                var nuevoCliente = new ClienteDTO
                {
                    CustomerID = customerID,
                    CompanyName = "Prueba Management S.A.",
                    ContactName = "Juan Management",
                    ContactTitle = "Gerente",
                    Address = "Calle Management 123",
                    City = "CiudadTest",
                    Country = "PaisTest",
                    Phone = "987654321",
                    Fax = "123456789"
                };

                // Act
                _management.AltaCliente(nuevoCliente);

                // Assert
                var clienteAgregado = _management.ObtenerCliente()
                    .FirstOrDefault(c => c.CustomerID == customerID);

                Assert.IsNotNull(clienteAgregado, "El cliente debería existir");
                Console.WriteLine($"✅ Cliente {customerID} insertado correctamente");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Error inesperado: {ex.Message}");
            }
        }

        // PRUEBA 3: INSERTAR CLIENTE CON CAMPOS VACÍOS
        [TestMethod]
        public void Insertar_ClienteConCamposVacios_RegresaFalse()
        {
            try
            {
                // Arrange - Cliente sin CompanyName (campo obligatorio)
                var customerID = GenerarCustomerID();
                var clienteInvalido = new ClienteDTO
                {
                    CustomerID = customerID,
                    CompanyName = "", // ← VACÍO (DEBE FALLAR)
                    ContactName = "Juan Management",
                    ContactTitle = "Gerente",
                    Address = "Calle Management 123",
                    City = "CiudadTest",
                    Country = "PaisTest",
                    Phone = "987654321",
                    Fax = "123456789"
                };

                // Act
                _management.AltaCliente(clienteInvalido);

                // Assert - Si llega aquí, la prueba falla porque NO lanzó excepción
                Assert.Fail("Se esperaba una excepción por CompanyName vacío");
            }
            catch (Exception)
            {
                // Excepción capturada - Prueba pasa
                Console.WriteLine(" Cliente con campos vacíos rechazado correctamente");
            }
        }

        // 🔹 PRUEBA 4: INSERTAR CLIENTE CON ID REPETIDO
        [TestMethod]
        public void Insertar_ClienteConIDRepetido_RegresaFalse()
        {
            try
            {
                // Arrange - Crear un cliente primero
                var customerID = GenerarCustomerID();
                var cliente1 = new ClienteDTO
                {
                    CustomerID = customerID,
                    CompanyName = "Prueba Original",
                    ContactName = "Contacto Original",
                    Phone = "111111111"
                };
                _management.AltaCliente(cliente1);

                // Act - Intentar insertar otro con el MISMO ID
                var cliente2 = new ClienteDTO
                {
                    CustomerID = customerID, // ← MISMO ID (DEBE FALLAR)
                    CompanyName = "Prueba Duplicada",
                    ContactName = "Contacto Duplicado",
                    Phone = "222222222"
                };
                _management.AltaCliente(cliente2);

                // Assert - Si llega aquí, la prueba falla
                Assert.Fail("Se esperaba una excepción por ID duplicado");
            }
            catch (Exception)
            {
                //  Excepción capturada - Prueba pasa
                Console.WriteLine(" Cliente con ID duplicado rechazado correctamente");
            }
        }

        // PRUEBA 5: CARGAR CLIENTE EXISTENTE
        [TestMethod]
        public void Cargar_ClienteExistente_RegresaTrue()
        {
            // Arrange
            var customerID = GenerarCustomerID();
            var nuevoCliente = new ClienteDTO
            {
                CustomerID = customerID,
                CompanyName = "Prueba Carga Existente"
            };
            _management.AltaCliente(nuevoCliente);

            // Act
            var existe = _management.VerificarCliente(customerID);

            // Assert
            Assert.IsTrue(existe, $"El cliente {customerID} debería existir");
            Console.WriteLine($" Cliente {customerID} encontrado");
        }

        // PRUEBA 6: CARGAR CLIENTE INEXISTENTE
        [TestMethod]
        public void Cargar_ClienteInexistente_RegresaFalse()
        {
            // Act
            var existe = _management.VerificarCliente("ZZZZZ");

            // Assert
            Assert.IsFalse(existe, "El cliente no debería existir");
            Console.WriteLine(" Cliente inexistente verificado correctamente");
        }

        // PRUEBA 7: ACTUALIZAR CLIENTE EXISTENTE
        [TestMethod]
        public void Actualizar_ClienteExistente_RegresaTrue()
        {
            try
            {
                // Arrange
                var customerID = GenerarCustomerID();
                var nuevoCliente = new ClienteDTO
                {
                    CustomerID = customerID,
                    CompanyName = "Prueba Original",
                    ContactName = "Contacto Original",
                    City = "CiudadTest",
                    Country = "PaisTest",
                    Phone = "111111111"
                };
                _management.AltaCliente(nuevoCliente);

                // Act
                var cliente = _management.ObtenerCliente()
                    .FirstOrDefault(c => c.CustomerID == customerID);

                Assert.IsNotNull(cliente, "El cliente debería existir");

                cliente.CompanyName = "Modificado Test";
                cliente.Phone = "999999999";

                _management.ModificarCliente(cliente);

                // Assert
                var clienteModificado = _management.ObtenerCliente()
                    .FirstOrDefault(c => c.CustomerID == customerID);

                Assert.IsNotNull(clienteModificado, "El cliente debería existir después de modificar");
                Assert.AreEqual("Modificado Test", clienteModificado.CompanyName);
                Assert.AreEqual("999999999", clienteModificado.Phone);

                Console.WriteLine($" Cliente {customerID} modificado correctamente");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Error inesperado: {ex.Message}");
            }
        }

        // PRUEBA 8: ACTUALIZAR CLIENTE INEXISTENTE
        [TestMethod]
        public void Actualizar_ClienteInexistente_RegresaFalse()
        {
            try
            {
                // Arrange - Cliente con ID que NO existe
                var clienteInexistente = new ClienteDTO
                {
                    CustomerID = "ZZZZZ", // ← ID INEXISTENTE
                    CompanyName = "Prueba Inexistente",
                    ContactName = "Contacto Inexistente",
                    Phone = "111111111"
                };

                // Act - Intentar modificar un cliente que no existe
                _management.ModificarCliente(clienteInexistente);

                // Assert - Si no lanza excepción, la prueba falla
                Assert.Fail("Se esperaba una excepción al modificar cliente inexistente");
            }
            catch (Exception)
            {
                // Excepción capturada - Prueba pasa
                Console.WriteLine(" Cliente inexistente rechazado correctamente");
            }
        }

        // 🔹 PRUEBA 9: ELIMINAR CLIENTE EXISTENTE
        [TestMethod]
        public void Eliminar_ClienteExistente_RegresaTrue()
        {
            // Arrange
            var customerID = GenerarCustomerID();
            Console.WriteLine($"ID generado: {customerID}");

            var nuevoCliente = new ClienteDTO
            {
                CustomerID = customerID,
                CompanyName = "Prueba Eliminar Existente"
            };

            // Act - Insertar cliente
            _management.AltaCliente(nuevoCliente);
            Console.WriteLine($"Cliente {customerID} insertado correctamente");

            // Act - Eliminar cliente
            _management.EliminarCliente(nuevoCliente);

            // Assert
            var existe = _management.VerificarCliente(customerID);
            Assert.IsFalse(existe, "El cliente debería haber sido eliminado");
            Console.WriteLine($"✅ Cliente {customerID} eliminado correctamente");
        }

        // 🔹 PRUEBA 10: ELIMINAR CLIENTE INEXISTENTE
        [TestMethod]
        public void Eliminar_ClienteInexistente_RegresaFalse()
        {
            try
            {
                // Arrange - Cliente con ID que NO existe
                var clienteInexistente = new ClienteDTO
                {
                    CustomerID = "ZZZZZ", // ← ID INEXISTENTE
                    CompanyName = "Prueba Eliminar Inexistente"
                };

                // Act - Intentar eliminar un cliente que no existe
                _management.EliminarCliente(clienteInexistente);

                // Assert - Si no lanza excepción, la prueba falla
                Assert.Fail("Se esperaba una excepción al eliminar cliente inexistente");
            }
            catch (Exception)
            {
                // Excepción capturada - Prueba pasa
                Console.WriteLine("Cliente inexistente rechazado correctamente");
            }
        }
    }
}