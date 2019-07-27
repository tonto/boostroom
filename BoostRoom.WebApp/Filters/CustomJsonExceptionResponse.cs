namespace BoostRoom.WebApp.Filters
{
    public class CustomJsonExceptionResponse
    {
        public string ErrorMessage { get; set; }
        
        public string Exception { get; set; }
        
        public int Status { get; set; }
    }
}
