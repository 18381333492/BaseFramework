
//home.html的相关的js

$(function () {
    home.initPhone();
    home.bingEvent();
});

var home= (function (obj) { return obj; }(new function () {

    var inputPhone;
    var userPhone;//用户输入的手机充值号码
    var cancelIcon;//删除图标
    var carrynoTip;//运营商

    //初始化手机号
    function initPhone() {
        inputPhone = $('input[name=mobile]');
        userPhone = localStorage.getItem("recharge_mobile");
        cancelIcon = $('.weui-icon-clear');
        carrynoTip = $('.tip');
        if (userPhone) {
            inputPhone.val(userPhone);
            cancelIcon.show();//显示删除按钮
            var phone = client.string.trimAll(userPhone);
            if (client.regex.isPhone(phone)) {
                //手机号码验证通过
                LoadMobile(phone);
            }
        }
    }

    //绑定相关事件
    function bingEvent() {
        //删除图标的点击事件
        cancelIcon.on("click", function () {
            inputPhone.val("");
            inputPhone.removeClass("mobile_focus");
            cancelIcon.hide();
            inputPhone.focus();
            unableStyle();
        });

        //输入手机号码键盘弹起的事件
        inputPhone.on("keyup", function (e) {
            var value = $(this).val();
            if (e.keyCode != 8) {
                //判断键盘输入的是否是数字
                if (client.regex.isNumber(value.substr(value.length - 1, 1))) {
                    //加上空格
                    if (value.length == 4 || value.length == 9) {
                        var i = value.length;
                        value = client.string.insert(value, i - 1, " ");
                        $(this).val(value);
                    }
                }
                else {
                    //输入的不是数字
                    $(this).val(value.substr(0, value.length - 1));
                    if ($(this).val().length == 0)//输入的第一个就不是数字移除样式
                        $(this).removeClass("mobile_focus");
                }
            }
            else {
                //回退按键的处理
                if (value.length == 9 || value.length == 4)
                    value = $(this).val(value.trim());
                if (value.length < 13) {
                    unableStyle();//展示默认样式
                }
            }
        });

        //手机号码值发生变化的事件
        inputPhone.on("input", function () {
            var value = $(this).val();
            if (value.length > 0) {
                //处理直接赋值号码
                var temp=value.replace(/\D/g, '');//去掉非数字的字符
                if (client.regex.isPhone(temp)) {
                    value = client.string.insert(temp, 3, ' ');
                    value = client.string.insert(value, 8, ' ');
                    $(this).val(value);
                }
            }
            if (value.length > 0) {
                cancelIcon.show();//显示删除图标
                $(this).addClass("mobile_focus");
            }
            if (value.length == 0) {
                cancelIcon.hide();//隐藏删除图标
                $(this).removeClass("mobile_focus");
            }
            if (value.trim().length == 13) {
                $(this).blur(); //失去焦点
                value = client.string.trimAll(value);//去掉空格
                if (client.regex.isPhone(value)) {
                    //手机号码验证通过
                    //  LoadMobile(value);
                }
                else {
                    dialog.alert("手机号码格式错误");
                }
            }
        });
    }

    //可用样式
    function ableStyle() {
        $('ul').removeClass("unable_ul").addClass("able_ul");

    }
    //默认的样式
    function unableStyle() {
        if ($('ul').hasClass("able_ul"))
            $('ul').removeClass("able_ul").addClass("unable_ul");
        carrynoTip.val("移动 联通 电信");
        carrynoTip.css("color", "#ccc");
    }


    return {
        initPhone: initPhone,
        bingEvent: bingEvent
    }

}));

