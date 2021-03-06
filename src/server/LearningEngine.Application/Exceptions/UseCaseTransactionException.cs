﻿using LearningEngine.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Application.Exceptions
{
    public class UseCaseTransactionException : Exception
    {
        public UseCaseTransactionException(Exception exception)
            : base(ExceptionDescriptionConstants.TransactionInterrupted, exception)
        {
        }
    }
}
