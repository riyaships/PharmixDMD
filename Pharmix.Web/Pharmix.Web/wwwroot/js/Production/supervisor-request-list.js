$(document).ready(function () {
    
    var connection = new signalR.HubConnection("/productionhub");
    connection.start().then(function () {
        console.log("connection start");
    });
    
    $("#SearchText").on("input", function () {
        $("#linkSearch").trigger("click");
    });

    var PerformApprove = function (id) {
        var aproveRequest = function () {
            var callback = function (data) {
                $("#linkSearch").trigger("click");
                connection.send('SendApprovalNotification', id);
                toastr.success(data.Message);
            };
            Framework.Utility.ExecuteAjaxRequest("/Production/ApproveRequest", { requestId: id }, callback);
        }

        Framework.Popup.CreateConfirmationPopup("Confrim Approval", "Are you sure want to approve this request?", aproveRequest);
    }

    var PerformDecline = function (id) {
        var declineRequest = function () {
            var callback = function (data) {
                $("#linkSearch").trigger("click");
                connection.send('SendApprovalNotification', id);
                toastr.success(data.Message);
            };
            Framework.Utility.ExecuteAjaxRequest("/Production/DeclineRequest", { requestId: id }, callback);
        }

        Framework.Popup.CreateConfirmationPopup("Confrim Approval", "Are you sure want to decline this request?", declineRequest);
    }

    var initializeRequestFormEvents = function () {
        $('#CreateRequestForm').on('click', '#create-request', function () {
            var $form = $(" #CreateRequestForm");
            $.validator.unobtrusive.parse($form);
            $form.validate();
            var data = $("#CreateRequestForm").serialize();

            if ($form.valid()) {
                var callback = function (data) {
                    Framework.Spinner.Stop();
                    if (data.IsSuccess) {
                        connection.send('SendRequest', 'One or more requests have been updated from your request list.');
                        toastr.success(data.Message);
                        $('#CreateRequestForm').closest(".modal").modal('hide');
                    } else {
                        toastr.warning(data.Message);
                    }
                };
                Framework.Spinner.Start();
                Framework.Utility.ExecuteAjaxRequest("/Production/CreateRequest", data, callback);
            } else {
                console.log("not valid");
            }
        });
    }

    var ShowDetailRequest = function (RequestId, isModelEditable) {
        var callback = function (response) {
            Framework.Popup.CreateInfoPopup("Request Details", response, null, null, null, true, isModelEditable? initializeRequestFormEvents:null);
            $('#modal-request').modal('show');
        };
        Framework.Utility.ExecuteAjaxRequest("/Production/GetRequestForm", { RequestId: RequestId, IsModelEditable: isModelEditable }, callback);
    }

    $("#divSupervisorRequestGrid").on("click", "table tbody a", function () {
        var id = $(this).closest("tr").data("id");
        var $iconAction = $(this).find(".fa");
        
        if ($iconAction.hasClass("fa-check")) {
            PerformApprove(id);
        } else if ($iconAction.hasClass("fa-times")) {
            PerformDecline(id);
        } else if ($iconAction.hasClass("fa-search")) {
            ShowDetailRequest(id);
        } else if ($iconAction.hasClass("fa-edit")) {
            ShowDetailRequest(id, true);
        } else if ($iconAction.hasClass("fa-trash")) {
            
        }

        return $(this).attr("href") !== "#";
    });

    $("#linkSearch").click(function () {
        var callback = function (response) {
            $("#divSupervisorRequestGrid").html(response);
        };

        var searchText = $("#SearchText").val();
        var pageLength = $('#pageLength').val();

        Framework.Utility.ExecuteAjaxRequest("/Production/Search", {
            Page: 1,
            SearchText: searchText,
            PageSize: pageLength
        }, callback);
        return false;
    });

    setTimeout(function () {
        $("#linkSearch").trigger("click");
    }, 200);

});