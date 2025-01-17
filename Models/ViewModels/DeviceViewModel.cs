namespace DeviceManagementApplication.Models.ViewModels
{
    public class DeviceViewModel
    {
        public IEnumerable<Device> devices { get; set; }
        public IEnumerable<Category> categories { get; set; }
        public int? selectedCategoryId { get; set; }

        public string searchTerm { get; set; }
    }
}
