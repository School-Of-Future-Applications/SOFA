using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace SOFA.Models.ViewModels
{
    /**
     * A view model for a reusable delete confirmation
     * modal.
     */
    public class DeleteConfirmationViewModel
    {
        //The post action to be executed.
        public string DeleteAction { get; set; }

        //The controller containing the action method.
        //Optional.
        public string DeleteController { get; set; }

        //The route dictionary values to be passed to the post method.
        public RouteValueDictionary RouteValues { get; set; }

        //The text displayed in the header of the modal.
        public string ModalHeaderText { get; set; }
    }
}