﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFriendOpenAPI.Extension;

// https://stackoverflow.com/questions/18924996/logging-request-response-messages-when-using-httpclient
public class LoggingHandler : DelegatingHandler
{
    public LoggingHandler(HttpMessageHandler innerHandler)
        : base(innerHandler)
    {
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
        
        Console.WriteLine("Request:");
        Console.WriteLine(request.ToString());
        if (request.Content != null)
        {
            Console.WriteLine(request.Content.ReadAsStringAsync().Result);
        }
        Console.WriteLine();

        Console.WriteLine("Response:");
        Console.WriteLine(response.ToString());
        if (response.Content != null)
        {
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
        Console.WriteLine();

        return response;
    }
}