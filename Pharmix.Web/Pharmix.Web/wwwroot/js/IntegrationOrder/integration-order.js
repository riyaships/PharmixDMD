$(document).ready(function () {

    var connection = new signalR.HubConnection("/productionhub");

    connection.on('ReloadRequest', function (sender, message) {
        //Do something here
    });

    connection.start().then(function () {
        console.log("connection start");
    });

    $.fn.serializeIncludeDisabled = function () {
        let disabled = this.find(":input:disabled").removeAttr("disabled");
        let serialized = this.serialize();
        disabled.attr("disabled", "disabled");
        return serialized;
    };

    $("#SearchText").on("input", function () {
        $("#linkSearch").trigger("click");
    });

    $('#modal-content').on('click', '#save-request', function () {
        var newForm = $("#IntegrationOrderCallSupervisorForm");

        $.validator.unobtrusive.parse(newForm);
        newForm.validate();
        var data = $("#IntegrationOrderCallSupervisorForm").serializeIncludeDisabled();

        if (newForm.valid()) {
            var callback = function (data) {
                Framework.Spinner.Stop();
                if (data.IsSuccess) {
                    $("#linkSearch").trigger("click");
                    toastr.success(data.Message);
                    $('#modal-request').modal('hide');
                } else {
                    toastr.warning(data.Message);
                }
            };
            Framework.Spinner.Start();
            Framework.Utility.ExecuteAjaxRequest("/IntegrationOrder/CallSupervisor", data, callback);
        } else {
            console.log("not valid");
        }
    });

    $('#modal-content').on('click', '#save-action', function () {
        var newForm = $("#IntegrationOrderCommentForm");

        $.validator.unobtrusive.parse(newForm);
        newForm.validate();
        var data = $("#IntegrationOrderCommentForm").serialize();

        if (newForm.valid()) {
            var callback = function (data) {
                Framework.Spinner.Stop();
                if (data.IsSuccess) {
                    $("#linkSearch").trigger("click");
                    toastr.success(data.Message);
                    $('#modal-request').modal('hide');
                } else {
                    toastr.warning(data.Message);
                }
            };
            Framework.Spinner.Start();
            Framework.Utility.ExecuteAjaxRequest("/IntegrationOrder/SaveActionDelineClassify", data, callback);
        } else {
            console.log("not valid");
        }
    });

    $("#modal-content").on('click', '#save-change', function () {
        var $form = $(" #IntegrationOrderForm");
        $.validator.unobtrusive.parse($form);
        $form.validate();
        var data = $("#IntegrationOrderForm").serialize();

        if ($form.valid()) {
            var callback = function (data) {
                Framework.Spinner.Stop();
                if (data.IsSuccess) {
                    $("#linkSearch").trigger("click");
                    toastr.success(data.Message);
                    $('#modal-request').modal('hide');
                } else {
                    toastr.warning(data.Message);
                }
            };
            Framework.Spinner.Start();
            Framework.Utility.ExecuteAjaxRequest("/IntegrationOrder/SaveUpdateIntegrationOrder", data, callback);
        } else {
            console.log("not valid");
        }
    });

    $("#modal-content").on('click', '#view-version', function () {
        var orderID = $(this).attr('data-val');

        var callback = function (response) {
            $('#modal-content').html(response);
        };
        Framework.Utility.ExecuteAjaxRequest("/AuditInfo/CompareVersion", { Id: orderID }, callback);
    });

    var PerformApprove = function (id) {
        var aproveRequest = function () {
            var callback = function (data) {
                Framework.Spinner.Stop();
                if (data.IsSuccess) {
                    $("#linkSearch").trigger("click");
                    toastr.success(data.Message);
                    $('#modal-request').modal('hide');
                } else {
                    toastr.warning(data.Message);
                }
            };
            Framework.Spinner.Start();
            Framework.Utility.ExecuteAjaxRequest("/IntegrationOrder/ApproveOrder", { OrderId: id }, callback);
        }

        Framework.Popup.CreateConfirmationPopup("Confrim Approval", "Are you sure want to approve this request?", aproveRequest);
    }

    var PerformDecline = function (id) {
        var callback = function (response) {
            $('#modal-content').html(response);
            $('#modal-request').modal('show');
            var newForm = $(" #IntegrationOrderCommentForm");
            $.validator.unobtrusive.parse(newForm);
        };
        Framework.Utility.ExecuteAjaxRequest("/IntegrationOrder/GetComment", { State: 0, OrderId: id }, callback);
    }

    var ClassifyOrder = function (id) {
        var callback = function (response) {
            $('#modal-content').html(response);
            $('#modal-request').modal('show');
            var newForm = $(" #IntegrationOrderCallSupervisorForm");
            $.validator.unobtrusive.parse(newForm);
        };
        Framework.Utility.ExecuteAjaxRequest("/IntegrationOrder/GetComment", { State: 1, OrderId: id }, callback);
    }

    var CallForSupervisor = function (id) {
        var callback = function (response) {
            $('#modal-content').html(response);
            $('#modal-request').modal('show');
            var newForm = $(" #IntegrationOrderCommentForm");
            $.validator.unobtrusive.parse(newForm);
        };
        Framework.Utility.ExecuteAjaxRequest("/IntegrationOrder/GetCallSUpervisor", { OrderId: id }, callback);
    }

    $("#divSupervisorRequestGrid").on("click", "table tbody a", function () {
        var id = $(this).closest("tr").data("id");
        var $iconAction = $(this).find(".fa");

        if ($iconAction.hasClass("fa-check")) {
            PerformApprove(id);
        } else if ($iconAction.hasClass("fa-times")) {
            PerformDecline(id);
        } else if ($iconAction.hasClass("fa-commenting-o")) {
            //Call For Supervisor
            CallForSupervisor(id);
        } else if ($iconAction.hasClass("fa-object-group")) {
            //Classify order
            ClassifyOrder(id);
        } else if ($iconAction.hasClass("fa-edit")) {
            //Classify order
            showPopup(id);
        } else if ($iconAction.hasClass("compareVersion")) {
            showVersionPopup(id);
        }

        return $(this).attr("href") !== "#";
    });

    var showVersionPopup = function (id) {
        var callback = function (response) {
            $('#modal-version-content').html(response);
            $('#modal-version').modal('show');
        };
        Framework.Utility.ExecuteAjaxRequest("/AuditInfo/VersionCompare", { entityName: 'IntegrationOrder', keyId: id }, callback);
    }

    $("#linkSearch").click(function () {
        var callback = function (response) {
            $("#divSupervisorRequestGrid").html(response);
        };

        var searchText = $("#SearchText").val();
        var pageLength = $('#pageLength').val();

        Framework.Utility.ExecuteAjaxRequest("/IntegrationOrder/Search", {
            Page: 1,
            SearchText: searchText,
            PageSize: pageLength
        }, callback);
        return false;
    });

    var showPopup = function (id) {
        var callback = function (response) {
            $('#modal-content').html(response);
            $('#modal-request').modal('show');
        };

        Framework.Utility.ExecuteAjaxRequest("/IntegrationOrder/Get", { Id: id }, callback);
    }

    $('#modal-request').on('hidden.bs.modal', function () {
        console.log('modal hide');
    })

    $("#btnCreateIntegrationOrder").click(function () {
        showPopup(0);
        return false;
    });

    setTimeout(function () {
        $("#linkSearch").trigger("click");
    }, 200);

});