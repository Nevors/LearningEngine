﻿using LearningEngine.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.Interfaces.PipelinePermissions
{
    public interface IPipelinePermissionCommand
    {
        int UserId { get; }

        int ObjectId { get; }

        ObjectType ObjectType { get; }
    }
}
