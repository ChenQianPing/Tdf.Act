jQuery.extend({
    url: function (paramName) {
        var url = unescape(window.location.href);
        var allargs = url.split("?")[1];
        if (allargs == undefined) return '';
        var args = allargs.split("&");
        var obj = {};
        for (var i = 0; i < args.length; i++) {
            var arg = args[i].split("=");
            if (arg[1].indexOf('#') != -1) {
                arg[1] = arg[1].substring(0, arg[1].indexOf('#'));
            }
            eval('obj.' + arg[0] + '="' + arg[1] + '";');
        }
        if (eval('obj.' + paramName) == undefined) {
            return '';
        } else {
            return eval('obj.' + paramName);
        }
    },
    formdialog: function (v) {
        var dialogdiv = $("body").find("#" + v.id);
        if (dialogdiv.length > 0) {
        } else {
            $("body").append("<div id='" + v.id + "'></div>");
            dialogdiv = $("body").find("#" + v.id);
        }
        $(dialogdiv).dialog({
            title: v.title,
            width: v.width,
            height: v.height,
            iconCls: v.iconCls,
            closed: true,
            cache: false,
            content: '<iframe name="fram_' + v.id + '" id="fram_' + v.id + '" src="' + v.src + '" style="width:100%;height:98%" scrolling="auto" frameborder="0"  ></iframe>',
            modal: true,
            buttons: [
                {
                    text: '提交',
                    iconCls: 'icon-ok',
                    handler: function () {
                        eval('$("#" + v.id).dialog("body").find("#fram_" + v.id)[0].contentWindow.' + v.handler);
                    }
                }, {
                    text: '取消',
                    iconCls: 'icon-no',
                    handler: function () {
                        $("#" + v.id).dialog("close");
                    }
                }
            ]
        });
    },
    reportdialog: function (v) {
        var dialogdiv = $("body").find("#" + v.id);
        if (dialogdiv.length > 0) {
        } else {
            $("body").append("<div id='" + v.id + "'></div>");
            dialogdiv = $("body").find("#" + v.id);
        }
        $(dialogdiv).dialog({
            title: v.title,
            width: v.width,
            height: v.height,
            iconCls: v.iconCls,
            closed: true,
            cache: false,
            content: '<iframe name="fram_' + v.id + '" id="fram_' + v.id + '" src="' + v.src + '" style="width:100%;height:98%" frameborder="0" scrolling="auto" ></iframe>',
            modal: true
        });
    },
    getSelected: function (objName, idName) {
        var rows = $('#' + objName).datagrid('getSelections');
        if (rows.length == 1) {
            var selected = $('#' + objName).datagrid('getSelected');
            if (selected) {
                return eval('selected.' + idName);
            }
        }
        return "";
    },
    getSelections: function (objName, idName) {
        var ids = [];
        var rows = $('#' + objName).datagrid('getSelections');
        for (var i = 0; i < rows.length; i++) {
            ids.push(eval('rows[i].' + idName));
        }
        return ids.join(',');
    }
});

