$(document).ready(function () {

    //Select List handlers
    $(".department-select").change(function () {
        var deptId = $(this).val();
        if (deptId != "") {
            //Get courses and append to course select
        }
    });

    $(".course-select").change(function () {
        console.log("Course Select")
    });

    $(".yearlevel-select").change(function () {
        console.log("Year Select")
    });
});