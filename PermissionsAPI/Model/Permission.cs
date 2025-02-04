namespace PermissionsAPI.Model
{
    public class Permission
    {
        public int Id { get; set; }
        public string NameEmployee { get; set; }
        public string LastNameEmployee { get; set; }
        // Propiedad de navegación
        public PermissionType PermissionType { get; set; }
        public int PermissionTypeId { get; set; }
        public DateTime PermissionDate { get; set; }

       
    }
}
