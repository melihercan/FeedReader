﻿using Application.Interfaces;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddFeedSource(this IServiceCollection services)
        {
            services.AddSingleton<IFeedSource, FeedSource>();
            return services;
        }
    }
}
