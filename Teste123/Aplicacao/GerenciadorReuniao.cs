using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teste123.BD;
using Teste123.CS;

namespace Teste123.Aplicacao
{
    // Aqui onde fica a regra de negocio
    public class GerenciadorReuniao
    {
        private DAOReuniao dao = new DAOReuniao();

        public List<ParticipantesReuniaoDiaria> GetNomes()
        {
            return dao.GetNomes();
        }

        public List<ItemReuniao> CarregaDadosDiario(DateTime dataParaCarga)
        {
            return dao.CarregaDadosDiario(dataParaCarga);
        }

        public bool VerificaUsuarioExistenteDiaAtual(int codUsuario)
        {
            return dao.VerificaUsuarioExistenteDiaAtual(codUsuario);
        }

        public void InserirReuniaoDiaria(ItemReuniao reuniao)
        {
            dao.InserirReuniaoDiaria(reuniao);
        }
        public bool VerificaEmailEnviado(string data)
        {
            return dao.VerificaEmailEnviado(data);
        }
        public void AtualizaStatusEmail(bool status, string data)
        {
            dao.AtualizaStatusEmail(status, data);
        }
        public int IDResponsavelReuniaoDiaria(string login)
        {
            return dao.IDResponsavelReuniaoDiaria(login);
        }
        public void ExcluiDadosReuniaoDiaria(int IDNome, string data)
        {
            dao.ExcluiDadosReuniaoDiaria(IDNome, data);
        }

    }
}