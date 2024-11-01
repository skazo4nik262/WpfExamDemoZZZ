namespace WpfExamDemoZZZ
{
    public partial class User
    {
        public int Id { get; set; }

        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int RoleId { get; set; }

        public bool FirstSign { get; set; } = false;

        public virtual Role Role { get; set; } = null!;
    }
}