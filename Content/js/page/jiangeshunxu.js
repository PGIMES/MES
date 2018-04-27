var deviceSetting = {
    async: {
        enable: true,
        url: "/JianGeShunXu/GetTrees",
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
        $("#DianyaId").val(parentNode.id);
        $("#JiexianXingshiID").val(treeNode.id);
    } else {
        $("#DianyaId").val(treeNode.id);
        $("#JiexianXingshiID").val("");
    }
    searchDevice();
}

function searchDevice() {
    var json = { DianyaId: $("#DianyaId").val(), JiexianXingshiID: $("#JiexianXingshiID").val() };
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
            var url = "/jiangeshunxu/SaveData";
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
    XPage.DelData("/jiangeshunxu/Delete");
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
function addModel() {
    var postData = {
        DianyaId: $("#DianyaId").val(),
        JiexianXingshiID: $("#JiexianXingshiID").val()
    };  
    if (postData.DianyaId === "") {
        alert("请选择电压");
        return;
    }else if (postData.JiexianXingshiID === "") {
        alert("请选择接线形式");
        return;
    }
   // window.location.href = "/jiangeshunxu/Add?DianyaId=" + postData.DianyaId + "&JiexianXingshiID=" + postData.JiexianXingshiID;

    parent.layer.open({
        shade: [0.5, '#000', false],
        type: 2,
        area: ['800px', '500px'],
        fix: false, //不固定
        maxmin: false,
        title: ['间隔顺序维护', false],
        closeBtn: 1,
        content: '/jiangeshunxu/Add?DianyaId=' + postData.DianyaId + "&JiexianXingshiID=" + postData.JiexianXingshiID,
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
        url: "/jiangeshunxu/JianGeList",
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
        title: '间隔顺序',
        url: '/jiangeshunxu/Search',
        colNames: ['主键','间隔ID', '间隔', '配置值'],
        colModel: [
            { name: 'Id', index: 'Id', width: 60, key: true, hidden: true },
            { name: 'JianGeId', index: 'JianGeId', width: 60, hidden: true },
            { name: 'JianGeMingCheng', index: 'JianGeMingCheng', width: 60, editable: true, edittype: 'select', editoptions: { value: gettypes() } },
            { name: 'XuanXiangZhiId', index: 'XuanXiangZhiId', width: 100, editable: true, edittype: 'text' }
        ]
    };
    DianBiaoGrid.Load(config);
});