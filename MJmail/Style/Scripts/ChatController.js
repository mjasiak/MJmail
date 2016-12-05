var Chat = function () {

};

var chat = new Chat();

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

            chatHub.server.connect(name);

            $('#addButton').click(function () {
                var friendName = $('#friendName').val();
                chatHub.server.addFriend(friendName);
                $('.addFriendOuter').hide();
            });

            $('.chat_menu-friendelete').find('.btn-danger').click(function () {
                var name = $('.chat_menu-friendelete').attr('id');
                chatHub.server.deleteFriend(name);
                $('.chat_menu-friendelete').fadeOut();
            });
}

Chat.prototype.registerClientMethods = function (chatHub) {

    chatHub.client.onConnected = function (id, userName, allUsers, friends) {

        chat.setScreen(true);

        $('#loggedID').val(id);
        $('#loggedName').text(userName);

        // Add All Users
        for (i = 0; i < allUsers.length; i++) {

            chat.AddUser(chatHub, allUsers[i].ConnectionId, allUsers[i].UserName);
        }

        // Add Friends to List
        chat.ListFriends(chatHub,friends);
    }

    chatHub.client.onNewUserConnected = function (id, userName, userEmail) {
        chat.AddUser(chatHub, id, userName);
        chat.ChangeFriendsStatus(chatHub, id, userName, userEmail);
    }

    chatHub.client.onUserDisconnected = function (id, userName, userEmail) {
        chat.DeleteUser(id);
        chat.ChangeFriendsStatus(chatHub, id, userName, userEmail);
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
    
    chatHub.client.friendsToList = function (friends) {
        chat.ListFriends(chatHub,friends);
    }
}

Chat.prototype.ListFriends = function (chatHub, friends) {
    chat.CleanElementChildren('.chat_menu-friendslist');
    for (i = 0; i < friends.length; i++) {
        var friend = "";
        if (friends[i].Friend == friends[i].FriendUserName) friend = friends[i].Friend;
        else friend = friends[i].FriendUserName;
        chat.AddListedFriend(chatHub, friends[i].ConnectionID, friend);
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
            if (userId != id) chat.OpenPrivateChatWindow(chatHub, id, name);
        });
    }
    $(".chat_menu-peoplelist").append(code);
}

Chat.prototype.ChangeFriendsStatus = function (chatHub, id, userName, userEmail) {
    var codesFriends = $(".chat_menu-friendslist").find("div.user");
    for (i = 0; i < codesFriends.length; i++) {
        var codeFriends = $(codesFriends[i]).find('a');
        var friendAccesibility = $(codesFriends[i]).find('div.user-access');
        if(codeFriends.text() == userName || codeFriends.text() == userEmail)
        {
            if (friendAccesibility.hasClass("user-inactive")) {
                friendAccesibility.removeClass("user-inactive");
                friendAccesibility.addClass("user-active");
                $(codeFriends).attr('id', id);
                chat.openChatEventCreate(chatHub, id, userName, codesFriends[i]);

            }
            else {
                friendAccesibility.removeClass("user-active");
                friendAccesibility.addClass("user-inactive");
                $(codeFriends).attr('id', null);
                chat.openChatEventCreate(chatHub, null, userName, codesFriends[i]);
            }         
        }
    }
}

Chat.prototype.AddListedFriend = function(chatHub,id,name){
    code = "";
    if (id != null) {
        code = $('<div class="user"><div class="user-active user-access"></div><a id="' + id + '">' + name + '</a><div class="delete-user" id="' + name + '"><i class="fa fa fa-times" aria-hidden="true"></i></div></div>');
        //$(code).find('.delete-user').dblclick(function () {
        //    alert('Czy na pewno usunąć ' + this.id + '?');
        //});
        chat.openChatEventCreate(chatHub, id, name, code);
    }
    else {
        code = $('<div class="user"><div class="user-inactive user-access"></div><a id="' + id + '">' + name + '</a><div class="delete-user" id="' + name + '"><i class="fa fa fa-times" aria-hidden="true"></i></div></div>');
        
        chat.openChatEventCreate(chatHub, id, name, code);
    }   
    $(".chat_menu-friendslist").append(code);
}

Chat.prototype.openChatEventCreate = function (chatHub, id, name, code) {
    $(code).children().off();
    if (id != null) {
        $(code).find('a').dblclick(function () {
            var id = $(this).find('a').attr('id');
            chat.OpenPrivateChatWindow(chatHub, id, name);
        });       
    }
    else {
        $(code).find('a').dblclick(function () {

            alert("Użytkownik " + name + " nie jest zalogowany!");
        });
    }
    $(code).find('.delete-user').dblclick(function () {
        var chatFriend = $('.chat_menu-friendelete');
        $(chatFriend).find('h6').text("Do you really want delete friend " + name + "?");
        $(chatFriend).attr('id', name);
        $(chatFriend).fadeIn();
    });
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

Chat.prototype.chatFriendHandling = function () {
    $('#addFriendExpand').click(function () {
        if ($('#friendName').value != 'add friend') $('#friendName').value = 'add friend';
        $('.addFriendOuter').fadeIn();
    });    

    $('.addFriendOuter i.fa-times').click(function () {
        $('.addFriendOuter').fadeOut();
    });

    $(".chat_menu-friendelete").find('.btn-default').click(function () {
        $('.chat_menu-friendelete').fadeOut();
    });
}

Chat.prototype.CleanElementChildren = function (element) {
    $(element).empty();
}

$(document).ready(function () {
    var chatController = new Chat();

    chatController.chatFriendHandling();
});