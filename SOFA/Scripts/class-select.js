$(document).ready(function () {


    $("#SelectedClassId").val("");
    
    //Select List handlers
    $(".department-select").change(function () {
        var deptId = $(this).val();
        var $courseSelect = $(".course-select");
        $courseSelect.empty();
        $courseSelect.append("<option value>Select a Course</option>");
        $(".yearlevel-select").empty();
        $(".yearlevel-select").append("<option value>Select a Year Level</option>");
        if (deptId != "") {
            $courseSelect.empty();
            $courseSelect.append("<option value>Loading courses...</option>");
            //Get courses and append to course select
            $.ajax({
                url : "/Course/CourseIndex_Json",
                data: { departmentId: deptId },
                type : "GET",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success :
                function (data) {
                    $courseSelect.empty();
                    if (data.Success == null) {                        
                        $courseSelect.append("<option value>Select a Course</option>");
                        $.each(data, function (i, entry) {
                            $courseSelect.append("<option value=\"" + entry.Id + "\">" + entry.Name + "</option>");
                        });
                    } else {
                        $courseSelect.append("<option value>No courses available...</option>");
                    }
                }
            });
        } 
    });

    $(".course-select").change(function () {
        var courseId = $(this).val();
        var $yearLevelSelect = $(".yearlevel-select");
        

        if (courseId != "") {
            $yearLevelSelect.empty();
            $yearLevelSelect.append("<option value>Loading Year Levels...</option>");
            //Get year levels and append to select
            $.get(
                "/ClassBase/ClassBaseYearLevels_JSON",
                { courseId : courseId},
                function (data) {
                    $yearLevelSelect.empty();
                    if (data["Success"] == false) {
                        $yearLevelSelect.append("<option value>No classes available</option>");
                    } else {                        
                        $yearLevelSelect.append("<option value>Select a Year Level</option>");
                        $.each(data, function (i, entry) {
                            $yearLevelSelect.append("<option value=\"" + entry + "\">" + entry + "</option>");
                    });
                    }
                    
                }
            );
        } else {
            $yearLevelSelect.empty();
            $yearLevelSelect.append("<option value>Select a year level</option>");
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
                $(".view-timetable").button("loading");
                var courseId = $(".course-select").val();
                var yearLevel = $(".yearlevel-select").val();
                 //Get the view and put it in the div
                $.get(
                    "/Enrolment/TimetableDisplay",
                    { courseId: courseId, yearLevel: yearLevel },
                    function(data)
                    {
                        $(".view-timetable").button("reset");
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
        if ($(this).text() == "Select") {
            var cid = $(this).attr("id");
            $("#SelectedClassId").val(cid);
            $(".class-select-btn").attr("class", "class-select-btn btn btn-primary");
            $(".class-select-btn").text("Select");
            $(this).attr("class", "class-select-btn btn btn-success");
            $(this).text("Selected");
            $("tr").attr("class", "");
            $(this).closest("tr").attr("class", "success");
        } else if ($(this).text() == "Selected") {
            $("#SelectedClassId").val("");
            $(this).text("Select");
            $(this).attr("class", "class-select-btn btn btn-primary");
            $(this).closest("tr").attr("class", "");
        }
        
    });
});