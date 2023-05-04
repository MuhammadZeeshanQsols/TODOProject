
$(document).ready(function () {

    try {
        $(".drag_drop_grid").sortable({
            update: function (event, ui) {
                //console.log('update: ' + ui.item.index())

                UpdatePosition(ui);
            },
            start: function (event, ui) {
                //console.log('start: ' + ui.item.index())
            }
        });
    } catch (ex) {
        alert("Error making items sortable, due to: " + ex.message);
    }

    //#region Events
    var $ListItem = $('#list').find('li').find('span');
    $ListItem.dblclick(function () {
        try {
            var ItemID = $(this).parent().attr('id');
            var Description = $(this).text().trim();
            $("#txtTaskName").val(Description);
            $("#hfEditID").val(ItemID);
            $("#lnkSave").hide();

            $("#lnkUpdate").show();
            $("#lnkCancelEdit").show();
            $("#txtTaskName").focus();
        } catch (ex) {
            alert("Error selecting task to edit, due to: " + ex.message);
        }
    });
    //#endregion
});

//#region   Custom Methods
function UpdatePosition(ui) {
    try {
        var $ListItem = $('#list').find('li');
        $ListItem.each(function (index) {
            console.log(index + ": " + $(this).text().trim());

            $.ajax({
                type: 'POST',
                url: 'ListMVP.aspx/UpdatePosition',
                data: "{ItemID:" + $(this).context.attributes["id"].value + ", Position:" + index + "}",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    console.log("Position updated: " + index + ": " + $(this).text().trim());
                },
                error: function (response) {
                    var ex = jQuery.parseJSON(response.responseText);
                    alert("Error updating position, due to: " + ex.Message);
                }
            });
        });
    } catch (ex) {
        alert("Error updating position, due to: " + ex.message);
    }
}
//#endregion

//#region Events

//#region Click


$(document).on("click", "#btnCancelDelete", function () {
    try {

        $(this).css('display', 'none');
        $(this.parentElement.children[this.parentElement.children.length - 1]).children(0).css('display', 'none');
        $(this.parentElement.children[3]).css('display', 'block');

        $("#hfEditID").val("");
    } catch (e) {
        alert("Error canceling delete, due to: " + e.message);
    }
});

$(document).on("click", "#btnDelete", function () {
    try {
        var ListItemID = $(this.parentElement).attr("id");
        $('#hfEditID').val(ListItemID);

        var $btnDeleteCurrentRow = $(this);
        var $CurrentRow = $($btnDeleteCurrentRow).parent;
        var $btnEdit = $($btnDeleteCurrentRow).siblings(04)[3];
        var $btnDelete = $($btnDeleteCurrentRow).siblings(04)[4];
        $btnDeleteCurrentRow.css('display', 'none');
        $($btnEdit).css('display', 'block');
        $($btnDelete).children(0).css('display', 'block');
    } catch (ex) {
        alert("Error deleting task, due to: " + ex.message);
    }
});

$(document).on("click", ".cardColor", function () {
    try {
        var ListItemID = $(this.parentElement).attr("id");
        $('#hfEditID').val(ListItemID);
    } catch (ex) {
        alert("Error updating color for task, due to: " + ex.message);
    }
});

//#endregion

//#endregion