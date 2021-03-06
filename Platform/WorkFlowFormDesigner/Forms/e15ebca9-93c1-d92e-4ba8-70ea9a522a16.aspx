﻿<%@ Page Language="C#"%>
<%
	string FlowID = Request.QueryString["flowid"];
	string StepID = Request.QueryString["stepid"];
	string GroupID = Request.QueryString["groupid"];
	string TaskID = Request.QueryString["taskid"];
	string InstanceID = Request.QueryString["instanceid"];
	string DisplayModel = Request.QueryString["display"] ?? "0";
	string DBConnID = "06075250-30dc-4d32-bf97-e922cb30fac8";
	string DBTable = "TempTest_CustomForm";
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
<input type="hidden" id="Form_TitleField" name="Form_TitleField" value="TempTest_CustomForm.Title" />
<input type="hidden" id="Form_DBConnID" name="Form_DBConnID" value="06075250-30dc-4d32-bf97-e922cb30fac8" />
<input type="hidden" id="Form_DBTable" name="Form_DBTable" value="TempTest_CustomForm" />
<input type="hidden" id="Form_DBTablePk" name="Form_DBTablePk" value="ID" />
<input type="hidden" id="Form_DBTableTitle" name="Form_DBTableTitle" value="Title" />
<input type="hidden" id="Form_AutoSaveData" name="Form_AutoSaveData" value="1" />
<script type="text/javascript">
	var initData = <%=BWorkFlow.GetFormDataJsonString(initData)%>;
	var fieldStatus = "1"=="<%=Request.QueryString["isreadonly"]%>"? {} : <%=fieldStatus%>;
	var displayModel = '<%=DisplayModel%>';
	$(window).load(function (){
		formrun.initData(initData, "TempTest_CustomForm", fieldStatus, displayModel);
	});
</script>
<table class="flowformtable" cellpadding="0" cellspacing="1"><tbody><tr class="firstRow"><td width="316" valign="top" style="word-break: break-all;">测试1：</td><td width="1343" valign="top"><input type="text" id="TempTest_CustomForm.Title" type1="flow_text" name="TempTest_CustomForm.Title" value="" align="left" valuetype="0" isflow="1" class="mytext" title=""/></td></tr><tr><td width="316" valign="top" style="word-break: break-all;">测试2：</td><td width="1343" valign="top" style="word-break: break-all;"><input type="text" id="TempTest_CustomForm.f1" type1="flow_text" name="TempTest_CustomForm.f1" value="" align="left" valuetype="0" isflow="1" class="mytext" title=""/></td></tr><tr><td width="316" valign="top" style="word-break: break-all;">测试3：</td><td width="1343" valign="top"><input type="text" id="TempTest_CustomForm.f2" type1="flow_text" name="TempTest_CustomForm.f2" value="" align="left" valuetype="0" isflow="1" class="mytext" title=""/></td></tr></tbody></table><p><br/></p>