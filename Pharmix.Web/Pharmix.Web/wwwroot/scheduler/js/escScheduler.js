var escScheduler = (function () {
    var defaults = {
        entityId: "0",
        elementId: "",
        jsonFile: "",
        beggingDate: "0",
        numDates: "30",
        width: "auto",
        alignment: "center",
        enableArrows: true,
        colors: {
            overlay: "blue",
            arrows: "black",
            defaultEventLine: "black"
        },
        dateType: "days",
        flashSpaceInterval: "5",
        localization: "en-US",
        differDateWithEvents: true,
        loadMousewheelPlugin: false,
        skipImportEvents: false,
        defaultActiveDate:undefined,
        urlGetEvents: "",
        urlViewEvent: "",
        urlOtherEvent: ""
    };

    var escImportedEvents = [];

    const escDateBoxSize = 43;
    const escArrowBoxSize = 42;

    const escMinWidth = (14 * escDateBoxSize);
    const escArrowsSize = (2 * escArrowBoxSize + 3);
    const escScrollMovement = (3 * escDateBoxSize);
    const escResponsiveWidth = 414;
   
    var getCurrentDate = function () {
        return new Date().toJSON().slice(0, 10);
    }

    var init = function (options) {
        var settings = $.extend(defaults, options);
        
        var scheduler = $("#" + settings.elementId);

        if (scheduler.length && settings.jsonFile.length > 0) {
            if (settings.loadMousewheelPlugin) {
                $.getScript(Framework.Utility.WheelerJsUrl).done(function (script, textStatus) {
                    main(scheduler, settings);
                });
            } else {
                main(scheduler, settings);
            }
        }

        defaults.entityId = settings.entityId;
        defaults.elementId = settings.elementId;
        defaults.jsonFile = settings.jsonFile;
    };

    var main = function (scheduler, settings) {
        var width = calcWidth(settings.width, settings.enableArrows, settings.numDates);

        var regCheck = new RegExp('^\\d{4}-\\d{2}-\\d{2}$');
        var regTest = regCheck.test(settings.beggingDate);

        if (regTest) {
            settings.beggingDate = dateToDayShift(settings.beggingDate);
        }

        createBody(scheduler, width, settings.enableArrows, settings.colors.overlay, settings.colors.arrows, settings.alignment);
        //importEvents(settings.elementId, settings.jsonFile, settings.beggingDate, settings.numDates);
        generateDays(settings.elementId, settings.beggingDate, settings.numDates, settings.dateType, settings.localization, settings.colors.overlay, settings.colors.defaultEventLine, settings.differDateWithEvents, settings.entityId);

        if (settings.dateType == "both") {
            setTimeout(function () {
                flashObjects(scheduler, settings.numDates, "months", settings.localization, settings.flashSpaceInterval);
            }, (settings.flashSpaceInterval * 1000));
        }
    };

    var calcWidth = function (width, arrowsEnabled, numDates) {
        if (width == "auto") {
            return escDateBoxSize * numDates;
        }

        var datesContainerWidth = escDateBoxSize * numDates;
        var schedulerWidth = datesContainerWidth;

        if (arrowsEnabled) {
            schedulerWidth += escArrowsSize;
        }

        var resize = "";
        if (width.indexOf("-") !== -1) {
            var widthArray = width.split("-");
            width = widthArray[0].trim();
            resize = widthArray[1].trim();
        }

        if (width.indexOf("%") !== -1) {
            var percentageWidth = parseInt(width, 10);
            width = (percentageWidth / 100) * screen.width;
        } else {
            width = parseInt(width, 10);
        }

        if (resize == "auto") {
            var calcWidth = width;
            if (arrowsEnabled) {
                calcWidth = width - escArrowsSize;
            }

            var i = 1;
            while ((i * escDateBoxSize) <= calcWidth) {
                width = i * escDateBoxSize;

                i++;
            }

            if (arrowsEnabled) {
                width = width + escArrowsSize;
            }
        }

        if (arrowsEnabled) {
            if (width < (escMinWidth + escArrowsSize)) {
                return escMinWidth + escArrowsSize;
            }
        } else {
            if (width < escMinWidth) {
                return escMinWidth;
            }
        }

        if (schedulerWidth > width) {
            if (arrowsEnabled) {
                return width - escArrowsSize;
            } else {
                return width;
            }
        } else {
            return datesContainerWidth;
        }
    };

    var dateToDayShift = function (date) {
        var todayDate = generateDate(0);
        todayDate = new Date(formateDate(todayDate));
        date = new Date(date);

        return Math.ceil((date - todayDate) / 8.64e7);
    }

    var createBody = function (scheduler, width, arrowsEnabled, color, arrowsColor, alignment) {
        scheduler.addClass("escScheduler");

        if (screen.width <= escResponsiveWidth) {
            scheduler.css("max-width", '100%');
        } else {
            if (arrowsEnabled) {
                var calcWidth = width + escArrowsSize;
                scheduler.css("max-width", calcWidth + "px");
            } else {
                scheduler.css("max-width", width + "px");
            }
        }

        if (alignment == "left") {
            scheduler.css("margin-left", "0");
            scheduler.css("margin-right", "auto");
        } else if (alignment == "right") {
            scheduler.css("margin-left", "auto");
            scheduler.css("margin-right", "0");
        } else {
            scheduler.css("margin-left", "auto");
            scheduler.css("margin-right", "auto");
        }

        var scrollDatesContainer = $(document.createElement('div'));
        scrollDatesContainer.addClass("escScrollDatesContainer");

        if (arrowsEnabled) {
            var arrowBoxPattern = $(document.createElement('div'));
            arrowBoxPattern.addClass("escArrow");
            arrowBoxPattern.addClass("esc" + color + "Pattern");
            //arrowBoxPattern.css("border-radius", "8px 0 0 8px");
            arrowBoxPattern.css("left", "0");

            var arrowBox = $(document.createElement('div'));
            arrowBox.addClass("escArrow");
            arrowBox.addClass("esc" + arrowsColor + "Background");
            arrowBox.css("left", "0");
            //arrowBox.css("border-radius", "8px 0 0 8px");
            arrowBox.on("click", { direction: "left" }, moveDatesContainerArrow);

            var arrowImage = $(document.createElement('div'));
            arrowImage.addClass("escArrowImageLeft");
            arrowBox.append(arrowImage);

            arrowBoxPattern.append(arrowBox);
            scrollDatesContainer.append(arrowBoxPattern);
        }

        var datesContainer = $(document.createElement('div'));
        datesContainer.addClass("escDatesContainer");
        datesContainer.addClass("esc" + color + "Pattern");

        if (arrowsEnabled) {
            var calcMargin = escArrowBoxSize + 1;
            datesContainer.css("margin-left", calcMargin + "px");

            datesContainer.width("calc(100% - 87px)");
        } else {
            datesContainer.width("100%");
        }

        datesContainer.on("mousewheel", moveDatesContainerScroll);
        scrollDatesContainer.append(datesContainer);

        if (arrowsEnabled) {
            var arrowBoxPattern = $(document.createElement('div'));
            arrowBoxPattern.addClass("escArrow");
            arrowBoxPattern.addClass("esc" + color + "Pattern");
            //arrowBoxPattern.css("border-radius", "0 8px 8px 0");
            arrowBoxPattern.css("right", "0");

            var arrowBox = $(document.createElement('div'));
            arrowBox.addClass("escArrow");
            arrowBox.addClass("esc" + arrowsColor + "Background");
            arrowBox.css("right", "0");
            //arrowBox.css("border-radius", "0 8px 8px 0");
            arrowBox.on("click", { direction: "right" }, moveDatesContainerArrow);

            var arrowImage = $(document.createElement('div'));
            arrowImage.addClass("escArrowImageRight");
            arrowBox.append(arrowImage);

            arrowBoxPattern.append(arrowBox);
            scrollDatesContainer.append(arrowBoxPattern);
        }

        var eventsContainerWidth = "";
        if (arrowsEnabled) {
            eventsContainerWidth = width - escDateBoxSize;
        } else {
            eventsContainerWidth = width - (3 * escDateBoxSize);
        }

        var eventsContainer = $(document.createElement('div'));
        eventsContainer.addClass("escEventsContainer");
        if (screen.width <= escResponsiveWidth) {
            eventsContainer.css("max-width", "100%");
        } else {
            eventsContainer.css("max-width", eventsContainerWidth + "px");
        }

        scheduler.append(scrollDatesContainer);
        scheduler.append(eventsContainer);
    };

    var importEvents = function (schedulerId, jsonFile, date, numDates, callback) {
        //var dates = [];
        var events = [];

        //var date = "";
        //var formatedDate = "";
        //for (var i = 0; i < numDates; i++) {
        //    date = generateDate(dateIndex);
        //    formatedDate = formateDate(date);

        //    dates.push(formatedDate);

        //    dateIndex++;
        //}

        //var dateTimestamp = new Date();
        //var timestamp = dateTimestamp.getTime();

        Framework.Spinner.Start();
        $.ajax({
            //url: jsonFile + '?v=' + timestamp,
            url: defaults.urlGetEvents,
            data: { entityId: defaults.entityId, RequestDate: date },
            type: "GET",
            async: false,
            success: function (jsonEvents) {
                Framework.Spinner.Stop();
                   
                $.each(jsonEvents,
	                function (i, item) {
	                    events.push(jsonEvents[i]);
	                });

                var eventObject = new Object();
                eventObject.id = schedulerId;
                eventObject.events = events;

                escImportedEvents = $.grep(escImportedEvents,
	                function (v) {
	                    return v.id != schedulerId;
	                });

                escImportedEvents.push(eventObject);

                if (callback != undefined)
                    callback(events);
            }
        });
    };

    var getEvents = function (schedulerId) {
        var events = [];

        $.each(escImportedEvents, function (i, item) {
            if (schedulerId == item.id) {
                events = item.events;
            }
        });

        return events;
    };

    var generateDays = function (schedulerId, dateIndex, numDates, dateType, localization, color, defaultLineColor, differDatesWithEvents) {
        var scheduler = $("#" + schedulerId);
        var datesContainer = scheduler.find(".escDatesContainer");

        var div = "";
        var spanDate = "";
        var spanText = "";
        var date = "";
        var todayDate = defaults.defaultActiveDate != undefined? defaults.defaultActiveDate : formateDate(generateDate(0));
        var formatedDate = "";
        for (var i = 0; i < numDates; i++) {
            date = generateDate(dateIndex);
            formatedDate = formateDate(date);

            div = $(document.createElement('div'));
            div.addClass("escDateBox");
            div.attr("formatedDate", formatedDate);

            if (checkEventExistance(schedulerId, formatedDate)) {
                div.attr("hasEvent", true);
            } else {
                div.attr("hasEvent", false);

                if (differDatesWithEvents) {
                    if (formatedDate != todayDate) {
                        div.addClass("escDateBoxNoEvent");
                    }
                }
            }

            div.on("click", { date: formatedDate, schedulerId: schedulerId, color: color, defaultLineColor: defaultLineColor, differDatesWithEvents: differDatesWithEvents }, triggerImportEvent);

            spanDate = $(document.createElement('div'));
            spanDate.addClass("escDateBoxDate");
            spanDate.text(date.getDate());

            spanText = $(document.createElement('div'));
            spanText.addClass("escDateBoxText");
            spanText.text(dateLocalization(date, dateType, localization));

            div.append(spanDate);
            div.append(spanText);

            if (formatedDate == todayDate) {
                div.trigger("click");
            }

            datesContainer.append(div);

            dateIndex++;
        }

        if (datesContainer.find(".escDateBoxSelected").length == 0) {
            datesContainer.find(".escDateBox").eq(0).trigger("click");
        }
    };

    var flashObjects = function (scheduler, numDates, dateType, localization, flashSpaceInterval) {
        for (var i = 0; i < numDates; i++) {
            var dateBox = scheduler.find(".escDateBox").eq(i);
            doSetTimeout(dateBox, dateType, localization, (i * 100));
        }

        if (dateType == "days") {
            dateType = "months";
        } else {
            dateType = "days";
        }

        setTimeout(function () { flashObjects(scheduler, numDates, dateType, localization, flashSpaceInterval) }, (flashSpaceInterval * 1000));
    };

    var doSetTimeout = function (dateBox, dateType, localization, timeout) {
        setTimeout(function () {
            changeDateText(dateBox, dateType, localization)
        }, timeout);
    };

    var changeDateText = function (dateBox, dateType, localization) {
        var date = new Date(dateBox.attr("formatedDate"));

        var showText = dateLocalization(date, dateType, localization);

        dateBox.find(".escDateBoxText").fadeOut("slow", function () { dateBox.find(".escDateBoxText").text(showText).fadeIn("slow"); })
    };

    var generateDate = function (dayShift) {
        var timeDifference = new Date().getTime() + 24 * 60 * 60 * 1000 * dayShift;
        var date = new Date(timeDifference);

        return date;
    };

    var formateDate = function (date) {
        var dd = date.getDate();
        var mm = (date.getMonth() + 1);
        var yyyy = date.getFullYear();

        if (mm < 10) { mm = "0" + mm }
        if (dd < 10) { dd = "0" + dd }

        return yyyy + "-" + mm + "-" + dd;
    };

    var dateLocalization = function (date, dateType, localization) {
        var options = "";
        if (dateType == "months") {
            options = { month: "short" };
        } else {
            options = { weekday: "short" };
        }

        return date.toLocaleDateString(localization, options);
    };

    var moveDatesContainerArrow = function (event) {
        var arrow = $(event.target);
        var position = "";

        if (event.data.direction == "left") {
            position = "-=" + escScrollMovement;
        } else if (event.data.direction == "right") {
            position = "+=" + escScrollMovement;
        }

        var datesContainer = arrow.closest(".escScrollDatesContainer").find(".escDatesContainer");
        datesContainer.stop(true, false);
        datesContainer.animate({ scrollLeft: position }, 150, "linear");
    };

    var moveDatesContainerScroll = function (event) {
        var dateBox = $(event.target);
        var position = "";

        if (event.deltaY == 1) {
            position = "+=" + escScrollMovement;
        } else if (event.deltaY == -1) {
            position = "-=" + escScrollMovement;
        }

        var datesContainer = dateBox.closest(".escDatesContainer");

        datesContainer.stop(true, false);
        datesContainer.animate({ scrollLeft: position }, 150, "linear");
    };

    var triggerImportEvent = function (event) {
        var objDate = new Date(event.data.date);
        var daysInMonth = new Date(objDate.getFullYear(), objDate.getMonth() + 1, 0).getDate();

        var callback = function (events) {
            if (events.length > 0) {
                $(event.target).closest(".escDateBox").attr("hasEvent", "true");
            }
            event.reloadEvents = true;
            createEvents(event);
        }

        if (defaults.skipImportEvents) {
            callback([{ DummyEvent:1 }]);
        } else {
            importEvents(event.data.schedulerId, defaults.jsonFile, event.data.date, daysInMonth, callback);
        }
    }

    var createEvents = function (event) {
        var dateBox = $(event.target).closest(".escDateBox");
        var schedulerId = event.data.schedulerId;
        var formatedDate = event.data.date;

        var datesContainer = dateBox.closest(".escDatesContainer");
        datesContainer.find(".escDateBoxSelected").removeClass("escDateBoxSelected");

        var differDatesWithEvents = event.data.differDatesWithEvents;
        if (differDatesWithEvents) {
            var previousSelectedDateBox = datesContainer.find("[hasEvent=false]").not(".escDateBoxNoEvent");
            previousSelectedDateBox.addClass("escDateBoxNoEvent");

            dateBox.removeClass("escDateBoxNoEvent");
        }
        dateBox.addClass("escDateBoxSelected");

        var eventsContainer = $("#" + schedulerId).find(".escEventsContainer");
        eventsContainer.addClass("panel panel-body panel-default");

        if (formatedDate != eventsContainer.attr("date") || event.reloadEvents) {
            var color = event.data.color;
            var lineColor = event.data.defaultLineColor;
            eventsContainer.attr("date", formatedDate);
            eventsContainer.empty();

            if (dateBox.attr("hasEvent") == "false") {
                var divEvent = createEventErrorMessage(color, lineColor);
                eventsContainer.append(divEvent);
                
                return;
            }

            if (defaults.skipImportEvents) {
                importCalendarContent(event, eventsContainer);
            } else {
                //var schedulerId = event.data.schedulerId;
                var events = getEvents(schedulerId);

                $.each(events, function (i, item) {
                    //if (events[i]['date'] == formatedDate) {
                    var divEvent = createEventDiv(events[i], formatedDate);
                    eventsContainer.append(divEvent);
                    //}
                });
            }
        }
    };

    var createEventErrorMessage = function (color, lineColor) {
        var divEvent = $(document.createElement('div'));
        //divEvent.addClass("escEventDataBox");
        divEvent.addClass("esc" + color + "Background");

        var divEventLineContainer = $(document.createElement('div'));
        divEventLineContainer.addClass("escEventsContainerLineContainer");

        var line = $(document.createElement('div'));
        line.addClass("escEventLine");
        line.css("background-color", lineColor);
        divEventLineContainer.append(line);

        divEvent.append(divEventLineContainer);

        var divEventDataContainer = $(document.createElement('div'));
        divEventDataContainer.addClass("escEventsContainerDataContainer");
        divEventDataContainer.css("width", "100%");
        divEventDataContainer.css("height", "70px");
        divEventDataContainer.css("background-color", "#882a68");

        var icon = $(document.createElement('i'));
        icon.addClass("escEventIconWarning");
        divEventDataContainer.append(icon);

        var title = $(document.createElement('div'));
        title.addClass("escEventTitle");
        title.text("No events to show...");
        title.css("margin-top", "23px");
        title.css("margin-left", "60px");
        divEventDataContainer.append(title);

        divEvent.append(divEventDataContainer);


        return divEvent;
    };

    var checkEventExistance = function (schedulerId, formatedDate) {
        var flag = false;

        var events = getEvents(schedulerId);
        $.each(events, function (i, item) {
            if (events[i]['date'] == formatedDate) {
                flag = true;
            }
        });

        if (flag) {
            return true;
        } else {
            return false;
        }
    };

    var createIsolatorEventDiv = function (event, date) {
        var $divParent = $(document.createElement('div'));
        $divParent.addClass("col-lg-4 col-sm-4 col-xs-6");

        var $divPill = $(document.createElement('div'));
        $divPill.addClass("small-box");

        var $hStatus = $(document.createElement('h4'));
        $hStatus.text(event['ShiftTitle'] == null ? "Shift Title" : event['ShiftTitle']);
        var $sUser = $(document.createElement('strong'));
        $sUser.text("N/A");

        var $divInner = $(document.createElement('div'));
        $divInner.addClass("inner");
        $divInner.append($hStatus);
        $divInner.append($sUser);

        var $divIcon = $(document.createElement('div'));
        $divIcon.addClass("icon");
        $divIcon.html('<i class="fa fa-user"></i>');

        var $linkAction = $(document.createElement('a'));
        $linkAction.addClass("btn small-box-footer").attr("href", "javascript:void(0)");

        $divPill.append($divInner);
        $divPill.append($divIcon);
        $divPill.append($linkAction);

        if (event['IsShiftUnavailable']) {
            $linkAction.attr("disabled", true).html('Shift unavailable &nbsp;&nbsp;<i class="fa fa-ban"></i>');
            $divPill.addClass("bg-black").css("cursor", "not-allowed");
        } else {

            if (event['AllocationId'] > 0) {
                $sUser.text(event['StaffName']); $divPill.addClass("bg-green");
                if (event['IsShiftReadonly']) {
                    $linkAction.html('View Allocation &nbsp;&nbsp;<i class="fa fa-mouse-pointer"></i>');
                    $divPill.css({ "opacity": 0.7 });
                } else {
                    $linkAction.html('Edit Allocation &nbsp;&nbsp;<i class="fa fa-edit"></i>');
                }
            } else {
                $divPill.addClass("bg-orange");
                if (event['IsShiftReadonly']) {
                    $linkAction.html('<i class="fa fa-ban"></i>');
                    $divPill.css({ "cursor": "not-allowed", "opacity": 0.7 });
                } else {
                    $linkAction.html('Allocate Now &nbsp;&nbsp;<i class="fa fa-arrow-circle-right"></i>');
                }
            }

            if (!(event['IsShiftReadonly']) || event['AllocationId'] > 0) {
                $linkAction.on("click",
                    function () {
                        var reqData = {
                            isolatorId: defaults.entityId,
                            AllocationId: event['AllocationId'],
                            ShiftId: event['ShiftId'],
                            ShiftDescription: event["ShiftTitle"],
                            StaffId: event['StaffId'],
                            RequestDate: event['Date'],
                            IsReadOnly: event['IsShiftReadonly']
                        };

                        var showAllocation = function (responseHtml) {
                            Framework.Popup.CreateInfoPopup(
                                reqData.AllocationId > 0 ? "Staff Allocation Details" : "Create Staff Allocation",
                                responseHtml, null, null, null, true);
                        };

                        Framework.Utility.ExecuteAjaxRequest(defaults.urlViewEvent, reqData, showAllocation);
                        return false;
                    });
            }
        };

        $divParent.append($divPill);

        return $divParent;
    }

    var commonCount = 1;

    var createOrderEventDiv = function (event, date) {
        var $divParent = $(document.createElement('div'));
        $divParent.addClass("col-lg-6 col-sm-6 col-xs-12");

        var $divPill = $(document.createElement('div'));
        $divPill.addClass("small-box bg-aqua-active");

        var $head = $(document.createElement('h4'));
        var $boday = $(document.createElement('strong'));
        var $divInner = $(document.createElement('div'));
        $divInner.addClass("inner");
        $divInner.append($head);
        $divInner.append($boday);

        var $divIcon = $(document.createElement('div'));
        $divIcon.addClass("icon");
        $divIcon.html('<i class="fa fa-laptop"></i>');

        var $divFooter = $(document.createElement('a'));
        $divFooter.addClass("btn small-box-footer progress")
            .css("height", "35px")
            .attr("href", defaults.urlViewEvent + "?isoid=" + event['IsolatorId'] + "&dt=" + date)
            .attr("title", "Click to select this isolator");
        
        var $pickIcon = $(document.createElement('i'));
        $pickIcon.addClass("fa fa-arrow-circle-right fa-2x")
            .html("&nbsp;Select")
            .css("position", "absolute")
            .css("margin-left", "170px");
        
        $divFooter.append($pickIcon);

        var $progress = $(document.createElement('div'));
        $divFooter.append($progress);
        
        $divPill.append($divInner);
        $divPill.append($divIcon);
        $divPill.append($divFooter);
        $divParent.append($divPill);

        $divPill.data("isoid", event['IsolatorId']);
        $head.text(event['IsolatorName']);
        $boday.append("<div>Doses per session : " + event["SessionDose"] + "</div>");
        $boday.append("<div>Scheduled orders : " + event["ScheduledOrders"] + "</div>");

        //$progress.attr("href", event['ScheduledPercent'] > 0 ? defaults.urlViewEvent + "?isoid=" + event['IsolatorId'] + "&dt=" + date+"" : "javascript:void(0)");
        $progress.attr({
                role: "progressbar",
                "aria-valuenow": event['ScheduledPercent'],
                "aria-valuemin": "0",
                "aria-valuemax": "100"
            })
            .addClass("progress-bar progress-bar-danger")
            .css("width", (event['ScheduledPercent'] > 100 ? 100 : event['ScheduledPercent']) + "%")
            .text(event['ScheduledPercent'] + " %");
        
        //$divPill
        //    .on("dragover", function(e) {
        //        e.preventDefault();
        //    })
        //    .on("drop", function (ev) {
        //        ev.preventDefault();
        //        var orderId = $("#hdnDraggedOrderId").val();
        //        var callback = function () {
        //            var refreshCalendar = function (response) {
        //                $("#divOrderGrid .small-box[data-entityid=" + orderId + "]").remove();
        //                $("#orderScheduler .escDatesContainer .escDateBox[formateddate='" + date + "']").trigger("click");
        //            };

        //            Framework.Utility.ExecuteAjaxRequest(defaults.urlOtherEvent, { orderId: orderId, isoId: event['IsolatorId'], scheduledDate: date }, refreshCalendar);
        //        }

        //        var msg = "You are about to schedule this order in isolator <b>" + event['IsolatorName'] + "</b> on <b>" + date +"</b>. Click YES to proceed.";
        //        Framework.Popup.CreateConfirmationPopup("Order Schedule Confirmation", msg, callback);
        //    });

        //$pickIcon.on("click",
        //    function() {
        //        var showShiftTimeline = function (response) {
        //            //createIsolatorShiftTimeline(response, date, $pickIcon.closest(".escEventsContainer"));
        //            $pickIcon.closest(".escEventsContainer").html(response);
        //        };

        //        Framework.Utility.ExecuteAjaxRequest(defaults.urlViewEvent, { Isoid: event['IsolatorId'], Dt: date }, showShiftTimeline);
        //        return false;
        //    });

        var $wrapper = $("<div></div>");
        $wrapper.append($divParent);
        if (commonCount % 2 === 0) {
            $wrapper.append("<div style='clear:left;'></div>");
        }

        commonCount++;

        return $wrapper;
    }

    var getOrderTimelineContent = function (isoId, date, $eventsContainer) {
        var showShiftTimeline = function(response) {
            $eventsContainer.html(response);
        };
        Framework.Utility.ExecuteAjaxRequest(defaults.urlViewEvent, { Isoid: isoId, Dt: date }, showShiftTimeline);
    }

    var createEventDiv = function (event, date) {
        switch (defaults.elementId) {
        case "isoScheduler":
                return createIsolatorEventDiv(event, date);
            case "orderSchedulerIndex":
                return createOrderEventDiv(event, date);
        default:
        }

        return "No events found";
    }

    var importCalendarContent = function (event, $eventsContainer) {
        switch (event.data.schedulerId) {
            case "orderScheduler":
                return getOrderTimelineContent(defaults.entityId, event.data.date, $eventsContainer);
        default:
        };
        return "No events available";
    }

    return {
        init: init
    }
})();