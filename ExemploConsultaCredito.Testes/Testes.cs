using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ExemploConsultaCredito;

namespace ExemploConsultaCredito.Testes
{
    [TestClass]
    public class Testes
    {
        [TestMethod]
        public void TestarParametroInvalido()
        {
            Mock<IServicoConsultaCredito> mock =
                new Mock<IServicoConsultaCredito>();
            mock.Setup(s => s.ConsultarPendenciasPorCPF(It.IsAny<string>()))
                .Returns(() => null);

            AnaliseCredito analise =
                new AnaliseCredito(mock.Object);
            StatusConsultaCredito status =
                analise.ConsultarSituacaoCPF("123");

            Assert.AreEqual(StatusConsultaCredito.ParametroEnvioInvalido,
                status);
        }

        [TestMethod]
        public void TestarErroComunicacao()
        {
            Mock<IServicoConsultaCredito> mock =
                new Mock<IServicoConsultaCredito>();
            mock.Setup(s => s.ConsultarPendenciasPorCPF(It.IsAny<string>()))
                .Throws(new Exception("Teste Moq - Erro Comunicação"));

            AnaliseCredito analise =
                new AnaliseCredito(mock.Object);
            StatusConsultaCredito status =
                analise.ConsultarSituacaoCPF("12345678901");

            Assert.AreEqual(StatusConsultaCredito.ErroComunicacao,
                status);
        }

        [TestMethod]
        public void TestarParametroSemPendencias()
        {
            Mock<IServicoConsultaCredito> mock =
                new Mock<IServicoConsultaCredito>();
            mock.Setup(s => s.ConsultarPendenciasPorCPF(It.IsAny<string>()))
                .Returns(new List<Pendencia>());

            AnaliseCredito analise =
                new AnaliseCredito(mock.Object);
            StatusConsultaCredito status =
                analise.ConsultarSituacaoCPF("12345578901");

            Assert.AreEqual(StatusConsultaCredito.SemPendencias,
                status);
        }

        [TestMethod]
        public void TestarInadimplente()
        {
            Mock<IServicoConsultaCredito> mock =
                new Mock<IServicoConsultaCredito>();

            var pendencias = new List<Pendencia>();
            pendencias.Add(new Pendencia()
                {
                    CPF = "12345678901",
                    DescricaoPendencia = "CARTÃO DE CRÉDITO"
                });
            
            mock.Setup(s => s.ConsultarPendenciasPorCPF(It.IsAny<string>()))
                .Returns(pendencias);

            AnaliseCredito analise =
                new AnaliseCredito(mock.Object);
            StatusConsultaCredito status =
                analise.ConsultarSituacaoCPF("12345578901");

            Assert.AreEqual(StatusConsultaCredito.Inadimplente,
                status);
        }
    }
}