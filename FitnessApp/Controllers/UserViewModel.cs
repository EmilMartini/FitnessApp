
namespace FitnessTracker
{
    public class UserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string Email { get; set; }
        public string ActivitiesCount { get; set; }

        public UserViewModel(User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Age = user.Age.ToString();
            Email = user.Email;
            ActivitiesCount = user.Activities.Count.ToString();
        }
    }
}