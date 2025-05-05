namespace SearchProject.Application.Dtos
{
    public class UserDto
    {
        /// <summary>
        /// unique user id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// user name
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// email of the user
        /// </summary>
        public string Email { get; set; }
    }
}
