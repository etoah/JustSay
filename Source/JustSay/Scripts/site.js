
$(document).ready(function () {
    $("#justsaymodal").on("hidden.bs.modal", function () {
        $(this).removeData("bs.modal");
    });
    //header
    $("[href='#confesssearch']").click(function () {
        var url = "/confess/search/" + $("#key").val();
        window.location.href = url;
    })
    $("[href='#relationsearch']").click(function () {
        var url = "/relation/search/" + $("#key").val();
        window.location.href = url;
    })

    //管理操作
    $("[data-control]").click(function () {
        var url = "/" + $("#controltype").val() + "/" + $(this).data("control") + "/" + $("#id").val();
        window.location.href = url;
    })
    $('.dropdown-toggle').dropdownHover();

});
function ModalConfig(title,isLarge)
{
    if (title) {
        $("#ModalTitle").html(title);
    }
    else {
        $(".modal-header").hide();
    }

    if (isLarge)
    {
        $(".modal-dialog").addClass("modal-lg");
    }
}

function sendsuccess(content)
{
    //$(".modal-content").html("<p class='text-center'><h3>" + content + "</h3></p>");
    $(".modal-content").html("<div class='modal-header'><button type='button' class='close' data-dismiss='modal'><span aria-hidden='true'>&times;</span><span class='sr-only'>Close</span></button><h4 class='modal-title' id='myModalLabel'>信息</h4></div><p class='text-center'><h4>" + content + "</h4></p>" +
        "<div class='modal-footer'><button type='button' class='btn btn-primary' data-dismiss='modal'>确定</button></div>");
}

function AjaxMessage(content) {
    $(".modal-content").html("<div class='modal-header'><button type='button' class='close' data-dismiss='modal'><span aria-hidden='true'>&times;</span><span class='sr-only'>Close</span></button><h4 class='modal-title' id='myModalLabel'>信息</h4></div><p class='text-center'><h4>" + content + "</h4></p>"+
        "<div class='modal-footer'><button type='button' class='btn btn-primary' data-dismiss='modal'>确定</button></div>");
    $('#justsaymodal').modal('show')
}

//以下是+1的代码


(function ($) {
    $.extend({
        tipsBox: function (options) {
            options = $.extend({
                obj: null,  //jq对象，要在那个html标签上显示
                str: "+1",  //字符串，要显示的内容;也可以传一段html，如: "<b style='font-family:Microsoft YaHei;'>+1</b>"
                startSize: "20px",  //动画开始的文字大小
                endSize: "40px",    //动画结束的文字大小
                interval: 1000,  //动画时间间隔
                color: "red",    //文字颜色
                callback: function () { }    //回调函数
            }, options);
            $("body").append("<span class='num'>" + options.str + "</span>");
            var box = $(".num");
            var left = options.obj.offset().left + options.obj.width() / 2;
            var top = options.obj.offset().top - options.obj.height();
            box.css({
                "position": "absolute",
                "left": left + "px",
                "top": top + "px",
                "z-index": 9999,
                "font-size": options.startSize,
                "line-height": options.endSize,
                "color": options.color
            });
            box.animate({
                "font-size": options.endSize,
                "opacity": "0",
                "top": top - parseInt(options.endSize) + "px"
            }, options.interval, function () {
                box.remove();
                options.callback();
            });
        }
    });
})(jQuery);


function showTip(id, msg) {
    var size = "40px";
    if (msg != "+1")
        size = "80px";

    $.tipsBox({
        obj: $("#" + id.toString()),
        str: msg,
        endSize: size,
        callback: function () {
            //alert(5);
        }
    });


}

/*
 * Project: Twitter Bootstrap Hover Dropdown
 * Author: Cameron Spear
 * Contributors: Mattia Larentis
 *
 * Dependencies?: Twitter Bootstrap's Dropdown plugin
 *
 * A simple plugin to enable twitter bootstrap dropdowns to active on hover and provide a nice user experience.
 *
 * No license, do what you want. I'd love credit or a shoutout, though.
 *
 * http://cameronspear.com/blog/twitter-bootstrap-dropdown-on-hover-plugin/
 */(function (e, t, n) { var r = e(); e.fn.dropdownHover = function (n) { r = r.add(this.parent()); return this.each(function () { var n = e(this).parent(), i = { delay: 500, instantlyCloseOthers: !0 }, s = { delay: e(this).data("delay"), instantlyCloseOthers: e(this).data("close-others") }, o = e.extend(!0, {}, i, o, s), u; n.hover(function () { o.instantlyCloseOthers === !0 && r.removeClass("open"); t.clearTimeout(u); e(this).addClass("open") }, function () { u = t.setTimeout(function () { n.removeClass("open") }, o.delay) }) }) }; e('[data-hover="dropdown"]').dropdownHover() })(jQuery, this);