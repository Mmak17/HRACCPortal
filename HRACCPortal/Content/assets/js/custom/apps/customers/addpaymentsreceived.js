﻿"use strict";

// Class definition
var KTModalPaymentsReceivedsAdd = function () {
    var submitButton;
    var cancelButton;
    var closeButton;
    var validator;
    var form;
    var modal;

    // Init form inputs
    var handleForm = function () {
        // Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
        validator = FormValidation.formValidation(
            form,
            {
                fields: {
                    'CustomerName': {
                        validators: {
                            notEmpty: {
                                message: 'Customer name is required'
                            }
                        }
                    },
                    'InvoiceNumber': {
                        validators: {
                            regexp: {
                                regexp: /[a-zA-Z0-9]/,
                                message: 'Invoice Number must be alphanumeric',
                            },
                            stringLength: {
                                max: 20,
                                message: 'The Invoice Number must be less than 20 characters',
                            },
                            notEmpty: {
                                message: 'Invoice Number is required'
                            }
                        }
                    },
                    
                    
                    'InvoiceAmount': {
                        validators: {
                            regexp: {
                                regexp: /[a-zA-Z0-9]/,
                                message: 'Invoice Amount must be alphanumeric',
                            },
                            stringLength: {
                                max: 20,
                                message: 'The Invoice Amount must be less than 20 characters',
                            },
                            notEmpty: {
                                message: 'Invoice Amount is required'
                            }
                        }
                    },
                    'InvoiceDueDate': {
                        validators: {
                            notEmpty: {
                                message: 'Invoice DueDate is required'
                            }
                        }
                    }
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    bootstrap: new FormValidation.plugins.Bootstrap5({
                        rowSelector: '.fv-row',
                        eleInvalidClass: '',
                        eleValidClass: ''
                    })
                }
            }
        );

        // Revalidate country field. For more info, plase visit the official plugin site: https://select2.org/
        //$(form.querySelector('[name="CustomerName"]')).on('change', function() {
        //    // Revalidate the field when an option is chosen
        //    validator.revalidateField('CustomerName');
        //});

        // Action buttons
        submitButton.addEventListener('click', function (e) {
            e.preventDefault();
            /*debugger;*/
            // Validate form before submit
            if (validator) {
                validator.validate().then(function (status) {
                    console.log('validated!');

                    if (status == 'Valid') {
                        submitButton.setAttribute('data-kt-indicator', 'on');
                        /*debugger;*/
                        // Disable submit button whilst loading
                        var paymentsreceivedData = {};
                        paymentsreceivedData.CustomerIdFK = form.CustomerName.value;
                        paymentsreceivedData.InvoiceNumber = form.InvoiceNumber.value;
                        paymentsreceivedData.InvoiceAmount = form.InvoiceAmount.value;
                        paymentsreceivedData.InvoiceDueDate = form.InvoiceDueDate.value;
                        paymentsreceivedData.PaymentsReceivedId = form.PaymentsReceivedId.value;
                      //  positionData.PositionScopeVariant = form.PositionScopeVariant.value;
                      //  positionData.PurchaseOrderNo = form.PurchaseOrderNo.value;
                        var actv = $('input#Status').prop('checked');
                      //  positionData.Status = actv;
                        //  positionData.PurchaseRequisitionNo = form.PurchaseRequisitionNo.value;
                  //      paymentsreceivedData.InactiveDate = form.InactiveDate.value;
                  //      paymentsreceivedData.InactiveReason = form.InactiveReason.value;
                        debugger;
                        if (actv == false && form.InactiveReason.value == "") {
                            $("#divreason").show();
                            submitButton.setAttribute('data-kt-indicator', 'off');
                            return false;
                        } else {
                            $("#divreason").hide();
                        }
                        if (actv == false && form.InactiveDate.value == "") {
                            $("#divdate").show();
                            submitButton.setAttribute('data-kt-indicator', 'off');
                            return false;
                        } else {
                            $("#divdate").hide();
                        }
                        
                        console.log("ConsultantObj:" + JSON.stringify(paymentsreceivedData));

                        $.ajax({
                            type: "POST",
                            //url: '@Url.Action("AddConsultant", "Consultant")',
                            url: '/PaymentsReceived/AddPaymentsReceived',
                            data: '{prmodel: ' + JSON.stringify(paymentsreceivedData) + '}',
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            success: function (response) {
                                submitButton.removeAttribute('data-kt-indicator');

                                // alert("Data has been added successfully.");
                                if (response.message == "Success") {
                                    Swal.fire({
                                        text: "Form has been successfully submitted!",
                                        icon: "success",
                                        buttonsStyling: false,
                                        confirmButtonText: "Ok, got it!",
                                        customClass: {
                                            confirmButton: "btn btn-primary"
                                        }
                                    }).then(function (result) {
                                        if (result.isConfirmed) {
                                            // Hide modal
                                            modal.hide();

                                            // Enable submit button after loading
                                            submitButton.disabled = false;

                                            // Redirect to customers list page
                                            window.location = form.getAttribute("data-kt-redirect");
                                        }
                                    });
                                } else {
                                    console.log(response.message);
                                    Swal.fire({
                                        text: response.message,
                                        icon: "error",
                                        buttonsStyling: false,
                                        confirmButtonText: "Ok, got it!",
                                        customClass: {
                                            confirmButton: "btn btn-primary"
                                        }
                                    });
                                }
                            },
                            error: function (jqXHR, textStatus, errorThrown) {
                                submitButton.removeAttribute('data-kt-indicator');

                                console.log("Custom error : " + jqXHR.responseText + " Status: " + textStatus + " Http error:" + errorThrown);

                                Swal.fire({
                                    text: "Custom error : " + jqXHR.responseText + " Status: " + textStatus + " Http error:" + errorThrown,
                                    icon: "error",
                                    buttonsStyling: false,
                                    confirmButtonText: "Ok, got it!",
                                    customClass: {
                                        confirmButton: "btn btn-primary"
                                    }
                                });

                            }
                        });
                    } else {
                        Swal.fire({
                            text: "Sorry, looks like there are some errors detected, please try again.",
                            icon: "error",
                            buttonsStyling: false,
                            confirmButtonText: "Ok, got it!",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        });
                    }
                });
            }
        });

        cancelButton.addEventListener('click', function (e) {
            e.preventDefault();

            Swal.fire({
                text: "Are you sure you would like to cancel?",
                icon: "warning",
                showCancelButton: true,
                buttonsStyling: false,
                confirmButtonText: "Yes, cancel it!",
                cancelButtonText: "No, return",
                customClass: {
                    confirmButton: "btn btn-primary",
                    cancelButton: "btn btn-success"
                }
            }).then(function (result) {
                if (result.value) {
                    form.reset(); // Reset form	
                    modal.hide(); // Hide modal				
                } else if (result.dismiss === 'cancel') {
                    Swal.fire({
                        text: "Your form has not been cancelled!.",
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "Ok, got it!",
                        customClass: {
                            confirmButton: "btn btn-primary",
                        }
                    });
                }
            });
        });

        closeButton.addEventListener('click', function (e) {
            e.preventDefault();

            Swal.fire({
                text: "Are you sure you would like to cancel?",
                icon: "warning",
                showCancelButton: true,
                buttonsStyling: false,
                confirmButtonText: "Yes, cancel it!",
                cancelButtonText: "No, return",
                customClass: {
                    confirmButton: "btn btn-primary",
                    cancelButton: "btn btn-success"
                }
            }).then(function (result) {
                if (result.value) {
                    form.reset(); // Reset form	
                    modal.hide(); // Hide modal				
                } else if (result.dismiss === 'cancel') {
                    Swal.fire({
                        text: "Your form has not been cancelled!.",
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "Ok, got it!",
                        customClass: {
                            confirmButton: "btn btn-primary",
                        }
                    });
                }
            });
        })
    }

    return {
        // Public functions
        init: function () {
            // Elements
            modal = new bootstrap.Modal(document.querySelector('#kt_modal_add_paymentsreceived'));

            form = document.querySelector('#kt_modal_add_paymentsreceived_form');
            submitButton = form.querySelector('#kt_modal_add_paymentsreceived_submit');
            cancelButton = form.querySelector('#kt_modal_add_paymentsreceived_cancel');
            closeButton = form.querySelector('#kt_modal_add_paymentsreceived_close');

            handleForm();
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTModalPaymentsReceivedsAdd.init();
});