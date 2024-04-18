namespace CourseProjectNew.Common.Filters
{
    public class Filter
    {
        public List<FilterValue> FilterValues { get; set; }
        public bool UseAndOperator { get; set; } = true;
    }
}
