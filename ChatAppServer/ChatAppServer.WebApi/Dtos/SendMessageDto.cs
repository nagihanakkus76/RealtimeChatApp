namespace ChatAppServer.WebApi.Dtos
{
    public sealed class SendMessageDto
    {
        public Guid UserId { get; set; }
        public Guid ToUserId { get; set; }
        public string Message { get; set; }
    }
}
