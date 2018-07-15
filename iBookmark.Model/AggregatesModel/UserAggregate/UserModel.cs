namespace iBookmark.Model.AggregatesModel.UserAggregate
{
    using System.Collections.Generic;

    public class UserModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public IEnumerable<UserRoleModel> UserRoles { get; set; }
    }
}
