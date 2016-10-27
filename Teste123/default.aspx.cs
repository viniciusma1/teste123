using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Teste123.CS;
using Teste123.Aplicacao;

namespace Teste123.ReuniãoDiária
{
    public partial class SalvarFormulário : System.Web.UI.Page
    {
        GerenciadorReuniao gerenciadorReuniao = new GerenciadorReuniao();
        EmailReuniao emailReuniao = new EmailReuniao();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                EscondeCampos();
                txtDate.Text = DateTime.Now.ToShortDateString();
                List<ParticipantesReuniaoDiaria> listaNomes = new List<ParticipantesReuniaoDiaria>();

                // Preenche a Data de Hoje
                Data.Text = DateTime.Now.ToShortDateString();

                // Carrega os nomes dos usuarios
                listaNomes = gerenciadorReuniao.GetNomes();
                ddlResponsavel.DataSource = listaNomes;
                
                ddlResponsavel.DataBind();
                ddlNome.DataSource = listaNomes;
                ddlNome.DataBind();

                // Carrega a tabela com os participantes de hoje.
                PopulaRepeater(gerenciadorReuniao.CarregaDadosDiario(Convert.ToDateTime(txtDate.Text)));

            }

        }

        protected void BtnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica se a pessoa ja está cadastrada na reuniao de hoje.  
                if (!gerenciadorReuniao.VerificaUsuarioExistenteDiaAtual(Convert.ToInt32(ddlNome.SelectedItem.Value)))
                {
                    // Cria um objeto com as informações da pessoa e da reuniao.
                    ItemReuniao reuniao = new ItemReuniao();
                    reuniao.Data = DateTime.Now;
                    reuniao.DiaAnterior = txtDiaAnterior.Text;
                    reuniao.Impedimento = txtImpedimento.Text;
                    reuniao.DiaHoje = txtDiaHoje.Text;
                    reuniao.IDNome = Convert.ToInt32(ddlNome.SelectedItem.Value);
                    reuniao.IDResponsavel = gerenciadorReuniao.IDResponsavelReuniaoDiaria(ddlResponsavel.SelectedValue);
                    reuniao.EmailEnviado = false;
                    // insere no banco
                    gerenciadorReuniao.InserirReuniaoDiaria(reuniao);
                    txtDate.Text = DateTime.Now.ToShortDateString();
                    // Carrega a Tabela com as informações de hoje
                    PopulaRepeater(gerenciadorReuniao.CarregaDadosDiario(Convert.ToDateTime(txtDate.Text)));

                    LimparCampos();
                }
                else
                {
                    // Caso a pessoa ja esteja cadastrada mandar uma mensagem na tela avisando.
                    Page.ClientScript.RegisterStartupScript(GetType(), "script", "alert('" + ddlNome.SelectedItem.Text + " Já está cadastrado hoje.');", true);
                }

                VerificaChefeReuniao();

            }
            catch (Exception ex)
            {
                // aso de algum erro mostrar a mensagem na tela.
                Page.ClientScript.RegisterStartupScript(GetType(), "script", "alert('" + ex.Message + "');", true);
            }
        }

        public void VerificaChefeReuniao()
        {
            // se o usuario logado for diferente do usuario responsavel
            // ToUpper() = Significa deixar tudo em letra maiuscula
            if (HttpContext.Current.User.Identity.Name.ToUpper() != ddlResponsavel.SelectedValue.ToUpper())
            {
                btnEmail.Visible = false;
                BtnEnviar.Visible = false;
            }
            else
            {
                btnEmail.Visible = true;
                BtnEnviar.Visible = true;
            }
        }

        public void EscondeCampos()
        {
            btnEmail.Visible = false;
            BtnEnviar.Visible = false;
        }

        public void PopulaRepeater(List<ItemReuniao> lista)
        {
            // Limpa a tabela 
            rptReuniao.Dispose();
            rptReuniao.DataBind();
            rptReuniao.DataSource = gerenciadorReuniao.CarregaDadosDiario(Convert.ToDateTime(txtDate.Text));
            rptReuniao.DataBind();
        }

        // Botao que enviará o Email
        protected void Button_Click(object sender, EventArgs e)
        {
            try
            {
                //Verificar se o email ja foi enviado
                if (gerenciadorReuniao.VerificaEmailEnviado(Convert.ToDateTime(txtDate.Text).Date.ToString("yyyy-MM-dd HH:mm:ss")))
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "script", "alert('E-mail já enviado');", true);
                }
                else
                {
                    // Montar Email
                    string CorpoEmail = emailReuniao.MontarEmail(gerenciadorReuniao.CarregaDadosDiario(Convert.ToDateTime(txtDate.Text)), Convert.ToDateTime(txtDate.Text));
                    // Enviar o Email
                    Email.EnviarEmail("Reunião Diária<reuniaodiaria@hdi.com.br>", "vinicius.marzani@hdi.com.br", "Reunião Diária - " + Convert.ToDateTime(txtDate.Text).ToShortDateString(), CorpoEmail);
                    gerenciadorReuniao.AtualizaStatusEmail(true, Convert.ToDateTime(txtDate.Text).Date.ToString("yyyy-MM-dd HH:mm:ss"));
                    //   Page.ClientScript.RegisterStartupScript(GetType(), "script", "", true);
                    Page.ClientScript.RegisterStartupScript(GetType(), "script", "alert('E-mail Enviado com Sucesso');", true);
                }

            }
            // APENAS SE DER ERRO
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "script", "alert('" + ex.Message + "');", true);
            }
        }


        public void LimparCampos()
        {
            txtDiaAnterior.Text = "";
            txtImpedimento.Text = "Sem Impedimento";
            txtDiaHoje.Text = "";
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            PopulaRepeater(gerenciadorReuniao.CarregaDadosDiario(Convert.ToDateTime(txtDate.Text)));
        }

        protected void ddlResponsavel_SelectedIndexChanged(object sender, EventArgs e)
        {
            VerificaChefeReuniao();
        }

        // Esse evento ja vai pegar o cara automatico no click
        protected void rptReuniao_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            gerenciadorReuniao.ExcluiDadosReuniaoDiaria(Convert.ToInt32(e.CommandName), Convert.ToDateTime(e.CommandArgument).ToString("yyyy-MM-dd HH:mm:ss"));
            PopulaRepeater(gerenciadorReuniao.CarregaDadosDiario(Convert.ToDateTime(txtDate.Text)));
        }


    }

}