
$(function () {
    //Flot Bar Chart
    var setData1 = function (res) {
        var barOptions = {
            series: {
                bars: {
                    show: true,
                    barWidth: 0.6,
                    fill: true,
                    fillColor: {
                        colors: [{
                            opacity: 0.8
                        }, {
                            opacity: 0.8
                        }]
                    }
                }
            },
            xaxis: {
                tickDecimals: 0,
                min: 0,
                ticks: []//显示名称
            },
            colors: ["#1ab394"],
            grid: {
                color: "#999999",
                hoverable: true,
                clickable: true,
                tickColor: "#D4D4D4",
                borderWidth: 0
            },
            legend: {
                show: false
            },
            tooltip: true,
            tooltipOpts: {
                content: "日期: %x, 访问量: %y"
            }
        };
        var barData = {
            label: "bar",
            data: []//数据
        };

        for (var i = 0, len = res.data.length; i < len; i++) {
            var data = res.data[i];
            barOptions.xaxis.ticks.push([i + 1, data.Date]);
            barData.data.push([i + 1, data.Number]);
        }
        $.plot($("#flot-bar-chart"), [barData], barOptions);
    }
    //Flot Line Chart
    var setDataLine = function(res) {
        var lineOptions = {
            series: {
                lines: {
                    show: true,
                    lineWidth: 2,
                    fill: true,
                    fillColor: {
                        colors: [{
                            opacity: 0.0
                        }, {
                            opacity: 0.0
                        }]
                    }
                }
            },
            xaxis: {
                tickDecimals: 0,
                min: 0,
                ticks: []
            },
            colors: ["#1ab394"],
            grid: {
                color: "#999999",
                hoverable: true,
                clickable: true,
                tickColor: "#D4D4D4",
                borderWidth: 0
            },
            legend: {
                show: false
            },
            tooltip: true,
            tooltipOpts: {
                content: "日期: %x, 访问量: %y"
            }
        };
        var lineData = {
            label: "bar",
            data: []
        };

        for (var i = 0, len = res.data.length; i < len; i++) {
            var data = res.data[i];
            lineOptions.xaxis.ticks.push([i + 1, data.Date]);
            lineData.data.push([i + 1, data.Number]);
        }
        $.plot($("#flot-line-chart"), [lineData], lineOptions);
    }
    //Flot Pie Chart
    var setDataPie = function(res) {
        var datas = [];
        var colors = ["#d3d3d3", "#bababa", "#79d2c0", "#1ab394", "#D4D4D4", "#19d2ca", "#fae396"];
        for (var i = 0, len = res.data.length; i < len; i++) {
            var data = res.data[i];
            datas.push({ label: data.Date, data: data.Number, color: colors[i] });
        }
        $.plot($("#flot-pie-chart"), datas, {
            series: {
                pie: {
                    show: true
                }
            },
            grid: {
                hoverable: true
            },
            tooltip: true,
            tooltipOpts: {
                content: "%p.0%, %s", // show percentages, rounding to 2 decimal places
                shifts: {
                    x: 20,
                    y: 0
                },
                defaultTheme: false
            }
        });
    }

    $.ajax({
        url: "/Log/ChartsDatas",
        type: "GET",
        data: null,
        dataType: "JSON",
        success: function (res) {
            setData1(res);
            setDataLine(res);
            setDataPie(res);
        }
    });
});




