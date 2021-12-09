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
                text: "নূতন প্লট/ফ্ল্যাট/বাড়ী মালিকের তথ্য সাবমিশন নিশ্চিত করুন",
                icon: "success",
                showCancelButton: true,
                buttonsStyling: false,
                confirmButtonText: "হ্যা, সাবমিট",
                cancelButtonText: "না, বাতিল",
                customClass: {
                    confirmButton: "btn font-weight-bold btn-primary",
                    cancelButton: "btn font-weight-bold btn-danger"
                }
            }).then(function (result) {
                if (result.value) {
                    //_formEl.submit(); // Submit form
                    var isAllValid = true;
                    var list = [];
                    //if (area_type_id === 2) {
                    //    $('#flat_details tbody tr').each(function (index, ele) {
                    //        var monthlyRent = parseFloat($('.MonthlyRent', this).val()) || 0;
                    //        if (monthlyRent === 0) {
                    //            Swal.fire({
                    //                text: "মাসিক ভাড়া/সম্ভাব্য মাসিক ভাড়া ঘরটি অবশই পূরণ করুন ",
                    //                icon: "error",
                    //                buttonsStyling: false,
                    //                confirmButtonText: "  ERROR  ",
                    //                customClass: {
                    //                    confirmButton: "btn font-weight-bold btn-danger",
                    //                }
                    //            });
                    //            $('#details_error').text("মাসিক ভাড়া/সম্ভাব্য মাসিক ভাড়া ঘরটি অবশই পূরণ করুন");
                    //            return;
                    //        }
                    //    });
                    //}

                    debugger;
                    var isHolderOwner = $('#IsHolderAnOwner').val();
                    if (isHolderOwner === 'true') {
                        $('#flat_details tbody tr').each(function (index, ele) {
                            var florNo = parseInt($('.FlorNo option:selected', this).val()) || 0;
                            var flatNo = $('.FlatNo', this).val().trim();
                            var flatArea = parseFloat($('.FlatArea', this).val()) || 0;
                            var ownOrRent = parseInt($('.OwnOrRent option:selected', this).val()) || 0;
                            var monthlyRent = parseFloat($('.MonthlyRent', this).val()) || 0;
                            var selfOwned = 1;//parseInt($('.SelfOwned option:selected', this).val()) || 0;
                            var ownerName = '';//$('.OwnerName', this).val().trim();
                            var isCheckedByHolder = $('.IsCheckedByHolder', this).val();
                            var remarks = $('.nRemarks', this).val().trim();
                            if (remarks == null) {
                                alert("null");
                                return;
                            }
                            var detailsData = {
                                HolderFlatId: 0,
                                FlorNo: florNo,
                                FlatNo: flatNo,
                                FlatArea: flatArea,
                                OwnOrRent: ownOrRent,
                                SelfOwn: selfOwned,
                                MonthlyRent: monthlyRent,
                                OwnerName: ownerName,
                                IsCheckedByHolder: isCheckedByHolder,
                                Remarks: remarks
                            }
                            list.push(detailsData);
                        });
                    } else {
                        $('#flat_details tbody tr').each(function (index, ele) {
                            var holderFlatId = parseInt($('.HolderFlatId', this).val()) || 0;
                            var florNo = parseInt($('.FlorNo', this).val()) || 0;
                            var flatNo = $('.FlatNo', this).html();
                            var flatArea = parseFloat($('.FlatArea', this).html()) || 0;
                            var ownOrRent = parseInt($('.OwnOrRent option:selected', this).val()) || 0;
                            var monthlyRent = parseFloat($('.MonthlyRent', this).val()) || 0;
                            var selfOwned = 1; //parseInt($('.SelfOwned option:selected', this).val()) || 0;
                            var ownerName = '';//$('.OwnerName', this).val().trim();
                            var isCheckedByHolder = $('.IsCheckedByHolder', this).val();
                            var remarks = $('.nRemarks', this).val();

                            var detailsData = {
                                HolderFlatId: holderFlatId,
                                FlorNo: florNo,
                                FlatNo: flatNo,
                                FlatArea: flatArea,
                                OwnOrRent: ownOrRent,
                                SelfOwn: selfOwned,
                                MonthlyRent: monthlyRent,
                                OwnerName: ownerName,
                                IsCheckedByHolder: isCheckedByHolder,
                                Remarks: remarks
                            }
                            list.push(detailsData);
                        });
                    }

                    //if (list.length == 0) {
                    //    $('#details_error').text('At least one flat details required.');
                    //    isAllValid = false;
                    //}

                    if (!isAllValid) {
                        Swal.fire({
                            text: "অত্যাবশ্যকীয় ঘর গুলো পুরোন করুন",
                            icon: "error",
                            buttonsStyling: false,
                            confirmButtonText: "  হ্যা  ",
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
                            HoldersFlatNumber: parseInt($('#HoldersFlatNumber').val()) || 0,
                            PreviousDueTax: parseFloat($('#PreviousDueTax').val()) || 0,
                            HolderFlatList: list,
                            AllocationLetterNo: $('#AllocationLetterNo').val().trim(),
                            StringAllocationDate: $('#StringAllocationDate').val().trim(),
                            NamjariLetterNo: $('#NamjariLetterNo').val().trim(),
                            StringNamjariDate: $('#StringNamjariDate').val().trim(),
                            StringRecordCorrectionDate: $('#StringRecordCorrectionDate').val().trim(),
                            IsHolderAnOwner: $("#IsHolderAnOwner").val()
                        };


                        $.ajax({
                            type: 'POST',
                            url: '/Holding/AddData',
                            data: JSON.stringify(data),
                            contentType: 'application/json',
                            success: function (d) {
                                if (d.status === "success") {
                                    list = [];
                                    $('#flat_details tbody').empty();
                                    Swal.fire({
                                        text: "নূতন প্লট/ফ্ল্যাট/বাড়ী মালিকের তথ্য সফলভাবে সংযুক্ত করা হয়েছে",
                                        icon: "success",
                                        buttonsStyling: false,
                                        confirmButtonText: "সফলভাবে সম্পন্ন",
                                        customClass: {
                                            confirmButton: "btn font-weight-bold btn-success",
                                        }
                                    });
                                    setTimeout(function () { window.location.href = "/Holding/Index"; }, 2000);
                                }
                                else if (d.status !== "success") {
                                    Swal.fire({
                                        text: d.status,
                                        icon: "error",
                                        buttonsStyling: false,
                                        confirmButtonText: "  ERROR  ",
                                        customClass: {
                                            confirmButton: "btn font-weight-bold btn-danger",
                                        }
                                    });
                                    $('#details_error').text(d.status);
                                    //KTUtil.scrollTop();
                                }
                                else {
                                    Swal.fire({
                                        text: "Unknow Error",
                                        icon: "error",
                                        buttonsStyling: false,
                                        confirmButtonText: "  ERROR  ",
                                        customClass: {
                                            confirmButton: "btn font-weight-bold btn-danger",
                                        }
                                    });
                                    $('#details_error').text(d.status);
                                }
                            },
                            error: function (error) {
                                console.log(error);
                            }
                        });
                    }
                }
                else if (result.dismiss === 'cancel') {
                    Swal.fire({
                        text: "নূতন প্লট/ফ্ল্যাট/বাড়ী মালিকের তথ্যের সাবমিশন বাতিল করা হয়েছে",
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "  সম্পন্ন  ",
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
                    AreaId: {
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
                    HolderName: {
                        validators: {
                            notEmpty: {
                                message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            }
                        }
                    },
                    NID: {
                        validators: {
                            notEmpty: {
                                message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            }
                        }
                    },
                    Gender: {
                        validators: {
                            notEmpty: {
                                message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            }
                        }
                    },
                    MaritialStatus: {
                        validators: {
                            notEmpty: {
                                message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            }
                        }
                    },
                    Father: {
                        validators: {
                            //notEmpty: {
                            //    message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            //}
                        }
                    },
                    Mother: {
                        validators: {
                            //notEmpty: {
                            //    message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            //}
                        }
                    },
                    Email: {
                        validators: {
                            emailAddress: {
                                message: 'ইংরেজিতে ই-মেইল এড্রেস দিন '
                            }
                        }
                    },
                    Contact2: {
                        validators: {
                            notEmpty: {
                                message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            },
                            numeric: {
                                message: 'মোবাইল নম্বর ইংরেজিতে দিন'
                            },
                            stringLength: {
                                min: 11,
                                max: 11,
                                message: '১১ সংখ্যার মোবাইল নম্বর দিন'
                            }
                        }
                    },
                    Contact1: {
                        validators: {
                            //notEmpty: {
                            //    message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            //},
                            numeric: {
                                message: 'ল্যান্ডলাইন নম্বর ইংরেজিতে দিন'
                            }
                        }
                    },
                    PresentAdd: {
                        validators: {
                            //notEmpty: {
                            //    message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            //}
                        }
                    },
                    PermanentAdd: {
                        validators: {
                            //notEmpty: {
                            //    message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            //}
                        }
                    },
                    ContactAdd: {
                        validators: {
                            //notEmpty: {
                            //    message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            //}
                        }
                    },
                    PreviousDueTax: {
                        validators: {
                            numeric: {
                                message: 'আগের অর্থ বছর পর্যন্ত বকেয়া ইংরেজিতে দিন'
                            }
                        }
                    },
                    image_file: {
                        validators: {
                            //notEmpty: {
                            //    message: 'পাসপোর্ট সাইজের ছবি আবশ্যক'
                            //}
                        }
                    },
                    IsHolderAnOwner: {
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
                            //notEmpty: {
                            //    message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            //},
                            numeric: {
                                message: 'জমির পরিমাণ ইংরেজিতে দিন'
                            }
                        }
                    },
                    TotalFloor: {
                        validators: {
                            //notEmpty: {
                            //    message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            //},
                            numeric: {
                                message: 'মোট তলার সংখ্যা ইংরেজিতে দিন'
                            }
                        }
                    },
                    EachFloorArea: {
                        validators: {
                            //notEmpty: {
                            //    message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            //},
                            numeric: {
                                message: 'প্রতিতলার আয়তন ইংরেজিতে দিন'
                            }
                        }
                    },
                    TotalFlat: {
                        validators: {
                            //notEmpty: {
                            //    message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            //},
                            numeric: {
                                message: 'মোট ফ্ল্যাট সংখ্যা ইংরেজিতে দিন'
                            }
                        }
                    },
                    HoldersFlatNumber: {
                        validators: {
                            //notEmpty: {
                            //    message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            //},
                            numeric: {
                                message: 'নিজ মালিকানাধীন ফ্ল্যাট সংখ্যা ইংরেজিতে দিন'
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
                fields: {
                    AllocationLetterNo: {
                        validators: {
                            //notEmpty: {
                            //    message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            //}
                        }
                    },
                    StringAllocationDate: {
                        validators: {
                            //notEmpty: {
                            //    message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            //}
                        }
                    },
                    NamjariLetterNo: {
                        validators: {
                            //notEmpty: {
                            //    message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            //}
                        }
                    },
                    StringNamjariDate: {
                        validators: {
                            //notEmpty: {
                            //    message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            //},
                        }
                    },
                    StringRecordCorrectionDate: {
                        validators: {
                            //notEmpty: {
                            //    message: 'ঘরটি অবশ্যই পূরণ করতে হবে'
                            //},
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

    //debugger;
    //var data = new FormData();
    //data.append("HolderId", 0);
    //data.append("AreaId", parseInt($('#AreaId option:selected').val()) || 0);
    //data.append("PlotId", parseInt($('#PlotId option:selected').val()) || 0);
    //data.append("HolderName", $('#HolderName').val().trim());
    //data.append("NID", $('#NID').val().trim());
    //data.append("Gender", parseInt($('#Gender option:selected').val()) || 0);
    //data.append("MaritialStatus", $('#MaritialStatus option:selected').val());
    //data.append("Father", $('#Father').val().trim());
    //data.append("Mother", $('#Mother').val().trim());
    //data.append("Spouse", $('#Spouse').val().trim());
    //data.append("Contact1", $('#Contact1').val().trim());
    //data.append("Contact2", $('#Contact2').val().trim());
    //data.append("Email", $('#Email').val().trim());
    //data.append("PresentAdd", $('#PresentAdd').val().trim());
    //data.append("PermanentAdd", $('#PermanentAdd').val().trim());
    //data.append("ContactAdd", $('#ContactAdd').val().trim());
    //data.append("OwnershipSourceId", parseInt($('#OwnershipSourceId option:selected').val()) || 0);
    //data.append("OwnerType", $('#OwnerType option:selected').val());
    //data.append("BuildingTypeId", parseInt($('#BuildingTypeId option:selected').val()) || 0);
    //data.append("AmountOfLand", parseFloat($('#AmountOfLand').val()) || 0);
    //data.append("TotalFloor", parseFloat($('#TotalFloor').val()) || 0);
    //data.append("EachFloorAre", parseFloat($('#EachFloorArea').val()) || 0);
    //data.append("TotalFla", parseInt($('#TotalFlat').val()) || 0);
    //data.append("HoldersFlatNumber", parseInt($('#TotalFlat').val()) || 0);
    //data.append("PreviousDueTax", parseFloat($('#PreviousDueTax').val()) || 0);
    //alert('list started');
    //debugger;
    //// var index = 0;
    //// for (var dd of list) {
    ////     var falt_data = dd;
    ////     ///formData.append("HolderFlatList[" + index + "]", pair.key);
    ////     formData.append("HolderFlatList[" + index + "].HolderFlatId", falt_data.HolderFlatId);
    ////     formData.append("HolderFlatList[" + index + "].FlorNo", falt_data.FlorNo);
    ////     formData.append("HolderFlatList[" + index + "].FlatNo", falt_data.FlatNo);
    ////     formData.append("HolderFlatList[" + index + "].FlatArea", falt_data.FlatArea);
    ////     formData.append("HolderFlatList[" + index + "].OwnOrRent", falt_data.OwnOrRent);
    ////     formData.append("HolderFlatList[" + index + "].SelfOwned", falt_data.SelfOwned);
    ////     formData.append("HolderFlatList[" + index + "].MonthlyRent", falt_data.MonthlyRent);
    ////     formData.append("HolderFlatList[" + index + "].OwnerName", falt_data.OwnerName);
    ////     index++;
    ////}
    ////for (var i = 0; i < list.length; i++) {
    ////    formData.append("HolderFlatList[" + i + "].HolderFlatId", list.HolderFlatId);
    ////    formData.append("HolderFlatList[" + i + "].FlorNo", list.FlorNo);
    ////    formData.append("HolderFlatList[" + i + "].FlatNo", list.FlatNo);
    ////    formData.append("HolderFlatList[" + i + "].FlatArea", list.FlatArea);
    ////    formData.append("HolderFlatList[" + i + "].OwnOrRent", list.OwnOrRent);
    ////    formData.append("HolderFlatList[" + i + "].SelfOwned", list.SelfOwned);
    ////    formData.append("HolderFlatList[" + i + "].MonthlyRent", list.MonthlyRent);
    ////    formData.append("HolderFlatList[" + i + "].OwnerName", list.OwnerName);
    ////}
    //data.append("HolderFlatList", JSON.stringify(list));
    //// //data.append("image_file", $("#image_file").get(0).files[0]);
    //// //data.append("document_file_1", $("#document_file_1").get(0).files[0]);
    //// //data.append("document_file_2", $("#document_file_2").get(0).files[0]);
});
