<%@ Page Language="C#"%>
<%
	string FlowID = Request.QueryString["flowid"];
	string StepID = Request.QueryString["stepid"];
	string GroupID = Request.QueryString["groupid"];
	string TaskID = Request.QueryString["taskid"];
	string InstanceID = Request.QueryString["instanceid"];
	string DisplayModel = Request.QueryString["display"] ?? "0";
	string DBConnID = "06075250-30dc-4d32-bf97-e922cb30fac8";
	string DBTable = "TempTest_Free";
	string DBTablePK = "id";
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
<input type="hidden" id="Form_TitleField" name="Form_TitleField" value="TempTest_Free.title" />
<input type="hidden" id="Form_DBConnID" name="Form_DBConnID" value="06075250-30dc-4d32-bf97-e922cb30fac8" />
<input type="hidden" id="Form_DBTable" name="Form_DBTable" value="TempTest_Free" />
<input type="hidden" id="Form_DBTablePk" name="Form_DBTablePk" value="id" />
<input type="hidden" id="Form_DBTableTitle" name="Form_DBTableTitle" value="title" />
<input type="hidden" id="Form_AutoSaveData" name="Form_AutoSaveData" value="1" />
<script type="text/javascript">
	var initData = <%=BWorkFlow.GetFormDataJsonString(initData)%>;
	var fieldStatus = "1"=="<%=Request.QueryString["isreadonly"]%>"? {} : <%=fieldStatus%>;
	var displayModel = '<%=DisplayModel%>';
	$(window).load(function (){
		formrun.initData(initData, "TempTest_Free", fieldStatus, displayModel);
	});
</script>
<p><br/></p><p><br/></p><p style="text-align: center;"><span style="font-size: 22px;"><strong>费用申请</strong></span></p><p><br/></p><table class="flowformtable" cellspacing="1" cellpadding="0" data-sort="sortDisabled"><tbody><tr class="firstRow"><td valign="top" style="-ms-word-break: break-all;" rowspan="1" colspan="1">标题：</td><td valign="top" rowspan="1" colspan="3"><input name="TempTest_Free.title" title="" class="mytext" id="TempTest_Free.title" style="width: 80%;" type="text" value="<%=RoadFlow.Platform.Users.CurrentUserName%>的费用申请" valuetype="0" type1="flow_text" isflow="1"/></td></tr><tr><td width="132" valign="top" style="-ms-word-break: break-all;">申请人:</td><td width="494" valign="top"><input name="TempTest_Free.SqUser" title="" class="mymember" id="TempTest_Free.SqUser" type="text" value="u_<%=RoadFlow.Platform.Users.CurrentUserID.ToString()%>" more="0" user="1" station="0" unit="0" dept="0" type1="flow_org" isflow="1" workgroup="0" rootid=""/></td><td width="126" valign="top" style="-ms-word-break: break-all;">申请日期：</td><td width="311" valign="top"><input name="TempTest_Free.SqDate" title="" class="mycalendar" id="TempTest_Free.SqDate" type="text" value="<%=RoadFlow.Utility.DateTimeNew.ShortDate%>" defaultvalue="%3C%25=RoadFlow.Utility.DateTimeNew.ShortDate%25%3E" type1="flow_datetime" currentmonth="0" dayafter="0" daybefor="0" istime="0" isflow="1"/></td></tr><tr><td width="132" valign="top" style="-ms-word-break: break-all;">说明：<br/></td><td width="53" valign="top" colspan="3"><textarea name="TempTest_Free.Note" class="mytext" id="TempTest_Free.Note" style="width: 99%; height: 100px;" type1="flow_textarea" isflow="1"></textarea></td></tr><tr><td width="132" valign="top" style="-ms-word-break: break-all;">费用明细：</td><td valign="top" colspan="3"><table class="flowformsubtable" id="subtable_TempTest_FreeList_id_id_FreeID" style="margin: 0px auto; width: 99%;" cellspacing="1" cellpadding="0" issubflowtable="1"><thead><tr><th>费用名称<input name="flowsubtable_id" type="hidden" value="TempTest_FreeList_id_id_FreeID"/><input name="flowsubtable_TempTest_FreeList_id_id_FreeID_secondtable" type="hidden" value="TempTest_FreeList"/><input name="flowsubtable_TempTest_FreeList_id_id_FreeID_primarytablefiled" type="hidden" value="id"/><input name="flowsubtable_TempTest_FreeList_id_id_FreeID_secondtableprimarykey" type="hidden" value="id"/><input name="flowsubtable_TempTest_FreeList_id_id_FreeID_secondtablerelationfield" type="hidden" value="FreeID"/></th><th>金额</th><th>费用说明</th><th></th></tr></thead><tbody><tr type1="listtr"><td iscount="0" colname="TempTest_FreeList_Name"><input name="hidden_guid_TempTest_FreeList_id_id_FreeID" type="hidden" value="748618aff3e5a74dab18ab4ed8bb9296"/><input name="flowsubid" type="hidden" value="TempTest_FreeList_id_id_FreeID"/><input name="TempTest_FreeList_id_id_FreeID_748618aff3e5a74dab18ab4ed8bb9296_TempTest_FreeList_Name" class="mytext" id="TempTest_FreeList_id_id_FreeID_748618aff3e5a74dab18ab4ed8bb9296_TempTest_FreeList_Name" type="text" value="" valuetype="0" defaultvalue="" type1="subflow_text" colname="TempTest_FreeList_Name" issubflow="1"/></td><td iscount="1" colname="TempTest_FreeList_Amount"><input name="TempTest_FreeList_id_id_FreeID_748618aff3e5a74dab18ab4ed8bb9296_TempTest_FreeList_Amount" class="mytext" id="TempTest_FreeList_id_id_FreeID_748618aff3e5a74dab18ab4ed8bb9296_TempTest_FreeList_Amount" onblur="formrun.subtableCount('TempTest_FreeList_id_id_FreeID','TempTest_FreeList_Amount','countspan_TempTest_FreeList_id_id_FreeID_TempTest_FreeList_Amount');" type="text" value="" valuetype="4" defaultvalue="" type1="subflow_text" iscount="1" colname="TempTest_FreeList_Amount" issubflow="1"/></td><td iscount="0" colname="TempTest_FreeList_Note"><input name="TempTest_FreeList_id_id_FreeID_748618aff3e5a74dab18ab4ed8bb9296_TempTest_FreeList_Note" class="mytext" id="TempTest_FreeList_id_id_FreeID_748618aff3e5a74dab18ab4ed8bb9296_TempTest_FreeList_Note" type="text" value="" valuetype="0" defaultvalue="" type1="subflow_text" colname="TempTest_FreeList_Note" issubflow="1"/></td><td><input class="mybutton" style="margin-right: 4px;" onclick="formrun.subtableNewRow(this);" type="button" value="增加"/><input class="mybutton" onclick="formrun.subtableDeleteRow(this);" type="button" value="删除"/></td></tr><tr type1="counttr"><td align="right" style="text-align: right; padding-right: 20px;" colspan="6"><span style="margin-right: 10px;">金额合计：<label id="countspan_TempTest_FreeList_id_id_FreeID_TempTest_FreeList_Amount">0</label></span></td></tr></tbody></table></td></tr></tbody></table><p><br/></p>