/*
 *  School Of Future Applications
 *
 *  Copyright (C) 2014  Terminal Coding
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

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
            else if (originalSectionId.Equals(PrefabSection.COURSE_SELECT))
                bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(CourseEnrolmentSectionViewModel));

            return base.BindModel(controllerContext, bindingContext);
        }
    }
}