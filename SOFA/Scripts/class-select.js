$(document).ready(function () {

    //Select List handlers
    $(".department-select").change(function () {
        var deptId = $(this).val();
        var $courseSelect = $(".course-select");
        $courseSelect.empty();
        $courseSelect.append("<option value>Select a Course</option>");
        $(".yearlevel-select").empty();
        $(".yearlevel-select").append("<option value>Select a Year Level</option>");
        if (deptId != "") {
            //Get courses and append to course select
            $.get(
                "/Course/CourseIndex_Json",
                { departmentId: deptId },
                function (data) {
                    if (data.Success == null) {                        
                        $courseSelect.empty();
                        $courseSelect.append("<option value>Select a Course</option>");
                        $.each(data, function (i, entry) {
                            $courseSelect.append("<option value=\"" + entry.Id + "\">" + entry.Name + "</option>");
                        });
                    }    
                }
            );
        } 
    });

    $(".course-select").change(function () {
        var courseId = $(this).val();
        var $yearLevelSelect = $(".yearlevel-select");
        $yearLevelSelect.empty();
        $yearLevelSelect.append("<option value>Select a Year Level</option>");

        if (courseId != "") {
            //Get year levels and append to select
            $.get(
                "/ClassBase/ClassBaseYearLevels_JSON",
                { courseId : courseId},
                function (data) {
                    $yearLevelSelect.empty();
                    $yearLevelSelect.append("<option value>Select a Year Level</option>");
                    $.each(data, function (i, entry) {
                        $yearLevelSelect.append("<option value=\"" + entry + "\">" + entry + "</option>");
                    });
                }
            );
        }
    });

    $(".yearlevel-select").change(function () {
        console.log("Year Select");
    });
});