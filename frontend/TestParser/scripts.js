$( document ).ready(function(){
    $('form#subForm').submit(function(){

        var formData = new FormData(this);
    
        $.ajax({
            url: "http://localhost:62085/api/UploadFile",
            type: 'POST',
            data: formData,
            async: false,
            success: function (data) {
                for (var i = 0; i < data.length; i++) {
                    var str = `${data[i].lineNumber}&nbsp;&nbsp;
                    ${data[i].startTime } --> 
                    ${data[i].endTime }&nbsp;&nbsp;
                    ${data[i].value } 
                 <br>`
                   $(".result").append(str)
                }
            },
            cache: false,
            contentType: false,
            processData: false
        });
        return false;
    });
});
