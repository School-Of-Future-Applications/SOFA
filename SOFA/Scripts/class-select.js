$(document).ready(function () {

    //Select List handlers
    $(".department-select").change(function () {
        var deptId = $(this).val();
        if (deptId != "") {
            //Get courses and append to course select
            $.get(
                "/Course/CourseIndex_Json",
                { departmentId: deptId },
                function (data) {
                    console.log(data);
                    foreach (entry in data)
                    {
                        ;
                    }
                    
                }
            );
        }
    });

    $(".course-select").change(function () {
        console.log("Course Select")
    });

    $(".yearlevel-select").change(function () {
        console.log("Year Select")
    });
});