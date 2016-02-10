/// <reference path="jquery-1.8.2.js" />
/// <reference path="jquery.validate-vsdoc.js" />
/// <reference path="_references.js" />

$(document).ready(function () {

    var chat = $.connection.chatHub;

    $.connection.hub.start().done(function () {
        $('#chat').click(function () {
            var name = $('#displayname').val();
            chat.server.connect(name);
        });


        $('#disconnect').click(function () {
            var name = $('#displayname').val();
            chat.server.disconnect(name);

        });

        $(":input[data-click]").each(function () {
            $(this).click(function () {
                var u = $("#displayname").val();
                var s = $(this).val();
                if ($(this).hasClass("notify")) {
                    $(this).removeClass("notify").addClass("user");

                }
                chat.server.showwindow(u, s);
            });
        });
        $('#send_private').each(function () {
            $(this).click(function () {

                if ($('#msg').val().length > 0) {
                    var u = $('#displayname').val();
                    var s = $('#touser').text();
                    chat.server.privateMessage(u, s, $('#msg').val());
                    $('#msg').val('').focus();
                }
            });

        });

    });

    chat.client.onConnected = function (name) {

        $(":input[data-click]").each(function () {
            var p = $(this).val();

            if (p == name) {
                if ($(this).hasClass("user1")) {
                    $(this).removeClass("user1").addClass("user");
                }
            }
        });
    };
    chat.client.onConnected1 = function (name, li) {
        $('#total').show();
        $('#members').show();
        var s = "You are joined as: " + name;
        $('#touser').text(s);
        $(":input[data-click]").each(function () {
            var p = $(this).val();
            var i;
            for (i = 0; i < li.length; i++) {
                if (p == li[i]) {
                    if ($(this).hasClass("user1")) {
                        $(this).removeClass("user1").addClass("user");
                    }

                }
            }
            if (p == name) {
                $(this).removeClass("user").addClass("user1");
            }
        });
    };

    chat.client.addNewMessageToPage = function (name, time, message) {
        $('#discussion').append('<li><strong>' + name
            + '</strong>' + '<a style="color:Blue;font:10px Tahoma">' + time + '</a>' + message + '</li>');
    };

    chat.client.onDisconnected1 = function (name) {

        var i;
        $(":input[class=user]").each(function () {
            var p = $(this).val();
            if (p == name) {
                $(this).removeClass("user").addClass("user1");
            }
        });
        var u = $("#displayname").val();
        if (u == name) {

            $('#total').hide();
            $('#members').hide();
        }                     //$('#total').addClass("h");


    };
    chat.client.onDisconnected2 = function () {

        $('#total').addClass("h");
        $('#members').hide();
    };


    chat.client.primessage = function (s, li) {
        //$('#total').removeClass("h").addClass("v");
        $('#touser').text(s);
        var i;
        $('#pc_ul').empty();
        for (i = 0; i < li.length; i++) {

            $('#pc_ul').append('<li style="list-style:none">' + '<a style="color:gray;font:.9em Tahoma">' + li[i] + '</a>' + '</li>');


        }


    };
    chat.client.show_pri_win = function (u, s, li) {

        var user = $("#displayname").val();
        var r = $('#touser').text();

        if (user == s) {
            if (r != u) {
                $(":input[class=user]").each(function () {
                    var p = $(this).val();
                    if (p == u) {
                        $(this).removeClass("user").addClass("notify");
                    }
                });
            }
            else {
                //    //if (p == touser) {
                var i;
                $('#pc_ul').empty();
                for (i = 0; i < li.length; i++) {

                    $('#pc_ul').append('<li style="list-style:none">' + '<a style="color:maroon;font:1em Tahoma">' + li[i] + '</a>' + '</li>');

                }
            };


            //    //$('#total').removeClass("h").addClass("v");
            //    $('#touser').text(name);
        };


    };


    chat.client.privatemsg = function (u, s, msg) {

        var p = $('#touser').text();
        var t = $("#displayname").val();
        if (p == u) {
            $('#pc_ul').append('<li style="list-style:none">' + '<a style="color:maroon;font:1em Tahoma">' + s + '</a>' + ":  " + '<a style="color:blue;font:1em Tahoma">' + msg + '</a>' + '</li>');
        }
        if (p == s) {
            $('#pc_ul').append('<li style="list-style:none>' + u + ":  " + '<a style="color:Blue;font:1em Tahoma">' + msg + '</a>' + '</li>');
        }

    };


});
