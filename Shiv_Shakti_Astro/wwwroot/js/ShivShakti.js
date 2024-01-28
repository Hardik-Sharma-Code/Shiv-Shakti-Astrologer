$(function () {

    $('#contactForm').submit(function (e) {
        e.preventDefault();

        // Create a FormData object to handle file uploads
        var formData = new FormData(this);

        var fileInput = $('#inputFile')[0].files;

        if (fileInput.length > 0) {
            formData.append('CustomerPhoto', fileInput[0]);
        }
        // for Name

        var nameValue = $("#txtname").val();

        // Check if the name is not empty
        if (nameValue.trim() == '') {
            alert("Name cannot be empty. Please enter a valid name.");
            return;
        }

        // for Date Validation
        var datetimeValue = $("#datetimeInput").val();
        var datetimeRegex = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}$/;
        if (!datetimeRegex.test(datetimeValue)) {
            alert("Invalid datetime format. Please enter a valid date and time.");
            return;
        }
        $.ajax({
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: formData,
            contentType: false, // Important: prevent jQuery from setting the Content-Type
            processData: false, // Important: prevent jQuery from processing data
            success: function (result) {
                // handle success response
                alert("success");
                $('#addModal').modal('hide');
                location.reload();
            },
            error: function (result) {
                // handle error response
                alert(result);
            }
        });
    });
});

$(function () {

    $(".dchk").click(function () {
        var arr = [];
        $.each($("input[name='dietrychk']:checked"), function () {
            arr.push($(this).val());
        });
        $(".dietry-habbits-input").val(arr.join(", "));
    });

    $(".ppdchk").click(function () {
        var pparr = [];
        $.each($("input[name='ppdietrychk']:checked"), function () {
            pparr.push($(this).val());
        });
        $(".ppdietry-habbits-input").val(pparr.join(", "));
    });

    $(".edit").click(function () {
        var str = $(this).attr('id');
        var ret = str.split("_");
        $.ajax({
            url: '/Customer/editCustomer?Id= ' + ret[1],
            type: 'Post',
            data: "{Id:'" + ret[1] + "'}",
            contentType: false, // Important: prevent jQuery from setting the Content-Type
            processData: false, // Important: prevent jQuery from processing data
            success: function (result) {
                // handle success response
                $('#editCustomerContent').html(result);

                // Show the modal
                $('#editCustomer').modal('show');
                //alert(result.data);
            },
            error: function () {
                // handle error response
                alert("error");
            }
        });

    });


    $('.fltages').on('change', function () {
        $('#searchname').val($('#txtsearch').val());
        // Trigger form submission when a radio button is changed
        $('#applyFilterBtn').click();
    });

    $('#btnsearch').on('click', function () {
        // Trigger form submission when a radio button is changed
        $('#searchname').val($('#txtsearch').val());
        $('#applyFilterBtn').click();
    });

    $('#clearfilter').on('click', function () {
        // Trigger form submission when a radio button is changed
        $('#searchname').val();
        $('.fltages:checked').prop('checked', false);
        $('#applyFilterBtn').click();
    });

    
});

function confirmDelete() {
    // Display a confirmation dialog
    var result = confirm("Are you sure you want to delete this customer?");

    // Return true if the user clicks "OK" in the confirmation dialog, otherwise return false
    return result;
}

function toggleBoxContent() {
    var boxContent = document.querySelector('.box-content');
    var toggleIcon = document.getElementById('toggleIcon');

    if (boxContent.style.display === 'block' || boxContent.style.display === '') {
        boxContent.style.display = 'none';
        toggleIcon.textContent = '+';
    } else {
        boxContent.style.display = 'block';
        toggleIcon.textContent = '-';
    }
}
