/*側邊快速選單*/
var scrollSpy = new bootstrap.ScrollSpy(document.body, {
    target: '#sideNav'
})

/*圖片放大顯示*/
var select = 0;
var sel_val = 0;
//網頁載入完成執行
$(document).ready(function () {
    var id = $('#HiddenID').val();
    $("#div-select").empty();
    //將預覽圖div1~div13新增到#div-select元素    
    for (var i = 1; i <= 15; i++) {
        $("#div-select").append
            (
                "<div id='div" + i + "'><img src='https://raw.githubusercontent.com/PokaKai/rent_photo/main/" + id+i +".webp' alt=''></div>"
            );
        //預覽圖區塊div1~div13新增click事件處理函式fnChange
        //按下預覽圖會傳送num參數,此參數為圖片編號
        $("#div" + i).on("click", { num: i }, fnChange);

    }
    //判斷往左往右鈕是否存在
    iconShow();
    //按下左鍵執行fnPrev函式
    $("#btnPrev").on("click", fnPrev);
    //按下右鍵執行fnNext函式
    $("#btnNext").on("click", fnNext);

})

function fnChange(event) {
    var id = $('#HiddenID').val();
    //取得選取圖片編號並組成完整圖檔
    var filename = "https://raw.githubusercontent.com/PokaKai/rent_photo/main/"+id+event.data.num + ".webp"
    //顯示圖片
    $("#show").attr("src", filename);
    $("#show").attr("data-big", filename);
    $("#show").attr("class", "zoom");

    //以1呈現淡出動畫
    $("#show").hide().fadeIn(1000);
    //放大鏡jquery <![CDATA[
    /*! jquery.mlens.js - magnifying lens jQuery plugin for images by Federica Sibella (@musings.it) - Double licensed MIT and GPLv3 */
    !function (e) { function t(e) { if ("string" == typeof e) { var t = e.indexOf("_"); -1 != t && (e = e.substr(t + 1)) } return e } function i(e, t) { if ("string" == typeof e) { var i = e.indexOf(t); if (-1 != i) return !0 } return !1 } var n = [], r = 0, o = { init: function (t) { var o = { lensShape: "square", lensSize: [100, 100], borderSize: 4, borderColor: "#888", borderRadius: 0, imgSrc: "", imgSrc2x: "", lensCss: "", imgOverlay: "", overlayAdapt: !0, zoomLevel: 1, responsive: !0 }, a = e.extend({}, o, t), s = "100px", d = "100px"; if (i(a.lensSize, ",")) { var u = a.lensSize.indexOf(","), l = []; l[0] = a.lensSize.substring(0, u), l[1] = a.lensSize.substring(u + 1), a.lensSize = l } this.each(function () { var t = e(this), u = t.data("mlens"), l = e(), c = e(), g = e(), p = e(), h = t.attr("src"), m = "auto"; ("number" != typeof a.zoomLevel || a.zoomLevel <= 0) && (a.zoomLevel = o.zoomLevel); var v = new Image; "" !== a.imgSrc2x && window.devicePixelRatio > 1 ? (h = a.imgSrc2x, v.onload = function () { m = String(parseInt(this.width / 2) * a.zoomLevel) + "px", l.css({ backgroundSize: m + " auto" }), p.css({ width: m }) }, v.src = h) : ("" !== a.imgSrc && (h = a.imgSrc), v.onload = function () { m = String(parseInt(this.width) * a.zoomLevel) + "px", l.css({ backgroundSize: m + " auto" }), p.css({ width: m }) }, v.src = h), jQuery.isArray(a.lensSize) === !0 ? (s = i(a.lensSize[0], "%") || i(a.lensSize[0], "px") ? String(a.lensSize[0]) : String(a.lensSize[0]) + "px", d = i(a.lensSize[1], "%") || i(a.lensSize[1], "px") ? String(a.lensSize[1]) : String(a.lensSize[1]) + "px") : (s = i(a.lensSize, "%") || i(a.lensSize, "px") ? String(a.lensSize) : String(a.lensSize) + "px", d = s); var f = "background-position: 0px 0px;width: " + s + ";height: " + d + ";float: left;display: none;border: " + String(a.borderSize) + "px solid " + a.borderColor + ";background-repeat: no-repeat;position: absolute;", S = "position: absolute; width: 100%; height: 100%; left: 0; top: 0; background-position: center center; background-repeat: no-repeat; z-index: 1;"; switch (a.overlayAdapt === !0 && (S += "background-position: center center fixed; -webkit-background-size: cover; -moz-background-size: cover; -o-background-size: cover; background-size: cover;"), a.lensShape) { case "square": f = f + "border-radius:" + String(a.borderRadius) + "px;", S = S + "border-radius:" + String(a.borderRadius) + "px;"; break; case "": f = f + "border-radius:" + String(a.borderRadius) + "px;", S = S + "border-radius:" + String(a.borderRadius) + "px;"; break; case "circle": f += "border-radius: 50%;", S += "border-radius: 50%;"; break; default: f = f + "border-radius:" + String(a.borderRadius) + "px;", S = S + "border-radius:" + String(a.borderRadius) + "px;" }return t.wrap("<div id='mlens_wrapper_" + r + "' />"), g = t.parent(), a.responsive === !0 ? t.css({ width: "100%" }) : g.css({ width: t.width() }), l = e("<div id='mlens_target_" + r + "' style='" + f + "' class='" + a.lensCss + "'>&nbsp;</div>").appendTo(g), l.css({ backgroundImage: "url('" + h + "')", backgroundSize: m + " auto", cursor: "none" }), p = e("<img style='display:none;width:" + m + ";height:auto;max-width:none;max-height;none;' src='" + h + "' />").appendTo(g), "" !== a.imgOverlay && (c = e("<div id='mlens_overlay_" + r + "' style='" + S + "'>&nbsp;</div>"), c.css({ backgroundImage: "url('" + a.imgOverlay + "')", cursor: "none" }), c.appendTo(l)), t.attr("data-id", "mlens_" + r), l.mousemove(function (i) { e.fn.mlens("move", t.attr("data-id"), i) }), t.mousemove(function (i) { e.fn.mlens("move", t.attr("data-id"), i) }), l.on("touchmove", function (i) { i.preventDefault(); var n = i.originalEvent.touches[0] || i.originalEvent.changedTouches[0]; e.fn.mlens("move", t.attr("data-id"), n) }), t.on("touchmove", function (i) { i.preventDefault(); var n = i.originalEvent.touches[0] || i.originalEvent.changedTouches[0]; e.fn.mlens("move", t.attr("data-id"), n) }), l.hover(function () { e(this).show() }, function () { e(this).hide() }), t.hover(function () { l.show() }, function () { l.hide() }), l.on("touchstart", function (t) { t.preventDefault(), e(this).show() }), l.on("touchend", function (t) { t.preventDefault(), e(this).hide() }), t.on("touchstart", function (e) { e.preventDefault(), l.show() }), t.on("touchend", function (e) { e.preventDefault(), l.hide() }), t.data("mlens", { image: t, settings: a, target: l, imageTag: p, imgSrc: h, imgWidth: m, imageWrapper: g, overlay: c, instance: r, lensSizeHoriz: s, lensSizeVert: d }), u = t.data("mlens"), n[r] = u, r++, n }) }, move: function (e, i) { e = t(e); var r = n[e], o = r.image, a = r.target, s = r.imageTag, d = o.offset(), u = parseInt(i.pageX - d.left), l = parseInt(i.pageY - d.top), c = s.width() / o.width(), g = s.height() / o.height(); return u > 0 && l > 0 && u < o.width() && l < o.height() && (u = String(-((i.pageX - d.left) * c - a.width() / 2)), l = String(-((i.pageY - d.top) * g - a.height() / 2)), a.css({ backgroundPosition: u + "px " + l + "px" }), u = String(i.pageX - d.left - a.width() / 2), l = String(i.pageY - d.top - a.height() / 2), a.css({ left: u + "px", top: l + "px" })), r.target = a, n[e] = r, n }, update: function (t) { e(this).mlens("destroy"), e(this).mlens("init", t) }, destroy: function () { var i = t(e(this).attr("data-id")), r = n[i]; r.target.remove(), r.imageTag.remove(), r.image.unwrap(), r.overlay.remove(), e.removeData(r, "mlens"), this.unbind(), this.element = null } }; e.fn.mlens = function (t) { return o[t] ? o[t].apply(this, Array.prototype.slice.call(arguments, 1)) : "object" != typeof t && t ? void e.error("Method " + t + " does not exist on jQuery.mlens") : o.init.apply(this, arguments) } }(jQuery);
    //]]>

    //放大鏡
    $(".zoom").each(function () {
        var $this = $(this);
        $this.mlens({
            imgSrc: $this.attr("data-big"),
            lensShape: "circle", // 放大鏡形狀 circle(圓形), square(方形)
            lensSize: ["100px", "100px"], // 放大鏡長寬 (可使用 px 或百分比 %)
            borderSize: 3, // 放大鏡邊框寬度 (px)
            borderColor: "#fff", // 放大鏡邊框顏色色碼
            borderRadius: 0, // 如果放大鏡為方形 設定圓角程度
            overlayAdapt: true,
            zoomLevel: 1,
            responsive: true // 圖片是否自適應
        });
    }).parent().css("position", "relative");



}
//判斷往左往右鈕是否出現
function iconShow() {
    if (select < 1) {
        $("#btnPrev").attr("style", "display:none;");

    }
    else if (select > 1) {
        $("#btnNext").attr("style", "display:none;");

    }
    else {
        $("#btnPrev").removeAttr("style");
        $("#btnNext").removeAttr("style");
    }
}
//往左鈕事件處裡函式
function fnPrev() {
    if (select != 0) {
        select -= 1;
        sel_val += 100;
        $("#div-select").css("left", sel_val + "%");
    }
    iconShow()
}
function fnNext() {
    //往左捲動動畫  
    select += 1;
    sel_val -= 100;
    $("#div-select").css("left", sel_val + "%");
    iconShow()
}

$(function () {
    $('$div-select img').click(function () {
        $(this).addClass('active').siblings().removeClass('active');
    });
});