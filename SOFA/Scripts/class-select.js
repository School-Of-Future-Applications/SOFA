$(document).ready(function () {

    //Select List handlers
    $(".department-select").change(function () {
        var deptId = $(this).val();
        var $courseSelect = $(".course-select");
        $courseSelect.empty();
        $courseSelect.append("<option>Select a Course</option>");

        if (deptId != "") {
            //Get courses and append to course select
            $.get(
                "/Course/CourseIndex_Json",
                { departmentId: deptId },
                function (data) {
                    console.log(data);
                    if (data.Success == null) {
                        
                        $courseSelect.empty();
                        $courseSelect.append("<option>Select a Course</option>");
                        $.each(data, function (i, entry) {
                            $courseSelect.append("<option value=\"" + entry.Id + "\">" + entry.Name + "</option>");
                        });

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