﻿"use strict";

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
                text: "জমি মালিকের সকল তথ্য সঠিক হলে সাবমিট নিশ্চিত করুন ",
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
                    var DesignApprovallist = [];
                    debugger;
                    $('#tableOther tbody tr').each(function (index, ele) {
                        debugger;
                        var OthetPlotOwnerId = parseInt($('.OthetPlotOwnerId', this).val()) || 0;
                        debugger;
                        var OthetOwneeName = $('.OthetOwneeName', this).val().trim();
                        var Address = $('.Address', this).val().trim();
                        var Remarks = $('.Remarks', this).val().trim();
                        

                        var OthetPlotOwner = {
                            OthetPlotOwnerId: OthetPlotOwnerId,
                            OthetOwneeName: OthetOwneeName,
                            Address: Address,
                            Remarks: Remarks
                        }
                        OthetPlotOwnerlist.push(OthetPlotOwner);
                    });



                    $('#DesignTable tbody tr').each(function (index, ele) {
                        var DesignAppId =  parseInt($('.DesignAppId', this).val()) || 0;
                        var StringMEO_NCCDate = $('.StringMEO_NCCDate', this).val().trim();
                        var Reference = $('.Reference', this).val().trim();
                        var ApprovalNo = parseInt($('.ApprovalNo', this).val().trim());
                        var StringApprovalDate = $('.StringApprovalDate', this).val().trim();
                        var ApprovalLetterNo = $('.ApprovalLetterNo', this).val().trim();
                        var FlorNumber = parseInt($('.FlorNumber', this).val().trim());
                        var GroundFlorArea = parseFloat($('.GroundFlorArea', this).val().trim());
                        var OtherFlorArea = parseFloat($('.OtherFlorArea', this).val().trim());
                        
                        var DesignApproval = {
                            DesignAppId: DesignAppId,
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
                 
                    debugger;
                    if (!isAllValid) {
                        Swal.fire({
                            text: "দয়া করে সকল প্রয়োজনীয় তথ্য দিন!",
                            icon: "error",
                            buttonsStyling: false,
                            confirmButtonText: "হ্যা",
                            customClass: {
                                confirmButton: "btn font-weight-bold btn-success",
                            }
                        });
                        $('#details_error').text('দয়া করে সকল প্রয়োজনীয় তথ্য দিন!');
                        KTUtil.scrollTop();
                    }
                    else {
                        var data = {
                            
                            PlotOwnerId: parseInt($('#PlotOwnerId').val()) || 0,
                          
                            PlotId: parseInt($('#PlotId option:selected').val()) || 0,
                            PlotOwnerName: $('#PlotOwnerName').val().trim(),
                            IsAlive: parseInt($('#IsAlive option:selected').val()) || 0,//$('#IsAlive').val().trim(),
                            OfficialStatusId: parseInt($('#OfficialStatusId option:selected').val()) || 0,
                            PresentAdd: $('#PresentAdd').val().trim(),
                            PermanentAdd: $('#PermanentAdd').val().trim(),
                            PhoneNumber: $('#PhoneNumber').val().trim(),
                            Email: $('#Email').val().trim(),
                            StringLeaveDate: $('#StringLeaveDate').val().trim(),
                            LeaseAuthority: $('#LeaseAuthority').val().trim(),
                            LeaseType: $('#LeaseType').val().trim(),
                            LeasePeriod: parseInt($('#LeasePeriod').val().trim()),
                            LeaseQuotaId: parseInt($('#LeaseQuotaId option:selected').val()) || 0,
                            HandOverOffice: $('#HandOverOffice').val().trim(),
                            HandOverLetterNo: $('#HandOverLetterNo').val().trim(),
                            LandDevelopChange: parseFloat($('#LandDevelopChange').val().trim()),
                            ConsStatusId: parseInt($('#ConsStatusId option:selected').val()) || 0,
                            Doc1: $('#Doc1').val().trim(),
                            Doc2: $('#Doc2').val().trim(),
                            Doc3: $('#Doc3').val().trim(),
                            Doc4: $('#Doc4').val().trim(),
                            Doc5: $('#Doc5').val().trim(),
                            Doc6: $('#Doc6').val().trim(),


                            
                            ConsProgressId: parseInt($('#ConsProgressId').val()) || 0,
                            OwnerDeclaration: $('#OwnerDeclaration option:selected').val().trim(),
                            //OwnerDeclaration: $('#OwnerDeclaration').val().trim(),
                            RealBuilder: $('#RealBuilder').val().trim(),
                            DevelopDeposit: parseFloat($('#DevelopDeposit').val().trim()),
                            FloorNumber: parseInt($('#FloorNumber').val().trim()),
                            StringCompletionDate: $('#StringCompletionDate').val().trim(),
                            StringGroundFCDate: $('#StringGroundFCDate').val().trim(),
                            StringFirstFCDate: $('#StringFirstFCDate').val().trim(),
                            StringSccFCDate: $('#StringSccFCDate').val().trim(),
                            StringThirdFCDate: $('#StringThirdFCDate').val().trim(),
                            StringForthFCDate: $('#StringForthFCDate').val().trim(),
                            StringFivthFCDate: $('#StringFivthFCDate').val().trim(),
                            StringSixFCDate: $('#StringSixFCDate').val().trim(),
                            StringOtherFCDate: $('#StringOtherFCDate').val().trim(),
                            OwnerPortion: $('#OwnerPortion').val().trim(),
                            DeveloperPortion: $('#DeveloperPortion').val().trim(),
                            BuyerPortion: $('#BuyerPortion').val().trim(),
                            SubmittedPortion: $('#SubmittedPortion').val().trim(),




                            UnauthComId: parseInt($('#UnauthComId').val()) || 0,
                            TotalUnauthArea: parseFloat($('#TotalUnauthArea').val()) || 0,
                            FineFreeArea: parseFloat($('#FineFreeArea').val()) || 0,
                            WithFineUnauth: parseFloat($('#WithFineUnauth').val()) || 0,
                            RemovedUnauthArea: parseFloat($('#RemovedUnauthArea').val()) || 0,
                            NonRemovedUnauth: parseFloat($('#NonRemovedUnauth').val()) || 0,
                            FineRate: parseFloat($('#FineRate').val()) || 0,
                            FineAmount: parseFloat($('#FineAmount').val()) || 0,


                            File1: $('#file_1').val().trim(),
                            File2: $('#file_2').val().trim(),
                            File3: $('#file_3').val().trim(),
                            File4: $('#file_4').val().trim(),
                            File5: $('#file_5').val().trim(),
                            File6: $('#file_6').val().trim(),

                            DesignApproval: DesignApprovallist,
                            OthetPlotOwner: OthetPlotOwnerlist
                           
                          
                        };
      debugger;

                        $.ajax({
                            type: 'POST',
                            url: '/PlotOwner/AddOrUpdate',
                            data: JSON.stringify(data),
                            contentType: 'application/json',
                            success: function (d) {
                                if (d.status === "success") {
                                    DesignApprovallist = [];
                                    OthetPlotOwnerlist = [];
                                    $('#DesignTable tbody').empty();
                                    $('#tableOther tbody').empty();
                                    swal.fire({
                                        "icon": 'success',
                                        "title": 'সফল ভাবে হালনাগাদ করা হয়েছে ',
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
                                $('#bill_submit').val('Update');
                            },
                            error: function (error) {
                                console.log(error);
                                $('#bill_submit').val('Update');
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
                    LeaseType: {
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
                   
                    ConsStatusId: {
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
                     LeaseQuotaId: {
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
                    PlotOwnerName: {
                        validators: {
                            notEmpty: {
                                message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            }
                        }
                    }
                 
                 
                 
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    bootstrap: new FormValidation.plugins.Bootstrap({
                        eleInvalidClass: '',
                        eleValidClass: ''
                    })
                }
            }
        ));

        // Step 2
        _validations.push(FormValidation.formValidation(
            _formEl,
            {
                fields: {
                    OwnershipSourceId1: {
                        validators: {
                            notEmpty: {
                                message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
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
