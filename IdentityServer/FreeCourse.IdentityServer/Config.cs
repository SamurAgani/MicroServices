﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace FreeCourse.IdentityServer
{
    public static class Config
    {

        public static IEnumerable<ApiResource> apiResources => new ApiResource[]
        {
            new ApiResource("resource_catalog"){ Scopes ={ "catalog_fullpermission" } },
            new ApiResource("photo_stock"){ Scopes ={ "photo_stock_fullpermission" } },
            new ApiResource("resource_basket"){ Scopes ={ "basket_fullpermission" } },
            new ApiResource("resource_discount"){ Scopes ={ "discount_fullpermission" } },
            new ApiResource("resource_order"){ Scopes ={ "order_fullpermission" } },
            new ApiResource("resource_payment"){ Scopes ={ "payment_fullpermission" } },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.Email(),
                       new IdentityResources.OpenId(),
                       new IdentityResources.Profile(),
                       new IdentityResource(){ Name = "roles",DisplayName = "Roles",Description="User roles", UserClaims = new[]{"role" } }
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalog_fullpermission","Catalog API full controll"),
                new ApiScope("photo_stock_fullpermission","Photo Stock API full controll"),
                new ApiScope("basket_fullpermission","Basket api API full controll"),
                new ApiScope("discount_fullpermission","Discount api API full controll"),
                new ApiScope("order_fullpermission","Discount api API full controll"),
                new ApiScope("payment_fullpermission","Discount api API full controll"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientName = "Asp.Net Core MVC",
                    ClientId = "WebMvcClient",
                    ClientSecrets = {new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "catalog_fullpermission", "photo_stock_fullpermission",IdentityServerConstants.LocalApi.ScopeName }
                },
                 new Client
                {
                    ClientName = "Asp.Net Core MVC",
                    ClientId = "WebMvcClientForUser",
                    AllowOfflineAccess = true,
                    ClientSecrets = {new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { "basket_fullpermission","payment_fullpermission","discount_fullpermission","order_fullpermission",IdentityServerConstants.StandardScopes.Email,IdentityServerConstants.StandardScopes.OpenId,
                         IdentityServerConstants.StandardScopes.Profile,
                         IdentityServerConstants.StandardScopes.OfflineAccess,IdentityServerConstants.LocalApi.ScopeName,"roles"},
                    AccessTokenLifetime = 1*60*60,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds,
                    RefreshTokenUsage = TokenUsage.ReUse

                }
            };
    }
}