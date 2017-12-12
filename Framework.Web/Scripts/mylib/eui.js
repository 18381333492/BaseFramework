

function eui() {
    this.config = function (name, controller) {
        var path = '/' + name + '/' + controller + '/';
        var object = {
            grid: $('#' + controller),
            toolbar: '#' + controller + '_toolbar',
            form: { add: '#add_form', edit: '#edit_form' },
            Insert: { handle: null },
            Update: { handle: null },
            Cancel: { handle: null },
            route: function (name) {
                return path + name;
            },
            button: {
                enable: function () { $('#ok').eq(0).linkbutton('enable'); },
                disable: function () { $('#ok').eq(0).linkbutton('disable'); }
            }
        }
        return object;
    }

    /*
    * EasyUi提示框（messager）.
    * @author [汤台]
    * @version 1.0.0
    * @param   msg, title, icon, fn(确认回调函数)
    * @return {dialog}
    * @icon   {String}error（X）,question(?),info,warning(!)
    */
    this.alert = function (msg, title, icon, fn) {
        title = title || "提示"
        icon = icon || 'info';
        $.messager.alert(title, msg, icon, fn);
    }


    /*
    * ajax的post请求的封装（form）.
    * @author [汤台]
    * @version 1.0.0
    * @param   url,data,callback,er_callback,async
    * @return {void}
    */
    this.post = function (url, data, callback, er_callback,async) {
        var f = new eui();
        $.ajax({
            url: url,
            data: data,
            type: 'POST',
            dataType: 'json',
            async: (async == null) ? true : async,
            success: function (r) {
                if (!r.over)
                { /*判断登录是否过期*/
                    if (r.success) {
                        callback(r);
                    }
                    else {
                        if (er_callback) {
                            er_callback(r);//手动提示错误
                        }
                        else {
                            f.alert(r.info);
                            //f.alert("操作失败,请联系管理员!");
                            $('#ok').eq(0).linkbutton('enable');//启用按钮
                        }
                    }
                }
                else {
                    f.alert("登录过期,请重新登录");
                    location.href = "/Admin/Home/Tip";
                }
            },
            // jqXHR 是经过jQuery封装的XMLHttpRequest对象
            // textStatus 可能为null、 'timeout（超时）'、 'error（错误）'、 'abort(中止)'和'parsererror（解析错误)'等
            // errorMsg 是错误信息字符串(响应状态的文本描述部分，例如'Not Found'或'Internal Server Error')
            error: function (jqXHR, textStatus, errorMsg) {
                switch (jqXHR.status) {
                    case 404: f.alert('链接地址错误!', null, 'error'); break;
                    case 500: f.alert('服务器内部错误!', null, 'error'); break;
                    default: f.alert(jqXHR.status + ":" + jqXHR.statusText, null, 'error');
                }
            }
        });
    }

    /*
    * EasyUi弹出框（dialog）.
    * @author [汤台]
    * @version 1.0.0
    * @param  url, title, callback(确认的回调函数),width,height,callload(加载成功的回调函数)
    * @return {dialog}
    */
    this.dialog = function (url, title, callback, width, height, callload) {
        width = width || 600;
        height = height || 400;
        var name = $('<div/>');
        var dlg =
            name.dialog({
                href: url,
                title: title,
                iconCls: 'icon-save',
                width: width,
                height: height,
                border: false,
                buttons: [{
                    id:'ok',
                    text: '<span style="padding-right:10px;">确 认</span>',
                    iconCls: 'icon-ok',
                    handler: callback,
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
                    if (callload) callload();
                },
                modal: true
            });
        return dlg;
    }

   /*
   * EasyUi详情弹出框（dialog）.
   * @author [汤台]
   * @version 1.0.0
   * @param  url, title,width,height,callload(加载成功的回调函数)
   * @return {dialog}
   */
    this.detail = function (url, title,width, height, callload) {
        width = width || 600;
        height = height || 400;
        var name = $('<div/>');
        var dlg =
            name.dialog({
                href: url,
                title: title,
                iconCls: 'icon-save',
                width: width,
                height: height,
                border: false,
                buttons: [{
                    id: 'ok',
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
                    if (callload) callload();
                },
                modal: true
            });
        return dlg;
    }
}