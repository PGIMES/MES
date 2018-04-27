<%@ Page Language="C#"%>
<%
	string FlowID = Request.QueryString["flowid"];
	string StepID = Request.QueryString["stepid"];
	string GroupID = Request.QueryString["groupid"];
	string TaskID = Request.QueryString["taskid"];
	string InstanceID = Request.QueryString["instanceid"];
	string DisplayModel = Request.QueryString["display"] ?? "0";
	string DBConnID = "a4eb9452-d87c-493d-89a5-95e2daa57b6e";
	string DBTable = "LOG";
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
<input type="hidden" id="Form_TitleField" name="Form_TitleField" value="LOG.TITLE" />
<input type="hidden" id="Form_DBConnID" name="Form_DBConnID" value="a4eb9452-d87c-493d-89a5-95e2daa57b6e" />
<input type="hidden" id="Form_DBTable" name="Form_DBTable" value="LOG" />
<input type="hidden" id="Form_DBTablePk" name="Form_DBTablePk" value="ID" />
<input type="hidden" id="Form_DBTableTitle" name="Form_DBTableTitle" value="TITLE" />
<input type="hidden" id="Form_AutoSaveData" name="Form_AutoSaveData" value="1" />
<script type="text/javascript">
	var initData = <%=BWorkFlow.GetFormDataJsonString(initData)%>;
	var fieldStatus = "1"=="<%=Request.QueryString["isreadonly"]%>"? {} : <%=fieldStatus%>;
	var displayModel = '<%=DisplayModel%>';
	$(window).load(function (){
		formrun.initData(initData, "LOG", fieldStatus, displayModel);
	});
</script>
<p><br/></p><table class="flowformtable" cellpadding="0" cellspacing="1"><tbody><tr class="firstRow"><td width="191" valign="top" style="word-break: break-all;">标题：</td><td width="914" valign="top" style="word-break: break-all;"><input type="text" id="LOG.TITLE" type1="flow_text" name="LOG.TITLE" value="" valuetype="0" isflow="1" class="mytext" title=""/>  <input type="hidden" id="LOG.WRITETIME" name="LOG.WRITETIME" isflow="1" type1="flow_hidden" value="<%=RoadFlow.Utility.DateTimeNew.ShortDateTime%>"/></td></tr><tr><td width="191" valign="top" style="word-break: break-all;">类型：</td><td width="914" valign="top"><select class="myselect" id="LOG.TYPE" name="LOG.TYPE" isflow="1" type1="flow_select"><%=new RoadFlow.Platform.WorkFlowForm().GetOptionsFromSql("a4eb9452-d87c-493d-89a5-95e2daa57b6e", "select type, type from log group by type", "")%></select></td></tr><tr><td width="191" valign="top" style="word-break: break-all;">内容：</td><td width="914" valign="top"><textarea isflow="1" type1="flow_textarea" id="LOG.CONTENTS" name="LOG.CONTENTS" class="mytext" style="width:90%;height:300px"></textarea></td></tr></tbody></table>