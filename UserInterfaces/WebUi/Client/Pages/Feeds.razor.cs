using Application.UseCases;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored;
using Blazored.Modal;
using Blazored.Modal.Services;

namespace WebUi.Client.Pages
{
    public partial class Feeds
    {
        private IEnumerable<FeedChannel> _feedChannels;

        [Inject]
        private ILogger<Feeds> _logger { get; set; }

        [Inject]
        private IMediator _mediator { get; set; }

        [Inject]
        private IModalService _modalService { get; set; }

        protected override async Task OnInitializedAsync()
        {

            var resultFeedChannels = await _mediator.Send(new GetAllFeeds { });
            if (resultFeedChannels.Success)
            {
                _feedChannels = resultFeedChannels.Value;
            }
            else
            {
                _logger.LogError(resultFeedChannels.Error);
                _modalService.Show<Feeds>(resultFeedChannels.Error);
            }

            await base.OnInitializedAsync();
        }
    }

}
