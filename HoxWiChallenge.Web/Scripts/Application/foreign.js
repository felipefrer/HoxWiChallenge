
// Calling configControls after documento has ready.
$(document).ready(configControls);

// Bootgrid variable
var grid;

/**
 * Function which configs all page controls.
 */
function configControls() {

    // Adding bootgrid config.
    configBootgrid();

    // Adding toastr config.
    toastrConfig();

    // Adding button behavior.
    newForeignClickHandler();
}

/**
 * Function which configures a DatePicker component.
 */
function configDatePiker() {

    $('.form-control.date').datepicker({
        format: "dd/mm/yyyy",
        autoclose: true,
        forceParse: true
    });
}

/**
 * Function responsible for setting bootgrid config.
 * Formatter: Manipulate the visualization of data cells..
 * Converter: Custom converter.
 */
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
    })

    // Adding specific handle to bootgrid commands
    grid.on("loaded.rs.jquery.bootgrid", function () {
        // Executes after data is loaded and rendered 
            grid.find(".command-edit").on("click", function (e) {
                openEditForeign($(this).data("row-id"));
            }).end().find(".command-delete").on("click", function (e) {
                deleteForeign($(this).data("row-id"));
            });
    });


    // Removing some fields from bootgrid dropdown menu.
    removeColumnsDropdownMenu();

    // Adding new foreign button into bootgrid actionbar.
    addButtonBootGrid();
}

/**
 * Function which add a new button into the first position on bootgrid actionbar dropdown menu.
 */
function addButtonBootGrid() {

    // Using jquery prepend() function to add a new button into the first position of component.
    $(".actions").prepend('<button id="newForeign" type="button" class="btn btn-primary"> ' + 
        '<span class="glyphicon glyphicon-plus"></span></button >');
}

/**
 * Function which remove fields from bootgrid actionbar dropdown menu.
 */
function removeColumnsDropdownMenu() {

    // Removing first element into the dropdown menu.
    $(".pull-right:eq(1) li").first().remove();

    // Removing last element into the dropdown menu.
    $(".pull-right:eq(1) li").last().remove();
}

/**
 * Function responsible for reloading newest data on bootgrid.
 */
function bootGridReload() {

    if (grid != null) {
        grid.bootgrid("reload");
    }
}

/**
 * Function responsible for setting toastr config.
 */
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

/**
 * Function which configure new foreign button click.
 */
function newForeignClickHandler() {

    $("#newForeign").on("click", function (event) {

        event.preventDefault();
        openModel();
    })
}

/**
 * Function responsible for config and opening a modal.
 */
function openModel(foreignId) {

    // Load content into the modal body.
    $(".modal-body").load(foreignFormUrl, function () {

        configDatePiker();

        loadDropdownsContent();

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

        // Disabling button until a server response to avoid double clicks.
        if (btn.attr("disabled") == null) {

            btn.attr("disabled", true);

            $.when(foreignId == null ? createForeign() : editForeign()).then(function () {

                btn.removeAttr("disabled");
            });
        }
    });
}

/**
 * Function responsible for creating a new foreign.
 */
function createForeign() {

    executePost(createForeignUrl, $("form").serialize());
}

/**
 * Function responsible for deleting foreign.
 */
function deleteForeign(hid) {

    $.alertable.confirm("Are you sure about this?").then(function () {

        executePost(deleteForeignUrl, { hidForeign: hid });
    });
}

/**
 * Function responsible for editing a new foreign.
 */
function editForeign(hid) {

    executePost(editForeignUrl, $("form").serialize());
}

/**
 * Function responsible for opening a modal with foreign details to changes
 */
function openEditForeign(id) {

    executePost(getForeignById, { foreignId: id }, false);

    openModel({ foreignId: id })
}

/**
 * Generic function which handles post request to server-side.
 * In addition to handle request it calls toastr function to show the server response.
 */
function executePost(urlPost, dataPost, useToastr = true) {

    $.post(urlPost, dataPost, function (data, status) {

    }).done(function (data) {

        if (useToastr) {

            toastr[data.Success == true ? "success" : "warning"](data.Message != null ? data.Message : data.Error);

            if (data.Success) {

                $(".modal").modal('hide');
                bootGridReload();
            }
        }
        else {

            $("form").autofill(data.Data);
            $('.selectpicker').selectpicker('render');
            $(".form-control.date").val(moment($(".form-control.date").val()).format("DD/MM/YYYY"));
        }
        
        return data;

    }).fail(function (data) {

        toastr["error"](data.Message != null ? data.Message : data.Error);

    })
}

/**
 * Function responsible for loading dropdown content.
 */
function loadDropdownsContent(data) {

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
        });
    });
}