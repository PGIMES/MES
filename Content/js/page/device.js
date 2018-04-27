var deviceSetting = {
    async: {
        enable: true,
        url: "/Device/GetTrees",
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
        $("#OneId").val(parentNode.id);
        $("#TwoId").val(treeNode.id);
    } else {
        $("#OneId").val(treeNode.id);
        $("#TwoId").val("");
    }
    searchDevice();
}

function searchDevice() {
    var json = { OneId: $("#OneId").val(), TwoId: $("#TwoId").val() };
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
            var url = "/Device/SaveData";
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
            grid.jqGrid('saveRow', row.Id, saveParameters);
        } else {
            btn.attr("editing", "true");
            btn.text("保存");
        }
    } else {
        parent.layer.alert("请选择要编辑的数据");
    }
}

function delData() {
    XPage.DelData("/Device/Delete");
}

function addModel() {
    var postData = {
        OneId: $("#OneId").val(),
        TwoId: $("#TwoId").val()
    };
    if (postData.OneId === "") {
        alert("请选择一级设备");
        return;
    }else if (postData.TwoId === "") {
        alert("请选择二级设备");
        return;
    }
    window.location.href = "/Device/Add?OneId=" + postData.OneId + "&TwoId=" + postData.TwoId;
}
$(document).ready(function () {
    $.fn.zTree.init($("#deviceTree"), deviceSetting);
    var config = {
        title: '设备详情列表',
        url: '/Device/Search',
        colNames: ['主键', '品牌名称', '品牌型号'],
        colModel: [
            { name: 'Id', index: 'Id', width: 60, key: true, hidden: true },
            { name: 'BrandName', index: 'BrandName', width: 60, editable: true, edittype: 'text' },
            { name: 'Model', index: 'Model', width: 100, editable: true, edittype: 'text' }
        ]
    };
    DianBiaoGrid.Load(config);
});