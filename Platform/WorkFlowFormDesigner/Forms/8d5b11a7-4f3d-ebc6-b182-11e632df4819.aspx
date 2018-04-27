<%@ Page Language="C#"%>
<%
	string FlowID = Request.QueryString["flowid"];
	string StepID = Request.QueryString["stepid"];
	string GroupID = Request.QueryString["groupid"];
	string TaskID = Request.QueryString["taskid"];
	string InstanceID = Request.QueryString["instanceid"];
	string DisplayModel = Request.QueryString["display"] ?? "0";
	string DBConnID = "06075250-30dc-4d32-bf97-e922cb30fac8";
	string DBTable = "TempTest_PurchaseList";
	string DBTablePK = "ID";
	string DBTableTitle = "Name";
	if(InstanceID.IsNullOrEmpty()){InstanceID = Request.QueryString["instanceid1"];}
	RoadFlow.Platform.Dictionary BDictionary = new RoadFlow.Platform.Dictionary();
	RoadFlow.Platform.WorkFlow BWorkFlow = new RoadFlow.Platform.WorkFlow();
	RoadFlow.Platform.WorkFlowTask BWorkFlowTask = new RoadFlow.Platform.WorkFlowTask();
	string fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);
	LitJson.JsonData initData = BWorkFlow.GetFormData(DBConnID, DBTable, DBTablePK, InstanceID, fieldStatus, "{\"temptest_purchaselist.date\":\"yyyy-MM-dd\"}", TaskID);
	string TaskTitle = BWorkFlow.GetFromFieldData(initData, DBTable, DBTableTitle);
%>
<link href="Scripts/Forms/flowform.css" rel="stylesheet" type="text/css" />
<script src="Scripts/Forms/common.js" type="text/javascript" ></script>
<input type="hidden" id="Form_ValidateAlertType" name="Form_ValidateAlertType" value="1" />
<input type="hidden" id="Form_TitleField" name="Form_TitleField" value="TempTest_PurchaseList.Name" />
<input type="hidden" id="Form_DBConnID" name="Form_DBConnID" value="06075250-30dc-4d32-bf97-e922cb30fac8" />
<input type="hidden" id="Form_DBTable" name="Form_DBTable" value="TempTest_PurchaseList" />
<input type="hidden" id="Form_DBTablePk" name="Form_DBTablePk" value="ID" />
<input type="hidden" id="Form_DBTableTitle" name="Form_DBTableTitle" value="Name" />
<input type="hidden" id="Form_AutoSaveData" name="Form_AutoSaveData" value="1" />
<script type="text/javascript">
	var initData = <%=BWorkFlow.GetFormDataJsonString(initData)%>;
	var fieldStatus = "1"=="<%=Request.QueryString["isreadonly"]%>"? {} : <%=fieldStatus%>;
	var displayModel = '<%=DisplayModel%>';
	$(window).load(function (){
		formrun.initData(initData, "TempTest_PurchaseList", fieldStatus, displayModel);
	});
</script>
<table class="flowformtable" cellspacing="1" cellpadding="0" data-sort="sortDisabled"><tbody><tr class="firstRow"><td style="WORD-BREAK: break-all" valign="top" width="152">物资名称：<br/></td><td valign="top" width="366"><input id="TempTest_PurchaseList.Name" value="" name="TempTest_PurchaseList.Name" type1="flow_text" valuetype="0" isflow="1" class="mytext" title=""/></td><td style="WORD-BREAK: break-all" valign="top" width="98">型号：<br/></td><td valign="top" width="420"><input id="TempTest_PurchaseList.Model" value="" name="TempTest_PurchaseList.Model" type1="flow_text" valuetype="0" isflow="1" class="mytext" title=""/></td></tr><tr><td style="WORD-BREAK: break-all" valign="top" width="152">单位：<br/></td><td valign="top" width="366"><input id="TempTest_PurchaseList.Unit" value="" name="TempTest_PurchaseList.Unit" type1="flow_text" valuetype="0" isflow="1" class="mytext" title=""/></td><td style="WORD-BREAK: break-all" valign="top" width="98">数量：<br/></td><td valign="top" width="420"><input id="TempTest_PurchaseList.Quantity" value="" name="TempTest_PurchaseList.Quantity" type1="flow_text" valuetype="4" isflow="1" class="mytext" title=""/></td></tr><tr><td style="WORD-BREAK: break-all" valign="top" width="152">要求日期：</td><td valign="top" width="366"><input type="text" type1="flow_datetime" id="TempTest_PurchaseList.Date" name="TempTest_PurchaseList.Date" value="" format="yyyy-MM-dd" defaultvalue="" istime="0" daybefor="0" dayafter="0" currentmonth="0" isflow="1" class="mycalendar" title=""/></td><td style="WORD-BREAK: break-all" valign="top" width="98">类型：<br/></td><td valign="top" width="420"><select class="myselect" id="TempTest_PurchaseList.Type" name="TempTest_PurchaseList.Type" isflow="1" type1="flow_select"><option value="办公用品">办公用品</option><option value="办公家具">办公家具</option><option value="电器">电器</option></select></td></tr><tr><td style="WORD-BREAK: break-all" valign="top" width="152">备注说明：</td><td valign="top" width="107" colspan="3"><input id="TempTest_PurchaseList.Note" value="" name="TempTest_PurchaseList.Note" type1="flow_files" filetype="" isflow="1" class="myfile" title=""/></td></tr></tbody></table><p><br/></p>