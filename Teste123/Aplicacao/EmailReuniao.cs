using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teste123.CS;

namespace Teste123.Aplicacao
{
    public class EmailReuniao
    {

        public string MontarEmail(List<ItemReuniao> listaItens,DateTime data)
        {
            string email = "  <style> table { font-family: Verdana; text-align: center;} </style> <table border='1'><thead><tr><th>" + data.ToShortDateString() + "</th>" + "<th>Tarefas dia Anterior</th>" +
               "<th>Impedimento?</th><th>Tarefas hoje</th></tr></thead><tbody>";
            foreach (var item in listaItens)
            {
                email += "<tr><td>" + item.Nome + "</td><td>" + item.DiaAnterior + "</td><td>" + item.Impedimento + "</td><td>" + item.DiaHoje + "</td></tr>";
            }

            email += "</tbody></table>";

            return email;
        }
    }
}