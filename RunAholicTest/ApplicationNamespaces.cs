global using Microsoft.AspNetCore.Mvc.Testing;
global using Microsoft.VisualStudio.TestPlatform.TestHost;
global using Xunit;
global using System.Threading.Tasks;
global using System.Net.Http.Json;
global using System.Collections.Generic;
global using System.Linq;
global using Microsoft.Extensions.DependencyInjection;
global using Newtonsoft.Json;
global using System;
global using System.Net;
global using FluentAssertions;

// Local Test API
global using RunAholicTest.Helpers;
global using RunAholicTest.Fakes;
global using RunAholicTest.IntegrationTesting;
global using RunAholicTest.Service;

// Local API
global using RunAholicAPI.Configuration;
global using RunAholicAPI.DataContext;
global using RunAholicAPI.Models;
global using RunAholicAPI.Repositories;
global using RunAholicAPI.Services;
global using RunAholicAPI.GraphQL;