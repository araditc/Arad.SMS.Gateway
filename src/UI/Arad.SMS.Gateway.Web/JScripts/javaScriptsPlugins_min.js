﻿$(document).ready(function (e) {
	e.prompt = function (t, n) {
		e.prompt.options = e.extend({}, e.prompt.defaults, n);
		e.prompt.currentPrefix = e.prompt.options.prefix;
		e.prompt.currentStateName = "";
		var r = e.browser.msie && e.browser.version < 7;
		var i = e(document.body);
		var s = e(window);
		e.prompt.options.classes = e.trim(e.prompt.options.classes);
		if (e.prompt.options.classes != "") e.prompt.options.classes = " " + e.prompt.options.classes;
		var o = '<div class="' + e.prompt.options.prefix + "box" + e.prompt.options.classes + '" id="' + e.prompt.options.prefix + 'box">';
		if (e.prompt.options.useiframe && (e("object, applet").length > 0 || r)) {
			o += '<iframe src="javascript:false;" style="display:block;position:absolute;z-index:-1;" class="' + e.prompt.options.prefix + 'fade" id="' + e.prompt.options.prefix + 'fade"></iframe>'
		} else {
			if (r) {
				e("select").css("visibility", "hidden")
			}
			o += '<div class="' + e.prompt.options.prefix + 'fade" id="' + e.prompt.options.prefix + 'fade"></div>'
		}
		o += '<div class="' + e.prompt.options.prefix + '" id="' + e.prompt.options.prefix + '"><div class="' + e.prompt.options.prefix + 'container"><div class="';
		o += e.prompt.options.prefix + 'close">X</div><div id="' + e.prompt.options.prefix + 'states"></div>';
		o += "</div></div></div>";
		e.prompt.jqib = e(o).appendTo(i);
		e.prompt.jqi = e.prompt.jqib.children("#" + e.prompt.options.prefix);
		e.prompt.jqif = e.prompt.jqib.children("#" + e.prompt.options.prefix + "fade");
		if (t.constructor == String) {
			t = {
				state0: {
					title: e.prompt.options.title,
					html: t,
					buttons: e.prompt.options.buttons,
					focus: e.prompt.options.focus,
					submit: e.prompt.options.submit
				}
			}
		}
		var u = "";
		e.each(t, function (n, r) {
			r = e.extend({}, e.prompt.defaults.state, r);
			t[n] = r;
			var i = "",
					s = "";
			if (r.position.arrow !== null) i = '<div class="' + e.prompt.options.prefix + "arrow " + e.prompt.options.prefix + "arrow" + r.position.arrow + '"></div>';
			if (r.title && r.title !== "") s = '<div class="' + e.prompt.options.prefix + "title " + e.prompt.options.prefix + 'title">' + r.title + "</div>";
			u += '<div id="' + e.prompt.options.prefix + "_state_" + n + '" class="' + e.prompt.options.prefix + '_state" style="display:none;">' + i + s + '<div class="' + e.prompt.options.prefix + 'message">' + r.html + '</div><div class="' + e.prompt.options.prefix + 'buttons">';
			e.each(r.buttons, function (t, r) {
				if (typeof r == "object") {
					u += "<button ";
					if (typeof r.classes !== "undefined") {
						u += 'class="' + (e.isArray(r.classes) ? r.classes.join(" ") : r.classes) + '" '
					}
					u += ' name="' + e.prompt.options.prefix + "_" + n + "_button" + r.title.replace(/[^a-z0-9]+/gi, "") + '" id="' + e.prompt.options.prefix + "_" + n + "_button" + r.title.replace(/[^a-z0-9]+/gi, "") + '" value="' + r.value + '">' + r.title + "</button>"
				} else {
					u += '<button name="' + e.prompt.options.prefix + "_" + n + "_button" + t + '" id="' + e.prompt.options.prefix + "_" + n + "_button" + t + '" value="' + r + '">' + t + "</button>"
				}
			});
			u += "</div></div>"
		});
		e.prompt.states = t;
		e.prompt.jqi.find("#" + e.prompt.options.prefix + "states").html(u).children("." + e.prompt.options.prefix + "_state:first").css("display", "block");
		e.prompt.jqi.find("." + e.prompt.options.prefix + "buttons:empty").css("display", "none");
		e.each(t, function (t, n) {
			var r = e.prompt.jqi.find("#" + e.prompt.options.prefix + "_state_" + t);
			if (e.prompt.currentStateName === "") e.prompt.currentStateName = t;
			r.bind("promptsubmit", n.submit);
			r.children("." + e.prompt.options.prefix + "buttons").children("button").click(function () {
				var i = e(this),
						s = r.children("." + e.prompt.options.prefix + "message"),
						o = n.buttons[i.text()] || n.buttons[i.html()];
				if (o == undefined) {
					for (var u in n.buttons)
						if (n.buttons[u].title == i.text() || n.buttons[u].title == i.html()) o = n.buttons[u].value
				}
				if (typeof o == "object") o = o.value;
				var a = {};
				e.each(e.prompt.jqi.find("#" + e.prompt.options.prefix + "states :input").serializeArray(), function (e, t) {
					if (a[t.name] === undefined) {
						a[t.name] = t.value
					} else if (typeof a[t.name] == Array || typeof a[t.name] == "object") {
						a[t.name].push(t.value)
					} else {
						a[t.name] = [a[t.name], t.value]
					}
				});
				var f = new e.Event("promptsubmit");
				f.stateName = t;
				f.state = r;
				r.trigger(f, [o, s, a]);
				if (!f.isDefaultPrevented()) {
					e.prompt.close(true, o, s, a)
				}
			});
			r.find("." + e.prompt.options.prefix + "buttons button:eq(" + n.focus + ")").addClass(e.prompt.options.prefix + "defaultbutton")
		});
		var a = function () {
			if (e.prompt.options.persistent) {
				var t = e.prompt.options.top.toString().indexOf("%") >= 0 ? s.height() * (parseInt(e.prompt.options.top, 10) / 100) : parseInt(e.prompt.options.top, 10),
						n = parseInt(e.prompt.jqi.css("top").replace("px", ""), 10) - t;
				e("html,body").animate({
					scrollTop: n
				}, "fast", function () {
					var t = 0;
					e.prompt.jqib.addClass(e.prompt.options.prefix + "warning");
					var n = setInterval(function () {
						e.prompt.jqib.toggleClass(e.prompt.options.prefix + "warning");
						if (t++ > 1) {
							clearInterval(n);
							e.prompt.jqib.removeClass(e.prompt.options.prefix + "warning")
						}
					}, 100)
				})
			} else {
				e.prompt.close(true)
			}
		};
		var f = function (t) {
			var n = window.event ? event.keyCode : t.keyCode;
			if (n == 27) {
				a()
			}
			if (n == 9) {
				var r = e(":input:enabled:visible", e.prompt.jqib);
				var i = !t.shiftKey && t.target == r[r.length - 1];
				var s = t.shiftKey && t.target == r[0];
				if (i || s) {
					setTimeout(function () {
						if (!r) return;
						var e = r[s === true ? r.length - 1 : 0];
						if (e) e.focus()
					}, 10);
					return false
				}
			}
		};
		e.prompt.position();
		e.prompt.style();
		e.prompt.jqif.click(a);
		s.resize({
			animate: false
		}, e.prompt.position);
		e.prompt.jqi.find("." + e.prompt.options.prefix + "close").click(e.prompt.close);
		e.prompt.jqib.bind("keydown keypress", f).bind("promptloaded", e.prompt.options.loaded).bind("promptclose", e.prompt.options.callback).bind("promptstatechanging", e.prompt.options.statechanging).bind("promptstatechanged", e.prompt.options.statechanged);
		e.prompt.jqif.fadeIn(e.prompt.options.overlayspeed);
		e.prompt.jqi[e.prompt.options.show](e.prompt.options.promptspeed, function () {
			e.prompt.jqib.trigger("promptloaded")
		});
		e.prompt.jqi.find("#" + e.prompt.options.prefix + "states ." + e.prompt.options.prefix + "_state:first ." + e.prompt.options.prefix + "defaultbutton").focus();
		if (e.prompt.options.timeout > 0) setTimeout(e.prompt.close, e.prompt.options.timeout);
		return e.prompt.jqib
	};
	e.prompt.defaults = {
		prefix: "jqi",
		classes: "",
		title: "",
		buttons: {
			Ok: true
		},
		loaded: function (e) { },
		submit: function (e, t, n, r) { },
		callback: function (e, t, n, r) { },
		statechanging: function (e, t, n) { },
		statechanged: function (e, t) { },
		opacity: .6,
		zIndex: 999,
		overlayspeed: "slow",
		promptspeed: "fast",
		show: "fadeIn",
		focus: 0,
		useiframe: false,
		top: "15%",
		persistent: true,
		timeout: 0,
		state: {
			title: "",
			html: "",
			buttons: {
				Ok: true
			},
			focus: 0,
			position: {
				container: null,
				x: null,
				y: null,
				arrow: null
			},
			submit: function (e, t, n, r) {
				return true
			}
		}
	};
	e.prompt.currentPrefix = e.prompt.defaults.prefix;
	e.prompt.currentStateName = "";
	e.prompt.setDefaults = function (t) {
		e.prompt.defaults = e.extend({}, e.prompt.defaults, t)
	};
	e.prompt.setStateDefaults = function (t) {
		e.prompt.defaults.state = e.extend({}, e.prompt.defaults.state, t)
	};
	e.prompt.position = function (t) {
		var n = e.fx.off,
				r = e(window),
				i = e(document.body).outerHeight(true),
				s = e(window).height(),
				o = e(document).height(),
				u = i > s ? i : s,
				a = parseInt(r.scrollTop(), 10) + (e.prompt.options.top.toString().indexOf("%") >= 0 ? s * (parseInt(e.prompt.options.top, 10) / 100) : parseInt(e.prompt.options.top, 10));
		if (t !== undefined && t.data.animate === false) e.fx.off = true;
		e.prompt.jqib.css({
			position: "absolute",
			height: u,
			width: "100%",
			top: 0,
			left: 0,
			right: 0,
			bottom: 0
		});
		e.prompt.jqif.css({
			position: "absolute",
			height: u,
			width: "100%",
			top: 0,
			left: 0,
			right: 0,
			bottom: 0
		});
		if (e.prompt.states[e.prompt.currentStateName].position.container) {
			var f = e.prompt.states[e.prompt.currentStateName].position,
					l = e(f.container).offset();
			if (e.isPlainObject(l) && l.top !== undefined) {
				e.prompt.jqi.css({
					position: "absolute"
				});
				e.prompt.jqi.animate({
					top: l.top + f.y,
					left: l.left + f.x,
					marginLeft: 0,
					width: f.width !== undefined ? f.width : null
				});
				a = l.top + f.y - (e.prompt.options.top.toString().indexOf("%") >= 0 ? s * (parseInt(e.prompt.options.top, 10) / 100) : parseInt(e.prompt.options.top, 10));
				e("html,body").animate({
					scrollTop: a
				}, "slow", "swing", function () { })
			}
		} else {
			e.prompt.jqi.css({
				position: "absolute",
				top: a,
				left: "50%",
				marginLeft: e.prompt.jqi.outerWidth() / 2 * -1
			})
		} if (t !== undefined && t.data.animate === false) e.fx.off = n
	};
	e.prompt.style = function () {
		e.prompt.jqif.css({
			zIndex: e.prompt.options.zIndex,
			display: "none",
			opacity: e.prompt.options.opacity
		});
		e.prompt.jqi.css({
			zIndex: e.prompt.options.zIndex + 1,
			display: "none"
		});
		e.prompt.jqib.css({
			zIndex: e.prompt.options.zIndex
		})
	};
	e.prompt.getStateContent = function (t) {
		return e("#" + e.prompt.currentPrefix + "_state_" + t)
	};
	e.prompt.getCurrentState = function () {
		return e("." + e.prompt.currentPrefix + "_state:visible")
	};
	e.prompt.getCurrentStateName = function () {
		var t = e.prompt.getCurrentState().attr("id");
		return t.replace(e.prompt.currentPrefix + "_state_", "")
	};
	e.prompt.goToState = function (t, n) {
		var r = new e.Event("promptstatechanging");
		e.prompt.jqib.trigger(r, [e.prompt.currentStateName, t]);
		if (!r.isDefaultPrevented()) {
			e.prompt.currentStateName = t;
			e("." + e.prompt.currentPrefix + "_state").slideUp("slow").find("." + e.prompt.currentPrefix + "arrow").fadeOut();
			e("#" + e.prompt.currentPrefix + "_state_" + t).slideDown("slow", function () {
				var r = e(this);
				r.find("." + e.prompt.currentPrefix + "defaultbutton").focus();
				r.find("." + e.prompt.currentPrefix + "arrow").fadeIn("slow");
				if (typeof n == "function") {
					e.prompt.jqib.bind("promptstatechanged.tmp", n)
				}
				e.prompt.jqib.trigger("promptstatechanged", [t]);
				if (typeof n == "function") {
					e.prompt.jqib.unbind("promptstatechanged.tmp")
				}
			});
			e.prompt.position()
		}
	};
	e.prompt.nextState = function (t) {
		var n = e("#" + e.prompt.currentPrefix + "_state_" + e.prompt.currentStateName).next();
		e.prompt.goToState(n.attr("id").replace(e.prompt.currentPrefix + "_state_", ""), t)
	};
	e.prompt.prevState = function (t) {
		var n = e("#" + e.prompt.currentPrefix + "_state_" + e.prompt.currentStateName).prev();
		e.prompt.goToState(n.attr("id").replace(e.prompt.currentPrefix + "_state_", ""), t)
	};
	e.prompt.close = function (t, n, r, i) {
		e.prompt.jqib.fadeOut("fast", function () {
			if (t) {
				e.prompt.jqib.trigger("promptclose", [n, r, i])
			}
			e.prompt.jqib.remove();
			e("window").unbind("resize", e.prompt.position);
			if (e.browser.msie && e.browser.version < 7 && !e.prompt.options.useiframe) {
				e("select").css("visibility", "visible")
			}
		})
	};
	e.fn.extend({
		prompt: function (t) {
			if (t == undefined) t = {};
			if (t.withDataAndEvents == undefined) t.withDataAndEvents = false;
			e.prompt(e(this).clone(t.withDataAndEvents).html(), t)
		},
		promptDropIn: function (t, n) {
			var r = e(this);
			if (r.css("display") == "none") {
				var i = r.css("top");
				r.css({
					top: e(window).scrollTop(),
					display: "block"
				}).animate({
					top: i
				}, t, "swing", n)
			}
		}
	})
});