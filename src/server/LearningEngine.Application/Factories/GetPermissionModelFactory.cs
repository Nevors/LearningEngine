﻿using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Application.Factories
{
    public class GetPermissionModelFactory : IGetPermissionModelFactory
    {
        private readonly Dictionary<ObjectType, Func<int, IRequest<ThemeDto>>> themeIdQueryGetter;
        public GetPermissionModelFactory()
        {
            themeIdQueryGetter = new Dictionary<ObjectType, Func<int, IRequest<ThemeDto>>>();
        }
        public IRequest<ThemeDto> GetModel(int objectId, ObjectType objectType)
        {
            return themeIdQueryGetter[objectType].Invoke(objectId);
        }
        public void AddQuery(ObjectType objectType, Func<int, IRequest<ThemeDto>> func)
        {
            themeIdQueryGetter.Add(objectType, func);
        }
    }
}
