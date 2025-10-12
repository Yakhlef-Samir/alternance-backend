namespace Alternance.Domain.Enum
{
    public enum CentralizedEnum { }
    public enum UserType
    {
        Student = 0,
        Company = 1
    }
    public enum Status
    {
        Active = 0,
        Closed = 1,
        Draft = 2
    }
    public enum ContractType
    {
        Alternance = 0,
        Stage = 1,
        CDI = 2
    }
    public enum ApplicationStatus
    {
        Pending = 0,
        Reviewed = 1,
        Accepted = 2,
        Rejected = 3
    }
}