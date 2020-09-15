using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Application.UseCases;
using Xamarin.Forms;
using System.Text.Json;
using Ardalis.Result;

namespace MobileUi.ViewModels
{
    public class InitializingViewModel : BaseViewModel
    {

        internal override async Task OnViewAppearingAsync()
        {
            var mediator = Registry.ServiceProvider.GetService<IMediator>();
            var tokenResult = await mediator.Send(new GetToken());
     //await Task.Delay(2000);
            if (tokenResult.Status == ResultStatus.Ok)
            {
                if (tokenResult.Value == null) //// TODO: or expired
                {
                    var schemesResult = await mediator.Send(new GetAuthenticationSchemes());
                    if (schemesResult.Status == ResultStatus.Ok)
                    {
                        var schemesJson = JsonSerializer.Serialize(schemesResult.Value);
                        await Shell.Current.GoToAsync($"///login?schemesjson={schemesJson}");
                    }
                }
                else
                {
                    await Shell.Current.GoToAsync("///feeds");
                }
            }
        }
    }
}
