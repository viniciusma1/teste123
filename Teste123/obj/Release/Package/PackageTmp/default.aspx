<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/ReuniaoDiaria.Master" CodeBehind="default.aspx.cs" Inherits="Teste123.ReuniãoDiária.SalvarFormulário" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">

    <script type="text/javascript" src="../Content/scriptteste.js"></script>

    <link rel="stylesheet" href="Content/formulário.css" />

    <div class="divCadastrar">
        <asp:Panel ID="loginPanel" runat="server" DefaultButton="BtnEnviar">
            <br />

            <table border="4" class="tablee">

                <thead>
                    <tr class="aas">
                        <th>Responsável Reunião Diária</th>

                        <th>
                            <asp:DropDownList ID="ddlResponsavel" CssClass="abc" AutoPostBack="true" DataValueField="Login" DataTextField="Nome" OnSelectedIndexChanged="ddlResponsavel_SelectedIndexChanged" runat="server"></asp:DropDownList>                                                       
                        </th>

                        <asp:HiddenField ID="hfResponsavel" runat="server" Value="1" />
                    </tr>
                </thead>

            </table>

            <br />

            <table border="5">

                <thead>
                    <tr>
                        <th>
                            <asp:Label ID="Data" runat="server"></asp:Label></th>

                        <th>Tarefas dia Anterior</th>

                        <th>Impedimento?</th>

                        <th>Tarefas hoje</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlNome" DataValueField="ID" DataTextField="Nome" CssClass="abc" runat="server"></asp:DropDownList></td>
                        <td>
                            <asp:TextBox ID="txtDiaAnterior" CssClass="ab" runat="server" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDiaAnterior" runat="server" ForeColor="Red" ValidationGroup="ItemReuniao" ErrorMessage="Preencha este Campo" ControlToValidate="txtDiaAnterior"></asp:RequiredFieldValidator>

                        </td>

                        <td>
                            <asp:TextBox ID="txtImpedimento" CssClass="abccc" runat="server" TextMode="MultiLine">Sem Impedimento</asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvImpedimento" runat="server" ForeColor="Red" ValidationGroup="ItemReuniao" ErrorMessage="*" ControlToValidate="txtImpedimento"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDiaHoje" CssClass="abcc" runat="server" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDiaHoje" runat="server" ForeColor="Red" ValidationGroup="ItemReuniao" ErrorMessage="Preencha este Campo" ControlToValidate="txtDiaHoje"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </tbody>
            </table>

            <div style="position: relative; margin-top: 1%; float: right;">
                <asp:Button runat="server" ID="BtnEnviar" CssClass="a" ValidationGroup="ItemReuniao" Text="Cadastrar" OnClick="BtnEnviar_Click" />
            </div>
        </asp:Panel>
    </div>

    <div class="divResultado">

        <div class="input-control text" data-role="datepicker" data-format="dd/mm/yyyy" data-locale="pt" data-scheme="darcula">
            <asp:TextBox ID="txtDate" runat="server" AutoPostBack="true" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
            <button id="botaocalen" runat="server" class="button"><span class="mif-calendar"></span></button>
        </div></>

        <asp:Repeater runat="server" ID="rptReuniao" OnItemCommand="rptReuniao_ItemCommand">
            <HeaderTemplate>
                <table border="5">
                    <tr>
                        <th>Participante</th>
                        <th>Tarefas dia Anterior</th>
                        <th>Impedimento?</th>
                        <th>Tarefas hoje</th>
                        <th>Ação</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("Nome")%></td>

                    <td><%# Eval("DiaAnterior")%></td>

                    <td><%# Eval("Impedimento")%> </td>

                    <td><%# Eval("DiaHoje")%></td>

                    <td>
                        <asp:Button ID="btnExcluirTarefas" runat="server" CssClass="Remover" CommandName='<%# Eval("IDNome")%>' CommandArgument='<%# Eval("Data")%>' Text="Excluir" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

        <div style="float: right; position: relative; margin-top: 1%;">

            <asp:Button ID="btnEmail" runat="server" Text="Enviar Email" CssClass="a" OnClick="Button_Click" OnClientClick="EvitarEmail()" />
        </div>
    </div>

    <script>
        function EvitarEmail() {
            if (!confirm('Todos estão cadastrado?'))
                event.preventDefault();
        }
       
        $(function () {
            $(".datepicker").datepicker({ dateFormat: 'dd/mm/yy' });

            $('<%= btnEmail.ClientID %>').click(function () {


            });
            $('textarea').on("keyup", function (e) {
                $(this).val(   $(this).val().replace(/'/g, ""));
                $(this).val($(this).val().replace(/"/g, ""));
            });
        });
    </script>

</asp:Content>
