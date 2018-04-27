<%@ Page Language="C#"%>
<%
	string FlowID = Request.QueryString["flowid"];
	string StepID = Request.QueryString["stepid"];
	string GroupID = Request.QueryString["groupid"];
	string TaskID = Request.QueryString["taskid"];
	string InstanceID = Request.QueryString["instanceid"];
	string DisplayModel = Request.QueryString["display"] ?? "0";
	string DBConnID = "a4eb9452-d87c-493d-89a5-95e2daa57b6e";
	string DBTable = "TEMPTEST";
	string DBTablePK = "ID";
	string DBTableTitle = "TITLE";
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
<input type="hidden" id="Form_TitleField" name="Form_TitleField" value="TEMPTEST.TITLE" />
<input type="hidden" id="Form_DBConnID" name="Form_DBConnID" value="a4eb9452-d87c-493d-89a5-95e2daa57b6e" />
<input type="hidden" id="Form_DBTable" name="Form_DBTable" value="TEMPTEST" />
<input type="hidden" id="Form_DBTablePk" name="Form_DBTablePk" value="ID" />
<input type="hidden" id="Form_DBTableTitle" name="Form_DBTableTitle" value="TITLE" />
<input type="hidden" id="Form_AutoSaveData" name="Form_AutoSaveData" value="1" />
<script type="text/javascript">
	var initData = <%=BWorkFlow.GetFormDataJsonString(initData)%>;
	var fieldStatus = "1"=="<%=Request.QueryString["isreadonly"]%>"? {} : <%=fieldStatus%>;
	var displayModel = '<%=DisplayModel%>';
	$(window).load(function (){
		formrun.initData(initData, "TEMPTEST", fieldStatus, displayModel);
	});
</script>
<p><br/></p><p><br/></p><p style="text-align: center;"><span style="font-size: 24px;"><strong>请 假 单</strong></span><br/></p><p><span style="font-size: 24px;"><strong><br/></strong></span></p><p><span style="font-size: 24px;"></span></p><table class="flowformtable" cellpadding="0" cellspacing="1" data-sort="sortDisabled"><tbody><tr class="firstRow"><td valign="top" colspan="1" rowspan="1" style="word-break: break-all;">标题：</td><td valign="top" colspan="3" rowspan="1"><input type="text" id="TEMPTEST.TITLE" type1="flow_text" name="TEMPTEST.TITLE" value="<%=RoadFlow.Platform.Users.CurrentUserName%>的请假申请" style="width:70%" valuetype="0" isflow="1" class="mytext" title=""/></td></tr><tr><td width="144" valign="top" style="word-break: break-all;">请假人：</td><td width="374" valign="top"><input type="text" type1="flow_org" id="TEMPTEST.USERID" name="TEMPTEST.USERID" value="u_<%=RoadFlow.Platform.Users.CurrentUserID.ToString()%>" more="0" isflow="1" class="mymember" title="" dept="0" station="0" user="1" workgroup="0" unit="0" rootid=""/></td><td width="152" valign="top" style="word-break: break-all;">所在部门：</td><td width="366" valign="top"><input type="text" type1="flow_org" id="TEMPTEST.DEPTID" name="TEMPTEST.DEPTID" value="<%=RoadFlow.Platform.Users.CurrentDeptID%>" more="0" isflow="1" class="mymember" title="" dept="1" station="0" user="0" workgroup="0" unit="0" rootid=""/></td></tr><tr><td width="144" valign="top" style="word-break: break-all;">请假时间：</td><td width="374" valign="top" style="word-break: break-all;"><input type="text" type1="flow_datetime" id="TEMPTEST.DATE1" name="TEMPTEST.DATE1" value="" defaultvalue="" istime="1" daybefor="0" dayafter="1" currentmonth="0" isflow="1" class="mycalendar" title=""/>至 <input type="text" type1="flow_datetime" id="TEMPTEST.DATE2" name="TEMPTEST.DATE2" value="" defaultvalue="" istime="1" daybefor="0" dayafter="1" currentmonth="0" isflow="1" class="mycalendar" title=""/></td><td width="152" valign="top" style="word-break: break-all;">请假天数：</td><td width="366" valign="top"><input type="text" id="TEMPTEST.DAYS" type1="flow_text" name="TEMPTEST.DAYS" value="" valuetype="3" isflow="1" class="mytext" title=""/></td></tr><tr><td width="144" valign="top" style="word-break: break-all;">请假事由：</td><td width="374" valign="top"><select class="myselect" id="TEMPTEST.TYPE" name="TEMPTEST.TYPE" isflow="1" type1="flow_select"><option value=""></option><option value="事假">事假</option><option value="病假">病假</option><option value="年假">年假</option><option value="产假">产假</option><option value="探亲假">探亲假</option></select></td><td width="152" valign="top" style="word-break: break-all;">附件：</td><td width="366" valign="top"><input type="text" type1="flow_files" id="TEMPTEST.TEST" name="TEMPTEST.TEST" value="" filetype="" isflow="1" class="myfile" title=""/></td></tr></tbody></table><p><span style="font-size: 24px;"></span><br/></p>