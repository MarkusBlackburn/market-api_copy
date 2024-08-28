namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50;

        public int PageIndex { get; set; } = 1;

        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }


        private List<string> _names =[] ;

		public List<string> Names
		{
			get => _names;
			set 
			{ 
				_names = value.SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList();  
			}
		}

        private List<string> _codes = [];

        public List<string> Codes
        {
            get => _codes;
            set
            {
                _codes = value.SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList();
            }
        }

        private string? _search;

        public string? Search
        {
            get => _search ?? "";
            set => _search = value?.ToLower();
        }


        public string? Sort { get; set; }
    }
}
