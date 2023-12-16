namespace DesignPlatform.Areas.Admin.ViewModels.AdminContactUsViewModels
{
    public class AdminContactUsEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }

        public string SuccessMessage { get; set; }
    }
}
