 
//获取树层级Id
function getSignal() {
    var ids = new Array();//存储id的数组
    var treeObj = $.fn.zTree.getZTreeObj("deviceTree");
    var treeNode = treeObj.getSelectedNodes();//当前节点
    if (treeNode.length>0 )
    {
        ids.push(treeNode[0].id);
        var pNode = treeNode[0].getParentNode();
        while (pNode != null) {
            ids.push(pNode.id);
            pNode = pNode.getParentNode();
        }
        return ids;
    }
    
}
function getSignalStr() {
    var str="" ;//存储id的数组
    var treeObj = $.fn.zTree.getZTreeObj("deviceTree");
    var treeNode = treeObj.getSelectedNodes();//当前节点
    if (treeNode.length > 0) {
        str=str+(treeNode[0].id);
        var pNode = treeNode[0].getParentNode();
        while (pNode != null) {
            str = str +","+ (pNode.id);
            pNode = pNode.getParentNode();
        }
        
    }
    return str;
}
function deviceClick(event, treeId, treeNode) {
    var ids = getSignal();
    strS = "";
    strS = getSignalStr();
    if ($("#XinHaoLeiBie").val() == "01") {
        $("#SheBeiDaLeiId").val(ids[4]);
        $("#SheBeiErLeiId").val(ids[3]);
        $("#ChangJiaId").val(ids[2]);
        $("#Xinghao").val(ids[1]);
        $("#BanBen").val(ids[0]);
        
    };
    if ($("#XinHaoLeiBie").val() == "02") {
        $("#SheBeiDaLeiId").val(ids[2]);
        $("#SheBeiErLeiId").val(ids[1]);
        $("#TongYongXinHao").val(ids[0]);
        
    }
    if ($("#XinHaoLeiBie").val() == "03") {
        $("#SheBeiDaLeiId").val(ids[4]);
        $("#SheBeiErLeiId").val(ids[3]);
        $("#YuanLiMingCheng").val(ids[2]);
        $("#SheBeiYuanLi").val(ids[1]);
        $("#DianYaId").val(ids[0]);
       
    };
    if (Searchcheckvalue(ids) == true)
    {
        searchDevice(ids);
    }
}
function deviceClick2(event, treeId, treeNode) {
    
    if ($("#XinHaoLeiBie").val() == "01") {
        $("#SheBeiDaLeiId").val(ids[4]);
        $("#SheBeiErLeiId").val(ids[3]);
        $("#ChangJiaId").val(ids[2]);
        $("#Xinghao").val(ids[1]);
        $("#BanBen").val(ids[0]);

    };
    if ($("#XinHaoLeiBie").val() == "02") {
        $("#SheBeiDaLeiId").val(ids[2]);
        $("#SheBeiErLeiId").val(ids[1]);
        $("#TongYongXinHao").val(ids[0]);

    }
    if ($("#XinHaoLeiBie").val() == "03") {
        $("#SheBeiDaLeiId").val(ids[4]);
        $("#SheBeiErLeiId").val(ids[3]);
        $("#YuanLiMingCheng").val(ids[2]);
        $("#SheBeiYuanLi").val(ids[1]);
        $("#DianYaId").val(ids[0]);

    };
    if (Searchcheckvalue(ids) == true) {
        searchDevice(ids);
    }
}
//验证选择tree资料完整性
function checkvalue() {
    var bln=true;
    var ids = getSignal();
    var lenth = ids.length;
    var xxlb = $("#XinHaoLeiBie").val();
    if (xxlb == "01"&&lenth<5) {//厂家判断；ids
        parent.layer.alert("请选择到厂家型号版本(若无版本，请至【厂家型号】维护版本)。");
        bln= false;
    }
    if (xxlb == "02" && lenth < 3) {//通用类别
        parent.layer.alert("请选择到通用信号。");
        bln = false;
    } 
    if (xxlb == "03" && lenth < 5)
        {//设备原理
        parent.layer.alert("请选择到设备原理下电压等级(若无原理(电压)，请至【设备原理】维护原理)。");
        bln=false;
    }
    return bln;
}
function Searchcheckvalue(ids) {
    //var ids = getSignal();
    var lenth = ids.length;
    var xxlb = $("#XinHaoLeiBie").val();
    if (xxlb == "01" && lenth == 5) {//厂家判断；ids
        return true;
    }
    if (xxlb == "02" && lenth == 3) {//通用类别
        return true;
    }
    if (xxlb == "03" && lenth == 5) {//设备原理
        return true;
    }
    return false;
}

