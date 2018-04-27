var deviceSetting = {
    async: {
        enable: true,
        url: "/JianGeGuYouXinHao/GetTrees",
        autoParam: ["id", "name=n", "level=lv"],
        otherParam: { "otherParam": "tree" }
    },
    data: {
        simpleData: {
            enable: true            
        }
    },
    callback: {
        onClick: deviceClick
    }
}; 

function deviceClick(event, treeId, treeNode) {
    var parentNode = treeNode.getParentNode();
    if (parentNode != null) {
        var parentParentNode = parentNode.getParentNode();
        if (parentParentNode != null) {
            $("#DianYaId").val(parentParentNode.id);
            $("#JieXianXingShiId").val(parentNode.id);
            $("#JianGeId").val(treeNode.id);
        } else {
            $("#DianYaId").val(parentNode.id);
            $("#JieXianXingShiId").val(treeNode.id);
            $("#JianGeId").val("");
        }
    } else {
        $("#DianYaId").val(treeNode.id);
        $("#JieXianXingShiId").val("");
        $("#JianGeId").val("");
    }
    searchDevice();
}



function searchDevice() {
    var json = { DianyaId: $("#DianYaId").val(), JieXianXingShiId: $("JieXianXingShiId").val(), JianGeId: $("#JianGeId").val() };
    XPage.Search(json);
}

function editModel() {
    var row = DianBiaoGrid.GetData();
    if (row != null) {
        var grid = $("#table_list");
        grid.jqGrid("editRow", row.Id, true);
        var btn = $("#btnEdit");
        var isEditing = btn.attr("editing") === "true";
        if (isEditing) {
            var url = "/JianGeGuYouXinHao/SaveData";
            var saveParameters = {
                "successfunc": function () { //success  
                    //重新设置行状态  
                    btn.attr("editing", "false");
                    btn.text("编辑");
                    searchDevice();
                },
                "url": url,
                "extraparam": {},
                "aftersavefunc": null,
                "errorfunc": null,
                "afterrestorefunc": null,
                "restoreAfterError": true,
                "mtype": "POST"
            }
            grid.jqGrid('saveRow', row.Id,  saveParameters);
        } else {
            btn.attr("editing", "true");
            btn.text("保存");
        }
    } else {
        parent.layer.alert("请选择要编辑的数据");
    }
}


function cancelModel() {
   // var row = DianBiaoGrid.GetData();
    var grid = $("#table_list");
    var id = jQuery("#table_list").jqGrid('getGridParam', 'selrow');
    grid.jqGrid('restoreRow', id);//重置grid
    grid.jqGrid('setSelection', id);//取消选择
    var btn = $("#btnEdit");
    btn.text("编辑");
    btn.attr("editing", "false");
    
}

function delData() {
    XPage.DelData("/JianGeGuYouXinHao/Delete");
}

function addModel() {
    var postData = {
        DianYaId: $("#DianYaId").val(),
        JieXianXingShiId: $("#JieXianXingShiId").val(),
        JianGeId: $("#JianGeId").val()
    };  
    if (postData.DianYaId === "") {
        alert("请选择电压");
        return;
    }else if (postData.JieXianXingShiId === "") {
        alert("请选择接线形式");
        return;
    } else if (postData.JianGeId === "") {
        alert("请选择间隔");
        return;
    } 

    //window.location.href = "/JianGeGuYouXinHao/Add?DianYaId=" + postData.DianYaId + "&JieXianXingShiId=" + postData.JieXianXingShiId + "&JianGeId=" + postData.JianGeId;
    
       
    parent.layer.open({
        shade: [0.5, '#000', false],
        type: 2,
        area: ['800px', '500px'],
        fix: false, //不固定
        maxmin: false,
        title: ['间隔固有信号维护', false],
        closeBtn: 1,
        content: '/JianGeGuYouXinHao/Add?DianYaId=' + postData.DianYaId + "&JieXianXingShiId=" + postData.JieXianXingShiId + "&JianGeId=" + postData.JianGeId,
        end: function (data) {
            $("#table_list").jqGrid('setGridParam', {
                datatype: 'json',
                postData: { 'Id':'' }, //可传参数数据
                page: 1
            }).trigger("reloadGrid");
        }
    });
    

}


function gettypes() {
    //动态生成select内容
    var str = "";
    $.ajax({
        type: "post",
        async: false,
        url: "/JianGeGuYouXinHao/SheBeiErLeiList",
        success: function (data) {
            if (data != null) {
                var jsonobj = eval(data);
                var length = jsonobj.length;
                for (var i = 0; i < length; i++) {
                    if (i != length - 1) {
                        str += jsonobj[i].Value + ":" + jsonobj[i].Text + ";";
                    } else {
                        str += jsonobj[i].Value + ":" + jsonobj[i].Text;// 这里是option里面的 value:label
                    }
                }                
            }
           
        }
    });
    return str;
}
function getgaojings() {
    //动态生成select内容
    var str = "";
    $.ajax({
        type: "post",
        async: false,
        url: "/JianGeGuYouXinHao/GaoJingFenJiList",
        success: function (data) {
            if (data != null) {
                var jsonobj = eval(data);
                var length = jsonobj.length;
                for (var i = 0; i < length; i++) {
                    if (i != length - 1) {
                        str += jsonobj[i].Value + ":" + jsonobj[i].Text + ";";
                    } else {
                        str += jsonobj[i].Value + ":" + jsonobj[i].Text;// 这里是option里面的 value:label
                    }
                }                
            }
        }
    });
    return str;
}

$(document).ready(function () {
    $.fn.zTree.init($("#deviceTree"), deviceSetting);
    var config = {
        title: '间隔固有信号',
        url: '/JianGeGuYouXinHao/Search',
        colNames: ['主键','间隔固有信号','告警分级','设备二类Id','设备分类'],
        colModel: [
            { name: 'Id', index: 'Id', width: 60, key: true, hidden: true },
            { name: 'GuYouXinHao', index: 'GuYouXinHao', width: 80, editable: true },
            { name: 'GaoJinFenJiMingCheng', index: 'GaoJinFenJiMingCheng', width: 40, editable: true, edittype: 'select', editoptions: { value: getgaojings() } },
            { name: 'SheiBeiErLeiId', index: 'SheBeiErLeiId', width: 60, hidden: true },
            { name: 'SheiBeiErLeiMingCheng', index: 'SheiBeiErLeiMingCheng', width: 60, editable: true, edittype: 'select', editoptions: { value: gettypes() } },
            
        ]
        
    };
    DianBiaoGrid.Load(config);

   
});