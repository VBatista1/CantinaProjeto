$(document).ready(function(){

    $("#cadastrar").click(function(){
        var Nome = $('#name').val();
        var Email = $('#email').val();
        var Senha = $('#pass').val();
        $.ajax({
            url: "http://localhost:7071/api/CreateUser",
            type: "POST", 
            data: JSON.stringify({
                Nome: Nome,
                Email: Email,
                Senha: Senha
            }),
            crossDomain: true,
            success: function(data, textStatus, xhr) {
                console.log(data);
                $('#successModal').modal('toggle');
            },
            error: function(xhr,textStatus, errorThrown){
                console.log(xhr, textStatus, errorThrown);
                console.log('Error in Operation');
            }
        });
    });

    $("#enviar").click(function(){
        var Email = $('#your_email').val();
        var Senha = $('#your_pass').val();
        $.ajax({
            url: "http://localhost:7071/api/LoginUser",
            type: "POST",
            dataType: 'json',
            data: JSON.stringify({
                Email: Email,
                Senha: Senha
            }),
            crossDomain: true,
            success: function(data, textStatus, xhr) {
                console.log(data);
                window.location.href= "user.html";
            },
            error: function(xhr,textStatus, errorThrown){
                $('#errorModal').modal('toggle');
                console.log(xhr, textStatus, errorThrown);
                console.log('Error in Operation');
            }
        });
    });
});