function cleartextvalue() {
    $("#SheBeiDaLeiId").val("");
    $("#SheBeiErLeiId").val("" );
    $("#ChangJiaId").val("");
    $("#Xinghao").val("");
    $("#BanBen").val("");
    $("#TongYongXinHao").val("");
    $("#YuanLiMingCheng").val("");
    $("#SheBeiYuanLi").val("");
    $("#DianYaId").val("");
}
function searchDevice(ids) {    
    cleartextvalue();
    var json;
    if ($("#XinHaoLeiBie").val() == "01") {
        $("#SheBeiErLeiId").val(ids[3]);
        $("#ChangJiaId").val(ids[2]);
        $("#Xinghao").val(ids[1]);
        $("#BanBen").val(ids[0]);
        json = { DaLeiId: ids[4], SheBeiErLeiId: ids[3], ChangJiaId: ids[2], Xinghao: ids[1], BanBen: ids[0], XinHaoLeiBie:$("#XinHaoLeiBie").val() };
    };
    if ($("#XinHaoLeiBie").val() == "02") {
        $("#SheBeiErLeiId").val(ids[1]);        
        $("#TongYongXinHao").val(ids[0]);        
        json = { DaLeiId: ids[2], SheBeiErLeiId: ids[1], TongYongXinHao: ids[0], XinHaoLeiBie: $("#XinHaoLeiBie").val() };
    }
    if ($("#XinHaoLeiBie").val() == "03") {
        $("#SheBeiErLeiId").val(ids[3]);
        $("#YuanLiMingCheng").val(ids[2]);
        $("#SheBeiYuanLi").val(ids[1]);
        $("#DianYaId").val(ids[0]);
        json = { DaLeiId: ids[4], SheBeiErLeiId: ids[3], YuanLiMingCheng: ids[2], SheBeiYuanLi: ids[1], DianYaId: ids[0], XinHaoLeiBie: $("#XinHaoLeiBie").val() };
    };

    getSignalId(json);//赋值给临时hidden     
   
    $("#table_list_0").jqGrid('setGridParam', {
        datatype: 'json',
        postData: { 'YaoXinSignalId': $("#YaoXinSignalId").val() }, //发送数据
        page: 1
    }).trigger("reloadGrid");
    $("#table_list_1").jqGrid('setGridParam', {
        datatype: 'json',
        postData: { 'YaoXinSignalId': $("#YaoXinSignalId").val() }, //发送数据
        page: 1
    }).trigger("reloadGrid");
    $("#table_list_2").jqGrid('setGridParam', {
        datatype: 'json',
        postData: { 'YaoXinSignalId': $("#YaoXinSignalId").val() }, //发送数据
        page: 1
    }).trigger("reloadGrid");
    $("#table_list_3").jqGrid('setGridParam', {
        datatype: 'json',
        postData: { 'YaoXinSignalId': $("#YaoXinSignalId").val() }, //发送数据
        page: 1
    }).trigger("reloadGrid");
    $("#table_list_4").jqGrid('setGridParam', {
        datatype: 'json',
        postData: { 'YaoXinSignalId': $("#YaoXinSignalId").val() }, //发送数据
        page: 1
    }).trigger("reloadGrid");
     
}



function getSignalId(jsons) {    
    var str = "";
    var xinhaoleibie = $("#XinHaoLeiBie").val();

    $.ajax({
        type: "post",        
        url: "/yaoxinshebeixinhao/GetSignalIdby" + "?id2=" + "2" + "&id3=" + "3" + "&id4=" + "4" + "&id5=" + "5" + "&xinhaoleibie=" + xinhaoleibie,
        dataType: "json",
        data: JSON.stringify({ jsons} ),
        contentType: "application/json",
        async: false,
        success: function (data) {
            if (data != null) {
                var jsonobj = eval(data);
                //alert(jsonobj);
                //var length = jsonobj.length;
                //for (var i = 0; i < length; i++) {
                    str = jsonobj;
                //}
                    $("#YaoXinSignalId").val(jsonobj);
               
            }
            
        }
    });
    return str;
}
//获取Grid选中值ids
function getIds(grid1) {
    var res = {
        Len: 0,
        Data: []
    };
    var grid = $("#" + grid1);
    
    var ids=grid.jqGrid('getGridParam','selarrrow');    

    if (ids) {        
        for (var i = 0; i < ids.length; i++) {
            var ret = grid.jqGrid('getRowData', ids[i])
            res.Data.push(ret);
        }
    }
    res.Len = res.Data.length;
    return res;
}

$(document).ready(function () {  
    
   

    
});