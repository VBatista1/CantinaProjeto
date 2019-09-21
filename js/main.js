$(document).ready(function(){
    $("#enviar").click(function(){
        $.ajax({
            url: "http://localhost:58687/api/values",
            type: "GET",
            dataType: 'json',
            data: ({
                email : $('#your_email').val(),
                password: $('#your_pass').val()
            }),
            crossDomain: true,
            success: function(data, textStatus, xhr) {
                console.log(data);
            },
            error: function(xhr,textStatus, errorThrown){
                console.log(xhr, textStatus, errorThrown);
                console.log('Error in Operation');
            }
        });
    });
});