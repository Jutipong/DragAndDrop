const _Enum = {
    GuidEmpty: "00000000-0000-0000-0000-000000000000",
    Message: {
        PleaseSelect: "Please Select",
        SaveSuccess: "บันทักข้อมูลสำเร็จ",
        SaveFail: "บันทึกข้อมูลไม่เสำเร็จ"
    },
    Operation: {
        VIEW: { Value: 0, Text: "View" },
        Create: { Value: 1, Text: "Create" },
        Update: { Value: 2, Text: "Update" },
        Delete: { Value: 3, Text: "Delete" },
    },
    Role: {
        ADMIN: "ADMIN",
        MAKER: "MAKER",
        APPROVER: "APPROVER",
        VIEWER: "VIEWER",
        MAKERFO: "MAKERFO"
    }
};

const _Config = {
    DataTable: {
        PageLength: 25,
        ASC: "asc",
        DESC: "desc"
    },
    IntervalTime: {
        _1M: 60000,
        _3M: 180000,
        _5M: 300000,
    },
    AjaxType: {
        Post: "POST",
        Get: "GET",
        Put: "PUT",
        Delete: "DELETE"
    },
    Confirm: {
        Red: "red",
        Blue: "blue",
        Orange: "orange",
        Purple: "purple"
    }
};

const _Filter = {
    DateTime: {
        DDMMYYYY(val) {
            if (!_isNull(val)) {
                return moment(val).format('DD/MM/YYYY');
            } else {
                return " - "
            }
        },
        DDMMYYYYHHmmss(val) {
            if (!_isNull(val)) {
                return moment(val).format('DD/MM/YYYY HH:mm:ss');
            } else {
                return " - "
            }
        },
        HHmmss(val) {
            if (!_isNull(val)) {
                return moment(val).format('HH:mm:ss');
            } else {
                return " - "
            }
        }
    },
    String: {

    },
    Number: {

    }
};


//=====================================
//============   Function  ============
//=====================================
const _genID = (length) => {
    var text = "";
    var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    for (var i = 0; i < length; i++) {
        text += possible.charAt(Math.floor(Math.random() * possible.length));
    }

    return text;
}

const _isNull = (value) => {
    let result = (typeof value === 'undefined' || value === undefined || value === '' || value === "" || value === null) ? true : false;
    return result
}

const _uuid = () => {
    let dt = new Date().getTime();
    let uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        let r = (dt + Math.random() * 16) % 16 | 0;
        dt = Math.floor(dt / 16);
        return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
    return uuid;
}
var toastr;
const _toasterOptions = () => {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "progressBar": true,
        "preventDuplicates": false,
        "positionClass": "toast-top-right",
        "onclick": null,
        "showDuration": "400",
        "hideDuration": "1000",
        "timeOut": "7000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };
};

const _toastrSuccess = (message) => {
    _toasterOptions();
    toastr.success(message);
};

const _toastrError = (message) => {
    _toasterOptions();
    toastr.error(message);
};

const _toastrWarning = (message) => {
    _toasterOptions();
    toastr.warning(message);
}

const _BlockUI = () => {
    $(".BlockUI").fadeIn(100);
}
const _UnBlockUI = () => {
    $(".BlockUI").fadeOut(600);
}

//$type => dark, red, blue, orange, purple
const _AlertConfirm = ($type, $icon, $title, $content, $btntextOk, $btntextCancel, callback) => {

    $icon = _isNull($icon) ? "fa fa-question" : $icon;
    $title = _isNull($title) ? "ใช่" : $title;
    $content = _isNull($content) ? "ดำเนินการต่อ ใช่หรือไม่?" : $content;
    $btntextOk = _isNull($btntextOk) ? "ใช่" : $btntextOk;
    $btntextCancel = _isNull($btntextCancel) ? "ยกเลิก" : $btntextCancel;

    $.confirm({
        type: $type,
        columnClass: 'col-md-3 col-md-offset-9',
        containerFluid: true,
        icon: $icon,
        title: $title,
        content: $content,
        theme: 'modern',
        closeIcon: true,
        animation: 'scale',
        closeAnimation: 'scale',
        animationBounce: 1,
        animationSpeed: 500,
        buttons: {

            Ok: {
                text: $btntextOk,
                btnClass: 'btn-' + $type,
                action: function () {
                    if (callback) {
                        callback(true);
                    }
                }
            },
            cancel: {
                text: $btntextCancel,
                action: function () {
                    if (callback) {
                        callback(false);
                    }
                }
            },
        }
    });
}

