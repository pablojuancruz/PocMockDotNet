using System.Collections.Generic;

namespace ExemploConsultaCredito
{
    public interface IServicoConsultaCredito
    {
        IList<Pendencia> ConsultarPendenciasPorCPF(string cpf);
    }
}