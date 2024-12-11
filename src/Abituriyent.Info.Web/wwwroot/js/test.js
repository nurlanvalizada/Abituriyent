function content() {
    $("#content").html('<div  id="editor">' + $("#content").html() + '</div>');
    CKEDITOR.replace('editor', {
        filebrowserUploadUrl: '/Admin/UploadTestImage',
        filebrowserWindowWidth: '640',
        filebrowserWindowHeight: '480'
    });
    $("#edit").css("display", "none");
    $("#save").css("display", "block");
};
function save() {

    var content = $("#content").contents().find("iframe").contents().find('body').html();
    $("#testcontent").val(content);
    $("#content").html(content);
    $("#edit").css("display", "block");
    $("#save").css("display", "none");
};
function contentAnswer(id) {
    var answer = $("#answer" + id).val();
    $("#answerContent" + id).html('<div  id="editor' + id + '">' + answer + '</div>');
    CKEDITOR.replace('editor' + id, {
        filebrowserUploadUrl: '/Admin/UploadTestImage',
        filebrowserWindowWidth: '640',
        filebrowserWindowHeight: '480'
    });
    $("#edit-" + id).css("display", "none");
    $("#save-" + id).css("display", "block");
};
function saveAnswer(id) {
    var content = $("#answerContent" + id).contents().find("iframe").contents().find('body').html();
    $("#answer" + id).val(content);
    $("#answerContent" + id).html('<label class="answer"><input style="border: 2px solid black"  id="@Model.Test.Id" name="CorrectAnswerId" value="' + id + '" type="radio">' + content + '</label>');
    $("#edit-" + id).css("display", "block");
    $("#save-" + id).css("display", "none");
};
function add(id) {
    $.post("../../Admin/TestAnswerAdd", $("form").serialize(), function (data, status) {
        $("#editcontent").html(data.replace(/&lt;/g, '<').replace(/&gt;/g, '>'));
    });
};
function deleteAnswer(id) {
    $.post("../../Admin/TestAnswerDelete?id=" + id, $("form").serialize(), function (data, status) {
        $("#editcontent").html(data.replace(/&lt;/g, '<').replace(/&gt;/g, '>'));
    });
};