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
                text: "নুতুন জমি মালিকের সকল তথ্য সঠিক হলে সাবমিট নিশ্চিত করুন ।",
                icon: "success",
                showCancelButton: true,
                buttonsStyling: false,
                confirmButtonText: "হ্যা, সাবমিট!",
                cancelButtonText: "না, বাতিল",
                customClass: {
                    confirmButton: "btn font-weight-bold btn-primary",
                    cancelButton: "btn font-weight-bold btn-danger"
                }
            }).then(function (result) {
                if (result.value) {
                    //_formEl.submit(); // Submit form
                    var isAllValid = true;
                    var OthetPlotOwnerlist = [];
                    
                    debugger;
                    $('#tableOther tbody tr').each(function (index, ele) {
                        var OthetOwneeName = $('.OldOthetOwneeName', this).val().trim();
                        var Address = $('.Address', this).val().trim();
                        var Remarks = $('.Remarks', this).val().trim();
                        
                            //if(OthetOwneeName.lenth>O){
                            //}
                        var OthetPlotOwner = {
                            OldOthetOwneeName: OthetOwneeName,
                            Address: Address,
                            Remarks: Remarks
                        }
                        OthetPlotOwnerlist.push(OthetPlotOwner);
                    });


                    if (!isAllValid) {
                        Swal.fire({
                            text: "দয়াকরে সকল প্রয়োজনীয় তথ্য দিন ।",
                            icon: "error",
                            buttonsStyling: false,
                            confirmButtonText: "Ok",
                            customClass: {
                                confirmButton: "btn font-weight-bold btn-success",
                            }
                        });
                        $('#details_error').text('দয়াকরে সকল প্রয়োজনীয় তথ্য দিন ।');
                        KTUtil.scrollTop();
                    }
                    else {
                        var data = {
                            
                            OldPlotOwnerId: 0,
                            PlotOwnerId: parseInt($('#PlotOwnerId').val()),
                            OldPlotOwnerName: $('#OldPlotOwnerName').val().trim(),
                            IsAlive: $('#IsAlive').val().trim(),
                            OfficialStatusId: parseInt($('#OfficialStatusId option:selected').val()) || 0,
                            PresentAdd: $('#PresentAdd').val().trim(),
                            PermanentAdd: $('#PermanentAdd').val().trim(),
                            PhoneNumber: $('#PhoneNumber').val().trim(),
                            Email: $('#Email').val().trim(),
                            OldOthetPlotOwner: OthetPlotOwnerlist
                        };


                        $.ajax({
                            type: 'POST',
                            url: '/OldPlotOwner/AddOrUpdate',
                            data: JSON.stringify(data),
                            contentType: 'application/json',
                            success: function (d) {
                                if (d.status === "success") {
                                    
                                    OthetPlotOwnerlist = [];
                                  
                                    $('#tableOther tbody').empty();
                                    swal.fire({
                                        "icon": 'success',
                                        "title": 'নুতুন জমি মালিকের সকল তথ্য সফল ভাবে সাবমিট  করা হয়েছে । ',
                                        "timer": 1000
                                    });
                                    setTimeout(function () { window.location.href = "/PlotOwner/Index"; }, 1000);
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
                        text: "তথ্য সাবমিট  বাতিল করা হল !.",
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
                    OldPlotOwnerName: {
                        validators: {
                            notEmpty: {
                                message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            }
                        }
                    },
                    PlotId: {
                        validators: {
                            notEmpty: {
                                message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            }
                        }
                    },
                     ConsStatusId1: {
                        validators: {
                            notEmpty: {
                                message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            }
                        }
                    },
                      OfficialStatusId1: {
                        validators: {
                            notEmpty: {
                                message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            }
                        }
                    },
                      LeaseQuotaId1: {
                        validators: {
                            notEmpty: {
                                message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            }
                        }
                    },

                 
                 
                
                  
                  
                  
                    PresentAdd: {
                        validators: {
                            notEmpty: {
                                message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            }
                        }
                    },
                    OldPlotOwnerName: {
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
