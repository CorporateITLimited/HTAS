"use strict";

// Class definition
var KTWizard4 = function () {
    // Base elements
    var _wizardEl;
    var _formEl;
    var _wizardObj;
    var _validations = [];

    // Private functions
    var _initWizard = function () {
        // Initialize form wizard
        _wizardObj = new KTWizard(_wizardEl, {
            startStep: 1, // initial active step number
            clickableSteps: true  // allow step clicking
        });

        // Validation before going to next page
        _wizardObj.on('change', function (wizard) {
            if (wizard.getStep() > wizard.getNewStep()) {
                return; // Skip if stepped back
            }

            // Validate form before change wizard step
            var validator = _validations[wizard.getStep() - 1]; // get validator for currnt step

            if (validator) {
                validator.validate().then(function (status) {
                    if (status == 'Valid') {
                        wizard.goTo(wizard.getNewStep());
                        KTUtil.scrollTop();
                    }
                    else {
                        KTUtil.scrollTop();
                    }
                });
            }

            return false;  // Do not change wizard step, further action will be handled by he validator
        });

        // Change event
        _wizardObj.on('changed', function (wizard) {
            KTUtil.scrollTop();
        });

        // Submit event
        _wizardObj.on('submit', function (wizard) {
            Swal.fire({
                text: "All is good! Please confirm the form submission.",
                icon: "success",
                showCancelButton: true,
                buttonsStyling: false,
                confirmButtonText: "Yes, submit!",
                cancelButtonText: "No, cancel",
                customClass: {
                    confirmButton: "btn font-weight-bold btn-primary",
                    cancelButton: "btn font-weight-bold btn-danger"
                }
            }).then(function (result) {
                if (result.value) {
                    //_formEl.submit(); // Submit form
                    var isAllValid = true;
                    var list = [];
                    $('#flat_details tbody tr').each(function (index, ele) {
                        var florNo = parseInt($('.FlorNo option:selected', this).val()) || 0;
                        var flatNo = $('.FlatNo', this).val().trim();
                        var flatArea = parseFloat($('.FlatArea', this).val()) || 0;
                        var ownOrRent = parseInt($('.OwnOrRent option:selected', this).val()) || 0;
                        var monthlyRent = parseFloat($('.MonthlyRent', this).val()) || 0;
                        var selfOwned = parseInt($('.SelfOwned option:selected', this).val()) || 0;
                        var ownerName = $('.OwnerName', this).val().trim();

                        var detailsData = {
                            HolderFlatId: 0,
                            FlorNo: florNo,
                            FlatNo: flatNo,
                            FlatArea: flatArea,
                            OwnOrRent: ownOrRent,
                            SelfOwned: selfOwned,
                            MonthlyRent: monthlyRent,
                            OwnerName: ownerName
                        }
                        list.push(detailsData);
                    });

                    //if (list.length == 0) {
                    //    $('#details_error').text('At least one flat details required.');
                    //    isAllValid = false;
                    //}

                    if (!isAllValid) {
                        Swal.fire({
                            text: "Please fill all the required fields.!",
                            icon: "error",
                            buttonsStyling: false,
                            confirmButtonText: "Ok",
                            customClass: {
                                confirmButton: "btn font-weight-bold btn-success",
                            }
                        });
                        $('#details_error').text('Please fill all the required fields.');
                        KTUtil.scrollTop();
                    }
                    else {
                        var data = {
                            HolderId: 0,
                            AreaId: parseInt($('#AreaId option:selected').val()) || 0,
                            PlotId: parseInt($('#PlotId option:selected').val()) || 0,
                            HolderName: $('#HolderName').val().trim(),
                            NID: $('#NID').val().trim(),
                            Gender: parseInt($('#Gender option:selected').val()) || 0,
                            MaritialStatus: $('#MaritialStatus option:selected').val(),
                            Father: $('#Father').val().trim(),
                            Mother: $('#Mother').val().trim(),
                            Spouse: $('#Spouse').val().trim(),
                            Contact1: $('#Contact1').val().trim(),
                            Contact2: $('#Contact2').val().trim(),
                            Email: $('#Email').val().trim(),
                            PresentAdd: $('#PresentAdd').val().trim(),
                            PermanentAdd: $('#PermanentAdd').val().trim(),
                            ContactAdd: $('#ContactAdd').val().trim(),
                            OwnershipSourceId: parseInt($('#OwnershipSourceId option:selected').val()) || 0,
                            OwnerType: $('#OwnerType option:selected').val(),
                            BuildingTypeId: parseInt($('#BuildingTypeId option:selected').val()) || 0,
                            AmountOfLand: parseFloat($('#AmountOfLand').val()) || 0,
                            TotalFloor: parseFloat($('#TotalFloor').val()) || 0,
                            EachFloorArea: parseFloat($('#EachFloorArea').val()) || 0,
                            TotalFlat: parseInt($('#TotalFlat').val()) || 0,
                            HoldersFlatNumber: parseInt($('#TotalFlat').val()) || 0,
                            PreviousDueTax: parseFloat($('#PreviousDueTax').val()) || 0,
                            HolderFlatList: list,
                            image_file: $("#image_file").get(0).files[0]
                        };


                        $.ajax({
                            type: 'POST',
                            url: '/Holding/AddOrUpdate',
                            data: JSON.stringify(data),
                            contentType: 'application/json',
                            success: function (d) {
                                if (d.status === "success") {
                                    list = [];
                                    $('#flat_details tbody').empty();
                                    swal.fire({
                                        "icon": 'success',
                                        "title": 'Your work has been saved',
                                        "timer": 1500
                                    });
                                    setTimeout(function () { window.location.href = "/PlotOwner/Index"; }, 2000);
                                }
                                else if (d.status === "error") {
                                    alert('Error');
                                } else if (d.status === "no_user") {
                                    alert('Session gone');
                                }
                                else {
                                    alert('error_full');
                                }
                                $('#bill_submit').val('Save');
                            },
                            error: function (error) {
                                console.log(error);
                                $('#bill_submit').val('Save');
                            }
                        });
                    }
                }
                else if (result.dismiss === 'cancel') {
                    Swal.fire({
                        text: "Your form has not been submitted!.",
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "Ok, got it!",
                        customClass: {
                            confirmButton: "btn font-weight-bold btn-success",
                        }
                    });
                }
            });
        });
    }

    var _initValidation = function () {
        // Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
        // Step 1
        _validations.push(FormValidation.formValidation(
            _formEl,
            {
                fields: {
                    LeaseType1: {
                        validators: {
                            notEmpty: {
                                message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            }
                        }
                    },
                    PlotId1: {
                        validators: {
                            notEmpty: {
                                message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            }
                        }
                    },
                   
                 
                 
                
                  
                  
                  
                    PresentAdd1: {
                        validators: {
                            notEmpty: {
                                message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            }
                        }
                    },
                    PermanentAdd1: {
                        validators: {
                            notEmpty: {
                                message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            }
                        }
                    },
                 
                 
                 
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    bootstrap: new FormValidation.plugins.Bootstrap({
                        eleInvalidClass: '',
                        eleValidClass: '',
                    })
                }
            }
        ));

        // Step 2
        _validations.push(FormValidation.formValidation(
            _formEl,
            {
                fields: {
                    OwnershipSourceId: {
                        validators: {
                            notEmpty: {
                                message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            }
                        }
                    },
                    OwnerType: {
                        validators: {
                            notEmpty: {
                                message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            }
                        }
                    },
                    BuildingTypeId: {
                        validators: {
                            notEmpty: {
                                message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            }
                        }
                    },
                    AmountOfLand: {
                        validators: {
                            notEmpty: {
                                message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            },
                            numeric: {
                                message: 'বৈধ ভ্যালু দিন'
                            }
                        }
                    },
                    TotalFloor: {
                        validators: {
                            notEmpty: {
                                message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            },
                            numeric: {
                                message: 'বৈধ ভ্যালু দিন'
                            }
                        }
                    },
                    EachFloorArea: {
                        validators: {
                            notEmpty: {
                                message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            },
                            numeric: {
                                message: 'বৈধ ভ্যালু দিন'
                            }
                        }
                    },
                    TotalFlat: {
                        validators: {
                            notEmpty: {
                                message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            },
                            numeric: {
                                message: 'বৈধ ভ্যালু দিন'
                            }
                        }
                    },
                    HoldersFlatNumber: {
                        validators: {
                            notEmpty: {
                                message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            },
                            numeric: {
                                message: 'বৈধ ভ্যালু দিন'
                            }
                        }
                    }
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    bootstrap: new FormValidation.plugins.Bootstrap({
                        eleInvalidClass: '',
                        eleValidClass: '',
                    })
                }
            }
        ));

        // Step 3
        _validations.push(FormValidation.formValidation(
            _formEl,
            {
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    // Bootstrap Framework Integration
                    bootstrap: new FormValidation.plugins.Bootstrap({
                        //eleInvalidClass: '',
                        eleValidClass: '',
                    })
                }
            }
        ));

        // Step 4
        _validations.push(FormValidation.formValidation(
            _formEl,
            {
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    // Bootstrap Framework Integration
                    bootstrap: new FormValidation.plugins.Bootstrap({
                        //eleInvalidClass: '',
                        eleValidClass: '',
                    })
                }
            }
        ));
    }

    return {
        // public functions
        init: function () {
            _wizardEl = KTUtil.getById('kt_wizard');
            _formEl = KTUtil.getById('kt_form');

            _initWizard();
            _initValidation();
        }
    };
}();


