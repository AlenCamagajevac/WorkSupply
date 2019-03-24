namespace WorkSupply.API.Responses
{
    public enum ErrorCodes
    {
        CouldNotCreateUser = 10,
        CouldNotLogInUser = 11,
        CouldNotGetUser = 12,
        CouldNotActivateUserAccount = 13,
        
        CouldNotCreateWorkLog = 20,
        CouldNotResolveWorkLog = 21,
        CouldNotGetWorkLog = 22,
        
        CouldNotChangeEmploymentStatus = 30
    }
}