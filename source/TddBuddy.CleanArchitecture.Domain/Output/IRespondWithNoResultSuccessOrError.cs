﻿namespace TddBuddy.CleanArchitecture.Domain.Output
{
    public interface IRespondWithNoResultSuccessOrError<in TError> : IRespondWith<TError>
    {
        void Respond();
    }
}