<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintFordTag1.aspx.cs" Inherits="PrintFordTag1" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ford Tag1</title>
    <script src="../Content/js/jquery.min.js"></script>
    <script>
        $(document).ready(function () {
            $("#btnPrint").click(function () {
                $("#btnPrint").css("display", "none");
                window.print();
               // printdiv("Repeater1");
            })

        })
        function printdiv(printpage) {
            var headstr = "<html><head></head><body>";
            var footstr = "</body>";
            var newstr = document.all.item(printpage).innerHTML;
            var oldstr = document.body.innerHTML;
            document.body.innerHTML = headstr + newstr + footstr;
            window.print();
            document.body.innerHTML = oldstr;
            return false;
        }
    </script>
    <style>
        td {
            font-family: Arial;
            font-size: 10px;
            border-color: gray;
        }

        .td_left {
            border-left-color: black;
        }

        .td_bottom {
            border-bottom-color: black;
        }
        .value {
            padding-bottom:3px;padding-top:2px
        }
    </style>
</head>
<body style="margin-left: 0px; margin-right: 0px; margin-top: 20px; margin-bottom: 0px">
    <form id="form1" runat="server">
        <input type="button" id="btnPrint" value="打印" />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:DataList ID="Repeater1" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
            <ItemTemplate>
                <div style="padding-bottom: 58px; margin-left: 25px; margin-right: 25px">
                    <table style="border-collapse: collapse; border-color: black; width: 320px; height: 190px" border="2">
                        <tr>
                            <td align="center" style="border-bottom:2px solid black">
                                <img src="FordFlag.png" /></td>
                            <td colspan="9" style="background-color: gray; color: white; font-size: 9px; border-bottom:2px solid black; font-family: Arial" align="center"><strong>PROTOTYPE & FUNCTIONAL BUILD MATERIAL</strong></td>

                        </tr>
                        <tr>
                            <td align="right" style="width: 80px;">Prifix</td>
                            <td align="center" style="width: 65px">Base</td>
                            <td style="border-right-color: black;">Suffix</td>
                            <td rowspan="4" style="border-right:2px solid black; border-left:2px solid  black; border-bottom-color: white; padding-left: 7px">&nbsp;</td>
                            <td align="center" style="border-top:2px solid black">FC</td>
                            <td align="center" style="border-top:2px solid black">MI</td>
                            <td align="center" style="border-top:2px solid black">RL</td>
                            <td align="center" style="border-top:2px solid black">TC</td>
                            <td align="center" style="border-top:2px solid black">TT</td>
                            <td align="center" style="width: 90px; text-wrap: none;border-top:2px solid black">Serial No.</td>
                        </tr>
                        <tr>
                            <td align="right" style="height:20px"><%#Eval("ljh1")%></td>
                            <td align="center"><%#Eval("ljh2")%></td>
                            <td align="left" style="border-right-color: black;"><%#Eval("ljh3")%></td>
                            <td align="center">-</td>
                            <td align="center">-</td>
                            <td align="center">-</td>
                            <td align="center">-</td>
                            <td align="center">W</td>
                            <td align="center"><%#Eval("SerialNo")%></td>
                        </tr>
                        <tr>
                            <td colspan="3" valign="top" style="border-right-color: black;">Description:<br />

                                <div class="value"><%#Eval("ljmc")%><br /></div>
                            </td>
                            <td colspan="6" rowspan="2" class="td_bottom" valign="top" style="border-bottom:2px solid black">Supplier Information:<br />
                               <div class="value"> <%#Eval("SupInfo")%><br />
                                <%#Eval("who")%><br />
                                <%#Eval("phoneNo")%><br /></div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="td_bottom" style="border-bottom:2px solid black">P.O./Req.Number:<br />
                               <div class="value"> <%#Eval("gkddh")%></div>
                            </td>
                            <td class="td_bottom" style="border-right-color: black; width: 80px;border-bottom:2px solid black"  >Line Code:<br />
                                <div class="value"><%#Eval("LineCode")%></div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="10" valign="top">P/N Bar Code (P)<br />
                                <img src="http://172.16.5.26:8090/<%# Eval("ljh") %>/1.jpg"  style=" margin-left:8px; margin-bottom:3px" width="300px" height="65px" alt="若无图片显示,请销售(张荣新)提供条码图至服务器" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="10" style="background-color: gray; height: 10px"></td>
                        </tr>
                    </table>

                </div>
            </ItemTemplate>
        </asp:DataList>



        <div style="visibility: hidden">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="400px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="600px" ZoomMode="PageWidth" DocumentMapWidth="100%">
                <LocalReport ReportPath="YangJian\FordTag1.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSetFordTag1" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>

            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="MESDataSetTableAdapters.print_form1_Sale_YJ_FordTag1TableAdapter">
                <SelectParameters>
                    <asp:Parameter Name="requestid" Type="String" />
                    <asp:Parameter Name="PrintCount" Type="Int32"></asp:Parameter>
                </SelectParameters>

            </asp:ObjectDataSource>

        </div>
    </form>
</body>
</html>
