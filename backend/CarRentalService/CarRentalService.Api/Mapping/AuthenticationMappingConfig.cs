﻿using CarRentalService.Application.Authentication.Commands.Register;
using Mapster;
using Microsoft.AspNetCore.Identity.Data;

namespace CarRentalService.Api.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();
        }
    }
}
