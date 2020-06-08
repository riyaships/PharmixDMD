var Framework = {};

(function ($, framework) {
    framework.UtilityService = function () {
        var formatToTwoDigitValue = function (value) {
            return value < 10 ? "0" + value : value;
        };

        var wireUpDatepicker = function (id, callback) {
            var pickerId = '#' + id;
            $(pickerId).inputmask("dd/mm/yyyy")
                .datetimepicker({
                    timepicker: false,
                    format: 'd/m/Y',
                    onSelectTime: function (t, $input) {
                        if (callback != null) {
                            callback(pickerId);
                        }
                    }
                });
        };

        var wireUpTimePicker = function (id, callback) {
            var pickerId = '#' + id;

            $(pickerId).inputmask("hh:mm")
                .datetimepicker({
                    datepicker: false,
                    format: 'H:i',
                    onSelectTime: function (t, $input) {
                        if (callback != null) {
                            callback(pickerId);
                        }
                    }
                });
        }

        var wireUpDateTimePicker = function (id, callback) {
            var pickerId = '#' + id;

            $(pickerId).inputmask("datetime")
                .datetimepicker({
                    format: 'd/m/Y H:i',
                    onSelectTime: function (t, $input) {
                        if (callback != null) {
                            callback(pickerId);
                        }
                    }
                });

            //$(pickerId).inputmask("datetime");
            //var addonId = pickerId + "Addon";
            //$(addonId).datetimepicker().on('change', function (ev) {
            //    var dateTime = ev.target.value;
            //    var dateTimeArray = dateTime.split(' ');
            //    var dateArray = dateTimeArray[0].split('/');

            //    var date = new Date(dateArray[0], dateArray[1] - 1, dateArray[2]);
            //    var formattedDateTime = formatValue(date.getDate()) + '/' + ("0" + (date.getMonth() + 1)).slice(-2) + '/' + date.getFullYear() + ' ' + dateTimeArray[1];
            //    $(pickerId).val(formattedDateTime);
            //    $(pickerId).trigger("focus");
            //});
        }

        var isValidNumber = function (i) {
            return !isNaN(i - 0) && i !== null && i !== "" && i !== false;
        }

        var isValidEmail = function (input) {
            var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            return regex.test(input);
        }

        var isValidDate = function (dateValue) {
            if (dateValue.indexOf("/") == -1) {
                return new Date(dateValue);
            }

            var dateParts = dateValue.split('/');
            var day = dateParts[0];
            var month = dateParts[1];
            var year = dateParts[2];

            var date = new Date(year, month - 1, day);

            if (date == "Invalid Date") return false;

            return (date.getFullYear() == year && (date.getMonth() + 1) == month && date.getDate() == day);
        };

        var parseValueAsDate = function (dateValue) {
            if (dateValue.indexOf("/") == -1) {
                return new Date(dateValue);
            }

            var dateParts = dateValue.split('/');
            return new Date(dateParts[2], dateParts[1] - 1, dateParts[0]);
        };

        var setPageReadonly = function (formId) {
            if (formId != undefined) {
                $("#" + formId + "").find("input, textarea, checkbox, radio").prop("disabled", true);
            } else {
                $("input, textarea, checkbox, radio").prop("disabled", true);
            }
        }

        var executeAjaxRequest = function (actionUrl, actionData, successCallBack, contentType, spinnerIntervalMs) {
            Framework.Spinner.Start(null, spinnerIntervalMs);
            
            var request = $.ajax({
                url: actionUrl,
                type: "POST",
                cache: false,
                traditional: true,
                data: actionData,
                contentType: contentType != null ? contentType: undefined,
                success: function (data) {
                    Framework.Spinner.Stop();
                    if (successCallBack != null) {
                        setTimeout(function () {
                            successCallBack(data);
                        }, 100);
                    }
                    return data;
                },
                error: function (data) {
                    
                },
                fail: function (data) {
                    window.location.href = "/account/login";
                }
            });
            return request.responseText;
        };

        var initializeEscScheduler = function (elementId, entityId, beggingDate, numDates, jsonFile, urlGetEvents, urlViewEvent, urlOtherEvent, skipImportEvents, defaultActiveDate) {
            $("#" + elementId+"").empty();
            escScheduler.init({
                entityId: entityId,
                elementId: elementId, // id of a div
                jsonFile: jsonFile, // name of a json file
                beggingDate: beggingDate, // integer or a specific date (examples: -3, 0, 3, 2017-03-12)
                numDates: numDates, // positive integer
                width: "1500", // number in pixels or percentage (examples: 120px, 150px, 50%, 70%, auto, 120px - auto, 50% - auto)
                alignment: "center", // 'left', 'center' and 'right'
                enableArrows: true, // true/false
                colors: {
                    overlay: "black", // 'black', 'green', 'blue', 'red', 'cyan', 'pink', 'purple' and 'orange'
                    arrows: "black", // 'black', 'green', 'blue', 'red', 'cyan', 'pink', 'purple' and 'orange'
                    defaultEventLine: "black" // any color in css format (name or hex value)
                },
                dateType: "both", // 'days', 'months' and 'both'
                flashSpaceInterval: "10", // positive integer
                localization: "en-US", // country ISO code
                differDateWithEvents: true, // true/false
                loadMousewheelPlugin: true, // true/false
                skipImportEvents: skipImportEvents,
                defaultActiveDate: defaultActiveDate == null || defaultActiveDate.length == 0 ? undefined : defaultActiveDate,
                urlGetEvents: urlGetEvents,
                urlViewEvent: urlViewEvent,
                urlOtherEvent: urlOtherEvent
            });
        }

        return {
            FormatToTwoDigitValue: function (val) {
                return formatToTwoDigitValue(val);
            },
            WireUpDatepicker: function (id, onchange) {
                return wireUpDatepicker(id, onchange);
            },
            WireUpTimePicker: function (id, callback) {
                return wireUpTimePicker(id, callback);
            },
            WireUpDateTimePicker: function (id) {
                return wireUpDateTimePicker(id);
            },
            IsValidNumber: function (number) {
                return isValidNumber(number);
            },
            IsValidEmail: function (input) {
                return isValidEmail(input);
            },
            IsValidDate: function (date) {
                return isValidDate(date);
            },
            ParseValueAsDate: function (date) {
                return parseValueAsDate(date);
            },
            SetPageReadOnly: function (fromId) {
                return setPageReadonly(fromId);
            },
            ExecuteAjaxRequest: function (actionUrl, actionData, successCallBack, contentType, spinnerIntervalMs) {
                return executeAjaxRequest(actionUrl, actionData, successCallBack, contentType, spinnerIntervalMs);
            },
            InitializeEscScheduler: function (elementId, entityId, beggingDate, numDates, jsonFile, urlGetEvents, urlViewEvent, urlOtherEvent, skipImportEvents, defaultActiveDate) {
                return initializeEscScheduler(elementId, entityId, beggingDate, numDates, jsonFile, urlGetEvents, urlViewEvent, urlOtherEvent, skipImportEvents, defaultActiveDate);
            }
        };
    }

    // Numeric only control handler
    jQuery.fn.ForceNumericOnly =
        function (allowNegative, noDecimalPoint, decimalPlaceLength, textLength, isInputTelphone) {

            return this.each(function () {
                $(this).keydown(function (e) {
                    var key = e.charCode || e.keyCode || 0;
                    // allow backspace, tab, delete, enter, arrows, numbers and keypad numbers ONLY
                    // home, end, period, and numpad decimal
                    if (e.shiftKey && (key == 9 || (isInputTelphone && key == 187 && $(this).val().length < 1))) {
                        return true;
                    }
                    if (e.shiftKey) {
                        return false;
                    }

                    if ((key == 110 || key == 190) && $(this).val().indexOf('.') > -1) {
                        return false;
                    }

                    if (((key >= 96 && key <= 105) || (key >= 48 && key <= 57)) && (textLength != undefined && $(this).val().length >= textLength)) {
                        if (e.target.selectionStart == e.target.selectionEnd)
                            return false;
                    }

                    if (((key >= 96 && key <= 105) || (key >= 48 && key <= 57)) && !noDecimalPoint && decimalPlaceLength != undefined && $(this).val().indexOf('.') > -1) {
                        var inputValue = $(this).val().split(".");
                        if (inputValue[1].length >= decimalPlaceLength && e.target.selectionStart == e.target.selectionEnd) {
                            return false;
                        }
                    }

                    if (!noDecimalPoint && (key == 110 || key == 190) && $(this).val().indexOf('.') < 0 && $(this).val().length < 1) {
                        $(this).val("0.");
                        return false;
                    }

                    return (((key == 109 || key == 189) && allowNegative && $(this).val().length < 1) ||
                        key == 8 ||
                        key == 9 ||
                        key == 13 ||
                        key == 46 ||
                        (!noDecimalPoint && (key == 110 || key == 190) && $(this).val().indexOf('.') < 0) ||
                        (key >= 35 && key <= 40) ||
                        (key >= 48 && key <= 57) ||
                        (key >= 96 && key <= 105) ||
                        (key == 107 && isInputTelphone && $(this).val().length < 1));
                });
            });
        };

    // AlphaNumeric only control handler
    jQuery.fn.ForceAlphaNumericOnly = function (length) {
        return this.each(function () {
            $(this).keydown(function (e) {
                var key = e.charCode || e.keyCode || 0;

                if (key == 8 || key == 46 || key == 37 || key == 20 || key == 39 || key == 144 || key == 9 || key == 35 || key == 36 || key == 45 || (key == 32 && (length == undefined || $(this).val().length < length))) return true;
                if (e.shiftKey && (key == 48 || key == 57)) return true;
                if (length != undefined && $(this).val().length >= length) return false;
                if (e.shiftKey && (key >= 48 && key <= 57)) return false;
                if ((key >= 48 && key <= 57) || ((key >= 65 && key <= 90) || (e.shiftKey && (key >= 65 && key <= 90))) || (key >= 96 && key <= 105)) return true;

                return false;
            });
        });
    }

    framework.Utility = new framework.UtilityService();

    Framework.Utility.WheelerJsUrl = '';

})(jQuery, Framework);

function onSuccess() { }

function SendTestPostx1() {
    var url = '/Customer/SendDemoSms';
    Framework.Utility.ExecuteAjaxRequest(url, null);

}