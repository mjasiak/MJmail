var Chat = function () {

};

Chat.prototype.onStart = function (name) {
    $(function () {

        chat.setScreen(false);

        // Declare a proxy to reference the hub. 
        var chatHub = $.connection.chatHub;

        chat.registerClientMethods(chatHub);

        // Start Hub
        $.connection.hub.start().done(function () {

            chat.registerEvents(chatHub,name)
        });      
    });
}

Chat.prototype.setScreen = function (isLogin) {

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

Chat.prototype.registerEvents = function (chatHub, name) {

    //$("#login").change(function () {
    //    var name = $("#login").val();
    //    if (name.length > 0) {
            chatHub.server.connect(name);
        //}
        //else {
        //    alert("Please enter name");
        //}
    //});
}

Chat.prototype.registerClientMethods = function (chatHub) {
    //chatHub.client.onConnected = function (id, userName, allUsers, messages) {
    chatHub.client.onConnected = function (id, userName, allUsers) {

        chat.setScreen(true);

        $('#loggedID').val(id);
        $('#loggedName').text(userName);

        // Add All Users
        for (i = 0; i < allUsers.length; i++) {

            chat.AddUser(chatHub, allUsers[i].ConnectionId, allUsers[i].UserName);
        }
    }

    chatHub.client.onNewUserConnected = function (id, userName) {
        chat.AddUser(chatHub, id, userName);
    }

    chatHub.client.onUserDisconnected = function (id) {
        chat.DeleteUser(id);
    }

    chatHub.client.sendMessage = function (windowId, userName, msgContent) {
        var divID = 'priv_' + windowId;
        var message = '<div class="cht_msg"><span>' + userName + ': </span>' + msgContent + '</div>';

        if ($("#" + divID).find(".cht_contentin").length == 0) {
            chat.CreatePrivateChatWindow(chatHub, windowId, divID, userName);
        }

        $("#" + divID).find(".cht_contentin").append(message);

        var height = $("#" + divID).find('.cht_contentin')[0].scrollHeight;
        $("#" + divID).find('.cht_contentin').scrollTop(height);
    }
}

Chat.prototype.DeleteUser = function (divID) {
    $('#' + divID).remove();
}

Chat.prototype.AddUser = function (chatHub, id, name) {

    var userId = $('#loggedID').val();

    var code = "";

    if (userId == id) {

    }
    else {

        code = $('<a id="' + id + '" class="user" >' + name + '</a>');

        $(code).dblclick(function () {

            var id = $(this).attr('id');

            if (userId != id)
                chat.OpenPrivateChatWindow(chatHub, id, name);

        });
    }

    $(".chat_menu-peoplelist").append(code);
}

Chat.prototype.OpenPrivateChatWindow = function (chatHub, id, name) {
    divID = 'priv_' + id;
    chat.CreatePrivateChatWindow(chatHub, id, divID, name);
};

Chat.prototype.CreatePrivateChatWindow = function (chatHub, id, divID, name) {
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

    chat.AddDivToContainer($messagebox);
};

Chat.prototype.AddDivToContainer = function ($div) {
    $('.chat_menu-talks').prepend($div);
}

