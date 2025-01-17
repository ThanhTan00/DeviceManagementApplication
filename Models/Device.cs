namespace DeviceManagementApplication.Models
{
    public class Device
    {
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public int categoryId {get; set; }
        public string status { get; set; }
        public DateTime dateOfEntry {get; set; }

    }
}
