namespace MeasureBoostApi.Interface
{
    public class EstimateSaveResult
    {
        public bool IsSuccess { get; set; } = true;
        public string EstimateId { get; set; } = "";
        public string Message { get; set; } = "";
    }
}
