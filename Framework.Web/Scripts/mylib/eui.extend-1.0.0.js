
/**
 * eui.extend.js 1.0.0
 * Most modern mobile touch slider and framework with hardware accelerated transitions
 * 
 * Copyright 2016-forever, TangTai
 * args-ajax
 * params-dialog
 * 
 * Released on: March 10, 2017
 */

(function ($) {
  
    window.eui = {
        form: '#dataForm',
        searchForm:'#searchForm',
        button: { disable: _disable, enable: _enable },
        alert: _alert,
        error: _error,
        confirm: _confrim,
        serializeJson:_serializeJson,
        dialogArgs: { url: undefined, title: undefined, width: undefined, height: undefined, callback: undefined, success: undefined, handler: undefined },
        dialog: _dialog,
        commitArgs: { url: undefined, data: undefined, success: undefined, fail: undefined, type: "POST", async: true, other: false },
        commit: _commit
    }
    
    /*
     * EasyUi按钮的启用
     * @param  按钮的标识的$对象(默认为提交按钮)
     * @return {bool}
     */
    function _enable(key) {
        key = key || $('#ok');
        key.linkbutton('enable');
    }

    /*
     * EasyUi按钮的禁用
     * @param  按钮的标识的$对象(默认为提交按钮)
     * @return {bool}
     */
    function _disable(key) {
        key = key || $('#ok');
        key.linkbutton('disable');
    }

    /*
    * 将表单数据序列成json对象
    * @param  form
    * @return {json}
    */
    function _serializeJson(form) {
        var array = $(form).serializeArray();
        var obj = {};
        for (var i = 0; i < array.length; i++) {
            var key = array[i]["name"];
            var value = array[i]["value"];
            obj[key] = value;
        }
        return obj;
    }

    /*
     * EasyUi表单提交
     * @param  form (默认为提交表单)
     * @return {bool}
     */
    function _commit(parameter) {
        $.ajax({
            url: parameter.url, 
            data: parameter.data,
            type: parameter.type,
            async: parameter.async,
            dataType: 'json',
            success: function (r) {
                 /*是否需要直接处理返回回调*/
                if (!parameter.other) {
                    if (r.success) {
                        parameter.success(r);
                        eui.dialogArgs.handler.dialog('close');//关闭弹出框
                    }
                    else {
                        if (parameter.fail) {
                            parameter.fail(r);
                        }
                        else {
                            eui.alert("操作失败");
                            eui.button.enable();//启用按钮
                        }
                    }
                }
                else
                    //自定义返回的处理
                    parameter.success(r);
            },
            // jqXHR 是经过jQuery封装的XMLHttpRequest对象
            // textStatus 可能为null、 'timeout（超时）'、 'error（错误）'、 'abort(中止)'和'parsererror（解析错误)'等
            // errorMsg 是错误信息字符串(响应状态的文本描述部分，例如'Not Found'或'Internal Server Error')
            error: function (jqXHR, textStatus, errorMsg) {
                switch (jqXHR.status) {
                    case 404: eui.alert('链接地址错误!'); break;
                    case 500: eui.error('服务器错误!'); break;
                    default: eui.error(jqXHR.status + ":" + jqXHR.statusText);
                }
            }
        });
    }

    /*
     * EasyUi消息提示框(alert）.
     * @param   msg 
     * @return {dialog}
     */
    function _alert(msg) {
        $.messager.alert('提示', msg, 'info');
    }

    /*
     * EasyUi错误提示框(alert）.
     * @param  msg
     * @return {dialog}
     */
    function _error(msg) {
        $.messager.alert('错误', msg, 'error');
    }

    /*
     * EasyUi确认框(alert）.
     * @param  msg,fn
     * @return {dialog}
     */
    function _confrim(msg,fn) {
        $.messager.confirm("操作确认", msg, function (r) {
            if (r) {
                fn();
            }
        });
    }

    /*
     * EasyUi弹出框（dialog）.
     * @param  url, title, callback(确认的回调函数),width,height,callload(加载成功的回调函数)
     * @return {dialog}
     */
    function _dialog(parameter) {
        parameter.width = parameter.width || 600;
        parameter.height = parameter.height || 400;
        var name = $('<div/>');
        eui.dialogArgs.handler =
            name.dialog({
                href: parameter.url,
                title: parameter.title,
                iconCls: 'icon-save',
                width: parameter.width,
                height: parameter.height,
                border: false,
                buttons: [{
                    id: 'ok',
                    text: '<span style="padding-right:10px;">确 认</span>',
                    iconCls: 'icon-ok',
                    handler: parameter.callback,
                }, {
                    text: '<span>关闭</span>',
                    iconCls: 'icon-cancel',
                    handler: function () { name.dialog('close'); }
                }],
                onClose: function () { name.dialog("destroy"); },
                onLoad: function () {
                    //加载之后的动作   
                    document.onkeydown = function (event) {
                        if (event.keyCode == "13") {
                            $('#ok').click();
                        }
                    }
                    if (parameter.success) parameter.success();
                },
                modal: true
            });
    }

})(jQuery);

