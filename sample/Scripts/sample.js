var Sample = { Handlers: {}, Services: {}, Page: {}, Layout: {}, Tests: {}, MapsUtility: {}, Utilities: {}, Analytics: {} };

Sample.Ajax = function (inputModel) {
    if (!inputModel.URL) {
        console.log("Sample.Ajax requires a URL");
        return;
    }

    var AjaxSettings = {
        headers: {
            Accept: "application/json; charset=utf-8",
        },
        url: inputModel.URL,
        type: inputModel.Method || "GET",
        data: inputModel.Data,
        success: inputModel.Success || Sample.Handlers.Success,
        error: inputModel.Error || Sample.Handlers.Error,
        cache: false,
        SampleModel: inputModel
    };

    if (inputModel.AjaxSettings) {
        jQuery.extend(true, AjaxSettings, inputModel.AjaxSettings);
    }

    $.ajax(AjaxSettings);
};

Sample.Handlers.Error = function (jqXHR, textStatus, errorThrow) {
    if (Sample.Page.ErrorHandler) {
        Sample.Page.ErrorHandler(jqXHR, textStatus, errorThrow);
    }
    else if (Sample.Layout.ErrorHandler) {
        Sample.Layout.ErrorHandler(jqXHR, textStatus, errorThrow);
    }
    else if (console) {
        console.log("not ok");
        console.log(textStatus);
    }
};

Sample.Handlers.Success = function (data) {
    console.log("all ok");
    console.log(data);
};

Sample.Services.SubmitTerm = function () {
    this.URL = "/api/terms/SaveTerm";
    this.Method = 'POST';
    this.Data = null;
    this.Success = function () {
        Sample.Page.OpenActionModal("accepted");
        $("#actionModal").on("hidden.bs.modal", function (evt) {
            evt.preventDefault();
            window.location = '/Home/Glossary';
        });
    }
    this.Error = function () {
        Sample.Page.OpenActionModal("not processed. Please check console for errors");
    }
}

Sample.Services.DeleteTerm = function () {
    this.URL = "/api/terms/DeleteTerm";
    this.Method = 'POST';
    this.Data = null;
    this.Success = function () {
        Sample.Page.OpenActionModal("deleted");
        $("#actionModal").on("hidden.bs.modal", function (evt) {
            evt.preventDefault();
            window.location = '/Home/Glossary';
        });
    }
    this.Error = function () {
        Sample.Page.OpenActionModal("not processed. Please check console for errors");
    }
}