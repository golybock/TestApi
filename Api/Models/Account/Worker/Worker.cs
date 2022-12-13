namespace Api.Models.Account.Worker;

public class Worker : Account
{
    public Worker(int id, string login, string password, string token, DateTime dateTimeOfRegistration, AccountData data, List<AccountPhoto> accountPhotos, Role role, DateTime startDate, DateTime endDate) : base(id, login, password, token, dateTimeOfRegistration, data, accountPhotos)
    {
        Role = role;
        StartDate = startDate;
        EndDate = endDate;
    }

    public Worker(Role role, DateTime startDate, DateTime endDate)
    {
        Role = role;
        StartDate = startDate;
        EndDate = endDate;
    }

    public Worker()
    {
        Role = new Role();
    }
    
    public Role Role { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}