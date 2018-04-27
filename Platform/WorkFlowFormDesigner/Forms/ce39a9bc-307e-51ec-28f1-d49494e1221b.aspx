<%@ Page Language="C#"%>
<%
	string FlowID = Request.QueryString["flowid"];
	string StepID = Request.QueryString["stepid"];
	string GroupID = Request.QueryString["groupid"];
	string TaskID = Request.QueryString["taskid"];
	string InstanceID = Request.QueryString["instanceid"];
	string DisplayModel = Request.QueryString["display"] ?? "0";
	string DBConnID = "ade83a9b-1d9e-4a32-a595-c8ebc2f50309";
	string DBTable = "temptest";
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
<input type="hidden" id="Form_TitleField" name="Form_TitleField" value="temptest.Title" />
<input type="hidden" id="Form_DBConnID" name="Form_DBConnID" value="ade83a9b-1d9e-4a32-a595-c8ebc2f50309" />
<input type="hidden" id="Form_DBTable" name="Form_DBTable" value="temptest" />
<input type="hidden" id="Form_DBTablePk" name="Form_DBTablePk" value="ID" />
<input type="hidden" id="Form_DBTableTitle" name="Form_DBTableTitle" value="Title" />
<input type="hidden" id="Form_AutoSaveData" name="Form_AutoSaveData" value="1" />
<script type="text/javascript">
	var initData = <%=BWorkFlow.GetFormDataJsonString(initData)%>;
	var fieldStatus = "1"=="<%=Request.QueryString["isreadonly"]%>"? {} : <%=fieldStatus%>;
	var displayModel = '<%=DisplayModel%>';
	$(window).load(function (){
		formrun.initData(initData, "temptest", fieldStatus, displayModel);
	});
</script>
<p><br/></p><p><br/></p><table class="flowformtable" cellpadding="0" cellspacing="1"><tbody><tr class="firstRow"><td width="194" valign="top" style="word-break: break-all;">标题：</td><td width="614" valign="top"><input type="text" id="temptest.Title" type1="flow_text" name="temptest.Title" value="" valuetype="0" isflow="1" class="mytext" title=""/></td><td width="210" valign="top"><br/></td><td width="598" valign="top"><br/></td></tr><tr><td width="194" valign="top"><br/></td><td width="614" valign="top"><br/></td><td width="210" valign="top"><br/></td><td width="598" valign="top"><br/></td></tr></tbody></table><p><br/></p>