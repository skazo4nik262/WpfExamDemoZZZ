namespace WpfExamDemoZZZ
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; } 

        public int RoleId { get; set; }

        public bool FirstSign { get; set; }

        public DateTime? LastVisit { get; set; } = DateTime.Now;

        public bool IsBlocked { get; set; }

        public int? ErrorCount { get; set; } = 0;

        public RoleModel Role { get; set; } = new();
    }
}