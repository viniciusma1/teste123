using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Teste123.CS;


namespace Teste123.ReuniãoDiária
{
    public partial class FormulárioReunião : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            rptReuniao.DataSource = CarregaDadosDiario();
            rptReuniao.DataBind();

        }


        public List<ItemReuniao> CarregaDadosDiario()
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=HDIBISQLD;Initial Catalog=BI_HDI_DAILY;User ID=bi_phd_digital;Password=phdigital50@;
"))
            {
                conn.Open();
                StringBuilder comando = new StringBuilder();
                List<ItemReuniao> listaItens = new List<ItemReuniao>();
                comando.Append("SELECT S.Nome,R.DIA_ANTERIOR, R.IMPEDIMENTO,R.DIA_HOJE, R.DATA from ReuniaoDiaria R inner join SalvarNome S on R.COD_CLI = S.COD_CLI where R.Data = convert(varchar(10), getdate(),120)");
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = comando.ToString();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ItemReuniao item = new ItemReuniao();
                    item.Data = DateTime.Now;
                    item.DiaAnterior = (string)dr[1];
                    item.Impedimento = (string)dr[2];
                    item.Nome = (string)dr[0];
                    item.DiaHoje = (string)dr[3];
                    listaItens.Add(item);
                }
                return listaItens;
            }
        }
        protected void btEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                string CorpoEmail = MontarEmail();
                Email.EnviarEmail("Reunião Diária<reuniaodiaria@hdi.com.br>", "viniciusma@hdi.com.br", "Reunião Diária - " + DateTime.Now.ToShortDateString(), CorpoEmail);
                Page.ClientScript.RegisterStartupScript(GetType(), "script", "alert('Formulário Enviado com Sucesso');", true);
            }
            catch (Exception ex)
            {
                
                 Page.ClientScript.RegisterStartupScript(GetType(), "script", "alert('"+ex.Message+"');", true);
            }

        }



        public string MontarEmail()
        {
            List<ItemReuniao> listaItens = CarregaDadosDiario();
            string email = "  <style> table { font-family: Verdana; text-align: center;} </style> <table border='1'><thead><tr><th>" + DateTime.Now.ToShortDateString() + "</th>" + "<th>Tarefas dia Anterior</th>" +
                "<th>Impedimento?</th><th>Tarefas hoje</th></tr></thead><tbody>";
            foreach (var item in listaItens)
            {
                email += "<tr><td>" + item.Nome + "</td><td>" + item.DiaAnterior + "</td><td>" + item.Impedimento + "</td><td>" + item.DiaHoje + "</td></tr>";
            }

            email += "</tbody></table>";

            return email;
        }



        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }
    }
}