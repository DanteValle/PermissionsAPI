namespace PermissionsAPI.Model
{
    public class PermissionType
    {
        public int Id { get; set; }
        public string Description { get; set; }

        // Navegación
        public ICollection<Permission> Permissions { get; set; }
    }
}
