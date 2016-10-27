<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/ReuniaoDiaria.Master" CodeBehind="FormulárioReunião.aspx.cs" Inherits="Teste123.ReuniãoDiária.FormulárioReunião" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">

    <link rel="stylesheet" href="../Content/formulário.css" />
    <asp:Repeater runat="server" ID="rptReuniao">
        <HeaderTemplate>
            <table border="5">
                <tr>
                    <th><%# DateTime.Now.ToShortDateString() %></th>
                    <th>Tarefas dia Anterior</th>
                    <th>Impedimento?</th>
                    <th>Tarefas hoje</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%# Eval("Nome")%>
                </td>
                <td>
                    <%# Eval("DiaAnterior")%></td>
                <td>
                    <%# Eval("Impedimento")%> </td>
                <td>
                    <%# Eval("DiaHoje")%></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
  
    <br />
    
 

</asp:Content>
