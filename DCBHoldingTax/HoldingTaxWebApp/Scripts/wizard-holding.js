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
                    } else {
                        KTUtil.scrollTop();
                    }
                    //} else {
                    //	Swal.fire({
                    //		text: "Sorry, looks like there are some errors detected, please try again.",
                    //		icon: "error",
                    //		buttonsStyling: false,
                    //		confirmButtonText: "Ok, got it!",
                    //		customClass: {
                    //			confirmButton: "btn font-weight-bold btn-light"
                    //		}
                    //	}).then(function () {
                    //		KTUtil.scrollTop();
                    //	});
                });
            }

            return false;  // Do not change wizard step, further action will be handled by he validator
        });

        // Change event
        _wizardObj.on('changed', function (wizard) {
            KTUtil.scrollTop();
        });

        // Submit event
        //_wizardObj.on('submit', function (wizard) {
        //    Swal.fire({
        //        text: "All is good! Please confirm the form submission.",
        //        icon: "success",
        //        showCancelButton: true,
        //        buttonsStyling: false,
        //        confirmButtonText: "Yes, submit!",
        //        cancelButtonText: "No, cancel",
        //        customClass: {
        //            confirmButton: "btn font-weight-bold btn-primary",
        //            cancelButton: "btn font-weight-bold btn-default"
        //        }
        //    }).then(function (result) {
        //        if (result.value) {
        //            _formEl.submit(); // Submit form
        //        } else if (result.dismiss === 'cancel') {
        //            Swal.fire({
        //                text: "Your form has not been submitted!.",
        //                icon: "error",
        //                buttonsStyling: false,
        //                confirmButtonText: "Ok, got it!",
        //                customClass: {
        //                    confirmButton: "btn font-weight-bold btn-primary",
        //                }
        //            });
        //        }
        //    });
        //});
    }

    var _initValidation = function () {
        // Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
        // Step 1
        _validations.push(FormValidation.formValidation(
            _formEl,
            {
                fields: {
                    //AreaId: {
                    //    validators: {
                    //        notEmpty: {
                    //            message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                    //        }
                    //    }
                    //},
                    //PlotId: {
                    //    validators: {
                    //        notEmpty: {
                    //            message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                    //        }
                    //    }
                    //},
                    //HolderName: {
                    //    validators: {
                    //        notEmpty: {
                    //            message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                    //        }
                    //    }
                    //},
                    //NID: {
                    //    validators: {
                    //        notEmpty: {
                    //            message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                    //        }
                    //    }
                    //},
                    //Gender: {
                    //    validators: {
                    //        notEmpty: {
                    //            message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                    //        }
                    //    }
                    //},
                    //MaritialStatus: {
                    //    validators: {
                    //        notEmpty: {
                    //            message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                    //        }
                    //    }
                    //},
                    //Gender: {
                    //    validators: {
                    //        notEmpty: {
                    //            message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                    //        }
                    //    }
                    //},
                    //Father: {
                    //    validators: {
                    //        notEmpty: {
                    //            message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                    //        }
                    //    }
                    //},
                    //Mother: {
                    //    validators: {
                    //        notEmpty: {
                    //            message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                    //        }
                    //    }
                    //},
                    //Email: {
                    //    validators: {
                    //        emailAddress: {
                    //            message: 'বৈধ ই-মেইল এড্রেস দিন '
                    //        }
                    //    }
                    //},
                    //Contact1: {
                    //    validators: {
                    //        numeric: {
                    //            message: 'বৈধ মোবাইল নম্বর দিন'
                    //        }
                    //    }
                    //},
                    //Contact2: {
                    //    validators: {
                    //        notEmpty: {
                    //            message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                    //        },
                    //        numeric: {
                    //            message: 'বৈধ নম্বর দিন'
                    //        }
                    //    }
                    //},
                    //PresentAdd: {
                    //    validators: {
                    //        notEmpty: {
                    //            message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                    //        }
                    //    }
                    //},
                    //PermanentAdd: {
                    //    validators: {
                    //        notEmpty: {
                    //            message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                    //        }
                    //    }
                    //},
                    //ContactAdd: {
                    //    validators: {
                    //        notEmpty: {
                    //            message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                    //        }
                    //    }
                    //},
                    //PreviousDueTax: {
                    //    validators: {
                    //        numeric: {
                    //            message: 'বৈধ ভ্যালু দিন'
                    //        }
                    //    }
                    //},
                    //image_file: {
                    //    validators: {
                    //        notEmpty: {
                    //            message: 'পাসপোর্ট সাইজের ছবি আবশ্যক'
                    //        }
                    //    }
                    //}
                },
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

        // Step 2
        _validations.push(FormValidation.formValidation(
            _formEl,
            {
                fields: {
                    //OwnershipSourceId: {
                    //    validators: {
                    //        notEmpty: {
                    //            message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                    //        }
                    //    }
                    //},
                    //OwnerType: {
                    //    validators: {
                    //        notEmpty: {
                    //            message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                    //        }
                    //    }
                    //},
                    //BuildingTypeId: {
                    //    validators: {
                    //        notEmpty: {
                    //            message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                    //        }
                    //    }
                    //},
                    //AmountOfLand: {
                    //    validators: {
                    //        notEmpty: {
                    //            message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                    //        },
                    //        numeric: {
                    //            message: 'বৈধ ভ্যালু দিন'
                    //        }
                    //    }
                    //},
                    //TotalFloor: {
                    //    validators: {
                    //        notEmpty: {
                    //            message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                    //        },
                    //        numeric: {
                    //            message: 'বৈধ ভ্যালু দিন'
                    //        }
                    //    }
                    //},
                    //EachFloorArea: {
                    //    validators: {
                    //        notEmpty: {
                    //            message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                    //        },
                    //        numeric: {
                    //            message: 'বৈধ ভ্যালু দিন'
                    //        }
                    //    }
                    //},
                    //TotalFlat: {
                    //    validators: {
                    //        notEmpty: {
                    //            message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                    //        },
                    //        numeric: {
                    //            message: 'বৈধ ভ্যালু দিন'
                    //        }
                    //    }
                    //},
                    //HoldersFlatNumber: {
                    //    validators: {
                    //        notEmpty: {
                    //            message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                    //        },
                    //        numeric: {
                    //            message: 'বৈধ ভ্যালু দিন'
                    //        }
                    //    }
                    //}
                },
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

        // Step 3
        _validations.push(FormValidation.formValidation(
            _formEl,
            {
                fields: {
                    ccname: {
                        validators: {
                            notEmpty: {
                                message: 'Credit card name is required'
                            }
                        }
                    },
                    ccnumber: {
                        validators: {
                            notEmpty: {
                                message: 'Credit card number is required'
                            },
                            creditCard: {
                                message: 'The credit card number is not valid'
                            }
                        }
                    },
                    ccmonth: {
                        validators: {
                            notEmpty: {
                                message: 'Credit card month is required'
                            }
                        }
                    },
                    ccyear: {
                        validators: {
                            notEmpty: {
                                message: 'Credit card year is required'
                            }
                        }
                    },
                    cccvv: {
                        validators: {
                            notEmpty: {
                                message: 'Credit card CVV is required'
                            },
                            digits: {
                                message: 'The CVV value is not valid. Only numbers is allowed'
                            }
                        }
                    }
                },
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
