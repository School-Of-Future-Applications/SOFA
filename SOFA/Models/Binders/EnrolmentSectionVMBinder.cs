using SOFA.Models.ViewModels.EnrolmentViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using SOFA.Models.Prefab;

namespace SOFA.Models.Binders
{
    public class EnrolmentSectionVMBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var originalSectionId = bindingContext.ValueProvider.GetValue("OriginalSectionId").AttemptedValue;
            if (originalSectionId.Equals(PrefabSection.STUDENT_DETAILS))
                bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(StudentEnrolmentSectionViewModel));
            return base.BindModel(controllerContext, bindingContext);
        }
    }
}