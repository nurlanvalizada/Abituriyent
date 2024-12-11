$("#checkResult")
    .click(function () {
        var closedTestAnswers = $("input:checked[name^=optionsRadios]");
        var openTestAnswers = $("input[type=text][name^=openValue]");
        var lessonId = $("input:hidden[name=lessonId]").val();
        var examId = $("input:hidden[name=examId]").val();
        var type = $("input:hidden[name=type]").val();
        var jsonString = "";
        if (lessonId !== undefined && lessonId !== null) {
            jsonString = '"lessonId":' + lessonId + ',';
        } else if (examId !== undefined && examId !== null) {
            jsonString = '"examId":' + examId + ',';
        }
        jsonString += '"type":"' + type + '",';
        for (var i = 0; i < closedTestAnswers.length; i++) {
            jsonString += '"' + closedTestAnswers[i].name + '":' + closedTestAnswers[i].value + ',';
        }

        for (var j = 0; j < openTestAnswers.length; j++) {
            jsonString += '"' + openTestAnswers[j].name + '":"' + openTestAnswers[j].value + '",';
        }

        jsonString = "{" + jsonString.substring(0, jsonString.length - 1) + "}";
        var jsonData = window.jQuery.parseJSON(jsonString);
        $("input[name^=optionsRadios]").attr("disabled", true);
        $("input[type=text][name^=openValue]").attr("disabled", true);

        $('.timerCountDown').remove();

        $.ajax({
            url: '/api/TestApi/CheckTestResults',
            type: 'POST',
            data: jsonData,
            dataType: 'json',
            success: function (data) {
                if (data === undefined || data === null) {
                    $('#modal-lessonTaken').modal('show', { backdrop: 'static' });
                } else {
                    var name = "";
                    var correctAnswerCount = 0;
                    var count = data.results.length;
                    for (var i = 0; i < count; i++) {
                        if (data.results[i].testType === 0) {
                            name = "optionsRadios" + data.results[i].testId;
                        } else {
                            name = "openValue" + data.results[i].testId;
                        }

                        if (data.results[i].isCorrect) {
                            correctAnswerCount++;
                            $("#" + name).css("border-color", "#07610C");

                            if (data.results[i].testType === 0) {
                                $(".answer")
                                    .has("input:checked[name=" + name + "]")
                                    .css({
                                        'color': '#07610C',
                                        'font-weight': 'bold'
                                    });
                            } else {
                                $(".answer")
                                    .has("input[type=text][name=" + name + "]")
                                    .css({
                                        'color': '#07610C',
                                        'font-weight': 'bold'
                                    });
                            }
                        } else {
                            $("#" + name).css("border-color", "#ac1818");

                            if (data.results[i].testType === 0) {
                                $(".answer")
                                    .has("input:checked[name=" + name + "]")
                                    .css({
                                        'color': '#ac1818',
                                        'font-weight': 'bold'
                                    });

                                $(".answer")
                                    .has("input[value=" + data.results[i].answerId + "]")
                                    .css({
                                        'color': '#07610C',
                                        'font-weight': 'bold'
                                    });
                            } else {
                                $(".answer>input[type=text][name=" + name + "]")
                                    .css({
                                        'color': '#ac1818',
                                        'font-weight': 'bold'
                                    });

                                $(".answer")
                                    .has("input[type=text][name=" + name + "]")
                                    .append(
                                        "<span>Doğru cavab: <b style='color:#07610C;'>" +
                                        data.results[i].answerText +
                                        "</b></span>");
                            }
                        }
                    }

                    $("#currentResult").text(count + " ümumi testdən " + correctAnswerCount + " doğru cavab");
                    var percentage = (correctAnswerCount / count) * 100;
                    $(".progress-bar").css("width", percentage.toFixed(1) + "%");

                    if (data.totalScore !== -1) {
                        $("#testScore")
                            .append("<span>Sizin topladığınız ümumi bal: <b>" + data.totalScore + "</b></span>");
                    }
                }
            }
        });
    });

$("input[name^=optionsRadios]")
    .click(progressBar);

$('input[name^=openValue').on('input', progressBar);

function progressBar() {
    var testCount = $(".quiz-wrapper").length;
    var testAnswersCount = $("input:checked[name^=optionsRadios]").length +
        $('input[name^=openValue')
        .filter(function () {
            return !!this.value;
        })
        .length;
    var percentage = (testAnswersCount / testCount) * 100;
    $(".progress-bar").css("width", percentage + "%");
    $("#currentResult").text(+percentage.toFixed(1) + "% tamamlanıb");
}