// .NET
global using System;
global using Microsoft.Extensions.Options;

// Local
global using RunAholicAPI.Configuration;
global using RunAholicAPI.DataContext;
global using RunAholicAPI.Models;
global using RunAholicAPI.Repositories;
global using RunAholicAPI.Services;
global using RunAholicAPI.GraphQL;

// Validation
global using RunAholicAPI.Validation;
global using FluentValidation;
global using FluentValidation.AspNetCore;

// Mongo
global using MongoDB.Bson;
global using MongoDB.Bson.Serialization.Attributes;
global using MongoDB.Driver;

// JWT
global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using Microsoft.AspNetCore.Authorization;