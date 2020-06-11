using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using WebUi.Models;

namespace WebUi.Pages
{
    public partial class AddFeed : ComponentBase
    {
        [CascadingParameter] 
        BlazoredModalInstance Modal { get; set; }


        private FeedUrl _feedUrl = new FeedUrl { };

        private void OnValidSubmit()
        {
            Modal.Close(ModalResult.Ok<FeedUrl>(_feedUrl));
        }
    }
}
