/* ===================================================
 * tdf.config.js
 * 全局配置
 * 
 * ===================================================
 * WebApiAdress：Request Api Url
 * VirtualAdminPath：VirtualAdminPath Setting
 * 
 * ========================================================== */
var WebApiAdress = 'http://localhost:7667';
var VirtualPath = '';
var VirtualAdminPath = '';

var cookieAccessToken = 'TdfAccessToken';
var cookieRefreshToken = 'TdfRefreshToken';
var cookieUserId = 'TdfUserId';
var cookieUserName = 'TdfUserName';

/* ===================================================
 * tdf.cookies.js
 * JS 设置与获取Cookie
 * 
 * ===================================================
 * $.tmCookie
 * setCookie: function (name, value, time, option)
 * getCookie: function (name)
 * delCookie: function (name)
 * getCookieTime: function (time)
 * 
 * 调用示例: 
 * $.tmCookie.setCookie(cookieAccessToken, data.access_token, "d1")
 * $.tmCookie.getCookie(cookieAccessToken);
 * $.tmCookie.delCookie(cookieAccessToken);
 * 
 * ========================================================== */
$.tmCookie = {
    setCookie: function (name, value, time, option) {
        var str = name + '=' + escape(value);
        var date = new Date();
        date.setTime(date.getTime() + this.getCookieTime(time));
        str += "; expires=" + date.toGMTString();
        if (option) {
            if (option.path) str += '; path=' + option.path;
            if (option.domain) str += '; domain=' + option.domain;
            if (option.secure) str += '; true';
        }
        document.cookie = str;
    },
    getCookie: function (name) {
        var arr = document.cookie.split('; ');
        if (arr.length == 0) return '';
        for (var i = 0; i < arr.length; i++) {
            tmp = arr[i].split('=');
            if (tmp[0] == name) return unescape(tmp[1]);
        }
        return '';
    },
    delCookie: function (name) {
        $.tmCookie.setCookie(name, '', -1);
        var date = new Date();
        date.setTime(date.getTime() - 10000);
        document.cookie = name + "=; expire=" + date.toGMTString() + "; path=/";
    },

    getCookieTime: function (time) {
        if (time <= 0) return time;
        var str1 = time.substring(1, time.length) * 1;
        var str2 = time.substring(0, 1);
        if (str2 == "s") {
            return str1 * 1000;
        }
        else if (str2 == "m") {
            return str1 * 60 * 1000;
        }
        else if (str2 == "h") {
            return str1 * 60 * 60 * 1000;
        }
        else if (str2 == "d") {
            return str1 * 24 * 60 * 60 * 1000;
        }
    }
};

/* ===================================================
 * tdf.request_api.js
 * 网络请求
 * 
 * BoerAjax(url, data, type, successCallBack)
 * 
 * @param url 必要
 * @param data 必要 
 * @param type 必要 
 * @param successCallBack 必要 
 * ========================================================== */
function BoerAjax(url, data, type, successCallBack) {
    // 判断token是否为空，为空则跳转到login页面
    //var token = $.tmCookie.getCookie(cookieAccessToken);
    //if (isEmpty(token)) {
    //    window.top.location = VirtualAdminPath + "/login";
    //    return;
    //}

    // 这两句必须要，不然取不到tdfClaims
    //var headers = {};
    //headers.Authorization = 'Bearer ' + token;

    $.ajax({
        type: type,
        cache: false,
        dataType: "json",
        //headers: headers,  // 这里加入headers
        url: WebApiAdress + url,
        data: data,
        beforeSend: function () {
            $.messager.progress();
        },
        success: successCallBack,
        complete: function () {
            $.messager.progress('close');
        },
        error: function (xmlHttpRequest, textStatus, errorThrown) {
            $.messager.alert('警告', '网络错误！', 'warning');
        }
    });
}

/**
  * GetToken
  */
function GetToken() {
    var token = $.tmCookie.getCookie(cookieAccessToken);
    if (isEmpty(token)) {
        window.top.location = VirtualAdminPath + "/login";
        return;
    }
    return token;
}

/**
  * GetEasyUIData
  * @param data 必要
  */
function GetEasyUIData(data) {
    var json = {};
    json.total = data.Total;
    json.rows = data.Records;
    return json;
}

/**
 * GetEasyUITreejson
 *
 * @param data 必要
 * @param pid 必要
 * @param idName 必要
 * @param pidName 必要
 */
function GetEasyUITreejson(data, pid, idName, pidName) {
    var json = [];
    for (var i = 0; i < data.length; i++) {
        if (data[i][pidName] == pid) {
            var obj = data[i];
            var newObj = {};
            for (var p in obj) {
                if (obj.hasOwnProperty(p)) {
                    newObj[p] = obj[p];
                }
            }
            newObj.children = GetEasyUITreejson(data, newObj[idName], idName, pidName);
            json.push(newObj);
        }
    }
    return json;
}


/**
 * 判断非空
 * 
 * @param val
 * @returns {Boolean}
 */
function isEmpty(val) {
    val = $.trim(val);
    if (val == null)
        return true;
    if (val == undefined || val == 'undefined')
        return true;
    if (val == "")
        return true;
    if (val.length == 0)
        return true;
    if (!/[^(^\s*)|(\s*$)]/.test(val))
        return true;
    return false;
}

/**
 * 对String进行扩展
 * 
 * @param val
 * @returns {Boolean}
 */
String.prototype.trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, "");
};