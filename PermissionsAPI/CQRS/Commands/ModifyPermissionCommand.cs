namespace PermissionsAPI.CQRS.Commands
{
    public class ModifyPermissionCommand
    {
        public int PermissionId { get; set; }
        public string NameEmploye { get; set; }
        public string LastNameEmployee { get; set; }
        public int PermissionTypeId { get; set; }
    }
}
