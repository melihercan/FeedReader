using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebUi.Pages
{
    public partial class RemoveFeed
    {
        [CascadingParameter]
        BlazoredModalInstance Modal { get; set; }

        private void OnOk()
        {
            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$ CLOSING MODAL!!!");
            Modal.Close();
        }
    }
}
