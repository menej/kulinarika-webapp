﻿using KulinarikaApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace KulinarikaApp.Authorization.RecipeAuthorization;

public class RecipeModeratorAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Recipe>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        OperationAuthorizationRequirement requirement,
        Recipe? recipe)
    {
        // Checking if the User is logged in or that recipe exists.
        if (context.User == null || recipe == null)
            return Task.CompletedTask;
        
        // Checking if the operations are in order
        if (requirement.Name != Constants.DeleteOperationName)
        {
            return Task.CompletedTask;
        }
        
        if (context.User.IsInRole(Constants.ModeratorRole))
            context.Succeed(requirement);

        return Task.CompletedTask;
        
    }
}