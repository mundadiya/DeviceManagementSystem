function bindcity() {
    //alert($.session.get("loginid"));
    $.ajax({

        type: 'GET',
        contentType: "application/json; charset=utf-8",
        url: 'http://localhost:16120/api/Tripentry/GetVehicleByVehicleTypeID',
        data: "VTID=" + $("#ddl_VehicleType").val(),
        async: false,

        success: function (response) {

            $(".cityitem").remove();
            $.each(response, function (i, item) {


                $('#myTable').append('<tr id=\"' + response[i]["VehicleNo"] + '\" onclick=\"findarea(' + response[i]["VID"] + ',this.id)\"><td style="display:none">' + response[i]["VID"] + '</td><td>' + response[i]["VehicleNo"] + '</td></tr>');


            });



        },
        error: function (response) {

            validationchk("Failed", "Oops Transaction Failed!!!")

        }
    });
    return false;
}
function findarea(a, b) {
    //bindpin(a);
    $('#hiddenareaid').val(a)
    $('#ddl_Vehicle').val(b)
    $('#myTable').hide();

}
function bindpin(cityid) {
    $('#Txt_pin').val("")
    $('#hiddenpinid').val("")
    $.ajax({

        type: 'GET',
        contentType: "application/json; charset=utf-8",
        url: apilink + 'api/Businesspage/Get_5_Business_City_Wise_PinCode',
        data: "CityID=" + cityid,
        async: false,

        success: function (response) {

            $(".picodeitem").remove();
            $.each(response, function (i, item) {


                $('#picodetable').append('<tr class=\"picodeitem\" onclick=\"findpin(' + response[i]["PINID"] + ',' + response[i]["PINCode"] + ')\"><td style="display:none">' + response[i]["PINID"] + '</td><td>' + response[i]["PINCode"] + '</td></tr>');


            });



        },
        error: function (response) {

            validationchk("Failed", "Oops Transaction Failed!!!")

        }
    });
    return false;
}
window.addEventListener('mouseup', function (event) {


    if (event.target.id != 'divstatecity') {
        if ($("#myTable").is(':visible')) {
            $('#hiddenareaid').val("")
            defaultcitybind();
        }
        else {

        }
    }

});
function defaultcitybind() {

    var pin = $("#ddl_Vehicle").val();
    if (pin.length > 0) {
        var input, filter, table, tr, td, i, txtValue;
        table = document.getElementById("myTable");
        tr = table.getElementsByTagName("tr");
        for (i = 0; i < tr.length; i++) {

            if (tr[i].style.display == "block") {

                td1 = tr[i].getElementsByTagName("td")[0]
                td2 = tr[i].getElementsByTagName("td")[1]
                $('#myTable').hide();
                $('#hiddenareaid').val(td1.innerHTML)
                $("#ddl_Vehicle").val(td2.innerHTML);
                bindpin(td1.innerHTML);
                break;
            }




        }
    }

}
function myFunction() {
    var pin = $("#ddl_Vehicle").val();


    if (pin.length > 2) {
        $('#myTable').show();
    }
    else {
        $('#myTable').hide();
    }

    var input, filter, table, tr, td, i, txtValue;
    var status = "False";
    input = document.getElementById("ddl_Vehicle");
    filter = input.value.toUpperCase();
    table = document.getElementById("myTable");
    tr = table.getElementsByTagName("tr");

    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[1];
        if (td) {
            txtValue = td.textContent || td.innerText;

            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "block";
                status = "True";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
    if (status == "False") {



    }

}
function displaycitylist() {
    myFunction();
    // $('#myTable').show();

}