// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

$(document).ready(function(){
    $("#submitButton").click(function(){
        var query = $("#inputText").val();
        $("#loadingGif").show();
        $.ajax({
            url: '/api/OpenAI/UseChatGPT',
            type: 'GET',
            data: { query: query },
            success: function(data) {
                $("#loadingGif").hide();
                console.log(data)
                var textContent = data.value.choices[0].message.textContent;
                $("#responseDiv").text(textContent);
            },
            error: function(error) {
                $("#loadingGif").hide();
                console.log(error);
            }
        });
    });
});