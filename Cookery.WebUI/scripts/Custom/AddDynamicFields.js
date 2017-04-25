//Скрипт динамически создаёт формы для ввода описания шагов процесса приготовления


$(document).ready(function () {

    //Записываем в пременную разметку пустой формы вводы описания шага инструкции приготовления
    var stepDescription = '<div class="stepDescription">'
            + '<label>Какой-то шаг</label>'
            + '<textarea name="instrStepDesc" class="form-control" rows="2" cols="20"></textarea>'
            + '<div style="position: relative;">'
            + '<a class="btn" href="javascript:;">'
            + '+ Пошаговое фото'
            + '<input name="InsrStepPhotos" style="position: absolute; z-index: 2; top: 0; left: 0; filter: alpha(opacity=0)'
            + '; opacity: 0; background-color: transparent; color: transparent;" type="file" size="40">'
            + '</a></div><br></div>';

    //При загрузке страницы делаем проверку: 
    //если не существует ни одного описания шага инструкции приготовления
    //то создаём пустую форму для ввода первого шага
    if (!$(".stepDescription").length) {
    $($.parseHTML(stepDescription)).appendTo(".instruction");
    }

    //При потере фокуса на любой форме ввода текста кроме последеней удаляем эту форму
    //при пустом поле ввода
    $(document).on("focusout", $(".stepDescription"), function () {
        $(".stepDescription").each(function (index) {
            if (!$.trim(($(this).find("textarea").val())) && (index + 1) < $(".stepDescription").length)
                $(this).remove();
        });
    });

    //При нажатии клавиши проверяем, есть ли описание шага инструкции приготовления в последней форме ввода,
    //и, если есть, то создаём новую пустую форму для ввода следующего шага
    $(document).on("keyup", $(".stepDescription").last(), function () {
        if ($(".stepDescription").last().find("textarea").val().length > 0) {
            
            $($.parseHTML(stepDescription)).insertAfter($(".stepDescription").last());
        };
    });

    //Перед сохранением изменений о рецепте удаляем пустое поле
    $("input[type='submit']").on('click', function () {
        if (!$.trim(($(".stepDescription").last().find("textarea").val())))
            $(".stepDescription").last().remove();
    });
});

