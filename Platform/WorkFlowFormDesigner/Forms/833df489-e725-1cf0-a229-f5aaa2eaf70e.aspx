<%@ Page Language="C#"%>
<%
	string FlowID = Request.QueryString["flowid"];
	string StepID = Request.QueryString["stepid"];
	string GroupID = Request.QueryString["groupid"];
	string TaskID = Request.QueryString["taskid"];
	string InstanceID = Request.QueryString["instanceid"];
	string DisplayModel = Request.QueryString["display"] ?? "0";
	string DBConnID = "06075250-30dc-4d32-bf97-e922cb30fac8";
	string DBTable = "TepTest_GoOut";
	string DBTablePK = "id";
	string DBTableTitle = "title";
	if(InstanceID.IsNullOrEmpty()){InstanceID = Request.QueryString["instanceid1"];}
	RoadFlow.Platform.Dictionary BDictionary = new RoadFlow.Platform.Dictionary();
	RoadFlow.Platform.WorkFlow BWorkFlow = new RoadFlow.Platform.WorkFlow();
	RoadFlow.Platform.WorkFlowTask BWorkFlowTask = new RoadFlow.Platform.WorkFlowTask();
	string fieldStatus = BWorkFlow.GetFieldStatus(FlowID, StepID);
	LitJson.JsonData initData = BWorkFlow.GetFormData(DBConnID, DBTable, DBTablePK, InstanceID, fieldStatus, "{\"teptest_goout.sqdate\":\"yyyy-MM-dd\",\"teptest_goout.date1\":\"yyyy-MM-dd\",\"teptest_goout.date2\":\"yyyy-MM-dd\"}", TaskID);
	string TaskTitle = BWorkFlow.GetFromFieldData(initData, DBTable, DBTableTitle);
%>
<link href="Scripts/Forms/flowform.css" rel="stylesheet" type="text/css" />
<script src="Scripts/Forms/common.js" type="text/javascript" ></script>
<input type="hidden" id="Form_ValidateAlertType" name="Form_ValidateAlertType" value="1" />
<input type="hidden" id="Form_TitleField" name="Form_TitleField" value="TepTest_GoOut.title" />
<input type="hidden" id="Form_DBConnID" name="Form_DBConnID" value="06075250-30dc-4d32-bf97-e922cb30fac8" />
<input type="hidden" id="Form_DBTable" name="Form_DBTable" value="TepTest_GoOut" />
<input type="hidden" id="Form_DBTablePk" name="Form_DBTablePk" value="id" />
<input type="hidden" id="Form_DBTableTitle" name="Form_DBTableTitle" value="title" />
<input type="hidden" id="Form_AutoSaveData" name="Form_AutoSaveData" value="1" />
<script type="text/javascript">
	var initData = <%=BWorkFlow.GetFormDataJsonString(initData)%>;
	var fieldStatus = "1"=="<%=Request.QueryString["isreadonly"]%>"? {} : <%=fieldStatus%>;
	var displayModel = '<%=DisplayModel%>';
	$(window).load(function (){
		formrun.initData(initData, "TepTest_GoOut", fieldStatus, displayModel);
	});
</script>
<p><br/></p><p style="text-align: center;"><span style="font-size: 24px;"><strong><br/></strong></span></p><p style="text-align: center;"><span style="font-size: 24px;"><strong>出差申请</strong></span><br/></p><p><br/></p><table class="flowformtable" cellpadding="0" cellspacing="1" data-sort="sortDisabled"><tbody><tr class="firstRow"><td valign="top" colspan="1" rowspan="1" style="word-break: break-all;">标题：</td><td valign="top" colspan="3" rowspan="1"><input type="text" id="TepTest_GoOut.title" type1="flow_text" name="TepTest_GoOut.title" value="<%=RoadFlow.Platform.Users.CurrentUserName%>的出差申请" style="width:80%" valuetype="0" isflow="1" class="mytext" title=""/></td></tr><tr><td width="144" valign="top" style="word-break: break-all;">申请人:</td><td width="421" valign="top"><input type="text" type1="flow_org" id="TepTest_GoOut.SqUser" name="TepTest_GoOut.SqUser" value="u_<%=RoadFlow.Platform.Users.CurrentUserID.ToString()%>" more="0" isflow="1" class="mymember" title="" dept="0" station="0" user="1" workgroup="0" unit="0" rootid=""/></td><td width="147" valign="top" style="word-break: break-all;">申请日期：</td><td width="351" valign="top"><input type="text" type1="flow_datetime" id="TepTest_GoOut.SqDate" name="TepTest_GoOut.SqDate" value="<%=RoadFlow.Utility.DateTimeNew.ShortDate%>" format="yyyy-MM-dd" defaultvalue="%3C%25=RoadFlow.Utility.DateTimeNew.ShortDate%25%3E" istime="0" daybefor="0" dayafter="1" currentmonth="0" isflow="1" class="mycalendar" title=""/></td></tr><tr><td width="144" valign="top" style="word-break: break-all;">出差地点：</td><td width="421" valign="top"><input type="text" id="TepTest_GoOut.address" type1="flow_text" name="TepTest_GoOut.address" value="" valuetype="0" isflow="1" class="mytext" title=""/></td><td width="147" valign="top" style="word-break: break-all;">出差时间：</td><td width="351" valign="top" style="word-break: break-all;"><input type="text" type1="flow_datetime" id="TepTest_GoOut.Date1" name="TepTest_GoOut.Date1" value="" format="yyyy-MM-dd" defaultvalue="" istime="0" daybefor="0" dayafter="0" currentmonth="0" isflow="1" class="mycalendar" title=""/>至 <input type="text" type1="flow_datetime" id="TepTest_GoOut.Date2" name="TepTest_GoOut.Date2" value="" format="yyyy-MM-dd" defaultvalue="" istime="0" daybefor="0" dayafter="0" currentmonth="0" isflow="1" class="mycalendar" title=""/></td></tr><tr><td width="144" valign="top" style="word-break: break-all;">工作内容：</td><td valign="top" colspan="3"><textarea isflow="1" type1="flow_textarea" id="TepTest_GoOut.Why" name="TepTest_GoOut.Why" class="mytext" style="width:99%;height:200px"></textarea></td></tr></tbody></table><p><br/></p>