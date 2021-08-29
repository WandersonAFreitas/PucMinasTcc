namespace ApplicationCore.Helpers.Pagination
{
    public class GridSettings
    {
        public bool _search { get; set; }
        public int rows { get; set; }
        public int page { get; set; }
        public string sidx { get; set; }
        public string sord { get; set; }
        public Filter filters { get; set; }

    }
}
