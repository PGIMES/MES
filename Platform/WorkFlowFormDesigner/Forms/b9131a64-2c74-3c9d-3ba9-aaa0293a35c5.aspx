<%@ Page Language="C#"%>
<%
	string FlowID = Request.QueryString["flowid"];
	string StepID = Request.QueryString["stepid"];
	string GroupID = Request.QueryString["groupid"];
	string TaskID = Request.QueryString["taskid"];
	string InstanceID = Request.QueryString["instanceid"];
	string DisplayModel = Request.QueryString["display"] ?? "0";
	string DBConnID = "06075250-30dc-4d32-bf97-e922cb30fac8";
	string DBTable = "TempTest_Purchase";
	string DBTablePK = "ID";
	string DBTableTitle = "Title";
	if(InstanceID.IsNullOrEmpty()){InstanceID = Request.QueryString["instanceid1"];}
	RoadFlow.Platform.Dictionary BDictionary = new RoadFlow.Platform.Dictionary();
	RoadFlow.Platform.WorkFlow BWorkFlow = new RoadFlow.Platform.WorkFlow();
	RoadFlow.Platform.WorkFlowTask BWorkFlowTask = new RoadFlow.Platform.WorkFlowTask();
	string fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);
	LitJson.JsonData initData = BWorkFlow.GetFormData(DBConnID, DBTable, DBTablePK, InstanceID, fieldStatus, "{}", TaskID);
	string TaskTitle = BWorkFlow.GetFromFieldData(initData, DBTable, DBTableTitle);
%>
<link href="Scripts/Forms/flowform.css" rel="stylesheet" type="text/css" />
<script src="Scripts/Forms/common.js" type="text/javascript" ></script>
<input type="hidden" id="Form_ValidateAlertType" name="Form_ValidateAlertType" value="1" />
<input type="hidden" id="Form_TitleField" name="Form_TitleField" value="TempTest_Purchase.Title" />
<input type="hidden" id="Form_DBConnID" name="Form_DBConnID" value="06075250-30dc-4d32-bf97-e922cb30fac8" />
<input type="hidden" id="Form_DBTable" name="Form_DBTable" value="TempTest_Purchase" />
<input type="hidden" id="Form_DBTablePk" name="Form_DBTablePk" value="ID" />
<input type="hidden" id="Form_DBTableTitle" name="Form_DBTableTitle" value="Title" />
<input type="hidden" id="Form_AutoSaveData" name="Form_AutoSaveData" value="1" />
<script type="text/javascript">
	var initData = <%=BWorkFlow.GetFormDataJsonString(initData)%>;
	var fieldStatus = "1"=="<%=Request.QueryString["isreadonly"]%>"? {} : <%=fieldStatus%>;
	var displayModel = '<%=DisplayModel%>';
	$(window).load(function (){
		formrun.initData(initData, "TempTest_Purchase", fieldStatus, displayModel);
	});
