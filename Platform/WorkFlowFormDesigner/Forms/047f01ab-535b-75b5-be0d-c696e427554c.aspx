<%@ Page Language="C#"%>
<%
	string FlowID = Request.QueryString["flowid"];
	string StepID = Request.QueryString["stepid"];
	string GroupID = Request.QueryString["groupid"];
	string TaskID = Request.QueryString["taskid"];
	string InstanceID = Request.QueryString["instanceid"];
	string DisplayModel = Request.QueryString["display"] ?? "0";
	string DBConnID = "06075250-30dc-4d32-bf97-e922cb30fac8";
	string DBTable = "TempTest_Vote";
	string DBTablePK = "ID";
	string DBTableTitle = "title";
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
<input type="hidden" id="Form_TitleField" name="Form_TitleField" value="TempTest_Vote.title" />
<input type="hidden" id="Form_DBConnID" name="Form_DBConnID" value="06075250-30dc-4d32-bf97-e922cb30fac8" />
<input type="hidden" id="Form_DBTable" name="Form_DBTable" value="TempTest_Vote" />
<input type="hidden" id="Form_DBTablePk" name="Form_DBTablePk" value="ID" />
<input type="hidden" id="Form_DBTableTitle" name="Form_DBTableTitle" value="title" />
<input type="hidden" id="Form_AutoSaveData" name="Form_AutoSaveData" value="1" />
<script type="text/javascript">
	var initData = <%=BWorkFlow.GetFormDataJsonString(initData)%>;
	var fieldStatus = "1"=="<%=Request.QueryString["isreadonly"]%>"? {} : <%=fieldStatus%>;
	var displayModel = '<%=DisplayModel%>';
	$(window).load(function (){
		formrun.initData(initData, "TempTest_Vote", fieldStatus, displayModel);
	});
</script>
<p> </p><p style="TEXT-ALIGN: center"><span style="FONT-SIZE: 22px">问卷调查</span></p><p><span style="FONT-SIZE: 22px"></span> </p><table class="flowformtable" cellspacing="1" cellpadding="0" data-sort="sortDisabled" align="center"><tbody><tr class="firstRow"><td style="WORD-BREAK: break-all" valign="top" colspan="2" width="836">流程标题：<input type="text" id="TempTest_Vote.title" type1="flow_text" name="TempTest_Vote.title" value="" style="width:70%" valuetype="0" isflow="1" class="mytext" title=""/></td></tr><tr><td style="WORD-BREAK: break-all" valign="top" colspan="2" width="836">您是如果知道RoadFlow的？</td></tr><tr><td valign="top" colspan="2" width="836" style="word-break: break-all;"><input type="radio" name="TempTest_Vote.votevalue" id="TempTest_Vote.votevalue_0" value="0" style="vertical-align:middle;" isflow="1" type1="flow_radio"/><label for="TempTest_Vote.votevalue_0" style="vertical-align:middle;margin-right:3px;">别人推荐</label><input type="radio" name="TempTest_Vote.votevalue" id="TempTest_Vote.votevalue_1" value="1" style="vertical-align:middle;" isflow="1" type1="flow_radio"/><label for="TempTest_Vote.votevalue_1" style="vertical-align:middle;margin-right:3px;">搜索引擎</label><input type="radio" name="TempTest_Vote.votevalue" id="TempTest_Vote.votevalue_2" value="2" style="vertical-align:middle;" isflow="1" type1="flow_radio"/><label for="TempTest_Vote.votevalue_2" style="vertical-align:middle;margin-right:3px;">广告</label>  <input type="hidden" id="TempTest_Vote.userid" name="TempTest_Vote.userid" isflow="1" type1="flow_hidden" value="u_<%=RoadFlow.Platform.Users.CurrentUserID.ToString()%>"/> <input type="hidden" id="TempTest_Vote.username" name="TempTest_Vote.username" isflow="1" type1="flow_hidden" value="<%=RoadFlow.Platform.Users.CurrentUserName%>"/> <input type="hidden" id="TempTest_Vote.instanceid" name="TempTest_Vote.instanceid" isflow="1" type1="flow_hidden" value="<%=InstanceID%>"/></td></tr></tbody></table><p style="text-align: center;"> </p><p> </p>