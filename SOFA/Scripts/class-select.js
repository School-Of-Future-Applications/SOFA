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
            $.ajax({
                url : "/Course/CourseIndex_Json",
                data: { departmentId: deptId },
                type : "GET",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success :
                function (data) {
                    console.log(data);
                    if (data.Success == null) {
                        $courseSelect.empty();
                        $courseSelect.append("<option value>Select a Course</option>");
                        $.each(data, function (i, entry) {
                            $courseSelect.append("<option value=\"" + entry.Id + "\">" + entry.Name + "</option>");
                        });
                    }    
                }
            });
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
        
    });

    $(".view-timetable").click(function (e) {
        $(this).attr("clicked", "true");
        
    });

    $(".section-form").submit(function (e) {
        //If displaying timetable, ajax it
        //Else submit as per normal
        if ($(".view-timetable").attr("clicked") == "true") {
            //Reset clicked attribute
            $(".view-timetable").attr("clicked", "false");
            //Get the data, check if validated
            if ($(this).valid()) {
                var courseId = $(".course-select").val();
                var yearLevel = $(".yearlevel-select").val();
                 //Get the view and put it in the div
                $.get(
                    "/Enrolment/TimetableDisplay",
                    { courseId: courseId, yearLevel: yearLevel },
                    function(data)
                    {
                        $("#timetable-display").html(data);
                    }
                );
            }
            
            return false; //So the page doesn't change
        } else {
            if ($("#SelectedClassId").val() > 0)
            {

            } else {
                return false;
            }
            
        }
        
        
    });

    $(document).on("click", ".class-select-btn", function (e) {
        var cid = $(this).attr("id");
        $("#SelectedClassId").val(cid);
        $(".class-select-btn").attr("class", "class-select-btn btn btn-primary");
        $(".class-select-btn").text("Select");
        $(this).attr("class", "class-select-btn btn btn-success");
        $(this).text("Selected");
        $("tr").attr("class", "");
        $(this).closest("tr").attr("class", "success");
    });
});