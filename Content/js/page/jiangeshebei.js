var strS = "";
var deviceSetting = {
    async: {
        enable: true,
        url: "/JianGeSheBei/GetTrees",
        autoParam: ["id", "name=n", "level=lv"],
        otherParam: { "otherParam": "tree", "strS": function () { return strS; } },
    },
    data: {
        simpleData: {
            enable: true            
        }
    },
    callback: {
        onClick: deviceClick,
        beforeExpand: zTreeBeforeExpand
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

function zTreeBeforeExpand(treeId, treeNode) {
    var str = "";

    str = str + (treeNode.id);
    var pNode = treeNode.getParentNode();
    while (pNode != null) {
        str = str + "," + (pNode.id);
        pNode = pNode.getParentNode();
    }

    strS = str;//给传入tree的参数赋值
    return true;
};



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
            var url = "/JianGeSheBei/SaveData";
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
    var row = DianBiaoGrid.GetData();
    if (row != null) {
        var grid = $("#table_list");
        grid.jqGrid('restoreRow', "1");
        var btn = $("#btnEdit");
        btn.text("编辑");
       
       // searchDevice();
    }
}

function delData() {
    XPage.DelData("/JianGeSheBei/Delete");
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
  
    } else if (postData.JianGeId === "") {
        alert("请选择间隔");
        return;
    } 

    window.location.href = "/JianGeSheBei/Add?DianYaId=" + postData.DianYaId + "&JieXianXingShiId=" + postData.JieXianXingShiId+"&JianGeId=" + postData.JianGeId;
}

function gettypes() {
    //动态生成select内容
    var str = "";
    $.ajax({
        type: "post",
        async: false,
        url: "/JianGeSheBei/SheBeiErLeiList",
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
                //$.each(jsonobj, function(i){
                //str+="personType:"+jsonobj[i].personType+";"
                //$("<option value='" + jsonobj[i].personType + "'>" + jsonobj[i].personType+ "</option>").appendTo(typeselect);
                //});
            }
           
        }
    });
    return str;
}

$(document).ready(function () {
    $.fn.zTree.init($("#deviceTree"), deviceSetting);
    var config = {
        title: '间隔设备',
        url: '/JianGeSheBei/Search',
        colNames: ['主键','设备二类Id','设备类型', '设备编号'],
        colModel: [
            { name: 'Id', index: 'Id', width: 60, key: true, hidden: true },          
            { name: 'SheiBeiErLeiId', index: 'SheBeiErLeiId', width: 60, hidden: true },
            { name: 'SheiBeiErLeiMinCheng', index: 'SheBeiErLeiMinCheng', width: 60, editable: true, edittype: 'select', editoptions: { value: gettypes() } },
            { name: 'SheBeiBianHao', index: 'SheBeiBianHao', width: 100, editable: true, edittype: 'text' }
        ]
    };
    DianBiaoGrid.Load(config);
});