const _alertFail = ($title, $content, callback) => {

    $title = _isNull($title) ? "เกิดข้อผิดพลาด" : $title;
    $content = _isNull($content) ? "" : $content;

    $.confirm({
        type: _Config.Confirm.Red,
        columnClass: 'col-md-3 col-md-offset-9',
        containerFluid: true,
        icon: 'fa fa-bug',
        title: $title,
        content: $content,
        theme: 'modern',
        closeIcon: true,
        animation: 'scale',
        closeAnimation: 'scale',
        animationBounce: 1,
        animationSpeed: 500,
        buttons: {
            Ok: {
                text: 'ปิด',
                btnClass: 'btn-red',
                action: function () {
                    if (callback) {
                        callback(true);
                    }
                }
            }
        }
    });
}

const _ajax = ($type, $url, $objs, $isBlockUi, callback) => {
    $isBlockUi = _isNull($isBlockUi) ? true : $isBlockUi;
    $.ajax({
        type: _isNull($type) ? _Config.AjaxType.Post : $type,
        async: true,
        cache: false,
        url: $url,
        dataType: "json",
        //traditional: true,
        //contentType: 'application/json',
        data: $objs,
        success(res) {
            if (callback) {
                callback(res);
            }
        },
        error(xhr, ajaxOptions, thrownError) {
            _alertFail(null, '_ajax on load fail.', () => { })
        },
        beforeSend() {
            if ($isBlockUi) {
                _BlockUI();
            }
        },
        complete() {
            if ($isBlockUi) {
                _UnBlockUI();
            }
        }
    });
}

const _resetForm = ($scope) => {
    $scope[0].reset();

    let readonly = $scope.find('input[type=text][readonly]')
    $.each(readonly, function (i, val) {
        $(this).prop('readonly', false);
    });

    let select2 = $scope.find('select');
    $.each(select2, function (i, val) {
        $(this).parent().find('.select2-selection--single').removeAttr('style');

        $(this).prop('selectedIndex', 0);
        $(this).trigger('change');
    });

    $.each($scope.find('.datePicker'), function (i, e) {
        $(this).datepicker('setStartDate', false);
        $(this).datepicker('setEndDate', false);
    });

    $.each($scope.find('.text-red'), function (i, e) {
        let _id = $(this).attr('id');
        if (_id != undefined) {
            $(this).removeClass('text-red');
        }
    });

    $.each($('.has-error'), function (i, e) {
        $(e).removeClass('has-error');
    });
    $.each($('.help-block'), function (i, e) {
        $(e).removeClass('help-block');
    });
}

const _resetSelector = ($scope) => {

    let input = $scope.find('input');
    $.each(input, function (i, val) {
        $(this).val(null);
    });

    let textarea = $scope.find('textarea');
    $.each(textarea, function (i, val) {
        $(this).val(null);
    });

    let hidden = $scope.find('hidden');
    $.each(hidden, function (i, val) {
        $(this).val(null);
    });

    let select2 = $scope.find('select');
    $.each(select2, function (i, val) {
        $(this).prop('selectedIndex', 0);
        $(this).trigger('change');
    });

    $.each($('.has-error'), function (i, e) {
        $(e).removeClass('has-error');
    });
    $.each($('.help-block'), function (i, e) {
        $(e).removeClass('help-block');
    });
}

const _submitForm = ($scope, callback) => {
    $scope.on("submit", function (e) {
        e.preventDefault();
        if (callback) {
            callback();
        }
    });
}

const _initData = ($scope, $obj) => {
    if (_isNull($obj)) return;

    let $input = $scope.find('input[type=hidden], input[type=text], textarea, select,input[type=number], input[type=checkbox]');
    $.each($input, function (indexInArray, val) {
        let $this = $(this);
        let $name = $(this).attr('name');
        if ($name !== undefined) {
            $name = $name.split('.').pop();
            if ($name == "Operation" || $name == "operation") {
                return true;
            }

            if ($this.is("input") || $this.is("textarea") || $this.is("select")) {

                if ($this.is('.datePicker')) {
                    var val = $obj[$name];
                    if (val) {
                        if (moment(val, moment.ISO_8601).isValid()) {
                            var dateFormat = moment(val).format('DD/MM/YYYY');
                            $this.datepicker("setDate", dateFormat);
                        }
                        else {
                            $this.val(val);
                        }
                    }
                } else {
                    if ($this.is(":checkbox")) {
                        var val = ($obj[$name] == "1" || $obj[$name] == true) ? true : false;
                        $this.prop("checked", val);
                    } else {
                        $(this).val($obj[$name]);
                        if ($this.is("select")) {
                            $(this).trigger('change');
                        }
                    }
                }
            }
        }
    });
}

