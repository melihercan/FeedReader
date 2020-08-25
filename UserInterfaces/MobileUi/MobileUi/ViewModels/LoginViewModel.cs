﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileUi.ViewModels
{
    [QueryProperty("SchemesJson", "schemesjson")]
    public class LoginViewModel : BaseViewModel
    {
        private string _schemesJson;
        public string SchemesJson
        {
            get => _schemesJson;
            set 
            {
                var schemesJson = Uri.UnescapeDataString(value);
                SetProperty(ref _schemesJson, schemesJson);
                _schemes = JsonSerializer.Deserialize<string[]>(schemesJson);
            }
        }

        private string[] _schemes { get; set; }


    }
}