jQuery(document).ready(function () {

    KTWizard4.init();


    $('#PlotId').select2({
        placeholder: "--নির্বাচন করুন--",
        allowClear: true
    });
 
   // $('#MaritialStatus').select2({
      //  placeholder: "--নির্বাচন করুন--",
     //   allowClear: true
   // });
   // autosize($('#PresentAdd'));
   // autosize($('#PermanentAdd'));
   // autosize($('#ContactAdd'));

 


    //$('#OwnershipSourceId').select2({
       // placeholder: "--নির্বাচন করুন--",
       // allowClear: true
    //});
    //$('#OwnerType').select2({
       // placeholder: "--নির্বাচন করুন--",
       // allowClear: true
   // });
  



         $('#StringLeaveDate').datepicker({
                rtl: KTUtil.isRTL(),
                todayBtn: "linked",
                clearBtn: true,
                todayHighlight: true,
                format: "dd/mm/yyyy",
                autoclose: true
            });
         $('#StringCompletionDate').datepicker({
                rtl: KTUtil.isRTL(),
                todayBtn: "linked",
                clearBtn: true,
                todayHighlight: true,
                format: "dd/mm/yyyy",
                autoclose: true
            });
         $('#StringGroundFCDate').datepicker({
                rtl: KTUtil.isRTL(),
                todayBtn: "linked",
                clearBtn: true,
                todayHighlight: true,
                format: "dd/mm/yyyy",
                autoclose: true
            });
         $('#StringFirstFCDate').datepicker({
                rtl: KTUtil.isRTL(),
                todayBtn: "linked",
                clearBtn: true,
                todayHighlight: true,
                format: "dd/mm/yyyy",
                autoclose: true
            });
         $('#StringSccFCDate').datepicker({
                rtl: KTUtil.isRTL(),
                todayBtn: "linked",
                clearBtn: true,
                todayHighlight: true,
                format: "dd/mm/yyyy",
                autoclose: true
            });
         $('#StringThirdFCDate').datepicker({
                rtl: KTUtil.isRTL(),
                todayBtn: "linked",
                clearBtn: true,
                todayHighlight: true,
                format: "dd/mm/yyyy",
                autoclose: true
            });
         $('#StringForthFCDate').datepicker({
                rtl: KTUtil.isRTL(),
                todayBtn: "linked",
                clearBtn: true,
                todayHighlight: true,
                format: "dd/mm/yyyy",
                autoclose: true
            });
         $('#StringFivthFCDate').datepicker({
                rtl: KTUtil.isRTL(),
                todayBtn: "linked",
                clearBtn: true,
                todayHighlight: true,
                format: "dd/mm/yyyy",
                autoclose: true
            });
         $('#StringSixFCDate').datepicker({
                rtl: KTUtil.isRTL(),
                todayBtn: "linked",
                clearBtn: true,
                todayHighlight: true,
                format: "dd/mm/yyyy",
                autoclose: true
            });
         $('#StringOtherFCDate').datepicker({
                rtl: KTUtil.isRTL(),
                todayBtn: "linked",
                clearBtn: true,
                todayHighlight: true,
                format: "dd/mm/yyyy",
                autoclose: true
            });
         $('.StringMEO_NCCDate').datepicker({
                rtl: KTUtil.isRTL(),
                todayBtn: "linked",
                clearBtn: true,
                todayHighlight: true,
                format: "dd/mm/yyyy",
                autoclose: true
            });
         $('.StringApprovalDate').datepicker({
                rtl: KTUtil.isRTL(),
                todayBtn: "linked",
                clearBtn: true,
                todayHighlight: true,
                format: "dd/mm/yyyy",
                autoclose: true
            });
         





  $("#LeaseType").change(function () {
          var otherOwnerTableHead = $('#tableOther thead');
          var otherOwnerTableBody = $('#tableOther tbody');
    //#যৌথ
        var la =$('#LeaseType').val().trim();
        if(la == "যৌথ"){
                 otherOwnerTableHead.append('<th>'+
            
            '<th>অন্য মালিকের নাম</th>' +
            '<th>ঠিকানা</th>' +
            '<th>মন্তব্য</th>' +
            '<th><button class="btn btn-success btn-sm" id="addOthersOwner"><i class="fas fa-plus"></i></button></th></tr>')
            }
            else{ 
            otherOwnerTableHead.empty();
            otherOwnerTableBody.empty();
            }
    });

    $("#addOthersOwner").on('click', function () {
        //e.preventDefault();
        var otherOwnerTableBody = $('#tableOther tbody');
        otherOwnerTableBody.append('<tr>'+
            '<td><input type="text" class="form-control OthetOwneeName" name="OthetOwneeName" /></td>' +
            '<td><input type="text" class="form-control Address" name="Address" /></td>' +
            '<td><input type="text" class="form-control Remarks" name="Remarks" /></td>' +
            '<td><button class="remove btn btn-danger btn-sm" id="remove_old_details"><i class="fas fa-minus"></i></button></td></tr>');
    });


    $('#tableOther tbody').on('click', '.remove', function (e) {
        e.preventDefault();
        $(this).parents('tr').css("background-color", "red").fadeOut(1000, function () {
            $(this).remove();
        });
    });


   $("#addDesign").on('click', function () {
        //e.preventDefault();
        var DesignTableBody = $('#DesignTable tbody');
        DesignTableBody.append('<tr>'+
            '<td><input type="text" class="form-control StringMEO_NCCDate" name="StringMEO_NCCDate" /></td>' +
            '<td><input type="text" class="form-control Reference" name="Reference" /></td>' +
            '<td><input type="text" class="form-control ApprovalNo" name="ApprovalNo" /></td>' +
            '<td><input type="text" class="form-control StringApprovalDate" name="StringApprovalDate" /></td>' +
            '<td><input type="text" class="form-control ApprovalLetterNo" name="ApprovalLetterNo" /></td>' +
            '<td><input type="text" class="form-control FlorNumber" name="FlorNumber" /></td>' +
            '<td><input type="text" class="form-control GroundFlorArea" name="GroundFlorArea" /></td>' +
            '<td><input type="text" class="form-control OtherFlorArea" name="OtherFlorArea" /></td>' +
            '<td><button class="remove btn btn-danger btn-sm" id="remove_old_details"><i class="fas fa-minus"></i></button></td></tr>');
    });

  $('#DesignTable tbody').on('click', '.remove', function (e) {
        e.preventDefault();
        $(this).parents('tr').css("background-color", "red").fadeOut(1000, function () {
            $(this).remove();
        });
    });









});
