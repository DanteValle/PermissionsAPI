namespace PermissionsAPI.CQRS.Commands
{
    public class RequestPermissionCommand
    {
        public int Id { get; set; }
        public int PermissionTypeId { get; set; }
        public string NameEmploye { get; set; }
        public string LastNameEmployee { get; set; }
    }
}
