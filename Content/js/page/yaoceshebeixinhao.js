var deviceSetting = {
    async: {
        enable: true,
        url: "/YaoCeShebeiXinHao/GetTrees",
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
//function deviceClick(event, treeId, treeNode) {
//    var parentNode = treeNode.getParentNode();
//    if (parentNode != null) {
//        $("#DianYaId").val(parentNode.id);
//        $("JianGeId").val(treeNode.id);
//        $("#JieXianXingShiId").val(treeNode.id);
//    } else {
//        $("#DianYaId").val(treeNode.id);
//        $("JianGeId").val("");
//        $("#JieXianXingShiId").val("");
//    }
//    searchDevice();
//}

function deviceClick(event, treeId, treeNode) {
    var parentNode = treeNode.getParentNode();
    if (parentNode != null) {
        var parentParentNode = parentNode.getParentNode();
        if (parentParentNode != null) {
            
            $("#SheBeiDaLeiId").val(parentParentNode.id);
            $("#SheiBeiErLeiId").val(parentNode.id);
            $("#DianYaId").val(treeNode.id);
           
           
        } else {
            $("#SheBeiDaLeiId").val(parentNode.id);
            $("#SheiBeiErLeiId").val(treeNode.id);
            $("#DianYaId").val("");
           
        }
    } else {
        $("#SheBeiDaLeiId").val(treeNode.id);
        $("#SheiBeiErLeiId").val("");
        $("#DianYaId").val("");
       
    }  
    searchDevice();
}



function searchDevice() {
   
    var json = { SheBeiDaLeiId: $("#SheBeiDaLeiId").val(), SheiBeiErLeiId: $("SheiBeiErLeiId").val(), DianYaId: $("#DianYaId").val() };
    XPage.Search(json);
}

;


function addModel() {
    var postData = {
        DianYaId: $("#DianYaId").val(),
        SheiBeiErLeiId: $("#SheiBeiErLeiId").val()
    };
    if (postData.DianYaId === "") {
        alert("请选择电压");
        return;

    }
    parent.layer.open({
        shade: [0.5, '#000', false],
        type: 2,
        area: ['800px', '500px'],
        fix: false, //不固定
        maxmin: false,
        title: ['遥测设备信号-添加', false],
        closeBtn: 1,
        content: '/YaoCeShebeiXinHao/Add?SheiBeiErLeiId=' + postData.SheiBeiErLeiId + '&DianYaId=' + postData.DianYaId,
        end: function () {
            $("#table_list").trigger("reloadGrid")
        }
    });
};


function editModel() {
   
    var row = DianBiaoGrid.GetData();
 
    if (row.Id != null)
    {
        
            parent.layer.open({
                shade: [0.5, '#000', false],
                type: 2,
                area: ['800px', '500px'],
                fix: false, //不固定
                maxmin: false,
                title: ['遥测设备信号-修改', false],
                closeBtn: 1,
                content: '/YaoCeShebeiXinHao/Edit/' + row.Id,
                end: function () {
                    $("#table_list").trigger("reloadGrid")
                }
            });
      
    
    }
    else alert("请先选择你要编辑的资料");
}



function delData() {
    XPage.DelData("/YaoCeShebeiXinHao/Delete");
}

//function addModel() {
//    var postData = {
//        DianYaId: $("#DianYaId").val()
      
//    };  
//    if (postData.DianYaId === "") {
//        alert("请选择电压");
//        return;
   
//    }
//    //jQuery("#table_list").jqGrid('editGridRow', "new", {
//    //    height: 300,
//    //    reloadAfterSubmit: true,
//    //    afterComplete: function (xhr) {
//    //        alert(xhr.responseText);
//    //        //成功后关闭此窗口
//    //    },
       
//    //    closeAfterAdd: true
//    //});
//}

function gettypes() {
    //动态生成select内容
    var str = "";
    $.ajax({
        type: "post",
        async: false,
        url: "/YaoCeShebeiXinHao/JianGeList",
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
          //  alert(str);
        }
    });
    return str;
}

$(document).ready(function () {
    $.fn.zTree.init($("#deviceTree"), deviceSetting);

  
    var config = {
        title: '间隔顺序',
        url: '/YaoCeShebeiXinHao/Search',
        postData: { SheBeiDaLeiId: $("#SheBeiDaLeiId").val(), SheBeierLeiId: $("SheBeierLeiId").val(), DianYaId: $("#DianYaId").val() },
        colNames: ['主键', '遥测信号', '单位', '越限', '侧电属性', '电容器伴生','信号释义'],
        colModel: [
            { name: 'Id', index: 'Id', width: 60, key: true, hidden: true },
            { name: 'YaoCeXinHao', index: 'YaoCeXinHao', width: 100, editable: true, edittype: 'text' },
            { name: 'DanWei', index: 'DanWei', width: 60, editable: true, edittype: 'text' },
            { name: 'YueXian', index: 'YueXian', width: 60, editable: true, edittype: 'select', editoptions: {value: "是:是;否:否"} },
            { name: 'CeDianShuXing', index: 'CeDianShuXing', width: 60, editable: true, edittype: 'text' },
            { name: 'DianRongQiBanSheng', index: 'DianRongQiBanSheng', width: 60, editable: true, edittype: 'text' },
            { name: 'XinHaoShiYi', index: 'XinHaoShiYi', width: 60, editable: true, edittype: 'text' }
           
           // { name: 'JianGeMingCheng', index: 'JianGeMingCheng', width: 60, editable: true, edittype: 'select', editoptions: { value: gettypes() } },
            
        ],
        editurl: '/YaoCeShebeiXinHao/AddJiange'
        
    };
    
    DianBiaoGrid.Load(config);
});