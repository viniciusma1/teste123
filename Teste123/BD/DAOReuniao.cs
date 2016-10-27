using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using Teste123.CS;

namespace Teste123.BD
{
    public class DAOReuniao
    {
        public List<ParticipantesReuniaoDiaria> GetNomes()
        {
            List<ParticipantesReuniaoDiaria> listaRenorno = new List<ParticipantesReuniaoDiaria>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoDeDados"].ConnectionString))
            {
                conn.Open();

                StringBuilder comando = new StringBuilder();
                // Query para trazer todos os nomes dos participantes.
                comando.Append("SELECT [COD_CLI] ,[Nome] ,[Login] FROM ParticipantesReuniaoDiaria");
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = comando.ToString();
                SqlDataReader dr = cmd.ExecuteReader();
                listaRenorno.Add(new ParticipantesReuniaoDiaria() { Nome="Selecione..."});
                while (dr.Read())
                {
                    ParticipantesReuniaoDiaria itemLista = new ParticipantesReuniaoDiaria();
                    itemLista.ID = (int)dr["COD_CLI"];
                    itemLista.Nome = (string)dr["Nome"];
                    itemLista.Login = (string)dr["Login"];
                    listaRenorno.Add(itemLista);

                }
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            return listaRenorno;
        }

        // Carrega os dados da reuniao de hoje.
        public List<ItemReuniao> CarregaDadosDiario(DateTime dataParaCarga)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoDeDados"].ConnectionString))
            {
                List<ItemReuniao> listaItens = new List<ItemReuniao>();
                try
                {
                    conn.Open();
                    StringBuilder comando = new StringBuilder();

                    // Query que traz do banco as informações da reuniao do dia Atual
                    comando.Append("SELECT S.Nome,R.DIA_ANTERIOR, R.IMPEDIMENTO,R.DIA_HOJE, R.DATA,R.COD_CLI from ReuniaoDiaria R inner join ParticipantesReuniaoDiaria S on R.COD_CLI = S.COD_CLI where R.Data = '" + dataParaCarga.Date.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = comando.ToString();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ItemReuniao item = new ItemReuniao();
                        item.Data = (DateTime)dr[4];
                        item.DiaAnterior = (string)dr[1];
                        item.Impedimento = (string)dr[2];
                        item.Nome = (string)dr[0];
                        item.DiaHoje = (string)dr[3];
                        item.IDNome = (int)dr[5];
                        listaItens.Add(item);
                    }
                    return listaItens;
                }
                catch (Exception ex)
                {
                    return new List<ItemReuniao>();
                }


            }
        }


        // Funcao que vai ao banco e verifica se o usuario ja existe
        public bool VerificaUsuarioExistenteDiaAtual(int codUsuario)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoDeDados"].ConnectionString))
            {
                conn.Open();
                StringBuilder comando = new StringBuilder();
                List<ItemReuniao> listaItens = new List<ItemReuniao>();
                comando.Append("SELECT * FROM tblReuniaoDiaria_Detalhes A inner join tblReuniaoDiaria B on A.ID_REUNIAO = B.ID_REUNIAO  where A.ID_MEMBRO =3 AND B.DT_REUNIAO = convert(varchar(10), getdate(),120)");
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = comando.ToString();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    return true;
                }
                return false;
            }
        }

        public void InserirReuniaoDiaria(ItemReuniao reuniao)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoDeDados"].ConnectionString);
                conn.Open();
                StringBuilder comando = new StringBuilder();
                // Insere no banco as informações da pessoa na reuniao.
                comando.Append("INSERT INTO [dbo].[ReuniaoDiaria] (COD_CLI,DIA_ANTERIOR,IMPEDIMENTO,DIA_HOJE,DATA,COD_CLI_CHEFE,EMAIL_ENVIADO) values (" + reuniao.IDNome + ",'" + reuniao.DiaAnterior +
                    "','" + reuniao.Impedimento + "','" + reuniao.DiaHoje + "',convert(varchar(10), getdate(),120)," + reuniao.IDResponsavel + ",'" + reuniao.EmailEnviado + "')");
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = comando.ToString();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public bool VerificaEmailEnviado(string data)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoDeDados"].ConnectionString))
            {
                conn.Open();
                StringBuilder comando = new StringBuilder();
                List<ItemReuniao> listaItens = new List<ItemReuniao>();
                comando.Append("SELECT *  FROM [dbo].[ReuniaoDiaria] where Data ='" + data + "' and EMAIL_ENVIADO = 1");
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = comando.ToString();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    return true;
                }
                return false;
            }
        }

        public void AtualizaStatusEmail(bool status, string data)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoDeDados"].ConnectionString);
                conn.Open();
                StringBuilder comando = new StringBuilder();
                // Insere no banco as informações da pessoa na reuniao.
                comando.Append("UPDATE  [dbo].[ReuniaoDiaria] set EMAIL_ENVIADO ='" + status + "' WHERE DATA = '" + data + "'");
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = comando.ToString();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public int IDResponsavelReuniaoDiaria(string login)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoDeDados"].ConnectionString))
                {
                    conn.Open();
                    StringBuilder comando = new StringBuilder();
                    List<ItemReuniao> listaItens = new List<ItemReuniao>();
                    comando.Append(String.Format("SELECT COD_CLI FROM ParticipantesReuniaoDiaria where Login = '{0}' ", login));
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = comando.ToString();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        return (int)dr["COD_CLI"];
                    }
                }
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void ExcluiDadosReuniaoDiaria(int IdPessoa, string Data)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoDeDados"].ConnectionString);
                conn.Open();
                StringBuilder comando = new StringBuilder();
                // Insere no banco as informações da pessoa na reuniao.
                comando.Append("DELETE FROM [dbo].[ReuniaoDiaria] WHERE COD_CLI = " + IdPessoa + " AND DATA = '" + Data + "'");
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = comando.ToString();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {

            }
        }

    }

}
