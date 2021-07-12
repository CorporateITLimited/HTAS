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
                    var OthetPlotOwnerlist = [];
                    var DesignApprovallist = [];
                    $('#tableOther tbody tr').each(function (index, ele) {
                        var OthetOwneeName = $('.OthetOwneeName', this).val().trim();
                        var Address = $('.Address', this).val().trim();
                        var Remarks = $('.Remarks', this).val().trim();
                        

                        var OthetPlotOwner = {
                            OthetOwneeName: OthetOwneeName,
                            Address: Address,
                            Remarks: Remarks
                        }
                        OthetPlotOwnerlist.push(OthetPlotOwner);
                    });



                    $('#addDesign tbody tr').each(function (index, ele) {
                        var StringMEO_NCCDate = $('.StringMEO_NCCDate', this).val().trim();
                        var Reference = $('.Reference', this).val().trim();
                        var ApprovalNo = $('.ApprovalNo', this).val().trim();
                        var StringApprovalDate = $('.StringApprovalDate', this).val().trim();
                        var ApprovalLetterNo = $('.ApprovalLetterNo', this).val().trim();
                        var FlorNumber = $('.FlorNumber', this).val().trim();
                        var GroundFlorArea = $('.GroundFlorArea', this).val().trim();
                        var OtherFlorArea = $('.OtherFlorArea', this).val().trim();
                        
                        

                        var DesignApproval = {
                            StringMEO_NCCDate: StringMEO_NCCDate,
                            Reference: Reference,
                            ApprovalNo: ApprovalNo,
                            StringApprovalDate: StringApprovalDate,
                            ApprovalLetterNo: ApprovalLetterNo,
                            FlorNumber: FlorNumber,
                            GroundFlorArea: GroundFlorArea,
                            OtherFlorArea: OtherFlorArea
                           
                        }
                        DesignApprovallist.push(DesignApproval);
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
                           ///HolderFlatList: list,
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









});