const _initDatePicker = () => {
    $('.datePicker').datepicker({
        language: "th",
        autoclose: true,
        todayHighlight: true,
        format: 'dd/mm/yyyy'
    });
}

const _initDatepickerStartEnd = ($scopeStart, $scopeEnd) => {
    $scopeStart.change(function () {
        if (!isNull($(this).val())) {
            $scopeEnd.datepicker('setStartDate', $(this).val());
        } else {
            $scopeEnd.datepicker('setEndDate', false);
        }
    });

    $scopeEnd.change(function () {
        if (!isNull($(this).val())) {
            $scopeStart.datepicker('setEndDate', $(this).val());
        } else {
            $scopeStart.datepicker('setEndDate', false);
        }
    });
}

const _initSelect2 = ($scope) => {
    var $input = $scope.find('select');
    $.each($input, function (index, val) {
        if ($(this).find('option:first').val() == "") {
            $(this).select2({
                placeholder: 'All',
                allowClear: true
            });
        } else {
            $(this).select2();
        }
    });
}

const intiHighlightText = ($scope, obj, bgCorlor) => {
    $.each(obj, function ($name) {
        let _nameE = `${$name}E`;
        if (obj[_nameE] == 1) {
            let $this = $scope.find(`[name='${$name}']`);
            if ($this.is("input") || $this.is("textarea")) {
                $this.addClass("text-red");
            }
            if ($this.is("select")) {
                let _id = $scope.find('[id*="select2-' + $name + '-"]').attr('id');

                if (obj[$name] == "" || obj[$name] == null) {

                    if ($scope.find("#" + _id).text() == jqEnum.Message.PleaseSelect && bgCorlor) {
                        $scope.find("#" + _id).closest('.select2-selection--single').css('background-color', '#f6e2e2');
                    } else {
                        $scope.find("#" + _id).find('span').addClass("text-red");
                    }

                } else {

                    if (bgCorlor) {
                        $scope.find("#" + _id).closest('.select2-selection--single').css('background-color', '#f6e2e2');
                    } else {
                        let $this = $scope.find('[id*="select2-' + $name + '-"]');
                        $this.addClass("text-red");
                    }
                }
            }
        }
    });
}

const focusError = ($scope) => {
    $scope.find('.has-error').first().find('input').focus();
}

const _getDatainTable = ($scope) => {
    let objs = new Array();
    $.each($scope.find('tr'), function () {
        let row = new Object();
        $.each($(this).find('input, select'), function (e, x) {
            row[x['name']] = $(this).is(':checkbox') ? $(this).prop('checked') : x['value'];
        });
        if (!jQuery.isEmptyObject(row)) {
            objs.push(row);
        }
    });
    return objs;
}

const _getDatainTable_tr = ($scope) => {
    let row = new Object();
    let $this = $scope.closest('tr').find('input, select').serializeArray();
    $.each($this, function (e, x) {
        row[x['name']] = $(this).is(':checkbox') ? $(this).prop('checked') : x['value'];
    });
    return row;
}

const _handleEventValidate = ($scope) => {
    let flagStatus = false;
    if (isNull($scope.val())) {
        $scope.closest('.custom-validate').addClass('has-error');
        flagStatus = false;
    }
    else {
        $scope.closest('.custom-validate').removeClass('has-error');
        flagStatus = true;
    }
    $scope.change(function () {
        if (isNull($(this).val())) {
            $(this).closest('.custom-validate').addClass('has-error');
            return false;
        }
        else {
            $(this).closest('.custom-validate').removeClass('has-error');
            return true;
        }
    });
    return flagStatus;
}

const _getData_in_Div = ($form) => {
    let objs = new Object();
    $.each($($form).find('input'), function (e, x) {
        objs[x['name']] = x['value'];
    });
    return objs;
}

