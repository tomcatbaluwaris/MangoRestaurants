namespace Mango.Web.Models
{
    public class ResponseDto
    {
        public Boolean IsSucess { get; set; }

        public object Result { get; set; }
        
        public string DisplayMessage { get; set; }
        
        public List<string> ErrorMessages { get; set; }
    }
}
