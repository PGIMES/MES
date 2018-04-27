<%@ Page Language="C#"%>
<%
	string FlowID = Request.QueryString["flowid"];
	string StepID = Request.QueryString["stepid"];
	string GroupID = Request.QueryString["groupid"];
	string TaskID = Request.QueryString["taskid"];
	string InstanceID = Request.QueryString["instanceid"];
	string DisplayModel = Request.QueryString["display"] ?? "0";
	string DBConnID = "06075250-30dc-4d32-bf97-e922cb30fac8";
	string DBTable = "TempTest_Project";
	string DBTablePK = "ID";
	string DBTableTitle = "Name";
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
<input type="hidden" id="Form_TitleField" name="Form_TitleField" value="TempTest_Project.Name" />
<input type="hidden" id="Form_DBConnID" name="Form_DBConnID" value="06075250-30dc-4d32-bf97-e922cb30fac8" />
<input type="hidden" id="Form_DBTable" name="Form_DBTable" value="TempTest_Project" />
<input type="hidden" id="Form_DBTablePk" name="Form_DBTablePk" value="ID" />
<input type="hidden" id="Form_DBTableTitle" name="Form_DBTableTitle" value="Name" />
<input type="hidden" id="Form_AutoSaveData" name="Form_AutoSaveData" value="1" />
<script type="text/javascript">
	var initData = <%=BWorkFlow.GetFormDataJsonString(initData)%>;
	var fieldStatus = "1"=="<%=Request.QueryString["isreadonly"]%>"? {} : <%=fieldStatus%>;
	var displayModel = '<%=DisplayModel%>';
	$(window).load(function (){
		formrun.initData(initData, "TempTest_Project", fieldStatus, displayModel);
	});
</script>
<p style="text-align: center;"><br/></p><p><br/></p><p style="text-align: center;"><span style="font-size: 20px;"><strong>项目信息</strong></span><br/></p><p><br/></p><table class="flowformtable" cellpadding="0" cellspacing="1"><tbody><tr class="firstRow"><td width="196" valign="top" style="word-break: break-all;">项目编号：<br/></td><td width="474" valign="top"><input type="text" type1="flow_serialnumber" id="TempTest_Project.Number" name="TempTest_Project.Number" value="" sqlwhere="" length="6" isflow="1" class="mytext" title="" readonly="" style="background-color: rgb(232, 232, 232);"/><input type="hidden" value="TempTest_Project.Number" name="HasSerialNumber"/><input type="hidden" value="{'formatstring':'YZ$date&yyyy&$','sqlwhere':'','length':'6'}" name="HasSerialNumberConfig_TempTest_Project.Number"/></td><td width="116" valign="top" style="word-break: break-all;">发文编号：<br/></td><td width="343" valign="top"><input type="text" type1="flow_serialnumber" id="TempTest_Project.FWNumber" name="TempTest_Project.FWNumber" value="" sqlwhere="" length="2" isflow="1" class="mytext" title="" readonly="" style="background-color: rgb(232, 232, 232);"/><input type="hidden" value="TempTest_Project.FWNumber" name="HasSerialNumber"/><input type="hidden" value="{'formatstring':'沪城估（$date&yyyy&$）（咨）字第','sqlwhere':'','length':'2'}" name="HasSerialNumberConfig_TempTest_Project.FWNumber"/></td><td width="116" valign="top" style="word-break: break-all;">项目归属部门：</td><td valign="top" colspan="1" rowspan="1" width="327"><input type="text" type1="flow_org" id="TempTest_Project.DeptID" name="TempTest_Project.DeptID" value="<%=RoadFlow.Platform.Users.CurrentDeptID%>" more="0" isflow="1" class="mymember" title="" dept="1" station="0" user="0" workgroup="0" unit="0" rootid=""/></td></tr><tr><td width="196" valign="top" style="word-break: break-all;">项目外部来源：<br/></td><td width="474" valign="top"><select class="myselect" id="TempTest_Project.Source" name="TempTest_Project.Source" isflow="1" type1="flow_select"><option value="法院">法院</option></select></td><td width="116" valign="top" style="word-break: break-all;">估价目的：</td><td width="343" valign="top"><select class="myselect" id="TempTest_Project.GJMD" name="TempTest_Project.GJMD" isflow="1" type1="flow_select"><option value=" 抵押贷款">抵押贷款</option></select></td><td width="116" valign="top" style="word-break: break-all;">委托日期：</td><td valign="top" colspan="1" rowspan="1" width="327"><input type="text" type1="flow_datetime" id="TempTest_Project.WTDate" name="TempTest_Project.WTDate" value="<%=RoadFlow.Utility.DateTimeNew.ShortDate%>" defaultvalue="%3C%25=RoadFlow.Utility.DateTimeNew.ShortDate%25%3E" istime="0" daybefor="0" dayafter="1" currentmonth="0" isflow="1" class="mycalendar" title=""/></td></tr><tr><td width="196" valign="top" style="word-break: break-all;">项目名称：</td><td width="474" valign="top"><input type="text" id="TempTest_Project.Name" type1="flow_text" name="TempTest_Project.Name" value="" align="left" style="width:97%;text-align:left;padding-right:3px;" valuetype="0" isflow="1" class="mytext" title=""/></td><td width="116" valign="top" style="word-break: break-all;">楼盘名称：</td><td width="343" valign="top"><input type="text" id="TempTest_Project.LpName" type1="flow_text" name="TempTest_Project.LpName" value="" align="left" valuetype="0" isflow="1" class="mytext" title=""/></td><td width="116" valign="top" style="word-break: break-all;">地上建筑面积：</td><td valign="top" colspan="1" rowspan="1" width="327"><input type="text" id="TempTest_Project.JZMJ" type1="flow_text" name="TempTest_Project.JZMJ" value="" align="left" valuetype="4" isflow="1" class="mytext" title=""/></td></tr><tr><td width="196" valign="top" style="word-break: break-all;">地下建筑面积：</td><td width="474" valign="top"><input type="text" id="TempTest_Project.JZMJ1" type1="flow_text" name="TempTest_Project.JZMJ1" value="" align="left" valuetype="4" isflow="1" class="mytext" title=""/></td><td width="116" valign="top" style="word-break: break-all;">WEAS总价：<br/></td><td width="343" valign="top"><input type="text" id="TempTest_Project.UEAS" type1="flow_text" name="TempTest_Project.UEAS" value="" align="left" valuetype="4" isflow="1" class="mytext" title=""/></td><td width="116" valign="top" style="word-break: break-all;">项目评估总价：<br/></td><td valign="top" colspan="1" rowspan="1" width="327"><input type="text" id="TempTest_Project.Price" type1="flow_text" name="TempTest_Project.Price" value="" align="left" valuetype="4" isflow="1" class="mytext" title=""/></td></tr></tbody></table><p><br/></p>