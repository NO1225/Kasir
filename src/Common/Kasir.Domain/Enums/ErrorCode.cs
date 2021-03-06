namespace Kasir.Domain.Enums
{
    public enum ErrorCode
    {
        NoFile,
        UserDoesntExist,
        InvalidNewPassword,
        InvalidSupervisor,
        SupervosorDoesntExist,
        EmailAlreadyExists,
        EmailDoesntExist,
        EmailAndPasswordDoesntMatch,
        WrongPassword,
        NotFound,
        ThisUserDoesntHaveAProfile,
        TimeDurationCantBeLessThan30Min,
        EndDateMustBeLargerThanStartingDate,
        InvalidUserId,
        ValueCantBeLessThanZero,
        SomethingWentWrong,
        InvalidAccessToken,
        InvalidRefreshToken,
        ProfileAlreadyExist,
        InvalidFileType,
        FileIsTooLarge,
        ImageTooSmall,
        CanOnlyCancelPendingRequests,
        LeavesBalanceIsNotEnough,
        CanOnlyUpdatePendingRequests,
        DecliningRequestsNeedAReason,
        RequestTimeOverlab,
    }
}
