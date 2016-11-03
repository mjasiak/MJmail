$(function () {

    setScreen(false);

    // Declare a proxy to reference the hub. 
    var chatHub = $.connection.chatHub;

    registerClientMethods(chatHub);

    // Start Hub
    $.connection.hub.start().done(function () {

        registerEvents(chatHub)

    });

});

function setScreen(isLogin) {

    if (!isLogin) {

        $("#loggedName").hide();
        $(".chat_menu-people").hide();
        $(".loginDialog").show();
    }
    else {

        $("#loggedName").show();
        $(".chat_menu-people").show();
        $(".loginDialog").hide();
    }

}

function registerEvents(chatHub) {

    $("#login").change(function () {
        var name = $("#login").val();
        if (name.length > 0) {
            chatHub.server.connect(name);
        }
        else {
            alert("Please enter name");
        }

    });
}

function registerClientMethods(chatHub) {
    //chatHub.client.onConnected = function (id, userName, allUsers, messages) {
    chatHub.client.onConnected = function (id, userName, allUsers) {

        setScreen(true);

        $('#loggedID').val(id);
        $('#loggedName').text(userName);

        // Add All Users
        for (i = 0; i < allUsers.length; i++) {

            AddUser(chatHub, allUsers[i].ConnectionId, allUsers[i].UserName);
        }

        //// Add Existing Messages
        //for (i = 0; i < messages.length; i++) {

        //    AddMessage(messages[i].UserName, messages[i].Message);
        //}
    }

    chatHub.client.onNewUserConnected = function (id, userName) {
        AddUser(chatHub, id, userName);
    }

    chatHub.client.onUserDisconnected = function (id) {
        DeleteUser(id);
    }

    chatHub.client.sendMessage = function (windowId, userName, msgContent) {
        var divID = 'priv_' + windowId;
        var message = '<div class="cht_msg"><span>' + userName + ': </span>' + msgContent + '</div>';

        var $messagebox = $("#" + divID);

        if ($messagebox.find(".cht_contentin").length == 0) {
            CreatePrivateChatWindow(chatHub,windowId,divID,userName);
        }

        $messagebox.find(".cht_contentin").append(message);

        var height = $messagebox.find('.cht_contentin')[0].scrollHeight;
        $messagebox.find('.cht_contentin').scrollTop(height);
    }
}

function DeleteUser(divID) {
    $('#' + divID).remove();
}

function AddUser(chatHub, id, name) {

    var userId = $('#loggedID').val();

    var code = "";

    if (userId == id) {

    }
    else {

        code = $('<a id="' + id + '" class="user" >' + name + '</a>');

        $(code).dblclick(function () {

            var id = $(this).attr('id');

            if (userId != id)
                OpenPrivateChatWindow(chatHub, id, name);

        });
    }

    $(".chat_menu-peoplelist").append(code);
}

function OpenPrivateChatWindow(chatHub, id, name) {
    divID = 'priv_' + id;
    CreatePrivateChatWindow(chatHub, id, divID, name);
};

function CreatePrivateChatWindow(chatHub, id, divID, name) {
    var window = "<div class='cht_message' id=" + divID + "><div class='cht_head'><h2>" + name + "</h2><div class='cht_headnav'><i class='fa fa-times fa-lg'></i></div></div><div class='cht_contentout'><div class='cht_contentin'></div></div><div class='cht_input'><textarea></textarea></div></div>";

    var $messagebox = $(window);

    $messagebox.on('keypress','textarea',function (e) {
        if (e.which == 13) {
            var msg = $messagebox.find('textarea').val();
            chatHub.server.sendPrivateMessage(id, msg);
            $(this).val('');
        }
    });

    $messagebox.on('click','.cht_headnav', function () {
        $messagebox.remove();
    });

    AddDivToContainer($messagebox);
};

function AddDivToContainer($div) {
    $('.chat_menu-talks').prepend($div);
}