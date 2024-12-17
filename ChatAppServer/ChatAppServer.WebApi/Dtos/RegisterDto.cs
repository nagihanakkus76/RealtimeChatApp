namespace ChatAppServer.WebApi.Dtos
{
    public sealed class RegisterDto 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IFormFile File { get; set; }

    } 
    
}