</script>
<p><br/></p><p><br/></p><table class="flowformtable" cellpadding="0" cellspacing="1" data-sort="sortDisabled"><tbody><tr class="firstRow"><td width="150" valign="top" style="word-break: break-all;">申请人：</td><td width="381" valign="top"><input type="text" type1="flow_org" id="TempTest_Purchase.UserID" name="TempTest_Purchase.UserID" value="u_<%=RoadFlow.Platform.Users.CurrentUserID.ToString()%>" more="0" isflow="1" class="mymember" title="" dept="0" station="0" user="0" workgroup="0" unit="0" rootid=""/></td><td width="143" valign="top" style="word-break: break-all;">部门：</td><td width="389" valign="top"><input type="text" type1="flow_org" id="TempTest_Purchase.UserDept" name="TempTest_Purchase.UserDept" value="<%=RoadFlow.Platform.Users.CurrentDeptID%>" more="0" isflow="1" class="mymember" title="" dept="0" station="0" user="0" workgroup="0" unit="0" rootid=""/></td></tr><tr><td width="150" valign="top" style="word-break: break-all;">申请时间：</td><td width="381" valign="top"><input type="text" type1="flow_datetime" id="TempTest_Purchase.SqDateTime" name="TempTest_Purchase.SqDateTime" value="<%=RoadFlow.Utility.DateTimeNew.ShortDate%>" defaultvalue="%3C%25=RoadFlow.Utility.DateTimeNew.ShortDate%25%3E" istime="0" daybefor="0" dayafter="0" currentmonth="0" isflow="1" class="mycalendar" title=""/></td><td width="143" valign="top"><br/></td><td width="389" valign="top"><br/></td></tr><tr><td valign="top" colspan="4" rowspan="1" style="word-break: break-all;">  <input type="button" class="mybutton" type1="flow_button" isflow="1" value="查询数据" onclick="onclick_7ef63f089f3db0f42a657eebdea08c34 (this);"/><script type="text/javascript">function onclick_7ef63f089f3db0f42a657eebdea08c34(srcObj){var tabid='<%=Request.QueryString["tabid"]%>';
new RoadUI.Window().open({url:"/tests/testselect.aspx",width:800,height:500,title:"选择数据",openerid:tabid})}</script></td></tr><tr><td valign="top" colspan="4" rowspan="1"><table class="flowformsubtable" style="width:99%;margin:0 auto;" cellpadding="0" cellspacing="1" issubflowtable="1" id="subtable_TempTest_PurchaseList_ID_ID_PurchaseID"><thead><tr><th>名称<input type="hidden" name="flowsubtable_id" value="TempTest_PurchaseList_ID_ID_PurchaseID"/><input type="hidden" name="flowsubtable_TempTest_PurchaseList_ID_ID_PurchaseID_secondtable" value="TempTest_PurchaseList"/><input type="hidden" name="flowsubtable_TempTest_PurchaseList_ID_ID_PurchaseID_primarytablefiled" value="ID"/><input type="hidden" name="flowsubtable_TempTest_PurchaseList_ID_ID_PurchaseID_secondtableprimarykey" value="ID"/><input type="hidden" name="flowsubtable_TempTest_PurchaseList_ID_ID_PurchaseID_secondtablerelationfield" value="PurchaseID"/></th><th>型号</th><th>单位</th><th>数量</th><th></th></tr></thead><tbody><tr type1="listtr"><td colname="TempTest_PurchaseList_Name" iscount="0"><input type="hidden" name="hidden_guid_TempTest_PurchaseList_ID_ID_PurchaseID" value="6223a78c162fa2b8194e3d5afa035786"/><input type="hidden" name="flowsubid" value="TempTest_PurchaseList_ID_ID_PurchaseID"/><input type="text" class="mytext" issubflow="1" type1="subflow_text" name="TempTest_PurchaseList_ID_ID_PurchaseID_6223a78c162fa2b8194e3d5afa035786_TempTest_PurchaseList_Name" id="TempTest_PurchaseList_ID_ID_PurchaseID_6223a78c162fa2b8194e3d5afa035786_TempTest_PurchaseList_Name" colname="TempTest_PurchaseList_Name" value="" defaultvalue="" valuetype="0"/></td><td colname="TempTest_PurchaseList_Model" iscount="0"><input type="text" class="mytext" issubflow="1" type1="subflow_text" name="TempTest_PurchaseList_ID_ID_PurchaseID_6223a78c162fa2b8194e3d5afa035786_TempTest_PurchaseList_Model" id="TempTest_PurchaseList_ID_ID_PurchaseID_6223a78c162fa2b8194e3d5afa035786_TempTest_PurchaseList_Model" colname="TempTest_PurchaseList_Model" value="" defaultvalue="" valuetype="0"/></td><td colname="TempTest_PurchaseList_Unit" iscount="0"><input type="text" class="mytext" issubflow="1" type1="subflow_text" name="TempTest_PurchaseList_ID_ID_PurchaseID_6223a78c162fa2b8194e3d5afa035786_TempTest_PurchaseList_Unit" id="TempTest_PurchaseList_ID_ID_PurchaseID_6223a78c162fa2b8194e3d5afa035786_TempTest_PurchaseList_Unit" colname="TempTest_PurchaseList_Unit" value="" defaultvalue="" valuetype="0"/></td><td colname="TempTest_PurchaseList_Quantity" iscount="1"><input type="text" class="mytext" issubflow="1" type1="subflow_text" name="TempTest_PurchaseList_ID_ID_PurchaseID_6223a78c162fa2b8194e3d5afa035786_TempTest_PurchaseList_Quantity" id="TempTest_PurchaseList_ID_ID_PurchaseID_6223a78c162fa2b8194e3d5afa035786_TempTest_PurchaseList_Quantity" colname="TempTest_PurchaseList_Quantity" value="" defaultvalue="" iscount="1" onblur="formrun.subtableCount('TempTest_PurchaseList_ID_ID_PurchaseID','TempTest_PurchaseList_Quantity','countspan_TempTest_PurchaseList_ID_ID_PurchaseID_TempTest_PurchaseList_Quantity');" valuetype="0"/></td><td><input type="button" class="mybutton" style="margin-right:4px;" value="增加" onclick="formrun.subtableNewRow(this);"/><input type="button" class="mybutton" value="删除" onclick="formrun.subtableDeleteRow(this);"/></td></tr><tr type1="counttr"><td colspan="11" align="right" style="padding-right:20px; text-align:right;"><span style="margin-right:10px;">数量合计：<label id="countspan_TempTest_PurchaseList_ID_ID_PurchaseID_TempTest_PurchaseList_Quantity">0</label></span></td></tr></tbody></table></td></tr></tbody></table><p><br/></p><script>function addrow(json)
  {
    var json1=eval(json);
    for(var i=0;i<json1.length;i++)
    {
        var tr=$(".mybutton",$("#subtable_TempTest_PurchaseList_ID_ID_PurchaseID tbody")).get(0);
        var $newtr = formrun.subtableNewRow(tr,'',false,false);
        $("[id$='_Name']",$newtr).val(json1[i].name);
        $("[id$='_Model']",$newtr).val(json1[i].model);
        $("[id$='_Unit']",$newtr).val(json1[i].unit);
        $("[id$='_Quantity']",$newtr).val(json1[i].quantity);
        new RoadUI.Window().close();
    }
  }</script>