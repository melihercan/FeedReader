using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileUi.ViewModels
{
    [QueryProperty("SchemesJson", "schemesjson")]
    public class LoginViewModel : BaseViewModel
    {
        private ObservableCollection<string> _schemeItems;
        public ObservableCollection<string> SchemeItems 
        { 
            get => _schemeItems; 
            set
            {
                _schemeItems = value;
                OnPropertyChanged(nameof(SchemeItems));
            }
        }

        private string _schemesJson;
        public string SchemesJson
        {
            get => _schemesJson;
            set 
            {
                var schemesJson = Uri.UnescapeDataString(value);
                SetProperty(ref _schemesJson, schemesJson);
                var schemes = JsonSerializer.Deserialize<string[]>(schemesJson);
                SchemeItems = new ObservableCollection<string>(schemes.ToList());
            }
        }
    }
}
