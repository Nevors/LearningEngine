﻿using LearningEngine.Application.PipelineBehaviors;
using LearningEngine.Application.PipelineValidators;
using LearningEngine.Domain.Interfaces;
using LearningEngine.Domain.Interfaces.PipelinePermissions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LearningEngine.Api.Extensions
{
    public static class DIForPipelineBehaviorValidatorExtension
    {
        private static string pipelineBehaviorInterfaceName = typeof(IPipelineBehavior<,>).Name;
        private static string mediatorIRequestName = typeof(IRequest<>).Name;

        public static void RegisterAllAssignableType<T>(this IServiceCollection services, Assembly commandQueryAssembly)
        {
            var interfacesAssembly = typeof(T).Assembly;
            var interfacesTypes = interfacesAssembly.GetTypes().Where(p => p.IsInterface == true)
                                    .Where(p => p.FullName.Contains(typeof(T).Namespace));

            foreach (var interfaceType in interfacesTypes)
            {
                var commandQueryTypes = commandQueryAssembly.GetTypes()
                                                            .Where(p => interfaceType.IsAssignableFrom(p)).ToArray();

                var pipelineBehaviorAssembly = typeof(PipelinePermissionCommandValidator<>).GetTypeInfo().Assembly;
                var pipelineBehaviorClassType = pipelineBehaviorAssembly.GetTypes()
                                      .FirstOrDefault(p => p.GetTypeInfo().ImplementedInterfaces
                                      .Where(inter => inter.Name == pipelineBehaviorInterfaceName
                                       && inter.GetGenericArguments()
                                      .Contains(interfaceType)).Any());

                foreach (var type in commandQueryTypes)
                {
                    if (type.IsInterface)
                    {
                        continue;
                    }

                    var genericArg = type.GetInterfaces().FirstOrDefault(inter => inter.Name == mediatorIRequestName)
                                                                                     .GenericTypeArguments[0];

                    services.AddTransient(typeof(IPipelineBehavior<,>).MakeGenericType(type, genericArg),
                                          pipelineBehaviorClassType.MakeGenericType(genericArg));
                }
            }
        }
    }
}