const _generate_html_dataTable = ($tableID, $option) => {
    let _th = '';
    $.each($option.columns, function (index, item) {
        _th += `<th class='text-center' style="font-weight: bold;">${item.title}</th>`
    });

    return `<table class="table table-bordered table-hover" id="${$tableID.replace(".", "").replace("#", "")}">
        <thead class="bg-primary">
            <tr>
               ${_th}
            </tr>
        </thead>
        <tbody></tbody>
    </table>`;
}

const _dataTable = ($scope, $tableID, $form_searchID, $option, callback) => {


    if ($option.autoRow) {
        $option.columns.unshift({ title: "No", data: null, sClass: "text-center", bSortable: false });
    }

    if ($option.btnEdit) {
        $option.columns.push({
            title: _Enum.Operation.Update.Text,
            data: null,
            sClass: "text-center",
            width: "32px",
            bSortable: false,
            render: function () {
                return '<a class="text-success" data-action=' + _Enum.Operation.Update.Text + ' href="javascript:;"><i class="fa fa-edit"></i></a>';
            }
        });
    }

    if ($option.btnDelete) {
        $option.columns.push({
            title: _Enum.Operation.Delete.Text,
            data: null,
            sClass: "text-center",
            width: "32px",
            bSortable: false,
            render: function () {
                return '<a class="text-danger" data-action=' + _Enum.Operation.Delete.Text + ' href="javascript:;"><i class="fa fa-trash"></i></a>';
            }
        });
    }

    if ($option.btnAction) {
        $option.columns.push({
            title: _Enum.Operation.VIEW.Text,
            data: null,
            sClass: "text-center",
            width: "32px",
            bSortable: false,
            render: function () {
                return '<a class="text-pimary" data-action=' + _Enum.Operation.VIEW.Text + ' href="javascript:;"><i class="fa fa-search"></i></a>';
            }
        });
    }

    $($scope).append(_generate_html_dataTable($tableID, $option));

    return $('#' + $tableID).dataTable({
        lengthChange: true,
        processing: false,
        searching: false,
        pageLength: _Config.DataTable.PageLength,
        serverSide: true,
        responsive: true,
        language: {
            emptyTable: "ไม่พบข้อมูล",
            paginate: {
                previous: 'ก่อนหน้า',
                next: 'ถัดไป'
            },
            lengthMenu: "แสดง _MENU_ แถว",
        },
        ajax: {
            async: true,
            url: $option.url,
            type: "POST",
            data: function (d) {
                if (_.isEmpty($('#' + $form_searchID).serializeArray())) {
                    return d;
                } else {
                    return $('#' + $form_searchID).serializeArray().reduce(function (a, x) {
                        d[x.name] = x.value;
                        return d;
                    }, {});
                }
            },
            beforeSend: function () {
                _BlockUI();
            },
            complete: function () {
                _UnBlockUI();
                if (callback) {
                    callback()
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                _alertFail(null, 'datatable on load fail.', () => { })
            }
        },
        columns: $option.columns,
        order: $option.order,
        fnRowCallback: function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            if (!$option.autoRow) return nRow;
            var info = this.api().page.info();
            $('td:eq(0)', nRow).html(iDisplayIndex + info.start + 1).addClass("text-center");
            return nRow;
        },
        fnDrawCallback: function (oSettings) {
            $('.pagination').addClass("pull-right");
            var info = this.api().page.info();
            var pageInfo = '';
            if (info.recordsTotal > 0) {
                pageInfo = 'หน้า ' + parseFloat((info.page + 1)) + '/' + parseFloat(info.pages) + ' แสดงลำดับที่ ' + parseFloat((info.start + 1)) + ' ถึง ' + parseFloat(((info.start + info.length) <= info.recordsTotal ? info.start + info.length : info.recordsTotal)) + ' จาก ' + parseFloat(info.recordsTotal) + ' รายการ';
            } else {
                pageInfo = 'หน้า ' + 0 + '/' + 0 + ' แสดงลำดับที่ ' + 0 + ' ถึง ' + 0 + ' จาก ' + 0 + ' รายการ';
            }
            $(`#${$tableID}_info`).text(pageInfo);
            $('#titleMessage').text('ทั้งหมด ' + parseFloat(info.recordsTotal) + " รายการ");
            $(this).attr("style", "width:100%");
        },

    });
} 
