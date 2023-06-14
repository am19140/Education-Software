namespace Education_Software.Models
{
    public class ProgressViewModel
    {
        public IEnumerable<ProgressModel> progress_table { get; set; }

        public StatisticsModel statistics_table { get; set; }
    }
}