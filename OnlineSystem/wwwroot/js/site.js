
function AjaxFunction(type, url, data) {
    return $.ajax({
        url: url,
        contentType: 'application/json',
        type: type,
        dataType: 'json',
        data: data
    });
}