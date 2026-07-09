using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientesDatos.Repositories;
using ClientesDatos.Infraestructura;

namespace ClientesPruebas
{
    [TestClass]
    public class PruebaClienteRepository
    {
        private ClienteRepository _repositorio;

        [TestInitialize]
        public void Inicializar()
        {
            _repositorio = new ClienteRepository();
        }

        [TestMethod]
        public void ObtenerClientes_DeberiaRetornarLista()
        {
            // Act
            var resultado = _repositorio.ObtenerClientes();

            // Assert
            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public void AltaCliente_ConDatosValidos_DeberiaAgregar()
        {
            // Arrange
            var nuevoCliente = new Clientescustomer
            {
                nombre = "Prueba",
                apellido = "Unitario",
                telefono = "123456789",
                email = "prueba@test.com",
                direccion = "Calle Falsa 123"
            };

            // Act
            _repositorio.AltaCliente(nuevoCliente);

            // Assert
            var clienteAgregado = _repositorio.ObtenerClientePorId(nuevoCliente.id_cliente);
            Assert.IsNotNull(clienteAgregado);
            Assert.AreEqual("Prueba", clienteAgregado.nombre);
        }

        [TestMethod]
        public void VerificarClienteExistente_ConIdValido_DeberiaRetornarTrue()
        {
            // Act
            var existe = _repositorio.VerificarClienteExistente(1);

            // Assert
            Assert.IsTrue(existe);
        }

        [TestMethod]
        public void EliminarCliente_ConIdValido_DeberiaEliminar()
        {
            // Arrange
            var nuevoCliente = new Clientescustomer
            {
                nombre = "Eliminar",
                apellido = "Test",
                telefono = "999999999",
                email = "eliminar@test.com"
            };
            _repositorio.AltaCliente(nuevoCliente);
            int idEliminar = nuevoCliente.id_cliente;

            // Act
            _repositorio.EliminarCliente(idEliminar);

            // Assert
            var eliminado = _repositorio.ObtenerClientePorId(idEliminar);
            Assert.IsNull(eliminado);
        }
    }
}