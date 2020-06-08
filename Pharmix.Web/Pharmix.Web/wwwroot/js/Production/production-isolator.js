 $(document).ready(function () {
    var connection = new signalR.HubConnection("/productionhub");
     connection.start().then(function () {
         console.log("connection start");
     });
   
     var initializeRequestFormEvents = function() {
         $('#CreateRequestForm').on('click', '#create-request', function () {
             var $form = $(" #CreateRequestForm");
             $.validator.unobtrusive.parse($form);
             $form.validate();
             var data = $("#CreateRequestForm").serialize();

             if ($form.valid()) {
                 var callback = function (data) {
                     Framework.Spinner.Stop();
                     if (data.IsSuccess) {
                         connection.send('SendRequest', 'New request has received.');
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

    var showModal = function (requestId) {
        var callback = function (response) {
            Framework.Popup.CreateInfoPopup("Create Request", response, null, null, null, true, initializeRequestFormEvents);
            var newForm = $(" #CreateRequestForm");
            $.validator.unobtrusive.parse(newForm);
        };

        var isoId = $("#contProducionOrders #IsolatorId").val();
        Framework.Utility.ExecuteAjaxRequest("/Production/GetRequestForm", { RequestId: requestId, IsoId: isoId, IsModelEditable:true }, callback);
    };

     $('#btnCreateRequest').on('click', function () {
        showModal(0);
    });
});