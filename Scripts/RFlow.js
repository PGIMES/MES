
//提出自定流程 JS
function SetButtons() {
    //alert("保存")
    if ($('#3B271F67-0433-4082-AD1A-8DF1B967B879', parent.document).length == 0) {
        $("#btnSave").hide();
    }
        //alert("发送")
    if ($('#8982B97C-ADBA-4A3A-AFD9-9A3EF6FF12D8', parent.document).length == 0) {
        $("#btnflowSend").hide();
    }
    else {
        var showTitle = $('a[id*=8982B97C-ADBA-4A3A-AFD9-9A3EF6FF12D8] span', parent.document).text();
        $("#btnflowSend").val(showTitle)
    }
    //alert("加签")
    if ($('#9A9C93B2-F77E-4EEE-BDF1-0E7D1A855B8D', parent.document).length == 0) {
        $("#btnaddWrite").hide();
    }
    // alert("退回");
    if ($('#86B7FA6C-891F-4565-9309-81672D3BA80A', parent.document).length == 0) {
        $("#btnflowBack").hide();
    }
    //alert("查看流程");
    if ($('#B8A7AF17-7AD5-4699-B679-D421691DD737', parent.document).length == 0) {
        //$("#btnshowProcess").hide();
    }
    //alert("转交");
    if ($('#347B811C-7568-4472-9A61-6C31F66980AE', parent.document).length == 0) {
        $("#btnflowRedirect").hide();
    }
        //alert("完成");
    if ($('#954EFFA8-03B8-461A-AAA8-8727D090DCB9', parent.document).length == 0) {
        $("#btnflowCompleted").hide();
    }
    else {
        var showTitle = $('a[id*=954EFFA8-03B8-461A-AAA8-8727D090DCB9] span', parent.document).text();
        $("#btnflowCompleted").val(showTitle)
    }
    //alert("终止");
    if ($('#BC3172E5-08D2-449A-BFF0-BD6F4DE797B0', parent.document).length == 0) {        
        $("#btntaskEnd").hide();
    }
    //打印
    if ($('#29B358E1-AD64-4F09-846C-4554AE6B85C4', parent.document).length == 0) {
        $("#btnPrint").hide();
    }
}
function DisableButtons() {
    if ($('#3B271F67-0433-4082-AD1A-8DF1B967B879', parent.document).length == 0) {
        //alert("保存")        
    }
    $("#btnSave").hide();
    if ($('#8982B97C-ADBA-4A3A-AFD9-9A3EF6FF12D8', parent.document).length == 0) {
        //alert("发送")        
    }
    $("#btnflowSend").hide();
    if ($('#9A9C93B2-F77E-4EEE-BDF1-0E7D1A855B8D', parent.document).length == 0) {
        //alert("加签")
    }
        $("#btnaddWrite").hide();
    if ($('#86B7FA6C-891F-4565-9309-81672D3BA80A', parent.document).length == 0) {
        // alert("退回");

    }        $("#btnflowBack").hide();
    if ($('#B8A7AF17-7AD5-4699-B679-D421691DD737', parent.document).length == 0) {
        //alert("查看流程");
        //$("#btnshowProcess").hide();
    }
    if ($('#347B811C-7568-4472-9A61-6C31F66980AE', parent.document).length == 0) {
        //alert("转交");
        
    }$("#btnflowRedirect").hide();
    if ($('#954EFFA8-03B8-461A-AAA8-8727D090DCB9', parent.document).length == 0) {
        //alert("完成");
       
    } $("#btnflowCompleted").hide();
    if ($('#BC3172E5-08D2-449A-BFF0-BD6F4DE797B0', parent.document).length == 0) {
        //alert("终止");        
    }$("#btntaskEnd").hide();
}
//提出自定流程 JS 
function setComment(val) {
    $('#comment', parent.document).val(val);
}

//    取得目标URL所包含的参数
//    @param url - url
//    @return Object 参数名值对，｛参数名:参数值,……｝
function getURLParams(url) {
    var regexpParam = /\??([\w\d%]+)=([\w\d%]*)&?/g; //分离参数的正则表达式
    var paramMap = null;
    var ret;
    paramMap = {};//初始化结果集
    //开始循环查找url中的参数，并以键值对形式放入结果集
   // debugger;
    while ((ret = regexpParam.exec(url)) != null) {
        //ret[1]是参数名，ret[2]是参数值
        paramMap[ret[1]] = ret[2];
    }
    return paramMap; //返回结果集
}
function getUrlParam(name) {
    debugger;
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
    var r = window.location.search.substr(1).match(reg);  //匹配目标参数
    if (r != null) return decodeURIComponent(r[2]); return null; //返回参数值
}

