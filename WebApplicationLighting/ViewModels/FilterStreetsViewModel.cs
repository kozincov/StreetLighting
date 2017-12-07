namespace WebApplicationLighting.ViewModels
{
    public class FilterStreetsViewModel
    {
        public FilterStreetsViewModel(string streetname)
        {
            SelectedStreetName = streetname;
        }
        public string SelectedStreetName { get; set; }
    }
}
