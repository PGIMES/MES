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
<p><br/></p><p><br/></p><p><br/></p><p><br/></p><script>$(function(){
    var id=$("#TempTest\\.Type").val();
    $("#tttt").html(id);
  });</script><table class="flowformtable" cellpadding="0" cellspacing="1" data-sort="sortDisabled"><tbody><tr class="firstRow"><td width="199" valign="top" style="word-break: break-all;">人员</td><td width="609" valign="top" style="word-break: break-all;"><input type="text" id="TempTest.Title" type1="flow_text" name="TempTest.Title" value="<%=new RoadFlow.Platform.Users().GetName(new RoadFlow.Platform.WorkFlowTask().GetFirstSnderID(FlowID.ToGuid(), GroupID.ToGuid(), true))%>的请假申请" valuetype="0" isflow="1" class="mytext" title=""/>  <input type="hidden" id="TempTest.Type" name="TempTest.Type" isflow="1" type1="flow_hidden" value="095BA7DE-084A-41AA-A21E-4CCBB7CD4FF8"/></td><td width="169" valign="top" style="word-break: break-all;">部门</td><td width="639" valign="top"><input type="text" type1="flow_org" id="TempTest.DeptID" name="TempTest.DeptID" value="<%=new RoadFlow.Platform.WorkFlowTask().GetFirstSnderDeptID(FlowID.ToGuid(), GroupID.ToGuid())%>" more="0" isflow="1" class="mymember" title="" dept="0" station="0" user="0" workgroup="0" unit="0" rootid=""/></td></tr><tr><td width="199" valign="top" style="word-break: break-all;">时间</td><td width="609" valign="top" style="word-break: break-all;"><input type="text" type1="flow_datetime" id="TempTest.Date1" name="TempTest.Date1" value="<%=RoadFlow.Utility.DateTimeNew.ShortDate%>" defaultvalue="%3C%25=RoadFlow.Utility.DateTimeNew.ShortDate%25%3E" istime="0" daybefor="0" dayafter="0" currentmonth="0" isflow="1" class="mycalendar" title=""/>至 <input type="text" type1="flow_datetime" id="TempTest.Date2" name="TempTest.Date2" value="" defaultvalue="" istime="0" daybefor="0" dayafter="0" currentmonth="0" isflow="1" class="mycalendar" title=""/></td><td width="169" valign="top" style="word-break: break-all;">天数</td><td width="639" valign="top"><input type="text" id="TempTest.Days" type1="flow_text" name="TempTest.Days" value="da4d48af-2e7a-4b15-b738-40d1402c791a" valuetype="3" isflow="1" class="mytext" title=""/></td></tr><tr><td valign="top" colspan="4" style="word-break: break-all;" id="tttt"><br/></td></tr></tbody></table><p><br/></p>