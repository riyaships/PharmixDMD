(function ($, framework) {
    framework.Spin = function () {
        //default setting for spin.js spinner
        var opts = {
            overlay: true,
            base: 0.9,
            circles: 3,
            width: 150,
            top: null,
            left: null,
            hide: false,
            destroy: false
        };

        var timer;
        //function to show the spinner
        var showSpinner = function (targetId, intervalMs) {
            var targetSpin = "fw-spinner";
            if (targetId != null) {
                targetSpin = targetId;
                $("#" + targetId).addClass("fw-spinnerOverlay");
                $("#" + targetId).css("position", "inherit");
            }
            timer = setTimeout(function () { $('#' + targetSpin).show().loading(opts); }, (intervalMs == undefined ? 0 : intervalMs));

            //$('#' + targetSpin).show().loading(opts);
        };

        //function to hide the spinner
        var hideSpinner = function () {
            opts.destroy = true;

            $('#fw-spinner').loading({destroy:true}).hide();
            $(".fw-spinnerOverlay").not($("#fw-spinner .fw-spinnerOverlay")).removeClass('fw-spinnerOverlay');
            clearTimeout(timer);
        };

        var showLoadingMessage = function (message, noAutoHide) {
            var $popup = $("#popup-message-box");

            message = message != null ? message : "Please wait.. your request being processed.";

            if ($popup.length > 0) {
                $popup.html(message);
                $popup.show();
            } else {
                $('body').append($("<div id='popup-message-box' class='col-md-4 col-md-offset-4 popup-message'>" + message + "</div>"));
            }

            if (!noAutoHide) {
                $("#popup-message-box").delay(2000).fadeOut("slow");
            }
        }

        var hideLoadingMessage = function () {
            $("#popup-message-box").hide();
        };
        
        return {
            Start: function (targetId, intervalMs) {
                showSpinner(targetId, intervalMs);
            },
            Stop: function () {
                hideSpinner(true);
            },
            ShowLoadingMessage: function (message, noAutoHide) {
                showLoadingMessage(message, noAutoHide);
            },
            HideLoadingMessage: function () {
                hideLoadingMessage();
            }
        };
    };
    
    framework.Spinner = new framework.Spin();
    Framework.Spinner = new framework.Spin();

})(jQuery, Framework)