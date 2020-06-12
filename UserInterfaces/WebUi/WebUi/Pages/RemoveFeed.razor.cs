using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebUi.Pages
{
    public partial class RemoveFeed : ComponentBase
    {
        [CascadingParameter]
        BlazoredModalInstance Modal { get; set; }

        private void OnOk()
        {
            Modal.Close();
        }


    }
}
