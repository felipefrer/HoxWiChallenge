$(document).ready(configControls);

function configControls() {

    configBootgrid();

    toastrConfig();

    newClickHandler();
}

function configBootgrid() {

    var grid = $("#gridForeign").bootgrid({
        ajax: true,
        url: getForeignUrl,
        formatters: {
            "actions": function (column, row) {
                return "<button data-row-id=\"" + row.Id + "\" type=\"button\" class=\"btn btn-warning btn-xs command-edit\">" +
                    "<span class=\"glyphicon glyphicon-edit\"></span>" +
                    "</button>" +
                    " " +
                    "<button data-row-id=\"" + row.Id + "\" type=\"button\" class=\"btn btn-danger btn-xs command-delete\">" +
                    "<span class=\"glyphicon glyphicon-remove\"></span>" +
                    "</button>";
            }
        },
        converters: {
            datetime: {
                from: function (value) { return moment(value); },
                to: function (value) { return moment(value).format("L"); }
            }
        }
    }).on("loaded.rs.jquery.bootgrid", function () {
        /* Executes after data is loaded and rendered */
        grid.find(".command-edit").on("click", function (e) {
            openEditForeign($(this).data("row-id"));
        }).end().find(".command-delete").on("click", function (e) {
            deleteForeign($(this).data("row-id"));
        });
    });
}

function toastrConfig() {

    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
}

function newClickHandler() {

    $("#newForeign").on("click", function (event) {

        event.preventDefault();
        openModel()
    })
}

function openModel(foreignData) {

    // Load content into the modal body
    $(".modal-body").load(foreignFormUrl, foreignData, function () {

        // showing modal
        $("#foreignModal").modal({

            show: true,
            keyboard: false,
            backdrop: 'static',
        });

    });

    // Config button into the modal.
    $("#btnModal").on("click", function (event) {

        if (foreignData == null) {

            createForeign();
        }
        else {

            editForeign();
        }
        
    });
}

function createForeign() {

    executePost(createForeignUrl, $("form").serialize());
}

function deleteForeign(hid) {

    $.alertable.confirm("Are you sure about this?").then(function () {

        executePost(deleteForeignUrl, { hidForeign: hid });
    });
}

function editForeign(hid) {

    executePost(editForeignUrl, $("form").serialize())
}


function openEditForeign(hid) {

    openModel({ foreignId: hid })
}

function executePost(urlPost, dataPost) {

    $.post(urlPost, dataPost, function (data, status) {

    }).done(function (data) {

        toastr[data.Success == true ? "success" : "warning"](data.Message);


    }).fail(function (data) {

        toastr["error"](data.Message != null ? data.Message : data.Error);
    })
}
