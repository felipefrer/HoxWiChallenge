$(document).ready(configControls);

var grid;

function configControls() {

    configBootgrid();

    toastrConfig();

    newClickHandler();
}

function configDatePiker() {

    $('.form-control.date').datepicker({
        format: "dd/mm/yyyy"
    });
}

function configBootgrid() {

    grid = $("#gridForeign").bootgrid({
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
            },
            "flag": function (column, row) {
                return "<span class='flag-icon flag-icon-" + row.Nationality.toLowerCase() + "'></span>";
            }
        },
        converters: {
            datetime: {
                from: function (value) { return moment(value); },
                to: function (value) { return moment(value).format("DD/MM/YYYY"); }
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

function bootGridReload() {

    if (grid != null) {
        grid.bootgrid("reload");
    }
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

        configDatePiker();

        loadDropdownsContent();

        $('.selectpicker').val("BR");

        // showing modal
        $("#foreignModal").modal({

            show: true,
            keyboard: false,
            backdrop: 'static',
        });
    });

    // Config button into the modal.
    $("#btnModal").off().on("click", function (event) {

        var btn = $(this);

        if (btn.attr("disabled") == null) {

            btn.attr("disabled", true);

            $.when(foreignData == null ? createForeign() : editForeign()).then(function () {

                btn.removeAttr("disabled");
            });
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

    executePost(editForeignUrl, $("form").serialize());
}

function openEditForeign(id) {

    openModel({ foreignId: id })
}

function executePost(urlPost, dataPost) {

    $.post(urlPost, dataPost, function (data, status) {

    }).done(function (data) {

        toastr[data.Success == true ? "success" : "warning"](data.Message != null ? data.Message : data.Error);

        if (data.Success) {

            $(".modal").modal('hide');
            bootGridReload();
        }


    }).fail(function (data) {

        toastr["error"](data.Message != null ? data.Message : data.Error);

    })
}

function loadDropdownsContent() {

    var objSource = [{ "key": "nationality", "url": "../fonts/countries.json" }, { "key": "visa", "url": "../fonts/visa.json" }]

    objSource.forEach(function (source) {

        var select = source.key == "visa" ? $("#visa") : $("#nationality")

        $.getJSON(source.url, function (json) {

            $.each(json, function (index, item) {

                var content = "";

                if (source.key == "visa") {

                    select.append($('<option value=' + item.code + '>' + item.description + '</option>')
                        .attr("title", item.name));
                }
                else {

                    select.append($('<option value=' + item.code + '>' + item.name + '</option>')
                        .attr("data-icon", 'flag-icon flag-icon-' + item.code.toLowerCase()));
                }
            });

            select.selectpicker();

            $('.selectpicker').selectpicker('render');

            //$(".bs-title-option").remove();
        });
    });
}