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
	LitJson.JsonData initData = BWorkFlow.GetFormData(DBConnID, DBTable, DBTablePK, InstanceID, fieldStatus, "{\"temptest.date1\":\"yyyy-MM-dd\"}", TaskID);
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
<p style="text-align: center;"><strong><span style="font-size: 24px;">请  假  单</span></strong></p><p> </p><table align="center" class="flowformtable" cellspacing="1" cellpadding="0" data-sort="sortDisabled"><tbody><tr class="firstRow"><td width="100" valign="top" style="border-color: rgb(221, 221, 221); ms-word-break: break-all;">标题：</td><td valign="top" style="border-color: rgb(221, 221, 221);" colspan="3"><input name="TempTest.Title" align="0" id="TempTest.Title" style="width: 80%; padding-right: 3px;" type="text" value="<%=RoadFlow.Platform.Users.CurrentUserName%>的请假申请" type1="flow_text" isflow="1" class="mytext" title=""/></td></tr><tr><td valign="top" style="border-color: rgb(221, 221, 221); ms-word-break: break-all;">请假人：<br/></td><td valign="top" style="border-color: rgb(221, 221, 221); ms-word-break: break-all;" colspan="3"><input name="TempTest.UserID" id="TempTest.UserID" type="text" value="u_<%=RoadFlow.Platform.Users.CurrentUserID.ToString()%>" more="0" type1="flow_org" isflow="1" class="mymember" title="" dept="0" station="0" user="1" workgroup="0" unit="0" rootid=""/></td></tr><tr><td valign="top" style="border-color: rgb(221, 221, 221); -ms-word-break: break-all;" rowspan="1" colspan="1">所在部门：</td><td valign="top" style="border-color: rgb(221, 221, 221);" rowspan="1" colspan="3"><input name="TempTest.DeptID" id="TempTest.DeptID" type="text" value="<%=RoadFlow.Platform.Users.CurrentDeptID%>" more="0" type1="flow_org" isflow="1" class="mymember" title="" dept="1" station="0" user="0" workgroup="0" unit="0" rootid=""/></td></tr><tr><td valign="top" style="border-color: rgb(221, 221, 221); -ms-word-break: break-all;">请假开始日期：<br/></td><td valign="top" style="border-color: rgb(221, 221, 221); -ms-word-break: break-all;" colspan="3"><input name="TempTest.Date1" id="TempTest.Date1" type="text" value="<%=RoadFlow.Utility.DateTimeNew.ShortDate%>" type1="flow_datetime" currentmonth="0" dayafter="1" daybefor="0" istime="1" defaultvalue="%3C%25=RoadFlow.Utility.DateTimeNew.ShortDate%25%3E" format="yyyy-MM-dd" isflow="1" class="mycalendar" title=""/> </td></tr><tr><td valign="top" style="border-color: rgb(221, 221, 221); -ms-word-break: break-all;" rowspan="1" colspan="1">请假结束日期：</td><td valign="top" style="border-color: rgb(221, 221, 221);" rowspan="1" colspan="3"><input name="TempTest.Date2" id="TempTest.Date2" type="text" value="" type1="flow_datetime" currentmonth="0" dayafter="1" daybefor="0" istime="0" defaultvalue="" isflow="1" class="mycalendar" title=""/></td></tr><tr><td valign="top" style="border-color: rgb(221, 221, 221); -ms-word-break: break-all;" rowspan="1" colspan="1">请假天数：</td><td valign="top" style="border-color: rgb(221, 221, 221); -ms-word-break: break-all;" rowspan="1" colspan="3"><input name="TempTest.Days" align="left" id="TempTest.Days" type="text" value="" type1="flow_text" valuetype="0" isflow="1" class="mytext" title=""/> 天</td></tr><tr><td valign="top" style="border-color: rgb(221, 221, 221); ms-word-break: break-all;">请假类型：</td><td valign="top" style="border-color: rgb(221, 221, 221); -ms-word-break: break-all;" colspan="3"><%=BDictionary.GetRadiosByID("e7f836be-f091-460f-86e1-f0b6cdceba39".ToGuid(), "TempTest.Type", RoadFlow.Platform.Dictionary.OptionValueField.ID, "", "isflow='1' type1='flow_radio'")%></td></tr><tr><td valign="top" style="border-color: rgb(221, 221, 221); ms-word-break: break-all;">请假事由：<br/></td><td valign="top" style="border-color: rgb(221, 221, 221); -ms-word-break: break-all;" colspan="3"><textarea isflow="1" type1="flow_textarea" id="TempTest.Reason" name="TempTest.Reason" class="mytext" style="width: 80%; height: 80px;" maxlength="500"></textarea></td></tr><tr><td valign="top" style="border-color: rgb(221, 221, 221); ms-word-break: break-all;">相关附件：<br/></td><td valign="top" style="border-color: rgb(221, 221, 221); -ms-word-break: break-all;" colspan="3"><input name="TempTest.test" id="TempTest.test" type="text" value="" type1="flow_files" filetype="" isflow="1" class="myfile" title=""/> </td></tr></tbody></table><p> </p>