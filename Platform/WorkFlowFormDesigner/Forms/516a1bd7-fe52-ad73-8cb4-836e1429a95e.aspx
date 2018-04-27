<%@ Page Language="C#"%>
<%
	string FlowID = Request.QueryString["flowid"];
	string StepID = Request.QueryString["stepid"];
	string GroupID = Request.QueryString["groupid"];
	string TaskID = Request.QueryString["taskid"];
	string InstanceID = Request.QueryString["instanceid"];
	string DisplayModel = Request.QueryString["display"] ?? "0";
	string DBConnID = "06075250-30dc-4d32-bf97-e922cb30fac8";
	string DBTable = "TempTest";
	string DBTablePK = "ID";
	string DBTableTitle = "Title";
if(InstanceID.IsNullOrEmpty()){InstanceID = Request.QueryString["instanceid1"];}	RoadFlow.Platform.Dictionary BDictionary = new RoadFlow.Platform.Dictionary();
	RoadFlow.Platform.WorkFlow BWorkFlow = new RoadFlow.Platform.WorkFlow();
	RoadFlow.Platform.WorkFlowTask BWorkFlowTask = new RoadFlow.Platform.WorkFlowTask();
	string fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);
	LitJson.JsonData initData = BWorkFlow.GetFormData(DBConnID, DBTable, DBTablePK, InstanceID, fieldStatus);
	string TaskTitle = BWorkFlow.GetFromFieldData(initData, DBTable, DBTableTitle);
%>
<link href="Scripts/Forms/flowform.css" rel="stylesheet" type="text/css" />
<script src="Scripts/Forms/common.js" type="text/javascript" ></script>
<input type="hidden" id="Form_ValidateAlertType" name="Form_ValidateAlertType" value="1" />
<input type="hidden" id="Form_TitleField" name="Form_TitleField" value="TempTest.Title" />
<input type="hidden" id="Form_DBConnID" name="Form_DBConnID" value="06075250-30dc-4d32-bf97-e922cb30fac8" />
<input type="hidden" id="Form_DBTable" name="Form_DBTable" value="TempTest" />
<input type="hidden" id="Form_DBTablePk" name="Form_DBTablePk" value="ID" />
<input type="hidden" id="Form_DBTableTitle" name="Form_DBTableTitle" value="Title" />
<input type="hidden" id="Form_AutoSaveData" name="Form_AutoSaveData" value="1" />
<script type="text/javascript">
	var initData = <%=BWorkFlow.GetFormDataJsonString(initData)%>;
	var fieldStatus = "1"=="<%=Request.QueryString["isreadonly"]%>"? {} : <%=fieldStatus%>;
	var displayModel = '<%=DisplayModel%>';
	$(window).load(function (){
		formrun.initData(initData, "TempTest", fieldStatus, displayModel);
	});
</script>
<p><br/></p><p style="text-align: center;">teser<br/></p><p><br/></p><table class="flowformtable" cellpadding="0" cellspacing="1"><tbody><tr class="firstRow"><td width="259" valign="top" style="word-break: break-all;">标题：</td><td width="259" valign="top"><input type="text" id="TempTest.Title" type1="flow_text" name="TempTest.Title" value="" valuetype="0" isflow="1" class="mytext" title=""/></td><td width="259" valign="top" style="word-break: break-all;">姓名：</td><td width="259" valign="top"><input type="text" type1="flow_org" id="TempTest.UserID" name="TempTest.UserID" value="u_<%=RoadFlow.Platform.Users.CurrentUserID.ToString()%>" more="0" isflow="1" class="mymember" title="" dept="0" station="0" user="0" workgroup="0" unit="0" rootid=""/></td></tr><tr><td width="259" valign="top" style="word-break: break-all;">时间：</td><td width="259" valign="top"><input type="text" type1="flow_datetime" id="TempTest.Date1" name="TempTest.Date1" value="" defaultvalue="" istime="0" daybefor="0" dayafter="0" currentmonth="0" isflow="1" class="mycalendar" title=""/></td><td width="259" valign="top"><input type="text" type1="flow_datetime" id="TempTest.Date2" name="TempTest.Date2" value="" defaultvalue="" istime="0" daybefor="0" dayafter="0" currentmonth="0" isflow="1" class="mycalendar" title=""/></td><td width="259" valign="top"><br/></td></tr><tr><td width="259" valign="top"><br/></td><td width="259" valign="top"><br/></td><td width="259" valign="top"><br/></td><td width="259" valign="top"><br/></td></tr></tbody></table><p><br/></p>