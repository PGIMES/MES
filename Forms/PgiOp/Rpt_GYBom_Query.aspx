<%@ Page Title="工艺数据状态查询" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Rpt_GYBom_Query.aspx.cs" Inherits="Forms_PgiOp_Rpt_GYBom_Query" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script type="text/javascript" src="/Content/js/jquery.min.js"></script>
    <script type="text/javascript" src="/Content/js/jquery.cookie.min.js"></script>
    <script src="/Content/js/plugins/layer/laydate/laydate.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#mestitle").text("【工艺数据状态查询】");
            setHeight();
            $(window).resize(function () {
                setHeight();
            });

        });


        function setHeight() {
            $("div[class=dxgvCSD]").css("height", ($(window).height() - $("#div_p").height() - 300) + "px");
        }
        	
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <div class="col-md-12"  id="div_p">
        <div class="row  row-container">            
            <div class="panel panel-info">
                <div class="panel-heading" >
                    <strong>查询</strong>
                </div>
                <div class="panel-body collapse in" id="CPXX" >
                    <div class="col-xs-12 col-sm-12  col-md-12 col-lg-12" style="font-size:13px;">
                        <table class="tblCondition" style=" border-collapse: collapse;">
                            <tr>
                               <td style="width:130px;">零件号/项目号：</td>
                                <td style="width:120px;">
                                    <asp:TextBox ID="txt_part" class="form-control input-s-sm" runat="server" Width="110px"></asp:TextBox>
                                </td>
                               
                                <td style="width:70px;">类别:</td>
                                <td style="width:100px;"> 
                                    <asp:DropDownList ID="ddl_type" runat="server" class="form-control input-s-md " Width="80px">
                                        <asp:ListItem Value="GY">工艺</asp:ListItem>
                                        <asp:ListItem Value="BOM">BOM</asp:ListItem>
                                         <asp:ListItem Value="part">物料</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                    <td style="width:70px;">工厂:</td>
                                <td style="width:100px;"> 
                                    <asp:DropDownList ID="ddl_comp" runat="server" class="form-control input-s-md " Width="120px">
                                        <asp:ListItem Value="">ALL</asp:ListItem>
                                        <asp:ListItem Value="200">昆山工厂</asp:ListItem>
                                        <asp:ListItem Value="100">上海工厂</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>  
                                    &nbsp;&nbsp;
                                    <button id="btn_search" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_search_Click"><i class="fa fa-search fa-fw"></i>&nbsp;查询</button>    
                                     <button id="btn_import" type="button" class="btn btn-primary btn-large" runat="server" onserverclick="btn_import_Click"><i class="fa fa-download fa-fw"></i>&nbsp;导出</button>
                                </td>
                            </tr>                      
                        </table>
                    </div>
                </div>
            </div>            
        </div>
    </div>

    <div class="col-sm-12">
        <table>
            <tr>
                <td><%-- OnHtmlDataCellPrepared="gv_HtmlDataCellPrepared"--%>
                    <dx:ASPxGridView ID="gv" runat="server" 
                        AutoGenerateColumns="False" Width="1200px" 
                        OnPageIndexChanged="gv_PageIndexChanged" 
                        oncustomcellmerge="gv_CustomCellMerge"    >
                        
                        <SettingsPager PageSize="100" ></SettingsPager>
                        <Settings ShowFilterRow="True" ShowGroupPanel="false" 
                            ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True"  VerticalScrollBarStyle="Standard" VerticalScrollableHeight="500"
                            AutoFilterCondition="Contains" ShowFooter="True"  />
                        <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control"  AllowCellMerge="True"  />
                        <Columns>
                                                      
                             <dx:GridViewDataTextColumn Caption="申请单号" 
                                 FieldName="FormNo" Width="100px" VisibleIndex="1" >
                               <%-- <Settings AllowCellMerge="True" /> --%>
                                <DataItemTemplate>
                                    <dx:ASPxHyperLink ID="hpl_FormNo" runat="server" Text='<%# Eval("FormNo")%>' Cursor="pointer" ClientInstanceName='<%# "FormNo"+Container.VisibleIndex.ToString() %>'  
                                         NavigateUrl='<%# "/Platform/WorkFlowRun/Default.aspx?flowid=ee59e0b3-d6a1-4a30-a3b4-65d188323134&display=1&appid=13093704-4425-4713-B3E1-81851C6F96CD&GroupID="+ Eval("GroupID")+"&InstanceID="+ Eval("FormNo") %>'
                                         Target="_blank"
                                        >                                        
                                    </dx:ASPxHyperLink>
                                </DataItemTemplate> 
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="项目号" FieldName="Projectno" Width="90px" VisibleIndex="2"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="零件号" FieldName="pn" Width="120px" VisibleIndex="3"> </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="工艺路线版本" FieldName="ver" Width="100px" VisibleIndex="4"> </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="工艺流程" FieldName="pgi_no_t" Width="120px" VisibleIndex="5"> </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn Caption="工序" FieldName="op" Width="60px" VisibleIndex="6"></dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn Caption="工序描述" FieldName="op_desc" Width="100px" VisibleIndex="7"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="工作中心" FieldName="gzzx" Width="80px" VisibleIndex="8"> </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="工厂" FieldName="domain" Width="80px" VisibleIndex="9"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="申请人" FieldName="createByName" Width="80px" VisibleIndex="10"></dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn Caption="申请时间" FieldName="createDate" Width="80px" VisibleIndex="11"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="当前签核人" FieldName="GY_currnode" Width="100px" VisibleIndex="12"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="批准日期" FieldName="gy_approvetime" Width="80px" VisibleIndex="13"></dx:GridViewDataTextColumn>
                            
                       
                           
                        </Columns>
                        
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                            <Footer HorizontalAlign="Right"></Footer>
                        </Styles>
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server">
                    </dx:ASPxGridViewExporter>
                    <br />
                    <dx:ASPxGridView ID="gv_BOM" runat="server" 
                        AutoGenerateColumns="False" Width="1100px" 
                        oncustomcellmerge="gv_BOM_CustomCellMerge">
                     
                        <SettingsPager PageSize="100" ></SettingsPager>
                        <Settings ShowFilterRow="True" ShowGroupPanel="false" 
                            ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True"  VerticalScrollBarStyle="Standard" VerticalScrollableHeight="500"
                            AutoFilterCondition="Contains" ShowFooter="True"  />
                        <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control" AllowCellMerge="True"  />
                        <Columns>
                                                      
                             <dx:GridViewDataTextColumn Caption="申请单号" 
                                 FieldName="aplno" Width="120px" VisibleIndex="1" >
                                <%--<Settings AllowCellMerge="True" />--%> 
                                <DataItemTemplate>
                                    <dx:ASPxHyperLink ID="hpl_FormNo0" runat="server" 
                                        Text='<%# Eval("aplno")%>' Cursor="pointer" ClientInstanceName='<%# "FormNo"+Container.VisibleIndex.ToString() %>'  
                                         NavigateUrl='<%# "/Platform/WorkFlowRun/Default.aspx?flowid=4a901bc7-ea83-43b1-80b6-5b14708dede9&appid=BDDCD717-2DD6-4D83-828C-71C92FFF6AE4&state=edit&InstanceID="+ Eval("aplno") %>'
                                         Target="_blank"
                                        >                                        
                                    </dx:ASPxHyperLink>
                                </DataItemTemplate> 
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="项目号" FieldName="pgino" Width="90px" VisibleIndex="2">  <Settings AllowCellMerge="True" /> </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="物料号" FieldName="pt_part" Width="90px" VisibleIndex="3"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="版本" FieldName="bomver" Width="90px" VisibleIndex="4"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="零件号" FieldName="pt_desc1" Width="120px" VisibleIndex="5"> </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn Caption="件数" FieldName="qty" Width="60px" VisibleIndex="6"> 
                                 <CellStyle HorizontalAlign="Right">
                                 </CellStyle>
                             </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn Caption="单件用量" FieldName="ps_qty_per" Width="80px" VisibleIndex="7"> </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="工厂" FieldName="domain" Width="80px" VisibleIndex="8"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="申请人" FieldName="CreateByName" Width="80px" VisibleIndex="9"></dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn Caption="申请时间" FieldName="createDate" Width="80px" VisibleIndex="10"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="当前签核人" FieldName="bom_currnode" Width="80px" VisibleIndex="11"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="批准日期" FieldName="approvetime" Width="100px" VisibleIndex="12"></dx:GridViewDataTextColumn>
                            
                       
                           
                        </Columns>
                        
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                            <Footer HorizontalAlign="Right"></Footer>
                        </Styles>
                    </dx:ASPxGridView>
                    <br/>
                               <dx:ASPxGridView ID="gv_Part" runat="server" 
                        AutoGenerateColumns="False" Width="1100px" 
                        oncustomcellmerge="gv_Part_CustomCellMerge" 
                        onpageindexchanged="gv_Part_PageIndexChanged">
                     
                        <SettingsPager PageSize="100" ></SettingsPager>
                        <Settings ShowFilterRow="True" ShowGroupPanel="false" 
                            ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True"  VerticalScrollBarStyle="Standard" VerticalScrollableHeight="500"
                            AutoFilterCondition="Contains" ShowFooter="True"  />
                        <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control" AllowCellMerge="True"  />
                        <Columns>
                                                      
                             <dx:GridViewDataTextColumn Caption="申请单号" 
                                 FieldName="formno" Width="120px" VisibleIndex="1" >
                                <%--<Settings AllowCellMerge="True" />--%> 
                                <DataItemTemplate>
                                    <dx:ASPxHyperLink ID="hpl_FormNo0" runat="server" 
                                        Text='<%# Eval("formno")%>' Cursor="pointer" ClientInstanceName='<%# "FormNo"+Container.VisibleIndex.ToString() %>'  
                                         NavigateUrl='<%# "/Platform/WorkFlowRun/Default.aspx?flowid=d9cb9476-13f9-48ec-a87e-5b84ca0790b0&appid=33C8FB5D-CB37-4667-AE06-69A87A23542E&domain="+ Eval("domain")+"&gc="+Eval("gc_version")+"&bz="+Eval("bz_version")+"&display=1&wlh="+Eval("wlh")+"&Instanceid="+ Eval("formno") %>'
                                         Target="_blank"
                                        >                                        
                                    </dx:ASPxHyperLink>
                                </DataItemTemplate> 
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="物料编码" FieldName="part_no" Width="90px" VisibleIndex="2">  <Settings AllowCellMerge="True" /> </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="项目号" FieldName="wlh" Width="90px" VisibleIndex="3"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="工程版本" FieldName="gc_version" Width="90px" VisibleIndex="4"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="包装版本" FieldName="bz_version" Width="120px" VisibleIndex="5"> </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn Caption="物料状态" FieldName="status" Width="60px" VisibleIndex="6"> 
                                 <CellStyle HorizontalAlign="Right">
                                 </CellStyle>
                             </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="工厂" FieldName="domain" Width="80px" VisibleIndex="8"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="申请人" FieldName="createbyname" Width="80px" VisibleIndex="9"></dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn Caption="申请时间" FieldName="createDate" Width="80px" VisibleIndex="10"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="当前签核人" FieldName="part_currnode" Width="80px" VisibleIndex="11"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="批准日期" FieldName="approvetime" Width="100px" VisibleIndex="12"></dx:GridViewDataTextColumn>
                            
                       
                           
                        </Columns>
                        
                        <Styles>
                            <Header BackColor="#99CCFF"></Header>
                            <FocusedRow BackColor="#99CCFF" ForeColor="#0000CC"></FocusedRow>
                            <Footer HorizontalAlign="Right"></Footer>
                        </Styles>
                    </dx:ASPxGridView>
                </td>
            </tr>
        </table>
    </div>
    


</asp:Content>

