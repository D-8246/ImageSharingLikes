$(() => {
    console.log("hello");
    setInterval(function () {
        resetLikes();
    }, 1000);

    const id = $("#image-id").val();

    $("#like-button").on('click', function () {
       
        $.post('/home/like', { id }, function () {
            resetLikes();
            $("#like-button").prop('disabled', true);
        });
    });

   function resetLikes() {
       $.get('/home/getlikes', { id }, function (count) {
           $("#likes-count").text(count);
       });
    }
})

