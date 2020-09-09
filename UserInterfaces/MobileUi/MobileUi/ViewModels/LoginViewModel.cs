using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;
using Application.UseCases;
using Domain.Entities;
using Ardalis.Result;

namespace MobileUi.ViewModels
{
    [QueryProperty("SchemesJson", "schemesjson")]
    public class LoginViewModel : BaseViewModel
    {
        private readonly IMediator _mediator;

        public LoginViewModel()
        {
            _mediator = Registry.ServiceProvider.GetService<IMediator>();
        }

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

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

        public ICommand LoginCommand => new Command(async () => 
        {
            var result = await _mediator.Send(new Login
            {
                User = new User
                {
                    Username = Username,
                    Password = Password,
                    RememberMe = false
                }
            });
            if (result.Status == ResultStatus.Ok)
            {
                var token = result.Value;

                await Shell.Current.GoToAsync("///feeds");
            }
        });

        public ICommand RegisterCommand => new Command(async () => 
        { 
            await Shell.Current.GoToAsync("/register"); 
        });



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
