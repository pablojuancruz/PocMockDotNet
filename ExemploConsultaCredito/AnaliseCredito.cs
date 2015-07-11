using System;

namespace ExemploConsultaCredito
{
    public class AnaliseCredito
    {
        private IServicoConsultaCredito _servico;

        public AnaliseCredito(IServicoConsultaCredito servConsultaCredito)
        {
            this._servico = servConsultaCredito;
        }

        public StatusConsultaCredito ConsultarSituacaoCPF(string cpf)
        {
            try
            {
                var pendencias = _servico
                    .ConsultarPendenciasPorCPF(cpf);

                if (pendencias == null)
                    return StatusConsultaCredito.ParametroEnvioInvalido;
                else if (pendencias.Count == 0)
                    return StatusConsultaCredito.SemPendencias;
                else
                    return StatusConsultaCredito.Inadimplente;
            }
            catch
            {
                return StatusConsultaCredito.ErroComunicacao;
            }
        }
    }
}