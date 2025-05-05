namespace SearchProject.Domain.Entities
{
    /// <summary>
    /// Base entitty to store all common entities
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Created time of the entry
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// updated time of entry
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
